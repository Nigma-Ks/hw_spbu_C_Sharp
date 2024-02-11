

namespace hw2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            StackCalculatorOfReversePolishNotation calculator = new();
            Console.WriteLine(
                "This program calculates expression in reverse polish notation by stack, you can choose one of the options:\n" +
                "1. Stack on array base\n" +
                "2. Stack on list base\n");
            Console.Write("write number 1 or 2: ");
            string? choice = Console.ReadLine();
            while (choice != "1" && choice != "2")
            {
                Console.WriteLine("You entered something wrong!");
                Console.WriteLine("You can choose one of the options:\n" +
                                  "1. Stack on array base\n" +
                                  "2. Stack on list base\n");
                Console.Write("Write number 1 or 2: ");
                choice = Console.ReadLine();
            }

            Console.Write("Enter expression in reverse polish notation: ");
            string? expression = Console.ReadLine();
            if (expression == null)
            {
                Console.Write("No string! Enter expression in reverse polish notation: ");
                expression = Console.ReadLine();
            }

            if (choice == "1")
            {
                FloatStackBasedOnArray stack = new();
                float? result = calculator.ToCalculate(expression, stack);
                if (result != null)
                {
                    Console.WriteLine($"Result: {result}");
                    return;
                }

                Console.WriteLine("Entered expression was incorrect!");
            }
            else
            {
                FloatStackBasedOnList stack = new();
                float? result = calculator.ToCalculate(expression, stack);
                if (result != null)
                {
                    Console.WriteLine($"Result: {result}");
                    return;
                }

                Console.WriteLine("Entered expression was incorrect!");
            }
        }
    }

}
