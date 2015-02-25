using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VK.API;
using VK.Logic;

namespace VK.App.Windows.GUI
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();

            this.webView.Url = new Uri(AppLogic.GetAuthUrl());
        }

        private void webView_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress == e.MaximumProgress)
            {
                this.pbar.Visible = false;
            }
            else if (e.CurrentProgress < 0)
            {

            }
            else
            {
                this.pbar.Visible = true;
                this.pbar.Style = ProgressBarStyle.Continuous;
                this.pbar.Maximum = (int)e.MaximumProgress;
                this.pbar.Value = (int)e.CurrentProgress;
            }
        }

        private static String _result;
        private static bool _authCompleted;

        private static bool _close = false;

        private void Done(String u)
        {
            _result = u;

            if (u.StartsWith("res://ieframe.dll/navcancl.htm"))
            {
                MessageBox.Show("No internet connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppEvents.Dispatch(AppEventType.AuthFailedNoInternet);
                this.Close();
            }
            else if (u.StartsWith("https://oauth.vk.com/blank.html"))
            {
                _authCompleted = true;
                this.Close();
            }
            else if (u.StartsWith("https://oauth.vk.com/authorize"))
            {

            }
        }

        public async Task Auth()
        {
            this.ShowDialog();

            await API.API.Auth(this.QueryAuth);

            this.Close();
        }

        public String QueryAuth()
        {
            while (!_authCompleted)
            {
                if (_close)
                    return null;

                Thread.Sleep(100);
            }

            return _result;
        }

        private void webView_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Done(e.Url.ToString());
        }

        private void webView_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Done(e.Url.ToString());
        }

        private void AuthForm_Shown(object sender, EventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!_close && !_authCompleted)
            {
                if (MessageBox.Show("You are not authorized!\nClose application?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    AppEvents.Dispatch(AppEventType.AuthFailedNoInternet);
                    _close = true;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
