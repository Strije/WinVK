namespace VK.App.Windows.GUI
{
    partial class DecryptMessagesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.passphraseTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.rememberCB = new System.Windows.Forms.CheckBox();
            this.showInputCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "There are encrypted messages in this chat.\r\nPlease enter passphrase to decrypt th" +
    "em:";
            // 
            // passphraseTextBox
            // 
            this.passphraseTextBox.Location = new System.Drawing.Point(97, 47);
            this.passphraseTextBox.Name = "passphraseTextBox";
            this.passphraseTextBox.PasswordChar = '*';
            this.passphraseTextBox.Size = new System.Drawing.Size(275, 20);
            this.passphraseTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Passphrase:";
            // 
            // decryptBtn
            // 
            this.decryptBtn.Location = new System.Drawing.Point(12, 99);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(175, 32);
            this.decryptBtn.TabIndex = 4;
            this.decryptBtn.Text = "&Decrypt";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(197, 99);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(175, 32);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // rememberCB
            // 
            this.rememberCB.AutoSize = true;
            this.rememberCB.Location = new System.Drawing.Point(15, 76);
            this.rememberCB.Name = "rememberCB";
            this.rememberCB.Size = new System.Drawing.Size(134, 17);
            this.rememberCB.TabIndex = 6;
            this.rememberCB.Text = "Remember passphrase";
            this.rememberCB.UseVisualStyleBackColor = true;
            // 
            // showInputCB
            // 
            this.showInputCB.AutoSize = true;
            this.showInputCB.Location = new System.Drawing.Point(197, 76);
            this.showInputCB.Name = "showInputCB";
            this.showInputCB.Size = new System.Drawing.Size(79, 17);
            this.showInputCB.TabIndex = 7;
            this.showInputCB.Text = "Show input";
            this.showInputCB.UseVisualStyleBackColor = true;
            this.showInputCB.CheckedChanged += new System.EventHandler(this.showInputCB_CheckedChanged);
            // 
            // DecryptMessagesForm
            // 
            this.AcceptButton = this.decryptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(384, 143);
            this.Controls.Add(this.showInputCB);
            this.Controls.Add(this.rememberCB);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.decryptBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passphraseTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DecryptMessagesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Decrypt messages...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passphraseTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox rememberCB;
        private System.Windows.Forms.CheckBox showInputCB;
    }
}