using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WordProcessor
{
    public class MyForm : Form
    {
        static List<string> dictionary = new List<string>();
        [STAThread]
        public static void Main(string[] args)
        {

            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(args));

        }
        private Button myButton;
        private Timer timer;
        private Random random;
        private TextBox inputBox;
        private TextBox inputBoxIndentLineCount;
        private Label outputLabel;
        private static RichTextBox fileTextOutput;
        private Label fileLabel;
        private Label indentCountLabel;


        private TextBox addLineInputBox;
        private Label addLineLabel_1;


        private TextBox replacebox1;
        private Label replacelabel1;

        private TextBox replacebox2;
        private Label replacelabel2;
        public MyForm()
        {
            this.Text = "Text File Modder";
            this.ClientSize = new Size(800, 600);
            //button 1
            this.myButton = new Button();
            this.myButton.Text = "Read File";
            this.myButton.Size = new Size(100, 50);
            this.myButton.Location = new Point(5, 5); // set the button's position
            this.myButton.Click += new EventHandler(ReadString);

            this.Controls.Add(myButton);

            //button 2
            this.myButton = new Button();
            this.myButton.Text = "Remove All Indentations";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(110, 5); // set the button's position
            this.myButton.Click += new EventHandler(RemoveAllIndents);

            this.Controls.Add(myButton);

            //button 3
            this.myButton = new Button();
            this.myButton.Text = "Add Indents Every X Lines";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(250, 5); // set the button's position
            this.myButton.Click += new EventHandler(AddIndentations);

            this.Controls.Add(myButton);

            //button 4
            this.myButton = new Button();
            this.myButton.Text = "Add To End Of Each Line";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(400, 5); // set the button's position
            this.myButton.Click += new EventHandler(AddEveryLine);

            this.Controls.Add(myButton);

            //button 5
            this.myButton = new Button();
            this.myButton.Text = "Remove Last Line";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(400, 150); // set the button's position
            this.myButton.Click += new EventHandler(RemoveLastLineFromFile);

            this.Controls.Add(myButton);


            //button 7
            this.myButton = new Button();
            this.myButton.Text = "Remove All Duplicate Words";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(550, 150); // set the button's position
            this.myButton.Click += new EventHandler(RemoveDuplicates);

            this.Controls.Add(myButton);

            //button 8
            this.myButton = new Button();
            this.myButton.Text = "Remove All Duplicate Words";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(550, 150); // set the button's position
            this.myButton.Click += new EventHandler(RemoveDuplicates);

            this.Controls.Add(myButton);

            //button 9
            this.myButton = new Button();
            this.myButton.Text = "Check Spelling";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(550, 300); // set the button's position
            this.myButton.Click += new EventHandler(RunSpellCheck);

            this.Controls.Add(myButton);

            //button 9
            this.myButton = new Button();
            this.myButton.Text = "Save text box to file";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(400, 300); // set the button's position
            this.myButton.Click += new EventHandler(SaveTextBoxToFile);

            this.Controls.Add(myButton);

            //button 10
            this.myButton = new Button();
            this.myButton.Text = "change text color";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(400, 500); // set the button's position
            this.myButton.Click += new EventHandler(ChangeTextColor);

            this.Controls.Add(myButton);
            /*
            //timer
            this.timer = new Timer();
            this.timer.Interval = 1000; // 1 seconds
            this.timer.Tick += new EventHandler(Timer_Tick);

            //random mover
            this.random = new Random();
            this.StartPosition = FormStartPosition.Manual;

            this.timer.Start();
            */

            // make the window resizable
            //this.FormBorderStyle = FormBorderStyle.Sizable;


            //TEXT VIEWING WINDOW
            fileTextOutput = new RichTextBox();
            fileTextOutput.Location = new Point(5, 150);
            fileTextOutput.Size = new Size(200, 400);
            fileTextOutput.BorderStyle = BorderStyle.FixedSingle; // add a border
            fileTextOutput.WordWrap = false; // don't wrap text to the next line
            fileTextOutput.ScrollBars = RichTextBoxScrollBars.Horizontal; // add a horizontal scrollbar
            this.Controls.Add(fileTextOutput);
            fileTextOutput.AutoScrollOffset = new Point(-1000, 0); // scroll the text to the left when it exceeds the width of the control

            //GET FILE NAME
            // Add label
            this.fileLabel = new Label();
            this.fileLabel.Text = "File name here:";
            this.fileLabel.Location = new Point(10, 55);
            this.fileLabel.Size = new Size(200, 20);
            this.fileLabel.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.fileLabel);

            // Create and configure input TextBox 
            this.inputBox = new TextBox();
            this.inputBox.Location = new Point(5, 80);
            this.inputBox.Size = new Size(200, 20);
            this.inputBox.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.inputBox.TextChanged += new EventHandler(InputBox_TextChanged);
            this.Controls.Add(this.inputBox);

            // Create and configure output Label
            this.outputLabel = new Label();
            this.outputLabel.Location = new Point(5, 110);
            this.outputLabel.Size = new Size(200, 20);
            this.outputLabel.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.outputLabel);

            //SET INDENT EACH X LINES
            // Add label
            this.indentCountLabel = new Label();
            this.indentCountLabel.Text = "Set Line Indent Count (X):";
            this.indentCountLabel.Location = new Point(245, 55);
            this.indentCountLabel.Size = new Size(150, 20);
            this.indentCountLabel.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.indentCountLabel);

            // Create and configure input TextBox
            this.inputBoxIndentLineCount = new TextBox();
            this.inputBoxIndentLineCount.Location = new Point(240, 80);
            this.inputBoxIndentLineCount.Size = new Size(150, 20);
            this.inputBoxIndentLineCount.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.inputBoxIndentLineCount.TextChanged += new EventHandler(SetIndentationLineCount);
            this.Controls.Add(this.inputBoxIndentLineCount);


            //CHANGE END OF EACH LINE
            // Add label
            this.addLineLabel_1 = new Label();
            this.addLineLabel_1.Text = "Add to each line:";
            this.addLineLabel_1.Location = new Point(400, 55);
            this.addLineLabel_1.Size = new Size(200, 20);
            this.addLineLabel_1.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.addLineLabel_1);

            // Create and configure input TextBox 
            this.addLineInputBox = new TextBox();
            this.addLineInputBox.Location = new Point(405, 80);
            this.addLineInputBox.Size = new Size(200, 20);
            this.addLineInputBox.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.addLineInputBox.TextChanged += new EventHandler(SetStringForEachLine);
            this.Controls.Add(this.addLineInputBox);


            //CHANGE END OF EACH LINE
            //button 6
            this.myButton = new Button();
            this.myButton.Text = "Replace text";
            this.myButton.Size = new Size(120, 50);
            this.myButton.Location = new Point(250, 150); // set the button's position
            this.myButton.Click += new EventHandler(WriteStringReplace);

            this.Controls.Add(myButton);
            // Add label
            this.replacelabel1 = new Label();
            this.replacelabel1.Text = "Text to replace:";
            this.replacelabel1.Location = new Point(250, 205);
            this.replacelabel1.Size = new Size(120, 20);
            this.replacelabel1.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.replacelabel1);

            // Create and configure input TextBox 
            this.replacebox1 = new TextBox();
            this.replacebox1.Location = new Point(250, 230);
            this.replacebox1.Size = new Size(120, 20);
            this.replacebox1.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.replacebox1.TextChanged += new EventHandler(SetStringToFind);
            this.Controls.Add(this.replacebox1);
            // Add label
            this.replacelabel2 = new Label();
            this.replacelabel2.Text = "Replacement text:";
            this.replacelabel2.Location = new Point(250, 260);
            this.replacelabel2.Size = new Size(120, 20);
            this.replacelabel2.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.Controls.Add(this.replacelabel2);

            // Create and configure input TextBox 
            this.replacebox2 = new TextBox();
            this.replacebox2.Location = new Point(250, 285);
            this.replacebox2.Size = new Size(120, 20);
            this.replacebox2.BorderStyle = BorderStyle.FixedSingle; // add a border
            this.replacebox2.TextChanged += new EventHandler(SetStringToReplace);
            this.Controls.Add(this.replacebox2);

        }
        //this.outputLabel.Text is the file to use path
        private void ChangeTextColor(object sender, EventArgs e)
        {
            string text = ":D";
            int Index = fileTextOutput.Text.IndexOf(":D");
            int length = text.Length;
            fileTextOutput.Select(Index, length);
            fileTextOutput.SelectionColor = Color.Red;
        }
        private void ChangeStringColor(string text, Color color)
        {
            int Index = fileTextOutput.Text.IndexOf(text);
            int length = text.Length;
            fileTextOutput.Select(Index, length);
            fileTextOutput.SelectionColor = color;
        }
        public void RunSpellCheck(object sender, EventArgs e)
        {
            //get dictionary
            string fileName = "SpellCheckDictionary.txt";
            string directoryPath = Directory.GetCurrentDirectory();
            // Combine the directory path and file name
            string filePath = Path.Combine(directoryPath, fileName);

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            foreach (string line in lines)
            {
                dictionary.Add(line.ToLower());
            }
            // Run spell check on startup
            string output = SpellCheckBetter(fileTextOutput.Text);

            // Get a reference 
            fileTextOutput.Text = output;
        }
        string SpellCheckBetter(string input)
        {

            // Split input into lines
            string[] lines = input.Split('\n');

            // Check each line for spelling and grammar errors
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Split line into words and punctuation marks
                string[] words = line.Split(new char[] { '"', '-', ' ', ',', '!', '?', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

                // Check each word for spelling and grammar errors
                StringBuilder checkedLine = new StringBuilder();
                for (int j = 0; j < words.Length; j++)
                {
                    string word = words[j];
                    bool isLastChar = false;

                    // Check if the word is the last character of the line and exclude the last character if it's a punctuation mark
                    if (j == words.Length - 1 && line[line.Length - 1] != ' ' && !Char.IsLetterOrDigit(line[line.Length - 1]))
                    {
                        word = word.Substring(0, word.Length - 1);
                        isLastChar = true;
                    }

                    // Check for spelling errors
                    if (!dictionary.Contains(word.ToLower()))
                    {
                        // Set the color of the misspelled word to red (WONT WORK?!)
                        fileTextOutput.SelectionStart = line.IndexOf(word);
                        fileTextOutput.SelectionLength = word.Length;
                        fileTextOutput.SelectionColor = Color.Red;

                        checkedLine.Append("*" + word);
                        if (!isLastChar) checkedLine.Append(" ");


                        continue;
                    }

                    // Check for common grammar errors
                    if (j > 0 && (word == "an"))
                    {
                        string previousWord = words[j - 1];
                        if (previousWord.EndsWith("ing") || previousWord.EndsWith("ed"))
                        {
                            // Suggest using "a" instead of "an" before verbs in the past or present participle form
                            checkedLine.Append("an *suggested: a*");
                            if (!isLastChar) checkedLine.Append(" ");
                            continue;
                        }
                    }

                    checkedLine.Append(word);
                    if (!isLastChar) checkedLine.Append(" ");
                }

                // Append checked line to output with original formatting intact
                output.Append(checkedLine.ToString().TrimEnd());
                if (i < lines.Length - 1)
                {
                    output.Append('\n'); // Add back the newline character that was removed by Split
                }
            }

            return output.ToString();
        }
        private void SaveTextBoxToFile(object sender, EventArgs e)
        {
            string outputText = fileTextOutput.Text; // Get the text to write
            File.WriteAllText(this.outputLabel.Text, outputText); // Write the text to the file

            MessageBox.Show("File saved!");
        }
        private string writeStringText;
        private string replaceStringText;
        public void SetStringToFind(object sender, EventArgs e)
        {
            writeStringText = this.replacebox1.Text;
        }
        public void SetStringToReplace(object sender, EventArgs e)
        {
            replaceStringText = this.replacebox2.Text;
        }
        public void WriteStringReplace(object sender, EventArgs e)
        {

            string searchText = writeStringText; // Set the text to search for
            string replaceText = replaceStringText; // Set the text to replace with
            string text = fileTextOutput.Text; // Get the text of the control
            text = text.Replace(searchText, replaceText); // Replace the text
            fileTextOutput.Text = text; // Set the text of the control to the modified text


            //ReadString(sender, e);


        }
        private string inputChange;
        private int lineCount;
        public void SetIndentationLineCount(object sender, EventArgs e)
        {
            // Set the value of X to the number of lines
            // after which you want to add indentation
            inputChange = this.inputBoxIndentLineCount.Text;
            int.TryParse(inputChange, out lineCount);
            //Debug.Log(lineCount);
        }
        public void AddIndentations(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(this.outputLabel.Text);

            // Write the output file with added indentation
            using (StreamWriter writer = new StreamWriter(this.outputLabel.Text))
            {
                int count = 0;
                foreach (string line in lines)
                {
                    // Write the current line
                    writer.WriteLine(line);

                    // Check if X number of lines have been written
                    count++;
                    if (count == lineCount)
                    {
                        // Add a blank line (indentation)
                        writer.WriteLine();

                        // Reset the count
                        count = 0;
                    }
                }
            }

            ReadString(sender, e);
        }
        public void RemoveLastLineFromFile(object sender, EventArgs e)
        {
            // Read all the lines from the file
            string[] lines = File.ReadAllLines(this.outputLabel.Text);

            // Remove the last line
            Array.Resize(ref lines, lines.Length - 1);

            // Overwrite the file with the modified lines
            File.WriteAllLines(this.outputLabel.Text, lines);


            ReadString(sender, e);
        }
        private void RemoveDuplicates(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();
            HashSet<string> uniqueWords = new HashSet<string>();

            using (StreamReader reader = new StreamReader(this.outputLabel.Text))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < words.Length; i++)
                    {
                        string word = words[i].ToLower();

                        if (!uniqueWords.Contains(word))
                        {
                            uniqueWords.Add(word);
                        }
                        else
                        {
                            words[i] = "";
                        }
                    }

                    line = string.Join(" ", words);
                    lines.Add(line);
                }
            }

            using (StreamWriter writer = new StreamWriter(this.outputLabel.Text))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
            ReadString(sender, e);
        }
        private static string finalReadString;
        private void ReadString(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(this.outputLabel.Text);
            finalReadString = reader.ReadToEnd();
            fileTextOutput.Text = finalReadString;
            reader.Close();
        }
        private void InputBox_TextChanged(object sender, EventArgs e)
        {
            this.outputLabel.Text = this.inputBox.Text;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            int x = this.random.Next(Screen.PrimaryScreen.Bounds.Width - this.Width);
            int y = this.random.Next(Screen.PrimaryScreen.Bounds.Height - this.Height);

            this.Location = new Point(x, y);
        }

        private void RemoveAllIndents(object sender, EventArgs e)
        {
            string filePath = this.outputLabel.Text;
            string[] lines = File.ReadAllLines(filePath);

            // Remove blank lines and indentation from each line
            for (int i = 0; i < lines.Length; i++)
            {
                string trimmedLine = lines[i].TrimStart();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    lines[i] = trimmedLine;
                }
                else
                {
                    lines[i] = null; // Mark blank lines for removal
                }
            }

            // Write modified lines back to the same file
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    if (line != null)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            ReadString(sender, e);
        }


        private string stringForEachLine;
        public void SetStringForEachLine(object sender, EventArgs e)
        {
            stringForEachLine = this.addLineInputBox.Text;

            ReadString(sender, e);
        }
        public void AddEveryLine(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(this.outputLabel.Text);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] += stringForEachLine;
            }

            File.WriteAllLines(this.outputLabel.Text, lines);

            ReadString(sender, e);
        }
        public void RemoveLastCharacterFromEveryLine(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(this.outputLabel.Text);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 0) // Make sure line is not empty
                {
                    lines[i] = lines[i].Substring(0, lines[i].Length - 1); // Remove last character
                }
            }

            File.WriteAllLines(this.outputLabel.Text, lines);

            ReadString(sender, e);
        }
    }
}