using System.Runtime.CompilerServices;

namespace hw2_2.Tests;

public class CalculatorTest
{
    private float delta = 0.0001f;
    StackCalculatorOfReversePolishNotation calculator;

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
        float? result = calculator.ToCalculate("", stack);
        Assert.IsTrue(result == null);
    }

    [TestCaseSource(nameof(Stacks))]
    public void NullStringTest(IStack stack)
    {
        float? result = calculator.ToCalculate(null, stack);
        Assert.IsTrue(result == null);
    }

    [TestCaseSource(nameof(Stacks))]
    public void DivisionByZeroTest(IStack stack)
    {
        float? result = calculator.ToCalculate("9 0 /", stack);
        Assert.IsTrue(result == null);
    }

    [TestCaseSource(nameof(Stacks))]
    public void IncorrectExpressionTest(IStack stack)
    {
        float? result = calculator.ToCalculate("9 9 / +", stack);
        Assert.IsTrue(result == null);
    }

    [TestCaseSource(nameof(Stacks))]
    public void CorrectLongExpressionTest(IStack stack)
    { 
        float? result = calculator.ToCalculate("8 2 5 * + 1 3 2 * + 4 - /", stack);
        Assert.IsTrue(result != null && (result - 6f) < delta);
    }

    [TestCaseSource(nameof(Stacks))]
    public void CorrectShortExpressionTest(IStack stack)
    {
        float? result = calculator.ToCalculate("2 4 8 + *", stack);
        Assert.IsTrue(result != null && (result - 24f) < delta);
    }

    [TestCaseSource(nameof(Stacks))]
    public void CalculatorReturnFloatTest(IStack stack)
    {
        float? result = calculator.ToCalculate("2 4 4 + /", stack);
        Assert.IsTrue(result != null && (result - 1.5f) < delta);
    }
}