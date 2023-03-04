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
                Console.Write(
                    $"Enter string (with more than 1 symbol) which you want to transform, please do not use {(char)0} symbol: ");
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
                $"Enter Burrows-Wheeler-transformed string (with more than 1 symbol) which you want to retransform, please do not use {(char)0} symbol: ");
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

        static void BubbleSortByFirstItem(ref (string, char)[] arr,
            ref int indexOfLastLetterInOriginalString)
        {
            int arrLength = arr.Length;
            string suffixOfEntireString = arr[arrLength - 1].Item1; //index with end of the original string
            (string, char) temp;
            for (int i = 0; i < arrLength; i++)
            {
                for (int j = 0; j < arrLength - 1 - i; j++)
                {
                    if (String.Compare(arr[j].Item1, arr[j + 1].Item1) > 0)
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
            //changing last symbol to make sort easier
            char lastSymbol = originalString[originalStringLength - 1];
            originalString = originalString.Remove(originalStringLength - 1, 1)
                .Insert(originalStringLength - 1, ((char)1).ToString());

            (string, char)[] arrayOfSuffixes = new (string, char)[originalStringLength];
            string currentSuffix = "";

            for (int i = 0; i < originalStringLength; i++)
            {
                currentSuffix = originalString[originalStringLength - 1 - i] + currentSuffix;
                arrayOfSuffixes[i] = (currentSuffix,
                    originalString[(2 * originalStringLength - 2 - i) % originalStringLength]);
            }

            int indexOfLastLetterInOriginalString = originalStringLength - 1;
            BubbleSortByFirstItem(ref arrayOfSuffixes,
                ref indexOfLastLetterInOriginalString);
            string transformedString = "";
            foreach (var tuple in arrayOfSuffixes)
                transformedString += tuple.Item2; //concatenation of sorted suffixes prior 
            endIndexOfOriginalInTransformedString = indexOfLastLetterInOriginalString;
            transformedString =
                transformedString.Replace((char)1, lastSymbol); //change our convenient symbol to original
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

        static string BurrowsWheelerRetransform(string transformedString, int endPositionOfOriginalInTransformedString)
        {
            //change last symbol with symbol that will indicate the end of the string because it solves problem of sort in sortedDifferentLettersInTransformedString
            char lastSymbol =
                transformedString[
                    endPositionOfOriginalInTransformedString -
                    1]; //where symbol of the end have to be in first position
            transformedString = transformedString.Remove(endPositionOfOriginalInTransformedString - 1, 1)
                .Insert(endPositionOfOriginalInTransformedString - 1, ((char)1).ToString());

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
            for (int i = 0; i < transformedStringLenght; i++)
            {
                p[i] = a[sortedDifferentLettersInTransformedString.IndexOf(transformedString[i])];
                a[sortedDifferentLettersInTransformedString.IndexOf(transformedString[i])]++;
            }

            string originalString = "";
            int currIndex = endPositionOfOriginalInTransformedString; //in p numbers from 1
            for (int i = 0; i < transformedStringLenght - 1; i++)
            {
                originalString += transformedString[Array.IndexOf(p, currIndex)];
                currIndex = Array.IndexOf(p, currIndex) + 1; //in p numbers from 1
            }

            return originalString + lastSymbol;
        }
    }
}
