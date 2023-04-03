namespace hw4_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = "..\\..\\..\\..\\hw4_1.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Incorrect file path!");
                return;
            }

            string expression;
            using (StreamReader reader = new StreamReader(path))
            {
                if ((expression = await reader.ReadToEndAsync()) == null)
                {
                    Console.WriteLine("Empty file!");
                    reader.Close();
                    return;
                }
            }
            
            Console.WriteLine("Expression in file: " + expression);
            try
            {
                ExpressionTree tree = new ExpressionTree(expression);
            }
            catch (Exception)
            {
                Console.WriteLine("File is incorrect!");
                return;
            }

            ExpressionTree newCorrectTree = new ExpressionTree(expression);
            Console.Write("Expression in expression tree: ");
            newCorrectTree.PrintExpressionInTree();
            try
            {
                newCorrectTree.CalculateExpression();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("\nDivision by zero!");
                Console.WriteLine("e.StackTrace:\n" + e.StackTrace);
                return;
            }

            float result = newCorrectTree.CalculateExpression();
            Console.WriteLine("\nResult: " + result);
            Console.WriteLine();
        }
    }

    public class ExpressionTree
    {
        private OperandVertex Root;

        public bool IsOperator(string symbol)
        {
            return symbol == "+" || symbol == "-" || symbol == "*" || symbol == "/";
        }

        public class OperationVertex
        {
            public string Operation;
            public OperandVertex LeftOperand;
            public OperandVertex RightOperand;

            public OperationVertex(string operation, OperandVertex left, OperandVertex right)
            {
                Operation = operation;
                LeftOperand = left;
                RightOperand = right;
            }

            public void PrintOperator()
            {
                Console.Write("( ");
                LeftOperand.PrintOperand(LeftOperand);
                Console.Write(Operation + " ");
                RightOperand.PrintOperand(RightOperand);
                Console.Write(") ");
            }
        }

        public class OperandVertex
        {
            public int Value;
            public OperationVertex? Operation;

            public OperandVertex(int value)
            {
                Value = value;
                Operation = null;
            }

            public OperandVertex(OperationVertex operation)
            {
                Operation = operation;
                Value = 0;
            }

            public void PrintOperand(OperandVertex? root)
            {
                if (root == null)
                {
                    return;
                }

                if (root.Operation != null)
                {
                    root.Operation.PrintOperator();
                    return;
                }

                Console.Write($"{root.Value} ");
            }
        }

        private bool RightBracketsInExpression(string expression)
        {
            string rightBracketExpression = ConstructRightBracketsExpression();
            expression = expression.Replace(" ", "");
            return String.Compare(expression, rightBracketExpression) == 0;
        }
        public ExpressionTree(string expression)
        {
            int currOperand;
            bool notEmptyStack;
            if (expression == null)
            {
                throw new Exception("Empty expression", new IncorrectExpressionException());
            }
            
            string[] parsedExpression =
                expression.Split(new char[] { '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Stack<OperandVertex> stack = new();
            for (int i = parsedExpression.Length - 1; i >= 0; i--)
            {
                string operandOrOperatorInExpression = parsedExpression[i];
                if (IsOperator(operandOrOperatorInExpression))
                {
                    OperandVertex left;
                    try
                    {
                        left = stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("File was incorrect!");
                        Console.WriteLine("Message: " + e.Message);
                        Console.WriteLine("StackTrace:\n" + e.StackTrace);
                        throw new Exception("Outer exception", e);
                    }

                    OperandVertex right;
                    try
                    {
                        right = stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Message: " + e.Message);
                        Console.WriteLine("StackTrace:\n" + e.StackTrace);
                        throw new Exception("Outer exception", e);
                    }

                    OperationVertex operationVertex = new(operandOrOperatorInExpression, left, right);
                    OperandVertex operandVertex = new(operationVertex);
                    stack.Push(operandVertex);
                }
                else if (Int32.TryParse(operandOrOperatorInExpression, out currOperand))
                {
                    OperandVertex newOperand = new(currOperand);
                    stack.Push(newOperand);
                }
                else
                {
                    WrongSymbolsInExpressionException e =
                        new WrongSymbolsInExpressionException("Incorrect numbers in file!");
                    Console.WriteLine("Message: " + e.Message);
                    Console.WriteLine("StackTrace:\n" + e.StackTrace);
                    throw new Exception("Outer exception", e);
                }
            }

            OperandVertex resultTree;
            try
            {
                resultTree = stack.Pop();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("File was incorrect!");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("StackTrace:\n" + e.StackTrace);
                throw new Exception("Outer exception", e);
            }
            Root = resultTree;

            if (!RightBracketsInExpression(expression))
            {
                IncorrectExpressionException exception = new IncorrectExpressionException("Incorrect Brackets");
                throw new Exception("Outer exception", exception);
            }
        }

        public void PrintExpressionInTree()
        {
            Root.PrintOperand(Root);
        }

        private string RecursiveConstruct(OperandVertex root)
        {
            if (root == null)
            {
                return "";
            }

            if (root.Operation != null)
            {
                return "(" + root.Operation.Operation  + RecursiveConstruct(root.Operation.LeftOperand) + RecursiveConstruct(root.Operation.RightOperand) + ")";
            }

            return root.Value.ToString();
        }
        private string ConstructRightBracketsExpression()
        {
            return RecursiveConstruct(Root);
        }

        private float RecursiveCalculation(OperandVertex root)
        {
            if (root.Operation != null)
            {
                switch (root.Operation.Operation)
                {
                    case "+":
                        return RecursiveCalculation(root.Operation.LeftOperand) +
                               RecursiveCalculation(root.Operation.RightOperand);
                    case "-":
                        return RecursiveCalculation(root.Operation.LeftOperand) -
                               RecursiveCalculation(root.Operation.RightOperand);
                    case "*":
                        return RecursiveCalculation(root.Operation.LeftOperand) *
                               RecursiveCalculation(root.Operation.RightOperand);
                    case "/":
                        int? divider = root.Operation.RightOperand.Value;
                        if (divider == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        return RecursiveCalculation(root.Operation.LeftOperand) /
                               RecursiveCalculation(root.Operation.RightOperand);
                }
            }

            return (float)root.Value;
        }

        public float CalculateExpression()
        {
            return RecursiveCalculation(Root);
        }
    }


    public class WrongSymbolsInExpressionException : ArgumentException
    {
        public WrongSymbolsInExpressionException()
        {
        }

        public WrongSymbolsInExpressionException(string message) : base(message)
        {
        }

        public WrongSymbolsInExpressionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class IncorrectExpressionException : ArgumentException
    {
        public IncorrectExpressionException()
        {
        }

        public IncorrectExpressionException(string message) : base(message)
        {
        }

        public IncorrectExpressionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}