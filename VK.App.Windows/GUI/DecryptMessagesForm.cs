using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VK.App.Windows.GUI
{
    public partial class DecryptMessagesForm : Form
    {
        public String Passphrase = null;

        public DecryptMessagesForm()
        {
            InitializeComponent();
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            this.Passphrase = this.passphraseTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void showInputCB_CheckedChanged(object sender, EventArgs e)
        {
            this.passphraseTextBox.PasswordChar = (this.showInputCB.Checked) ? '\0' : '*';
        }
    }
}
