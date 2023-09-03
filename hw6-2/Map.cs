namespace hw6_2
{
    public class Map
    {
        public int initialStateX;
        public int initialStateY;
        const int minSizeOfMap = 3;
        public string[] _map;
        private int _lenghtOfSide;

        public Map(string[] map)
        {
            _map = map;
            if (_map.Length == 0)
            {
                throw new ArgumentException("Map is empty!");
            }

            if (!isRectangle())
            {
                throw new ArgumentException("Map isn't rectangle!");
            }

            if (!isBigEnough())
            {
                throw new ArgumentException("Map is too small!");
            }


            var amountOfCharectersOnMap = 0;

            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _lenghtOfSide; j++)
                {
                    if (_map[i][j] == '@')
                    {
                        initialStateX = j;
                        initialStateY = i;
                        amountOfCharectersOnMap++;
                        if (amountOfCharectersOnMap > 1)
                        {
                            throw new ArgumentException("More than one character on the map");
                        }
                    }
                    if (_map[i][j] != '*' && _map[i][j] != ' ' && _map[i][j] != '@')
                    {
                        throw new ArgumentException("Wrong symbols on the map");
                    }
                }
            }

            if (!isMapLimited())
            {
                throw new ArgumentException("Map isn't limited!");
            }


        }
        private bool isRectangle()
        {
            int lengthOfSide = _map[0].Length;
            foreach (var line in _map)
            {
                if (line.Length != lengthOfSide)
                {
                    return false;
                }
            }
            _lenghtOfSide = lengthOfSide;
            return true;
        }

        private bool isBigEnough()
        {
            int lengthOfSide = _map[0].Length;

            if (lengthOfSide < minSizeOfMap)
            {
                return false;
            }

            return _map.Length > minSizeOfMap;
        }

        private bool isMapLimited()
        {
            for (int i = 0; i < _lenghtOfSide; i++)
            {
                if (_map[0][i] != '*' || _map[_map.Length - 1][i] != '*')
                {
                    return false;
                }
            }
            for (int i = 0; i < _map.Length; i++)
            {
                if (_map[i][0] != '*' || _map[i][_lenghtOfSide - 1] != '*')
                {
                    return false;
                }
            }
            return true;
        }
    }
}

