using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LibEmoji;
using VK.API;
using VK.API.Data;
using Message = VK.API.Data.Message;

namespace VK.GenericUI
{
    public enum ControlState
    {
        Idle,
        Hover,
        Pressed,
        Active
    }

    public class MouseState
    {
        public static MouseState Empty = new MouseState();

        public Point Position;
        public bool Clicked;

        public MouseState() : this(-1, -1, false)
        { }

        public MouseState(int x = -1, int y = -1, bool clicked = false) : this(new Point(-1, -1), false)
        { }

        public MouseState(Point pos, bool clicked = false)
        {
            this.Position = pos;
            this.Clicked = clicked;
        }
    }

    public enum TextStyle
    {
        Text,
        BoldText,
        ItalicText,
        Link,
        Emoji
    }

    public class TextBlock
    {
        public int Start;
        public int Length;
        public int End;

        public TextStyle Style;

        public ControlState State;

        public String Fragment;

        public Point StartPoint = Point.Empty;
        public Point EndPoint = Point.Empty;

        public TextBlock(String str, int start, int len, TextStyle style = TextStyle.Text, ControlState state = ControlState.Idle)
        {
            this.Start = start;
            this.Length = len;
            this.End = start + len;
            this.Style = style;
            this.State = state;

            this.Fragment = str.Substring(start, len);
        }
    }

    public class EmojiBlock : TextBlock
    {
        public Bitmap Emoji;

        public EmojiBlock(String str, int start, int len, ControlState state = ControlState.Idle) : base(str, start, len, TextStyle.Emoji, state)
        {
            this.Emoji = LibEmoji.Emoji.ResolveEmoji(this.Fragment);
        }
    }

    public static class Drawer
    {
        public static Regex _URL = new Regex(@"(?:(?:https?|ftp)://)?(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:/\S*)?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public static Rectangle DrawString(Graphics g, String str, Font fnt, Brush brush, Rectangle viewport, StringFormat fmt)
        {
            if (fmt == null)
                fmt = StringFormat.GenericDefault;

            g.DrawString(str, fnt, brush, viewport, fmt);

            return viewport;
        }

        public static Rectangle DrawString(Graphics g, String str, Font fnt, Brush brush, Rectangle viewport, StringAlignment hAlign = StringAlignment.Center, StringAlignment vAlign = StringAlignment.Center)
        {
            StringFormat fmt = StringFormat.GenericDefault;
            fmt.Alignment = hAlign;
            fmt.LineAlignment = vAlign;

            g.DrawString(str, fnt, brush, viewport, fmt);

            return viewport;
        }

        public static void DrawChat(Graphics g, Dialog chat, Font fnt, ControlState state, int y = 0, int width = 200)
        {
            Color bg = Color.FromArgb(164, Color.White);

            if (state == ControlState.Hover)
            {
                bg = Color.FromArgb(182, Color.White);
            }
            else if (state == ControlState.Pressed)
            {
                bg = Color.FromArgb(196, Color.White);
            }

            g.FillRectangle(new SolidBrush(bg), 0, y, width, 50);

            if(chat == null)
                return;

            Message msg = chat.Message;
            if(msg == null)
                return;

            User usr = msg.User;

            if (usr != null)
            {
                Bitmap p = msg.GetPhoto();

                if (p == null)
                    p = usr.GetPhoto();

                if (p != null)
                    g.DrawImage(p, new Rectangle(1, y + 1, 48, 48));
            }

            DrawString(g, chat.Title, fnt, Brushes.Black, new Rectangle(55, y, width - 55, 50), StringAlignment.Near);

            if (state == ControlState.Active)
            {
                g.FillPolygon(new SolidBrush(Color.FromArgb(78, Color.White)), new []
                {
                    new Point(width, y),
                    new Point(width, y + 50),
                    new Point(width - 25, y + 25)
                });
            }
        }

        public static int DrawMessage(Graphics g, Message msg, Message prevMsg, Font fnt, ControlState state, int y = 0, int width = 500, int cHeight = 400, MouseState mouse = default(MouseState))
        {
            if (msg == null)
                return y;

            int height = 0;

            const int padding = 10;

            const int avatarSz = 48;

            int hWidth = width - avatarSz - 3 * padding;
            int msgWidth = width / 100 * 80;

            User usr = msg.Author;

            if (usr == null)
                return y;

            Size hSize = Size.Empty;
            if(prevMsg == null || prevMsg.Author != usr){
                int aX = padding;
                if (msg.Out)
                    aX = width - padding - avatarSz;

                Bitmap p = usr.GetPhoto();
                if (p != null && y > -avatarSz)
                    g.DrawImage(p, new Rectangle(aX, y, avatarSz, avatarSz));

                hSize = Size.Ceiling(g.MeasureString(usr.FullName, fnt, hWidth));

                int hX = 2 * padding + avatarSz;
                if (msg.Out)
                    hX = width - 2 * padding - avatarSz - hSize.Width;

                if(y > -hSize.Height)
                    DrawString(g, usr.FullName, fnt, Brushes.DimGray, new Rectangle(new Point(hX, y), hSize));
            }

            height += padding / 2 + hSize.Height;

            Size msgSize = Size.Ceiling(g.MeasureString(msg.Body, fnt, msgWidth));
            Size msgRectSize = Size.Add(msgSize, new Size(2 * padding, 2 * padding));

            int rX = 2 * padding + avatarSz + 5;
            if (msg.Out)
                rX = width - 2 * padding - avatarSz - msgRectSize.Width - 5;

            int rY = y + padding / 2 + hSize.Height;

            if (msg.Attachments != null && msg.Attachments.Length > 0)
            {
                int ax = 0, ay = 0;
                int mw = 0, mh = 0;
                foreach (Attachment a in msg.Attachments)
                {
                    Size asz = a.Measure(g, fnt, msgWidth - ax, true);

                    if (mh < asz.Height)
                        mh = asz.Height;

                    if (ax + asz.Width > msgWidth)
                    {
                        ax = 0;
                        ay += mh + padding;
                        mh = 0;
                    }
                    else
                        ax += asz.Width + padding;

                    if (mw < ax)
                        mw = ax;
                }

                msgRectSize = new Size(Math.Max(msgSize.Width, mw) + padding, msgSize.Height + ay + mh + 2 * padding);

                rX = 2 * padding + avatarSz + 5;
                if (msg.Out)
                    rX = width - 2 * padding - avatarSz - msgRectSize.Width - 5;

                rY = y + padding / 2 + hSize.Height;
            }

            Color msgRectColor = Color.White;

            if (msg.Crypted)
                msgRectColor = Color.Lime;

            if (!msg.Read)
            {
                msgRectColor = Color.DodgerBlue;

                if (msg.Crypted)
                    msgRectColor = Color.LimeGreen;
            }

            Brush msgRectBrush = new SolidBrush(Color.FromArgb(128, msgRectColor));

            if (rY > -msgRectSize.Height && rY < cHeight)
                g.FillRoundRectangle(msgRectBrush, new Rectangle(new Point(rX, rY), msgRectSize), padding / 2);

            int tX = rX;
            int tY = rY + padding;
            if (msg.Out)
                tX = rX + msgRectSize.Width;

            g.FillPolygon(msgRectBrush, new[]
            {
                new Point(tX, tY),
                new Point(tX, tY + 16),
                new Point(((msg.Out) ? tX + 10 : tX - 10), tY + 8)
            });

            if (y + padding + padding / 2 + hSize.Height > -msgSize.Height && y < cHeight)
            {
                Rectangle msgRectangle = new Rectangle(new Point(rX + padding, y + padding + padding/2 + hSize.Height), msgSize);

                /*if (msg.Body.Length > 256)
                {
                    DrawString(g, msg.Body, fnt, Brushes.Black, msgRectangle, StringAlignment.Near);
                }
                else
                {*/
                List<TextBlock> blocks;

                if (msg.RuntimeData.ContainsKey("TextBlocks"))
                {
                    blocks = (List<TextBlock>) msg.RuntimeData["TextBlocks"];
                }
                else
                {
                    blocks = new List<TextBlock>();

                    int blkend = 0;

                    MatchCollection matches = _URL.Matches(msg.Body);
                    if (matches.Count > 0)
                    {
                        foreach (Match m in matches)
                        {
                            String url = m.ToString();

                            int idx = msg.Body.IndexOf(url, StringComparison.Ordinal);

                            if (idx > blkend)
                            {
                                blocks.Add(new TextBlock(msg.Body, blkend, idx - blkend));
                            }

                            TextBlock blk = new TextBlock(msg.Body, idx, url.Length, TextStyle.Link);
                            blocks.Add(blk);

                            blkend = blk.End;
                        }
                    }

                    if (msg.Body.Length > blkend)
                    {
                        blocks.Add(new TextBlock(msg.Body, blkend, msg.Body.Length - blkend));
                    }

                    if (Emoji.FindEmojis(msg.Body).Count > 0)
                    {
                        List<TextBlock> nbl = new List<TextBlock>();

                        foreach (TextBlock b in blocks)
                        {
                            foreach (String em in Emoji.FindEmojis(b.Fragment))
                            {
                                int idx = b.Fragment.IndexOf(em, StringComparison.Ordinal);

                                if (idx > 0)
                                {
                                    nbl.Add(new TextBlock(msg.Body, b.Start, idx, b.Style, b.State));
                                }

                                nbl.Add(new EmojiBlock(msg.Body, b.Start + idx, em.Length, b.State));

                                if (idx + em.Length < b.Length)
                                {
                                    nbl.Add(new TextBlock(msg.Body, b.Start + idx + em.Length, b.Length - idx - em.Length, b.Style, b.State));
                                }
                            }
                        }

                        blocks = nbl;
                    }

                    Console.WriteLine(((msg.Body.Length > 32) ? msg.Body.Substring(0, 32) + "..." : msg.Body) + ": " + blocks.Count + " block(s)");

                    blocks.Sort((blk, blk2) => blk.Start.CompareTo(blk2.Start));

                    msg.RuntimeData.Add("TextBlocks", blocks);
                }

                if (blocks.Count > 16)
                {
                    DrawString(g, msg.Body, fnt, Brushes.Black, msgRectangle, StringAlignment.Near);
                }
                else {
                    int sX = rX + padding,
                        sY = y + padding + padding/2 + hSize.Height,
                        eX = msgSize.Width,
                        eY = msgSize.Height;

                    int cX = 0, cY = 0, cH = 0;

                    CharacterRange[] ranges = {new CharacterRange(0, 1)};
                    String _c;
                    Rectangle charRect = Rectangle.Empty;

                    int cP = 0;

                    using (StringFormat stringFormat = new StringFormat())
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        foreach (TextBlock blk in blocks)
                        {
                            blk.StartPoint = new Point(cX, cY);

                            if (blk is EmojiBlock)
                            {
                                Bitmap e = ((EmojiBlock) blk).Emoji;

                                if (e != null)
                                {
                                    g.DrawImage(e, new Rectangle(new Point(sX + cX + 2, sY + cY), new Size(16, 16)));

                                    if (cH < 16)
                                        cH = 16;

                                    cX += 20;
                                    if (cX > eX)
                                    {
                                        cX = 0;
                                        cY += cH;
                                    }
                                }

                                cP += blk.Length;
                            }
                            else
                            {
                                while (cP >= blk.Start && cP < blk.End)
                                {
                                    ranges[0].First = cP;

                                    stringFormat.SetMeasurableCharacterRanges(ranges);

                                    _c = msg.Body.Substring(cP, 1);

                                    charRect =
                                        Rectangle.Round(
                                            g.MeasureCharacterRanges(msg.Body, fnt, msgRectangle, stringFormat)[0]
                                                .GetBounds(g));

                                    if (cH < charRect.Height)
                                        cH = charRect.Height;

                                    Color c = Color.Black;
                                    if (blk.Style == TextStyle.Link)
                                    {
                                        c = Color.Blue;

                                        if (mouse != null)
                                        {
                                            int mpX = mouse.Position.X;
                                            int mpY = mouse.Position.Y;

                                            if (mpY >= sY + blk.StartPoint.Y && mpY <= sY + blk.EndPoint.Y &&
                                                mpX >= sX + blk.StartPoint.X &&
                                                mpX <= sX + blk.EndPoint.X)
                                            {
                                                blk.State = ControlState.Hover;

                                                if (mouse.Clicked)
                                                {
                                                    blk.State = ControlState.Pressed;
                                                    mouse.Clicked = false;
                                                }
                                            }
                                            else
                                            {
                                                blk.State = ControlState.Idle;
                                            }

                                            if (blk.State != ControlState.Idle)
                                            {
                                                if (blk.State == ControlState.Hover)
                                                    c = Color.DarkOrange;

                                                if (blk.State == ControlState.Pressed)
                                                {
                                                    c = Color.Red;

                                                    App.Platform.OpenUrl(msg.Body.Substring(blk.Start, blk.Length));
                                                }

                                                g.FillRectangle(Brushes.White, charRect);
                                            }
                                        }
                                        else
                                        {
                                            blk.State = ControlState.Idle;
                                            c = Color.Blue;
                                        }
                                    }

                                    g.DrawString(_c, fnt, new SolidBrush(c), sX + cX, sY + cY);

                                    if (blk.Style == TextStyle.Link)
                                        g.DrawLine(new Pen(c), sX + cX + 2, sY + cY + cH, sX + cX + charRect.Width + 2,
                                            sY + cY + cH);

                                    cX += charRect.Width;
                                    if (cX > eX || _c == "\n")
                                    {
                                        cX = 0;
                                        cY += cH;

                                        if (_c != "\n")
                                            cH = 0;
                                    }
                                    cP++;
                                }
                            }

                            blk.EndPoint = new Point(cX + charRect.Width + 2, cY + cH);
                        }
                    }
                }
            }

            height += msgRectSize.Height + padding;

            if (msg.Attachments != null && msg.Attachments.Length > 0)
            {
                int ax = 0, ay = 0;
                int mh = 0;
                foreach (Attachment a in msg.Attachments)
                {
                    Size asz = a.Measure(g, fnt, msgWidth - ax, true);

                    Rectangle aRect = new Rectangle(new Point(rX + padding + ax, rY + msgSize.Height + padding + ay), asz);

                    bool aHover = false, aClick = false;

                    if (mouse != null && aRect.Contains(mouse.Position))
                    {
                        aHover = true;

                        if (mouse.Clicked)
                        {
                            aClick = true;
                            mouse.Clicked = false;
                        }
                    }

                    a.Draw(g, aRect, fnt, true, aHover, aClick);

                    if (mh < asz.Height)
                        mh = asz.Height;

                    if (ax + asz.Width > msgWidth)
                    {
                        ax = 0;
                        ay += mh + padding;
                        mh = 0;
                    }
                    else
                        ax += asz.Width + padding;
                }
            }

            return height;
        }

        public static Bitmap SetImageOpacity(Bitmap image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return image;
            }
        }

        public static Bitmap SetImageOpacity(String cacheKey, float opacity)
        {
            cacheKey = Path.GetFileNameWithoutExtension(new Uri(cacheKey).LocalPath);

            String oKey = cacheKey + @"@" + opacity;

            Bitmap oImg = APICache.Get<Bitmap>(oKey);

            if (oImg != null)
                return oImg;

            Bitmap img = APICache.Get<Bitmap>(cacheKey);

            if (img == null)
                return null;

            oImg = SetImageOpacity(img, opacity);

            if(oImg != null)
                APICache.Put(oKey, oImg);

            return oImg;
        }
    }
}
