using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using rtaGlassEffectsLib;
using VK.Logic;

namespace VK.App.Windows.GUI.Controls
{
    sealed class UserBarControl : Panel
    {
        private bool authCompleted = false;

        public UserBarControl()
            : base()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            AppEvents.On(AppEventType.AuthCompleted, e =>
            {
                this.authCompleted = true;
                this.Invalidate();
            });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(Color.FromArgb(196, Color.White));

            if (!authCompleted)
            {
                GenericUI.Drawer.DrawString(g, "Loading user...", this.Font, Brushes.Black, this.ClientRectangle);
            }
            else
            {
                GenericUI.Drawer.DrawString(g, API.API.Session.User.FullName, this.Font, Brushes.Black, this.ClientRectangle);
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            this.Invalidate();
        }
    }
}
