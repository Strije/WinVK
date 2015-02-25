using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using rtaGlassEffectsLib;
using VK.GenericUI;

namespace VK.App.Windows.GUI.Controls
{
    class ScrollingControl : Panel
    {
        public const float InertiaScrollFactor = 4f;
        public const float InertiaAcceleration = 0.98f;

        private float _vel = 0;

        private Timer _timer = new Timer();

        private int _sHeight = 0;
        private int _sPos = 0;
        private int _sOffset = 0;
        private float _sJump = 0;

        private int _cHeight = 0;
        public int ContentHeight
        {
            get
            {
                return this._cHeight;
            }
            set
            {
                this._cHeight = value;

                this._sHeight = (int) Math.Max((float)this.Height / this.ContentHeight * this.Height, 20f);

                int scrollTrackSpace = this.ContentHeight - this.Height;
                int scrollThumbSpace = this.Height - this._sHeight;
                this._sJump = (float) scrollTrackSpace / scrollThumbSpace;

                this.Invalidate();
            }
        }

        public int ScrollOffset
        {
            get { return this._sOffset; }
            set
            {
                this._sOffset = value;
                this._sPos = (int) (value / this._sJump);
                this.Invalidate();
            }
        }

        public MouseState Mouse { get; set; }

        public ScrollingControl() : base()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            if (!rtaGlassEffect.GlassEnabled)
            {
                this.BackColor = SystemColors.ActiveCaption;
            }


            this.Mouse = MouseState.Empty;

            this._timer.Interval = 16;
            this._timer.Tick += (s, e) => this.CalcScroll();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            this._sHeight = (int) Math.Max((float) this.Height / this.ContentHeight * this.Height, 20f);

            int scrollTrackSpace = this.ContentHeight - this.Height;
            int scrollThumbSpace = this.Height - this._sHeight;
            this._sJump = (float) scrollTrackSpace / scrollThumbSpace;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.ContentHeight > this.Height)
            {
                int y = this._sPos;

                e.Graphics.FillRectangle(Brushes.DimGray, this.Width - 7, y, 5, this._sHeight);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            this.Mouse.Position = e.Location;

            this.Focus();

            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.Mouse = MouseState.Empty;

            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.Mouse.Clicked = true;

            this.Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if(e.Delta > 0)
                this._vel--;
            else
                this._vel++;

            this._timer.Enabled = true;

            this.Invalidate();
        }

        protected virtual void CalcScroll()
        {
            this._sOffset += (int) (this._vel * InertiaScrollFactor);
            this._vel *= InertiaAcceleration;

            if (this._sOffset < 0)
            {
                this._sOffset = 0;
                this._vel = 0;
            }

            if (this._sOffset > this.ContentHeight - this.Height)
            {
                this._sOffset = this.ContentHeight - this.Height;
                this._vel = 0;
            }

            this._sPos = (int) (this._sOffset/this._sJump);

            if (this._vel == 0)
            {
                this._timer.Enabled = false;
            }

            this.Invalidate();
        }
    }
}
