namespace Font_Extender
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddLetter = new System.Windows.Forms.Button();
            this.PlannedGlyphSymbolsList = new System.Windows.Forms.ListBox();
            this.PlannedListClearButton = new System.Windows.Forms.Button();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SelectTTXButton = new System.Windows.Forms.Button();
            this.TTXPathTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.StartUniIndexTextBox = new System.Windows.Forms.TextBox();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConvertTTXtoTTF = new System.Windows.Forms.Button();
            this.RemoveLetter = new System.Windows.Forms.Button();
            this.picTestFont = new System.Windows.Forms.PictureBox();
            this.TestPhraseTextBox = new System.Windows.Forms.TextBox();
            this.TestPhrase2TextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SmallLetterCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxCustomWidthTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplaceCombinationCountTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SelectTextFilesButton = new System.Windows.Forms.Button();
            this.TextFilesList = new System.Windows.Forms.ListBox();
            this.GoButton = new System.Windows.Forms.Button();
            this.RestoreFontButton = new System.Windows.Forms.Button();
            this.SymbolsList = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTestFont)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddLetter
            // 
            this.AddLetter.Location = new System.Drawing.Point(163, 97);
            this.AddLetter.Name = "AddLetter";
            this.AddLetter.Size = new System.Drawing.Size(124, 58);
            this.AddLetter.TabIndex = 1;
            this.AddLetter.Text = "Add >>>";
            this.AddLetter.UseVisualStyleBackColor = true;
            this.AddLetter.Click += new System.EventHandler(this.AddLetter_Click);
            // 
            // PlannedGlyphSymbolsList
            // 
            this.PlannedGlyphSymbolsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PlannedGlyphSymbolsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlannedGlyphSymbolsList.FormattingEnabled = true;
            this.PlannedGlyphSymbolsList.ItemHeight = 24;
            this.PlannedGlyphSymbolsList.Location = new System.Drawing.Point(293, 97);
            this.PlannedGlyphSymbolsList.Name = "PlannedGlyphSymbolsList";
            this.PlannedGlyphSymbolsList.Size = new System.Drawing.Size(148, 340);
            this.PlannedGlyphSymbolsList.TabIndex = 2;
            this.PlannedGlyphSymbolsList.DoubleClick += new System.EventHandler(this.PlannedGlyphSymbolsList_DoubleClick);
            // 
            // PlannedListClearButton
            // 
            this.PlannedListClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PlannedListClearButton.Location = new System.Drawing.Point(293, 457);
            this.PlannedListClearButton.Name = "PlannedListClearButton";
            this.PlannedListClearButton.Size = new System.Drawing.Size(148, 43);
            this.PlannedListClearButton.TabIndex = 3;
            this.PlannedListClearButton.Text = "Clear";
            this.PlannedListClearButton.UseVisualStyleBackColor = true;
            this.PlannedListClearButton.Click += new System.EventHandler(this.PlannedListClearButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProcessButton.Location = new System.Drawing.Point(12, 506);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(429, 23);
            this.ProcessButton.TabIndex = 4;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SelectTTXButton);
            this.groupBox1.Controls.Add(this.TTXPathTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 55);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TTX Path";
            // 
            // SelectTTXButton
            // 
            this.SelectTTXButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectTTXButton.Location = new System.Drawing.Point(398, 22);
            this.SelectTTXButton.Name = "SelectTTXButton";
            this.SelectTTXButton.Size = new System.Drawing.Size(25, 20);
            this.SelectTTXButton.TabIndex = 1;
            this.SelectTTXButton.Text = "...";
            this.SelectTTXButton.UseVisualStyleBackColor = true;
            this.SelectTTXButton.Click += new System.EventHandler(this.SelectTTXButton_Click);
            // 
            // TTXPathTextBox
            // 
            this.TTXPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TTXPathTextBox.Location = new System.Drawing.Point(6, 22);
            this.TTXPathTextBox.Name = "TTXPathTextBox";
            this.TTXPathTextBox.ReadOnly = true;
            this.TTXPathTextBox.Size = new System.Drawing.Size(386, 20);
            this.TTXPathTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Start Uni Index:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StartUniIndexTextBox
            // 
            this.StartUniIndexTextBox.Location = new System.Drawing.Point(188, 180);
            this.StartUniIndexTextBox.Name = "StartUniIndexTextBox";
            this.StartUniIndexTextBox.Size = new System.Drawing.Size(35, 20);
            this.StartUniIndexTextBox.TabIndex = 7;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar,
            this.StatusProgressBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 565);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(905, 22);
            this.StatusStrip.TabIndex = 10;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusBar
            // 
            this.StatusBar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(788, 17);
            this.StatusBar.Spring = true;
            this.StatusBar.Text = "Ready to work!";
            // 
            // ConvertTTXtoTTF
            // 
            this.ConvertTTXtoTTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConvertTTXtoTTF.Location = new System.Drawing.Point(12, 535);
            this.ConvertTTXtoTTF.Name = "ConvertTTXtoTTF";
            this.ConvertTTXtoTTF.Size = new System.Drawing.Size(429, 23);
            this.ConvertTTXtoTTF.TabIndex = 11;
            this.ConvertTTXtoTTF.Text = "Convert TTX to TTF (Need installed fonttools)";
            this.ConvertTTXtoTTF.UseVisualStyleBackColor = true;
            this.ConvertTTXtoTTF.Click += new System.EventHandler(this.ConvertTTXtoTTF_Click);
            // 
            // RemoveLetter
            // 
            this.RemoveLetter.Location = new System.Drawing.Point(163, 161);
            this.RemoveLetter.Name = "RemoveLetter";
            this.RemoveLetter.Size = new System.Drawing.Size(124, 58);
            this.RemoveLetter.TabIndex = 12;
            this.RemoveLetter.Text = "<<< Remove";
            this.RemoveLetter.UseVisualStyleBackColor = true;
            this.RemoveLetter.Click += new System.EventHandler(this.RemoveLetter_Click);
            // 
            // picTestFont
            // 
            this.picTestFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picTestFont.BackColor = System.Drawing.Color.White;
            this.picTestFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTestFont.Location = new System.Drawing.Point(13, 101);
            this.picTestFont.Name = "picTestFont";
            this.picTestFont.Size = new System.Drawing.Size(404, 121);
            this.picTestFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTestFont.TabIndex = 13;
            this.picTestFont.TabStop = false;
            this.picTestFont.Click += new System.EventHandler(this.picTestFont_Click);
            this.picTestFont.Paint += new System.Windows.Forms.PaintEventHandler(this.picTestFont_Paint);
            // 
            // TestPhraseTextBox
            // 
            this.TestPhraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestPhraseTextBox.Location = new System.Drawing.Point(13, 36);
            this.TestPhraseTextBox.Name = "TestPhraseTextBox";
            this.TestPhraseTextBox.Size = new System.Drawing.Size(404, 20);
            this.TestPhraseTextBox.TabIndex = 15;
            this.TestPhraseTextBox.TextChanged += new System.EventHandler(this.TestPhraseTextBox_TextChanged);
            // 
            // TestPhrase2TextBox
            // 
            this.TestPhrase2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestPhrase2TextBox.Location = new System.Drawing.Point(13, 75);
            this.TestPhrase2TextBox.Name = "TestPhrase2TextBox";
            this.TestPhrase2TextBox.Size = new System.Drawing.Size(404, 20);
            this.TestPhrase2TextBox.TabIndex = 16;
            this.TestPhrase2TextBox.TextChanged += new System.EventHandler(this.TestPhrase2TextBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SmallLetterCheckBox);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.MaxCustomWidthTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ReplaceCombinationCountTextBox);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.StartUniIndexTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(447, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 481);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General options and replace mode";
            // 
            // SmallLetterCheckBox
            // 
            this.SmallLetterCheckBox.AutoSize = true;
            this.SmallLetterCheckBox.Checked = true;
            this.SmallLetterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SmallLetterCheckBox.Location = new System.Drawing.Point(17, 206);
            this.SmallLetterCheckBox.Name = "SmallLetterCheckBox";
            this.SmallLetterCheckBox.Size = new System.Drawing.Size(143, 17);
            this.SmallLetterCheckBox.TabIndex = 6;
            this.SmallLetterCheckBox.Text = "Process only small letters";
            this.SmallLetterCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.picTestFont);
            this.groupBox4.Controls.Add(this.TestPhraseTextBox);
            this.groupBox4.Controls.Add(this.TestPhrase2TextBox);
            this.groupBox4.Location = new System.Drawing.Point(7, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(433, 238);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Font Demonstration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Second text line";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "First text line";
            // 
            // MaxCustomWidthTextBox
            // 
            this.MaxCustomWidthTextBox.Location = new System.Drawing.Point(188, 154);
            this.MaxCustomWidthTextBox.Name = "MaxCustomWidthTextBox";
            this.MaxCustomWidthTextBox.Size = new System.Drawing.Size(35, 20);
            this.MaxCustomWidthTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Custom symbol max width:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Replace combination count:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReplaceCombinationCountTextBox
            // 
            this.ReplaceCombinationCountTextBox.Location = new System.Drawing.Point(188, 128);
            this.ReplaceCombinationCountTextBox.Name = "ReplaceCombinationCountTextBox";
            this.ReplaceCombinationCountTextBox.Size = new System.Drawing.Size(35, 20);
            this.ReplaceCombinationCountTextBox.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.SelectTextFilesButton);
            this.groupBox3.Controls.Add(this.TextFilesList);
            this.groupBox3.Location = new System.Drawing.Point(7, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(433, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Text Files";
            // 
            // SelectTextFilesButton
            // 
            this.SelectTextFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectTextFilesButton.Location = new System.Drawing.Point(391, 20);
            this.SelectTextFilesButton.Name = "SelectTextFilesButton";
            this.SelectTextFilesButton.Size = new System.Drawing.Size(36, 69);
            this.SelectTextFilesButton.TabIndex = 1;
            this.SelectTextFilesButton.Text = "...";
            this.SelectTextFilesButton.UseVisualStyleBackColor = true;
            this.SelectTextFilesButton.Click += new System.EventHandler(this.SelectTextFilesButton_Click);
            // 
            // TextFilesList
            // 
            this.TextFilesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextFilesList.FormattingEnabled = true;
            this.TextFilesList.Location = new System.Drawing.Point(7, 20);
            this.TextFilesList.Name = "TextFilesList";
            this.TextFilesList.Size = new System.Drawing.Size(378, 69);
            this.TextFilesList.TabIndex = 0;
            // 
            // GoButton
            // 
            this.GoButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GoButton.Location = new System.Drawing.Point(447, 506);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(446, 51);
            this.GoButton.TabIndex = 18;
            this.GoButton.Text = "Replace symbols in text files";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // RestoreFontButton
            // 
            this.RestoreFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RestoreFontButton.Location = new System.Drawing.Point(12, 457);
            this.RestoreFontButton.Name = "RestoreFontButton";
            this.RestoreFontButton.Size = new System.Drawing.Size(145, 43);
            this.RestoreFontButton.TabIndex = 19;
            this.RestoreFontButton.Text = "Restore Original Font";
            this.RestoreFontButton.UseVisualStyleBackColor = true;
            this.RestoreFontButton.Click += new System.EventHandler(this.RestoreFontButton_Click);
            // 
            // SymbolsList
            // 
            this.SymbolsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SymbolsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SymbolsList.FormattingEnabled = true;
            this.SymbolsList.ItemHeight = 24;
            this.SymbolsList.Location = new System.Drawing.Point(12, 97);
            this.SymbolsList.Name = "SymbolsList";
            this.SymbolsList.Size = new System.Drawing.Size(145, 340);
            this.SymbolsList.TabIndex = 20;
            this.SymbolsList.DoubleClick += new System.EventHandler(this.SymbolsList_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "General Symbols List";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Custom Symbol Components";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 587);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SymbolsList);
            this.Controls.Add(this.RestoreFontButton);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.RemoveLetter);
            this.Controls.Add(this.ConvertTTXtoTTF);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.PlannedListClearButton);
            this.Controls.Add(this.PlannedGlyphSymbolsList);
            this.Controls.Add(this.AddLetter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Font Extender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTestFont)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddLetter;
        private System.Windows.Forms.ListBox PlannedGlyphSymbolsList;
        private System.Windows.Forms.Button PlannedListClearButton;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SelectTTXButton;
        private System.Windows.Forms.TextBox TTXPathTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StartUniIndexTextBox;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusBar;
        private System.Windows.Forms.Button ConvertTTXtoTTF;
        private System.Windows.Forms.Button RemoveLetter;
        private System.Windows.Forms.PictureBox picTestFont;
        private System.Windows.Forms.TextBox TestPhraseTextBox;
        private System.Windows.Forms.TextBox TestPhrase2TextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SelectTextFilesButton;
        private System.Windows.Forms.ListBox TextFilesList;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ReplaceCombinationCountTextBox;
        private System.Windows.Forms.TextBox MaxCustomWidthTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox SmallLetterCheckBox;
        private System.Windows.Forms.Button RestoreFontButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox SymbolsList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar;
    }
}

