using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Font_Extender
{
    public class TextAnalyzer
    {
        public Dictionary<string, int> CombinationHistogram { get; set; }

        private readonly string cyrillicLettersPlusSpace = "абгдеёжзийклмнопрстуфхцчшщъыьэюя ";

        private readonly string[] _filePaths;
        private readonly Encoding _fileEncoding;
        private readonly bool _onlySmallLetters;

        private string[] _filesText;

        public TextAnalyzer(string filePath, Encoding fileEncoding, bool onlySmallLetters) : this(new string[] { filePath }, fileEncoding, onlySmallLetters) { }
        public TextAnalyzer(string[] filePaths, Encoding fileEncoding, bool onlySmallLetters)
        {
            _filePaths = filePaths;
            _fileEncoding = fileEncoding;
            _onlySmallLetters = onlySmallLetters;

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

        public void Analyze()
        {
            CombinationHistogram = new Dictionary<string, int>();

            for (int firstLetterIndex = 0; firstLetterIndex < cyrillicLettersPlusSpace.Length; firstLetterIndex++)
            {
                for (int secondLetterIndex = 0; secondLetterIndex < cyrillicLettersPlusSpace.Length; secondLetterIndex++)
                {
                    for (int thirdLetterIndex = 0; thirdLetterIndex < cyrillicLettersPlusSpace.Length; thirdLetterIndex++)
                    {
                        var searchCombination = cyrillicLettersPlusSpace[firstLetterIndex].ToString() +
                                                cyrillicLettersPlusSpace[secondLetterIndex].ToString() +
                                                cyrillicLettersPlusSpace[thirdLetterIndex].ToString();

                        var countOfCombination = 0;
                        for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
                        {
                            countOfCombination += Regex.Matches(_filesText[textIndex], searchCombination).Count;
                        }

                        if (countOfCombination != 0)
                        {
                            CombinationHistogram.Add(searchCombination, countOfCombination);
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

                    if (countOfCombination != 0)
                    {
                        CombinationHistogram.Add(searchCombination, countOfCombination);
                    }
                }
            }

            CombinationHistogram = CombinationHistogram.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
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

        public void SaveChanges()
        {
            for (int textIndex = 0; textIndex < _filesText.Length; textIndex++)
            {
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(_filePaths[textIndex]), Path.GetFileNameWithoutExtension(_filePaths[textIndex]) + "_modified" + Path.GetExtension(_filePaths[textIndex])), _filesText[textIndex], _fileEncoding);
            }
        }
    }
}
