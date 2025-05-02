using System;
using System.Linq;
using System.Windows.Forms;

namespace RacecardAnalyser
{
    partial class CourseLblToolTip
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
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.InformationTextBox = new System.Windows.Forms.TextBox();
            this.DirectionLabel = new System.Windows.Forms.Label();
            this.courselbl = new System.Windows.Forms.Label();
            this.directionlbl = new System.Windows.Forms.Label();
            this.speedlbl = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryLabel.Location = new System.Drawing.Point(60, 2);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(28, 15);
            this.CategoryLabel.TabIndex = 0;
            this.CategoryLabel.Text = "Cat.";
            // 
            // InformationTextBox
            // 
            this.InformationTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.InformationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InformationTextBox.CausesValidation = false;
            this.InformationTextBox.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.InformationTextBox.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationTextBox.Location = new System.Drawing.Point(12, 47);
            this.InformationTextBox.Multiline = true;
            this.InformationTextBox.Name = "InformationTextBox";
            this.InformationTextBox.ReadOnly = true;
            this.InformationTextBox.Size = new System.Drawing.Size(292, 78);
            this.InformationTextBox.TabIndex = 1;
            this.InformationTextBox.UseWaitCursor = true;
            // 
            // DirectionLabel
            // 
            this.DirectionLabel.AutoSize = true;
            this.DirectionLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectionLabel.Location = new System.Drawing.Point(278, 2);
            this.DirectionLabel.Name = "DirectionLabel";
            this.DirectionLabel.Size = new System.Drawing.Size(26, 15);
            this.DirectionLabel.TabIndex = 2;
            this.DirectionLabel.Text = "Dir.";
            // 
            // courselbl
            // 
            this.courselbl.AutoSize = true;
            this.courselbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.courselbl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.courselbl.Location = new System.Drawing.Point(8, 2);
            this.courselbl.Name = "courselbl";
            this.courselbl.Size = new System.Drawing.Size(48, 15);
            this.courselbl.TabIndex = 3;
            this.courselbl.Text = "Course:";
            // 
            // directionlbl
            // 
            this.directionlbl.AutoSize = true;
            this.directionlbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directionlbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.directionlbl.Location = new System.Drawing.Point(251, 2);
            this.directionlbl.Name = "directionlbl";
            this.directionlbl.Size = new System.Drawing.Size(27, 15);
            this.directionlbl.TabIndex = 4;
            this.directionlbl.Text = "Dir:";
            // 
            // speedlbl
            // 
            this.speedlbl.AutoSize = true;
            this.speedlbl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedlbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.speedlbl.Location = new System.Drawing.Point(8, 18);
            this.speedlbl.Name = "speedlbl";
            this.speedlbl.Size = new System.Drawing.Size(42, 15);
            this.speedlbl.TabIndex = 5;
            this.speedlbl.Text = "Speed:";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(60, 18);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(39, 15);
            this.speedLabel.TabIndex = 6;
            this.speedLabel.Text = "Speed";
            // 
            // CourseLblToolTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(327, 137);
            this.ControlBox = false;
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.speedlbl);
            this.Controls.Add(this.directionlbl);
            this.Controls.Add(this.courselbl);
            this.Controls.Add(this.DirectionLabel);
            this.Controls.Add(this.InformationTextBox);
            this.Controls.Add(this.CategoryLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CourseLblToolTip";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public void SetTooltipText(string category, string direction, string speed, string information)
        {
            CategoryLabel.Text = category;
            speedLabel.Text = speed;
            DirectionLabel.Text = direction;
            InformationTextBox.Text = information;

            InformationTextBox.Select(0, 0);
            InformationTextBox.SelectionLength = 0;

        }

        public void SetTooltipPosition(int x, int y)
        {
            // Set the position of the form
            SetBounds(x, y, Width, Height);
        }

        private Label CategoryLabel;
        private TextBox InformationTextBox;
        private Label DirectionLabel;
        private Label courselbl;
        private Label directionlbl;
        private Label speedlbl;
        private Label speedLabel;
    }
}