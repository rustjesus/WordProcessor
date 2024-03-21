
namespace WordProcessor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.outputLabel = new System.Windows.Forms.Label();
            this.ReadStringBtn = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.filetoolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.panelBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTextColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxTextColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highligherTextColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightCodeSyntaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findAndHighlightTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.spellCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boldTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFontSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unBoldTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFontStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.togglePageClientSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleClientScalingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Replace_toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.replaceTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceEndOfEachLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceStartOfEachLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remove_toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLastLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Add_toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.addIndentEveryXLinesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToEndOfEveryLineToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToStartOfEveryLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.View_toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toggleStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modified_label2 = new System.Windows.Forms.Label();
            this.undo_button1 = new System.Windows.Forms.Button();
            this.redo_button1 = new System.Windows.Forms.Button();
            this.linecol_label = new System.Windows.Forms.Label();
            this.ver_label = new System.Windows.Forms.Label();
            this.wordwrap_label2 = new System.Windows.Forms.Label();
            this.wordcount_label = new System.Windows.Forms.Label();
            this.fileTextOutput = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.find_textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.findNext_button1 = new System.Windows.Forms.Button();
            this.findLast_button1 = new System.Windows.Forms.Button();
            this.addNumberToEveryLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 57);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(60, 15);
            this.outputLabel.TabIndex = 0;
            this.outputLabel.Text = "(File Path)";
            // 
            // ReadStringBtn
            // 
            this.ReadStringBtn.Location = new System.Drawing.Point(68, 31);
            this.ReadStringBtn.Name = "ReadStringBtn";
            this.ReadStringBtn.Size = new System.Drawing.Size(90, 23);
            this.ReadStringBtn.TabIndex = 2;
            this.ReadStringBtn.Text = "Re-Open File";
            this.ReadStringBtn.UseVisualStyleBackColor = true;
            this.ReadStringBtn.Click += new System.EventHandler(this.ReadStringBtn_Click);
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(272, 31);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(75, 23);
            this.inputBox.TabIndex = 3;
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged_1);
            // 
            // filetoolStripDropDownButton1
            // 
            this.filetoolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.filetoolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1});
            this.filetoolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("filetoolStripDropDownButton1.Image")));
            this.filetoolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filetoolStripDropDownButton1.Name = "filetoolStripDropDownButton1";
            this.filetoolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.filetoolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem1.Text = "Save as";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelBackgroundColorToolStripMenuItem,
            this.panelTextColorToolStripMenuItem,
            this.textBoxBackgroundColorToolStripMenuItem,
            this.textBoxTextColorToolStripMenuItem,
            this.highligherTextColorToolStripMenuItem,
            this.highlightCodeSyntaxToolStripMenuItem,
            this.findAndHighlightTextToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripDropDownButton1.Text = "Color";
            // 
            // panelBackgroundColorToolStripMenuItem
            // 
            this.panelBackgroundColorToolStripMenuItem.Name = "panelBackgroundColorToolStripMenuItem";
            this.panelBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.panelBackgroundColorToolStripMenuItem.Text = "Panel: Background Color";
            this.panelBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.panelBackgroundColorToolStripMenuItem_Click);
            // 
            // panelTextColorToolStripMenuItem
            // 
            this.panelTextColorToolStripMenuItem.Name = "panelTextColorToolStripMenuItem";
            this.panelTextColorToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.panelTextColorToolStripMenuItem.Text = "Panel: Text Color";
            this.panelTextColorToolStripMenuItem.Click += new System.EventHandler(this.panelTextColorToolStripMenuItem_Click);
            // 
            // textBoxBackgroundColorToolStripMenuItem
            // 
            this.textBoxBackgroundColorToolStripMenuItem.Name = "textBoxBackgroundColorToolStripMenuItem";
            this.textBoxBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.textBoxBackgroundColorToolStripMenuItem.Text = "Text Box: Background Color";
            this.textBoxBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.textBoxBackgroundColorToolStripMenuItem_Click);
            // 
            // textBoxTextColorToolStripMenuItem
            // 
            this.textBoxTextColorToolStripMenuItem.Name = "textBoxTextColorToolStripMenuItem";
            this.textBoxTextColorToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.textBoxTextColorToolStripMenuItem.Text = "Text Box: Text Color";
            this.textBoxTextColorToolStripMenuItem.Click += new System.EventHandler(this.textBoxTextColorToolStripMenuItem_Click);
            // 
            // highligherTextColorToolStripMenuItem
            // 
            this.highligherTextColorToolStripMenuItem.Name = "highligherTextColorToolStripMenuItem";
            this.highligherTextColorToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.highligherTextColorToolStripMenuItem.Text = "Find Text: (Highlight) Text Color";
            this.highligherTextColorToolStripMenuItem.Click += new System.EventHandler(this.highligherTextColorToolStripMenuItem_Click);
            // 
            // highlightCodeSyntaxToolStripMenuItem
            // 
            this.highlightCodeSyntaxToolStripMenuItem.Name = "highlightCodeSyntaxToolStripMenuItem";
            this.highlightCodeSyntaxToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.highlightCodeSyntaxToolStripMenuItem.Text = "Highlight Code Syntax";
            this.highlightCodeSyntaxToolStripMenuItem.Click += new System.EventHandler(this.highlightCodeSyntaxToolStripMenuItem_Click);
            // 
            // findAndHighlightTextToolStripMenuItem
            // 
            this.findAndHighlightTextToolStripMenuItem.Name = "findAndHighlightTextToolStripMenuItem";
            this.findAndHighlightTextToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.findAndHighlightTextToolStripMenuItem.Text = "Find And Highlight Text";
            this.findAndHighlightTextToolStripMenuItem.Click += new System.EventHandler(this.findAndHighlightTextToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spellCheckToolStripMenuItem,
            this.wordWrapOnToolStripMenuItem,
            this.boldTextToolStripMenuItem,
            this.setFontSizeToolStripMenuItem,
            this.unBoldTextToolStripMenuItem,
            this.setFontStyleToolStripMenuItem,
            this.togglePageClientSizeToolStripMenuItem,
            this.toggleClientScalingToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(47, 22);
            this.toolStripDropDownButton2.Text = "Tools";
            // 
            // spellCheckToolStripMenuItem
            // 
            this.spellCheckToolStripMenuItem.Name = "spellCheckToolStripMenuItem";
            this.spellCheckToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.spellCheckToolStripMenuItem.Text = "Spell Check";
            this.spellCheckToolStripMenuItem.Click += new System.EventHandler(this.spellCheckToolStripMenuItem_Click);
            // 
            // wordWrapOnToolStripMenuItem
            // 
            this.wordWrapOnToolStripMenuItem.Name = "wordWrapOnToolStripMenuItem";
            this.wordWrapOnToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.wordWrapOnToolStripMenuItem.Text = "Word Wrap";
            this.wordWrapOnToolStripMenuItem.Click += new System.EventHandler(this.wordWrapOnToolStripMenuItem_Click);
            // 
            // boldTextToolStripMenuItem
            // 
            this.boldTextToolStripMenuItem.Name = "boldTextToolStripMenuItem";
            this.boldTextToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.boldTextToolStripMenuItem.Text = "Bold Text";
            this.boldTextToolStripMenuItem.Click += new System.EventHandler(this.boldTextToolStripMenuItem_Click);
            // 
            // setFontSizeToolStripMenuItem
            // 
            this.setFontSizeToolStripMenuItem.Name = "setFontSizeToolStripMenuItem";
            this.setFontSizeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setFontSizeToolStripMenuItem.Text = "Un-Bold Text";
            this.setFontSizeToolStripMenuItem.Click += new System.EventHandler(this.setFontSizeToolStripMenuItem_Click);
            // 
            // unBoldTextToolStripMenuItem
            // 
            this.unBoldTextToolStripMenuItem.Name = "unBoldTextToolStripMenuItem";
            this.unBoldTextToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.unBoldTextToolStripMenuItem.Text = "Set Font Size";
            this.unBoldTextToolStripMenuItem.Click += new System.EventHandler(this.unBoldTextToolStripMenuItem_Click);
            // 
            // setFontStyleToolStripMenuItem
            // 
            this.setFontStyleToolStripMenuItem.Name = "setFontStyleToolStripMenuItem";
            this.setFontStyleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setFontStyleToolStripMenuItem.Text = "Set Font Style";
            this.setFontStyleToolStripMenuItem.Click += new System.EventHandler(this.setFontStyleToolStripMenuItem_Click);
            // 
            // togglePageClientSizeToolStripMenuItem
            // 
            this.togglePageClientSizeToolStripMenuItem.Name = "togglePageClientSizeToolStripMenuItem";
            this.togglePageClientSizeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.togglePageClientSizeToolStripMenuItem.Text = "Toggle Page/Client Size";
            this.togglePageClientSizeToolStripMenuItem.Click += new System.EventHandler(this.togglePageClientSizeToolStripMenuItem_Click);
            // 
            // toggleClientScalingToolStripMenuItem
            // 
            this.toggleClientScalingToolStripMenuItem.Name = "toggleClientScalingToolStripMenuItem";
            this.toggleClientScalingToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.toggleClientScalingToolStripMenuItem.Text = "Toggle Client Scaling";
            this.toggleClientScalingToolStripMenuItem.Click += new System.EventHandler(this.toggleClientScalingToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filetoolStripDropDownButton1,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.Replace_toolStripDropDownButton3,
            this.remove_toolStripDropDownButton3,
            this.Add_toolStripDropDownButton3,
            this.View_toolStripDropDownButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1137, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Replace_toolStripDropDownButton3
            // 
            this.Replace_toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Replace_toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceTextToolStripMenuItem,
            this.replaceEndOfEachLineToolStripMenuItem,
            this.replaceStartOfEachLineToolStripMenuItem});
            this.Replace_toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("Replace_toolStripDropDownButton3.Image")));
            this.Replace_toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Replace_toolStripDropDownButton3.Name = "Replace_toolStripDropDownButton3";
            this.Replace_toolStripDropDownButton3.Size = new System.Drawing.Size(61, 22);
            this.Replace_toolStripDropDownButton3.Text = "Replace";
            // 
            // replaceTextToolStripMenuItem
            // 
            this.replaceTextToolStripMenuItem.Name = "replaceTextToolStripMenuItem";
            this.replaceTextToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.replaceTextToolStripMenuItem.Text = "Replace Text";
            this.replaceTextToolStripMenuItem.Click += new System.EventHandler(this.replaceTextToolStripMenuItem_Click);
            // 
            // replaceEndOfEachLineToolStripMenuItem
            // 
            this.replaceEndOfEachLineToolStripMenuItem.Name = "replaceEndOfEachLineToolStripMenuItem";
            this.replaceEndOfEachLineToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.replaceEndOfEachLineToolStripMenuItem.Text = "Replace End Of Each Line";
            this.replaceEndOfEachLineToolStripMenuItem.Click += new System.EventHandler(this.replaceEndOfEachLineToolStripMenuItem_Click);
            // 
            // replaceStartOfEachLineToolStripMenuItem
            // 
            this.replaceStartOfEachLineToolStripMenuItem.Name = "replaceStartOfEachLineToolStripMenuItem";
            this.replaceStartOfEachLineToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.replaceStartOfEachLineToolStripMenuItem.Text = "Replace Start Of Each Line";
            this.replaceStartOfEachLineToolStripMenuItem.Click += new System.EventHandler(this.replaceStartOfEachLineToolStripMenuItem_Click);
            // 
            // remove_toolStripDropDownButton3
            // 
            this.remove_toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.remove_toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem,
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem,
            this.removeLastLineToolStripMenuItem});
            this.remove_toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("remove_toolStripDropDownButton3.Image")));
            this.remove_toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.remove_toolStripDropDownButton3.Name = "remove_toolStripDropDownButton3";
            this.remove_toolStripDropDownButton3.Size = new System.Drawing.Size(63, 22);
            this.remove_toolStripDropDownButton3.Text = "Remove";
            // 
            // removeLastCharacterFromEndOfEachLineToolStripMenuItem
            // 
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem.Name = "removeLastCharacterFromEndOfEachLineToolStripMenuItem";
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem.Text = "Remove Last Character From End Of Each Line";
            this.removeLastCharacterFromEndOfEachLineToolStripMenuItem.Click += new System.EventHandler(this.removeLastCharacterFromEndOfEachLineToolStripMenuItem_Click);
            // 
            // removeLastCharacterFromStartOfEachLineToolStripMenuItem
            // 
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem.Name = "removeLastCharacterFromStartOfEachLineToolStripMenuItem";
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem.Text = "Remove Last Character From Start Of Each Line";
            this.removeLastCharacterFromStartOfEachLineToolStripMenuItem.Click += new System.EventHandler(this.removeLastCharacterFromStartOfEachLineToolStripMenuItem_Click);
            // 
            // removeLastLineToolStripMenuItem
            // 
            this.removeLastLineToolStripMenuItem.Name = "removeLastLineToolStripMenuItem";
            this.removeLastLineToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.removeLastLineToolStripMenuItem.Text = "Remove Last Line";
            this.removeLastLineToolStripMenuItem.Click += new System.EventHandler(this.removeLastLineToolStripMenuItem_Click);
            // 
            // Add_toolStripDropDownButton3
            // 
            this.Add_toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Add_toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addIndentEveryXLinesToolStripMenuItem1,
            this.addToEndOfEveryLineToolStripMenuItem1,
            this.addToStartOfEveryLineToolStripMenuItem,
            this.addNumberToEveryLineToolStripMenuItem});
            this.Add_toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("Add_toolStripDropDownButton3.Image")));
            this.Add_toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_toolStripDropDownButton3.Name = "Add_toolStripDropDownButton3";
            this.Add_toolStripDropDownButton3.Size = new System.Drawing.Size(42, 22);
            this.Add_toolStripDropDownButton3.Text = "Add";
            // 
            // addIndentEveryXLinesToolStripMenuItem1
            // 
            this.addIndentEveryXLinesToolStripMenuItem1.Name = "addIndentEveryXLinesToolStripMenuItem1";
            this.addIndentEveryXLinesToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.addIndentEveryXLinesToolStripMenuItem1.Text = "Add Indent Every X Lines";
            this.addIndentEveryXLinesToolStripMenuItem1.Click += new System.EventHandler(this.addIndentEveryXLinesToolStripMenuItem1_Click);
            // 
            // addToEndOfEveryLineToolStripMenuItem1
            // 
            this.addToEndOfEveryLineToolStripMenuItem1.Name = "addToEndOfEveryLineToolStripMenuItem1";
            this.addToEndOfEveryLineToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.addToEndOfEveryLineToolStripMenuItem1.Text = "Add To End Of Every Line";
            this.addToEndOfEveryLineToolStripMenuItem1.Click += new System.EventHandler(this.addToEndOfEveryLineToolStripMenuItem1_Click);
            // 
            // addToStartOfEveryLineToolStripMenuItem
            // 
            this.addToStartOfEveryLineToolStripMenuItem.Name = "addToStartOfEveryLineToolStripMenuItem";
            this.addToStartOfEveryLineToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addToStartOfEveryLineToolStripMenuItem.Text = "Add To Start Of Every Line";
            this.addToStartOfEveryLineToolStripMenuItem.Click += new System.EventHandler(this.addToStartOfEveryLineToolStripMenuItem_Click);
            // 
            // View_toolStripDropDownButton3
            // 
            this.View_toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.View_toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleStatusBarToolStripMenuItem});
            this.View_toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("View_toolStripDropDownButton3.Image")));
            this.View_toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_toolStripDropDownButton3.Name = "View_toolStripDropDownButton3";
            this.View_toolStripDropDownButton3.Size = new System.Drawing.Size(45, 22);
            this.View_toolStripDropDownButton3.Text = "View";
            // 
            // toggleStatusBarToolStripMenuItem
            // 
            this.toggleStatusBarToolStripMenuItem.Name = "toggleStatusBarToolStripMenuItem";
            this.toggleStatusBarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleStatusBarToolStripMenuItem.Text = "Toggle Status Bar";
            this.toggleStatusBarToolStripMenuItem.Click += new System.EventHandler(this.toggleStatusBarToolStripMenuItem_Click);
            // 
            // modified_label2
            // 
            this.modified_label2.AutoSize = true;
            this.modified_label2.Location = new System.Drawing.Point(0, 25);
            this.modified_label2.Name = "modified_label2";
            this.modified_label2.Size = new System.Drawing.Size(56, 15);
            this.modified_label2.TabIndex = 11;
            this.modified_label2.Text = "*unsaved";
            // 
            // undo_button1
            // 
            this.undo_button1.Location = new System.Drawing.Point(741, 31);
            this.undo_button1.Name = "undo_button1";
            this.undo_button1.Size = new System.Drawing.Size(75, 23);
            this.undo_button1.TabIndex = 12;
            this.undo_button1.Text = "Undo";
            this.undo_button1.UseVisualStyleBackColor = true;
            this.undo_button1.Click += new System.EventHandler(this.undo_button1_Click);
            // 
            // redo_button1
            // 
            this.redo_button1.Location = new System.Drawing.Point(660, 31);
            this.redo_button1.Name = "redo_button1";
            this.redo_button1.Size = new System.Drawing.Size(75, 23);
            this.redo_button1.TabIndex = 13;
            this.redo_button1.Text = "Redo";
            this.redo_button1.UseVisualStyleBackColor = true;
            this.redo_button1.Click += new System.EventHandler(this.redo_button1_Click);
            // 
            // linecol_label
            // 
            this.linecol_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linecol_label.AutoSize = true;
            this.linecol_label.Location = new System.Drawing.Point(4, 692);
            this.linecol_label.Name = "linecol_label";
            this.linecol_label.Size = new System.Drawing.Size(52, 15);
            this.linecol_label.TabIndex = 14;
            this.linecol_label.Text = "Line/Col";
            // 
            // ver_label
            // 
            this.ver_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ver_label.AutoSize = true;
            this.ver_label.Location = new System.Drawing.Point(1088, 692);
            this.ver_label.Name = "ver_label";
            this.ver_label.Size = new System.Drawing.Size(37, 15);
            this.ver_label.TabIndex = 15;
            this.ver_label.Text = "v0.9.3";
            // 
            // wordwrap_label2
            // 
            this.wordwrap_label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.wordwrap_label2.AutoSize = true;
            this.wordwrap_label2.Location = new System.Drawing.Point(473, 692);
            this.wordwrap_label2.Name = "wordwrap_label2";
            this.wordwrap_label2.Size = new System.Drawing.Size(100, 15);
            this.wordwrap_label2.TabIndex = 16;
            this.wordwrap_label2.Text = "word wrap on/off";
            // 
            // wordcount_label
            // 
            this.wordcount_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.wordcount_label.AutoSize = true;
            this.wordcount_label.Location = new System.Drawing.Point(579, 692);
            this.wordcount_label.Name = "wordcount_label";
            this.wordcount_label.Size = new System.Drawing.Size(44, 15);
            this.wordcount_label.TabIndex = 17;
            this.wordcount_label.Text = "Words:";
            // 
            // fileTextOutput
            // 
            this.fileTextOutput.Location = new System.Drawing.Point(0, 75);
            this.fileTextOutput.Name = "fileTextOutput";
            this.fileTextOutput.Size = new System.Drawing.Size(1137, 614);
            this.fileTextOutput.TabIndex = 1;
            this.fileTextOutput.Text = "";
            this.fileTextOutput.SelectionChanged += new System.EventHandler(this.fileTextOutput_SelectionChanged);
            this.fileTextOutput.TextChanged += new System.EventHandler(this.fileTextOutput_TextChanged);
            this.fileTextOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileTextOutput_KeyDown);
            this.fileTextOutput.Leave += new System.EventHandler(this.fileTextOutput_Leave);
            this.fileTextOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fileTextOutput_MouseDown);
            this.fileTextOutput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.fileTextOutput_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Manual Path Input:";
            // 
            // find_textBox1
            // 
            this.find_textBox1.Location = new System.Drawing.Point(392, 31);
            this.find_textBox1.Name = "find_textBox1";
            this.find_textBox1.Size = new System.Drawing.Size(100, 23);
            this.find_textBox1.TabIndex = 19;
            this.find_textBox1.TextChanged += new System.EventHandler(this.find_textBox1_TextChanged);
            this.find_textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.find_textBox1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Find:";
            // 
            // findNext_button1
            // 
            this.findNext_button1.Location = new System.Drawing.Point(498, 31);
            this.findNext_button1.Name = "findNext_button1";
            this.findNext_button1.Size = new System.Drawing.Size(75, 23);
            this.findNext_button1.TabIndex = 21;
            this.findNext_button1.Text = "Find Next";
            this.findNext_button1.UseVisualStyleBackColor = true;
            this.findNext_button1.Click += new System.EventHandler(this.findNext_button1_Click);
            // 
            // findLast_button1
            // 
            this.findLast_button1.Location = new System.Drawing.Point(579, 31);
            this.findLast_button1.Name = "findLast_button1";
            this.findLast_button1.Size = new System.Drawing.Size(75, 23);
            this.findLast_button1.TabIndex = 22;
            this.findLast_button1.Text = "Find Last";
            this.findLast_button1.UseVisualStyleBackColor = true;
            this.findLast_button1.Click += new System.EventHandler(this.findLast_button1_Click);
            // 
            // addNumberToEveryLineToolStripMenuItem
            // 
            this.addNumberToEveryLineToolStripMenuItem.Name = "addNumberToEveryLineToolStripMenuItem";
            this.addNumberToEveryLineToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addNumberToEveryLineToolStripMenuItem.Text = "Add Number To Every Line";
            this.addNumberToEveryLineToolStripMenuItem.Click += new System.EventHandler(this.addNumberToEveryLineToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1137, 711);
            this.Controls.Add(this.findLast_button1);
            this.Controls.Add(this.findNext_button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.find_textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wordcount_label);
            this.Controls.Add(this.wordwrap_label2);
            this.Controls.Add(this.ver_label);
            this.Controls.Add(this.linecol_label);
            this.Controls.Add(this.redo_button1);
            this.Controls.Add(this.undo_button1);
            this.Controls.Add(this.modified_label2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.ReadStringBtn);
            this.Controls.Add(this.fileTextOutput);
            this.Controls.Add(this.outputLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button ReadStringBtn;
        private System.Windows.Forms.Button ReadString;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.ToolStripDropDownButton filetoolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem panelBackgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelTextColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBoxBackgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBoxTextColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem spellCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem wordWrapOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boldTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFontSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unBoldTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highligherTextColorToolStripMenuItem;
        private System.Windows.Forms.Label modified_label2;
        private System.Windows.Forms.Button undo_button1;
        private System.Windows.Forms.Button redo_button1;
        private System.Windows.Forms.Label linecol_label;
        private System.Windows.Forms.Label ver_label;
        private System.Windows.Forms.Label wordwrap_label2;
        private System.Windows.Forms.Label wordcount_label;
        private System.Windows.Forms.ToolStripDropDownButton Replace_toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem replaceTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceEndOfEachLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceStartOfEachLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton remove_toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem removeLastCharacterFromEndOfEachLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLastCharacterFromStartOfEachLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLastLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton Add_toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem addIndentEveryXLinesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToEndOfEveryLineToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setFontStyleToolStripMenuItem;
        private System.Windows.Forms.RichTextBox fileTextOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox find_textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button findNext_button1;
        private System.Windows.Forms.Button findLast_button1;
        private System.Windows.Forms.ToolStripMenuItem highlightCodeSyntaxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToStartOfEveryLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton View_toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem toggleStatusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem togglePageClientSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleClientScalingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndHighlightTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNumberToEveryLineToolStripMenuItem;
    }
}

