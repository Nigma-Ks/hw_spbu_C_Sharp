using System.IO;


namespace hw3_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //string path;
            //string argument;
            //if (args.Length != 2)
            //{
            //    Console.WriteLine(
            //        "Few arguments! Enter file path and \"-c\" if you want to compress text\nor \"-u\" if decompress");
            //    return;
            //}

            //path = args[0];
            //argument = args[1];
            //if (argument != "-c" && argument != "-u")
            //{
            //    Console.WriteLine(
            //        "Incorrect argument. Enter file path and \"-c\" if you want to compress text\nor \"-u\" if decompress");
            //    return;
            //}

            //if (!File.Exists(path))
            //{
            //    Console.WriteLine("Incorrect file path!");
            //    return;
            //}


            Lzw lzw = new();
            string path = "/Users/nigmatulinakseniya/Desktop/test.rtf.zipped";
            //if (argument == "-c")
            //{
            //string? text = File.ReadAllText(path);
            //    {
            //        if (text == null)
            //        {
            //            Console.WriteLine("Empty file!");
            //            return;
            //        }
            //    }

            //    bool isEmpty;
            //    List<Byte[]> listOfCodes = lzw.LzwCompression(text, out isEmpty);
            //    if (isEmpty)
            //    {
            //        Console.WriteLine("Empty file!");
            //        return;
            //    }
            //    string newName = path + ".zipped";
            //    using (FileStream zippedFile = File.Create(newName))
            //    {
            //        if (!File.Exists(newName))
            //        {
            //            Console.WriteLine("Incorrect file path!");
            //            return;
            //        }

            //        foreach (byte[] code in listOfCodes)
            //        {
            //            zippedFile.Write(code, 0, code.Length);
            //        }
            //    }
            //text = File.ReadAllText(newName);
            //Console.WriteLine($"Compression ratio: {4 * listOfCodes.Count / (float)(2 * text.Length)}");
            //    return;
            //}

            //    if (argument == "-u")
            //    {
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
            //if (isEmpty)
            //{
            //    Console.WriteLine("Empty file!");
            //    return;
            //}

            //if (!isCorrect)
            //{
            //    Console.WriteLine("Impossible to decompress. Wrong codes");
            //}

            //string newFileName = path.Replace(".zipped", "");

            //StreamWriter unzippedFileWriter = new StreamWriter(newFileName);
            //unzippedFileWriter.Write(decompressedText);
            //unzippedFileWriter.Close();
        }
        //}

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