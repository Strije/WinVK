namespace VK.App.Windows.GUI
{
    partial class SettingsForm
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
            this.notifyGB = new System.Windows.Forms.GroupBox();
            this.soundCB = new System.Windows.Forms.CheckBox();
            this.showEncryptedMessagesCB = new System.Windows.Forms.CheckBox();
            this.showMsgPreviewCB = new System.Windows.Forms.CheckBox();
            this.showSenderCB = new System.Windows.Forms.CheckBox();
            this.notifyCB = new System.Windows.Forms.CheckBox();
            this.kbGB = new System.Windows.Forms.GroupBox();
            this.keyboardGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.taskbarIconCB = new System.Windows.Forms.CheckBox();
            this.trayIconCB = new System.Windows.Forms.CheckBox();
            this.minCB = new System.Windows.Forms.CheckBox();
            this.autostartCB = new System.Windows.Forms.CheckBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.checkUpdatesBtn = new System.Windows.Forms.Button();
            this.updatesGB = new System.Windows.Forms.GroupBox();
            this.autoUpdateCB = new System.Windows.Forms.CheckBox();
            this.chatOptionsGB = new System.Windows.Forms.GroupBox();
            this.advancedSettingsCB = new System.Windows.Forms.CheckBox();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.testNotificationBtn = new System.Windows.Forms.Button();
            this.notifyGB.SuspendLayout();
            this.kbGB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.updatesGB.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyGB
            // 
            this.notifyGB.Controls.Add(this.testNotificationBtn);
            this.notifyGB.Controls.Add(this.soundCB);
            this.notifyGB.Controls.Add(this.showEncryptedMessagesCB);
            this.notifyGB.Controls.Add(this.showMsgPreviewCB);
            this.notifyGB.Controls.Add(this.showSenderCB);
            this.notifyGB.Controls.Add(this.notifyCB);
            this.notifyGB.Location = new System.Drawing.Point(259, 12);
            this.notifyGB.Name = "notifyGB";
            this.notifyGB.Size = new System.Drawing.Size(241, 97);
            this.notifyGB.TabIndex = 0;
            this.notifyGB.TabStop = false;
            this.notifyGB.Text = "__ Notifications";
            // 
            // soundCB
            // 
            this.soundCB.AutoSize = true;
            this.soundCB.Location = new System.Drawing.Point(12, 68);
            this.soundCB.Name = "soundCB";
            this.soundCB.Size = new System.Drawing.Size(57, 17);
            this.soundCB.TabIndex = 4;
            this.soundCB.Text = "Sound";
            this.soundCB.UseVisualStyleBackColor = true;
            // 
            // showEncryptedMessagesCB
            // 
            this.showEncryptedMessagesCB.AutoSize = true;
            this.showEncryptedMessagesCB.Location = new System.Drawing.Point(156, 45);
            this.showEncryptedMessagesCB.Name = "showEncryptedMessagesCB";
            this.showEncryptedMessagesCB.Size = new System.Drawing.Size(81, 17);
            this.showEncryptedMessagesCB.TabIndex = 3;
            this.showEncryptedMessagesCB.Text = "if encrypted";
            this.showEncryptedMessagesCB.UseVisualStyleBackColor = true;
            // 
            // showMsgPreviewCB
            // 
            this.showMsgPreviewCB.AutoSize = true;
            this.showMsgPreviewCB.Location = new System.Drawing.Point(12, 45);
            this.showMsgPreviewCB.Name = "showMsgPreviewCB";
            this.showMsgPreviewCB.Size = new System.Drawing.Size(138, 17);
            this.showMsgPreviewCB.TabIndex = 2;
            this.showMsgPreviewCB.Text = "Show message preview";
            this.showMsgPreviewCB.UseVisualStyleBackColor = true;
            // 
            // showSenderCB
            // 
            this.showSenderCB.AutoSize = true;
            this.showSenderCB.Location = new System.Drawing.Point(12, 22);
            this.showSenderCB.Name = "showSenderCB";
            this.showSenderCB.Size = new System.Drawing.Size(88, 17);
            this.showSenderCB.TabIndex = 1;
            this.showSenderCB.Text = "Show sender";
            this.showSenderCB.UseVisualStyleBackColor = true;
            // 
            // notifyCB
            // 
            this.notifyCB.AutoSize = true;
            this.notifyCB.Location = new System.Drawing.Point(8, -1);
            this.notifyCB.Name = "notifyCB";
            this.notifyCB.Size = new System.Drawing.Size(84, 17);
            this.notifyCB.TabIndex = 0;
            this.notifyCB.Text = "Notifications";
            this.notifyCB.UseVisualStyleBackColor = true;
            // 
            // kbGB
            // 
            this.kbGB.Controls.Add(this.keyboardGrid);
            this.kbGB.Location = new System.Drawing.Point(506, 12);
            this.kbGB.Name = "kbGB";
            this.kbGB.Size = new System.Drawing.Size(241, 354);
            this.kbGB.TabIndex = 6;
            this.kbGB.TabStop = false;
            this.kbGB.Text = "Keyboard shortcuts";
            // 
            // keyboardGrid
            // 
            this.keyboardGrid.Location = new System.Drawing.Point(6, 19);
            this.keyboardGrid.Name = "keyboardGrid";
            this.keyboardGrid.Size = new System.Drawing.Size(229, 329);
            this.keyboardGrid.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.taskbarIconCB);
            this.groupBox1.Controls.Add(this.trayIconCB);
            this.groupBox1.Controls.Add(this.minCB);
            this.groupBox1.Controls.Add(this.autostartCB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 179);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // taskbarIconCB
            // 
            this.taskbarIconCB.AutoSize = true;
            this.taskbarIconCB.Location = new System.Drawing.Point(6, 68);
            this.taskbarIconCB.Name = "taskbarIconCB";
            this.taskbarIconCB.Size = new System.Drawing.Size(114, 17);
            this.taskbarIconCB.TabIndex = 16;
            this.taskbarIconCB.Text = "Show taskbar icon";
            this.taskbarIconCB.UseVisualStyleBackColor = true;
            // 
            // trayIconCB
            // 
            this.trayIconCB.AutoSize = true;
            this.trayIconCB.Location = new System.Drawing.Point(126, 68);
            this.trayIconCB.Name = "trayIconCB";
            this.trayIconCB.Size = new System.Drawing.Size(96, 17);
            this.trayIconCB.TabIndex = 15;
            this.trayIconCB.Text = "Show tray icon";
            this.trayIconCB.UseVisualStyleBackColor = true;
            // 
            // minCB
            // 
            this.minCB.AutoSize = true;
            this.minCB.Location = new System.Drawing.Point(24, 45);
            this.minCB.Name = "minCB";
            this.minCB.Size = new System.Drawing.Size(96, 17);
            this.minCB.TabIndex = 14;
            this.minCB.Text = "Start minimized";
            this.minCB.UseVisualStyleBackColor = true;
            // 
            // autostartCB
            // 
            this.autostartCB.AutoSize = true;
            this.autostartCB.Location = new System.Drawing.Point(6, 22);
            this.autostartCB.Name = "autostartCB";
            this.autostartCB.Size = new System.Drawing.Size(133, 17);
            this.autostartCB.TabIndex = 13;
            this.autostartCB.Text = "Start on system startup";
            this.autostartCB.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(423, 10);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1000;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBtn.Location = new System.Drawing.Point(342, 10);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 1000;
            this.applyBtn.Text = "&Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.versionLabel.Location = new System.Drawing.Point(108, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(127, 13);
            this.versionLabel.TabIndex = 10;
            this.versionLabel.Text = "WinVK v.1.0.5532.19233";
            // 
            // checkUpdatesBtn
            // 
            this.checkUpdatesBtn.Location = new System.Drawing.Point(6, 42);
            this.checkUpdatesBtn.Name = "checkUpdatesBtn";
            this.checkUpdatesBtn.Size = new System.Drawing.Size(229, 23);
            this.checkUpdatesBtn.TabIndex = 11;
            this.checkUpdatesBtn.Text = "Check for &updates now";
            this.checkUpdatesBtn.UseVisualStyleBackColor = true;
            // 
            // updatesGB
            // 
            this.updatesGB.Controls.Add(this.autoUpdateCB);
            this.updatesGB.Controls.Add(this.versionLabel);
            this.updatesGB.Controls.Add(this.checkUpdatesBtn);
            this.updatesGB.Location = new System.Drawing.Point(259, 115);
            this.updatesGB.Name = "updatesGB";
            this.updatesGB.Size = new System.Drawing.Size(241, 76);
            this.updatesGB.TabIndex = 8;
            this.updatesGB.TabStop = false;
            this.updatesGB.Text = "Updates";
            // 
            // autoUpdateCB
            // 
            this.autoUpdateCB.AutoSize = true;
            this.autoUpdateCB.Location = new System.Drawing.Point(6, 19);
            this.autoUpdateCB.Name = "autoUpdateCB";
            this.autoUpdateCB.Size = new System.Drawing.Size(163, 17);
            this.autoUpdateCB.TabIndex = 12;
            this.autoUpdateCB.Text = "Check for updates on startup";
            this.autoUpdateCB.UseVisualStyleBackColor = true;
            // 
            // chatOptionsGB
            // 
            this.chatOptionsGB.Location = new System.Drawing.Point(12, 197);
            this.chatOptionsGB.Name = "chatOptionsGB";
            this.chatOptionsGB.Size = new System.Drawing.Size(488, 169);
            this.chatOptionsGB.TabIndex = 7;
            this.chatOptionsGB.TabStop = false;
            this.chatOptionsGB.Text = "Chat options";
            // 
            // advancedSettingsCB
            // 
            this.advancedSettingsCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.advancedSettingsCB.AutoSize = true;
            this.advancedSettingsCB.Location = new System.Drawing.Point(13, 10);
            this.advancedSettingsCB.Name = "advancedSettingsCB";
            this.advancedSettingsCB.Size = new System.Drawing.Size(66, 23);
            this.advancedSettingsCB.TabIndex = 10;
            this.advancedSettingsCB.Text = "Advanced";
            this.advancedSettingsCB.UseVisualStyleBackColor = true;
            this.advancedSettingsCB.CheckedChanged += new System.EventHandler(this.advancedSettingsCB_CheckedChanged);
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.buttonsPanel.Controls.Add(this.advancedSettingsCB);
            this.buttonsPanel.Controls.Add(this.cancelBtn);
            this.buttonsPanel.Controls.Add(this.applyBtn);
            this.buttonsPanel.Location = new System.Drawing.Point(-1, 195);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonsPanel.Size = new System.Drawing.Size(511, 43);
            this.buttonsPanel.TabIndex = 1001;
            // 
            // testNotificationBtn
            // 
            this.testNotificationBtn.Location = new System.Drawing.Point(111, 64);
            this.testNotificationBtn.Name = "testNotificationBtn";
            this.testNotificationBtn.Size = new System.Drawing.Size(124, 23);
            this.testNotificationBtn.TabIndex = 5;
            this.testNotificationBtn.Text = "Test notification";
            this.testNotificationBtn.UseVisualStyleBackColor = true;
            this.testNotificationBtn.Click += new System.EventHandler(this.testNotificationBtn_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.applyBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(509, 237);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.chatOptionsGB);
            this.Controls.Add(this.updatesGB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.kbGB);
            this.Controls.Add(this.notifyGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.notifyGB.ResumeLayout(false);
            this.notifyGB.PerformLayout();
            this.kbGB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.updatesGB.ResumeLayout(false);
            this.updatesGB.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox notifyGB;
        private System.Windows.Forms.CheckBox soundCB;
        private System.Windows.Forms.CheckBox showEncryptedMessagesCB;
        private System.Windows.Forms.CheckBox showMsgPreviewCB;
        private System.Windows.Forms.CheckBox showSenderCB;
        private System.Windows.Forms.CheckBox notifyCB;
        private System.Windows.Forms.GroupBox kbGB;
        private System.Windows.Forms.PropertyGrid keyboardGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox taskbarIconCB;
        private System.Windows.Forms.CheckBox trayIconCB;
        private System.Windows.Forms.CheckBox minCB;
        private System.Windows.Forms.CheckBox autostartCB;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button checkUpdatesBtn;
        private System.Windows.Forms.GroupBox updatesGB;
        private System.Windows.Forms.CheckBox autoUpdateCB;
        private System.Windows.Forms.GroupBox chatOptionsGB;
        private System.Windows.Forms.CheckBox advancedSettingsCB;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button testNotificationBtn;
    }
}