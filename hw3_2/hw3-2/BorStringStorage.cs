using System;
namespace hw3_2
{
    class BorStringStorage
    {
        class Vertex
        {
            public int NumberOfSuffix;
            public int HowManyDescendants;
            public bool IsTerminal;
            public Dictionary<char, Vertex> dictionaryOfRelatedVertexes;

            public Vertex(bool isTerminal)
            {
                NumberOfSuffix = -1;
                HowManyDescendants = 0;
                this.IsTerminal = isTerminal;
                dictionaryOfRelatedVertexes = new Dictionary<char, Vertex>();
            }
        }

        public BorStringStorage()
        {
            _root = new Vertex(false);
        }

        public int Size { get; private set; } = 0;

        private Vertex _root;

        public bool Add(string element)
        {
            bool isNewString = InsertString(element); //not null because constructs with no null root
            if (isNewString)
            {
                ++Size;
            }

            UpdateDescendantCountIfAdd(element);
            return isNewString;
        }

        private void UpdateDescendantCountIfAdd(string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++) //element was added last vertex terminal
            {
                currVertex.HowManyDescendants++;
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
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
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
            }

            if (currVertex == null)
            {
                return;
            }

            currVertex.HowManyDescendants--;
            _root.HowManyDescendants = 0;
        }

        private bool InsertString(string element)
        {
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {
                bool isTerminal = i == element.Length - 1;

                if (!currVertex.dictionaryOfRelatedVertexes.ContainsKey(element[i]))
                {
                    currVertex.dictionaryOfRelatedVertexes.Add(element[i], null);
                }

                if (currVertex.dictionaryOfRelatedVertexes[element[i]] == null)
                {
                    var newVertex = new Vertex(isTerminal);
                    currVertex.dictionaryOfRelatedVertexes[element[i]] = newVertex;
                    currVertex = newVertex;
                    if (isTerminal)
                    {
                        currVertex.NumberOfSuffix = Size; //size updates later
                        return true;
                    }
                }
                else
                {
                    currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
                    if (isTerminal)
                    {
                        if (!currVertex.IsTerminal)
                        {
                            currVertex.IsTerminal = true;
                            currVertex.NumberOfSuffix = Size; //size updates later
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public (bool isTerminal, int numberOfSuffix) Contains(string element)
        {
            int numberOfSuffix = -1;
            Vertex currVertex = _root; //not null
            for (int i = 0; i < element.Length; i++)
            {

                if (!currVertex.dictionaryOfRelatedVertexes.ContainsKey(element[i]))
                {
                    currVertex.dictionaryOfRelatedVertexes.Add(element[i], null);
                }

                if (currVertex.dictionaryOfRelatedVertexes[element[i]] == null)
                {
                    return (false, numberOfSuffix);
                }

                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
            }

            if (currVertex.IsTerminal)
            {
                numberOfSuffix = currVertex.NumberOfSuffix;
                return (true, numberOfSuffix);
            }
            return (false, numberOfSuffix);
        }

        public bool Remove(string element)
        {
            bool stringWasInStorage = Remove(_root, element);
            if (stringWasInStorage) --Size;
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

                currVertex.IsTerminal = false;
                currVertex.NumberOfSuffix = -1;
                return true;
            }

            Vertex lastTerminalVertex = root;
            int indexOfUniquePartOfString =
                0; //initialization to fix warning, this variety will be initialized if there is string we want to remove
            for (int i = 0; i < element.Length; i++)
            {
                currVertex = currVertex.dictionaryOfRelatedVertexes[element[i]];
                if (currVertex.IsTerminal)
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

            amountOfStringsWithPrefix = currVertex.HowManyDescendants;
            return amountOfStringsWithPrefix;
        }
    }
}

