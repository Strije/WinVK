namespace VK.App.Windows.GUI
{
    sealed partial class MainForm
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
            VK.GenericUI.MouseState mouseState1 = new VK.GenericUI.MouseState();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.msgField = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.encryptCB = new System.Windows.Forms.CheckBox();
            this.chatControl1 = new VK.App.Windows.GUI.Controls.ChatControl();
            this.chatsControl1 = new VK.App.Windows.GUI.Controls.ChatsListControl();
            this.SuspendLayout();
            // 
            // msgField
            // 
            this.msgField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msgField.Location = new System.Drawing.Point(202, 319);
            this.msgField.Margin = new System.Windows.Forms.Padding(1);
            this.msgField.Multiline = true;
            this.msgField.Name = "msgField";
            this.msgField.Size = new System.Drawing.Size(399, 41);
            this.msgField.TabIndex = 0;
            this.msgField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msgField_KeyDown);
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendBtn.Image = global::VK.App.Windows.Properties.Resources.send;
            this.sendBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sendBtn.Location = new System.Drawing.Point(602, 319);
            this.sendBtn.Margin = new System.Windows.Forms.Padding(0);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(80, 41);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Send";
            this.sendBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Location = new System.Drawing.Point(2, 319);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(196, 41);
            this.settingsBtn.TabIndex = 4;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // encryptCB
            // 
            this.encryptCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.encryptCB.AutoSize = true;
            this.encryptCB.BackColor = System.Drawing.SystemColors.Window;
            this.encryptCB.Location = new System.Drawing.Point(537, 340);
            this.encryptCB.Name = "encryptCB";
            this.encryptCB.Size = new System.Drawing.Size(62, 17);
            this.encryptCB.TabIndex = 5;
            this.encryptCB.Text = "Encrypt";
            this.encryptCB.UseVisualStyleBackColor = false;
            // 
            // chatControl1
            // 
            this.chatControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatControl1.BackColor = System.Drawing.Color.Black;
            this.chatControl1.ContentHeight = 0;
            this.chatControl1.Location = new System.Drawing.Point(199, 0);
            this.chatControl1.Mouse = mouseState1;
            this.chatControl1.Name = "chatControl1";
            this.chatControl1.ScrollOffset = 0;
            this.chatControl1.Size = new System.Drawing.Size(485, 316);
            this.chatControl1.TabIndex = 1;
            // 
            // chatsControl1
            // 
            this.chatsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chatsControl1.BackColor = System.Drawing.Color.Black;
            this.chatsControl1.ContentHeight = 0;
            this.chatsControl1.Location = new System.Drawing.Point(0, 0);
            this.chatsControl1.Mouse = mouseState1;
            this.chatsControl1.Name = "chatsControl1";
            this.chatsControl1.ScrollOffset = 0;
            this.chatsControl1.Size = new System.Drawing.Size(200, 316);
            this.chatsControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.encryptCB);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.msgField);
            this.Controls.Add(this.chatControl1);
            this.Controls.Add(this.chatsControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinVK";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ChatsListControl chatsControl1;
        private Controls.ChatControl chatControl1;
        private System.Windows.Forms.TextBox msgField;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.CheckBox encryptCB;
    }
}