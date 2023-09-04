namespace hw6_2
{
    public class Game
    {

        public int CurrPositionX { get; private set; }

        public int CurrPositionY { get; private set; }

        private Map GetMap;

        public Game(Map map)
        {
            GetMap = map;
            CurrPositionX = map.initialStateX;
            CurrPositionY = map.initialStateY;
        }

        public enum Direction
        {
            Up,
            Left,
            Down,
            Right
        }

        private void Move(Direction direction)
        {
            Console.CursorLeft = CurrPositionX;
            Console.CursorTop = CurrPositionY;
            Console.Write(' ');
            Console.CursorLeft = CurrPositionX;
            switch (direction)
            {
                case Direction.Left:
                    Console.CursorLeft = CurrPositionX - 1;
                    CurrPositionX -= 1;
                    Console.Write('@');
                    break;
                case Direction.Up:
                    Console.CursorTop = CurrPositionY - 1;
                    CurrPositionY -= 1;
                    Console.Write('@');
                    break;
                case Direction.Right:
                    Console.CursorLeft = CurrPositionX + 1;
                    CurrPositionX += 1;
                    Console.Write('@');
                    break;
                case Direction.Down:
                    Console.CursorTop = CurrPositionY + 1;
                    CurrPositionY += 1;
                    Console.Write('@');
                    break;
            }
        }

        /// <summary>
        /// Write map on the console.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Start(object? sender, EventArgs args)
        {
            foreach (var line in GetMap._map)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Move character one "step" to the left on the Console if it's possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnLeft(object? sender, EventArgs args)
        {
            if (GetMap._map[CurrPositionY][CurrPositionX - 1] != '*')
            {
                Move(Direction.Left);
            }
        }

        /// <summary>
        /// Move character one "step" to the right on the Console if it's possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnRight(object? sender, EventArgs args)
        {
            if (GetMap._map[CurrPositionY][CurrPositionX + 1] != '*')
            {
                Move(Direction.Right);
            }
        }

        /// <summary>
        /// Move character one "step" up on the Console if it's possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTop(object? sender, EventArgs args)
        {
            if (GetMap._map[CurrPositionY - 1][CurrPositionX] != '*')
            {
                Move(Direction.Up);
            }
        }

        /// <summary>
        /// Move character one "step" down on the Console if it's possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnBottom(object? sender, EventArgs args)
        {
            if (GetMap._map[CurrPositionY + 1][CurrPositionX] != '*')
            {
                Move(Direction.Down);
            }
        }
    }
}

