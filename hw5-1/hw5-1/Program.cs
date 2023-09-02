namespace hw5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            string originalTopologyPath;
            string optimizedTopologyPath;
            if (args.Length != 2)
            {
                throw new IncorrectFileException("There aren't two entered paths there are more or less");
            }

            originalTopologyPath = args[0];
            optimizedTopologyPath = args[1];

            if (!File.Exists(originalTopologyPath))
            {
                throw new IncorrectFileException("Invalid path");
            }

            string topologyFromFile = "";

            using (StreamReader reader = new StreamReader(originalTopologyPath))
            {
                if ((topologyFromFile = reader.ReadToEnd()) == null)
                {
                    throw new IncorrectFileException("Empty file!");
                }
            }

            string tableOfOptimizedGraph = graph.FindMst(topologyFromFile);

            if (!File.Exists(optimizedTopologyPath))
            {
                using (FileStream optimizedGraphFile = File.Create(optimizedTopologyPath))
                {
                    if (!File.Exists(optimizedTopologyPath))
                    {
                        Console.WriteLine("Incorrect file path!");
                        return;
                    }

                    StreamWriter sw = new StreamWriter(optimizedTopologyPath);
                    sw.Write(tableOfOptimizedGraph);
                    sw.Close();
                }
            }
        }
    }

    public class IncorrectFileException : ArgumentException
    {
        public IncorrectFileException()
        {
        }

        public IncorrectFileException(string message) : base(message)
        {
        }

        public IncorrectFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
