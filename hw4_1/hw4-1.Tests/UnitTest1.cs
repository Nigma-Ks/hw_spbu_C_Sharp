namespace hw4_1.Tests;

public class Tests
{
    [Test]
    public void NullExpressionTest()
    {
        Assert.Throws<Exception>(() => new ExpressionTree(null));
    }

    [Test]
    public void WrongSymbolsInExpressionTest()
    {
        Assert.Throws<Exception>(() => new ExpressionTree("+ 7-8 0"));
    }

    [Test]
    public void IncorrectWrittenExpressionTest()
    {
        Assert.Throws<Exception>(() => new ExpressionTree("(8 + 7)"));
    }

    [Test]
    public void ExpressionWithDividingByZeroTest()
    {
        ExpressionTree tree = new ExpressionTree("(/ 9 0)");
        Assert.Throws<DivideByZeroException>(() => tree.CalculateExpression());
    }

    [Test]
    public void ExpressionFromExerciseTest()
    {
        ExpressionTree tree = new ExpressionTree("(* (+ 1 1) 2)");
        Assert.IsTrue(tree.CalculateExpression() - 4f < 0.01f);
    }

    [Test]
    public void ExpressionWithWrongBrackets()
    {
        Assert.Throws<Exception>(() => new ExpressionTree("(* (+ 1) 1 2)"));
    }
}