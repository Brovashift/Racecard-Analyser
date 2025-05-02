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
    public partial class courseCellTooltip : Form
    {
        public courseCellTooltip()
        {
            InitializeComponent();
        }

        public void SetTooltipPosition(int x, int y)
        {
            // Set the position of the form
            SetBounds(x, y, Width, Height);
        }

        public void SetTooltipText(string category, string direction, string speed)
        {
            // Set the text values in the UI elements of your tooltip form
            catLabel.Text = category;
            directionLabel.Text = direction;
            speedLabel.Text = speed;

        }
    }
}
