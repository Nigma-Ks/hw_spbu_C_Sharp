namespace hw5_1.tests;

public class Tests
{
    private Graph _graph;

    [SetUp]
    public void Setup()
    {
        _graph = new Graph();
    }

    [Test]
    public void ExampleTest()
    {
        string topology = "1: 2 (10), 3 (5)\n2: 3(1)";
        string tableOfOptimizedGraph = _graph.FindMst(topology);
        Assert.IsTrue(tableOfOptimizedGraph == "1: 2 (10), 3 (5)");
    }

    [Test]
    public void DisconnectedTopologyTest()
    {
        string topology = "1: 2 (10)\n3: 4(1)";
        try
        {
            string tableOfOptimizedGraph = _graph.FindMst(topology);
        }
        catch (IncorrectFileException)
        {
            Assert.IsTrue(true);
            return;
        }

        Assert.IsTrue(false);
    }

    [Test]
    public void IncorrectStringTest()
    {
        string topology = "1: 2 (10\n3: 4(1)";
        try
        {
            string tableOfOptimizedGraph = _graph.FindMst(topology);
        }
        catch (IncorrectFileException)
        {
            Assert.IsTrue(true);
            return;
        }

        Assert.IsTrue(false);
    }

    [Test]
    public void ComplicatedTopologyTest()
    {
        string topology = "1: 2 (-7), 3 (-8)\n2: 3(-11), 4(-2)\n3: 4(-6), 5(-9)\n4: 5(-11), 6(-9)\n5: 6(-10)";
        string tableOfOptimizedGraph = _graph.FindMst(topology);
        Assert.IsTrue(tableOfOptimizedGraph == "1: 2 (-7)\n2: 4 (-2)\n3: 4 (-6), 5 (-9)\n4: 6 (-9)");
    }
}
