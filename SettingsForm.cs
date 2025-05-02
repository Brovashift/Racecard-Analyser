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
    public partial class SettingsForm : Form
    {
        private bool settingsFormMoved = false;
        private Point newLocation;
        private RacecardForm racecardFormInstance;
        public bool SettingsFormMoved => settingsFormMoved;
        public Point NewLocation => newLocation;
        private Point racecardFormOriginalLocation;

        public SettingsForm(RacecardForm racecardForm, Point originalLocation)
        {
            InitializeComponent();
            racecardFormOriginalLocation = originalLocation;
            racecardFormInstance = racecardForm;
            racecardFormInstance.Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = racecardFormOriginalLocation; // Set the location to match the RacecardForm
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            settingsFormMoved = true; // Indicate that the SettingsForm was moved
            newLocation = this.Location; // Store the new location
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            racecardFormInstance.Show();
            Close();
        }
    }
}
