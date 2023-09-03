
namespace hw6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            //string path = "//Users//nigmatulinakseniya//Downloads//Map.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Incorrect file path!");
                return;
            }

            Map map = new(File.ReadAllLines(path));

            var eventLoop = new EventLoop();
            var game = new Game(map);

            eventLoop.LeftHandler += game.OnLeft;
            eventLoop.RightHandler += game.OnRight;
            eventLoop.UpHandler += game.OnTop;
            eventLoop.DownHandler += game.OnBottom;
            eventLoop.StartGame += game.Start;
            eventLoop.Run();
        }
    }
}