
namespace hw6_1;

public class Tests
{
    private static IEnumerable<TestCaseData> ForMapTest
        => new TestCaseData[]
        {
            new TestCaseData(new Func<int, int>(x => x + 2),
                new List<int>(){ 4, -2, 5, 1, 20, -13 }, new List<int>() { 6, 0, 7, 3, 22, -11 }),
            new TestCaseData(new Func<int, int>(x => x % 2),
                new List<int>(){ 1, 2, 3, 2, 9 }, new List<int>() { 1, 0, 1, 0, 1})
        };

    private static IEnumerable<TestCaseData> ForFilterTest
        => new TestCaseData[]
        {
            new TestCaseData(new Func<int, bool>(x => x % 4 == 1),
                    new List<int>(){1, 2, -3, 4, -5}, new List<int>() {1}),
            new TestCaseData(new Func<int, bool>(x => x > 6),
                new List<int>(){11, 6, 10, 4, -5}, new List<int>() {11, 10})
        };

    private static IEnumerable<TestCaseData> ForFoldTest
        => new TestCaseData[]
        {
            new TestCaseData( new Func<int, int, int>((x, y) =>
                    x * y), new List<int>() {1, 2, 3, 4, 5}, 1, 120),
            new TestCaseData(new Func<int, int, int>((x, y) =>
                    x / y), new List<int>() {1, 2, 3}, 12, 2)
        };

    [Test]
    public void TestNullArgument()
    {
        List<int>? list = null;
        Assert.Throws<ArgumentNullException>(() => ListLambdaFunctionsApplier<int>.Map(list, x => 2 * x));
        Assert.Throws<ArgumentNullException>(() => ListLambdaFunctionsApplier<int>.Filter(list, x => x % 3 == 0));
        Assert.Throws<ArgumentNullException>(() => ListLambdaFunctionsApplier<int>.Fold(list, 1, (x, y) => x * y));
    }

    [TestCaseSource(nameof(ForMapTest))]
    public void MapTest(Func<int, int> lambdaFunction, List<int> list, List<int> correctList)
    {
        List<int> resultList = ListLambdaFunctionsApplier<int>.Map(list, lambdaFunction);
        Assert.True(resultList != null);
        Assert.True(resultList.Count == correctList.Count);
        for (int i = 0; i < list.Count; i++)
        {
            Assert.True(resultList[i] == correctList[i]);
        }
    }

    [TestCaseSource(nameof(ForFilterTest))]
    public void TestFilter(Func<int, bool> lambdaFunction, List<int> list, List<int> correctList)
    {
        List<int> resultList= ListLambdaFunctionsApplier<int>.Filter(list, lambdaFunction);
        Assert.True(resultList != null);
        Assert.True(resultList.Count == correctList.Count);
        for (int i = 0; i < resultList.Count; i++)
        {
            Assert.True(resultList[i] == correctList[i]);
        }
    }

    [TestCaseSource(nameof(ForFoldTest))]
    public void TestFold(Func<int, int, int> lambdaFunction, List<int> list, int initialValue, int correctAnswer)
    {
        int answer = ListLambdaFunctionsApplier<int>.Fold(list, initialValue, lambdaFunction);
        Assert.True(answer == correctAnswer);
    }
}
