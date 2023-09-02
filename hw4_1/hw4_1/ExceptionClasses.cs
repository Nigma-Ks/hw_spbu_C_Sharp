
namespace hw4_1
{
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
    public class IncorrectOperationException : ArgumentException
    {
        public IncorrectOperationException()
        {
        }

        public IncorrectOperationException(string message) : base(message)
        {
        }

        public IncorrectOperationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

