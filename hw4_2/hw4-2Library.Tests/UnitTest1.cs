namespace hw4_2Library.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(Lists))]
    public void ElementAppendingTest(List<int> list)
    {
        list.Append(2);
        Assert.IsFalse(list.IsEmpty());
    }

    [TestCaseSource(nameof(Lists))]
    public void ElementAppendingAndDeletingTest(List<int> list)
    {
        list.Append(2);
        list.DeleteByValue(2);
        Assert.IsTrue(list.IsEmpty());
    }

    [TestCaseSource(nameof(Lists))]
    public void FromEmptyDeletingTest(List<int> list)
    {
        Assert.Throws<NoneExistentElementRemovalException>(() => list.DeleteByValue(2));
    }

    [TestCaseSource(nameof(Lists))]
    public void DeletingLastInTreeElementsTest(List<int> list)
    {
        list.Append(1);
        list.Append(2);
        list.Append(3);
        list.DeleteByValue(3);

        Assert.IsTrue(list.GetValue(1) == 1 && list.GetValue(0) == 2);
    }

    [TestCaseSource(nameof(Lists))]
    public void DeletingMiddleInTreeElementsTest(List<int> list)
    {
        list.Append(1);
        list.Append(2);
        list.Append(3);
        list.DeleteByValue(2);

        Assert.IsTrue(list.GetValue(1) == 1 && list.GetValue(0) == 3);
    }

    [TestCaseSource(nameof(Lists))]
    public void DeletingFirstInTreeElementsTest(List<int> list)
    {
        list.Append(1);
        list.Append(2);
        list.Append(3);
        list.DeleteByValue(1);

        Assert.IsTrue(list.GetValue(1) == 2 && list.GetValue(0) == 3);
    }

    [TestCaseSource(nameof(Lists))]
    public void ChangingFirstInTreeElementsTest(List<int> list)
    {
        list.Append(1);
        list.Append(2);
        list.Append(3);
        list.ChangeByPosition(0, 7);

        Assert.IsTrue(list.GetValue(0) == 7);
    }

    [TestCaseSource(nameof(Lists))]
    public void TryingToChangeByNegativeIndexTest(List<int> list)
    {
        list.Append(1);
        Assert.Throws<IncorrectIndexException>(() => list.ChangeByPosition(-9, 7));
    }

    [TestCaseSource(nameof(Lists))]
    public void TryingToChangeByNoneExistedIndexTest(List<int> list)
    {
        list.Append(1);
        Assert.Throws<IncorrectIndexException>(() => list.ChangeByPosition(10, 7));
    }

    [Test]
    public void AddingRepeatedElementInUniqueList()
    {
        UniqueList<int> list = new UniqueList<int>();
        list.Append(1);
        Assert.Throws<AddingAlreadyExistedElementException>(() => list.Append(1));
    }

    private static IEnumerable<TestCaseData> Lists => new TestCaseData[]
    {
        new TestCaseData(new UniqueList<int>()),
        new TestCaseData(new List<int>()),
    };
}