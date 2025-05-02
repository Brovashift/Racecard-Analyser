using CsvHelper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RacecardAnalyser.Classes
{
    internal class DataAnalyser
    {
        //CourseType
        //RaceType
        //FieldSize (potential pace)

        private IEnumerable<string[]> filteredRacecardRecords;
        private IEnumerable<string[]> filteredHistoricalRecords;

        public void AnalyseRacecardData(IEnumerable<string[]> racecardRecords)
        {
            filteredRacecardRecords = racecardRecords;
        }
        public void AnalyseHistoricalData(IEnumerable<string[]> historicalRecords)
        {
            filteredHistoricalRecords = historicalRecords;
        }

        public class CourseProfileClass
        {
            public string CourseName { get; set; }
            public string CourseDirection { get; set; }
            public string Constitution { get; set; }
            public string Speed { get; set; }
            public string Category { get; set; }
            public string CourseValue { get; set; }
            public List<string> HorseNames { get; set; } // List to hold multiple horse names
            // Add any other relevant properties for course profile data
        }

        public class TripAnalysisResultClass
        {
            // Filtered Racecard Data
            public double RacecardDistance { get; set; }
            public string selectedCourseName { get; set; }
            public bool IsUnexposed { get; set; }
            public DateTime Date { get; set; }

            // Historical Racecard Data
            public string CourseName { get; set; }
            public string HorseName { get; set; }
            public double HistoricalDistance { get; set; }
            public string FinishPosition { get; set; }
            public double BeatenBy { get; set; }
        }

        public class CustomRatingsClass
        {
            public string HorseName { get; set; }
            public string RacecardRPR { get; set; }
            public string RacecardWGT { get; set; }

            // Create integer properties to hold the converted values
            public double IntRacecardRPR { get; set; }
            public double IntRacecardWGT { get; set; }

            //public string RacecardGoing { get; set; }
            //public string RacecardDistance { get; set; }
            //public string RacecardCourse { get; set; }
            //public string RacecardHG { get; set; }
            //
            //public string HistoryRTS { get; set; }
            //public string HistoryWGT { get; set; }
            //public string HistoryGoing { get; set; }
            //public string HistoryDistance { get; set; }
            //public string HistoryCourse { get; set; }
            //public string HistoryHG { get; set; }
            //
            public double CalculatedCustomRating { get; set; }

            internal int HighestHistoryRTS;
        }

        public class CourseAnalysisClass
        {
            public string SelectedCourseName { get; set; }
            //public string historicalCourseName { get; set; }
            public bool IsUnexposed { get; set; }
            public bool CourseSpecialist { get; set; }
            public bool CourseExposure { get; set; }
            public string HorseName { get; set; }
            public string FavouredDirection { get; set; }
            public string SelectedCourseDirection { get; set; }
            public string CourseConstitution { get; set; }

        }

        public List<CourseAnalysisClass> CourseAnalyser(string filePath)
        {
            //Method to check for course specialist, unexposed, favoured direction, constituion
            List<CourseAnalysisClass> courseAnalysis = new List<CourseAnalysisClass>();
            DateTime Date = DateTime.Now.AddMonths(-12);   // 18 months ago

            foreach (var racecardRecords in filteredRacecardRecords)
            {
                string horseName = racecardRecords[15].Trim();
                string racecardCourseName = racecardRecords[2].Trim();

                var analysis = new CourseAnalysisClass
                {
                    HorseName = horseName
                };

                analysis.CourseSpecialist = false;
                analysis.IsUnexposed = false;
                analysis.CourseExposure = false;
                analysis.SelectedCourseName = racecardCourseName;

                //Looking for a course specialist *****************************
                //Find relevant historical records for the seleceted racecard course
                var historicalCourseRecords = filteredHistoricalRecords
                    .Where(record => record[23].Trim() == horseName)
                    .Where(record => record[2].Trim() == racecardCourseName)
                    .Where(record => int.TryParse(record[19].Trim(), out int position) && position <= 4)
                    .Where(record => double.Parse(record[21].Trim()) <= 6)
                    .Where(record => DateTime.Parse(record[0].Trim()) >= Date)
                    .ToList();

                int totalCourseRuns = filteredHistoricalRecords
                    .Count(record => record[23].Trim() == horseName && record[2].Trim() == racecardCourseName);

                if (historicalCourseRecords.Count >= 4)
                {
                    // Course Specialist
                    analysis.IsUnexposed = false;
                    analysis.CourseSpecialist = true;
                }
                else if (totalCourseRuns == 0)
                {
                    // Unexposed at course
                    analysis.IsUnexposed = true;
                }
                else if (totalCourseRuns > 0 && analysis.CourseSpecialist == false)
                {
                    analysis.CourseExposure = true;
                }

                // Create a dictionary to store course directions and constitutions
                Dictionary<string, string[]> courseInfoDictionary = new Dictionary<string, string[]>();
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader(); // Read the header row to map the fields by name
                    var courseProfilesLookupByHorse = new Dictionary<string, Dictionary<string, (string CourseDirection, string Constitution)>>();
                    int RHcount = 0;
                    int LHcount = 0;

                    // Looking for favoured direction
                    var wonOrPlacedAllCourses = filteredHistoricalRecords
                        .Where(record => record[23].Trim() == horseName)
                        .Where(record => DateTime.Parse(record[0].Trim()) >= Date)
                        .Where(record => int.TryParse(record[19].Trim(), out int finishPosition) && (finishPosition <= 4 || finishPosition == 0))
                        .ToList();

                    while (csv.Read())
                    {
                        var courseProfile = new CourseProfileClass
                        {
                            CourseName = csv.GetField<string>("Racecourse"),
                            CourseDirection = csv.GetField<string>("Direction"),
                            Constitution = csv.GetField<string>("Constitution")
                        };
                        // Populate the courseInfoDictionary with course direction and constitution
                        courseInfoDictionary[courseProfile.CourseName] = new string[] { courseProfile.CourseDirection, courseProfile.Constitution };
                    }  
  
                    var courseProfilesLookup = new Dictionary<string, (string CourseDirection, string Constitution)>();
                    // Populate the courseProfilesLookup dictionary with course direction and constitution
                    string racecardCourseDirection = "";
                    //string racecardCourseConstitution = "";

                    if (courseInfoDictionary.TryGetValue(racecardCourseName.Trim(), out var courseInfo))
                    {
                        racecardCourseDirection = courseInfo[0];
                        //racecardCourseConstitution = courseInfo[1];
                    }

                    foreach (var course in wonOrPlacedAllCourses)
                    {
                        if (!courseProfilesLookup.ContainsKey(course[2].Trim()) && courseInfoDictionary.TryGetValue(course[2].Trim(), out courseInfo))
                        {
                            string courseDirection = courseInfo[0];
                            string constitution = courseInfo[1];
                            if (courseDirection == "*")
                            {
                                continue;
                            }

                            courseProfilesLookup[course[2].Trim()] = (courseDirection, constitution);

                            if (courseDirection == "Right")
                            {
                                RHcount++;
                            }
                            else if (courseDirection == "Left")
                            {
                                LHcount++;
                            }
                            else
                            {
                                // No favoured Direction
                                racecardCourseDirection = "None";
                            }    
                        }              
                    }

                    string favouredDirection;
                    if (RHcount > LHcount)
                    {
                        favouredDirection = "Right";
                    }
                    else if (LHcount > RHcount)
                    {
                        favouredDirection = "Left";
                    }
                    else
                    {
                        favouredDirection = "None";
                    }

                    // Store the horse-specific courseProfilesLookup in the courseProfilesLookupByHorse dictionary
                    courseProfilesLookupByHorse[horseName] = courseProfilesLookup;

                    analysis.FavouredDirection = favouredDirection;
                    analysis.HorseName = horseName;
                    analysis.SelectedCourseDirection = racecardCourseDirection;
                    //analysis.CourseConstitution = racecardCourseConstitution;
                    
                    courseAnalysis.Add(analysis); // Add all analysis objects here. Creating new instances makes duplicate results
                }
            }
            return courseAnalysis;
        }

        public List<TripAnalysisResultClass> TripAnalyser()
        {
            List<TripAnalysisResultClass> analysisResults = new List<TripAnalysisResultClass>();

            // Check if filteredRacecardRecords and filteredHistoricalRecords are not null
            if (filteredRacecardRecords != null && filteredHistoricalRecords != null)
            {
                // Loop through each horse in the filtered racecard records
                foreach (var racecardRecord in filteredRacecardRecords)
                {
                    string horseName = racecardRecord[15].Trim(); // Assuming horse name is at index 15

                    // Find the most recent historical record for the current horse
                    var mostRecentHistoricalRecord = filteredHistoricalRecords
                        .Where(record => record[23].Trim() == horseName) // Assuming horse name is at index 15
                        .OrderByDescending(record => DateTime.Parse(record[0].Trim())) // Assuming date is at index 0
                        .FirstOrDefault();

                    // Check if a most recent historical record is found
                    if (mostRecentHistoricalRecord != null)
                    {
                        // Get the selected course name and race distance from the filtered racecard record
                        string selectedCourseName = racecardRecord[2].Trim(); // Assuming course name is at index 2
                        string selectedRaceDistanceString = racecardRecord[5].Trim(); // Assuming distance is at index 5
                        double selectedRaceDistance = FormatRacecardDistance(selectedRaceDistanceString);

                        // Compare the distances (trip) between racecard and historical data
                        string racecardDistanceString = racecardRecord[5].Trim(); // Assuming distance is at index 4
                        double racecardDistance = FormatRacecardDistance(racecardDistanceString); // Convert and preprocess racecard distance

                        string historicalDistanceString = mostRecentHistoricalRecord[12].Trim(); // Assuming distance is at index 11
                        double historicalDistance = double.Parse(historicalDistanceString.TrimEnd('f')); // Convert to double 

                        // Check if the horse is unexposed
                        bool isUnexposed = !filteredHistoricalRecords
                            .Where(record => record[23].Trim() == horseName) // Filter records for the specific horse
                            .Any(record => double.Parse(record[12].TrimEnd('f')) >= racecardDistance);

                        // Retrieve the finish position and beaten by values for the current horse
                        string finishPositionString = mostRecentHistoricalRecord[19].Trim();
                        //int finishPosition = 0;

                        string dateString = mostRecentHistoricalRecord[0].Trim();
                        DateTime date = DateTime.Parse(dateString);


                        string beatenByString = mostRecentHistoricalRecord[21].Trim();
                        double beatenBy = 0.0; // Initialize to a default value

                        if (!string.IsNullOrEmpty(beatenByString))
                        {
                            if (double.TryParse(beatenByString, out double numericBeatenBy))
                            {
                                // If the value is a valid numeric representation, use it as the double value
                                beatenBy = numericBeatenBy;
                            }
                            else
                            {
                                // Handle special cases where beatenByString contains non-numeric values like "-"
                                // For example, you can assign a specific value for such cases or handle them as needed.
                            }
                        }

                        // Store the result in the dictionary
                        TripAnalysisResultClass analysisResult = new TripAnalysisResultClass
                        {
                            RacecardDistance = racecardDistance,
                            HistoricalDistance = historicalDistance,
                            IsUnexposed = isUnexposed, // Reverse the value since you want to indicate unexposed if true
                            selectedCourseName = selectedCourseName,
                            BeatenBy = beatenBy,
                            FinishPosition = finishPositionString,
                            Date = date,
                            HorseName = horseName,
                            CourseName = mostRecentHistoricalRecord[2].Trim()
                        };
                        analysisResults.Add(analysisResult);
                    }
                }
            }
            else
            {
                MessageBox.Show("Filtered Records Empty");
                // Handle the case where filteredRacecardRecords or filteredHistoricalRecords is null
                // You can add appropriate error handling or log a message here
            }

            return analysisResults;
        }


        // Helper method to format racecard distance with 'f' if needed
        private double FormatRacecardDistance(string distanceString)
        {
            double distance;
            // Try parsing the distance string to a double
            if (double.TryParse(distanceString, out distance))
            {
                // Check if the distance has one decimal place, if not, append '.0' to it
                distanceString = distance.ToString(distance % 1 == 0 ? "0.#" : "0.0");

                // Try parsing the formatted distance string to a double
                if (double.TryParse(distanceString, out distance))
                {
                    return distance;
                }
            }


            // Return 0 if parsing fails
            return 0;
        }

        public Dictionary<string, CourseProfileClass> LoadCourseProfilesFromCSV(string filePath)
        {
            var courseProfiles = new Dictionary<string, CourseProfileClass>(); // Create a Dictionary to store the course profiles

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader(); // Read the header row to map the fields by name

                while (csv.Read())
                {
                    var courseProfile = new CourseProfileClass
                    {
                        CourseName = csv.GetField<string>("Racecourse"),
                        CourseDirection = csv.GetField<string>("Direction"),
                        Speed = csv.GetField<string>("Speed"),
                        Constitution = ParseCourseType(csv.GetField<string>("Constitution")), // Use helper method
                        CourseValue = csv.GetField<string>("Value"),
                        // Initialize the HorseNames list
                        HorseNames = new List<string>(),
                        // Add other properties as needed
                    };
                    // Check if the courseProfile's CourseName exists in the filteredHistoricalRecords
                    if (filteredHistoricalRecords.Any(record => record[2].Trim() == courseProfile.CourseName))
                    {
                        // Find all associated horseNames from the corresponding records in filteredHistoricalRecords
                        var associatedHorseNames = filteredHistoricalRecords
                            .Where(record => record[2].Trim() == courseProfile.CourseName)
                            .Select(record => record[23].Trim());

                        // Add the associated horseNames to the HorseNames list
                        courseProfile.HorseNames.AddRange(associatedHorseNames);

                        // Use the CourseName as the key to store the CourseProfile in the dictionary
                        courseProfiles.Add(courseProfile.CourseName, courseProfile);
                    }
                }
            }
            return courseProfiles;
        }
        // Helper method to handle asterisks and convert to null
        private string ParseCourseType(string value)
        {
            return value == "*" ? null : value;
        }

        public void GoingAnalyser()
        {
            //Container for storing going profiles #######################################################
        }

        public void ClassAnalyser()
        {

        }

        public void WGTAnalyser()
        {

        }
        public void LastRunAnalyser()
        {

        }
 
        public void DrawAnalyser()
        {
            // Form cycles
        }
        public void FormAnalyser()
        {

        }
       
        public List<CustomRatingsClass> AdjustedRatings()
        {
            // To calculate my adjusted ratings using RPR, TS, and WGT considerations
            List < CustomRatingsClass> customRatingsList = new List<CustomRatingsClass> ();

            DateTime Date = DateTime.Now.AddMonths(-24);   // 24 months ago

            // Convert filteredRacecardRecords to a list of CustomRatings
            foreach (var racecardRecord in filteredRacecardRecords)
            {
                string horseName = racecardRecord[15].Trim();

                // Find historical records for the current horse
                var historicalRecordsForHorse = filteredHistoricalRecords
                    .Where(record => record[23].Trim() == horseName)
                    .Where(record => !string.IsNullOrWhiteSpace(record[36]) && int.TryParse(record[36].Trim(), out _)) // Filter out non-integer values
                    .Where(record => DateTime.Parse(record[0].Trim()) >= Date)
                    .ToList();


                // Check if historical record is found
                if (historicalRecordsForHorse.Any())
                {
                    // Find the highest historical TS value among the historical records
                    int highestHistoryRTS = historicalRecordsForHorse
                        .Where(record => int.TryParse(record[36].Trim(), out _))
                        .Max(record => int.Parse(record[36].Trim()));

                    // Find the historical record with the highestHistoryRTS
                    var historicalRecordWithHighestRTS = historicalRecordsForHorse
                        .FirstOrDefault(record => record[36].Trim() == highestHistoryRTS.ToString());

                    if (historicalRecordWithHighestRTS != null)
                    {
                        // Get racecardWGT from the current racecardRecord
                        double racecardWGT = double.Parse(racecardRecord[30].Trim()); // Adjust the property name as needed
                        //double racecardRPR = double.Parse(racecardRecord[32].Trim());

                        string rprString = racecardRecord[32].Trim();
                        double racecardRPR;

                        if (rprString.Equals("nan", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        else if (double.TryParse(rprString, out racecardRPR))
                        {
                            racecardRPR = double.Parse(racecardRecord[32].Trim());
                        }
                        else
                        {
                            // The parsing failed, handle the error or set a default value
                        }


                        double historyWGT = double.Parse(historicalRecordWithHighestRTS[26].Trim());

                        double WGTDifference = Math.Abs(racecardWGT - historyWGT);

                        double newTS;

                        if (racecardWGT > historyWGT)
                        {
                            newTS = highestHistoryRTS - WGTDifference;
                        }
                        else if (racecardWGT < historyWGT)
                        {
                            newTS = highestHistoryRTS + WGTDifference;
                        }
                        else
                        {
                            newTS = 0;
                        }

                        double newCustomRating = racecardRPR + newTS;

                        // Create a single instance of CustomRatings for this horse
                        CustomRatingsClass customRatings = new CustomRatingsClass
                        {
                            HorseName = racecardRecord[15].Trim(),
                            RacecardRPR = racecardRecord[32].Trim(),
                            RacecardWGT = racecardRecord[30].Trim(),
                            // Set other properties accordingly...
                            CalculatedCustomRating = newCustomRating
                        };

                        customRatings.IntRacecardRPR = racecardRPR;
                        customRatings.IntRacecardWGT = racecardWGT;

                        customRatingsList.Add(customRatings);
                    }
                }   
            }
            return customRatingsList;
        }

        public void OfficalRatingAnalyser()
        {

        }
        public void Conditions()
        {
            // To referrence under what conditions a previous race was run, inc; field_size, course type, going, class, WGT
            // Compared to todays conditions; field_size, going etc
        }

        public void RaceTypeAnalyser()
        {
            //Container for storing race profiles
        }

        //*******************************************************************************************
        //************** Further Development   ******************************************************
        //*******************************************************************************************

        public void BreedAnalyser()
        {
            //BloodlineInfo[]
        }

        public void JockeyAnalyser()
        {
            //JockeyStyles[]
        }
        public void TrainerAnalyser()
        {
            //TrainerProfiles[]
        }
        public void PaceAnalyser()
        {
            //Early postions / pace maps
        }
    }
}
