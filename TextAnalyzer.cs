using Font_Extender.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Font_Extender
{
    public class TextAnalyzer
    {
        public List<CombinationInfo> CombinationInfos { get; set; }

        private readonly string cyrillicLettersPlusSpace = "абгдеёжзийклмнопрстуфхцчшщъыьэюя ";

        private readonly string[] _filePaths;
        private readonly Encoding _fileEncoding;

        private readonly double _frequencyRatio;
        private readonly double _totalWidthRatio;

        private string[] _filesText;

        public TextAnalyzer(string filePath, Encoding fileEncoding, bool onlySmallLetters, double frequencyRatio, double totalWidthRatio)
            : this(new string[] { filePath }, fileEncoding, onlySmallLetters, frequencyRatio, totalWidthRatio) { }
        public TextAnalyzer(string[] filePaths, Encoding fileEncoding, bool onlySmallLetters, double frequencyRatio, double totalWidthRatio)
        {
            _filePaths = filePaths;
            _fileEncoding = fileEncoding;

            _frequencyRatio = frequencyRatio;
            _totalWidthRatio = totalWidthRatio;

            _filesText = new string[_filePaths.Length];
            for (int fileIndex = 0; fileIndex < _filePaths.Length; fileIndex++)
            {
                _filesText[fileIndex] = OpenFile(_filePaths[fileIndex], _fileEncoding);
            }

            if (!onlySmallLetters)
            {
                cyrillicLettersPlusSpace += cyrillicLettersPlusSpace.ToUpper();
                cyrillicLettersPlusSpace = cyrillicLettersPlusSpace.Remove(cyrillicLettersPlusSpace.Length - 1);
            }
        }

        public void Analyze(string[] excludedCombination, Dictionary<string, int> symbolWidths, int maxWidth)
        {
            CombinationInfos = new List<CombinationInfo>();

            for (int firstLetterIndex = 0; firstLetterIndex < cyrillicLettersPlusSpace.Length; firstLetterIndex++)
            {
                for (int secondLetterIndex = 0; secondLetterIndex < cyrillicLettersPlusSpace.Length; secondLetterIndex++)
                {
                    for (int thirdLetterIndex = 0; thirdLetterIndex < cyrillicLettersPlusSpace.Length; thirdLetterIndex++)
                    {
                        var searchCombination = cyrillicLettersPlusSpace[firstLetterIndex].ToString() +
                                                cyrillicLettersPlusSpace[secondLetterIndex].ToString() +
                                                cyrillicLettersPlusSpace[thirdLetterIndex].ToString();

                        if (excludedCombination.Contains(searchCombination))
                        {
                            continue;
                        }

                        var countOfCombination = 0;
                        for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
                        {
                            countOfCombination += Regex.Matches(_filesText[textIndex], searchCombination).Count;
                        }

                        if (countOfCombination != 0)
                        {
                            var combinationWidth = 0;
                            foreach (var cSymbol in searchCombination)
                            {
                                combinationWidth += symbolWidths[cSymbol.ToString()];
                            }

                            if (combinationWidth > maxWidth)
                            {
                                continue;
                            }

                            CombinationInfos.Add(new CombinationInfo
                            {
                                Value = searchCombination,
                                Frequency = countOfCombination,
                                TotalWidth = combinationWidth
                            });
                        }
                    }
                }
            }

            for (int firstLetterIndex = 0; firstLetterIndex < cyrillicLettersPlusSpace.Length; firstLetterIndex++)
            {
                for (int secondLetterIndex = 0; secondLetterIndex < cyrillicLettersPlusSpace.Length; secondLetterIndex++)
                {
                    var searchCombination = cyrillicLettersPlusSpace[firstLetterIndex].ToString() +
                                             cyrillicLettersPlusSpace[secondLetterIndex].ToString();

                    var countOfCombination = 0;
                    for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
                    {
                        countOfCombination += Regex.Matches(_filesText[textIndex], searchCombination).Count;
                    }

                    if (excludedCombination.Contains(searchCombination))
                    {
                        continue;
                    }

                    if (countOfCombination != 0)
                    {
                        var combinationWidth = 0;
                        foreach (var cSymbol in searchCombination)
                        {
                            combinationWidth += symbolWidths[cSymbol.ToString()];
                        }

                        if (combinationWidth > maxWidth)
                        {
                            continue;
                        }

                        CombinationInfos.Add(new CombinationInfo
                        {
                            Value = searchCombination,
                            Frequency = countOfCombination,
                            TotalWidth = combinationWidth
                        });
                    }
                }
            }

            if (CombinationInfos.Count > 0)
            {
                var maxFrequency = CombinationInfos.Max(x => x.Frequency);
                var maxTotalWidth = CombinationInfos.Max(x => x.TotalWidth);

                CombinationInfos = CombinationInfos.OrderByDescending(
                    x => (100.0 * x.Frequency / maxFrequency * _frequencyRatio) + (100.0 * x.TotalWidth / maxTotalWidth * _totalWidthRatio)).ToList();
            }
        }

        private string OpenFile(string filePath, Encoding fileEncoding)
        {
            return File.ReadAllText(filePath, fileEncoding);
        }

        public void ReplaceWithSymbol(string textForReplace, char targetSymbol)
        {
            for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
            {
                _filesText[textIndex] = _filesText[textIndex].Replace(textForReplace, targetSymbol.ToString());
            }
        }

        public void SaveChanges(bool overrideFile = true)
        {
            for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
            {
                if (overrideFile)
                {
                    File.WriteAllText(_filePaths[textIndex], _filesText[textIndex], _fileEncoding);
                }
                else
                {
                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(_filePaths[textIndex]), Path.GetFileNameWithoutExtension(_filePaths[textIndex]) + "_modified" + Path.GetExtension(_filePaths[textIndex])), _filesText[textIndex], _fileEncoding);
                }
            }
        }
    }
}
