using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using rtaGlassEffectsLib;
using VK.API.Data;
using VK.Logic;

namespace VK.App.Windows.GUI
{
    public sealed partial class MainForm : Form
    {
        private readonly rtaGlassEffect _glass;

        public MainForm()
        {
            InitializeComponent();

            this.MinimumSize = this.ClientSize;

            if(rtaGlassEffect.GlassEnabled){
                this._glass = new rtaGlassEffect
                {
                    UseHandCursorOnTitle = false,
                    TopBarSize = this.ClientSize.Height - 44,
                    LeftBarSize = 1,
                    RightBarSize = 1,
                    BottomBarSize = 1
                };

                this._glass.ShowEffect(this);
            }

            AppEvents.On(AppEventType.ChatOpened, e =>
            {
                this.InvokeEx(t =>
                {
                    Dialog chat = (Dialog) e.Data[0];

                    t.Text = chat.Title + @" - WinVK";
                    t.encryptCB.Checked = (chat.Crypt && !String.IsNullOrEmpty(chat.CryptKey));
                });
            });
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this._glass != null)
            {
                this._glass.TopBarSize = this.ClientSize.Height - 44;
                this._glass.ShowEffect(this);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            AuthForm f = new AuthForm();

            await f.Auth();

            AppEvents.Dispatch(AppEventType.AuthCompleted, API.API.Session);
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            String msg = this.msgField.Text;

            if(String.IsNullOrEmpty(msg))
                return;

            this.chatControl1.SendMessage(msg, (this.encryptCB.Checked));
            this.msgField.Text = "";
        }

        private void msgField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                this.sendBtn_Click(sender, EventArgs.Empty);
                this.msgField.Text = "";
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm frm = new SettingsForm();
            frm.ShowDialog();
        }
    }
}
