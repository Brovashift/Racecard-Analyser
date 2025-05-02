using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RacecardAnalyser
{
    public partial class timeCellTooltip : Form
    {
        public timeCellTooltip()
        {
            InitializeComponent();
        }

        public void SetTooltipPosition(int x, int y)
        {
            // Set the position of the form
            SetBounds(x, y, Width, Height);
        }

        public void SetTooltipText(string adjustedTime, string avgSpeed)
        {
            adjustedTimeLabel.Text = adjustedTime;
            avgSpeedLabel.Text = avgSpeed;
        }
    }
}
