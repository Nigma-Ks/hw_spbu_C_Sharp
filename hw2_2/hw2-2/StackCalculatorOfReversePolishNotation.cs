
namespace hw2_2
{
    public class StackCalculatorOfReversePolishNotation
    {
        public const float delta = 0.0001f; //null comparison precision

        private string[] ToParse(string expressionInReversePolishNotation)
        {
            return expressionInReversePolishNotation.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries); //if there more than one spaces
        }

        public float? ToCalculate(string expessionInReversePolishNotation, IStack stack)
        {
            if (expessionInReversePolishNotation == null)
            {
                return null;
            }
            string[] parsedExpression = ToParse(expessionInReversePolishNotation);
            for (int i = 0; i < parsedExpression.Length; i++)
            {
                int currOperand;
                bool isOperand = int.TryParse(parsedExpression[i], out currOperand);
                bool isOperation = IsOperation(parsedExpression[i]);
                if (isOperand)
                {
                    stack.Add((float)currOperand);
                }
                else if (isOperation)
                {
                    var (fstNumber, isFstEmpty) = stack.Remove();
                    if (isFstEmpty)
                    {
                        return null;
                    }

                    var (sndNumber, isSndEmpty) = stack.Remove();
                    if (isSndEmpty)
                    {
                        return null;
                    }

                    switch (parsedExpression[i])
                    {
                        case "+":
                            stack.Add(fstNumber + sndNumber);
                            break;
                        case "/":

                            if (Math.Abs(fstNumber) < delta)
                            {
                                return null;
                            }

                            stack.Add(sndNumber / fstNumber);
                            break;
                        case "-":
                            stack.Add(fstNumber + sndNumber);
                            break;
                        case "*":
                            stack.Add(fstNumber * sndNumber);
                            break;

                    }
                }
                else
                {
                    return null;
                }
            }

            var (result, stackOver) = stack.Remove();
            if (stackOver) //in stack was nothing
            {
                return null;
            }

            var (leftInStack, isOnlyOneLeftInStack) = stack.Remove();
            if (isOnlyOneLeftInStack)
            {
                return result; //in stack was only result
            }

            return null;
        }

        private bool IsOperation(string operation)
        {
            return (operation == "+" || operation == "-" || operation == "*"
                || operation == "/");
        }
    }

}

