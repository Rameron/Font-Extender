using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Font_Extender
{
    public partial class MainForm : Form
    {
        #region Const

        private const string ORIGINAL_FONT_FILE_ENDS = "_orig.ttf";

        #endregion

        #region Private Fields

        private Dictionary<string, string> _symbolsDictionary;
        private TTXManager _ttxManager;

        private const bool _clearFontChanges = false;
        private const bool _useTestSymbolsSet = false;
        private const bool _useTestFile = false;

        private string _originalFontLocation;
        private string _modifiedFontLocation;
        private string _ttxLocation;

        private string _defaultStartUniIndex;
        private int _defaultReplaceCombinationCount;
        private int _defaultMaxCustomWidthCount;

        #endregion

        #region ctor

        public MainForm()
        {
            LoadWorkingPaths();

            _defaultStartUniIndex = Properties.Settings.Default.StartUniIndex;
            _defaultReplaceCombinationCount = Properties.Settings.Default.ReplaceCombinationCount;
            _defaultMaxCustomWidthCount = Properties.Settings.Default.MaxCustomWidthCount;

            LoadSymbols();
            InitTTXManager();

            if (_clearFontChanges)
            {
                File.Delete(_modifiedFontLocation);
                File.Delete(_ttxLocation);
                File.Copy(_originalFontLocation, _modifiedFontLocation);
                TTXUtils.ExecuteTTXConversion(_modifiedFontLocation);
                File.Delete(_modifiedFontLocation);
                _ttxManager.RemoveGlyphHistory();

                if (_useTestSymbolsSet)
                {
                    _ttxManager.AddСombinedGlyph(new string[] { "uni0432", "uni0446", "uni0435" });
                    _ttxManager.AddСombinedGlyph(new string[] { "space", "uni0436", "uni0435" });
                    _ttxManager.AddСombinedGlyph(new string[] { "uni043B", "space", "uni0434" });
                    _ttxManager.AddСombinedGlyph(new string[] { "space", "uni0432", "space" });
                    _ttxManager.SaveChanges();
                    TTXUtils.ExecuteTTXConversion(_ttxLocation);
                }
            }
            if (_useTestFile)
            {
                var analyzer = new TextAnalyzer(@"C:\Users\Ramer\Desktop\Original.txt", Encoding.UTF8, true);
                for (int customSymbolIndex = 0; customSymbolIndex < 50; customSymbolIndex++)
                {
                    analyzer.Analyze();
                    var replacedCombination = analyzer.CombinationHistogram.First().Key;

                    var combinedSymbols = new List<string>();
                    foreach (var sChar in replacedCombination)
                    {
                        if (sChar.ToString() != " ")
                        {
                            combinedSymbols.Add(_symbolsDictionary[sChar.ToString()]);
                        }
                        else
                        {
                            combinedSymbols.Add("space");
                        }
                    }

                    var currentSymbolCode = _ttxManager.AddСombinedGlyph(combinedSymbols.ToArray());
                    _ttxManager.SaveChanges();

                    analyzer.ReplaceWithSymbol(replacedCombination, (char)(currentSymbolCode));
                }
                TTXUtils.ExecuteTTXConversion(_ttxLocation);
                analyzer.SaveChanges();
            }

            InitializeComponent();
        }

        #endregion

        #region Load Methods

        private void LoadSymbols()
        {
            _symbolsDictionary = new Dictionary<string, string>();

            string[] symbolLines = File.ReadAllLines("Symbols.txt");
            foreach (var symbolLine in symbolLines)
            {
                var symbolLineItems = symbolLine.Split(new char[] { '=' }, 2);
                _symbolsDictionary.Add(symbolLineItems[0], symbolLineItems[1]);
            }
        }

        private void InitTTXManager()
        {
            //Check ttx file exists
            if (!File.Exists(_ttxLocation))
            {
                MessageBox.Show(this, $"TTX file not found. It will now be created with 'fonttools' util 'ttx'.", "TTX file doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (File.Exists(_modifiedFontLocation))
                {
                    File.Delete(_modifiedFontLocation);
                }
                File.Copy(_originalFontLocation, _modifiedFontLocation);
                TTXUtils.ExecuteTTXConversion(_modifiedFontLocation);
                File.Delete(_modifiedFontLocation);

                if (!File.Exists(_ttxLocation))
                {
                    MessageBox.Show(this, $"TTX file couldn't create. Please install 'fonttools' and try again.", "TTX wasn't created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            _ttxManager = new TTXManager(
                _ttxLocation,
                int.Parse(_defaultStartUniIndex, System.Globalization.NumberStyles.HexNumber),
                _defaultMaxCustomWidthCount);
        }

        private void LoadWorkingPaths()
        {
            string fontsDirectoryPath = $@"{Environment.CurrentDirectory}\Fonts\";

            if (!Directory.Exists(fontsDirectoryPath))
            {
                MessageBox.Show(this, $"The directory 'Fonts' doesn't exist. Please create it and copy ttf font file with name ended with '{ORIGINAL_FONT_FILE_ENDS}' in it.", "Directory not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            string[] fontsDirectoryFiles = Directory.GetFiles(fontsDirectoryPath);

            if (fontsDirectoryFiles.Length == 0)
            {
                MessageBox.Show(this, $"The directory 'Fonts' is empty. Please create it and copy ttf font file with name ended with '{ORIGINAL_FONT_FILE_ENDS}' in it.", "Directory is empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            int originalFontFileCount = fontsDirectoryFiles.Count(f => f.EndsWith("_orig.ttf"));

            if (originalFontFileCount > 1)
            {
                MessageBox.Show(this, $"The directory 'Fonts' has more than one ttf font file with name ended with '{ORIGINAL_FONT_FILE_ENDS}'. Please left only one in it.", "Many orig fonts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (originalFontFileCount == 0)
            {
                MessageBox.Show(this, $"The directory 'Fonts' hasn't any ttf font file with name ended with '{ORIGINAL_FONT_FILE_ENDS}'. Please copy ttf font file with name ended with '{ORIGINAL_FONT_FILE_ENDS}' in it.", "No orig fonts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            _originalFontLocation = fontsDirectoryFiles.First(f => f.EndsWith("_orig.ttf"));
            _modifiedFontLocation = _originalFontLocation.Substring(0, _originalFontLocation.Length - ORIGINAL_FONT_FILE_ENDS.Length) + ".ttf";
            _ttxLocation = _originalFontLocation.Substring(0, _originalFontLocation.Length - ORIGINAL_FONT_FILE_ENDS.Length) + ".ttx";
        }

        #endregion

        #region Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Initialize textboxes with default starting values
            TTXPathTextBox.Text = _ttxLocation;
            StartUniIndexTextBox.Text = _defaultStartUniIndex;
            ReplaceCombinationCountTextBox.Text = _defaultReplaceCombinationCount.ToString();
            MaxCustomWidthTextBox.Text = _defaultMaxCustomWidthCount.ToString();

            //Fill first phrase textbox with custom symbols values
            for (var i = 0; i < 30; i++)
            {
                TestPhraseTextBox.Text += $@"\u{int.Parse(StartUniIndexTextBox.Text, System.Globalization.NumberStyles.HexNumber) + i:X}";
            }

            //Fill symbols list with loaded symbols
            foreach (var key in _symbolsDictionary.Keys)
            {
                SymbolsList.Items.Add(key);
            }
        }


        private void SelectTTXButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "TTX File (*.ttx)|*.ttx";
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                TTXPathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void PlannedListClearButton_Click(object sender, EventArgs e)
        {
            PlannedGlyphSymbolsList.Items.Clear();
        }

        private void SymbolsList_DoubleClick(object sender, EventArgs e)
        {
            AddLetter.PerformClick();
        }

        private void RemoveLetter_Click(object sender, EventArgs e)
        {
            if (PlannedGlyphSymbolsList.SelectedIndex != -1)
            {
                PlannedGlyphSymbolsList.Items.Remove(PlannedGlyphSymbolsList.SelectedItem);
            }
        }

        private void PlannedGlyphSymbolsList_DoubleClick(object sender, EventArgs e)
        {
            RemoveLetter.PerformClick();
        }

        private void AddLetter_Click(object sender, EventArgs e)
        {
            if (SymbolsList.SelectedIndex != -1)
            {
                PlannedGlyphSymbolsList.Items.Add(SymbolsList.SelectedItem);
            }
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StartUniIndexTextBox.Text))
            {
                StatusBar.Text = "Cannot start processing: 'Start Uni Index' field is empty.";
                return;
            }
            if (!int.TryParse(StartUniIndexTextBox.Text, System.Globalization.NumberStyles.HexNumber, null, out var startUniIndex))
            {
                StatusBar.Text = "Cannot start processing: 'Start Uni Index' field has invalid value. It should be hex number.";
                return;
            }
            if (PlannedGlyphSymbolsList.Items.Count < 2)
            {
                StatusBar.Text = "Cannot start processing: you should add at least two symbols for processing.";
                return;
            }
            if (string.IsNullOrEmpty(MaxCustomWidthTextBox.Text))
            {
                StatusBar.Text = "Cannot start processing: 'MaxCustomWidthTextBox' field is empty.";
                return;
            }
            if (!int.TryParse(MaxCustomWidthTextBox.Text, out var maxGlyphWidth))
            {
                StatusBar.Text = "Cannot start processing: 'MaxCustomWidthTextBox' field has invalid value. It should be decimal number.";
                return;
            }

            StatusBar.Text = "Processing, please wait...";

            var ttxManager = new TTXManager(
               TTXPathTextBox.Text,
               startUniIndex,
               maxGlyphWidth
               );

            List<string> symbolCodes = new List<string>();
            foreach (var item in PlannedGlyphSymbolsList.Items)
            {
                var targetKey = item.ToString();

                if (targetKey == "<пробел>")
                {
                    symbolCodes.Add("space");
                }
                else
                {
                    symbolCodes.Add(_symbolsDictionary[targetKey]);
                }
            }

            try
            {
                ttxManager.AddСombinedGlyph(symbolCodes.ToArray()).ToString("X");
                ttxManager.SaveChanges();

                foreach (var item in PlannedGlyphSymbolsList.Items)
                {
                    var targetKey = item.ToString();

                    if (targetKey == "<пробел>")
                    {
                        TestPhrase2TextBox.Text += " ";
                    }
                    else
                    {
                        TestPhrase2TextBox.Text += targetKey;
                    }
                }
                PlannedListClearButton.PerformClick();

                StatusBar.Text = "Glyph creation successfully completed!";
            }
            catch (Exception exception)
            {
                StatusBar.Text = $"Conversion failed: {exception.Message}";

                File.AppendAllText("Errors.log", $"{DateTime.Now}: Error message - {exception.Message}{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        private void ConvertTTXtoTTF_Click(object sender, EventArgs e)
        {
            StatusBar.Text = "Wait for conversion complete...";

            var ttfFilePath = Path.Combine(Path.GetDirectoryName(TTXPathTextBox.Text), Path.GetFileNameWithoutExtension(TTXPathTextBox.Text) + ".ttf");
            if (File.Exists(ttfFilePath))
            {
                var dialogResult = MessageBox.Show(this, $"File {Path.GetFileName(ttfFilePath)} already exists. Do you want to override it?", "Override file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Delete(ttfFilePath);
                }
            }

            TTXUtils.ExecuteTTXConversion(TTXPathTextBox.Text);
            picTestFont.Refresh();

            StatusBar.Text = "Сonversion successfully complete!";
        }

        private void picTestFont_Paint(object sender, PaintEventArgs e)
        {
            var ttfFilePath = Path.Combine(Path.GetDirectoryName(TTXPathTextBox.Text), Path.GetFileNameWithoutExtension(TTXPathTextBox.Text) + ".ttf");

            if (File.Exists(ttfFilePath))
            {
                PrivateFontCollection privateFontCollection = new PrivateFontCollection();

                using (var stream = File.OpenRead(ttfFilePath))
                {
                    var fontData = new byte[stream.Length];
                    stream.Read(fontData, 0, fontData.Length);
                    var fontDataPtr = Marshal.AllocCoTaskMem(fontData.Length);
                    try
                    {
                        Marshal.Copy(fontData, 0, fontDataPtr, fontData.Length);
                        privateFontCollection.AddMemoryFont(fontDataPtr, fontData.Length);
                        Thread.Sleep(100);
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(fontDataPtr);
                    }
                }
                //privateFontCollection.AddFontFile(ttfFilePath);

                FontFamily[] fontFamilies = privateFontCollection.Families;
                Font customFont = new Font(fontFamilies[0], 16.0F);

                PointF pointF = new PointF(10, 0);
                SolidBrush solidBrush = new SolidBrush(Color.Black);

                var showingText = TestPhraseTextBox.Text;
                MatchCollection matches = Regex.Matches(showingText, @"\\u([0-9A-Fa-f]{4})");
                foreach (var oMatch in matches)
                {
                    var match = oMatch as Match;
                    showingText = showingText.Replace(match.Value, ((char)int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString());
                }

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                e.Graphics.DrawString(
                   showingText,
                   customFont,
                   solidBrush,
                   pointF);

                e.Graphics.DrawString(
                   TestPhrase2TextBox.Text,
                   customFont,
                   solidBrush,
                   new PointF(pointF.X, pointF.Y + 17));

                customFont.Dispose();
                privateFontCollection.Dispose();
            }
        }

        private void TestPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            picTestFont.Refresh();
        }

        private void TestPhrase2TextBox_TextChanged(object sender, EventArgs e)
        {
            picTestFont.Refresh();
        }

        private void picTestFont_Click(object sender, EventArgs e)
        {
            picTestFont.Refresh();
        }

        private void SelectTextFilesButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Any File (*.*)|*.*";
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                if (!TextFilesList.Items.Contains(openFileDialog.FileName))
                {
                    TextFilesList.Items.Add(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show(this, "File already added!", "Error");
                }
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StartUniIndexTextBox.Text))
            {
                StatusBar.Text = "Cannot start processing: 'Start Uni Index' field is empty.";
                return;
            }
            if (!int.TryParse(StartUniIndexTextBox.Text, System.Globalization.NumberStyles.HexNumber, null, out var startUniIndex))
            {
                StatusBar.Text = "Cannot start processing: 'Start Uni Index' field has invalid value. It should be hex number.";
                return;
            }

            if (string.IsNullOrEmpty(ReplaceCombinationCountTextBox.Text))
            {
                StatusBar.Text = "Cannot start processing: 'Replace Combination Count' field is empty.";
                return;
            }
            if (!int.TryParse(ReplaceCombinationCountTextBox.Text, out var combinationCount))
            {
                StatusBar.Text = "Cannot start processing: 'Replace Combination Count' field has invalid value. It should be decimal number.";
                return;
            }

            if (TextFilesList.Items.Count < 1)
            {
                StatusBar.Text = "Cannot start processing: you should add at least one file with text for processing.";
                return;
            }

            if (string.IsNullOrEmpty(MaxCustomWidthTextBox.Text))
            {
                StatusBar.Text = "Cannot start processing: 'MaxCustomWidthTextBox' field is empty.";
                return;
            }
            if (!int.TryParse(MaxCustomWidthTextBox.Text, out var maxGlyphWidth))
            {
                StatusBar.Text = "Cannot start processing: 'MaxCustomWidthTextBox' field has invalid value. It should be decimal number.";
                return;
            }

            StatusBar.Text = "Processing, please wait...";
            Application.DoEvents();

            var ttxManager = new TTXManager(
                    TTXPathTextBox.Text,
                    startUniIndex,
                    maxGlyphWidth
                    );

            var analyzer = new TextAnalyzer(TextFilesList.Items.Cast<string>().ToArray(), Encoding.UTF8, SmallLetterCheckBox.Checked);
            for (int customSymbolIndex = 0; customSymbolIndex < combinationCount; customSymbolIndex++)
            {
                analyzer.Analyze();

                if (analyzer.CombinationHistogram.Count == 0)
                {
                    break;
                }

                var replacedCombination = analyzer.CombinationHistogram.First().Key;

                var combinedSymbols = new List<string>();
                foreach (var sChar in replacedCombination)
                {
                    if (sChar.ToString() != " ")
                    {
                        combinedSymbols.Add(_symbolsDictionary[sChar.ToString()]);
                    }
                    else
                    {
                        combinedSymbols.Add("space");
                    }
                }

                var currentSymbolCode = ttxManager.AddСombinedGlyph(combinedSymbols.ToArray());
                analyzer.ReplaceWithSymbol(replacedCombination, (char)(currentSymbolCode));
            }

            ttxManager.SaveChanges();
            TTXUtils.ExecuteTTXConversion(TTXPathTextBox.Text);
            analyzer.SaveChanges();

            StatusBar.Text = "Processing successfully completed!";
        }

        private void RestoreFontButton_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(this, $"Do you really want to restore original font?", "Font Restoring", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(_modifiedFontLocation);
                File.Delete(_ttxLocation);
                File.Copy(_originalFontLocation, _modifiedFontLocation);
                TTXUtils.ExecuteTTXConversion(_modifiedFontLocation);
                File.Delete(_modifiedFontLocation);
                _ttxManager.RemoveGlyphHistory();

                picTestFont.Refresh();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.StartUniIndex = StartUniIndexTextBox.Text;
            Properties.Settings.Default.ReplaceCombinationCount = int.Parse(ReplaceCombinationCountTextBox.Text);
            Properties.Settings.Default.MaxCustomWidthCount = int.Parse(MaxCustomWidthTextBox.Text);

            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
