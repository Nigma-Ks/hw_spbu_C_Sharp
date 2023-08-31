

namespace hw3_2
{
    class Program
    {
        static void Main(string[] args)
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
                string? text = File.ReadAllText(path);
                {
                    if (text == null)
                    {
                        Console.WriteLine("Empty file!");
                        return;
                    }
                }

                bool isEmptyText;
                (List<Byte[]> listOfCodes, isEmptyText) = lzw.LzwCompression(text);

                if (isEmptyText)
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
                float ratio = CompressionCoefficientCounter(listOfCodes.Count, text.Length);
                Console.WriteLine($"Compression ratio: {ratio}");
                return;
            }
            Byte[] allBytes;
            allBytes = File.ReadAllBytes(path);
            if (allBytes.Length == 0)
            {
                Console.WriteLine("Empty file!");
                return;
            }

            var (decompressedText, isEmpty, isCorrect) = lzw.LzwDecompression(GroupBytesByFour(allBytes));
            if (isEmpty)
            {
                Console.WriteLine("Empty file!");
                return;
            }

            if (!isCorrect)
            {
                Console.WriteLine("Impossible to decompress. Wrong codes");
            }

            string newFileName = NewNameForDecompressedFile(path);

            StreamWriter unzippedFileWriter = new StreamWriter(newFileName);
            unzippedFileWriter.Write(decompressedText);
            unzippedFileWriter.Close();
        }

        public static float CompressionCoefficientCounter(int amountOfBytesInCompressedText, int amountOfSymbolsInText)
        {
            return amountOfBytesInCompressedText * sizeof(UInt32) / ((float)amountOfSymbolsInText * sizeof(char));
        }

        public static string NewNameForDecompressedFile(string oldName)
        {
            if (oldName.LastIndexOf(".zipped") != oldName.Length - 1 - 6) //6- amount of chars in ".zipped" + 1
            {
                return oldName + ".unzipped";
            }
            oldName += '?'; //недопустимый символ в path
            return oldName.Replace(".zipped?", "");
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

}