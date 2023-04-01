using System;
using System.Collections.Generic;

namespace hw3_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path;
            string argument;
            if (args.Length != 2)
            {
                Console.WriteLine(
                    "Few arguments! Enter file path and \"-c\" if you want to compress text\nor \"-u\" if decompress");
                return;
            }

            path = args[0];
            argument = args[1];
            if (argument != "-c" && argument != "-u")
            {
                Console.WriteLine(
                    "Incorrect argument. Enter file path and \"-c\" if you want to compress text\nor \"-u\" if decompress");
                return;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("Incorrect file path!");
                return;
            }

            Lzw lzw = new();
            if (argument == "-c")
            {
                string? text = "";

                using (StreamReader reader = new StreamReader(path))
                {
                    if ((text = await reader.ReadToEndAsync()) == null)
                    {
                        Console.WriteLine("Empty file!");
                        return;
                    }
                }

                bool isEmpty;
                List<Byte[]> listOfCodes = lzw.LzwCompression(text, out isEmpty);
                if (isEmpty)
                {
                    Console.WriteLine("Empty file!");
                    return;
                }

                string newName = path + ".zipped";
                using (FileStream zippedFile = File.Create(newName))
                {
                    if (!File.Exists(newName))
                    {
                        Console.WriteLine("Incorrect file path!");
                        return;
                    }

                    foreach (byte[] code in listOfCodes)
                    {
                        zippedFile.Write(code, 0, code.Length);
                    }
                }

                Console.WriteLine($"Compression ratio: {4 * listOfCodes.Count / (float)(2 * text.Length)}");
                return;
            }

            if (argument == "-u")
            {
                Byte[] allBytes;
                allBytes = File.ReadAllBytes(path);
                if (allBytes.Length == 0)
                {
                    Console.WriteLine("Empty file!");
                    return;
                }

                bool isEmpty;
                bool isCorrect;
                string decompressedText = lzw.LzwDecompression(GroupBytesByFour(allBytes), out isEmpty, out isCorrect);
                if (isEmpty)
                {
                    Console.WriteLine("Empty file!");
                    return;
                }

                if (!isCorrect)
                {
                    Console.WriteLine("Impossible to decompress. Wrong codes");
                }

                string newFileName = path.Replace(".zipped", "");

                StreamWriter unzippedFileWriter = new StreamWriter(newFileName);
                unzippedFileWriter.Write(decompressedText);
                unzippedFileWriter.Close();
            }
        }

        public static List<Byte[]> GroupBytesByFour(Byte[] arrayOfBytes)
        {
            List<Byte[]> listOfBytesByFour = new List<byte[]>();
            for (int i = 0; i < arrayOfBytes.Length; i += 4)
            {
                Byte[] byFour = { arrayOfBytes[i], arrayOfBytes[i + 1], arrayOfBytes[i + 2], arrayOfBytes[i + 3] };
                listOfBytesByFour.Add(byFour);
            }

            return listOfBytesByFour;
        }
    }

    public class Lzw
    {
        private BorStringStorage _storage;

        private void AddAllSymbolsToStorage()
        {
            for (int i = 0; i < 256; i++)
            {
                string symbol = "";
                symbol += (char)i;
                _storage.Add(symbol);
            }
        }

        private void AddAllSymbolsToList(List<string> list)
        {
            for (int i = 0; i < 256; i++)
            {
                string symbol = "";
                symbol += (char)i;
                list.Add(symbol);
            }
        }

        public List<Byte[]> LzwCompression(string? textForCompression, out bool isEmptyText)
        {
            _storage = new BorStringStorage();
            List<Byte[]> codesOfCompressedText = new();
            if (String.IsNullOrEmpty(textForCompression))
            {
                isEmptyText = true;
                return codesOfCompressedText;
            }

            isEmptyText = false;
            AddAllSymbolsToStorage();
            int textForCompressionLenght = textForCompression.Length;
            int code;
            int currentCode = 0; //0 never used because entire alphabet is already in storage
            string currentSuffix = "";
            for (int i = 0; i < textForCompressionLenght; i++)
            {
                currentSuffix += textForCompression[i];
                if (_storage.Contains(currentSuffix, out code))
                {
                    currentCode = code;
                    if (i == textForCompressionLenght - 1) //if last suffix is already in storage
                    {
                        codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    }
                }
                else
                {
                    _storage.Add(currentSuffix);
                    codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    currentSuffix = "";
                    currentSuffix += textForCompression[i];
                    _storage.Contains(currentSuffix, out currentCode); //only one symbol, it will be in storage
                    if (i == textForCompressionLenght - 1) //if last suffix is new 
                    {
                        codesOfCompressedText.Add(BitConverter.GetBytes(Convert.ToUInt32(currentCode)));
                    }
                }
            }

            return codesOfCompressedText;
        }

        public string LzwDecompression(List<Byte[]> compressedMessage, out bool isEmptyText, out bool isCorrectText)
        {
            isCorrectText = true;
            string decompressedText = "";
            int amountOfSuffixesInCompressedText = compressedMessage.Count;
            if (amountOfSuffixesInCompressedText == 0)
            {
                isEmptyText = true;
                return "";
            }

            isEmptyText = false;
            List<string> suffixes = new();
            string lastSuffix = ""; //unnecessary initialization
            AddAllSymbolsToList(suffixes);
            for (int i = 0; i < amountOfSuffixesInCompressedText; i++)
            {
                UInt32 uIntCode = BitConverter.ToUInt32(compressedMessage[i]);
                int intCode = Convert.ToInt32(uIntCode);
                if (intCode < suffixes.Count)
                {
                    decompressedText += suffixes[intCode];
                    if (i > 0)
                    {
                        suffixes.Add(lastSuffix +
                                     suffixes[intCode][0]); //there is warning here if no initialization
                    }

                    lastSuffix = suffixes[intCode];
                }
                else if (intCode == suffixes.Count)
                {
                    lastSuffix = lastSuffix + lastSuffix[0];
                    suffixes.Add(lastSuffix);
                    decompressedText += lastSuffix;
                }
                else
                {
                    isCorrectText = false;
                    return "";
                }
            }

            return decompressedText;
        }
    }

    class BorStringStorage
    {
        class Vertex
        {
            public int NumberOfSuffix;
            public int HowManyDescendants;
            public bool IsTerminal;
            public Vertex[] ArrayOfRelatedVertexes;

            public Vertex(bool isTerminal, Vertex[] arrayOfRelatedVertexes)
            {
                NumberOfSuffix = -1;
                HowManyDescendants = 0;
                this.IsTerminal = isTerminal;
                this.ArrayOfRelatedVertexes = arrayOfRelatedVertexes;
            }
        }

        private int _size = 0;

        public BorStringStorage()
        {
            Vertex[] currArrayOfVertexes = new Vertex[256];
            _root = new Vertex(false, currArrayOfVertexes);
        }

        public int Size {
            get => _size;
            private set { _size = value; }
        }

        private Vertex _root;

        public bool Add(string element)
        {
            bool isNewString;
            isNewString = InsertString(_root, element); //not null because constructs with no null root
            if (isNewString)
            {
                Size = ++Size;
            }

            UpdateDescendantCountIfAdd(element);
            return isNewString;
        }

        private void UpdateDescendantCountIfAdd(string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++) //element was added last vertex terminal
            {
                int codeOfCurrentChar = (int)element[i];
                currVertex.HowManyDescendants++;
                currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
            }

            currVertex.HowManyDescendants++;
            _root.HowManyDescendants = 0;
        }

        private void UpdateDescendantCountIfRemove(string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                if (currVertex == null)
                {
                    return;
                }

                currVertex.HowManyDescendants--;
                int codeOfCurrentChar = (int)element[i];
                currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
            }

            if (currVertex == null)
            {
                return;
            }

            currVertex.HowManyDescendants--;
            _root.HowManyDescendants = 0;
        }

        private bool InsertString(Vertex current, string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                int codeOfCurrentChar = (int)element[i];
                if (currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    bool isTerminal = i == element.Length - 1;
                    Vertex newVertex = new Vertex(isTerminal, new Vertex[256]);
                    currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar] = newVertex;
                    currVertex = newVertex;
                    if (isTerminal)
                    {
                        currVertex.NumberOfSuffix = _size; //size updates later
                        return true;
                    }
                }
                else
                {
                    currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
                    if (i == element.Length - 1)
                    {
                        if (!currVertex.IsTerminal)
                        {
                            currVertex.IsTerminal = true;
                            currVertex.NumberOfSuffix = _size; //size updates later
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool Contains(string element, out int numberOfSuffix)
        {
            numberOfSuffix = -1;
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                int codeOfCurrentChar = (int)element[i];
                if (currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    return false;
                }

                currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
            }

            if (currVertex.IsTerminal)
            {
                numberOfSuffix = currVertex.NumberOfSuffix;
                return true;
            }

            return false;
        }

        public bool Remove(string element)
        {
            bool stringWasInStorage = Remove(_root, element);
            if (stringWasInStorage) Size = --Size;
            if (stringWasInStorage) UpdateDescendantCountIfRemove(element);
            return stringWasInStorage;
        }

        private bool Remove(Vertex root, string element)
        {
            int howManyStartsWithElement = HowManyStartsWithPrefix(element);
            if (howManyStartsWithElement == 0)
            {
                return false;
            }

            Vertex currVertex = root; //not null
            if (howManyStartsWithElement > 1)
            {
                for (int i = 0; i < element.Length; i++)
                {
                    int codeOfCurrentChar = (int)element[i];
                    if (currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar] == null)
                    {
                        return false;
                    }

                    currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
                }

                currVertex.IsTerminal = false;
                currVertex.NumberOfSuffix = -1;
                return true;
            }

            Vertex lastTerminalVertex = root;
            int indexOfUniquePartOfString =
                0; //initialization to fix warning, this variety will be initialized if there is string we want to remove
            for (int i = 0; i < element.Length; i++)
            {
                int codeOfCurrentChar = (int)element[i];
                currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
                if (currVertex.IsTerminal)
                {
                    indexOfUniquePartOfString = i + 1;
                    lastTerminalVertex = currVertex;
                }
            }

            if (indexOfUniquePartOfString == element.Length) return false; //no unique part
            lastTerminalVertex.ArrayOfRelatedVertexes[(int)element[indexOfUniquePartOfString]] = null;
            return true;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            int amountOfStringsWithPrefix;
            Vertex currVertex = _root; //not null
            for (int i = 0; i < prefix.Length; i++)
            {
                int codeOfCurrentChar = (int)prefix[i];
                if (currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    return 0;
                }

                currVertex = currVertex.ArrayOfRelatedVertexes[codeOfCurrentChar];
            }

            amountOfStringsWithPrefix = currVertex.HowManyDescendants;
            return amountOfStringsWithPrefix;
        }
    }
}