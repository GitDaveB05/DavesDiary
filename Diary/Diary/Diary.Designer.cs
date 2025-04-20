namespace Diary
{
    partial class Diary
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnPrevYear = new Button();
            btnNextYear = new Button();
            btnNextMonth = new Button();
            btnPrevMonth = new Button();
            labelMonth = new Label();
            labelYear = new Label();
            daysContainer = new FlowLayoutPanel();
            textAreaContainer = new FlowLayoutPanel();
            homeText = new TextBox();
            saveButton = new Button();
            currentDayLabel = new Label();
            todayLabel = new Label();
            homeButton = new Button();
            homeTextLabel = new Label();
            EntryContainer = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F);
            label1.Location = new Point(1165, 36);
            label1.Name = "label1";
            label1.Size = new Size(239, 54);
            label1.TabIndex = 0;
            label1.Text = "Dave's Diary";
            // 
            // btnPrevYear
            // 
            btnPrevYear.Location = new Point(24, 53);
            btnPrevYear.Name = "btnPrevYear";
            btnPrevYear.Size = new Size(75, 23);
            btnPrevYear.TabIndex = 2;
            btnPrevYear.Text = "<--";
            btnPrevYear.UseVisualStyleBackColor = true;
            btnPrevYear.Click += btnPrevYear_Click;
            // 
            // btnNextYear
            // 
            btnNextYear.Location = new Point(195, 53);
            btnNextYear.Margin = new Padding(0);
            btnNextYear.Name = "btnNextYear";
            btnNextYear.Size = new Size(75, 23);
            btnNextYear.TabIndex = 3;
            btnNextYear.Text = "-->";
            btnNextYear.UseVisualStyleBackColor = true;
            btnNextYear.Click += btnNextYear_Click;
            // 
            // btnNextMonth
            // 
            btnNextMonth.BackColor = SystemColors.HighlightText;
            btnNextMonth.FlatAppearance.BorderColor = Color.SlateGray;
            btnNextMonth.ForeColor = SystemColors.ControlText;
            btnNextMonth.Location = new Point(195, 91);
            btnNextMonth.Name = "btnNextMonth";
            btnNextMonth.Size = new Size(75, 23);
            btnNextMonth.TabIndex = 5;
            btnNextMonth.Text = "-->";
            btnNextMonth.UseVisualStyleBackColor = false;
            btnNextMonth.Click += btnNextMonth_Click;
            // 
            // btnPrevMonth
            // 
            btnPrevMonth.Location = new Point(24, 91);
            btnPrevMonth.Name = "btnPrevMonth";
            btnPrevMonth.Size = new Size(75, 23);
            btnPrevMonth.TabIndex = 4;
            btnPrevMonth.Text = "<--";
            btnPrevMonth.UseVisualStyleBackColor = true;
            btnPrevMonth.Click += btnPrevMonth_Click;
            // 
            // labelMonth
            // 
            labelMonth.AutoSize = true;
            labelMonth.Location = new Point(123, 95);
            labelMonth.Name = "labelMonth";
            labelMonth.Size = new Size(52, 15);
            labelMonth.TabIndex = 6;
            labelMonth.Text = "MONTH";
            labelMonth.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelYear
            // 
            labelYear.AutoSize = true;
            labelYear.Location = new Point(134, 57);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(31, 15);
            labelYear.TabIndex = 7;
            labelYear.Text = "yyyy";
            labelYear.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // daysContainer
            // 
            daysContainer.AutoScroll = true;
            daysContainer.Location = new Point(24, 130);
            daysContainer.Name = "daysContainer";
            daysContainer.Size = new Size(479, 376);
            daysContainer.TabIndex = 8;
            // 
            // textAreaContainer
            // 
            textAreaContainer.Location = new Point(509, 130);
            textAreaContainer.Name = "textAreaContainer";
            textAreaContainer.Size = new Size(900, 587);
            textAreaContainer.TabIndex = 9;
            // 
            // homeText
            // 
            homeText.Font = new Font("Segoe UI", 14F);
            homeText.Location = new Point(509, 130);
            homeText.MaximumSize = new Size(950, 600);
            homeText.Multiline = true;
            homeText.Name = "homeText";
            homeText.ScrollBars = ScrollBars.Vertical;
            homeText.Size = new Size(895, 600);
            homeText.TabIndex = 17;
            homeText.Text = "Welcome to Dave's Diary\r\n\r\nAn open source Diary App which allows you to make entries for each day, week, month and year\r\n\r\nFeel free to use this app in any way you like";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(41, 520);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 75);
            saveButton.TabIndex = 10;
            saveButton.Text = "save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // currentDayLabel
            // 
            currentDayLabel.AutoSize = true;
            currentDayLabel.Font = new Font("Segoe UI", 18F);
            currentDayLabel.Location = new Point(509, 56);
            currentDayLabel.Name = "currentDayLabel";
            currentDayLabel.Size = new Size(142, 32);
            currentDayLabel.TabIndex = 12;
            currentDayLabel.Text = "Current Day";
            // 
            // todayLabel
            // 
            todayLabel.AutoSize = true;
            todayLabel.Font = new Font("Segoe UI", 18F);
            todayLabel.Location = new Point(509, 24);
            todayLabel.Name = "todayLabel";
            todayLabel.Size = new Size(131, 32);
            todayLabel.TabIndex = 13;
            todayLabel.Text = "Today date";
            // 
            // homeButton
            // 
            homeButton.Location = new Point(24, 12);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(75, 23);
            homeButton.TabIndex = 14;
            homeButton.Text = "Home";
            homeButton.UseVisualStyleBackColor = true;
            homeButton.Click += homeButton_Click;
            // 
            // homeTextLabel
            // 
            homeTextLabel.AutoSize = true;
            homeTextLabel.Font = new Font("Segoe UI", 18F);
            homeTextLabel.Location = new Point(509, 93);
            homeTextLabel.MaximumSize = new Size(800, 0);
            homeTextLabel.Name = "homeTextLabel";
            homeTextLabel.Size = new Size(79, 32);
            homeTextLabel.TabIndex = 16;
            homeTextLabel.Text = "Home";
            // 
            // EntryContainer
            // 
            EntryContainer.AutoScroll = true;
            EntryContainer.Location = new Point(150, 520);
            EntryContainer.Name = "EntryContainer";
            EntryContainer.Size = new Size(353, 210);
            EntryContainer.TabIndex = 18;
            // 
            // Diary
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1470, 785);
            Controls.Add(EntryContainer);
            Controls.Add(homeText);
            Controls.Add(homeTextLabel);
            Controls.Add(homeButton);
            Controls.Add(todayLabel);
            Controls.Add(currentDayLabel);
            Controls.Add(saveButton);
            Controls.Add(textAreaContainer);
            Controls.Add(daysContainer);
            Controls.Add(labelYear);
            Controls.Add(labelMonth);
            Controls.Add(btnNextMonth);
            Controls.Add(btnPrevMonth);
            Controls.Add(btnNextYear);
            Controls.Add(btnPrevYear);
            Controls.Add(label1);
            Name = "Diary";
            Text = "Form1";
            Load += Diary_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnPrevYear;
        private Button btnNextYear;
        private Button btnNextMonth;
        private Button btnPrevMonth;
        private Label labelMonth;
        private Label labelYear;
        private FlowLayoutPanel daysContainer;
        private FlowLayoutPanel textAreaContainer;
        private Button saveButton;
        private Label currentDayLabel;
        private Label todayLabel;
        private Button homeButton;
        private Label homeTextLabel;
        private TextBox homeText;
        private FlowLayoutPanel EntryContainer;
    }
}
