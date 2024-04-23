using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Drawing.Printing;

namespace WordProcessor
{
    public partial class Form1 : Form
    {
        static List<string> dictionary = new List<string>();
        string[] _args;
        public Form1(string[] args)
        {
            InitializeComponent();
            _args = args;

            this.Text = "Notepad On CrAcK";


            // Set the Anchor property to anchor the control to the sides of its container
            fileTextOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        //FUNCTIONS *****************************************************************************************************************************
        //load the file text to the rich text box
        private string fullFilePath;
        private bool isLoading = true;
        private int statusBarValue = 0;
        private bool statusBarToggle = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            //loading start check
            isLoading = true;

            LoadUnsaveTxtTextColor();
            LoadSaveTxtTextColor();
            SavedText();
            //get file path from args
            if (_args.Length > 0)
            {
                SavedText();
                if (File.Exists(_args[0]))
                {
                    //openfile on load (extension)
                    //IsFileTextChanged();
                    fileTextOutput.Text = File.ReadAllText(_args[0]);
                    fullFilePath = Path.GetFullPath(_args[0]);
                    this.outputLabel.Text = fullFilePath;
                    this.Text = "Notepad On CrAcK";
                }
            }

            // Set KeyPreview property to true to handle keyboard events at the form level
            this.KeyPreview = true;
            //load colors
            LoadTxtBoxTextColor();
            LoadBackgroundPanelColor();
            LoadPanelTextColor();
            LoadBackgroundTxtBoxColor();
            LoadHighlighterColor();

            //load settings
            LoadFormSize();
            //LoadFontSizeValue();//load font size
            LoadFontNameFromJson();

            //FontSettingsManager.LoadFontSettings();
            //fileTextOutput.Font = new Font(fileTextOutput.Font.FontFamily, fontSize, fileTextOutput.Font.Style);//set font size
            fileTextOutput.Font = new Font(fileTextOutput.Font.FontFamily, 12, fileTextOutput.Font.Style);//set font size

            //load and set bold mode
            LoadBoldModeValue();
            if (boldMode == 1)
            {
                MakeTextBold(fileTextOutput);
            }
            if (boldMode == 0)
            {
                MakeTextNotBold(fileTextOutput);
            }

            LoadWordWrapValue();
            if (wordWrapValue == 1)
            {
                wordwrap_label2.Text = "Wordwrap On";
                //nothing (wordwrap on)
            }
            if (wordWrapValue == 0)
            {
                wordwrap_label2.Text = "Wordwrap Off";
                ToggleWordWrap(fileTextOutput);//turns word wrap off by def
            }

            LoadStatusBarValue();
            if (statusBarValue == 1)//status bar off
            {
                HideStatusBar();
            }
            if (statusBarValue == 0)//status bar on
            {
                ShowStatusBar();
            }
            //other loads
            //ToggleWordWrap(fileTextOutput);//turns word wrap off by def
            //fontSize = 12;//set def text size


            //loading done check
            isLoading = false;
        }
        public void RemoveLastLineFromTextBox()
        {
            // Read all the lines from the file
            string[] lines = fileTextOutput.Lines;

            // Remove the last line
            Array.Resize(ref lines, lines.Length - 1);

            // Overwrite the file with the modified lines
            fileTextOutput.Lines = lines;

            AddToRedoStack(fileTextOutput.Text);

            //ReadString(sender, e);
        }
        public void RemoveLastCharacterFromEveryLine()
        {
            string[] lines = fileTextOutput.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 0) // Make sure line is not empty
                {
                    lines[i] = lines[i].Substring(0, lines[i].Length - 1); // Remove last character
                }
            }
            fileTextOutput.Lines = lines;

            AddToRedoStack(fileTextOutput.Text);
        }
        public void RemoveFirstCharacterFromEveryLine()
        {
            string[] lines = fileTextOutput.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 0) // Make sure line is not empty
                {
                    lines[i] = lines[i].Substring(1); // Remove first character
                }
            }

            fileTextOutput.Lines = lines;
            
            //AddToUndoStack(fileTextOutput.Text);
        }

        private void SaveFormSize()
        {
            var data = new { Width = this.Width, Height = this.Height };

            string jsonFilePath = Path.Combine(Application.StartupPath, "formSize.json");
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonFilePath, json);
        }

        private void LoadFormSize()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "formSize.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int width = Convert.ToInt32(data.Width);
                    int height = Convert.ToInt32(data.Height);
                    this.Width = width;
                    this.Height = height;
                }
            }
        }

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();
        private void AddToRedoStack(string text)
        {
            redoStack.Push(text);
        }
        private void AddToUndoStack(string text)
        {
            undoStack.Push(text);
        }
        private void ClearUndoStack()
        {
            undoStack.Clear();
        }
        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                string text = undoStack.Pop();
                redoStack.Push(text);
                UpdateText(text);
            }
        }
        private void Redo()
        {
            if (redoStack.Count > 0)
            {
                string text = redoStack.Pop();
                undoStack.Push(text);
                UpdateText(text);
            }
        }
        private void UpdateText(string text)
        {
            fileTextOutput.Text = text;
        }

        private void ToggleWordWrap(RichTextBox textBox)
        {
            textBox.WordWrap = !textBox.WordWrap;
            SaveWordWrapValue();
        }
        private void ToggleStatusBar()
        {
            statusBarToggle = !statusBarToggle;
            SaveStatusBarValue();
        }

        private string searchText;
        private string replaceText;

        public void ReplaceAllText()
        {
            // Create a new instance of the Form class for the input box
            Form inputForm = new Form();
            inputForm.Text = "Replace All Text";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Size = new Size(350, 160);

            // Create a new instance of the Label class for the search text label
            Label searchLabel = new Label();
            searchLabel.Text = "Search Text:";
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(10, 10);

            // Create a new instance of the TextBox class for search text input
            TextBox searchTextBox = new TextBox();
            searchTextBox.Size = new Size(200, 25);
            searchTextBox.Location = new Point(110, 10);

            // Create a new instance of the Label class for the replace text label
            Label replaceLabel = new Label();
            replaceLabel.Text = "Replace Text:";
            replaceLabel.AutoSize = true;
            replaceLabel.Location = new Point(10, 45);

            // Create a new instance of the TextBox class for replace text input
            TextBox replaceTextBox = new TextBox();
            replaceTextBox.Size = new Size(200, 25);
            replaceTextBox.Location = new Point(110, 45);

            // Create a new instance of the Button class for submitting the input
            Button submitButton = new Button();
            submitButton.Text = "OK";
            submitButton.Size = new Size(75, 25);
            submitButton.Location = new Point(10, 80);

            // Add an event handler to the button's Click event
            submitButton.Click += (sender, e) =>
            {
                searchText = searchTextBox.Text;
                replaceText = replaceTextBox.Text;

                if (fileTextOutput != null && fileTextOutput.Text != null)
                {
                    string text = fileTextOutput.Text;

                    if (searchText != null && replaceText != null)
                    {
                        text = text.Replace(searchText, replaceText);
                        fileTextOutput.Text = text;
                        AddToRedoStack(fileTextOutput.Text);
                    }
                }

                // Close the input form
                inputForm.Close();
            };


            // Add an event handler to the inputForm's KeyPreview property's KeyDown event
            inputForm.KeyPreview = true;
            inputForm.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // simulate the submitButton's Click event
                    submitButton.PerformClick();
                }
            };

            // Add the controls to the form's controls
            inputForm.Controls.Add(searchLabel);
            inputForm.Controls.Add(searchTextBox);
            inputForm.Controls.Add(replaceLabel);
            inputForm.Controls.Add(replaceTextBox);
            inputForm.Controls.Add(submitButton);

            // Display the input form
            inputForm.ShowDialog();
        }
        private void SaveFile(string filePath)
        {
            // Save the file with the current contents of the rich text box
            File.WriteAllText(filePath, fileTextOutput.Text); //this is the contents text
            SavedText();

            ClearUndoStack();
        }

        private void SaveFileAs()
        {
            // Show a save file dialog and get the selected file path
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the file with the selected file path
                SaveFile(saveFileDialog.FileName);
                UpdateOutputLabel(saveFileDialog.FileName);
                SavedText();
            }

            ClearUndoStack();
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Read the file contents and display them in the rich text box
                fileTextOutput.Text = File.ReadAllText(openFileDialog.FileName);
                this.outputLabel.Text = openFileDialog.FileName;
                UpdateOutputLabel(this.outputLabel.Text);
            }
            SavedText();
            ClearUndoStack();

        }
        //unsaved text color
        private void ChangeUnsavedTextColor(Color color)
        {
            unsavedtextColor = color;
            modified_label2.ForeColor = unsavedtextColor;
            SaveUnsavedTxtTextColor();
        }
        private void colorUnsavedTextChangeFunction()
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ChangeUnsavedTextColor(colorDialog.Color);
            }
        }
        private void SaveUnsavedTxtTextColor()
        {
            var color = modified_label2.ForeColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "unsavedTxtColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private Color unsavedtextColor;
        private void LoadUnsaveTxtTextColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "unsavedTxtColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    unsavedtextColor = Color.FromArgb(r, g, b);
                    modified_label2.ForeColor = unsavedtextColor;
                }
            }
        }
        //saved text color
        private void ChangeSaveTextColor(Color color)
        {
            modified_label2.ForeColor = color;
            SaveSaveTxtTextColor();
        }
        private void colorSaveTextChangeFunction()
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ChangeSaveTextColor(colorDialog.Color);
            }
        }
        private void SaveSaveTxtTextColor()
        {
            var color = modified_label2.ForeColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "savedTxtColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private Color savetextColor;
        private void LoadSaveTxtTextColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "savedTxtColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    savetextColor = Color.FromArgb(r, g, b);
                    modified_label2.ForeColor = savetextColor;
                }
            }
        }
        //textbox
        private void ChangeTextBoxTextColor(Color color)
        {
            fileTextOutput.ForeColor = color;
            SaveTxtBoxTextColor();
        }
        private void colorTextChangeFunction()
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ChangeTextBoxTextColor(colorDialog.Color);
            }
        }
        private void SaveTxtBoxTextColor()
        {
            var color = fileTextOutput.ForeColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "txtBoxTextColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private Color textColor;
        private void LoadTxtBoxTextColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "txtBoxTextColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    textColor = Color.FromArgb(r, g, b);
                    fileTextOutput.ForeColor = textColor;
                }
            }
        }
        //this.outputLabel.Text is the file to use path
        private void ChangeTxtBoxBackgroundColor()
        {
            // Show color picker dialog to select color
            ColorDialog colorPicker = new ColorDialog();
            DialogResult result = colorPicker.ShowDialog();

            // If user selects a color, set it as the background color of the fileTextOutput
            if (result == DialogResult.OK)
            {
                fileTextOutput.BackColor = colorPicker.Color;
            }
            SaveBackgroundTxtBoxColor();
        }
        private void SaveBackgroundTxtBoxColor()
        {
            var color = fileTextOutput.BackColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "backgroundColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private void LoadBackgroundTxtBoxColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "backgroundColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    Color color = Color.FromArgb(r, g, b);
                    fileTextOutput.BackColor = color;
                }
            }
        }
        private void ChangeAppTextColor()
        {
            ColorDialog colorPicker = new ColorDialog();
            DialogResult result = colorPicker.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.ForeColor = colorPicker.Color;
            }
            SavePanelTextColor();
        }
        private void SavePanelTextColor()
        {
            var color = this.ForeColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "panelTxtColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private void LoadPanelTextColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "panelTxtColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    Color color = Color.FromArgb(r, g, b);
                    this.ForeColor = color;
                }
            }
        }
        private void SelectBackgroundColor()
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.BackColor = colorDialog.Color;
            }
            SaveBackgroundPanelColor();
        }
        private void SaveBackgroundPanelColor()
        {
            var color = this.BackColor;
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "panelBGColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private void LoadBackgroundPanelColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "panelBGColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    Color color = Color.FromArgb(r, g, b);
                    this.BackColor = color;
                }
            }
        }
        private void FindChangeTextColor()
        {
            // create a form to ask for user input
            Form inputForm = new Form();
            inputForm.Text = "Enter text to highlight";
            Label label = new Label();
            label.Text = "Enter the text to highlight:";
            label.AutoSize = true;
            label.Location = new Point(10, 20);
            TextBox textBox = new TextBox();
            textBox.Location = new Point(10, 50);
            Button buttonOk = new Button();
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(110, 80);
            inputForm.ClientSize = new Size(240, 110);
            inputForm.Controls.AddRange(new Control[] { label, textBox, buttonOk });
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.StartPosition = FormStartPosition.CenterScreen;

            // allow enter key to submit the input
            textBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonOk.PerformClick();
                }
            };

            // show the input form and get user input from the textbox
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string text = textBox.Text;

                // highlight the text in the RichTextBox
                int index = 0;
                int length = text.Length;
                while (index < fileTextOutput.Text.Length)
                {
                    index = fileTextOutput.Find(text, index, RichTextBoxFinds.None);
                    if (index == -1) break; // exit the loop if the text is not found
                    fileTextOutput.SelectionStart = index;
                    fileTextOutput.SelectionLength = length;
                    fileTextOutput.SelectionColor = highlighterColor;
                    index += length;
                }

                // add the highlighted text to the redo stack
                AddToRedoStack(fileTextOutput.Text);
            }
        }
        private Color highlighterColor;
        private void SetHighlighterColor()
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                highlighterColor = colorDialog.Color;
            }
            SaveHighlighterColor();
        }
        private void SaveHighlighterColor()
        {
            var color = highlighterColor; // color to save
            var data = new { R = color.R, G = color.G, B = color.B };

            var json = JsonConvert.SerializeObject(data);

            // Get the application path and combine it with the file name
            string fullPath = Path.Combine(Application.StartupPath, "highlighterColor.json");

            // Write the text to the file
            File.WriteAllText(fullPath, json);
        }
        private void LoadHighlighterColor()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "highlighterColor.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int r = Convert.ToInt32(data.R);
                    int g = Convert.ToInt32(data.G);
                    int b = Convert.ToInt32(data.B);
                    Color color = Color.FromArgb(r, g, b);
                    highlighterColor = color; // color to load
                }
            }
        }
        public void RunSpellCheck()
        {
            // Get dictionary file path from application path
            string dictionaryFileName = "SpellCheckDictionary.txt";
            string dictionaryFilePath = Path.Combine(Application.StartupPath, dictionaryFileName);

            // Read dictionary file and add words to dictionary
            string[] lines = File.ReadAllLines(dictionaryFilePath, Encoding.UTF8);
            foreach (string line in lines)
            {
                dictionary.Add(line.ToLower());
            }

            // Run spell check on startup
            string output = SpellCheckBetter(fileTextOutput.Text);

            // Set the text of the RichTextBox to the spell-checked output
            fileTextOutput.Text = output;

            AddToRedoStack(fileTextOutput.Text);
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

                        checkedLine.Append("*" + word);
                        if (!isLastChar) checkedLine.Append(" ");


                        // Set the color of the misspelled word to red (WONT WORK?!)
                        //ChangeStringColor(word, Color.Red);

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
        private void UpdateOutputLabel(string filePath)
        {
            // Display the file name in the output label
            outputLabel.Text = $"{Path.GetFullPath(filePath)}";
        }
        private void MakeTextBold(RichTextBox textBox)
        {
            if (textBox.SelectionFont != null)
            {
                textBox.SelectionFont = new Font(textBox.SelectionFont.FontFamily, fontSize, FontStyle.Bold);
                SaveBoldModeValue(1);
            }

        }
        private void MakeTextNotBold(RichTextBox textBox)
        {
            if (textBox.SelectionFont != null)
            {
                Font currentFont = textBox.SelectionFont;
                FontStyle newStyle = currentFont.Style & ~FontStyle.Bold;
                textBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
                SaveBoldModeValue(0);
            }
        }
        private int fontSize;
        private void ChangeFontSize(RichTextBox richTextBox)
        {
            // create a new form with a label and text box for the user input
            Form form = new Form();
            form.Text = "Change Font Size";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterParent;
            Label label = new Label();
            label.Text = "Enter the new font size:";
            label.AutoSize = true;
            TextBox textBox = new TextBox();
            textBox.Text = richTextBox.SelectionFont.Size.ToString();
            textBox.Width = 50;
            Button buttonOk = new Button();
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            Button buttonCancel = new Button();
            buttonCancel.Text = "Cancel";
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 150, 20);
            buttonOk.SetBounds(191, 36, 75, 23);
            buttonCancel.SetBounds(272, 36, 75, 23);
            label.AutoSize = true;
            form.ClientSize = new Size(356, 80);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            // display the form as a dialog box
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && int.TryParse(textBox.Text, out fontSize))
            {
                FontStyle previousFontStyle = richTextBox.SelectionFont.Style;

                // set the new font size
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont.FontFamily, fontSize, previousFontStyle);
                //SaveFontSizeValue(fontSize);
                // restore the selection and previous font style
                //richTextBox.Select(selectionStart, selectionLength);
                //richTextBox.SelectionFont = new Font(richTextBox.Font, previousFontStyle);
            }
        }

        //NEED TO ADD BUTTON TO SET DEFUALT SAVED FONT SIZE
        /*
        private void SaveFontSizeValue(int value)
        {
            var data = new { Value = value };

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText("fontSizeValue.json", json);
        }

        private int LoadFontSizeValue()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "fontSizeValue.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int value = Convert.ToInt32(data.Value);
                    fontSize = value;
                    return value;
                }
            }

            return 0;
        }*/

        private int lineToIndent;
        private int indentCount;

        public void AddIndentationsWithPopup()
        {
            // Create a new instance of the input form
            InputForm inputForm = new InputForm();

            // Show the input form as a modal dialog and get the result
            DialogResult result = inputForm.ShowDialog();

            // If the user clicked OK, parse the input values and add indentations
            if (result == DialogResult.OK)
            {
                int.TryParse(inputForm.LineToIndentInputValue, out lineToIndent);
                int.TryParse(inputForm.IndentCountInputValue, out indentCount);

                string[] lines = fileTextOutput.Lines;
                List<string> outputLines = new List<string>();

                int count = 0;
                foreach (string line in lines)
                {
                    // Add the current line
                    outputLines.Add(line);

                    // Check if lineToIndent number of lines have been added
                    count++;
                    if (count == lineToIndent)
                    {
                        // Add the specified number of indents
                        for (int i = 0; i < indentCount; i++)
                        {
                            outputLines.Add("");
                        }

                        // Reset the count
                        count = 0;
                    }
                }

                // Set the lines of the output RichTextBox
                fileTextOutput.Lines = outputLines.ToArray();
            }
            AddToRedoStack(fileTextOutput.Text);
        }

        // InputForm class
        public class InputForm : Form
        {
            private Label lineToIndentLabel;
            private TextBox lineToIndentTextBox;
            private Label indentCountLabel;
            private TextBox indentCountTextBox;
            private Button okButton;
            private Button cancelButton;

            public string LineToIndentInputValue { get { return lineToIndentTextBox.Text; } }
            public string IndentCountInputValue { get { return indentCountTextBox.Text; } }

            public InputForm()
            {
                // Initialize components
                this.Text = "Set Indentation Line Count";
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Size = new Size(350, 200);

                lineToIndentLabel = new Label();
                lineToIndentLabel.Text = "Enter the line number after which to add indentation:";
                lineToIndentLabel.Location = new Point(10, 10);
                lineToIndentLabel.AutoSize = true;

                lineToIndentTextBox = new TextBox();
                lineToIndentTextBox.Location = new Point(10, 30);
                lineToIndentTextBox.Size = new Size(100, 20);

                indentCountLabel = new Label();
                indentCountLabel.Text = "Enter the number of indents to add:";
                indentCountLabel.Location = new Point(10, 60);
                indentCountLabel.AutoSize = true;

                indentCountTextBox = new TextBox();
                indentCountTextBox.Location = new Point(10, 80);
                indentCountTextBox.Size = new Size(100, 20);

                okButton = new Button();
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(10, 120);

                cancelButton = new Button();
                cancelButton.Text = "Cancel";
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Location = new Point(90, 120);

                this.Controls.Add(lineToIndentLabel);
                this.Controls.Add(lineToIndentTextBox);
                this.Controls.Add(indentCountLabel);
                this.Controls.Add(indentCountTextBox);
                this.Controls.Add(okButton);
                this.Controls.Add(cancelButton);

                this.AcceptButton = okButton;
                this.CancelButton = cancelButton;
            }
        }
        private int boldMode;
        private void SaveBoldModeValue(int value)
        {
            var data = new { Value = value };

            string jsonFilePath = Path.Combine(Application.StartupPath, "boldModeValue.json");
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonFilePath, json);
        }

        private int LoadBoldModeValue()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "boldModeValue.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int value = Convert.ToInt32(data.Value);
                    boldMode = value;
                    return value;
                }
            }

            return 0;
        }
        private int wordWrapValue = 0;

        private void SaveWordWrapValue()
        {
            //atlernate between 0 & 1
            wordWrapValue = (wordWrapValue == 0) ? 1 : 0;
            var data = new { Value = wordWrapValue };
            string jsonFilePath = Path.Combine(Application.StartupPath, "wordWrapValue.json");
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonFilePath, json);
        }

        private int LoadWordWrapValue()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "wordwrapValue.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int value = Convert.ToInt32(data.Value);
                    wordWrapValue = value;
                    return value;
                }
            }

            return 0;
        }
        private void SaveStatusBarValue()
        {
            //atlernate between 0 & 1
            statusBarValue = (statusBarValue == 0) ? 1 : 0;
            var data = new { Value = statusBarValue };
            string jsonFilePath = Path.Combine(Application.StartupPath, "statusBarValue.json");
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonFilePath, json);
        }
        private int LoadStatusBarValue()
        {
            string jsonFilePath = Path.Combine(Application.StartupPath, "statusBarValue.json");
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data != null)
                {
                    int value = Convert.ToInt32(data.Value);
                    statusBarValue = value;
                    return value;
                }
            }

            return 0;
        }
        private void UpdateCursorPositionLabel()
        {
            int cursorPosition = fileTextOutput.SelectionStart;

            int line = fileTextOutput.GetLineFromCharIndex(cursorPosition) + 1;
            int col = cursorPosition - fileTextOutput.GetFirstCharIndexFromLine(line - 1) + 1;

            linecol_label.Text = "Line " + line + ", Col " + col;
        }
        private void UpdateWordCountLabel()
        {
            string text = fileTextOutput.Text.Trim();
            int wordCount = text.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            wordcount_label.Text = "Words: " + wordCount;
        }


        private string stringForEachLine = string.Empty;
        public void AddStartEveryLine()
        {
            // Create a new instance of the Form class for the input box
            Form inputForm = new Form();
            inputForm.Text = "Enter text to prepend to every line:";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Size = new Size(350, 120);

            // Create a new instance of the TextBox class for user input
            TextBox inputTextBox = new TextBox();
            inputTextBox.Size = new Size(200, 25);
            inputTextBox.Location = new Point(10, 10);

            // Add the TextBox to the form's controls
            inputForm.Controls.Add(inputTextBox);

            // Create a new instance of the Button class for submitting the input
            Button submitButton = new Button();
            submitButton.Text = "OK";
            submitButton.Size = new Size(75, 25);
            submitButton.Location = new Point(10, 45);

            // Add an event handler to the button's Click event
            submitButton.Click += (sender, e) =>
            {
                // Set the value of stringForEachLine to the text entered by the user
                stringForEachLine = inputTextBox.Text;

                // Get the lines of text in the fileTextOutput control
                string[] lines = fileTextOutput.Lines;

                // Iterate over each line and prepend the input text to the beginning of each line
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = stringForEachLine + lines[i];
                }

                // Set the Lines property of the fileTextOutput control to the modified lines array
                fileTextOutput.Lines = lines;

                AddToRedoStack(fileTextOutput.Text);

                // Close the input form
                inputForm.Close();
            };

            // Set the AcceptButton property of the form to the submit button, so pressing Enter will click the button
            inputForm.AcceptButton = submitButton;

            // Add the submit button to the form's controls
            inputForm.Controls.Add(submitButton);

            // Show the input form as a modal dialog, which means the user cannot interact with the parent form until the input form is closed
            inputForm.ShowDialog();
        }
        public void AddLineNumberingOnlyTextLines()
        {
            // Get the lines of text in the fileTextOutput control
            string[] lines = fileTextOutput.Lines;

            // Create a StringBuilder to hold the modified text with line numbering
            StringBuilder sb = new StringBuilder();

            // Counter for line numbering
            int lineNumber = 1;

            // Iterate over each line and append the line number followed by a space
            // then append the original line text if it's not empty
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    sb.AppendLine($"{lineNumber}. {line}");
                    lineNumber++;
                }
                else
                {
                    sb.AppendLine(); // If the line is empty, just append an empty line
                }
            }

            // Set the modified text with line numbering to the fileTextOutput control
            fileTextOutput.Text = sb.ToString();

            // Add the modified text to the redo stack
            AddToRedoStack(fileTextOutput.Text);
        }
        public void AddLineNumberingAll()
        {
            // Get the lines of text in the fileTextOutput control
            string[] lines = fileTextOutput.Lines;

            // Create a StringBuilder to hold the modified text with line numbering
            StringBuilder sb = new StringBuilder();

            // Iterate over each line and append the line number followed by a space
            // then append the original line text
            for (int i = 0; i < lines.Length; i++)
            {
                sb.AppendLine($"{i + 1}. {lines[i]}");
            }

            // Set the modified text with line numbering to the fileTextOutput control
            fileTextOutput.Text = sb.ToString();

            // Add the modified text to the redo stack
            AddToRedoStack(fileTextOutput.Text);
        }
        public void AddEndEveryLine()
        {
            // Create a new instance of the Form class for the input box
            Form inputForm = new Form();
            inputForm.Text = "Enter text to append to every line:";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Size = new Size(350, 120);

            // Create a new instance of the TextBox class for user input
            TextBox inputTextBox = new TextBox();
            inputTextBox.Size = new Size(200, 25);
            inputTextBox.Location = new Point(10, 10);

            // Add the TextBox to the form's controls
            inputForm.Controls.Add(inputTextBox);

            // Create a new instance of the Button class for submitting the input
            Button submitButton = new Button();
            submitButton.Text = "OK";
            submitButton.Size = new Size(75, 25);
            submitButton.Location = new Point(10, 45);

            // Add an event handler to the button's Click event
            submitButton.Click += (sender, e) =>
            {
                // Set the value of stringForEachLine to the text entered by the user
                stringForEachLine = inputTextBox.Text;

                // Get the lines of text in the fileTextOutput control
                string[] lines = fileTextOutput.Lines;

                // Iterate over each line and append the input text to the end of each line
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] += stringForEachLine;
                }

                // Set the Lines property of the fileTextOutput control to the modified lines array
                fileTextOutput.Lines = lines;

                AddToRedoStack(fileTextOutput.Text);

                // Close the input form
                inputForm.Close();
            };

            // Set the AcceptButton property of the form to the submit button, so pressing Enter will click the button
            inputForm.AcceptButton = submitButton;

            // Add the submit button to the form's controls
            inputForm.Controls.Add(submitButton);

            // Show the input form as a modal dialog, which means the user cannot interact with the parent form until the input form is closed
            inputForm.ShowDialog();
        }
        public void ReplaceAtEndOfEveryLine()
        {
            // Create a new instance of the Form class for the input box
            Form inputForm = new Form();
            inputForm.Text = "Find and replace text at end of every line:";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Size = new Size(350, 160);

            // Create a new instance of the Label class for the find text input
            Label findLabel = new Label();
            findLabel.Text = "Find:";
            findLabel.Size = new Size(75, 25);
            findLabel.Location = new Point(10, 10);

            // Add the find Label to the form's controls
            inputForm.Controls.Add(findLabel);

            // Create a new instance of the TextBox class for the find text input
            TextBox findTextBox = new TextBox();
            findTextBox.Size = new Size(200, 25);
            findTextBox.Location = new Point(90, 10);

            // Add the find TextBox to the form's controls
            inputForm.Controls.Add(findTextBox);

            // Create a new instance of the Label class for the replace text input
            Label replaceLabel = new Label();
            replaceLabel.Text = "Replace with:";
            replaceLabel.Size = new Size(75, 25);
            replaceLabel.Location = new Point(10, 45);

            // Add the replace Label to the form's controls
            inputForm.Controls.Add(replaceLabel);

            // Create a new instance of the TextBox class for the replace text input
            TextBox replaceTextBox = new TextBox();
            replaceTextBox.Size = new Size(200, 25);
            replaceTextBox.Location = new Point(90, 45);

            // Add the replace TextBox to the form's controls
            inputForm.Controls.Add(replaceTextBox);

            // Create a new instance of the Button class for submitting the input
            Button submitButton = new Button();
            submitButton.Text = "OK";
            submitButton.Size = new Size(75, 25);
            submitButton.Location = new Point(10, 80);

            // Add an event handler to the button's Click event
            submitButton.Click += (sender, e) =>
            {
                // Set the values of findString and replaceString to the text entered by the user
                string findString = findTextBox.Text;
                string replaceString = replaceTextBox.Text;

                // Get the lines of text in the fileTextOutput control
                string[] lines = fileTextOutput.Lines;

                // Iterate over each line and replace the find string with the replace string at the end of each line
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].EndsWith(findString))
                    {
                        lines[i] = lines[i].Substring(0, lines[i].LastIndexOf(findString)) + replaceString;
                    }
                }

                // Set the Lines property of the fileTextOutput control to the modified lines array
                fileTextOutput.Lines = lines;

                AddToRedoStack(fileTextOutput.Text);

                // Close the input form
                inputForm.Close();
            };
            // Add an event handler to the inputForm's KeyPreview property's KeyDown event
            inputForm.KeyPreview = true;
            inputForm.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // simulate the submitButton's Click event
                    submitButton.PerformClick();
                }
            };

            // Add the submit button to the form's controls
            inputForm.Controls.Add(submitButton);

            // Display the input form
            inputForm.ShowDialog();
        }
        public void ReplaceAtStartOfEveryLine()
        {
            // Create a new instance of the Form class for the input box
            Form inputForm = new Form();
            inputForm.Text = "Find and replace text at start of every line:";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Size = new Size(350, 160);

            // Create a new instance of the Label class for the find text input
            Label findLabel = new Label();
            findLabel.Text = "Find:";
            findLabel.Size = new Size(75, 25);
            findLabel.Location = new Point(10, 10);

            // Add the find Label to the form's controls
            inputForm.Controls.Add(findLabel);

            // Create a new instance of the TextBox class for the find text input
            TextBox findTextBox = new TextBox();
            findTextBox.Size = new Size(200, 25);
            findTextBox.Location = new Point(90, 10);

            // Add the find TextBox to the form's controls
            inputForm.Controls.Add(findTextBox);

            // Create a new instance of the Label class for the replace text input
            Label replaceLabel = new Label();
            replaceLabel.Text = "Replace with:";
            replaceLabel.Size = new Size(75, 25);
            replaceLabel.Location = new Point(10, 45);

            // Add the replace Label to the form's controls
            inputForm.Controls.Add(replaceLabel);

            // Create a new instance of the TextBox class for the replace text input
            TextBox replaceTextBox = new TextBox();
            replaceTextBox.Size = new Size(200, 25);
            replaceTextBox.Location = new Point(90, 45);

            // Add the replace TextBox to the form's controls
            inputForm.Controls.Add(replaceTextBox);

            // Create a new instance of the Button class for submitting the input
            Button submitButton = new Button();
            submitButton.Text = "OK";
            submitButton.Size = new Size(75, 25);
            submitButton.Location = new Point(10, 80);

            // Add an event handler to the button's Click event
            submitButton.Click += (sender, e) =>
            {
                // Set the values of findString and replaceString to the text entered by the user
                string findString = findTextBox.Text;
                string replaceString = replaceTextBox.Text;

                // Get the lines of text in the fileTextOutput control
                string[] lines = fileTextOutput.Lines;

                // Iterate over each line and replace the find string with the replace string at the start of each line
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(findString))
                    {
                        lines[i] = replaceString + lines[i].Substring(findString.Length);
                    }
                }

                // Set the Lines property of the fileTextOutput control to the modified lines array
                fileTextOutput.Lines = lines;

                AddToRedoStack(fileTextOutput.Text);

                // Close the input form
                inputForm.Close();
            };
            // Add an event handler to the inputForm's KeyPreview property's KeyDown event
            inputForm.KeyPreview = true;
            inputForm.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // simulate the submitButton's Click event
                    submitButton.PerformClick();
                }
            };

            // Add the submit button to the form's controls
            inputForm.Controls.Add(submitButton);

            // Display the input form
            inputForm.ShowDialog();
        }

        public void OpenFontSelectionForm()
        {
            // Create a new form to display the font selection dropdown
            Form fontSelectionForm = new Form();
            fontSelectionForm.Text = "Select a Font";
            fontSelectionForm.Size = new Size(250, 100);

            // Create a new ComboBox control for font selection
            ComboBox fontSelectionComboBox = new ComboBox();
            fontSelectionComboBox.Location = new Point(10, 10);
            fontSelectionComboBox.Width = 220;

            // Populate the ComboBox with the list of installed fonts
            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                fontSelectionComboBox.Items.Add(fontFamily.Name);
            }

            // Add the ComboBox to the form
            fontSelectionForm.Controls.Add(fontSelectionComboBox);

            // Show the form and wait for the user to make a selection
            DialogResult result = fontSelectionForm.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                string selectedFontName = fontSelectionComboBox.SelectedItem.ToString();
                FontStyle selectedFontStyle = FontStyle.Regular; // default to Regular if no other style is specified
                if (fileTextOutput.SelectionFont != null)
                {
                    selectedFontStyle = fileTextOutput.SelectionFont.Style; // use the current style if available
                }
                Font selectedFont = new Font(selectedFontName, fileTextOutput.Font.Size, selectedFontStyle);
                fileTextOutput.SelectionFont = new Font(selectedFont, selectedFontStyle);
                //FontSettingsManager.SaveFontSettings(selectedFontStyle);
                SaveFontNameToJson(selectedFontName);
            }
        }
        public class FontSettings
        {
            public FontSettings(FontStyle fontStyle)
            {
                FontStyle = fontStyle;
            }

            public FontStyle FontStyle { get; }
        }

        public static class FontSettingsManager
        {
            public static void SaveFontSettings(FontStyle fontStyle)
            {
                try
                {
                    var fontSettings = new FontSettings(fontStyle);
                    string json = JsonConvert.SerializeObject(fontSettings);
                    // Write the JSON string to a file
                    string filePath = Path.Combine(Application.StartupPath, "fontStyle.json");
                    File.WriteAllText(filePath, json);
                    Console.WriteLine("FontSettings saved to file: " + filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while saving font settings: " + ex.Message);
                }
            }

            public static FontStyle LoadFontSettings()
            {
                string filePath = Path.Combine(Application.StartupPath, "fontStyle.json");

                if (!File.Exists(filePath))
                {
                    return FontStyle.Regular; // default to Regular if file does not exist
                }

                string jsonString = File.ReadAllText(filePath);
                FontSettings fontSettings = JsonConvert.DeserializeObject<FontSettings>(jsonString);
                return fontSettings.FontStyle;
            }
        }
        public void SaveFontNameToJson(string fontName)
        {
            try
            {
                // Create a new anonymous object to hold the fontName string
                var font = new { FontName = fontName };

                // Serialize the anonymous object to JSON
                string json = JsonConvert.SerializeObject(font, Formatting.Indented);

                // Combine the application directory path with the file name to get the full file path
                string filePath = Path.Combine(Application.StartupPath, "fontName.json");

                // Write the JSON to the specified file
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // Handle any exception that might occur and display a message box to the user
                MessageBox.Show("Error saving font name: " + ex.Message);
            }
        }
        public void LoadFontNameFromJson()
        {
            try
            {
                // Combine the application directory path with the file name to get the full file path
                string filePath = Path.Combine(Application.StartupPath, "fontName.json");

                // Read the contents of the JSON file
                string json = File.ReadAllText(filePath);

                // Deserialize the JSON to an anonymous object with a FontName property
                var font = JsonConvert.DeserializeAnonymousType(json, new { FontName = "" });

                // Create a new Font object using the loaded fontName
                Font newFont = new Font(font.FontName, fileTextOutput.Font.Size);

                // Set the font of fileTextOutput to the new Font object
                fileTextOutput.Font = newFont;
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception
                MessageBox.Show("File not found.");
            }
            catch (JsonReaderException)
            {
                // Handle invalid JSON exception
                MessageBox.Show("Invalid JSON.");
            }
        }
        private bool isPaperSize = false;
        private bool isPaperSizeToggled = false;
        private bool resizeableClient = true;
        private void AdjustRichTextBoxToPaperSize(RichTextBox richTextBox)
        {
            // Create a PrintDocument object to get the paper size
            PrintDocument printDoc = new PrintDocument();
            PaperSize paperSize = printDoc.DefaultPageSettings.PaperSize;

            // Convert to paper size
            int paperWidth = (int)(paperSize.Width);
            int paperHeight = (int)(paperSize.Height);

            // Set the RichTextBox size to match the paper size
            richTextBox.Width = paperWidth;
            richTextBox.Height = paperHeight;

            // Set a margin on all sides
            richTextBox.Margin = new Padding(50);

            // Anchor the RichTextBox to the top and left of the form
            richTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Adjust the client size of the form to fit the RichTextBox
            this.ClientSize = new Size(richTextBox.Width + richTextBox.Margin.Horizontal, richTextBox.Height + richTextBox.Margin.Vertical);

            isPaperSize = true;
        }
        private void UndoAdjustRichTextBoxToPaperSize(RichTextBox richTextBox)
        {
            if(isPaperSize == true)
            {
                richTextBox.Width = richTextBox.Width + 100;
                // Set the Anchor property to anchor the control to the sides of its container
                fileTextOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                isPaperSize = false;
            }

        }
        private void DisableClientResizing(RichTextBox richTextBox)
        {
            // Set the form's border style to a fixed style
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void EnableClientResizing(RichTextBox richTextBox)
        {
            // Set the form's border style to a fixed style
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }
        private void HighlightSyntax()
        {
            //unity
            // Define a regular expression for Unity functions
            string unityFunctionPattern = @"\b(Start|Update|FixedUpdate|LateUpdate|OnCollisionEnter|OnCollisionExit|OnTriggerEnter|OnTriggerExit)\b";
            string unityTypePattern = @"\b(Transform|GameObject|Vector[234]|Quaternion|Color)\b";
            string unityVariablePattern = @"\b(transform|gameObject|renderer|position|rotation|scale|color)\b";

            // Define the colors to use for Unity syntax elements
            Color unityTypeColor = Color.GreenYellow;
            Color unityVariableColor = Color.Teal;
            Color unityFunctionColor = Color.Orange;


            // Define regular expressions for different syntax elements
            string keywordPattern = @"\b(if|else|for|while|do|switch|case|default|break|continue|return)\b";
            string variablePattern = @"\b[A-Za-z_][A-Za-z0-9_]*\b";
            string stringPattern = @"""[^""\\]*(?:\\.[^""\\]*)*""|'[^'\\]*(?:\\.[^'\\]*)*'";
            string numberPattern = @"\b\d+(\.\d+)?\b";
            string commentPattern = @"^\s*(//|#).*$|/\*(.|\n)*?\*/";
            string indentPattern = @"^[\t ]+";
            string functionPattern = @"[A-Za-z_][A-Za-z0-9_]*\s*\(";

            // Define the colors to use for each syntax element
            Color keywordColor = Color.BlueViolet;
            Color variableColor = Color.DarkKhaki;
            Color stringColor = Color.OrangeRed;
            Color numberColor = Color.MediumPurple;
            Color commentColor = Color.Green;
            Color indentColor = Color.LightGray;
            Color functionColor = Color.DarkCyan;

            //standard variables
            string standardVariablePattern = @"\b(int|bool|float|double|string|static|constant|private|public|long|void|get|set|using|protected)\b";
            Color standardVariableColor = Color.LightBlue;


            // Apply the syntax highlighting to the entire text in the RichTextBox
            int start = fileTextOutput.SelectionStart;
            foreach (Match match in Regex.Matches(fileTextOutput.Text, variablePattern, RegexOptions.IgnoreCase))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = variableColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, stringPattern, RegexOptions.IgnoreCase))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = stringColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, numberPattern, RegexOptions.IgnoreCase))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = numberColor;
            }

            foreach (Match match in Regex.Matches(fileTextOutput.Text, indentPattern, RegexOptions.Multiline))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = indentColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, functionPattern, RegexOptions.Multiline))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = functionColor;
            }
            // Apply the syntax highlighting for Unity 
            foreach (Match match in Regex.Matches(fileTextOutput.Text, unityTypePattern))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = unityTypeColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, unityVariablePattern))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = unityVariableColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, unityFunctionPattern))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = unityFunctionColor;
            }
            //standard variable patterns
            foreach (Match match in Regex.Matches(fileTextOutput.Text, standardVariablePattern))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = standardVariableColor;
            }

            //(last)
            foreach (Match match in Regex.Matches(fileTextOutput.Text, keywordPattern, RegexOptions.IgnoreCase))
            {
                fileTextOutput.SelectionStart = match.Index;
                fileTextOutput.SelectionLength = match.Length;
                fileTextOutput.SelectionColor = keywordColor;
            }
            foreach (Match match in Regex.Matches(fileTextOutput.Text, commentPattern, RegexOptions.Multiline))
            {
                int lineIndex = fileTextOutput.GetLineFromCharIndex(match.Index);
                int lineStart = fileTextOutput.GetFirstCharIndexFromLine(lineIndex);
                int lineEnd = fileTextOutput.GetFirstCharIndexFromLine(lineIndex + 1);
                if (lineEnd < 0)
                {
                    lineEnd = fileTextOutput.Text.Length;
                }
                fileTextOutput.SelectionStart = lineStart;
                fileTextOutput.SelectionLength = lineEnd - lineStart;
                fileTextOutput.SelectionColor = commentColor;
            }

            fileTextOutput.SelectionStart = start;
            fileTextOutput.SelectionLength = 0;
            fileTextOutput.SelectionColor = textColor;

        }
        private void FindNearest(string searchTerm)
        {
            if(find_textBox1.Text != "")
            {
                // Find the next occurrence of the search term
                int startIndex = fileTextOutput.SelectionStart + fileTextOutput.SelectionLength;
                int nextIndex = fileTextOutput.Find(searchTerm, startIndex, -1, RichTextBoxFinds.None);

                // Find the previous occurrence of the search term
                int length = fileTextOutput.SelectionStart;
                int prevIndex = fileTextOutput.Find(searchTerm, 0, length, RichTextBoxFinds.Reverse);

                // Determine which occurrence is closest to the current selection
                int index;
                if (nextIndex == -1 && prevIndex == -1)
                {
                    MessageBox.Show("No matches found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (nextIndex == -1)
                {
                    index = prevIndex;
                }
                else if (prevIndex == -1)
                {
                    index = nextIndex;
                }
                else
                {
                    int nextDistance = nextIndex - startIndex;
                    int prevDistance = startIndex - prevIndex;
                    index = (nextDistance <= prevDistance) ? nextIndex : prevIndex;
                }

                // Select and scroll to the closest occurrence of the search term
                fileTextOutput.Select(index, searchTerm.Length);
                fileTextOutput.ScrollToCaret();
                fileTextOutput.Select();
            }
       
        }
        private void FindNext(string searchTerm)
        {
            if (find_textBox1.Text != "")
            {
                int startIndex = fileTextOutput.SelectionStart + fileTextOutput.SelectionLength;
                int index = fileTextOutput.Find(searchTerm, startIndex, -1, RichTextBoxFinds.None);

                if (index != -1)
                {
                    fileTextOutput.Select(index, searchTerm.Length);
                    fileTextOutput.ScrollToCaret();
                }
                else
                {
                    MessageBox.Show("No matches found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                fileTextOutput.Select();
            }

        }

        private void FindLast(string searchTerm)
        {
            if (find_textBox1.Text != "")
            {
                int startIndex = 0;
                int length = fileTextOutput.SelectionStart;
                int index = fileTextOutput.Find(searchTerm, startIndex, length, RichTextBoxFinds.Reverse);

                if (index != -1)
                {
                    fileTextOutput.Select(index, searchTerm.Length);
                    fileTextOutput.ScrollToCaret();
                }
                else
                {
                    MessageBox.Show("No matches found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                fileTextOutput.Select();
            }

        }
        
        private int searchIndex = 0;
        private void ShowFindDialog()
        {
            // Create a new form
            Form findForm = new Form();
            findForm.Text = "Find";
            findForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            findForm.StartPosition = FormStartPosition.CenterScreen;

            // Create a label for the textbox
            Label findLabel = new Label();
            findLabel.Text = "Enter text to find:";
            findLabel.Location = new Point(10, 10);
            findLabel.AutoSize = true;

            // Create a textbox for user input
            TextBox findTextBox = new TextBox();
            findTextBox.Location = new Point(10, 30);
            findTextBox.Width = 200;

            // Create a button to initiate the search
            Button findButton = new Button();
            findButton.Text = "Find";
            findButton.Location = new Point(220, 30);
            findButton.Click += (sender, args) =>
            {
                // Get the user input
                string searchText = findTextBox.Text;

                // Display the search results
                if (searchIndex != -1)
                {
                    // Search for the text in the RichTextBox starting from the last found index
                    searchIndex = fileTextOutput.Find(searchText, searchIndex, RichTextBoxFinds.None);
                    // Select the found text
                    fileTextOutput.Select(searchIndex, searchText.Length);

                    // Scroll the selected text into view
                    fileTextOutput.ScrollToCaret();

                    // Increment the search index
                    searchIndex += searchText.Length;
                    MessageBox.Show("Text found at index " + searchIndex);
                }
                else
                {
                    MessageBox.Show("Text not found.");
                }

                // Move the text cursor to the RichTextBox
                fileTextOutput.Select();
            };
            // Add the controls to the form
            findForm.Controls.Add(findLabel);
            findForm.Controls.Add(findTextBox);
            findForm.Controls.Add(findButton);

            // Display the form
            findForm.ShowDialog();
        }

        private void SavedText()
        {
            this.Text = "Notepad On CrAcK";
            modified_label2.Text = "SAVED";
            modified_label2.ForeColor = savetextColor;
        }
        private void UnsavedText()
        {
            this.Text = "* Notepad On CrAcK";
            modified_label2.Text = "*unsaved*";
            modified_label2.ForeColor = unsavedtextColor;

        }
        private void HideStatusBar()
        {
            linecol_label.Visible = false;
            wordwrap_label2.Visible = false;
            wordcount_label.Visible = false;
            ver_label.Visible = false;

            // Calculate the height of the status bar based on the height of the labels
            int statusBarHeight = linecol_label.Height + wordwrap_label2.Height + wordcount_label.Height + ver_label.Height;

            // Adjust the height of the fileTextOutput control to fill the remaining space
            fileTextOutput.Height = this.ClientSize.Height - statusBarHeight - 15;

            // Anchor the fileTextOutput control to the top, bottom, left, and right sides of its container
            //fileTextOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }
        private void ShowStatusBar()
        {
            linecol_label.Visible = true;
            wordwrap_label2.Visible = true;
            wordcount_label.Visible = true;
            ver_label.Visible = true;

            // Calculate the height of the status bar based on the height of the labels
            int statusBarHeight = linecol_label.Height + wordwrap_label2.Height + wordcount_label.Height + ver_label.Height;

            // Adjust the height of the fileTextOutput control to fill the remaining space
            fileTextOutput.Height = this.ClientSize.Height - statusBarHeight - 40;

            // Reset the Dock property of the fileTextOutput control to its default value
            //fileTextOutput.Dock = DockStyle.None;

            // Reset the Anchor property of the fileTextOutput control to its default value
            //fileTextOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Call the PerformLayout method of the form to rearrange the controls
            this.PerformLayout();
        }

        //DOESN'T WORK UNLESS WE UPGRADE TO .NET :C
        private void OpenNotepadOnCrackLink()
        {    
            // Specify the URL to open
            string url = "https://manbearpigman.itch.io/notepad-on-crack";

            // Start a new process to open the default browser and navigate to the URL
            System.Diagnostics.Process.Start(url);
        }
        //INPUTS ***********************************************************************************************************************************************************************************************************************************************************
        //**********************************************************************************************************************************************************************************************************************************************************************
        //**********************************************************************************************************************************************************************************************************************************************************************

        //on form open
        private static string finalReadString;
        private void ReadStringBtn_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(this.outputLabel.Text); // this is the path
            finalReadString = reader.ReadToEnd();
            fileTextOutput.Text = finalReadString;
            reader.Close();
            SavedText();
            ClearUndoStack();
        }
        //on form close
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormSize();

            if (modified_label2.Text != "SAVED")
            {
                DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (this.outputLabel.Text != null)
                    {
                        SaveFile(this.outputLabel.Text);
                    }

                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Cancel the form closing event if the user clicks Cancel
                }
            }
        }
        private void inputBox_TextChanged_1(object sender, EventArgs e)
        {
            this.outputLabel.Text = this.inputBox.Text;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFile(this.outputLabel.Text);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void panelBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBackgroundColor();
        }

        private void panelTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAppTextColor();
        }

        private void textBoxBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTxtBoxBackgroundColor();
        }

        private void textBoxTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorTextChangeFunction();
        }

        private void spellCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunSpellCheck();
        }



        private void wordWrapOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWordWrap(fileTextOutput);
            if (wordWrapValue == 1)
            {
                wordwrap_label2.Text = "Wordwrap On";
            }
            if (wordWrapValue == 0)
            {
                wordwrap_label2.Text = "Wordwrap Off";
            }
        }

        private void boldTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeTextBold(fileTextOutput);
        }

        //these two are swapped!
        private void setFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeTextNotBold(fileTextOutput);
        }
        //these two are swapped!
        private void unBoldTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontSize(fileTextOutput);
        }

        private void highligherTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHighlighterColor();
        }


        private bool blockLeave = false;
        private void fileTextOutput_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the user pressed the "Control" and "S" keys together
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Prevent the default behavior of the keys (saving the file)
                e.SuppressKeyPress = true;

                // Call the SaveFile function to save the file
                SaveFile(outputLabel.Text);
                SavedText();
            }

            //move cursor to search bar
            if (e.Control && e.KeyCode == Keys.F)
            {
                find_textBox1.Focus();
                find_textBox1.SelectionStart = find_textBox1.TextLength;
            }

            if (e.KeyCode == Keys.Tab)
            {
                blockLeave = true;
            }
        }

        //on keyboard input
        private void fileTextOutput_SelectionChanged(object sender, EventArgs e)
        {
            AddToUndoStack(fileTextOutput.Text);
            UpdateCursorPositionLabel();
            UpdateWordCountLabel();
        }
        //TEXT BOX TEXT CHANGED
        private void fileTextOutput_TextChanged(object sender, EventArgs e)
        {
            //IsFileTextChanged();
            if (!isLoading)
            {
                UnsavedText();
            }
        }

        private void undo_button1_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redo_button1_Click(object sender, EventArgs e)
        {
            Redo();
        }


        private void replaceTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceAllText();
        }

        private void replaceEndOfEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceAtEndOfEveryLine();
        }

        private void replaceStartOfEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceAtStartOfEveryLine();
        }

        private void removeLastCharacterFromEndOfEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLastCharacterFromEveryLine();
        }

        private void removeLastCharacterFromStartOfEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveFirstCharacterFromEveryLine();
        }

        private void removeLastLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLastLineFromTextBox();
        }

        private void addIndentEveryXLinesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddIndentationsWithPopup();
        }

        private void addToEndOfEveryLineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddEndEveryLine();
        }

        private void setFontStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFontSelectionForm();
        }

 

        private void fileTextOutput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem menuItemCut = new ToolStripMenuItem("Cut");
                ToolStripMenuItem menuItemCopy = new ToolStripMenuItem("Copy");
                ToolStripMenuItem menuItemPaste = new ToolStripMenuItem("Paste");

                ToolStripMenuItem menuItemSelectAll = new ToolStripMenuItem("Select All");

                menuItemCut.Click += new EventHandler(menuItemCut_Click);
                menuItemCopy.Click += new EventHandler(menuItemCopy_Click);
                menuItemPaste.Click += new EventHandler(menuItemPaste_Click);

                menuItemSelectAll.Click += new EventHandler(menuItemSelectAll_Click);

                contextMenuStrip.Items.Add(menuItemCut);
                contextMenuStrip.Items.Add(menuItemCopy);
                contextMenuStrip.Items.Add(menuItemPaste);
                contextMenuStrip.Items.Add(menuItemSelectAll);

                fileTextOutput.ContextMenuStrip = contextMenuStrip;
            }
        }
        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            if (fileTextOutput.SelectionLength > 0)
            {
                fileTextOutput.Copy();
            }
        }
        private void menuItemCut_Click(object sender, EventArgs e)
        {
            if (fileTextOutput.SelectionLength > 0)
            {
                fileTextOutput.Cut();
            }
        }
        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            fileTextOutput.Paste();
        }
        private void menuItemSelectAll_Click(object sender, EventArgs e)
        {
            fileTextOutput.SelectAll();
        }

 
        private string searchForText;
        private void find_textBox1_TextChanged(object sender, EventArgs e)
        {
            searchForText = find_textBox1.Text;
        }

        private void findNext_button1_Click(object sender, EventArgs e)
        {
            FindNext(searchForText);
        }

        private void findLast_button1_Click(object sender, EventArgs e)
        {
            FindLast(searchForText);
        }


        private void highlightCodeSyntaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighlightSyntax();
        }

        private void find_textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindNearest(searchForText);
            }
        }

        private void addToStartOfEveryLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStartEveryLine();
        }

        private void toggleStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStatusBar();

            if (statusBarValue == 1)//status bar off
            {
                HideStatusBar();
            }
            if (statusBarValue == 0)//status bar on
            {
                ShowStatusBar();
            }
        }


        private void togglePageClientSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isPaperSizeToggled = !isPaperSizeToggled;
            if(isPaperSizeToggled)
            {
                AdjustRichTextBoxToPaperSize(fileTextOutput);
            }
            else
            {
                UndoAdjustRichTextBoxToPaperSize(fileTextOutput);
            }
        }

        private void toggleClientScalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resizeableClient = !resizeableClient;
            if (resizeableClient)
            {
                EnableClientResizing(fileTextOutput);
            }
            else
            {
                DisableClientResizing(fileTextOutput);

            }
        }

        private void fileTextOutput_Leave(object sender, EventArgs e)
        {
  
        }

        private void fileTextOutput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void findAndHighlightTextToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FindChangeTextColor();
        }

        private void addNumberToEveryLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLineNumberingAll();
        }

        private void modified_label2_Click(object sender, EventArgs e)
        {

        }

        private void sAVEDTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorSaveTextChangeFunction();
        }

        private void uNSAVEDTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorUnsavedTextChangeFunction();
        }

        private void addNumberToEveryUsedLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLineNumberingOnlyTextLines();
        }
    }

}


