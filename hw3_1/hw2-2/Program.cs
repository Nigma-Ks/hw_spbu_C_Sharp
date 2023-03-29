using System;
using System.Collections.Generic;

namespace hw2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            StackCalculatorOfReversePolishNotation calculator = new();
            bool correctNotation;
            Console.WriteLine(
                "This program calculates expression in reverse polish notation by stack, you can choose one of the options:\n" +
                "1. Stack on array base\n" +
                "2. Stack on list base\n");
            Console.Write("write number 1 or 2: ");
            string choice = Console.ReadLine();
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
            string expression = Console.ReadLine();
            if (expression == null)
            {
                Console.Write("No string! Enter expression in reverse polish notation: ");
                expression = Console.ReadLine();
            }

            if (choice == "1")
            {
                FloatStackBasedOnArray stack = new();
                float result = calculator.ToCalculate(expression, stack, out correctNotation);
                if (correctNotation)
                {
                    Console.WriteLine($"Result: {result}");
                    return;
                }

                Console.WriteLine("Entered expression was incorrect!");
            }
            else
            {
                FloatStackBasedOnList stack = new();
                float result = calculator.ToCalculate(expression, stack, out correctNotation);
                if (correctNotation)
                {
                    Console.WriteLine($"Result: {result}");
                    return;
                }

                Console.WriteLine("Entered expression was incorrect!");
            }
        }
    }

    public class FloatStackBasedOnArray : IStack
    {
        private const int defaultArraySize = 20;

        private float[] _ArrayOfOperands;

        private int size = 0;

        public int Size { get; private set; }

        public FloatStackBasedOnArray()
        {
            _ArrayOfOperands = new float[defaultArraySize];
        }

        private float[] ExpandArrayOnDefaultSize(float[] arrayToExpand)
        {
            float[] newExpandedArray = new float[arrayToExpand.Length + defaultArraySize];
            for (int i = 0; i < arrayToExpand.Length; i++)
            {
                newExpandedArray[i] = arrayToExpand[i];
            }

            return newExpandedArray;
        }

        public void Add(float value)
        {
            if (Size == _ArrayOfOperands.Length)
            {
                _ArrayOfOperands = ExpandArrayOnDefaultSize(_ArrayOfOperands);
            }

            _ArrayOfOperands[Size] = value;
            Size = ++Size;
        }

        public float Remove(out bool isEmpty)
        {
            isEmpty = false;
            if (Size < 1)
            {
                isEmpty = true;
                return 0;
            }

            float value = _ArrayOfOperands[Size - 1]; //get head of the stack
            _ArrayOfOperands[Size - 1] = 0;
            Size--;
            return value;
        }
    }

    public class FloatStackBasedOnList : IStack
    {
        private List<float> _listOfOperands;

        private int size = 0;

        public int Size { get; private set; }

        public FloatStackBasedOnList()
        {
            _listOfOperands = new List<float>();
        }

        public void Add(float value)
        {
            _listOfOperands.Add(value);
            Size = ++Size;
        }

        public float Remove(out bool isEmpty)
        {
            isEmpty = false;
            if (Size < 1)
            {
                isEmpty = true;
                return 0;
            }

            float value = _listOfOperands[Size - 1]; //get head of the stack
            _listOfOperands.RemoveAt(Size - 1);
            Size--;
            return value;
        }
    }


    public interface IStack
    {
        void Add(float value);

        float Remove(out bool isEmpty);
    }

    public class StackCalculatorOfReversePolishNotation
    {
        public float delta = 0.0001f; //null comparison precision

        private string[] ToParse(string expressionInReversePolishNotation)
        {
            return expressionInReversePolishNotation.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries); //if there more than one spaces
        }

        public float ToCalculate(string expessionInReversePolishNotation, IStack stack,
            out bool isCorrectExpression)
        {
            isCorrectExpression = true;
            bool stackOver;
            string[] parsedExpression = ToParse(expessionInReversePolishNotation);
            float result;
            for (int i = 0; i < parsedExpression.Length; i++)
            {
                int currOperand;
                bool isOperand = int.TryParse(parsedExpression[i], out currOperand);
                if (isOperand)
                {
                    stack.Add((float)currOperand);
                }
                else
                {
                    if (parsedExpression[i] == "+")
                    {
                        bool isEmpty;
                        float fstNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        float sndNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        stack.Add(fstNumber + sndNumber);
                    }
                    else if (parsedExpression[i] == "/")
                    {
                        bool isEmpty;
                        float fstNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        float sndNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        if (Math.Abs(fstNumber) < delta)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        stack.Add(sndNumber / fstNumber);
                    }
                    else if (parsedExpression[i] == "-")
                    {
                        bool isEmpty;
                        float fstNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        float sndNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        stack.Add(sndNumber - fstNumber);
                    }
                    else if (parsedExpression[i] == "*")
                    {
                        bool isEmpty;
                        float fstNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        float sndNumber = stack.Remove(out isEmpty);
                        if (isEmpty)
                        {
                            isCorrectExpression = false;
                            return 0;
                        }

                        stack.Add(sndNumber * fstNumber);
                    }
                    else
                    {
                        isCorrectExpression = false;
                        return 0;
                    }
                }
            }

            result = stack.Remove(out stackOver);
            if (stackOver) //in stack was nothing
            {
                isCorrectExpression = false;
                return 0;
            }

            stack.Remove(out stackOver);
            if (stackOver)
            {
                return result; //in stack was only result
            }

            isCorrectExpression = false;
            return 0;
        }
    }
}