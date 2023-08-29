namespace hw2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BorStringStorage storage = new();
            PrintStorageCommands();
            Console.Write("Write number 1, 2, 3, 4, 5 or 6: ");
            string? choice = Console.ReadLine();
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter string which you want to add: ");
                        string? stringToAdd = Console.ReadLine();
                        if (stringToAdd == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringToAdd = Console.ReadLine();
                        }

                        bool isNewString = storage.Add(stringToAdd);
                        if (isNewString)
                        {
                            Console.WriteLine("String successfully added!");
                        }
                        else
                        {
                            Console.WriteLine("This string already in storage!");
                        }

                        break;
                    case "2":
                        Console.Write("Enter string which you want to remove: ");
                        string? stringToRemove = Console.ReadLine();
                        if (stringToRemove == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringToRemove = Console.ReadLine();
                        }

                        bool wasInStorage = storage.Remove(stringToRemove);
                        if (wasInStorage)
                        {
                            Console.WriteLine("String successfully removed!");
                        }
                        else
                        {
                            Console.WriteLine("This string wasn't in storage!");
                        }

                        break;
                    case "3":
                        Console.WriteLine($"Strings in storage: {storage.Size}");
                        break;
                    case "4":
                        Console.Write("Enter prefix: ");
                        string? prefix = Console.ReadLine();
                        if (prefix == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            prefix = Console.ReadLine();
                        }

                        Console.WriteLine(
                            $"There are {storage.HowManyStartsWithPrefix(prefix)} strings which start with this prefix");
                        break;
                    case "5":
                        Console.Write("Enter string which you want check if it is in storage: ");
                        string? stringForCheckingIfItInStorage = Console.ReadLine();
                        if (stringForCheckingIfItInStorage == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringForCheckingIfItInStorage = Console.ReadLine();
                        }

                        bool isInStorage = storage.Contains(stringForCheckingIfItInStorage);
                        if (isInStorage)
                        {
                            Console.WriteLine("There is entered string in storage!");
                        }
                        else
                        {
                            Console.WriteLine("There isn't entered string in storage!");
                        }

                        break;
                    default:
                        Console.WriteLine("\nYou entered something wrong!");
                        break;
                }

                PrintStorageCommands();
                Console.Write("Write number 1, 2, 3, 4, 5 or 6: ");
                choice = Console.ReadLine();
            }
        }

        static void PrintStorageCommands()
        {
            Console.WriteLine("You can choose one of the options:\n" +
                                  "1. Add string\n" +
                                  "2. Remove string\n" +
                                  "3. Find out how many strings are in storage\n" +
                                  "4. Find out how many strings in storage starts with your entered string\n" +
                                  "5. Find out if storage contains entered string\n" +
                                  "6. Exit\n");
        }
    }

    class BorStringStorage
    {
        class Vertex
        {
            public int howManyDescendants;
            public bool isTerminal;
            public Dictionary<char, Vertex> dictionaryOfRelatedVertexes;

            public Vertex(bool isTerminal)
            {
                howManyDescendants = 0;
                this.isTerminal = isTerminal;
                dictionaryOfRelatedVertexes = new Dictionary<char, Vertex>();
            }
        }

        private int size = 0;

        public BorStringStorage()
        {
            _root = new Vertex(false);
        }

        public int Size { get; private set; } = 0;

        private Vertex _root;

        public bool Add(string element)
        {
            bool isNewString = InsertString(_root, element); //not null because constructs with no null root
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
                currVertex.howManyDescendants++;
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
            }

            currVertex.howManyDescendants++;
            _root.howManyDescendants = 0;
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

                currVertex.howManyDescendants--;
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
            }

            if (currVertex == null)
            {
                return;
            }

            currVertex.howManyDescendants--;
            _root.howManyDescendants = 0;
        }

        private bool InsertString(Vertex current, string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                bool isTerminal = i == element.Length - 1;
                if (currVertex.dictionaryOfRelatedVertexes[element[i]] == null)
                {
                    var newVertex = new Vertex(isTerminal);
                    currVertex.dictionaryOfRelatedVertexes[element[i]] = newVertex;
                    currVertex = newVertex;
                    if (isTerminal) return true;
                }
                else
                {
                    currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
                    if (isTerminal)
                    {
                        if (!currVertex.isTerminal)
                        {
                            currVertex.isTerminal = true;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool Contains(string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                if (currVertex.dictionaryOfRelatedVertexes[element[i]] == null)
                {
                    return false;
                }

                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
            }

            return currVertex.isTerminal;
        }

        public bool Remove(string element)
        {
            bool stringWasInStorage = Remove(_root, element);
            if (stringWasInStorage) Size = --Size;
            if (stringWasInStorage) UpdateDescendantCountIfRemove(element);
            return stringWasInStorage;
        }

        private bool Remove(Vertex root, string element) //добавить проверку на существоаание слова с таким суффиксом
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
                    if (currVertex.dictionaryOfRelatedVertexes[element[i]] == null)
                    {
                        return false;
                    }

                    currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
                }

                currVertex.isTerminal = false;
                return true;
            }

            Vertex lastTerminalVertex = root;
            int indexOfUniquePartOfString =
                0; //initialization to fix warning, this variety will be initialized if there is string we want to remove
            for (int i = 0; i < element.Length; i++)
            {
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
                if (currVertex.isTerminal)
                {
                    indexOfUniquePartOfString = i + 1;
                    lastTerminalVertex = currVertex;
                }
            }

            if (indexOfUniquePartOfString == element.Length) return false; //no unique part
            lastTerminalVertex.dictionaryOfRelatedVertexes[element[indexOfUniquePartOfString]] = null;
            return true;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            int amountOfStringsWithPrefix;
            Vertex currVertex = _root; //not null
            for (int i = 0; i < prefix.Length; i++)
            {
                if (currVertex.dictionaryOfRelatedVertexes[prefix[i]] == null)
                {
                    return 0;
                }

                currVertex = currVertex.dictionaryOfRelatedVertexes[prefix[i]];
            }

            amountOfStringsWithPrefix = currVertex.howManyDescendants;
            return amountOfStringsWithPrefix;
        }
    }
}