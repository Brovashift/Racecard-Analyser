namespace RacecardAnalyser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.guideTabPage = new System.Windows.Forms.TabPage();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.BackButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingsHistoryDateLbl = new System.Windows.Forms.Label();
            this.settingCourseDateLbl = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.settingTripDateLbl = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.guideTabPage);
            this.tabControl1.Controls.Add(this.settingsTabPage);
            this.tabControl1.Location = new System.Drawing.Point(50, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1125, 626);
            this.tabControl1.TabIndex = 0;
            // 
            // guideTabPage
            // 
            this.guideTabPage.BackColor = System.Drawing.Color.Ivory;
            this.guideTabPage.Location = new System.Drawing.Point(4, 22);
            this.guideTabPage.Name = "guideTabPage";
            this.guideTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.guideTabPage.Size = new System.Drawing.Size(1117, 600);
            this.guideTabPage.TabIndex = 1;
            this.guideTabPage.Text = "User Guide";
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.BackColor = System.Drawing.Color.Ivory;
            this.settingsTabPage.Controls.Add(this.panel2);
            this.settingsTabPage.Controls.Add(this.settingsHistoryDateLbl);
            this.settingsTabPage.Controls.Add(this.panel1);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(1117, 600);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Transparent;
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.ForeColor = System.Drawing.Color.Yellow;
            this.BackButton.Location = new System.Drawing.Point(50, 17);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(101, 28);
            this.BackButton.TabIndex = 13;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.settingCourseDateLbl);
            this.panel1.Location = new System.Drawing.Point(42, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 62);
            this.panel1.TabIndex = 0;
            // 
            // settingsHistoryDateLbl
            // 
            this.settingsHistoryDateLbl.AutoSize = true;
            this.settingsHistoryDateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsHistoryDateLbl.ForeColor = System.Drawing.Color.Red;
            this.settingsHistoryDateLbl.Location = new System.Drawing.Point(39, 38);
            this.settingsHistoryDateLbl.Name = "settingsHistoryDateLbl";
            this.settingsHistoryDateLbl.Size = new System.Drawing.Size(169, 16);
            this.settingsHistoryDateLbl.TabIndex = 1;
            this.settingsHistoryDateLbl.Text = "Custom Historical Date:";
            // 
            // settingCourseDateLbl
            // 
            this.settingCourseDateLbl.AutoSize = true;
            this.settingCourseDateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingCourseDateLbl.ForeColor = System.Drawing.Color.Blue;
            this.settingCourseDateLbl.Location = new System.Drawing.Point(33, 21);
            this.settingCourseDateLbl.Name = "settingCourseDateLbl";
            this.settingCourseDateLbl.Size = new System.Drawing.Size(104, 15);
            this.settingCourseDateLbl.TabIndex = 0;
            this.settingCourseDateLbl.Text = "Course History:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(169, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(77, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "-12 months";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(296, 21);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(77, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "-18 months";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(423, 21);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(77, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "-24 months";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(793, 24);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(77, 17);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "-60 months";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(666, 24);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(77, 17);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "-48 months";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(539, 21);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(77, 17);
            this.radioButton6.TabIndex = 4;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "-36 months";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(793, 27);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(77, 17);
            this.radioButton7.TabIndex = 13;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "-60 months";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(666, 27);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(77, 17);
            this.radioButton8.TabIndex = 12;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "-48 months";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(539, 24);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(77, 17);
            this.radioButton9.TabIndex = 11;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "-36 months";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(423, 24);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(77, 17);
            this.radioButton10.TabIndex = 10;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "-24 months";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(296, 24);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(77, 17);
            this.radioButton11.TabIndex = 9;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "-18 months";
            this.radioButton11.UseVisualStyleBackColor = true;
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(169, 21);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(77, 17);
            this.radioButton12.TabIndex = 8;
            this.radioButton12.TabStop = true;
            this.radioButton12.Text = "-12 months";
            this.radioButton12.UseVisualStyleBackColor = true;
            // 
            // settingTripDateLbl
            // 
            this.settingTripDateLbl.AutoSize = true;
            this.settingTripDateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingTripDateLbl.ForeColor = System.Drawing.Color.Blue;
            this.settingTripDateLbl.Location = new System.Drawing.Point(33, 24);
            this.settingTripDateLbl.Name = "settingTripDateLbl";
            this.settingTripDateLbl.Size = new System.Drawing.Size(84, 15);
            this.settingTripDateLbl.TabIndex = 7;
            this.settingTripDateLbl.Text = "Trip History:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.radioButton7);
            this.panel2.Controls.Add(this.settingTripDateLbl);
            this.panel2.Controls.Add(this.radioButton8);
            this.panel2.Controls.Add(this.radioButton12);
            this.panel2.Controls.Add(this.radioButton9);
            this.panel2.Controls.Add(this.radioButton11);
            this.panel2.Controls.Add(this.radioButton10);
            this.panel2.Location = new System.Drawing.Point(42, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1036, 62);
            this.panel2.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::RacecardAnalyser.Properties.Resources.ra_background_darkBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1233, 725);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Racecard Analyser - Settings";
            this.tabControl1.ResumeLayout(false);
            this.settingsTabPage.ResumeLayout(false);
            this.settingsTabPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabPage guideTabPage;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Label settingsHistoryDateLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label settingCourseDateLbl;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.Label settingTripDateLbl;
        private System.Windows.Forms.Panel panel2;
    }
}