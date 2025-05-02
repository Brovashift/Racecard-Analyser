namespace RacecardAnalyser
{
    partial class timeCellTooltip
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
            this.adjustedTimeLabel = new System.Windows.Forms.Label();
            this.avgSpeedLabel = new System.Windows.Forms.Label();
            this.adjustedTimeLbl = new System.Windows.Forms.Label();
            this.avgSpeedLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // adjustedTimeLabel
            // 
            this.adjustedTimeLabel.AutoSize = true;
            this.adjustedTimeLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustedTimeLabel.Location = new System.Drawing.Point(12, 24);
            this.adjustedTimeLabel.Name = "adjustedTimeLabel";
            this.adjustedTimeLabel.Size = new System.Drawing.Size(86, 15);
            this.adjustedTimeLabel.TabIndex = 1;
            this.adjustedTimeLabel.Text = "Adjusted Time";
            // 
            // avgSpeedLabel
            // 
            this.avgSpeedLabel.AutoSize = true;
            this.avgSpeedLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgSpeedLabel.Location = new System.Drawing.Point(12, 54);
            this.avgSpeedLabel.Name = "avgSpeedLabel";
            this.avgSpeedLabel.Size = new System.Drawing.Size(67, 15);
            this.avgSpeedLabel.TabIndex = 2;
            this.avgSpeedLabel.Text = "Avg. Speed";
            // 
            // adjustedTimeLbl
            // 
            this.adjustedTimeLbl.AutoSize = true;
            this.adjustedTimeLbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustedTimeLbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.adjustedTimeLbl.Location = new System.Drawing.Point(12, 9);
            this.adjustedTimeLbl.Name = "adjustedTimeLbl";
            this.adjustedTimeLbl.Size = new System.Drawing.Size(87, 15);
            this.adjustedTimeLbl.TabIndex = 4;
            this.adjustedTimeLbl.Text = "Adjusted Time:";
            // 
            // avgSpeedLbl
            // 
            this.avgSpeedLbl.AutoSize = true;
            this.avgSpeedLbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avgSpeedLbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.avgSpeedLbl.Location = new System.Drawing.Point(12, 39);
            this.avgSpeedLbl.Name = "avgSpeedLbl";
            this.avgSpeedLbl.Size = new System.Drawing.Size(61, 15);
            this.avgSpeedLbl.TabIndex = 5;
            this.avgSpeedLbl.Text = "Avg. mph:";
            // 
            // timeCellTooltip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(112, 85);
            this.ControlBox = false;
            this.Controls.Add(this.avgSpeedLbl);
            this.Controls.Add(this.adjustedTimeLbl);
            this.Controls.Add(this.avgSpeedLabel);
            this.Controls.Add(this.adjustedTimeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "timeCellTooltip";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 2, 3);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "timeCellTooltip";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label adjustedTimeLabel;
        private System.Windows.Forms.Label avgSpeedLabel;
        private System.Windows.Forms.Label adjustedTimeLbl;
        private System.Windows.Forms.Label avgSpeedLbl;
    }
}