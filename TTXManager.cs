using Font_Extender.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace Font_Extender
{
    public class TTXManager
    {
        private readonly XmlDocument ttxFontFile;

        private string _ttxFilePath;
        private int _startUniIndex;
        private int _maxSymbolWidth;

        private int _currentUniIndex;

        private readonly string _glyphHistoryLocation;
        private Dictionary<string, string[]> _glyphHistoryDictionary;

        public string TtxFilePath { get => _ttxFilePath; set => _ttxFilePath = value; }
        public int StartUniIndex { get => _startUniIndex; set => _startUniIndex = value; }
        public int MaxSymbolWidth { get => _maxSymbolWidth; set => _maxSymbolWidth = value; }

        public TTXManager(string ttxFilePath, int startUniIndex, int maxSymbolWidth)
        {
            _glyphHistoryLocation = $@"{Environment.CurrentDirectory}\Fonts\AsteriskSansPro-SemiBoldItalic.history.txt";
            LoadGlyphHistory();

            _ttxFilePath = ttxFilePath;
            _startUniIndex = startUniIndex;
            _maxSymbolWidth = maxSymbolWidth;

            _currentUniIndex = _startUniIndex;

            ttxFontFile = new XmlDocument();
            ttxFontFile.Load(_ttxFilePath);
        }

        public int AddСombinedGlyph(string[] combinedGlyphComponentNames)
        {
            //Check if lglyph from same components exists
            CheckGlyphHistory(combinedGlyphComponentNames);

            //Get root node and glyf node of TTX(XML) file for glyphs parsing and writing
            XmlElement xRoot = ttxFontFile.DocumentElement;
            XmlNode glyfsNode = xRoot.SelectSingleNode("glyf");

            //Find next free uni index for GlyphOrder node
            FindFreeUniIndex(glyfsNode);
            
            //Parse info about combined glyph components from TTX(XML) file
            List<TTGlyph> combinedGlyphComponents = ParsePartGlyphs(glyfsNode, combinedGlyphComponentNames);
            
            //Get hmtx node and mtx nodes of TTX(XML) file for calculating of total glyph width and writing to hmtx table
            XmlNode hmtxNode = xRoot.SelectSingleNode("hmtx");
            XmlNodeList mtxNodes = hmtxNode.SelectNodes("mtx");

            //Parsing glyphs width for using in creating of combined glyph and calculating of total combined glyph width
            List<MtxGlyphInfo> combinedGlyphComponentsMtxGlyphInfo = ParseGlyphWidths(combinedGlyphComponents, mtxNodes);

            //Creating of combined glyph
            TTGlyph combinedGlyph = CreateCombinedGlyph(combinedGlyphComponents, combinedGlyphComponentsMtxGlyphInfo);

            //Calculating of total width of combined glyph
            var totalGlyphWidth = CalculateTotalGlyphWidth(combinedGlyphComponents, combinedGlyphComponentsMtxGlyphInfo);

            //Write all necessary information in  TTX(XML) file
            WriteCombinedGlyphInfoToTTX(xRoot, glyfsNode, hmtxNode, combinedGlyph, totalGlyphWidth);

            //Save created glyph to glyph history for avoidance of repetitions
            SaveGlyphToHistory(combinedGlyph.Name, combinedGlyphComponentNames);

            //Return uni index of created glyph
            return _currentUniIndex;
        }

        private void FindFreeUniIndex(XmlNode glyfsNode)
        {
            XmlNode creationGlyphNode;
            do
            {
                creationGlyphNode = glyfsNode.SelectSingleNode(string.Format("//TTGlyph[@name='{0}']", "uni" + _currentUniIndex.ToString("X")));
                if (creationGlyphNode != null)
                {
                    _currentUniIndex++;
                }
            } while (creationGlyphNode != null);
        }

        private List<TTGlyph> ParsePartGlyphs(XmlNode glyfsNode, string[] combinedGlyphComponentNames)
        {
            List<TTGlyph> partGlyphs = new List<TTGlyph>();
            foreach (string glyphName in combinedGlyphComponentNames)
            {
                XmlNode currentNode = glyfsNode.SelectSingleNode(string.Format("//TTGlyph[@name='{0}']", glyphName));
                if (currentNode == null)
                {
                    throw new Exception("Glyph not found");
                }

                TTGlyph currentGlyph;
                if (glyphName == "space")
                {
                    currentGlyph = new TTGlyph
                    {
                        Name = "space"
                    };
                }
                else
                {
                    currentGlyph = new TTGlyph
                    {
                        Name = glyphName,
                        XMin = int.Parse(currentNode.Attributes["xMin"].Value),
                        YMin = int.Parse(currentNode.Attributes["yMin"].Value),
                        XMax = int.Parse(currentNode.Attributes["xMax"].Value),
                        YMax = int.Parse(currentNode.Attributes["yMax"].Value),
                        Contours = ReadContours(currentNode)
                    };
                }

                partGlyphs.Add(currentGlyph);
            }

            return partGlyphs;
        }

        private Contour[] ReadContours(XmlNode ttGlyphNode)
        {
            XmlNodeList contoursList = ttGlyphNode.SelectNodes("contour");
            Contour[] contours = new Contour[contoursList.Count];

            for (int contourIndex = 0; contourIndex < contoursList.Count; contourIndex++)
            {
                XmlNodeList pointsList = contoursList[contourIndex].SelectNodes("pt");

                Contour currentContour = new Contour();
                currentContour.ContourPoints = new ContourPoint[pointsList.Count];
                for (int pointIndex = 0; pointIndex < pointsList.Count; pointIndex++)
                {
                    currentContour.ContourPoints[pointIndex] = new ContourPoint
                    {
                        X = int.Parse(pointsList[pointIndex].Attributes["x"].Value),
                        Y = int.Parse(pointsList[pointIndex].Attributes["y"].Value),
                        On = int.Parse(pointsList[pointIndex].Attributes["on"].Value)
                    };
                }
                contours[contourIndex] = currentContour;
            }

            return contours;
        }

        private List<MtxGlyphInfo> ParseGlyphWidths(List<TTGlyph> combinedGlyphComponents, XmlNodeList mtxNodes)
        {
            List<MtxGlyphInfo> glyphWidths = new List<MtxGlyphInfo>();
            for (int glyphIndex = 0; glyphIndex < combinedGlyphComponents.Count; glyphIndex++)
            {
                glyphWidths.Add(new MtxGlyphInfo
                {
                    Name = combinedGlyphComponents[glyphIndex].Name,
                    Width = int.Parse(mtxNodes.Cast<XmlNode>().First(node => node.Attributes["name"].Value == combinedGlyphComponents[glyphIndex].Name).Attributes["width"].Value),
                    LSB = int.Parse(mtxNodes.Cast<XmlNode>().First(node => node.Attributes["name"].Value == combinedGlyphComponents[glyphIndex].Name).Attributes["lsb"].Value),
                });
            }

            return glyphWidths;
        }

        private TTGlyph CreateCombinedGlyph(List<TTGlyph> combinedGlyphComponents, List<MtxGlyphInfo> combinedGlyphComponentsMtxGlyphInfo)
        {
            var xGlyphShift = 0;
            var combinedGlyphContourCounter = 0;

            TTGlyph combinedGlyph = new TTGlyph();
            combinedGlyph.Name = "uni" + _currentUniIndex.ToString("X");
            combinedGlyph.Contours = new Contour[combinedGlyphComponents.Sum(x => x.Contours?.Length ?? 0)];

            // ----- Let's process every glyph for combining ----- 
            for (int partGlyphIndex = 0; partGlyphIndex < combinedGlyphComponents.Count; partGlyphIndex++)
            {
                //For space glyph we should just increase shift for x axis
                if (combinedGlyphComponents[partGlyphIndex].Name == "space")
                {
                    xGlyphShift += combinedGlyphComponentsMtxGlyphInfo.First(g => g.Name == "space").Width;
                }
                else
                {
                    //For any other glyph we should process all contours of glyph
                    for (int glyphContourIndex = 0; glyphContourIndex < combinedGlyphComponents[partGlyphIndex].Contours.Length; glyphContourIndex++)
                    {
                        //Initialize new contour for combined glyph
                        combinedGlyph.Contours[combinedGlyphContourCounter] = new Contour();

                        //Initialize contour point count in this contour
                        combinedGlyph.Contours[combinedGlyphContourCounter].ContourPoints = new ContourPoint[combinedGlyphComponents[partGlyphIndex].Contours[glyphContourIndex].ContourPoints.Length];

                        //Process every point of contour and write it value with properly shift
                        for (int contourPointIndex = 0; contourPointIndex < combinedGlyphComponents[partGlyphIndex].Contours[glyphContourIndex].ContourPoints.Length; contourPointIndex++)
                        {
                            //Create new ContourPoint object with shifted x value
                            combinedGlyph.Contours[combinedGlyphContourCounter].ContourPoints[contourPointIndex] = new ContourPoint
                            {
                                X = combinedGlyphComponents[partGlyphIndex].Contours[glyphContourIndex].ContourPoints[contourPointIndex].X + xGlyphShift,
                                Y = combinedGlyphComponents[partGlyphIndex].Contours[glyphContourIndex].ContourPoints[contourPointIndex].Y,
                                On = combinedGlyphComponents[partGlyphIndex].Contours[glyphContourIndex].ContourPoints[contourPointIndex].On
                            };
                        }

                        //Increase counter for combined glyph contours
                        combinedGlyphContourCounter++;
                    }

                    xGlyphShift = combinedGlyph.Contours.Max(c => c?.ContourPoints.Max(cp => cp.X) ?? 0) + (combinedGlyphComponentsMtxGlyphInfo[partGlyphIndex].Width - combinedGlyphComponents[partGlyphIndex].XMax);
                }
            }

            return combinedGlyph;
        }

        private int CalculateTotalGlyphWidth(List<TTGlyph> combinedGlyphComponents, List<MtxGlyphInfo> combinedGlyphComponentsMtxGlyphInfo)
        {
            var totalGlyphWidth = combinedGlyphComponentsMtxGlyphInfo[0].Width;
            for (int i = 1; i < combinedGlyphComponentsMtxGlyphInfo.Count; i++)
            {
                if (combinedGlyphComponents[i].Name == "space")
                {
                    totalGlyphWidth += combinedGlyphComponentsMtxGlyphInfo[i].Width;
                }
                else
                {
                    totalGlyphWidth += combinedGlyphComponents[i].XMax + (combinedGlyphComponentsMtxGlyphInfo[i].Width - combinedGlyphComponents[i].XMax);
                }
            }

            if (totalGlyphWidth > _maxSymbolWidth)
            {
                throw new Exception("Custom glyph width is too big!");
            }

            return totalGlyphWidth;
        }

        private void WriteCombinedGlyphInfoToTTX(XmlNode xRoot, XmlNode glyfsNode, XmlNode hmtxNode, TTGlyph combinedGlyph, int totalGlyphWidth)
        {
            WriteGlyphInfoToGlyphOrderNode(xRoot, combinedGlyph);
            WriteGlyphInfoToHMTXNode(hmtxNode, combinedGlyph, totalGlyphWidth);
            WriteGlyphInfoToCMapNode(xRoot, combinedGlyph);
            WriteGlyphInfoToGlyphNode(glyfsNode, combinedGlyph);                                   
            WriteGlyphInfoToPostNode(xRoot, combinedGlyph);
            WriteGlyphInfoToGDEFNode(xRoot, combinedGlyph);
        }

        private void WriteGlyphInfoToGlyphNode(XmlNode glyfsNode, TTGlyph combinedGlyph)
        {
            XmlNode combinedGlyphNode = ttxFontFile.CreateNode(XmlNodeType.Element, "TTGlyph", "");
            AddAttribute(combinedGlyphNode, "name", combinedGlyph.Name);
            AddAttribute(combinedGlyphNode, "xMin", combinedGlyph.Contours.Min(c => c.ContourPoints.Min(cp => cp.X)).ToString());
            AddAttribute(combinedGlyphNode, "yMin", combinedGlyph.Contours.Min(c => c.ContourPoints.Min(cp => cp.Y)).ToString());
            AddAttribute(combinedGlyphNode, "xMax", combinedGlyph.Contours.Max(c => c.ContourPoints.Max(cp => cp.X)).ToString());
            AddAttribute(combinedGlyphNode, "yMax", combinedGlyph.Contours.Max(c => c.ContourPoints.Max(cp => cp.Y)).ToString());

            for (int contourIndex = 0; contourIndex < combinedGlyph.Contours.Length; contourIndex++)
            {
                XmlNode currentContourNode = ttxFontFile.CreateNode(XmlNodeType.Element, "contour", "");

                for (int contourPointIndex = 0; contourPointIndex < combinedGlyph.Contours[contourIndex].ContourPoints.Length; contourPointIndex++)
                {
                    XmlNode currentPtNode = ttxFontFile.CreateNode(XmlNodeType.Element, "pt", "");

                    XmlAttribute xAttribute = ttxFontFile.CreateAttribute("x");
                    xAttribute.Value = combinedGlyph.Contours[contourIndex].ContourPoints[contourPointIndex].X.ToString();

                    XmlAttribute yAttribute = ttxFontFile.CreateAttribute("y");
                    yAttribute.Value = combinedGlyph.Contours[contourIndex].ContourPoints[contourPointIndex].Y.ToString();

                    XmlAttribute onAttribute = ttxFontFile.CreateAttribute("on");
                    onAttribute.Value = combinedGlyph.Contours[contourIndex].ContourPoints[contourPointIndex].On.ToString();

                    currentPtNode.Attributes.Append(xAttribute);
                    currentPtNode.Attributes.Append(yAttribute);
                    currentPtNode.Attributes.Append(onAttribute);

                    currentContourNode.AppendChild(currentPtNode);
                }

                combinedGlyphNode.AppendChild(currentContourNode);
            }

            XmlNode instructionsNode = ttxFontFile.CreateNode(XmlNodeType.Element, "instructions", "");
            combinedGlyphNode.AppendChild(instructionsNode);

            glyfsNode.AppendChild(combinedGlyphNode);
        }

        private void WriteGlyphInfoToGlyphOrderNode(XmlNode xRoot, TTGlyph combinedGlyph)
        {
            XmlNode glyphOrderNode = xRoot.SelectSingleNode("GlyphOrder");
            XmlNodeList glyphIDNodes = glyphOrderNode.SelectNodes("GlyphID");

            var maxIdValue = glyphIDNodes.Cast<XmlNode>().Select(node => int.Parse(node.Attributes["id"].Value)).Max();

            XmlNode glyphIDNode = ttxFontFile.CreateNode(XmlNodeType.Element, "GlyphID", "");
            AddAttribute(glyphIDNode, "id", maxIdValue + 1);
            AddAttribute(glyphIDNode, "name", combinedGlyph.Name);
            glyphOrderNode.AppendChild(glyphIDNode);
        }

        private void WriteGlyphInfoToHMTXNode(XmlNode hmtxNode, TTGlyph combinedGlyph, int totalGlyphWidth)
        {
            XmlNode mtxNode = ttxFontFile.CreateNode(XmlNodeType.Element, "mtx", "");
            AddAttribute(mtxNode, "name", combinedGlyph.Name);
            AddAttribute(mtxNode, "width", totalGlyphWidth);
            AddAttribute(mtxNode, "lsb", combinedGlyph.Contours.Min(c => c.ContourPoints.Min(cp => cp.X)).ToString());
            hmtxNode.AppendChild(mtxNode);
        }

        private void WriteGlyphInfoToCMapNode(XmlNode xRoot, TTGlyph combinedGlyph)
        {
            XmlNode cmapNode = xRoot.SelectSingleNode("cmap");
            XmlNodeList cmapFormat4Nodes = cmapNode.SelectNodes("cmap_format_4");

            for (int cmapFormat4NodeIndex = 0; cmapFormat4NodeIndex < cmapFormat4Nodes.Count; cmapFormat4NodeIndex++)
            {
                XmlNode mapNode = ttxFontFile.CreateNode(XmlNodeType.Element, "map", "");
                AddAttribute(mapNode, "code", "0x" + _currentUniIndex.ToString("x"));
                AddAttribute(mapNode, "name", combinedGlyph.Name);

                cmapFormat4Nodes[cmapFormat4NodeIndex].AppendChild(mapNode);
            }
        }

        private void WriteGlyphInfoToPostNode(XmlNode xRoot, TTGlyph combinedGlyph)
        {
            XmlNode postNode = xRoot.SelectSingleNode("post");
            XmlNode extraNamesNode = postNode.SelectSingleNode("extraNames");

            XmlNode psNameNode = ttxFontFile.CreateNode(XmlNodeType.Element, "psName", "");
            AddAttribute(psNameNode, "name", combinedGlyph.Name);
            extraNamesNode.AppendChild(psNameNode);
        }

        private void WriteGlyphInfoToGDEFNode(XmlNode xRoot, TTGlyph combinedGlyph)
        {
            XmlNode gdefNode = xRoot.SelectSingleNode("GDEF");
            XmlNode glyphClassDefNode = gdefNode.SelectSingleNode("GlyphClassDef");

            XmlNode classDefNode = ttxFontFile.CreateNode(XmlNodeType.Element, "ClassDef", "");
            AddAttribute(classDefNode, "glyph", combinedGlyph.Name);
            AddAttribute(classDefNode, "class", "1");
            glyphClassDefNode.AppendChild(classDefNode);
        }

        public void SaveChanges(bool replaceFile = true)
        {
            if (replaceFile)
            {
                ttxFontFile.Save(_ttxFilePath);
            }
            else
            {
                ttxFontFile.Save(Path.Combine(Path.GetDirectoryName(_ttxFilePath), Path.GetFileNameWithoutExtension(_ttxFilePath) + "_modified.ttx"));
            }
        }

        #region Glyph history methods

        private void CheckGlyphHistory(string[] glyphArray)
        {
            if (_glyphHistoryDictionary.Any(x => x.Value.SequenceEqual(glyphArray)))
            {
                throw new Exception("Glyph already was created!");
            }
        }
        private void LoadGlyphHistory()
        {
            _glyphHistoryDictionary = new Dictionary<string, string[]>();

            if (File.Exists(_glyphHistoryLocation))
            {
                string[] historyLines = File.ReadAllLines(_glyphHistoryLocation);
                foreach (var historyLine in historyLines)
                {
                    var historyLineParts = historyLine.Split(':');
                    var glyphComponents = historyLineParts[1].Split(',');

                    _glyphHistoryDictionary.Add(historyLineParts[0], glyphComponents);
                }
            }
        }
        private void SaveGlyphToHistory(string glyphName, string[] glyphComponents)
        {
            var saveString = $"{glyphName}:";
            foreach (var glyphComponent in glyphComponents)
            {
                saveString += glyphComponent + ",";
            }
            saveString = saveString.Remove(saveString.Length - 1);
            saveString += Environment.NewLine;

            File.AppendAllText(_glyphHistoryLocation, saveString);


            _glyphHistoryDictionary.Add(glyphName, glyphComponents);
        }
        public void RemoveGlyphHistory()
        {
            File.Delete(_glyphHistoryLocation);
        }

        #endregion

        #region General TTX function

        public void ExecuteTTXConversion(string targetPath)
        {
            var ttxConvertCommand = $"ttx {Path.GetFileName(targetPath)}";
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + ttxConvertCommand);
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.WorkingDirectory = Path.GetDirectoryName(targetPath);
            var process = Process.Start(processInfo);

            process.WaitForExit();

            process.Close();
        }

        #endregion

        #region XML Utils
        
        private void AddAttribute(XmlNode targetNode, string name, int value)
        {
            AddAttribute(targetNode, name, value.ToString());
        }
        private void AddAttribute(XmlNode targetNode, string name, string value)
        {
            XmlAttribute newAttribute = ttxFontFile.CreateAttribute(name);
            newAttribute.Value = value;

            targetNode.Attributes.Append(newAttribute);
        }

        #endregion
    }
}