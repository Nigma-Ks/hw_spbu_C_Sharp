namespace hw4_1
{
    public class ExpressionTree
    {
        private IVertex Root;

        const float accuracy = 0.001f;

        public interface IVertex
        {
            void PrintVertex();
            float CountVertexValue();
            string PrintedVertexString();
        }

        public class OperationVertex : IVertex
        {
            public string Operation;
            public IVertex LeftOperand;
            public IVertex RightOperand;

            public OperationVertex(string operation, IVertex left, IVertex right)
            {
                Operation = operation;
                LeftOperand = left;
                RightOperand = right;
            }

            public void PrintVertex()
            {
                Console.Write("( ");
                LeftOperand.PrintVertex();
                Console.Write(Operation + " ");
                RightOperand.PrintVertex();
                Console.Write(") ");
            }

            public string PrintedVertexString()
            {
                return "(" + Operation + LeftOperand.PrintedVertexString() + RightOperand.PrintedVertexString() + ")";
            }
            public float CountVertexValue()
            {
                switch (Operation)
                {
                    case "+":
                        return LeftOperand.CountVertexValue() + RightOperand.CountVertexValue();
                    case "-":
                        return LeftOperand.CountVertexValue() - RightOperand.CountVertexValue();
                    case "*":
                        return LeftOperand.CountVertexValue() * RightOperand.CountVertexValue();
                    case "/":
                        float divider = RightOperand.CountVertexValue();

                        if (divider < accuracy)
                        {
                            throw new DivideByZeroException();
                        }

                        return LeftOperand.CountVertexValue() / RightOperand.CountVertexValue();
                    default:
                        throw new IncorrectOperationException();
                }
            }
        }
        public class OperandVertex : IVertex
        {
            public int Value;

            public OperandVertex(int value)
            {
                Value = value;
            }

            public void PrintVertex()
            {
                Console.Write($"{Value} ");
            }
            public float CountVertexValue()
            {
                return Value;
            }
            public string PrintedVertexString()
            {
                return Value.ToString();
            }
        }

        private bool RightBracketsInExpression(string expression)
        {
            string rightBracketExpression = ConstructRightBracketsExpression();
            expression = expression.Replace(" ", "");
            return String.Compare(expression, rightBracketExpression) == 0;
        }

        public bool IsOperator(string symbol)
        {
            return symbol == "+" || symbol == "-" || symbol == "*" || symbol == "/";
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
            Stack<IVertex> stack = new();
            for (int i = parsedExpression.Length - 1; i >= 0; i--)
            {
                string operandOrOperatorInExpression = parsedExpression[i];
                if (IsOperator(operandOrOperatorInExpression))
                {
                    IVertex left;
                    try
                    {
                        left = stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        throw new Exception("Outer Exception", e);
                    }

                    IVertex right;
                    try
                    {
                        right = stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        throw new Exception("Outer Exception", e);
                    }

                    OperationVertex operationVertex = new(operandOrOperatorInExpression, left, right);
                    stack.Push(operationVertex);
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
                    throw new Exception("Outer exception", e);
                }
            }

            IVertex resultTree;
            try
            {
                resultTree = stack.Pop();
            }
            catch (InvalidOperationException e)
            {
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
            Root.PrintVertex();
        }

        private string ConstructRightBracketsExpression()
        {
            return Root.PrintedVertexString();
        }

        public float CalculateExpression()
        {
            return Root.CountVertexValue();
        }
    }
}

