using NUnit.Framework.Internal;

namespace hw6_2.Tests;

public class Tests
{
    private Game game;
    private Map map;

    [SetUp]
    public void SetUp()
    {
        map = new(File.ReadAllLines("..//..//..//Test map.txt"));
        game = new Game(map);
    }

    [Test]
    public void IncorrectMapTest()
    {
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "**", "***" })); //not a rectangle
        Assert.Throws<ArgumentException>(() => new Map(new string[] { })); //Lenght = 0
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "***", "***" })); //too small
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "*@*", "***", "***", "* *" })); //non limited
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "***", "***", "***", "*6*" })); //wrong symbols
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "***", "***", "***", "***" })); //no character
        Assert.Throws<ArgumentException>(() => new Map(new string[] { "@**", "**@", "***", "***" })); //more than one character

    }

    [Test]
    public void GoIfItIsPossibleTest()
    {
        var initialX = map.initialStateX;
        var initialY = map.initialStateY;
        Assert.IsTrue(initialX == 13 && initialY == 9);
        game.OnTop(null, EventArgs.Empty);
        Assert.IsTrue(game.CurrPositionX == 13 && game.CurrPositionY == 8);
        game.OnLeft(null, EventArgs.Empty);
        Assert.IsTrue(game.CurrPositionX == 12 && game.CurrPositionY == 8);
    }

    [Test]
    public void DoNotGoIfItIsImpossibleTest()
    {
        var initialX = map.initialStateX;
        var initialY = map.initialStateY;
        Assert.IsTrue(initialX == 13 && initialY == 9);
        game.OnBottom(null, EventArgs.Empty);
        Assert.IsTrue(initialX == 13 && initialY == 9);
    }
}
