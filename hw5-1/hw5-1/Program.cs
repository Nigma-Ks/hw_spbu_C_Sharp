namespace hw5_1
{
    class Program
    {
        static async Task Main(string[] args)
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
                if ((topologyFromFile = await reader.ReadToEndAsync()) == null)
                {
                    reader.Close();
                    throw new IncorrectFileException("Empty file!");
                }

                reader.Close();
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

                    optimizedGraphFile.Close();
                    StreamWriter sw = new StreamWriter(optimizedTopologyPath);
                    sw.Write(tableOfOptimizedGraph);
                    sw.Close();
                }
            }
        }
    }

    public class Graph
    {
        private SortedDictionary<int, List<(int endEdge, int weight)>> _mst = new();
        private List<(int edgeValue, int begEdge, int endEdge)> _edges = new(); //start, end, weight
        private Dictionary<int, int> _vertexesWithColour = new();

        private void ChangeGraphView(string graphInString)
        {
            string[] lines = graphInString.Split('\n');
            foreach (string line in lines)
            {
                int begEdge;
                string lineWithoutBeg = GetNumberOfCurrVertex(line, out begEdge);
                GetEdgesAndValuesFromString(lineWithoutBeg, begEdge);
            }
        }

        private void GetEdgesAndValuesFromString(string line, int begEdge)
        {
            string[] stringEdges = line.Split(',');
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
                    throw new IncorrectFileException("Incorrect lines with edges", e);
                }

                bool isCorrectEndOfEdgeValue = int.TryParse(stringEdge.Substring(0, stringEdge.IndexOf('(')),
                    out endOfEdge);
                try
                {
                    int.TryParse(
                        stringEdge.Substring(stringEdge.IndexOf('(') + 1,
                            stringEdge.IndexOf(')') - stringEdge.IndexOf('(') - 1),
                        out edgeValue);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new IncorrectFileException("Incorrect lines with edges", e);
                }

                bool isCorrectEdgeValue = int.TryParse(stringEdge.Substring(0, stringEdge.IndexOf('(')),
                    out endOfEdge);
                _vertexesWithColour.TryAdd(endOfEdge, endOfEdge);
                if (!isCorrectEdgeValue || !isCorrectEndOfEdgeValue)
                {
                    throw new IncorrectFileException("Incorrect lines with edges");
                }

                _edges.Add((edgeValue, begEdge, endOfEdge));
            }
        }

        private string GetNumberOfCurrVertex(string line, out int begEdge)
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
                throw new IncorrectFileException("No colon");
            }

            int currVertexInt;
            bool isNumberBeforeColon = int.TryParse(currVertexString, out currVertexInt);
            if (!isNumberBeforeColon)
            {
                throw new IncorrectFileException("No number before colon");
            }

            begEdge = currVertexInt;
            _vertexesWithColour.TryAdd(currVertexInt, currVertexInt);
            return line.Replace(currVertexString + ':', "");
        }

        private void RecolourVertexes(int oldColour, int newColour)
        {
            foreach (var vertex in _vertexesWithColour.Keys)
            {
                if (_vertexesWithColour[vertex] == oldColour)
                {
                    _vertexesWithColour[vertex] = newColour;
                }
            }
        }

        private bool Merge(int begin, int end)
        {
            if (_vertexesWithColour[begin] != _vertexesWithColour[end])
            {
                RecolourVertexes(_vertexesWithColour[begin], _vertexesWithColour[end]);
                return true;
            }

            return false;
        }

        private void AddNewEdgeToMst((int edgeValue, int begEdge, int endEdge) edge)
        {
            if (edge.begEdge > edge.endEdge)
            {
                if (_mst.ContainsKey(edge.endEdge))
                {
                    _mst[edge.endEdge].Add((edge.begEdge, edge.edgeValue));
                }
                else
                {
                    List<(int, int)> relatedVertexes = new();
                    relatedVertexes.Add((edge.begEdge, edge.edgeValue));
                    _mst.TryAdd(edge.endEdge, relatedVertexes);
                }

                return;
            }

            if (_mst.ContainsKey(edge.begEdge))
            {
                _mst[edge.begEdge].Add((edge.endEdge, edge.edgeValue));
            }
            else
            {
                List<(int, int)> relatedVertexes = new();
                relatedVertexes.Add((edge.endEdge, edge.edgeValue));
                _mst.TryAdd(edge.begEdge, relatedVertexes);
            }
        }

        private string GetGraphInString()
        {
            string graphInString = "";
            int amountOfPairs = _mst.Count;
            foreach (var pair in _mst)
            {
                pair.Value.Sort();
                graphInString += "" + pair.Key + ": ";
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    string end = "" + pair.Value[i].endEdge;
                    string weight = "" + pair.Value[i].weight;
                    if (i != pair.Value.Count - 1)
                    {
                        graphInString += "" + end + " (" + weight + "), ";
                    }
                    else
                    {
                        graphInString += "" + end + " (" + weight + ")";
                    }
                }

                graphInString += '\n';
            }

            return graphInString;
        }

        public string FindMst(string graphInString)
        {
            ChangeGraphView(graphInString);
            var _sortedEdges = _edges.OrderBy(edge => -edge.edgeValue).ToArray();
            int amountOfEdgesInMst = 0;
            foreach (var edge in _sortedEdges)
            {
                if (Merge(edge.begEdge, edge.endEdge))
                {
                    AddNewEdgeToMst(edge);
                    amountOfEdgesInMst += 1;
                }
            }

            if (amountOfEdgesInMst != _vertexesWithColour.Count - 1)
            {
                throw new IncorrectFileException("Disconnected topology!");
            }

            string result = GetGraphInString().TrimEnd('\n');
            return result;
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
