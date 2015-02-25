using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VK.API.Data;
using VK.GenericUI;
using VK.Logic;
using Message = VK.API.Data.Message;

namespace VK.App.Windows.GUI.Controls
{
    sealed class ChatsListControl : ScrollingControl
    {
        private bool loaded = false;
        private List<Dialog> chats;

        private int _cActive = -1;

        public ChatsListControl() : base()
        {
            AppEvents.On(AppEventType.AuthCompleted, e =>
            {
                this.LoadChats();
            });
        }

        private void LoadChats()
        {
            (new Thread(async () =>
            {
                if (API.API.Session == null)
                    return;

                this.chats = await Dialog.GetDialogs();
                this.loaded = true;

                List<int> ids = new List<int>();
                foreach (Dialog chat in this.chats)
                {
                    Message msg = chat.Message;

                    if (!ids.Contains(msg.UserID))
                        ids.Add(msg.UserID);
                }

                User[] users = await User.GetAll(ids);

                foreach (User u in users)
                {
                    u.GetPhoto();
                }

                this.InvokeEx(t =>
                {
                    if (t != null && t.chats != null)
                    {
                        t.ContentHeight = t.chats.Count * 50;
                        t.Invalidate();
                    }
                });
            })).Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnPaintBackground(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;

            if (!loaded)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(164, Color.White)), this.ClientRectangle);
                Drawer.DrawString(g, "Loading chats...", this.Font, Brushes.Black, this.ClientRectangle);
            }
            else
            {
                int y = -this.ScrollOffset;

                int i = -1;
                foreach (Dialog chat in this.chats)
                {
                    i++;

                    if (y < -50)
                    {
                        y += 50;
                        continue;
                    }

                    if (y > this.Height)
                        break;

                    ControlState s = ControlState.Idle;

                    int my = this.Mouse.Position.Y;

                    if (this.Mouse.Position.X != -1 && my > y && my < y + 50)
                    {
                        s = ControlState.Hover;

                        if (this.Mouse.Clicked)
                        {
                            s = ControlState.Pressed;

                            this._cActive = i;

                            AppEvents.Dispatch(AppEventType.OpenChat, chat);

                            this.Mouse.Clicked = false;

                            this.Invalidate();
                        }
                    }

                    if(i == this._cActive)
                        s = ControlState.Active;

                    Drawer.DrawChat(g, chat, this.Font, s, y, this.Width);

                    y += 50;
                }
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            this.Invalidate();
        }
    }
}
