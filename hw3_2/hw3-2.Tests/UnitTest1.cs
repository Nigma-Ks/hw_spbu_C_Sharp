using NUnit.Framework;

namespace hw3_2.Tests;

public class LzwTests
{
    private Lzw lzw;

    [SetUp]
    public void Initialize()
    {
        lzw = new Lzw();
    }

    [Test]
    public void EmptyStringCompression()
    {
        bool isEmpty;
        (List<Byte[]> result, isEmpty, bool isFileTooBig) = lzw.LzwCompression("");
        Assert.IsTrue(isEmpty);
    }

    [Test]
    public void OneCharStringCompression()
    {
        bool isEmpty;
        (List<Byte[]> result, isEmpty, bool isFileTooBig) = lzw.LzwCompression("\n");
        Assert.IsTrue(!isEmpty && BitConverter.ToUInt32(result[0]) == (int)'\n');
    }

    [Test]
    public void StringCompression()
    {
        bool isEmpty;
        UInt32[] arrayOfRightIntCodes = { 97, 98, 97, 99, 256, 97, 100, 260, 259, 257, 101 };
        (List<Byte[]> result, isEmpty, bool isFileTooBig) = lzw.LzwCompression("abacabadabacabae");

        int resultLenght = result.Count;
        Assert.IsTrue(resultLenght == 11 && !isEmpty);

        for (int i = 0; i < resultLenght; i++)
        {
            Assert.IsTrue(BitConverter.ToUInt32(result[i]) == arrayOfRightIntCodes[i]);
        }

        Assert.Pass();
    }

    [Test]
    public void StringWithRepeatedSymbolsCompression()
    {
        bool isEmpty;
        UInt32[] arrayOfRightIntCodes = { 97, 256, 257, 258 };
        (List<Byte[]> result, isEmpty, bool isFileTooBig) = lzw.LzwCompression("aaaaaaaaaa");
        int resultLenght = result.Count;
        if (resultLenght != 4 || isEmpty) //Lenght of arrayOfRightCodes
        {
            Assert.IsTrue(false);
        }

        for (int i = 0; i < resultLenght; i++)
        {
            if (BitConverter.ToUInt32(result[i]) != arrayOfRightIntCodes[i])
            {
                Assert.IsTrue(false);
            }
        }

        Assert.Pass();
    }

    [Test]
    public void StringCompressionPlusDecompression()
    {
        bool isEmpty;
        bool isCorrect;
        string testString = "\n\n\n\\pbdbdbtjjj\\{{";
        (List<Byte[]> result, isEmpty, bool isFileTooBig) = lzw.LzwCompression(testString);
        (string resultOfDecompression, isEmpty, isCorrect) = lzw.LzwDecompression(result);
        Assert.IsTrue(resultOfDecompression == testString);
    }
}