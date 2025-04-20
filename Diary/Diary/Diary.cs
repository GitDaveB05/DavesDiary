using System.Xml;
using System.IO;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Globalization;

namespace Diary
{
    public partial class Diary : Form
    {
        private int currentYear;
        private int currentMonth;
        private int currentDay;

        DateTime nowDate;

        private string homeTextString;
        private string aboutmeTextString;

        Color pastCol = ColorTranslator.FromHtml("#DDDDDD");

        //private int currentDay;
        TextBox currentTextBox = null;
        int currentSelectedDay;
        string currentSelectedWeekEntry, currentSelectedMonthEntry, currentSelectedYearEntry;

        private Dictionary<int, string> dayEntries = new Dictionary<int, string>();


        public Diary()
        {
            InitializeComponent();
            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            currentDay = DateTime.Now.Day;
            nowDate = new DateTime(currentYear, currentMonth, currentDay);
            todayLabel.Text = $"Today: {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}";
            string filePath = $"Entries/Home.json"; // File path
            string aboutmePath = $"Entries/Aboutme.json";

            homeText.Visible = true;
            if (File.Exists(filePath))
            {
                homeTextString = File.ReadAllText(filePath);
                homeText.Text = homeTextString;

                aboutmeTextString = File.ReadAllText(aboutmePath);
            }
            UpdateCalendar();

            //LoadTextFromJsonFile();
        }

        private void UpdateCalendar()
        {
            labelYear.Text = currentYear.ToString();
            labelMonth.Text = new DateTime(currentYear, currentMonth, 1).ToString("MMMM");

            // Clear existing day
            daysContainer.Controls.Clear();
            textAreaContainer.Controls.Clear();
            EntryContainer.Controls.Clear();

            // Get number of days in the current month
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            //Reset current selected
            currentSelectedDay = 0;
            currentSelectedWeekEntry = "";
            currentTextBox = null;

            //Some vars
            string monthName = new DateTime(currentYear, currentMonth, 1).ToString("MMMM");

            // Generate buttons for each day of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime thisDate = new DateTime(currentYear, currentMonth, day);

                Calendar calendar = CultureInfo.CurrentCulture.Calendar;
                CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek;
                DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
                int weekOfYear = calendar.GetWeekOfYear(thisDate, weekRule, firstDayOfWeek);

                string dayName = thisDate.DayOfWeek.ToString().Substring(0, 3);
                Button dayButton = new Button
                {
                    Text = $"{day}\n{dayName}\nKW {weekOfYear}",
                    Width = 60,
                    Height = 60,
                    Margin = new Padding(2),
                    Name = $"d{day}",
                    Tag = day // Set the Tag to the day value
                };
                ButtonUpdateColor(dayButton);

                // Create a TextBox for the day (hidden initially)
                TextBox dayTextBox = new TextBox
                {
                    Width = 900,
                    Height = 500,
                    Multiline = true,
                    Visible = false, // Initially hide the TextBox
                    Name = $"d{day}",
                    Tag = day, // Set the Tag to the day value, so it's associated with the day
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font(TextBox.DefaultFont.FontFamily, 14)
                };

                // Event handler to add custom action when a day button is clicked
                dayButton.Click += (sender, e) =>
                {
                    homeText.Visible = false;

                    Button clickedButton = sender as Button;
                    if (clickedButton != null)
                    {
                        string clickedDayName = clickedButton.Name;
                        int clickedDay = (int)clickedButton.Tag; // Retrieve the day from the Tag
                        currentSelectedDay = clickedDay;

                        homeTextLabel.Text = new DateTime(currentYear, currentMonth, clickedDay).DayOfWeek.ToString();
                        foreach (Button button in daysContainer.Controls)
                        {
                            ButtonUpdateColor(button);
                        }

                        // Find the TextBox associated with this day button
                        TextBox associatedTextBox = dayTextBox;
                        if (currentTextBox != null)
                        {
                            if (string.Equals(currentTextBox.Name, clickedDayName, StringComparison.CurrentCulture))
                                return; // Ignore if the same day is clicked again
                        }
                        if (currentTextBox != null)
                        {
                            currentTextBox.Visible = false;
                        }
                        // Toggle visibility of the TextBox for the selected day
                        associatedTextBox.Visible = true; //!associatedTextBox.Visible;
                        currentTextBox = associatedTextBox;

                        currentDayLabel.Text = $"Selected Date: {clickedDay}.{currentMonth}.{currentYear} KW {weekOfYear}";
                    }
                };


                // Event handler to save text when the user presses Enter or loses focus (optional)
                dayTextBox.Leave += (sender, e) =>
                {
                    // Save or process the text entered into the TextBox
                    string enteredText = dayTextBox.Text;
                };

                // Add the button and text box to the layout
                daysContainer.Controls.Add(dayButton);
                textAreaContainer.Controls.Add(dayTextBox);
            }

            //Go through every week of the month and spawn a button to write down Entrys for the week
            for (int week = 0; week <= (int)((daysInMonth / 7)); week++)
            {
                int day = 1 + week * 7;
                day = Math.Clamp(day, 1, daysInMonth); //This solution is shit but it should never fail
                DateTime thisDate = new DateTime(currentYear, currentMonth, day);

                Calendar calendar = CultureInfo.CurrentCulture.Calendar;
                CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek;
                DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
                int weekOfYear = calendar.GetWeekOfYear(thisDate, weekRule, firstDayOfWeek);

                Button weekEntryButton = new Button
                {
                    Text = $"KW {weekOfYear}",
                    Width = 60,
                    Height = 60,
                    Margin = new Padding(2),
                    Name = $"kw{weekOfYear}.{currentYear}",
                    Tag = weekOfYear,
                };

                TextBox weekEntryTextBox = new TextBox
                {
                    Width = 900,
                    Height = 500,
                    Multiline = true,
                    Visible = false, // Initially hide the TextBox
                    Name = $"kw{weekOfYear}.{currentYear}",
                    Tag = weekOfYear,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font(TextBox.DefaultFont.FontFamily, 14)
                };

                // Event handler to add custom action when a day button is clicked
                weekEntryButton.Click += (sender, e) =>
                {
                    homeText.Visible = false;

                    Button clickedButton = sender as Button;
                    if (clickedButton != null)
                    {
                        string clickedDay = clickedButton.Name; // Retrieve the calender week from the Tag
                        currentSelectedWeekEntry = clickedDay;

                        homeTextLabel.Text = $"Entry for KW {weekOfYear}, {currentYear}";

                        // Find the TextBox associated with this week Entry button
                        TextBox associatedTextBox = weekEntryTextBox;
                        if (currentTextBox != null)
                        {
                            if (string.Equals(currentTextBox.Name, clickedDay, StringComparison.CurrentCulture))
                                return; // Ignore if the same week Entry is clicked again
                        }
                        if (currentTextBox != null)
                        {
                            currentTextBox.Visible = false;
                        }
                        // Toggle visibility of the TextBox for the selected day
                        associatedTextBox.Visible = true; //!associatedTextBox.Visible;
                        currentTextBox = associatedTextBox;

                        currentDayLabel.Text = $"Selected Date: KW {weekOfYear}";
                    }
                };

                EntryContainer.Controls.Add(weekEntryButton);
                textAreaContainer.Controls.Add(weekEntryTextBox);
            }

            //Month and year Entry buttons
            Button monthEntryButton = new Button
            {
                Text = $"Month Entry: {monthName}",
                Width = 180,
                Height = 60,
                Margin = new Padding(2),
                Name = $"m{currentMonth}.{currentYear}",
                Tag = currentMonth,
            };

            TextBox monthEntryTextBox = new TextBox
            {
                Width = 900,
                Height = 500,
                Multiline = true,
                Visible = false, // Initially hide the TextBox
                Name = $"m{currentMonth}.{currentYear}",
                Tag = currentMonth * currentYear,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font(TextBox.DefaultFont.FontFamily, 14)
            };

            // Event handler to add custom action when a day button is clicked
            monthEntryButton.Click += (sender, e) =>
            {
                homeText.Visible = false;

                Button clickedButton = sender as Button;
                if (clickedButton != null)
                {
                    string clickedDay = clickedButton.Name; // Retrieve the calender week from the Tag
                    currentSelectedMonthEntry = clickedDay;

                    homeTextLabel.Text = $"Entry for {monthName}";

                    // Find the TextBox associated with this week Entry button
                    TextBox associatedTextBox = monthEntryTextBox;
                    if (currentTextBox != null)
                    {
                        if (string.Equals(currentTextBox.Name, clickedDay, StringComparison.CurrentCulture))
                            return; // Ignore if the same week Entry is clicked again
                    }
                    if (currentTextBox != null)
                    {
                        currentTextBox.Visible = false;
                    }
                    // Toggle visibility of the TextBox for the selected day
                    associatedTextBox.Visible = true; //!associatedTextBox.Visible;
                    currentTextBox = associatedTextBox;

                    currentDayLabel.Text = $"Selected Month: {monthName}";
                }
            };

            Button yearEntryButton = new Button
            {
                Text = $"Year Entry: {currentYear}",
                Width = 180,
                Height = 60,
                Margin = new Padding(2),
                Name = $"y{currentYear}",
                Tag = currentYear,
            };

            TextBox yearEntryTextBox = new TextBox
            {
                Width = 900,
                Height = 500,
                Multiline = true,
                Visible = false,
                Name = $"y{currentYear}",
                Tag = currentYear,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font(TextBox.DefaultFont.FontFamily, 14)
            };

            // Event handler for the year Entry button
            yearEntryButton.Click += (sender, e) =>
            {
                homeText.Visible = false;

                Button clickedButton = sender as Button;
                if (clickedButton != null)
                {
                    string clickedYear = clickedButton.Name;
                    currentSelectedYearEntry = clickedYear;

                    homeTextLabel.Text = $"Entry for Year {currentYear}";

                    // Find the TextBox associated with this year Entry button
                    TextBox associatedTextBox = yearEntryTextBox;
                    if (currentTextBox != null)
                    {
                        if (string.Equals(currentTextBox.Name, clickedYear, StringComparison.CurrentCulture))
                            return; // Ignore if the same year Entry is clicked again
                    }
                    if (currentTextBox != null)
                    {
                        currentTextBox.Visible = false;
                    }
                    // Toggle visibility of the TextBox for the selected year
                    associatedTextBox.Visible = true;
                    currentTextBox = associatedTextBox;

                    currentDayLabel.Text = $"Selected Year: {currentYear}";
                }
            };

            EntryContainer.Controls.Add(monthEntryButton);
            textAreaContainer.Controls.Add(monthEntryTextBox);

            EntryContainer.Controls.Add(yearEntryButton);
            textAreaContainer.Controls.Add(yearEntryTextBox);

            LoadTextFromJsonFile();
        }

        private void ButtonUpdateColor(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            int buttonDay = (int)button.Tag;
            DateTime buttonDate = new DateTime(currentYear, currentMonth, buttonDay);

            if (buttonDate < nowDate) // Past Day
            {
                button.BackColor = Color.White;
                button.FlatAppearance.BorderColor = pastCol;
            }
            else if (buttonDate.Year == nowDate.Year && buttonDate.Month == nowDate.Month && buttonDate.Day == nowDate.Day) // Today
            {
                button.BackColor = Color.LightYellow;
                button.FlatAppearance.BorderColor = Color.Yellow;
            }
            else if (buttonDate > nowDate) // Future Day
            {
                button.BackColor = Color.LightGray;
                button.FlatAppearance.BorderColor = Color.Gray;
            }

            if (buttonDay == currentSelectedDay && currentMonth == buttonDate.Month && currentYear == buttonDate.Year) // Selected Day
            {
                button.BackColor = Color.AliceBlue;
                button.FlatAppearance.BorderColor = Color.CadetBlue;
            }

            button.Invalidate();
        }

        private void btnPrevYear_Click(object sender, EventArgs e)
        {
            currentYear--;
            UpdateCalendar();
        }

        private void btnNextYear_Click(object sender, EventArgs e)
        {
            currentYear++;
            UpdateCalendar();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            currentMonth = (currentMonth == 12) ? 1 : currentMonth + 1;
            if (currentMonth == 1) currentYear++; // Move to January of the next year if necessary
            UpdateCalendar();
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            currentMonth = (currentMonth == 1) ? 12 : currentMonth - 1;
            if (currentMonth == 12) currentYear--; // Move to December of the previous year if necessary
            UpdateCalendar();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Entries"))
            {
                Directory.CreateDirectory("Entries");
            }

            dayEntries.Clear();
            bool changesDetected = false;

            foreach (TextBox ctrl in textAreaContainer.Controls)
            {
                string ctrlName = ctrl.Name;

                // For day entries
                if (!ctrlName.Contains("kw") && !ctrlName.Contains("m"))
                {
                    int day = (int)ctrl.Tag;
                    if (!string.IsNullOrEmpty(ctrl.Text) && (dayEntries.ContainsKey(day) == false || dayEntries[day] != ctrl.Text))
                    {
                        dayEntries[day] = ctrl.Text;
                        changesDetected = true;
                    }
                }
                // For week and month Entrys
                else
                {
                    foreach (Button btn in EntryContainer.Controls)
                    {
                        string btnName = btn.Name;
                        if (string.Equals(ctrlName, btnName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            string filePath = $"Entries/EntryEntry{ctrlName}.json";

                            // Check if file exists and content differs
                            bool contentChanged = true;
                            if (File.Exists(filePath))
                            {
                                string existingJson = File.ReadAllText(filePath);
                                string existingText = string.Empty;
                                if (!string.IsNullOrEmpty(existingJson))
                                {
                                    existingText = JsonConvert.DeserializeObject<string>(existingJson);
                                }
                                // Only mark as changed if content differs
                                contentChanged = !string.Equals(existingText, ctrl.Text);
                            }

                            if (contentChanged)
                            {
                                string json = JsonConvert.SerializeObject(ctrl.Text, Newtonsoft.Json.Formatting.Indented);
                                File.WriteAllText(filePath, json); // Save the JSON data to the file
                                changesDetected = true;
                            }
                        }
                    }
                }
            }

            // Save day entries if there are any
            if (dayEntries.Count > 0)
            {
                SaveTextToJsonFile();
                changesDetected = true;
            }

            // Show appropriate message based on whether changes were detected
            if (changesDetected)
            {
                MessageBox.Show("Data saved to JSON file successfully.");
            }
            else
            {
                MessageBox.Show("No entries made.");
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            LoadTextFromJsonFile();
        }


        private void SaveTextToJsonFile()
        {
            //Save Day Entries
            try
            {
                string json = JsonConvert.SerializeObject(dayEntries, Newtonsoft.Json.Formatting.Indented);
                string filePath = $"Entries/dayEntries{currentMonth}.{currentYear}.json"; // Specify the path where you want to save the file
                File.WriteAllText(filePath, json); // Save the JSON data to the file
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}");
            }

            //Save Week Entry Entries
        }

        private void LoadTextFromJsonFile()
        {
            try
            {
                //Load days
                string filePath = $"Entries/dayEntries{currentMonth}.{currentYear}.json"; // File path
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    dayEntries = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);

                    // Update the TextBox controls with the loaded text
                    foreach (TextBox text in textAreaContainer.Controls)
                    {
                        string textName = text.Name;
                        if (textName.Contains("kw") || textName.Contains("m"))
                            continue;
                        int day = (int)text.Tag;
                        if (dayEntries.ContainsKey(day))
                        {
                            text.Text = dayEntries[day];
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("No saved data found.");
                }

                //Load week Entrys
                foreach (TextBox ctrl in textAreaContainer.Controls)
                {
                    string weekEntryPath = $"Entries/EntryEntry{ctrl.Name}.json"; // File path
                    if (File.Exists(weekEntryPath))
                    {
                        string json = File.ReadAllText(weekEntryPath);
                        if (json != string.Empty)
                        {
                            string finalText = JsonConvert.DeserializeObject<string>(json);
                            ctrl.Text = finalText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the file: {ex.Message}");
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            homeText.Visible = true;
            homeText.Text = homeTextString;
            if (currentTextBox != null)
            {
                currentTextBox.Visible = false;
            }
        }

        private void Diary_Load(object sender, EventArgs e)
        {

        }

        private void aboutmeButton_Click(object sender, EventArgs e)
        {
            homeText.Visible = true;
            homeText.Text = aboutmeTextString;
            if (currentTextBox != null)
            {
                currentTextBox.Visible = false;
            }
        }

        private void currentDayLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
