using CsvHelper;
using CsvHelper.Configuration;
using RacecardAnalyser.Classes;
using Svg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RacecardAnalyser.Classes.DataAnalyser;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;


namespace RacecardAnalyser
{
    public partial class RacecardForm : Form
    {
        private string targetRacecardFilePath = "D:\\Racecard Analyser C#\\RacecardAnalyser - 1.20.2\\csv\\Racecards\\racecards.csv";
        private string targetStdTimesFilePath = "D:\\Racecard Analyser C#\\RacecardAnalyser - 1.20.2\\csv\\Standard Times\\StdRaceTimes.csv";
        private string targetCourseInfoFilePath = "D:\\Racecard Analyser C#\\RacecardAnalyser - 1.20.2\\csv\\Race course Info\\RaceCourseInfo.csv";
        private string flatsGBdataPath = @"D:\Racecard Analyser C#\RacecardAnalyser - 1.20.2\csv\Data\GB\Flats\flatsData.csv";
        private string jumpsGBdataPath = @"D:\Racecard Analyser C#\RacecardAnalyser - 1.20.2\csv\Data\GB\Jumps\jumpsData.csv";
        //Cache Image Directory
        private string cacheDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"D:\Racecard Analyser C#\RacecardAnalyser - 1.20.2\Image Cache Directory\");

        //private string region; // Stores the selected race region (e.g., GB, IRE)
        private string raceCourse;
        private string raceType; // Stores the selected race type (e.g., Flat, Chase, Hurdle, NH Flat)
        private int analysisCounter = 0;
        private int racecardSelectionCount = 0;

        private CourseLblToolTip tooltipForm;
        private courseCellTooltip courseCellTooltipForm;
        private timeCellTooltip timeCellTooltipForm;

        // Define raceData as a class-level field
        private Dictionary<string, List<string>> raceData = new Dictionary<string, List<string>>();
        // Course Information
        private Dictionary<string, string> courseInfoDictionary = new Dictionary<string, string>();
        // Scores Dictionary
        private Dictionary<string, double> horseScores = new Dictionary<string, double>();

        DataAnalyser dataAnalyzer = new DataAnalyser();

        public RacecardForm()
        {
            InitializeComponent();
            FormInitialize();
        }

        private void FormInitialize()
        {
            tooltipForm = new CourseLblToolTip(); // Custom form for tooltip

            // Attach the MouseHover event handler to the CourseLbl control
            CourseLbl.MouseHover += (sender, e) => ShowTooltip(tooltipForm);
            CourseLbl.MouseLeave += (sender, e) => tooltipForm.Hide();

            // Attach the CellMouseEnter event handler to the DataGridView2
            dataGridView2.CellMouseEnter += dataGridView2_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView2_CellMouseLeave;

            // Initialize the courseCellTooltipForm instance
            courseCellTooltipForm = new courseCellTooltip();
            // Initialize the timeCellTooltipForm instance
            timeCellTooltipForm = new timeCellTooltip();

            // Load the course information from the CSV file into the dictionary
            LoadCourseInfoFromCsv();

            CreateDataGridView1ColumnHeaders();
            CreateDataGridView2ColumnHeaders();

            // Clear the raceData dictionary
            raceData.Clear();

            using (StreamReader reader = new StreamReader(targetRacecardFilePath))
            {
                // Skip header row if necessary
                if (!reader.EndOfStream)
                {
                    reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    if (fields.Length >= 12)
                    {
                        string raceCourse = fields[2];
                        string raceTime = fields[1];
                        string horseName = fields[11];

                        if (!raceData.ContainsKey(raceCourse))
                        {
                            raceData.Add(raceCourse, new List<string>());
                        }

                        List<string> raceTimes = raceData[raceCourse];

                        if (!raceTimes.Contains(raceTime))
                        {
                            raceTimes.Add(raceTime);
                        }
                    }
                }
            }

            treeView1.Nodes.Clear();
            // Populate TreeView with data from dictionary
            foreach (KeyValuePair<string, List<string>> race in raceData)
            {
                TreeNode parentNode = treeView1.Nodes.Add(race.Key, race.Key, race.Key);

                foreach (string raceTime in race.Value.OrderBy(time => time))
                {
                    parentNode.Nodes.Add(raceTime, raceTime, raceTime); // Add child nodes
                }
            }
            // Refresh the TreeView to update the UI
            treeView1.Refresh();
        }

        private void CreateDataGridView1ColumnHeaders()
        {
            // Clear existing column headers
            dataGridView1.Columns.Clear();

            // Set font style and size for the column headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            // Set foreground color and background color for the column headers
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(69, 129, 142);

            // Add column headers
            dataGridView1.Columns.Add("HorseName", "Horse"); // Column 1

            dataGridView1.Columns.Add("Score", "Score"); // Column #
            dataGridView1.Columns["Score"].HeaderCell.Style.BackColor = Color.Yellow;
            dataGridView1.Columns["Score"].HeaderCell.Style.ForeColor = Color.FromArgb(69, 129, 142);
            dataGridView1.Columns["Score"].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            //dataGridView1.Columns["Score"].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;

            dataGridView1.Columns.Add("Course", "Course"); // Column 3
            dataGridView1.Columns.Add("Trip", "Trip(f)"); // Column 4
            dataGridView1.Columns.Add("Going", "Going"); // Column 6
            dataGridView1.Columns.Add("Class", "Class"); // Column 5
            dataGridView1.Columns.Add("WGT", "WGT"); // Column 7
            dataGridView1.Columns.Add("OR", "OR"); // Column 8
            dataGridView1.Columns.Add("HG", "HG"); // Column 9
            dataGridView1.Columns.Add("Jockey", "Jockey"); // Column 11
            dataGridView1.Columns.Add("TrainerRTF", "Trainer RTF%"); // Column 12
            dataGridView1.Columns.Add("Trainer14Days", "Trainer 14Days"); // Column 13
            dataGridView1.Columns.Add("Form", "Form"); // Column 14
            dataGridView1.Columns.Add("LR", "LR"); // Column 15
            dataGridView1.Columns.Add("TS", "TS"); // Column 16
            dataGridView1.Columns.Add("Age", "Age"); // Column 19
            dataGridView1.Columns.Add("RPR", "RPR"); // Column 20
            dataGridView1.Columns.Add("GoingDetails", "Going Details"); // Column 21  
            dataGridView1.Columns.Add("Trainer", "Trainer"); // Column 22  
            dataGridView1.Columns.Add("Comments", "Comments"); // Column 23
            dataGridView1.Columns.Add("Spotlight", "Spotlight"); // Column 24
            // Add more column headers as needed
            dataGridView1.Columns.Add("Sex", "Sex"); // Column 25
            dataGridView1.Columns.Add("Dam", "Dam"); // Column 26
            dataGridView1.Columns.Add("Sire", "Sire"); // Column 27
            dataGridView1.Columns.Add("Owner", "Owner"); // Column 28
            dataGridView1.Columns.Add("Trip_f", "Trip_f"); // Column 31

            dataGridView1.Columns.Add("CustomRating", "CR"); // Column #
            dataGridView1.Columns["CustomRating"].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            //dataGridView1.Columns["Rating"].Frozen = true;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(1);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.White;

            dataGridView1.CellClick += DataGridView1_CellClick;

        }

        private void CreateDataGridView2ColumnHeaders()
        {
            // Clear existing column headers
            dataGridView2.Columns.Clear();

            // Set font style and size for the column headers
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            // Set foreground color and background color for the column headers
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(69, 129, 142);

            // Add column headers
            dataGridView2.Columns.Add("date", "Date   "); // Column 1

            // off time hidden **************************************************************
            dataGridView2.Columns.Add("off", "Off Time");
            dataGridView2.Columns["off"].Visible = false;

            // Add the Silks column
            DataGridViewImageColumn silksColumn = new DataGridViewImageColumn();
            silksColumn.Name = "Silks";
            silksColumn.HeaderText = "Silks";
            silksColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Adjust the image layout if needed
            dataGridView2.Columns.Add(silksColumn);

            dataGridView2.Columns.Add("HorseName", "Horse"); // Column 2
            dataGridView2.Columns.Add("course", "Course"); // Column 3 

            dataGridView2.Columns.Add("trip", "Trip"); // Column 4

            // Add a hidden column for the additional distance value Yards
            dataGridView2.Columns.Add("hiddenDistance", "Hidden Distance");
            dataGridView2.Columns["hiddenDistance"].Visible = false;

            dataGridView2.Columns.Add("going", "Going"); // Column 5
            dataGridView2.Columns.Add("Class", "Class"); // Column 6
            dataGridView2.Columns.Add("WGT", "WGT"); // Column 7
            dataGridView2.Columns.Add("OR", "OR"); // Column 8
            dataGridView2.Columns.Add("time", "Winner Time"); // Column 9

            // Add a hidden column for the additional time value Seconds
            dataGridView2.Columns.Add("hiddenTime", "Hidden Time");
            dataGridView2.Columns["hiddenTime"].Visible = false;

            dataGridView2.Columns.Add("position", "Pos"); // Column 10
            dataGridView2.Columns.Add("btn", "Btn"); // Column 11
            dataGridView2.Columns.Add("HG", "HG"); // Column 12
            dataGridView2.Columns.Add("draw", "Draw"); // Column 13
            dataGridView2.Columns.Add("Jockey", "Jockey"); // Column 14
            dataGridView2.Columns.Add("prize", "Prize"); // Column 15
            dataGridView2.Columns.Add("Type", "Type"); // Column 16
            dataGridView2.Columns.Add("RPR", "RPR"); // Column 17
            dataGridView2.Columns.Add("Comments", "Comments"); // Column 18
            dataGridView2.Columns.Add("RTS", "RTS"); // Column 19
            dataGridView2.Columns.Add("SP", "SP"); // Column 20

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView2.ColumnHeadersDefaultCellStyle.Padding = new Padding(1);
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.DefaultCellStyle.BackColor = Color.White;
            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView2.GridColor = Color.White;

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode clickedNode = e.Node;

            // Check if the clicked node has child nodes
            if (clickedNode.Parent != null)
            {
                // Get the race course from the parent node
                raceCourse = clickedNode.Parent.Text;
                // Get the race time from the clicked node
                string raceTime = clickedNode.Text;

                // Call your method or perform any desired actions for the race time node
                PopulateRaceInfo(raceCourse, raceTime);
            }
            else
            {
                // It is a parent node, do nothing or handle parent node click as needed
            }

        }

        private void PopulateRaceInfo(string raceCourse, string raceTime)
        {
            treeView1.Invoke((Action)(() =>
            {
                Cursor.Current = Cursors.WaitCursor;
                treeView1.Enabled = false;
            }));


            // Clear existing items from parent DataGridView
            dataGridView1.Rows.Clear();
            analysisCounter = 0;
            percentageCompletionLabel.Text = "%";
            horseScores.Clear();

            // Declare variables
            DateTime offTime;
            string course;
            string raceName;
            DateTime raceDate;
            string prizeMoney;
            string formattedPrizeMoney;
            string raceDist;
            string raceDist_mfy;
            string raceClass;
            string going;
            string fieldSize;
            string weather;
            string ageBand;
            double standardTime;
            double lbsPerLength;
            string surface;

            string[][] targetData = null;
            string[] targetRowData = null;

            using (var reader = new StreamReader(targetRacecardFilePath))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",", // Set the delimiter to comma (',')
                    TrimOptions = TrimOptions.Trim // Trim the cell values
                };

                using (var csv = new CsvReader(reader, configuration))
                {
                    List<string[]> records = new List<string[]>();

                    while (csv.Read())
                    {
                        int fieldCount = csv.Context.Parser.Record.Length;
                        string[] record = new string[fieldCount];
                        for (int i = 0; i < fieldCount; i++)
                        {
                            record[i] = csv.GetField(i);
                        }
                        records.Add(record);
                    }

                    targetData = records.ToArray();
                }
            }

            // Find match in the target data based on race time
            targetRowData = targetData.FirstOrDefault(row => row[1] == raceTime);

            if (targetRowData != null)
            {
                // Get race data from target row
                offTime = DateTime.Parse(targetRowData[1]);
                course = targetRowData[2];
                raceName = targetRowData[3];
                raceDate = DateTime.Parse(targetRowData[0]);
                prizeMoney = targetRowData[10];
                raceDist = targetRowData[4]; //[5]
                raceClass = targetRowData[6];
                raceType = targetRowData[7];
                going = targetRowData[9];
                fieldSize = targetRowData[8];
                weather = targetRowData[12];
                ageBand = targetRowData[11];
                surface = targetRowData[14];
                //region = targetRowData[18];
                // ******
                raceDist_mfy = targetRowData[4]; // mile/furlong/yard format

                // Remove non-numeric characters from prizeMoney
                prizeMoney = new string(prizeMoney.Where(char.IsDigit).ToArray());
                formattedPrizeMoney = "£" + double.Parse(prizeMoney).ToString("#,##0");

                // Display race data in labels
                TimeLbl.Text = offTime.ToString("h:mm");
                CourseLbl.Text = course;
                RaceNameLbl.Text = raceName;
                DateLbl.Text = raceDate.ToString("dd MMM yy");
                PrizeLbl.Text = formattedPrizeMoney;
                DistLbl.Text = raceDist;
                ClassLbl.Text = raceClass;
                RaceTypeLbl.Text = raceType + "/" + surface;
                GoingLbl.Text = going;
                RunnersLbl.Text = fieldSize;
                WeatherLbl.Text = weather;
                AgeLbl.Text = ageBand;

                //**************************
                //**   GETTING STD TIMES  **
                //**************************

                string formatedRaceCourse = raceCourse.Replace("(", "").Replace(")", "");

                // Read data from CSV file
                var sheet1Data = File.ReadAllLines(targetStdTimesFilePath)
                    .Select(line => line.Split(','));

                // Find match in Sheet based on race course and race distance
                var targetRowData2 = sheet1Data.FirstOrDefault(row => row[1] == formatedRaceCourse && row[2] == raceDist_mfy);

                if (targetRowData2 != null)
                {
                    // Get additional race data from Sheet
                    standardTime = double.Parse(targetRowData2[5]);
                    lbsPerLength = double.Parse(targetRowData2[7]);

                    // Calculate minutes, seconds, and milliseconds
                    long minutes = (long)(standardTime / 60);
                    long seconds = (long)(standardTime % 60);
                    long milliseconds = (long)Math.Round((standardTime - (long)standardTime) * 10, 0); // previously * 100, 0);

                    // Display additional race data in labels
                    string formattedTime = $"{minutes:00}:{seconds:00}.{milliseconds:0}"; // Change the milliseconds format specifier to display a single decimal place

                    StdTimeLbl.Text = formattedTime;
                    LbsLbl.Text = lbsPerLength.ToString("0.00");
                }
                else
                {
                    // No standard times found, clear labels
                    StdTimeLbl.Text = "No Data";
                    LbsLbl.Text = "No Data";
                }

                // Populate parent DataGridView with data from Sheet1
                PopulateDataGridView1(raceCourse, raceTime);
            }
            else
            {
                // Display error message if no match was found
                MessageBox.Show($"No race found for {raceTime} at {raceCourse}");
            }

        }

        private async void PopulateDataGridView1(string raceCourseName, string raceTime)
        {
            // Clear existing items from parent DataGridView
            dataGridView1.Rows.Clear();

            List<string[]> records = new List<string[]>();

            using (var reader = new StreamReader(targetRacecardFilePath))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",", // Set the delimiter to comma (',')
                    TrimOptions = TrimOptions.Trim // Trim the cell values
                };

                using (var csv = new CsvReader(reader, configuration))
                {
                    while (csv.Read())
                    {
                        int fieldCount = csv.Context.Parser.Record.Length;
                        string[] record = new string[fieldCount];
                        for (int i = 0; i < fieldCount; i++)
                        {
                            record[i] = csv.GetField(i);
                        }
                        records.Add(record);
                    }
                }
            }

            // Filter the records based on raceCourseName and raceTime
            var filteredRecords = records.Where(r => r[2].Trim() == raceCourseName && r[1].Trim() == raceTime);

            // Store the horse names in a separate list
            List<string> horseNames = filteredRecords.Select(record => record[15].Trim()).ToList();

            foreach (var record in filteredRecords)
            {
                // Create a new row and set the cell values
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);

                row.Cells[0].Value = record[15].Trim(); // Column 1 Name
                row.Cells[0].ToolTipText = "Click to filter by Horse Name"; // Tooltip for Horse Name column

                row.Cells[1].Style.BackColor = Color.FromArgb(25, 25, 122);
                row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                row.Cells[2].Value = record[2].Trim(); // Column 2 course
                row.Cells[3].Value = record[5].Trim(); // Column 3 Trip [4]

                row.Cells[4].Value = record[9].Trim(); // Column 4 Going
                row.Cells[5].Value = record[6].Trim(); // Column 5 Class
                row.Cells[6].Value = record[30].Trim(); // Column 6 WGT
                row.Cells[7].Value = record[31].Trim().Replace(".0", ""); // Column 7 OR

                if (!string.IsNullOrEmpty(record[29].Trim()))
                {
                    row.Cells[8].Value = record[28].Trim() + " (" + record[29].Trim() + ") "; // Column 8 HG/HG1
                }
                else
                {
                    row.Cells[8].Value = record[28].Trim();
                }

                row.Cells[9].Value = record[34].Trim(); // Column 9 Jockey
                row.Cells[9].ToolTipText = "Click to filter by Jockey Name"; // Tooltip for Jockey Name column

                row.Cells[10].Value = record[37].Trim(); // Column 10 Trainer RTF
                row.Cells[11].Value = record[22].Trim(); // Column 11 Trainer 14 days
                row.Cells[12].Value = record[36].Trim(); // Column 12 Form
                row.Cells[13].Value = record[35].Trim(); // Column 13 LR
                row.Cells[14].Value = record[33].Trim().Replace(".0", ""); // Column 14 TS 
                row.Cells[15].Value = record[16].Trim(); // Column 15 Age
                row.Cells[16].Value = record[32].Trim().Replace(".0", ""); // Column 16 RPR
                row.Cells[17].Value = record[13].Trim(); // Column 17 Going Details
                row.Cells[18].Value = record[21].Trim(); // Column 18 Trainer
                row.Cells[19].Value = record[24].Trim(); // Column 19 Comments
                row.Cells[20].Value = record[25].Trim(); // Column 20 Spotlight
                row.Cells[21].Value = record[17].Trim(); // Column 21 Sex
                row.Cells[22].Value = record[19].Trim(); // Column 22 dam
                row.Cells[23].Value = record[20].Trim(); // Column 23 sire
                row.Cells[24].Value = record[23].Trim(); // Column 24 Owner

                double distance = double.Parse(record[5].Trim());
                string formattedDistance = $"{distance:0}f";
                row.Cells[25].Value = formattedDistance; // Column 26 dist f

                // Replace "nan" with "-" for specific columns
                int[] columnsToReplaceNaN = { 5, 6, 13, 15 }; // Specify the column indices to replace nan
                foreach (int columnIndex in columnsToReplaceNaN)
                {
                    string cellValue = row.Cells[columnIndex].Value?.ToString();
                    if (cellValue == "nan")
                    {
                        cellValue = "-";
                    }
                    row.Cells[columnIndex].Value = cellValue;
                }

                dataGridView1.Rows.Add(row);
                racecardSelectionCount = dataGridView1.Rows.Count;
            }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.ClearSelection();

            if (analyseRacecardCheckBox.Checked )
            {
                // Call methods or access properties of the DataAnalyzer instance
                dataAnalyzer.AnalyseRacecardData(filteredRecords);

                await PopulateDataGridView2(horseNames); 
            }
            else if (analyseRacecardCheckBox.Checked.Equals(false))
            {
                dataGridView2.Rows.Clear();

                treeView1.Invoke((Action)(() =>
                {
                    treeView1.Enabled = true;
                }));

            }
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0) // Check if the clicked cell is in column index 0
                {
                    string selectedHorseName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    await PopulateDataGridView2(selectedHorseName);
                }
                else if (e.ColumnIndex == 9) // Check if the clicked cell is in column index 9
                {
                    string selectedJockeyName = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    await PopulateDataGridView2ByJockeyName(selectedJockeyName);
                }
            }
        }

        private async Task PopulateDataGridView2(List<string> horseNames)
        {
            await PopulateDataGridView(horseNames, r => horseNames.Contains(r[23].Trim()));
        }

        private async Task PopulateDataGridView2(string horseName)
        {
            await PopulateDataGridView(new List<string> { horseName }, r => r[23].Trim() == horseName);
        }
        private async Task PopulateDataGridView2ByJockeyName(string jockeyName)
        {
            await PopulateDataGridView(new List<string> { jockeyName }, r => r[31].Trim() == jockeyName);
        }

        private async Task PopulateDataGridView(List<string> filterValues, Func<string[], bool> filterCondition)
        {
            // Clear existing items from dataGridView2
            dataGridView2.Rows.Clear();

            LoadingForm loadingForm = new LoadingForm();
            ManualResetEvent dataThreadCompleted = new ManualResetEvent(false); // ManualResetEvent to signal data thread completion

            // Create and show the loading form on a separate thread
            Thread loadingThread = new Thread(() =>
            {
                try
                {
                    loadingForm.StartPosition = FormStartPosition.Manual;
                    loadingForm.Location = new System.Drawing.Point(
                        this.Left + (this.Width - loadingForm.Width) / 2,
                        this.Top + (this.Height - loadingForm.Height) / 2);
                    // Wait for dataThread to complete or 2000ms, whichever comes first
                    if (!dataThreadCompleted.WaitOne(500))
                    {
                        // Data thread has not completed within 2000ms, show the loading form
                        loadingForm.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    // Exclude the "being Aborted" message
                    if (!(ex is ThreadAbortException))
                    {
                        // Handle other exceptions if necessary
                        MessageBox.Show("Error in loadingForm thread: " + ex.Message);
                    }
                }
            });
            loadingThread.Start();

            await Task.Run(async () =>
            {
                // Load data from the second CSV file
                List<string[]> records = LoadRecordsFromCSV();

                // Filter the records based on the filter condition
                var filteredRecords = records.Where(filterCondition).ToList();

                // Load images asynchronously and populate dataGridView2
                var tasks = new List<Task>();

                // Populate dataGridView2 with the filtered records
                foreach (var record in filteredRecords)
                {
                    tasks.Add(ProcessRecordAsync(record));
                }
                await Task.WhenAll(tasks); // Wait for all asynchronous tasks to complete

                // Signal data thread completion
                dataThreadCompleted.Set();

                // Call methods or access properties of the DataAnalyzer instance
                dataAnalyzer.AnalyseHistoricalData(filteredRecords);

                // Close the loading form if it is still open
                if (loadingForm.Visible)
                {
                    loadingForm.Invoke((Action)(() =>
                    {
                        loadingForm.Close();
                        loadingForm.Dispose();
                    }));
                }

                dataGridView2.Invoke((Action)(() =>
                {
                    CourseAnalysis(dataAnalyzer);
                    UpdateHorseScores();

                    if (filterValues.Count > 1)
                    {
                        AnalysisCount();
                    }

                    // Unlock the treeView1
                    treeView1.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }));


            });
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ClearSelection();
        }

        private async Task ProcessRecordAsync(string[] record)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView2);

            // Set the cell values based on the record data
            row.Cells[0].Value = record[0].Trim(); // Column 1 date
            row.Cells[0].ToolTipText = "Click to filter by race"; // Tooltip for race makeu up

            row.Cells[1].Value = record[3].Trim(); // off time

            string imageUrl = record[41].Trim(); // Column 2 Silks
            // Load the image from the URL
            Image image = await LoadImageFromUrlWithCaching(imageUrl);
            row.Cells[2].Value = image; // Column 2 Silks

            row.Cells[3].Value = record[23].Trim(); // Column 3 horse name
            row.Cells[4].Value = record[2].Trim(); // Column 4 Course

            row.Cells[5].Value = record[12].Trim(); // Column 5 Distance [11]

            // Assign the additional distance value to the hidden column
            row.Cells[6].Value = record[14].Trim(); // Yards

            row.Cells[7].Value = record[15].Trim(); // Column 7 Going
            row.Cells[8].Value = record[6].Trim(); // Column 8 class
            row.Cells[9].Value = record[26].Trim(); // Column 9 wgt
            row.Cells[10].Value = record[34].Trim(); // Column 10 or
            row.Cells[11].Value = record[28].Trim(); // Column 11 time

            // Assign the additional distance value to the hidden column
            row.Cells[12].Value = record[29].Trim(); // Seconds

            row.Cells[13].Value = record[19].Trim(); // Column 13 poition
            row.Cells[14].Value = record[21].Trim(); // Column 14 ovr_btn
            row.Cells[15].Value = record[27].Trim(); // Column 15 hg
            row.Cells[16].Value = record[20].Trim(); // Column 16 draw
            row.Cells[17].Value = record[31].Trim(); // Column 17 Jockey

            string prizeMoney = record[33].Trim(); // Column 18 Prize
                                                   // Remove the diamond symbol from prizeMoney
            prizeMoney = prizeMoney.Replace("�", "");
            row.Cells[18].Value = "£" + prizeMoney;

            row.Cells[19].Value = record[4].Trim(); // Column 19 Type
            row.Cells[20].Value = record[35].Trim(); // Column 20 rpr   
            row.Cells[21].Value = record[42].Trim(); // Column 21 comments
            row.Cells[22].Value = record[36].Trim(); // Column 22 RTS   
            row.Cells[23].Value = record[30].Trim(); // Column 23 SP
            //string ownerName = record[40].Trim(); // Owner name

            dataGridView2.Invoke((Action)(() =>
            {
                dataGridView2.Rows.Add(row);
                countLabel.Text = dataGridView2.Rows.Count.ToString();
            }));
        }

        private async Task<Image> LoadImageFromUrlWithCaching(string imageUrl)
        {
            // Generate a unique identifier from the imageUrl
            string uniqueIdentifier = GenerateUniqueFileName(imageUrl);

            string cacheFilePath = Path.Combine(cacheDirectoryPath, $"{uniqueIdentifier}");

            if (File.Exists(cacheFilePath)) // If image stored locally
            {
                using (var stream = new FileStream(cacheFilePath, FileMode.Open))
                {
                    return Image.FromStream(stream);
                }
            }
            else
            {
                using (var webClient = new WebClient()) // Otherwise get the image from the web server
                {
                    try
                    {
                        var svgContent = await webClient.DownloadStringTaskAsync(imageUrl); // remote server returned 403 Forbidden - too many records
                        var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgContent);

                        svgDocument.Width = 100;
                        svgDocument.Height = 100;

                        using (var bitmap = svgDocument.Draw())
                        {
                            var image = new Bitmap(bitmap);
                            image.Save(cacheFilePath);

                            return image;
                        }
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Error retrieving image from server. Do you have an internet connection? " + ex.Message);
                        return null;
                    }
                }

            }
        }

        private string GenerateUniqueFileName(string imageUrl)
        {
            string uniqueIdentifier = GenerateUniqueIdentifier(imageUrl);
            return $"{uniqueIdentifier}.svg"; // Append .svg extension
        }

        private string GenerateUniqueIdentifier(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void AnalysisCount()
        {
            int totalAnalysisCount = 5; //############ UPDATE THIS NUMBER+ AFTER EVERY COMPLETED COLUMN ANALYSIS ################

            int newTotalAnalysisCount = totalAnalysisCount * racecardSelectionCount;
            // Calculate the percentage completion
            double percentageCompletion = (double)analysisCounter / newTotalAnalysisCount * 100;

            // Update the UI to display the percentage completion
            percentageCompletionLabel.Text = $"{percentageCompletion:F0}%"; // Display as whole percentage
            percentageCompletionLabel.ForeColor = (percentageCompletion <= 59) ? Color.Red : Color.LimeGreen;
        }

        private void UpdateHorseScores()
        {
            foreach (var horseScorePair in horseScores)
            {
                string horseName = horseScorePair.Key;
                double score = horseScorePair.Value;

                dataGridView1.Invoke((Action)(() =>
                {
                    // Find the corresponding row in dataGridView1
                    DataGridViewRow row = dataGridView1.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["HorseName"].Value?.ToString() == horseName);

                    if (row != null)
                    {
                        // Update the score cell in the "Scores" column
                        score = (int)Math.Truncate(score);
                        row.Cells["Score"].Value = score;

                        // Get the cell for the "Scores" column
                        DataGridViewCell scoreCell = row.Cells["Score"];

                        // Update the ForeColor based on score value
                        if (score > 0)
                        {
                            scoreCell.Style.ForeColor = Color.Aqua; // Set to a high value color
                        }
                        else if (score < 0)
                        {
                            scoreCell.Style.ForeColor = Color.LightPink; // Set to a low value color
                        }
                        else
                        {
                            score = -1;
                            row.Cells["Score"].Value = score;
                            scoreCell.Style.ForeColor = Color.LightPink;
                        }
                    }
                }));
            }
            dataGridView1.ClearSelection();

            CustomRatings(dataAnalyzer);

        }

        private void CustomRatings(DataAnalyser dataAnalyzer)
        {
            List<CustomRatingsClass> customRatingsList = dataAnalyzer.AdjustedRatings();

            foreach (var customRatings in customRatingsList)
            {
                string horseName = customRatings.HorseName;
                double? rating = customRatings.CalculatedCustomRating; // Use a nullable double

                if (rating.HasValue) // Check if the rating is not null
                {
                    dataGridView1.Invoke((Action)(() =>
                    {
                        // Find the corresponding row in dataGridView1
                        DataGridViewRow row = dataGridView1.Rows
                            .Cast<DataGridViewRow>()
                            .FirstOrDefault(r => r.Cells["HorseName"].Value?.ToString() == horseName);

                        if (row != null)
                        {
                            // Update the custom rating cell in the "CustomRating" column
                            row.Cells["CustomRating"].Value = rating;

                            // Get the cell for the "CustomRating" column
                            DataGridViewCell customRatingCell = row.Cells["CustomRating"];

                            customRatingCell.Style.ForeColor = Color.Red;
                            //customRatingCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            customRatingCell.Style.WrapMode = DataGridViewTriState.True;

                        }
                    }));
                }

                // Perform any other actions, like updating your horseScores dictionary or other calculations
            }

        }

        private void CourseAnalysis (DataAnalyser dataAnalyser) 
        { 
            List<CourseAnalysisClass> courseAnalysisResults = dataAnalyser.CourseAnalyser(targetCourseInfoFilePath);

            // Clear the existing horseScores dictionary
            horseScores.Clear();

            foreach (var courseAnalysisResult in courseAnalysisResults)
            {
                string horseName = courseAnalysisResult.HorseName;
                bool courseSpecialist = courseAnalysisResult.CourseSpecialist;
                bool courseExposure = courseAnalysisResult.CourseExposure;
                bool IsUnexposed = courseAnalysisResult.IsUnexposed;
                string favouredDirection = courseAnalysisResult.FavouredDirection;
                string courseName = courseAnalysisResult.SelectedCourseName;
                string racecardCourseDirection = courseAnalysisResult.SelectedCourseDirection;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string horseNameInGrid = row.Cells[0].Value?.ToString().Trim();
                    if (horseNameInGrid == horseName)
                    {
                        DataGridViewCell cell = row.Cells[2]; // Course column index [2]
                        if (courseExposure == true)
                        {
                            cell.Style.BackColor = Color.FromArgb(144, 238, 144); // light Green
                            analysisCounter++; // Increment global score
                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1; 
                            // + 1 : 1; First number is the amount to increment by if records already exists, increments by second number if record doesn't exist 

                            if (courseSpecialist == true)
                            {
                                cell.Style.BackColor = Color.Green;
                                analysisCounter++; // Increment global score
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                break;
                            }
                        }
                        else // Its unexposed to the course
                        { 
                            cell.Style.ForeColor = Color.Red;
                            analysisCounter++; // Increment global score
                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] - 2 : -2;
                        }

                        if (favouredDirection == racecardCourseDirection) // Favoured direction DOES suit race direction ################
                        {
                            if (favouredDirection == "Right")
                            {
                                string cellContent = $"{courseName} \u21AA";
                                Size textSize = TextRenderer.MeasureText(cellContent, new Font("Arial Rounded MT", 10));
                                int requiredWidth = textSize.Width + 5; // Adding a small buffer for padding

                                cell.Value = cellContent;
                                cell.Style.ForeColor = Color.Green;
                                cell.Style.Font = new Font("Arial Rounded MT", 10);
                                cell.OwningColumn.Width = requiredWidth; // Set the width of the column

                                analysisCounter++; // Increment analysisCounter
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2; //Increment global score
                                break;
                            }
                            else if (favouredDirection == "Left")
                            {
                                string cellContent = $"\u21A9 {courseName}";
                                Size textSize = TextRenderer.MeasureText(cellContent, new Font("Arial Rounded MT", 10));
                                int requiredWidth = textSize.Width + 5; // Adding a small buffer for padding

                                cell.Value = cellContent;
                                cell.Style.ForeColor = Color.Green;
                                cell.Style.Font = new Font("Arial Rounded MT", 10);
                                cell.OwningColumn.Width = requiredWidth; // Set the width of the column

                                analysisCounter++; // Increment analysisCounter
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2; //Increment global score
                                break;
                            }
                        }
                        else if (favouredDirection != racecardCourseDirection) // Favoured direction DOESN'T suit race direction ################
                        {
                            if (favouredDirection == "Right")
                            {
                                string cellContent = $"{courseName} \u21AA";
                                Size textSize = TextRenderer.MeasureText(cellContent, new Font("Arial Rounded MT", 10));
                                int requiredWidth = textSize.Width + 5; // Adding a small buffer for padding

                                cell.Value = cellContent;
                                cell.Style.ForeColor = Color.Red;
                                cell.Style.Font = new Font("Arial Rounded MT", 10);
                                cell.OwningColumn.Width = requiredWidth; // Set the width of the column

                                analysisCounter++; // Increment global score
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] - 2 : -2;
                                break;
                            }
                            else if (favouredDirection == "Left")
                            {
                                string cellContent = $"\u21A9 {courseName}";
                                Size textSize = TextRenderer.MeasureText(cellContent, new Font("Arial Rounded MT", 10));
                                int requiredWidth = textSize.Width + 5; // Adding a small buffer for padding

                                cell.Value = cellContent;
                                cell.Style.ForeColor = Color.Red;
                                cell.Style.Font = new Font("Arial Rounded MT", 10);
                                cell.OwningColumn.Width = requiredWidth; // Set the width of the column

                                analysisCounter++; // Increment global score
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] - 2 : -2;
                                break;
                            }
                        }
                        else
                        {
                            // No Favoured Direction
                            break;
                        }

                    }
                }

            }
            //GoingAnalysis(dataAnalyzer);
            TripAnalysis(dataAnalyzer);
        }

        private void TripAnalysis(DataAnalyser dataAnalyzer)
        {
            List<TripAnalysisResultClass> tripAnalysisResults = dataAnalyzer.TripAnalyser();
            Dictionary<string, CourseProfileClass> courseProfiles = dataAnalyzer.LoadCourseProfilesFromCSV(targetCourseInfoFilePath);
            DateTime dateThreshold = DateTime.Now.AddMonths(-18); // Checks last 12 months only

            foreach (var analysisResult in tripAnalysisResults)
            {
                string horseName = analysisResult.HorseName;
                //TripAnalysisResult analysisResult = analysisResultEntry;

                // Get the selected course name from the horse's analysis result
                string selectedCourseName = analysisResult.selectedCourseName;
                // If the selectedCourseName is not found in the courseProfiles dictionary, use a default course profile as a backup.
                CourseProfileClass selectedCourseProfile;
                if (!courseProfiles.TryGetValue(selectedCourseName, out selectedCourseProfile))
                {
                    // Check if the selectedCourseName exists in the courseInfoDictionary
                    if (courseInfoDictionary.ContainsKey(selectedCourseName))
                    {
                        // If the courseInfoDictionary contains the course name, use the information from the dictionary to create a default course profile
                        string courseInfo = courseInfoDictionary[selectedCourseName];
                        // Split the courseInfo string to get individual properties (assuming the information is comma-separated)
                        string[] courseInfoParts = courseInfo.Split(',');
                        // Use the parts to create a default course profile
                        selectedCourseProfile = new CourseProfileClass
                        {
                            CourseName = selectedCourseName,
                            Category = courseInfoParts[1].Trim(),
                            Constitution = courseInfoParts[2].Trim(),
                            CourseDirection = courseInfoParts[3].Trim(),
                            Speed = courseInfoParts[4].Trim(),
                            // Add other properties as needed based on the available information
                        };
                    }
                    else
                    {
                        // If the course name is not found in the courseInfoDictionary as well, create a default course profile with some default values
                    }
                }

                var filteredAnalysisResults = tripAnalysisResults
                   .Where(entry => (int.TryParse(entry.FinishPosition, out int finishPosition) ? finishPosition : 0) <= 4) // Check for finish position
                   .Where(entry => entry.BeatenBy <= 5) // Check for beaten_by
                   .Where(entry => entry.Date >= dateThreshold) // Check if the date is after the date threshold
                   .ToList();

                // Get the selected course's constitution and speed from the horse's analysis result
                string selectedCourseConstitution = selectedCourseProfile.Constitution;
                int selectedCourseRatingValue = ConvertConstitutionToRating(selectedCourseConstitution);

                // Get all other courses with the same rating as the selected course for the current horse
                var otherCoursesWithSameRating = courseProfiles.Values
                    .Where(courseProfile => ConvertConstitutionToRating(courseProfile.Constitution) == selectedCourseRatingValue)
                    .Select(courseProfile => new { courseProfile.CourseName, courseProfile.CourseValue })
                    .ToList();

                // Get all other courses with different rating than the selected course for the current horse
                var otherCoursesWithDifferentRating = courseProfiles.Values
                    .Where(courseProfile => ConvertConstitutionToRating(courseProfile.Constitution) != selectedCourseRatingValue)
                    .Select(courseProfile => new { courseProfile.CourseName, courseProfile.CourseValue })
                    .ToList();


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string horseNameInGrid = row.Cells[0].Value?.ToString().Trim();
                    if (horseNameInGrid == horseName)
                    {
                        DataGridViewCell cell = row.Cells[3]; // Assuming Trip column is at index 4
                        if (!analysisResult.IsUnexposed)
                        {
                            double racecardDistance = analysisResult.RacecardDistance;
                            double historicalDistance = analysisResult.HistoricalDistance;
                            // Add your variables....

                            if (racecardDistance > historicalDistance)
                            {
                                cell.Style.BackColor = Color.FromArgb(255, 150, 150); // light Red
                                analysisCounter++; // Increment global score
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] - 2 : -2;

                                if (selectedCourseRatingValue == 1 && filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                    filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName)))
                                {
                                    cell.Style.BackColor = Color.Red;
                                    analysisCounter++; // Increment global score
                                    horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                    break;
                                }
                                if (selectedCourseRatingValue == 2)
                                {
                                    if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) != 1)))
                                    {
                                        cell.Style.BackColor = Color.Red;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                        break;
                                    }
                                    else if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) == 1)))
                                    {
                                        if (historicalDistance > racecardDistance)
                                        {
                                            cell.Style.BackColor = Color.Red;
                                            analysisCounter++; // Increment global score
                                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                            break;
                                        }
                                        else
                                        {
                                            // DOES THIS NEED TO INCLUDE ANALYSISCOUNTER++ ALSO????????????????????
                                            break;
                                        }
                                    }
                                }
                                if (selectedCourseRatingValue == 3)
                                {
                                    if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) == 4)))
                                    {
                                        cell.Style.BackColor = Color.Red;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                        break;
                                    }
                                    else if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) != 4)))
                                    {
                                        if (historicalDistance > racecardDistance)
                                        {
                                            cell.Style.BackColor = Color.Red;
                                            analysisCounter++; // Increment global score
                                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (selectedCourseRatingValue == 4 && filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                    filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName)))
                                {
                                    if (historicalDistance > racecardDistance)
                                    {
                                        cell.Style.BackColor = Color.Red;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                            }
                            else if (racecardDistance < historicalDistance)
                            {
                                cell.Style.BackColor = Color.FromArgb(144, 238, 144); // light Green
                                analysisCounter++; // Increment global score
                                horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;

                                if (selectedCourseRatingValue == 1 && filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                    filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName)))
                                {
                                    cell.Style.BackColor = Color.Green;

                                    analysisCounter++; // Increment analysisCounter
                                    horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2; //Increment global score
                                    break;
                                }
                                if (selectedCourseRatingValue == 2)
                                {
                                    if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) != 1)))
                                    {
                                        cell.Style.BackColor = Color.Green;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                        break;
                                    }
                                    else if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) == 1)))
                                    {
                                        if (historicalDistance > racecardDistance)
                                        {
                                            cell.Style.BackColor = Color.Green;
                                            analysisCounter++; // Increment global score
                                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                            break;
                                        }
                                        else
                                        { 
                                            break;
                                        }
                                    }
                                }
                                if (selectedCourseRatingValue == 3)
                                {
                                    if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) == 4)))
                                    {
                                        cell.Style.BackColor = Color.Green;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                        break;
                                    }
                                    else if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                        filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName && int.Parse(course.CourseValue) != 4)))
                                    {
                                        if (historicalDistance > racecardDistance)
                                        {
                                            cell.Style.BackColor = Color.Green;
                                            analysisCounter++; // Increment global score
                                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (selectedCourseRatingValue == 4 && filteredAnalysisResults.Any(entry => entry.HorseName == horseName) &&
                                    filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName)))
                                {
                                    if (historicalDistance > racecardDistance)
                                    {
                                        cell.Style.BackColor = Color.Green;
                                        analysisCounter++; // Increment global score
                                        horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 2 : 2;
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                            }
                            else if (racecardDistance == historicalDistance)
                            {
                                cell.Style.BackColor = Color.FromArgb(255, 200, 100); // light Orange
                                analysisCounter++; // Increment global score

                                if (filteredAnalysisResults.Any(entry => entry.HorseName == horseName) && filteredAnalysisResults.Any(entry => otherCoursesWithDifferentRating.Any(course => course.CourseName == entry.CourseName)))
                                {
                                    analysisCounter++; // Increment global score
                                    horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] + 1 : 1;
                                    cell.Style.BackColor = Color.DarkOrange;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Reset the color if the horse is unexposed
                            cell.Style.ForeColor = Color.Red;
                            analysisCounter++; // Increment global score
                            horseScores[horseName] = horseScores.ContainsKey(horseName) ? horseScores[horseName] - 2 : -2;
                            break;
                        }
                    }
                    else
                    {
                        // No horseName match
                    }
                } // foreach datagridview1 row
            }
            //CourseAnalysis(dataAnalyzer);
            GoingAnalysis(dataAnalyzer);
        }

        private void GoingAnalysis(DataAnalyser dataAnalyzer)
        {
            //throw new NotImplementedException(); @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }

        private int ConvertConstitutionToRating(string constitution)
        {
            switch (constitution)
            {
                case "Flat":
                    return 1;
                case "Undulating":
                    return 2;
                case "V Undulating":
                    return 3;
                case "Uphill":
                    return 4;
                default:
                    // Handle any other cases or null values here (e.g., asterisk for Irish racing)
                    return 0;
            }
        }

        private List<string[]> LoadRecordsFromCSV()
        {
            List<string[]> records = new List<string[]>();

            // Define a dictionary to store course names and their corresponding regions
            Dictionary<string, string> courseRegions = new Dictionary<string, string>();

            // Populate the dictionary with GB courses and their regions
            courseRegions.Add("Aintree", "GB");
            courseRegions.Add("Ascot", "GB");
            courseRegions.Add("Ayr", "GB");
            courseRegions.Add("Bangor", "GB");
            courseRegions.Add("Bath", "GB");
            courseRegions.Add("Beverley", "GB");
            courseRegions.Add("Brighton", "GB");
            courseRegions.Add("Carlisle", "GB");
            courseRegions.Add("Cartmel", "GB");
            courseRegions.Add("Catterick", "GB");
            courseRegions.Add("Chelmsford (AW)", "GB");
            courseRegions.Add("Cheltenham (AW)", "GB");
            courseRegions.Add("Chepstow", "GB");
            courseRegions.Add("Chester", "GB");
            courseRegions.Add("Doncaster", "GB");
            courseRegions.Add("Epsom", "GB");
            courseRegions.Add("Exeter", "GB");
            courseRegions.Add("Fakenham", "GB");
            courseRegions.Add("Ffos Las", "GB");
            courseRegions.Add("Fontwell", "GB");
            courseRegions.Add("Goodwood", "GB");
            courseRegions.Add("Hamilton", "GB");
            courseRegions.Add("Haydock", "GB");
            courseRegions.Add("Hereford", "GB");
            courseRegions.Add("Hexham", "GB");
            courseRegions.Add("Huntingdon", "GB");
            courseRegions.Add("Kelso", "GB");
            courseRegions.Add("Kempton", "GB");
            courseRegions.Add("Kempton (AW)", "GB");
            courseRegions.Add("Leicester", "GB");
            courseRegions.Add("Lingfield", "GB");
            courseRegions.Add("Lingfield (AW)", "GB");
            courseRegions.Add("Ludlow", "GB");
            courseRegions.Add("Market Rasen", "GB");
            courseRegions.Add("Musselburgh", "GB");
            courseRegions.Add("Newbury", "GB");
            courseRegions.Add("Newcastle", "GB");
            courseRegions.Add("Newcastle (AW)", "GB");
            courseRegions.Add("Newmarket", "GB");
            courseRegions.Add("Newton Abbot", "GB");
            courseRegions.Add("Nottingham", "GB");
            courseRegions.Add("Perth", "GB");
            courseRegions.Add("Plumpton", "GB");
            courseRegions.Add("Pontefract", "GB");
            courseRegions.Add("Redcar", "GB");
            courseRegions.Add("Ripon", "GB");
            courseRegions.Add("Salisbury", "GB");
            courseRegions.Add("Sandown", "GB");
            courseRegions.Add("Sedgefield", "GB");
            courseRegions.Add("Southwell", "GB");
            courseRegions.Add("Southwell (AW)", "GB");
            courseRegions.Add("Stratford", "GB");
            courseRegions.Add("Taunton", "GB");
            courseRegions.Add("Thirsk", "GB");
            courseRegions.Add("Uttoxeter", "GB");
            courseRegions.Add("Warwick", "GB");
            courseRegions.Add("Wetherby", "GB");
            courseRegions.Add("Wincanton", "GB");
            courseRegions.Add("Windsor", "GB");
            courseRegions.Add("Wolverhampton (AW)", "GB");
            courseRegions.Add("Worcester", "GB");
            courseRegions.Add("Yarmouth", "GB");
            courseRegions.Add("York", "GB");
            // Add more GB course names and regions

            // Populate the dictionary with IRE courses and their regions
            courseRegions.Add("Ballinrobe", "IRE");
            courseRegions.Add("Bellewstown", "IRE");
            courseRegions.Add("Clonmel", "IRE");
            courseRegions.Add("CorkE", "IRE");
            courseRegions.Add("Curragh", "IRE");
            courseRegions.Add("Down Royal", "IRE");
            courseRegions.Add("Downpatrick", "IRE");
            courseRegions.Add("Dundalk", "IRE");
            courseRegions.Add("Fairyhouse", "IRE");
            courseRegions.Add("Galway", "IRE");
            courseRegions.Add("Gowran", "IRE");
            courseRegions.Add("Kilbeggan", "IRE");
            courseRegions.Add("Killarney", "IRE");
            courseRegions.Add("Laytown", "IRE");
            courseRegions.Add("Leopardstown", "IRE");
            courseRegions.Add("Limerick", "IRE");
            courseRegions.Add("Listowel", "IRE");
            courseRegions.Add("Naas", "IRE");
            courseRegions.Add("Navan", "IRE");
            courseRegions.Add("Punchestown", "IRE");
            courseRegions.Add("Roscommon", "IRE");
            courseRegions.Add("Sligo", "IRE");
            courseRegions.Add("Thurles", "IRE");
            courseRegions.Add("Tipperary", "IRE");
            courseRegions.Add("Towcester", "IRE");
            courseRegions.Add("Tramore", "IRE");
            courseRegions.Add("Wexford", "IRE");

            // Add more IRE course names and regions

            string selectedCourseName = raceCourse; // Set the selected racecard course name here

            string region = "Unknown"; // Default region

            // Check if the selectedCourseName exists in the dictionary, and retrieve the corresponding region
            if (courseRegions.TryGetValue(selectedCourseName, out string courseRegion))
            {
                region = courseRegion;
            }

            string csvDataFilePath;

            if (region == "GB" && raceType == "Flat")
            {
                csvDataFilePath = flatsGBdataPath;
            }
            else if (region == "GB" && (raceType == "Chase" || raceType == "Hurdle" || raceType == "NH Flat"))
            {
                csvDataFilePath = jumpsGBdataPath;
            }
            //else if (region == "IRE" && raceType == "Flat") // region on racecard refers to horses region, not course region. Why no history records!!! #####
            //{
            //    //csvDataFilePath = flatsGBdataPath;
            //}
            //else if (region == "IRE" && (raceType == "Chase" || raceType == "Hurdle" || raceType == "NH Flat"))
            //{
            //    //csvDataFilePath = jumpsGBdataPath;
            //}
            else
            {
                // Handle other race regions and types accordingly
                return records; // Return empty records if not applicable
            }

            using (var reader = new StreamReader(csvDataFilePath, Encoding.UTF8))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",", // Set the delimiter to comma (',')
                    TrimOptions = TrimOptions.Trim // Trim the cell values
                };

                using (var csv = new CsvReader(reader, configuration))
                {
                    while (csv.Read())
                    {
                        //string rawRowData = csv.Context.Parser.RawRecord;
                        //Console.WriteLine(rawRowData);

                        int fieldCount = csv.Context.Parser.Record.Length;
                        string[] record = new string[fieldCount];
                        for (int i = 0; i < fieldCount; i++)
                        {
                            record[i] = csv.GetField(i);
                        }
                        records.Add(record);
                    }
                }
            }

            // Remove the region letters from the horse names
            for (int i = 0; i < records.Count; i++)
            {
                int regionPos = records[i][23].IndexOf("("); // Assuming horse names are in column [23], edit as necessary!
                if (regionPos > 0)
                {
                    records[i][23] = records[i][23].Substring(0, regionPos).Trim();
                }
            }

            return records;
        }


        private void LoadCourseInfoFromCsv()
        {
            using (var reader = new StreamReader(targetCourseInfoFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Read the CSV file and populate the courseInfoDictionary
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    string courseName = csv.GetField("Racecourse");
                    string category = csv.GetField("Category");
                    string constitution = csv.GetField("Constitution");
                    string direction = csv.GetField("Direction");
                    string speed = csv.GetField("Speed");
                    string information = csv.GetField("Information");

                    if (!courseInfoDictionary.ContainsKey(courseName))
                    {
                        string courseInfo = $"{category}, {direction}, {constitution}, {speed}, {information}";
                        courseInfoDictionary.Add(courseName, courseInfo);
                    }
                }
            }
        }

        private void ShowTooltip(CourseLblToolTip tooltipForm)
        {
            string courseName = CourseLbl.Text;

            if (courseInfoDictionary.TryGetValue(courseName, out string courseInfo))
            {
                string[] infoParts = courseInfo.Split(',');

                if (infoParts.Length >= 4)
                {
                    string category = infoParts[0].Trim();
                    string direction = infoParts[1].Trim();
                    string speed = infoParts[3].Trim();
                    string information = string.Join(", ", infoParts.Skip(4).Select(part => part.Trim()));

                    tooltipForm.SetTooltipText(category, direction, speed, information);
                    tooltipForm.SetTooltipPosition(MousePosition.X + 10, MousePosition.Y + 10);
                    tooltipForm.Show();
                }
            }
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Handle tooltip for the Race Course column
                if (cell.OwningColumn.Name == "course")
                {
                    string courseName = cell.Value.ToString();

                    if (courseCellTooltipForm == null)
                    {
                        courseCellTooltipForm = new courseCellTooltip();
                    }

                    if (courseInfoDictionary.TryGetValue(courseName, out string courseInfo))
                    {
                        string[] infoParts = courseInfo.Split(',');

                        if (infoParts.Length >= 4)
                        {
                            string category = infoParts[0].Trim();
                            string direction = infoParts[1].Trim();
                            string speed = infoParts[3].Trim();

                            // Show tooltip form
                            courseCellTooltipForm.SetTooltipText(category, direction, speed);
                            courseCellTooltipForm.SetTooltipPosition(MousePosition.X + 10, MousePosition.Y + 10);
                            courseCellTooltipForm.Show();
                        }
                    }
                }

                // Handle tooltip for the Time column
                if (cell.OwningColumn.Name == "time")
                {
                    int rowIndex = e.RowIndex;

                    // Access other cells in the same row to obtain necessary values
                    DataGridViewCell hiddenDistanceCell = dataGridView2.Rows[rowIndex].Cells["hiddenDistance"];
                    DataGridViewCell beatenByCell = dataGridView2.Rows[rowIndex].Cells["btn"];
                    DataGridViewCell hiddenTimeCell = dataGridView2.Rows[rowIndex].Cells["hiddenTime"];

                    string timeValue = hiddenTimeCell.Value.ToString();
                    string distanceValue = hiddenDistanceCell.Value.ToString();
                    string beatenByValue = beatenByCell.Value.ToString();

                    if (timeValue == "-")
                    {
                        return; // Skip further processing and exit the event handler
                    }

                    // Call your method and pass the necessary values for calculation
                    string adjustedTime = CalculateAdjustedTime(timeValue, distanceValue, beatenByValue);

                    // Your logic to calculate adjusted times and avg speeds
                    string avgSpeed = CalculateAvgSpeed(adjustedTime, distanceValue);

                    // Show tooltip form
                    timeCellTooltipForm.SetTooltipText(adjustedTime, avgSpeed);
                    timeCellTooltipForm.SetTooltipPosition(MousePosition.X + 10, MousePosition.Y + 10);
                    timeCellTooltipForm.Show();
                }
            }
        }
        private string CalculateAdjustedTime(string time, string dist, string btn)
        {
            if (decimal.TryParse(time, out decimal parsedTime) &&
                decimal.TryParse(dist, out decimal parsedDist) &&
                decimal.TryParse(btn, out decimal parsedBtn))
            {
                if (parsedBtn == 0)
                {
                    // Horse was the winner, convert the original time to "mm:ss:ff" format
                    TimeSpan winnerTimeSpan = TimeSpan.FromSeconds((double)parsedTime);
                    string formattedTime = $"{winnerTimeSpan:mm\\:ss\\:fff}";
                    return formattedTime;
                }
                else
                {
                    // Calculate adjusted time
                    decimal timePerYard = parsedTime / parsedDist;
                    decimal timePerLength = timePerYard / 3;
                    decimal additionalTime = timePerLength * parsedBtn;
                    decimal adjustedTimeInSeconds = parsedTime + additionalTime;

                    // Convert adjusted time to mm:ss:ms format
                    TimeSpan adjustedTimeSpan = TimeSpan.FromSeconds((double)adjustedTimeInSeconds);
                    string adjustedTime = $"{adjustedTimeSpan.Minutes:D2}:{adjustedTimeSpan.Seconds:D2}:{adjustedTimeSpan.Milliseconds:D3}";

                    return adjustedTime.ToString();
                }
            }
            else
            {
                // Handle the case where parsing fails
                // Return a default or error value, display an error message, etc.
                return "N/A";
            }
        }

        private string CalculateAvgSpeed(string adTime, string distance)
        {
            if (TimeSpan.TryParseExact(adTime, "mm\\:ss\\:fff", CultureInfo.InvariantCulture, out TimeSpan adjustedTimeSpan) &&
                decimal.TryParse(distance, out decimal distanceValue))
            {
                decimal adjustedTimeInSeconds = (decimal)adjustedTimeSpan.TotalSeconds;

                if (adjustedTimeInSeconds > 0)
                {
                    // Convert distance from yards to miles
                    decimal distanceInMiles = distanceValue / 1760; // 1760 yards in 1 mile

                    // Calculate average speed in miles per hour (mph)
                    decimal avgSpeedMph = distanceInMiles / (adjustedTimeInSeconds / 3600); // Divide by 3600 seconds to get hours

                    // Format the average speed with two decimal places
                    return avgSpeedMph.ToString("0.00");
                }
            }

            // Handle the case where parsing fails or adjusted time is zero
            // Return a default or error value, display an error message, etc.
            return "N/A";
        }


        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            // Hide the tooltip form
            courseCellTooltipForm.Hide();
            timeCellTooltipForm.Hide();
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(this, this.Location);
            settings.ShowDialog();

            if (settings.SettingsFormMoved)
            {
                this.Location = settings.NewLocation; // Set the RacecardForm's location to the new location
                this.Show(); // Show the RacecardForm at the new location
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0) // Check if the clicked cell is in column index 0
                {
                    string selectedRaceDate = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string selectedRaceTime = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //MessageBox.Show("Date: " + selectedRaceDate + "\nOff Time: " + selectedRaceTime);

                    FilterByRaceMakeUp(selectedRaceDate, selectedRaceTime);
                }
                else
                {
                    
                }
            }
            dataGridView2.Enabled = true;
            Cursor = Cursors.Default;
        }

        private async void FilterByRaceMakeUp(string selectedRaceDate, string selectedRaceTime)
        {
            // Clear existing items from parent DataGridView
            dataGridView2.Rows.Clear();
            dataGridView2.Enabled = false;
            Cursor = Cursors.WaitCursor;

            // Load data from the history CSV file
            List<string[]> records = LoadRecordsFromCSV();

                // Filter the records based on the filter condition
                var filteredRecords = records.Where(record =>
                {
                    // Adjust the column indices according to your CSV structure
                    string raceDate = record[0];
                    string raceTime = record[3];

                    // Check if both date and time match the selected values
                    return raceDate == selectedRaceDate && raceTime == selectedRaceTime;
                })
                .ToList();

            foreach (var raceRecord in filteredRecords ) 
            {
                await ProcessRecordAsync(raceRecord);
            }
        }
    }

}
