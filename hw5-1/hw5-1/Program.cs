using System;
using System.Collections.Generic;

namespace hw5_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string originalTopologyPath;
            string optimizedTopologyPath;
            if (args.Length != 2)
            {
                throw new IncorrectFilesException("There aren't two entered paths there are more or less");
            }

            originalTopologyPath = args[0];
            optimizedTopologyPath = args[1];

            if (!File.Exists(originalTopologyPath) || IsFileNameValid(optimizedTopologyPath))
            {
                throw new IncorrectFilesException("Invalid paths");
            }
            string? topologyFromFile = "";
            
            using (StreamReader reader = new StreamReader(originalTopologyPath))
            {
                if ((topologyFromFile = await reader.ReadToEndAsync()) == null)
                {
                    throw new IncorrectFilesException("Empty file!");
                }
            }
            // Lzw lzw = new();
            // if (argument == "-c")
            // {
            //     string? text = "";
            //
            //     using (StreamReader reader = new StreamReader(path))
            //     {
            //         if ((text = await reader.ReadToEndAsync()) == null)
            //         {
            //             Console.WriteLine("Empty file!");
            //             return;
            //         }
            //     }
            //
            //     bool isEmpty;
            //     List<Byte[]> listOfCodes = lzw.LzwCompression(text, out isEmpty);
            //     if (isEmpty)
            //     {
            //         Console.WriteLine("Empty file!");
            //         return;
            //     }
            //
            //     string newName = path + ".zipped";
            //     using (FileStream zippedFile = File.Create(newName))
            //     {
            //         if (!File.Exists(newName))
            //         {
            //             Console.WriteLine("Incorrect file path!");
            //             return;
            //         }
            //
            //         foreach (byte[] code in listOfCodes)
            //         {
            //             zippedFile.Write(code, 0, code.Length);
            //         }
            //     }
            //
            //     Console.WriteLine($"Compression ratio: {4 * listOfCodes.Count / (float)(2 * text.Length)}");
            //     return;
            // }
            //
            // if (argument == "-u")
            // {
            //     Byte[] allBytes;
            //     allBytes = File.ReadAllBytes(path);
            //     if (allBytes.Length == 0)
            //     {
            //         Console.WriteLine("Empty file!");
            //         return;
            //     }
            //
            //     bool isEmpty;
            //     bool isCorrect;
            //     string decompressedText = lzw.LzwDecompression(GroupBytesByFour(allBytes), out isEmpty, out isCorrect);
            //     if (isEmpty)
            //     {
            //         Console.WriteLine("Empty file!");
            //         return;
            //     }
            //
            //     if (!isCorrect)
            //     {
            //         Console.WriteLine("Impossible to decompress. Wrong codes");
            //     }
            //
            //     string newFileName = path.Replace(".zipped", "");
            //
            //     StreamWriter unzippedFileWriter = new StreamWriter(newFileName);
            //     unzippedFileWriter.Write(decompressedText);
            //     unzippedFileWriter.Close();
            // }
        }
        static bool IsFileNameValid(string fileName)
        {
            if (fileName == null || fileName.Length == 0 ||fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                return false;
            try
            {
                var tempFileInfo = new FileInfo(fileName);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }            
        }
    }

    class Graph
    {
        private List<(int, int, int)> _edges = new(); //start, end, weight
        private List<int[]> _vertexes = new();

        private void ChangeGraphView(string graphInString)
        {
            string[] lines = graphInString.Split('\n');
            foreach (string line in lines)
            {
                string lineWithEdgesOfCurrVertex = GetNumberOfCurrVertex(line);
                string[] stringEdges = lineWithEdgesOfCurrVertex.Split(',');
                foreach (string stringEdge in stringEdges)
                {
                    int edgeValue;
                    int endOfEdge = 0;
                    try
                    {
                        int.TryParse(stringEdge.Substring(0, stringEdge.IndexOf('(')),
                            out endOfEdge);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        throw new IncorrectFilesException("Incorrect lines with edges", e);
                    }
                    bool isCorrectEndOfEdgeValue = int.TryParse(stringEdge.Substring(0, stringEdge.IndexOf('(')),
                        out endOfEdge);
                    try
                    {
                        int.TryParse(stringEdge.Substring(stringEdge.IndexOf('(') + 1, stringEdge.IndexOf(')') - stringEdge.IndexOf('(') + 1),
                            out edgeValue);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        throw new IncorrectFilesException("Incorrect lines with edges", e);
                    }
                    bool isCorrectEdgeValue = int.TryParse(stringEdge.Substring(0, stringEdge.IndexOf('(')),
                        out endOfEdge);
                    if (!isCorrectEdgeValue || !isCorrectEndOfEdgeValue)
                    {
                        throw new IncorrectFilesException("Incorrect lines with edges");
                    }
                }
            }
        }

        private string GetNumberOfCurrVertex(string line)
        {
            string currVertexString = "";
            bool isColonInLine = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':')
                {
                    isColonInLine = true;
                    break;
                }

                currVertexString += line[i];
            }

            if (!isColonInLine)
            {
                throw new IncorrectFilesException("No colon");
            }
            int currVertexInt;
            bool isNumberBeforeColon = int.TryParse(currVertexString, out currVertexInt);
            if (!isNumberBeforeColon)
            {
                throw new IncorrectFilesException("No number before colon");
            }

            int[] vertexAndColour = new[] { currVertexInt, currVertexInt };

            _vertexes.Add(vertexAndColour);
            return line.Replace(currVertexString + ':', "");
        }
        
        public string FindMst(string graphInString)
        {
            return "";
        }
    }
    public class IncorrectFilesException : ArgumentException
    {
        public IncorrectFilesException()
        {
        }

        public IncorrectFilesException(string message) : base(message)
        {
        }

        public IncorrectFilesException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}