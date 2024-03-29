﻿namespace rbBeatDetect
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.errorLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.beat1 = new System.Windows.Forms.Button();
            this.beat2 = new System.Windows.Forms.Button();
            this.beat3 = new System.Windows.Forms.Button();
            this.beat4 = new System.Windows.Forms.Button();
            this.masterDeckHelper = new System.Windows.Forms.Label();
            this.masterDeckLabel = new System.Windows.Forms.Label();
            this.oscGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.oscHumanDelay = new System.Windows.Forms.NumericUpDown();
            this.oscDelayHelper = new System.Windows.Forms.Label();
            this.oscDelay = new System.Windows.Forms.NumericUpDown();
            this.oscMimicHuman = new System.Windows.Forms.CheckBox();
            this.oscPortBox = new System.Windows.Forms.TextBox();
            this.oscPath = new System.Windows.Forms.TextBox();
            this.oscIpAddr = new System.Windows.Forms.TextBox();
            this.oscPathHelper = new System.Windows.Forms.Label();
            this.oscPortHelper = new System.Windows.Forms.Label();
            this.oscAddressHelper = new System.Windows.Forms.Label();
            this.stats = new System.Windows.Forms.GroupBox();
            this.runningCheckbox = new System.Windows.Forms.CheckBox();
            this.verBox = new System.Windows.Forms.GroupBox();
            this.versionsBox = new System.Windows.Forms.ListBox();
            this.supportedVersionsHelper = new System.Windows.Forms.Label();
            this.runningVersionLabel = new System.Windows.Forms.Label();
            this.runningVersionHelper = new System.Windows.Forms.Label();
            this.rbVersionCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.updateGuiTimer = new System.Windows.Forms.Timer(this.components);
            this.selfSponsor = new System.Windows.Forms.LinkLabel();
            this.oscGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oscHumanDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oscDelay)).BeginInit();
            this.stats.SuspendLayout();
            this.verBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorLabel
            // 
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(152, 428);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(280, 55);
            this.errorLabel.TabIndex = 2;
            this.errorLabel.Text = "[placeholder]";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.Location = new System.Drawing.Point(12, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(566, 25);
            this.titleLabel.TabIndex = 11;
            this.titleLabel.Text = "rbBeatDetect GUI";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // beat1
            // 
            this.beat1.BackColor = System.Drawing.Color.White;
            this.beat1.Enabled = false;
            this.beat1.Location = new System.Drawing.Point(68, 42);
            this.beat1.Name = "beat1";
            this.beat1.Size = new System.Drawing.Size(33, 21);
            this.beat1.TabIndex = 13;
            this.beat1.UseVisualStyleBackColor = false;
            // 
            // beat2
            // 
            this.beat2.BackColor = System.Drawing.Color.White;
            this.beat2.Enabled = false;
            this.beat2.Location = new System.Drawing.Point(101, 42);
            this.beat2.Name = "beat2";
            this.beat2.Size = new System.Drawing.Size(33, 21);
            this.beat2.TabIndex = 14;
            this.beat2.UseVisualStyleBackColor = false;
            // 
            // beat3
            // 
            this.beat3.BackColor = System.Drawing.Color.White;
            this.beat3.Enabled = false;
            this.beat3.Location = new System.Drawing.Point(134, 42);
            this.beat3.Name = "beat3";
            this.beat3.Size = new System.Drawing.Size(33, 21);
            this.beat3.TabIndex = 15;
            this.beat3.UseVisualStyleBackColor = false;
            // 
            // beat4
            // 
            this.beat4.BackColor = System.Drawing.Color.White;
            this.beat4.Enabled = false;
            this.beat4.Location = new System.Drawing.Point(167, 42);
            this.beat4.Name = "beat4";
            this.beat4.Size = new System.Drawing.Size(33, 21);
            this.beat4.TabIndex = 16;
            this.beat4.UseVisualStyleBackColor = false;
            // 
            // masterDeckHelper
            // 
            this.masterDeckHelper.AutoSize = true;
            this.masterDeckHelper.Location = new System.Drawing.Point(82, 79);
            this.masterDeckHelper.Name = "masterDeckHelper";
            this.masterDeckHelper.Size = new System.Drawing.Size(71, 13);
            this.masterDeckHelper.TabIndex = 17;
            this.masterDeckHelper.Text = "Master Deck:";
            // 
            // masterDeckLabel
            // 
            this.masterDeckLabel.AutoSize = true;
            this.masterDeckLabel.Location = new System.Drawing.Point(159, 79);
            this.masterDeckLabel.Name = "masterDeckLabel";
            this.masterDeckLabel.Size = new System.Drawing.Size(68, 13);
            this.masterDeckLabel.TabIndex = 18;
            this.masterDeckLabel.Text = "[placeholder]";
            // 
            // oscGroup
            // 
            this.oscGroup.Controls.Add(this.label1);
            this.oscGroup.Controls.Add(this.oscHumanDelay);
            this.oscGroup.Controls.Add(this.oscDelayHelper);
            this.oscGroup.Controls.Add(this.oscDelay);
            this.oscGroup.Controls.Add(this.oscMimicHuman);
            this.oscGroup.Controls.Add(this.oscPortBox);
            this.oscGroup.Controls.Add(this.oscPath);
            this.oscGroup.Controls.Add(this.oscIpAddr);
            this.oscGroup.Controls.Add(this.oscPathHelper);
            this.oscGroup.Controls.Add(this.oscPortHelper);
            this.oscGroup.Controls.Add(this.oscAddressHelper);
            this.oscGroup.Location = new System.Drawing.Point(301, 55);
            this.oscGroup.Name = "oscGroup";
            this.oscGroup.Size = new System.Drawing.Size(277, 226);
            this.oscGroup.TabIndex = 20;
            this.oscGroup.TabStop = false;
            this.oscGroup.Text = "OSC Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Delay (in ms):";
            // 
            // oscHumanDelay
            // 
            this.oscHumanDelay.Enabled = false;
            this.oscHumanDelay.Location = new System.Drawing.Point(138, 162);
            this.oscHumanDelay.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.oscHumanDelay.Name = "oscHumanDelay";
            this.oscHumanDelay.Size = new System.Drawing.Size(58, 20);
            this.oscHumanDelay.TabIndex = 29;
            this.oscHumanDelay.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.oscHumanDelay.ValueChanged += new System.EventHandler(this.oscHumanDelay_ValueChanged);
            // 
            // oscDelayHelper
            // 
            this.oscDelayHelper.AutoSize = true;
            this.oscDelayHelper.Location = new System.Drawing.Point(6, 104);
            this.oscDelayHelper.Name = "oscDelayHelper";
            this.oscDelayHelper.Size = new System.Drawing.Size(79, 13);
            this.oscDelayHelper.TabIndex = 28;
            this.oscDelayHelper.Text = "OSC Pre-delay:";
            // 
            // oscDelay
            // 
            this.oscDelay.Location = new System.Drawing.Point(103, 102);
            this.oscDelay.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.oscDelay.Name = "oscDelay";
            this.oscDelay.Size = new System.Drawing.Size(58, 20);
            this.oscDelay.TabIndex = 27;
            this.oscDelay.ValueChanged += new System.EventHandler(this.oscDelay_ValueChanged);
            // 
            // oscMimicHuman
            // 
            this.oscMimicHuman.AutoSize = true;
            this.oscMimicHuman.Location = new System.Drawing.Point(72, 137);
            this.oscMimicHuman.Name = "oscMimicHuman";
            this.oscMimicHuman.Size = new System.Drawing.Size(133, 17);
            this.oscMimicHuman.TabIndex = 26;
            this.oscMimicHuman.Text = "Mimic human keypress";
            this.oscMimicHuman.UseVisualStyleBackColor = true;
            this.oscMimicHuman.CheckedChanged += new System.EventHandler(this.oscMimicHuman_CheckedChanged);
            // 
            // oscPortBox
            // 
            this.oscPortBox.Location = new System.Drawing.Point(103, 51);
            this.oscPortBox.Name = "oscPortBox";
            this.oscPortBox.Size = new System.Drawing.Size(152, 20);
            this.oscPortBox.TabIndex = 25;
            this.oscPortBox.Text = "7700";
            // 
            // oscPath
            // 
            this.oscPath.Location = new System.Drawing.Point(103, 76);
            this.oscPath.Name = "oscPath";
            this.oscPath.Size = new System.Drawing.Size(152, 20);
            this.oscPath.TabIndex = 24;
            this.oscPath.Text = "/beat";
            // 
            // oscIpAddr
            // 
            this.oscIpAddr.Location = new System.Drawing.Point(103, 28);
            this.oscIpAddr.Name = "oscIpAddr";
            this.oscIpAddr.Size = new System.Drawing.Size(152, 20);
            this.oscIpAddr.TabIndex = 23;
            this.oscIpAddr.Text = "127.0.0.1";
            // 
            // oscPathHelper
            // 
            this.oscPathHelper.AutoSize = true;
            this.oscPathHelper.Location = new System.Drawing.Point(6, 79);
            this.oscPathHelper.Name = "oscPathHelper";
            this.oscPathHelper.Size = new System.Drawing.Size(57, 13);
            this.oscPathHelper.TabIndex = 22;
            this.oscPathHelper.Text = "OSC Path:";
            // 
            // oscPortHelper
            // 
            this.oscPortHelper.AutoSize = true;
            this.oscPortHelper.Location = new System.Drawing.Point(6, 54);
            this.oscPortHelper.Name = "oscPortHelper";
            this.oscPortHelper.Size = new System.Drawing.Size(29, 13);
            this.oscPortHelper.TabIndex = 21;
            this.oscPortHelper.Text = "Port:";
            // 
            // oscAddressHelper
            // 
            this.oscAddressHelper.AutoSize = true;
            this.oscAddressHelper.Location = new System.Drawing.Point(6, 31);
            this.oscAddressHelper.Name = "oscAddressHelper";
            this.oscAddressHelper.Size = new System.Drawing.Size(91, 13);
            this.oscAddressHelper.TabIndex = 20;
            this.oscAddressHelper.Text = "Network Address:";
            // 
            // stats
            // 
            this.stats.Controls.Add(this.runningCheckbox);
            this.stats.Controls.Add(this.beat3);
            this.stats.Controls.Add(this.beat1);
            this.stats.Controls.Add(this.masterDeckLabel);
            this.stats.Controls.Add(this.beat2);
            this.stats.Controls.Add(this.masterDeckHelper);
            this.stats.Controls.Add(this.beat4);
            this.stats.Location = new System.Drawing.Point(152, 312);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(280, 113);
            this.stats.TabIndex = 21;
            this.stats.TabStop = false;
            this.stats.Text = "Stats";
            // 
            // runningCheckbox
            // 
            this.runningCheckbox.AutoSize = true;
            this.runningCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.runningCheckbox.Location = new System.Drawing.Point(103, 19);
            this.runningCheckbox.Name = "runningCheckbox";
            this.runningCheckbox.Size = new System.Drawing.Size(73, 17);
            this.runningCheckbox.TabIndex = 19;
            this.runningCheckbox.Text = "Running";
            this.runningCheckbox.UseVisualStyleBackColor = true;
            this.runningCheckbox.CheckedChanged += new System.EventHandler(this.runningCheckbox_CheckedChanged);
            // 
            // verBox
            // 
            this.verBox.Controls.Add(this.versionsBox);
            this.verBox.Controls.Add(this.supportedVersionsHelper);
            this.verBox.Controls.Add(this.runningVersionLabel);
            this.verBox.Controls.Add(this.runningVersionHelper);
            this.verBox.Location = new System.Drawing.Point(12, 55);
            this.verBox.Name = "verBox";
            this.verBox.Size = new System.Drawing.Size(266, 226);
            this.verBox.TabIndex = 23;
            this.verBox.TabStop = false;
            this.verBox.Text = "Version Settings";
            // 
            // versionsBox
            // 
            this.versionsBox.Enabled = false;
            this.versionsBox.FormattingEnabled = true;
            this.versionsBox.Location = new System.Drawing.Point(125, 54);
            this.versionsBox.Name = "versionsBox";
            this.versionsBox.Size = new System.Drawing.Size(120, 121);
            this.versionsBox.TabIndex = 31;
            // 
            // supportedVersionsHelper
            // 
            this.supportedVersionsHelper.AutoSize = true;
            this.supportedVersionsHelper.Location = new System.Drawing.Point(16, 54);
            this.supportedVersionsHelper.Name = "supportedVersionsHelper";
            this.supportedVersionsHelper.Size = new System.Drawing.Size(102, 13);
            this.supportedVersionsHelper.TabIndex = 29;
            this.supportedVersionsHelper.Text = "Supported Versions:";
            // 
            // runningVersionLabel
            // 
            this.runningVersionLabel.AutoSize = true;
            this.runningVersionLabel.Location = new System.Drawing.Point(121, 28);
            this.runningVersionLabel.Name = "runningVersionLabel";
            this.runningVersionLabel.Size = new System.Drawing.Size(68, 13);
            this.runningVersionLabel.TabIndex = 28;
            this.runningVersionLabel.Text = "[placeholder]";
            // 
            // runningVersionHelper
            // 
            this.runningVersionHelper.AutoSize = true;
            this.runningVersionHelper.Location = new System.Drawing.Point(16, 28);
            this.runningVersionHelper.Name = "runningVersionHelper";
            this.runningVersionHelper.Size = new System.Drawing.Size(87, 13);
            this.runningVersionHelper.TabIndex = 27;
            this.runningVersionHelper.Text = "Running version:";
            // 
            // rbVersionCheckTimer
            // 
            this.rbVersionCheckTimer.Interval = 1000;
            this.rbVersionCheckTimer.Tick += new System.EventHandler(this.rbCheckTimer_Tick);
            // 
            // updateGuiTimer
            // 
            this.updateGuiTimer.Interval = 50;
            this.updateGuiTimer.Tick += new System.EventHandler(this.updateGuiTimer_Tick);
            // 
            // selfSponsor
            // 
            this.selfSponsor.AutoSize = true;
            this.selfSponsor.Location = new System.Drawing.Point(252, 502);
            this.selfSponsor.Name = "selfSponsor";
            this.selfSponsor.Size = new System.Drawing.Size(82, 13);
            this.selfSponsor.TabIndex = 25;
            this.selfSponsor.TabStop = true;
            this.selfSponsor.Text = "Project Website";
            this.selfSponsor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selfSponsor_LinkClicked);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 524);
            this.Controls.Add(this.selfSponsor);
            this.Controls.Add(this.verBox);
            this.Controls.Add(this.stats);
            this.Controls.Add(this.oscGroup);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.errorLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.Text = "rbBeatDetect GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.oscGroup.ResumeLayout(false);
            this.oscGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oscHumanDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oscDelay)).EndInit();
            this.stats.ResumeLayout(false);
            this.stats.PerformLayout();
            this.verBox.ResumeLayout(false);
            this.verBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button beat1;
        private System.Windows.Forms.Button beat2;
        private System.Windows.Forms.Button beat3;
        private System.Windows.Forms.Button beat4;
        private System.Windows.Forms.Label masterDeckHelper;
        private System.Windows.Forms.Label masterDeckLabel;
        private System.Windows.Forms.GroupBox oscGroup;
        private System.Windows.Forms.Label oscAddressHelper;
        private System.Windows.Forms.TextBox oscPortBox;
        private System.Windows.Forms.TextBox oscPath;
        private System.Windows.Forms.TextBox oscIpAddr;
        private System.Windows.Forms.Label oscPathHelper;
        private System.Windows.Forms.Label oscPortHelper;
        private System.Windows.Forms.GroupBox stats;
        private System.Windows.Forms.CheckBox runningCheckbox;
        private System.Windows.Forms.GroupBox verBox;
        private System.Windows.Forms.Label supportedVersionsHelper;
        private System.Windows.Forms.Label runningVersionLabel;
        private System.Windows.Forms.Label runningVersionHelper;
        private System.Windows.Forms.Timer rbVersionCheckTimer;
        private System.Windows.Forms.Timer updateGuiTimer;
        private System.Windows.Forms.NumericUpDown oscDelay;
        private System.Windows.Forms.CheckBox oscMimicHuman;
        private System.Windows.Forms.Label oscDelayHelper;
        private System.Windows.Forms.NumericUpDown oscHumanDelay;
        private System.Windows.Forms.LinkLabel selfSponsor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox versionsBox;
    }
}

