using System;
using System.Drawing;
using Newtonsoft.Json;
using VK.API.Data.Attachments;

namespace VK.API.Data
{
    public class Attachment
    {
        [JsonProperty("type")]
        public String Type = null;

        [JsonProperty("photo")]
        public Photo Photo = null;

        [JsonProperty("doc")]
        public Document Document = null;

        private String _str;
        public override String ToString()
        {
            if (this._str != null)
                return this._str;

            String str = "Attachment";

            switch (this.Type)
            {
                case "photo":
                    if(!String.IsNullOrEmpty(this.Photo.Text))
                        str = "Photo: " + this.Photo.Text + " (" + this.Photo.Width + "x" + this.Photo.Height + ")";
                    else
                        str = "Photo: " + this.Photo.Width + "x" + this.Photo.Height;
                    break;

                case "doc":
                    str = "Document: " + this.Document.Title + " (" + this.Document.Extension + ")";
                    break;
            }

            this._str = str;
            return this._str;
        }

        private Size _defSize = Size.Empty;
        private Size _expSize = Size.Empty;
        public Size Measure(Graphics g, Font fnt, int width, bool expand = false)
        {
            if (!expand)
            {
                if (this._defSize == Size.Empty)
                    this._defSize = Size.Add(new Size(5, 10), Size.Round(g.MeasureString(this.ToString(), fnt, width, StringFormat.GenericDefault)));

                return this._defSize;
            }
            if (this._expSize == Size.Empty)
            {
                switch (this.Type)
                {
                    case "photo":
                        this._expSize = Size.Add(new Size(10, 10), this.Photo.GetSize());
                        break;

                    default:
                        this._expSize = Size.Add(new Size(10, 10), Size.Round(g.MeasureString(this.ToString(), fnt, width, StringFormat.GenericDefault)));
                        break;
                }
            }

            return this._expSize;
        }

        public void Draw(Graphics g, Rectangle r, Font fnt, bool expand = false, bool hover = false, bool click = false)
        {

            if (hover)
            {
                g.FillRectangle(Brushes.DodgerBlue, r);

                if (click)
                {
                    this.Open();
                    click = false;
                }
            }

            if (!expand)
            {
                g.DrawString(this.ToString(), fnt, Brushes.Black, r.X + 5, r.Y + 5);
            }
            else
            {
                switch (this.Type)
                {
                    case "photo":
                        g.DrawImage(this.Photo.Load(), new Rectangle(r.X + 5, r.Y + 5, r.Width - 10, r.Height - 10));
                        g.FillRectangle(new SolidBrush(Color.FromArgb(192, 0, 0, 0)), r.X + 5, r.Bottom - 30, r.Width - 10, 25);
                        g.DrawString(this.ToString(), fnt, Brushes.White, r.X + 10, r.Bottom - 25);
                        break;

                    default:
                        this.Draw(g, r, fnt, false, hover, click);
                        break;
                }
            }
        }

        public void Open()
        {
            switch (this.Type)
            {
                case "photo":
                    this.Photo.Open();
                    break;

                case "doc":
                    this.Document.Open();
                    break;
            }
        }
    }
}
