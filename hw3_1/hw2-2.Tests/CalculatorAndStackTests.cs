using System.Runtime.CompilerServices;

namespace hw2_2.Tests;

public class CalculatorTest
{
    private float delta = 0.0001f;
    FloatStackBasedOnArray arrayStack;
    FloatStackBasedOnList listStack;
    StackCalculatorOfReversePolishNotation calculator;
    bool correctNotation;

    [SetUp]
    public void Initialize()
    {
        arrayStack = new();
        listStack = new();
        calculator = new();
    }

    private (float, bool) calculateWithBothStacks(out bool areEqual, string expression)
    {
        bool isCorrectExpressionArrayStack;
        bool isCorrectExpressionListStack;
        float resultArrayStack = calculator.ToCalculate(expression, arrayStack, out isCorrectExpressionArrayStack);
        float resultListStack = calculator.ToCalculate(expression, listStack, out isCorrectExpressionListStack);
        if (((resultArrayStack - resultListStack) < delta) &&
            isCorrectExpressionArrayStack == isCorrectExpressionListStack)
        {
            areEqual = true;
            return (resultArrayStack, isCorrectExpressionArrayStack);
        }

        areEqual = false;
        return (0, false);
    }

    [Test]
    public void EmptyStringTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, "");
        if (isEqualResultsByStacks)
        {
            Assert.IsFalse(resultNumberAndCorrectness.Item2);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }

    [Test]
    public void NullStringTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, null);
        if (isEqualResultsByStacks)
        {
            Assert.IsFalse(resultNumberAndCorrectness.Item2);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }

    [Test]
    public void DivisionByZeroTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, "9 0 /");
        if (isEqualResultsByStacks)
        {
            Assert.IsFalse(resultNumberAndCorrectness.Item2);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }

    [Test]
    public void IncorrectExpressionTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, "9 / 3");
        if (isEqualResultsByStacks)
        {
            Assert.IsFalse(resultNumberAndCorrectness.Item2);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }

    [Test]
    public void CorrectLongExpressionTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, "825*+132*+4-/");
        if (isEqualResultsByStacks)
        {
            Assert.IsTrue(resultNumberAndCorrectness.Item1 - 6.0f < delta);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }

    [Test]
    public void CorrectShortExpressionTest()
    {
        bool isEqualResultsByStacks;
        (float, bool) resultNumberAndCorrectness = calculateWithBothStacks(out isEqualResultsByStacks, "2 4 8 + *");
        if (isEqualResultsByStacks)
        {
            Assert.IsTrue(resultNumberAndCorrectness.Item1 - 24.0f < delta);
            return;
        }

        Assert.IsFalse(isEqualResultsByStacks);
    }
}