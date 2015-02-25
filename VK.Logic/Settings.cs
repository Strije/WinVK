using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VK.Logic
{
    public class Settings
    {
        [Category("Chat")]
        [DisplayName("Send message")]
        [Description("Send message")]
        [DefaultValue(Keys.Control | Keys.Enter)]
        public Keys SendMessage { get; set; }

        [Category("Chat")]
        [DisplayName("Switch encryption")]
        [Description("Switch encryption")]
        [DefaultValue(Keys.Control | Keys.E)]
        public Keys SwitchEncryption { get; set; }

        public bool AutoStart = false;
        public bool StartMinimized = false;

        public bool ShowTaskbarIcon = true;
        public bool ShowTrayIcon = true;

        public bool Notifications = true;
        public bool ShowSender = true;
        public bool ShowMessagePreview = true;
        public bool ShowEncryptedMessagePreview = true;
        public bool NotificationSound = true;

        public bool CheckForUpdates = true;
    }
}
