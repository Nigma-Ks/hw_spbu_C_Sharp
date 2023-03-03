namespace hw1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(
                "This program can do Burrows-Wheeler transformation and reverse transformation.\nIf you want to do first, enter '1', if second, '2': ");
            byte choice = Convert.ToByte(Console.ReadLine());
            while (choice != 1 && choice != 2)
            {
                Console.Write(
                    "Incorrect input!\nIf you want to do Burrows-Wheeler transformation, enter '1', if reverse transformation, '2': ");
                choice = Convert.ToByte(Console.ReadLine());
            }

            if (choice == 1)
            {
                Console.Write("Enter string (with more than 1 symbol) which you want to transform: ");
                string originalString = Console.ReadLine(); //warning fixes if I use var instead of string,
                //but after there is a checkout if string is empty
                while (string.IsNullOrWhiteSpace(originalString) || originalString.Length == 1)
                {
                    Console.WriteLine("Error! You entered empty or too small string");
                    Console.Write("Enter string which you want to transform again: ");
                    originalString = Console.ReadLine();
                }

                string transformedString =
                    BurrowsWheelerTransform(originalString, out int endIndexOfOriginalInTransformedString);
                Console.WriteLine(
                    $"Transformed string: {transformedString}\nPositionOfEnd: {endIndexOfOriginalInTransformedString + 1}\n");
                return;
            }

            Console.Write(
                "Enter Burrows-Wheeler-transformed string (with more than 1 symbol) which you want to retransform: ");
            string enteredTransformedString = Console.ReadLine(); //warning fixes if I use var instead of string,
            //but after there is a checkout if string is empty
            while (string.IsNullOrWhiteSpace(enteredTransformedString) || enteredTransformedString.Length == 1)

            {
                Console.WriteLine("Error! You entered empty or too small string");
                Console.Write("Enter string which you want to retransform again: ");
                enteredTransformedString = Console.ReadLine();
            }

            Console.Write("Enter position (counts from 1) of the last letter in Burrows-Wheeler-transformed string: ");
            int positionOfLastLetterInEnteredTransformedString = Convert.ToInt32(Console.ReadLine());
            while (positionOfLastLetterInEnteredTransformedString > enteredTransformedString.Length ||
                   positionOfLastLetterInEnteredTransformedString < 1)
            {
                Console.WriteLine("Error! You entered wrong position");
                Console.Write("Enter again: ");
                positionOfLastLetterInEnteredTransformedString = Convert.ToInt32(Console.ReadLine());
            }

            string restoredString =
                BurrowsWheelerRetransform(enteredTransformedString, positionOfLastLetterInEnteredTransformedString);
            Console.WriteLine($"Original string: {restoredString}\n");
            return;
        }

        static bool IsFirstStringSmaller(string fstStr, string sndStr)
        {
            int fstStrLength = fstStr.Length;
            int sndStrLength = sndStr.Length;
            if (fstStrLength >= sndStrLength)
            {
                for (int i = 0; i < sndStrLength; i++)
                {
                    if (fstStr[i] > sndStr[i])
                    {
                        return false;
                    }

                    if (fstStr[i] < sndStr[i])
                    {
                        return true;
                    }
                }

                return false;
            }

            for (int i = 0; i < fstStrLength; i++)
            {
                if (fstStr[i] > sndStr[i])
                {
                    return false;
                }

                if (fstStr[i] < sndStr[i])
                {
                    return true;
                }
            }

            return true;
        }

        static void BubbleSortOfArrayByFirstItemInTupleNoChangeWithFirst(ref (string, char)[] arr,
            ref int indexOfLastLetterInOriginalString)
        {
            int arrLength = arr.Length;
            string suffixOfEntireString = arr[arrLength - 1].Item1; //index with end of the original string
            (string, char) temp;
            for (int i = 0; i < arrLength; i++)
            {
                for (int j = 1; j < arrLength - 1 - i; j++)
                {
                    if (!IsFirstStringSmaller(arr[j].Item1, arr[j + 1].Item1))
                    {
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                        if (arr[j].Item1 == suffixOfEntireString)
                        {
                            indexOfLastLetterInOriginalString = j;
                        }

                        if (arr[j + 1].Item1 == suffixOfEntireString)
                        {
                            indexOfLastLetterInOriginalString = j + 1;
                        }
                    }
                }
            }
        }

        static string BurrowsWheelerTransform(string originalString, out int endIndexOfOriginalInTransformedString)
        {
            int originalStringLength = originalString.Length;
            (string, char)[] arrayOfSuffixes = new (string, char)[originalStringLength];
            string currentSuffix = "";

            for (int i = 0; i < originalStringLength; i++)
            {
                currentSuffix = originalString[originalStringLength - 1 - i] + currentSuffix;
                arrayOfSuffixes[i] = (currentSuffix,
                    originalString[(2 * originalStringLength - 2 - i) % originalStringLength]);
            }

            int indexOfLastLetterInOriginalString = originalStringLength - 1;
            BubbleSortOfArrayByFirstItemInTupleNoChangeWithFirst(ref arrayOfSuffixes,
                ref indexOfLastLetterInOriginalString);
            string transformedString = "";
            foreach (var tuple in arrayOfSuffixes)
                transformedString += tuple.Item2; //concatenation of sorted suffixes prior symbols
            endIndexOfOriginalInTransformedString = indexOfLastLetterInOriginalString;
            return transformedString;
        }

        static (char, int)[] ArrayOfDifferentLettersInSortedStringWithTheirNumberInItConstructor(string sortedString,
            out int currIndex)
        {
            (char, int)[] uniqueLettersAndTheirAmount = new (char, int) [sortedString.Length];
            uniqueLettersAndTheirAmount[0] = (sortedString[0], 1);
            currIndex = 0;
            for (int i = 1; i < sortedString.Length; i++)
            {
                if (sortedString[i] != sortedString[i - 1])
                {
                    currIndex++;
                    uniqueLettersAndTheirAmount[currIndex].Item1 = sortedString[i];
                }

                uniqueLettersAndTheirAmount[currIndex].Item2++;
            }

            return uniqueLettersAndTheirAmount;
        }

        static int IndexOfElementInArray(int element, int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        static string BurrowsWheelerRetransform(string transformedString, int endPositionOfOriginalInTransformedString)
        {
            int transformedStringLenght = transformedString.Length;
            string sortedTransformedString = "";
            for (int i = 0; i < transformedStringLenght; i++)
            {
                sortedTransformedString += transformedString[i];
            }

            sortedTransformedString = string.Concat(sortedTransformedString.OrderBy(x => x).ToArray());
            int lastIndexWithLetter = 0;
            (char, int)[] uniqueLettersAndTheirAmount =
                ArrayOfDifferentLettersInSortedStringWithTheirNumberInItConstructor(sortedTransformedString,
                    out lastIndexWithLetter);
            string sortedDifferentLettersInTransformedString = "";

            for (int i = 0; i <= lastIndexWithLetter; i++)
            {
                sortedDifferentLettersInTransformedString += uniqueLettersAndTheirAmount[i].Item1;
            }

            int[] a = new int[lastIndexWithLetter + 1];
            a[0] = uniqueLettersAndTheirAmount[0].Item2;
            for (int i = 1; i <= lastIndexWithLetter; i++)
            {
                a[i] = a[i - 1] + uniqueLettersAndTheirAmount[i - 1].Item2;
            }

            int[] p = new int[transformedStringLenght];
            p[0] = a[uniqueLettersAndTheirAmount[0].Item2];
            for (int i = 1; i < transformedStringLenght; i++)
            {
                p[i] = a[sortedDifferentLettersInTransformedString.IndexOf(transformedString[i])];
                a[sortedDifferentLettersInTransformedString.IndexOf(transformedString[i])]++;
            }

            string originalString = "";
            int currIndex = endPositionOfOriginalInTransformedString; //in p numbers from 1
            for (int i = 0; i < transformedStringLenght; i++)
            {
                originalString += transformedString[IndexOfElementInArray(currIndex, p)];
                currIndex = IndexOfElementInArray(currIndex, p) + 1; //in p numbers from 1
            }

            return originalString;
        }
    }
}
