using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using rtaGlassEffectsLib;
using Timer = System.Threading.Timer;

using VK.Logic;

namespace VK.GenericUI
{
    public sealed partial class Notification : Form
    {
        private static int _lastHeight;
        private static readonly List<Notification> ActiveNotifications = new List<Notification>();
        private static Rectangle _scr = Rectangle.Empty;

        private int _time;
        private readonly Timer _timer;

        private int _x, _y;

        public String Title
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public override String Text
        {
            get
            {
                if(this.textLabel != null && this.textLabel.Text != null)
                    return this.textLabel.Text;

                return "";
            }
            set
            {
                if (this.textLabel != null){
                    this.textLabel.Text = value;
                    this.textLabel.Invalidate();
                }
            }
        }

        public int Time
        {
            get {
                return this._time;
            }
            set {
                this._time = value;

                long t = this._time > 0 ? this._time : Timeout.Infinite;

                this._timer.Change(t, Timeout.Infinite);
            }
        }

        private Notification(String text, String title="", int time=0)
        {
            InitializeComponent();

            this.MinimumSize = this.MaximumSize = this.ClientSize;

            this.Title = title;
            this.Text = text;

            this._time = time;
            if (this._time > 0)
            {
                this._timer = new Timer(obj =>
                {
                    this.InvokeEx(f => f.Close());
                }, null, (long) this._time, Timeout.Infinite);
            }

            rtaGlassEffect glass = new rtaGlassEffect { UseHandCursorOnTitle = false, TopBarSize = -9999, BottomBarSize = 9999 };
            glass.ShowEffect(this);

            if (_scr == Rectangle.Empty)
            {
                _scr = Screen.GetWorkingArea(this);
            }

            this._x = _scr.Width - this.Width;
            this._y = _scr.Height - _lastHeight;

            _lastHeight += this.Height;
            ActiveNotifications.Add(this);

            this.Left = _x;
            this.Top = _scr.Height;

            FormTransform.Transform(this, this.Size, new Point(_x, _y - this.Height));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(this.IsDisposed)
                return;

            base.OnFormClosing(e);

            _lastHeight -= this.Height;
            ActiveNotifications.Remove(this);
        }

        public static Notification Notify(String text, String title = "", int time = 0)
        {
            Notification n = new Notification(text, title, time);
            n.Show();

            return n;
        }

        private void textLabel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(Color.FromArgb(200, 255, 255, 255));

            Rectangle r = this.textLabel.ClientRectangle;
            Padding p = this.textLabel.Padding;

            Rectangle r2 = new Rectangle(r.Top + p.Top, r.Left + p.Left, r.Right - p.Right, r.Bottom - p.Bottom);

            g.DrawString(this.textLabel.Text, this.textLabel.Font, Brushes.Black, r2);
        }
    }
}
