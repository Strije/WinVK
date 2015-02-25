using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using rtaGlassEffectsLib;
using VK.API.Data;
using VK.API.LPServer;
using VK.GenericUI;
using VK.Logic;
using Message = VK.API.Data.Message;

namespace VK.App.Windows.GUI.Controls
{
    sealed class ChatControl : ScrollingControl
    {
        private Dialog _chat = null;
        private List<Message> _messages = null;

        private bool _loaded = false;

        private bool _heightCalculated = false;
        private int _oldH = 0;
        private int _so = 0;

        private bool _sending = false;

        private bool _loading;
        private bool _allLoaded;

        public ChatControl() : base()
        {
            AppEvents.On(AppEventType.OpenChat, OpenChat);

            LongPollServer.On(LPEventType.MessageAdded, async e =>
            {
                if(this._chat == null)
                    return;

                int msgID = (int) ((long) ((JValue) e.Data[0]).Value);

                Message msg = await Message.Get(msgID);

                if (msg.UserID == this._chat.Message.UserID || (msg.ChatID != 0 && msg.ChatID == this._chat.Message.ChatID))
                {
                    (new Thread(() =>
                    {
                        if (msg.Attachments != null && msg.Attachments.Length > 0)
                        {
                            foreach (Attachment a in msg.Attachments)
                            {
                                if (a.Type == "photo" && a.Photo != null)
                                {
                                    a.Photo.Load();
                                }
                            }
                        }

                        if (msg.CryptedNow && this._chat.CryptKey != null)
                            msg.Decrypt(this._chat.CryptKey);

                        msg.Author.GetPhoto();

                        this._messages.Add(msg);

                        this._loaded = true;

                        this._heightCalculated = false;

                        this.InvokeEx(t =>
                        {
                            t.Invalidate();
                        });
                    })).Start();
                }
            });
        }

        private void LoadMessages()
        {
            if (this._loading || this._allLoaded)
                return;

            this._loading = true;

            this._so = this.ScrollOffset;
            this._oldH = this.ContentHeight;
            new Thread(async () =>
            {
                List<Message> oldMessages = this._messages.Reverse<Message>().ToList();

                List<Message> newMessages = await this._chat.GetMessages(this._messages.Count);

                if (newMessages.Count == 0)
                {
                    this._allLoaded = true;
                }
                else
                {
                    oldMessages.AddRange(newMessages);
                }

                this._messages = oldMessages.Reverse<Message>().ToList();

                if (this._messages != null)
                {
                    List<int> ids = new List<int>();

                    foreach (Message msg in this._messages)
                    {
                        if (!ids.Contains(msg.UserID))
                            ids.Add(msg.UserID);

                        if (msg.CryptedNow && this._chat.CryptKey != null)
                            msg.Decrypt(this._chat.CryptKey);

                        if (msg.Attachments != null && msg.Attachments.Length > 0)
                        {
                            foreach (Attachment a in msg.Attachments)
                            {
                                if (a.Type == "photo" && a.Photo != null)
                                {
                                    a.Photo.Load();
                                }
                            }
                        }
                    }

                    User[] users = await User.GetAll(ids);

                    if (users != null)
                    {
                        foreach (User u in users)
                        {
                            u.GetPhoto();
                        }
                    }
                }

                this._heightCalculated = false;

                this._loading = false;

                this.InvokeEx(t => t.Invalidate());
            }).Start();
        }

        private void OpenChat(AppEvent e)
        {
            this._chat = (Dialog) e.Data[0];

            (new Thread(async () =>
            {
                this._messages = await this._chat.GetMessages();

                if (this._chat.Crypt)
                {
                    this.InvokeEx(t =>
                    {
                        DecryptMessagesForm frm = new DecryptMessagesForm();
                        if (frm.ShowDialog(t.FindForm()) == DialogResult.OK)
                        {
                            t._chat.CryptKey = frm.Passphrase;
                        }
                    });
                }

                this._so = 0;
                this._oldH = 0;
                if (this._messages != null)
                {
                    this._messages = this._messages.Reverse<Message>().ToList();

                    List<int> ids = new List<int>();

                    foreach (Message msg in this._messages)
                    {
                        if (!ids.Contains(msg.UserID))
                            ids.Add(msg.UserID);

                        if (msg.CryptedNow && this._chat.CryptKey != null)
                            msg.Decrypt(this._chat.CryptKey);

                        if (msg.Attachments != null && msg.Attachments.Length > 0)
                        {
                            foreach (Attachment a in msg.Attachments)
                            {
                                if (a.Type == "photo" && a.Photo != null)
                                {
                                    a.Photo.Load();
                                }
                            }
                        }
                    }

                    User[] users = await User.GetAll(ids);

                    if(users != null){
                        foreach (User u in users)
                        {
                            u.GetPhoto();
                        }
                    }
                }

                this._loaded = true;

                this._heightCalculated = false;

                AppEvents.Dispatch(AppEventType.ChatOpened, this._chat);

                this.InvokeEx(t =>
                {
                    t.Invalidate();
                });
            })).Start();
        }

        public void SendMessage(String text, bool encrypt = false)
        {
            if (this._chat == null)
                return;

            new Thread(async () =>
            {
                while (this._sending)
                {
                    Thread.Sleep(10);
                }

                String chat = "user_id=" + this._chat.Message.UserID;
                if (this._chat.Message.ChatID > 0)
                    chat = "chat_id=" + this._chat.Message.ChatID;

                if (!String.IsNullOrEmpty(text))
                {
                    this._sending = true;

                    this.InvokeEx(t => t.Invalidate());

                    if (encrypt){
                        if (String.IsNullOrEmpty(this._chat.CryptKey))
                        {
                            this.InvokeEx(t =>
                            {
                                DecryptMessagesForm frm = new DecryptMessagesForm();
                                if (frm.ShowDialog(t.FindForm()) == DialogResult.OK)
                                {
                                    t._chat.CryptKey = frm.Passphrase;
                                }
                            });
                        }

                        if(!String.IsNullOrEmpty(this._chat.CryptKey))
                            text = Message.Encrypt(text, this._chat.CryptKey);
                    }

                    await Message.Send(chat, text);

                    this._sending = false;

                    this.InvokeEx(t => t.Invalidate());
                }
            }).Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnPaintBackground(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillRectangle(new SolidBrush(Color.FromArgb(196, Color.White)), this.ClientRectangle);

            if (!this._loaded || this._messages == null || this._messages.Count == 0)
            {
                Drawer.DrawString(g, "No messages", this.Font, Brushes.Black, this.ClientRectangle);
            }
            else
            {
                String bgi = this._chat.Message.Photo;

                if (String.IsNullOrEmpty(bgi))
                {
                    bgi = this._chat.Message.User.Photo;
                }

                if(!String.IsNullOrEmpty(bgi)){
                    Bitmap bg = Drawer.SetImageOpacity(bgi, 0.05f);
                    if (bg != null)
                    {
                        int sz = Math.Max(this.Width, this.Height);

                        g.DrawImage(bg, (this.Width - sz)/2, (this.Height - sz)/2, sz, sz);
                    }
                }

                int y = -this.ScrollOffset + 10;

                Message prevMsg = null;
                foreach (Message msg in this._messages)
                {
                    y += Drawer.DrawMessage(g, msg, prevMsg, this.Font, ControlState.Idle, y, this.Width, this.Height, this.Mouse);

                    prevMsg = msg;
                }

                if (!this._heightCalculated)
                {
                    this.ContentHeight = y + this.ScrollOffset;
                    this.ScrollOffset = this.ContentHeight - this._oldH + this._so - ((this._oldH == 0) ? this.Height : 0);

                    this._heightCalculated = true;
                }
            }

            this.Mouse.Clicked = false;

            base.OnPaint(e);
        }

        protected override void CalcScroll()
        {
            base.CalcScroll();

            if (this.ScrollOffset < 40)
            {
                this.LoadMessages();
            }
        }
    }
}
