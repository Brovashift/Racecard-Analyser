namespace RacecardAnalyser
{
    partial class courseCellTooltip
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
            this.catLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.directionLabel = new System.Windows.Forms.Label();
            this.categoryLbl = new System.Windows.Forms.Label();
            this.speedLbl = new System.Windows.Forms.Label();
            this.DirectionLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // catLabel
            // 
            this.catLabel.AutoSize = true;
            this.catLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catLabel.Location = new System.Drawing.Point(12, 24);
            this.catLabel.Name = "catLabel";
            this.catLabel.Size = new System.Drawing.Size(56, 15);
            this.catLabel.TabIndex = 0;
            this.catLabel.Text = "Category";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(12, 54);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(41, 15);
            this.speedLabel.TabIndex = 1;
            this.speedLabel.Text = "Speed";
            // 
            // directionLabel
            // 
            this.directionLabel.AutoSize = true;
            this.directionLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directionLabel.Location = new System.Drawing.Point(12, 84);
            this.directionLabel.Name = "directionLabel";
            this.directionLabel.Size = new System.Drawing.Size(57, 15);
            this.directionLabel.TabIndex = 2;
            this.directionLabel.Text = "Direction";
            // 
            // categoryLbl
            // 
            this.categoryLbl.AutoSize = true;
            this.categoryLbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryLbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.categoryLbl.Location = new System.Drawing.Point(12, 9);
            this.categoryLbl.Name = "categoryLbl";
            this.categoryLbl.Size = new System.Drawing.Size(58, 15);
            this.categoryLbl.TabIndex = 4;
            this.categoryLbl.Text = "Category:";
            // 
            // speedLbl
            // 
            this.speedLbl.AutoSize = true;
            this.speedLbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.speedLbl.Location = new System.Drawing.Point(12, 39);
            this.speedLbl.Name = "speedLbl";
            this.speedLbl.Size = new System.Drawing.Size(42, 15);
            this.speedLbl.TabIndex = 5;
            this.speedLbl.Text = "Speed:";
            // 
            // DirectionLbl
            // 
            this.DirectionLbl.AutoSize = true;
            this.DirectionLbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectionLbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.DirectionLbl.Location = new System.Drawing.Point(12, 69);
            this.DirectionLbl.Name = "DirectionLbl";
            this.DirectionLbl.Size = new System.Drawing.Size(60, 15);
            this.DirectionLbl.TabIndex = 6;
            this.DirectionLbl.Text = "Direction:";
            // 
            // courseCellTooltip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(82, 111);
            this.ControlBox = false;
            this.Controls.Add(this.DirectionLbl);
            this.Controls.Add(this.speedLbl);
            this.Controls.Add(this.categoryLbl);
            this.Controls.Add(this.directionLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.catLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "courseCellTooltip";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 2, 3);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "courseCellTooltip";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label catLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label directionLabel;
        private System.Windows.Forms.Label categoryLbl;
        private System.Windows.Forms.Label speedLbl;
        private System.Windows.Forms.Label DirectionLbl;
    }
}