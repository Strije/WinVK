using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using VK.GenericUI;
using VK.Logic;

namespace VK.App.Windows.GUI
{
    public partial class SettingsForm : Form
    {
        public Settings Settings = new Settings();

        public SettingsForm()
        {
            InitializeComponent();

            this.keyboardGrid.SelectedObject = this.Settings;

            this.versionLabel.Text = "WinVK v." + Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void advancedSettingsCB_CheckedChanged(object sender, EventArgs e)
        {
            Point oldLoc = this.Location;
            Size oldSz = this.Size;

            Size sz = new Size(525, 275);

            if(this.advancedSettingsCB.Checked)
                sz = new Size(775, 455);

            Point loc = new Point(oldLoc.X + (oldSz.Width - sz.Width) / 2, oldLoc.Y + (oldSz.Height - sz.Height)/2);

            FormTransform.Transform(this, sz, loc);
        }

        private void testNotificationBtn_Click(object sender, EventArgs e)
        {
            Notification.Notify("This is a test notification from WinVK!\nHello, world!", "WinVK", 5000);
        }
    }
}
