namespace hw4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine("Incorrect file path!");
                return;
            }

            string? expression = File.ReadAllText(path);
            {
                if (expression == null)
                {
                    Console.WriteLine("Empty file!");
                    return;
                }
            }


            Console.WriteLine("Expression in file: " + expression);

            ExpressionTree tree;

            try
            {
                tree = new ExpressionTree(expression);
            }
            catch (Exception e)
            {
                Console.WriteLine("File is incorrect!");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("StackTrace:\n" + e.StackTrace);
                return;
            }

            Console.Write("Expression in expression tree: ");
            tree.PrintExpressionInTree();
            float result;
            try
            {
                result = tree.CalculateExpression();
            }
            catch (Exception e)
            {
                Console.WriteLine("File is incorrect!");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("StackTrace:\n" + e.StackTrace);
                return;
            }

            Console.WriteLine("\nResult: " + result);
            Console.WriteLine();
        }
    }
}