namespace hw2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BorStringStorage storage = new BorStringStorage();
            Console.WriteLine("This program creates Bor storage, you can choose one of the options:\n" +
                              "1. Add string\n" +
                              "2. Remove string\n" +
                              "3. Find out how many strings are in storage\n" +
                              "4. Find out how many strings in storage starts with your entered string\n" +
                              "5. Find out if storage contains entered string\n" +
                              "6. Exit\n");
            Console.Write("write number 1, 2, 3, 4, 5 or 6: ");
            string choice = Console.ReadLine();
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter string which you want to add: ");
                        string stringToAdd = Console.ReadLine();
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
                        string stringToRemove = Console.ReadLine();
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
                        string prefix = Console.ReadLine();
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
                        string stringForCheckingIfItInStorage = Console.ReadLine();
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
                        Console.WriteLine("You entered something wrong!");
                        break;
                }

                Console.WriteLine("You can choose one of the options:\n" +
                                  "1. Add string\n" +
                                  "2. Remove string\n" +
                                  "3. Find out how many strings are in storage\n" +
                                  "4. Find out how many strings in storage starts with your entered string\n" +
                                  "5. Find out if storage contains entered string\n" +
                                  "6. Exit\n");
                Console.Write("Write number 1, 2, 3, 4, 5 or 6: ");
                choice = Console.ReadLine();
            }
        }
    }

    class BorStringStorage
    {
        class Vertex
        {
            public int howManyDescendants;
            public bool isTerminal;
            public Vertex[] arrayOfRelatedVertexes;

            public Vertex(bool isTerminal, Vertex[] arrayOfRelatedVertexes)
            {
                howManyDescendants = 0;
                this.isTerminal = isTerminal;
                this.arrayOfRelatedVertexes = arrayOfRelatedVertexes;
            }
        }

        private int size = 0;

        public BorStringStorage()
        {
            Vertex[] currArrayOfVertexes = new Vertex[256];
            _root = new Vertex(false, currArrayOfVertexes);
        }

        public int Size
        {
            get { return size; }
            private set { size = value; }
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
                currVertex.howManyDescendants++;
                currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
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
                int codeOfCurrentChar = (int)element[i];
                currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
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
                int codeOfCurrentChar = (int)element[i];
                if (currVertex.arrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    bool isTerminal = i == element.Length - 1;
                    Vertex newVertex = new Vertex(isTerminal, new Vertex[256]);
                    currVertex.arrayOfRelatedVertexes[codeOfCurrentChar] = newVertex;
                    currVertex = newVertex;
                    if (isTerminal) return true;
                }
                else
                {
                    currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
                    if (i == element.Length - 1)
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
                int codeOfCurrentChar = (int)element[i];
                if (currVertex.arrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    return false;
                }

                currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
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
                    int codeOfCurrentChar = (int)element[i];
                    if (currVertex.arrayOfRelatedVertexes[codeOfCurrentChar] == null)
                    {
                        return false;
                    }

                    currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
                }

                currVertex.isTerminal = false;
                return true;
            }

            Vertex lastTerminalVertex = root;
            int indexOfUniquePartOfString =
                0; //initialization to fix warning, this variety will be initialized if there is string we want to remove
            for (int i = 0; i < element.Length; i++)
            {
                int codeOfCurrentChar = (int)element[i];
                currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
                if (currVertex.isTerminal)
                {
                    indexOfUniquePartOfString = i + 1;
                    lastTerminalVertex = currVertex;
                }
            }

            if (indexOfUniquePartOfString == element.Length) return false; //no unique part
            lastTerminalVertex.arrayOfRelatedVertexes[(int)element[indexOfUniquePartOfString]] = null;
            return true;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            int amountOfStringsWithPrefix;
            Vertex currVertex = _root; //not null
            for (int i = 0; i < prefix.Length; i++)
            {
                int codeOfCurrentChar = (int)prefix[i];
                if (currVertex.arrayOfRelatedVertexes[codeOfCurrentChar] == null)
                {
                    return 0;
                }

                currVertex = currVertex.arrayOfRelatedVertexes[codeOfCurrentChar];
            }

            amountOfStringsWithPrefix = currVertex.howManyDescendants;
            return amountOfStringsWithPrefix;
        }
    }
}