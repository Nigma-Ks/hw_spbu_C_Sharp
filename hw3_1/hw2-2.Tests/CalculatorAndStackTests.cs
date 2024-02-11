using System.Runtime.CompilerServices;

namespace hw2_2.Tests;

public class CalculatorTest
{
    private float delta = 0.0001f;
    StackCalculatorOfReversePolishNotation calculator;
    bool correctNotation;

    [SetUp]
    public void Initialize()
    {
        calculator = new();
    }
    
    private static IEnumerable<TestCaseData> Stacks
        => new TestCaseData[]
        {
            new TestCaseData(new FloatStackBasedOnArray()),
            new TestCaseData(new FloatStackBasedOnList()),
        };

    [TestCaseSource(nameof(Stacks))]
    public void EmptyStringTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate("", stack, out isCorrectExpression);
        Assert.IsTrue(result == 0 && !isCorrectExpression);
    }

    [TestCaseSource(nameof(Stacks))]
    public void NullStringTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate(null, stack, out isCorrectExpression);
        Assert.IsTrue(result == 0 && !isCorrectExpression);
    }

    [TestCaseSource(nameof(Stacks))]
    public void DivisionByZeroTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate("9 0 /", stack, out isCorrectExpression);
        Assert.IsTrue(result == 0 && !isCorrectExpression);
    }

    [TestCaseSource(nameof(Stacks))]
    public void IncorrectExpressionTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate("9 9 / +", stack, out isCorrectExpression);
        Assert.IsTrue(result == 0 && !isCorrectExpression);
    }

    [TestCaseSource(nameof(Stacks))]
    public void CorrectLongExpressionTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate("8 2 5 * + 1 3 2 * + 4 - /", stack, out isCorrectExpression);
        Assert.IsTrue((result - 6f) < delta && isCorrectExpression);
    }

    [TestCaseSource(nameof(Stacks))]
    public void CorrectShortExpressionTest(IStack stack)
    {
        bool isCorrectExpression;
        float result = calculator.ToCalculate("2 4 8 + *", stack, out isCorrectExpression);
        Assert.IsTrue((result - 24f) < delta && isCorrectExpression);
    }
}
