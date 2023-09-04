

namespace hw1_2
{
    class Program
    {
        static void Main()
        {
            Console.Write(
                "Эта программа преобразовывает введеную строку методом Барроуза-Уилера: \n" +
                "выводится преобразованная строка и индекс конца строки в результате преобразования.\n\n" +
                "Также она делает и обратное преобразование, принимающее преобразованную строку и индекс конца исходной в ней,\n" +
                "возвращает исходную строку.\n\n");
            Console.Write("Если вы хотите преобразовать строку методом Барроуза-Уилера, введите 1,\n" +
                "если совершить обратное преобразование, введите 2:");
            string? choice = Console.ReadLine();
            byte numberInChoice;
            bool isChoiceByteDigit = Byte.TryParse(choice, out numberInChoice);
            while (!isChoiceByteDigit || (numberInChoice != 1 && numberInChoice != 2))
            {
                Console.Write(
                    "Некорректный ввод!\nЕсли вы хотите преобразовать строку методом Барроуза-Уилера, введите 1,\nесли совершить обратное преобразование, введите 2:");
                choice = Console.ReadLine();
                isChoiceByteDigit = Byte.TryParse(choice, out numberInChoice);
            }

            if (numberInChoice == 1)
            {
                Console.Write(
                    $"Введите строку, в которой больше 1 символа, для преобразования: ");
                string? originalString = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(originalString) || originalString.Length == 1)
                {
                    Console.WriteLine("Введена пустая или односимвольная строка");
                    Console.Write("Повторно введите строку, в которой больше 1 символа, для преобразования: ");
                    originalString = Console.ReadLine();
                }

                string transformedString =
                    BurrowsWheelerTransform(originalString, out int endIndexOfOriginalInTransformedString);
                Console.WriteLine(
                    $"Преобразованная строка: {transformedString}\nИндекс конца строки: {endIndexOfOriginalInTransformedString}\n");
                return;
            }

            Console.Write(
                $"Введите строку, в которой больше 1 символа, для обратного преобразования: ");
            string? enteredTransformedString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(enteredTransformedString) || enteredTransformedString.Length == 1)

            {
                Console.WriteLine("Введена пустая или односимвольная строка");
                Console.Write("Повторно введите строку, в которой больше 1 символа, для обратного преобразования: ");
                enteredTransformedString = Console.ReadLine();
            }

            Console.Write("Введите индекс (счет с 0) конца строки в введенной,\nпреобразованной методом Барроузом-Уилера строке: ");
            int positionOfLastLetterInEnteredTransformedString = Convert.ToInt32(Console.ReadLine());
            while (positionOfLastLetterInEnteredTransformedString >= enteredTransformedString.Length ||
                   positionOfLastLetterInEnteredTransformedString < 0)
            {
                Console.WriteLine("Введенный индекс некорректен");
                Console.Write("Введите индекс: ");
                positionOfLastLetterInEnteredTransformedString = Convert.ToInt32(Console.ReadLine());
            }

            string restoredString =
                BurrowsWheelerRetransform(enteredTransformedString, positionOfLastLetterInEnteredTransformedString);
            Console.WriteLine($"Исходная строка: {restoredString}\n");
        }

        static string BurrowsWheelerTransform(string originalString, out int endIndexOfOriginalInTransformedString)
        {
            int originalStringLength = originalString.Length;
            char lastSymbol = originalString[originalStringLength - 1];

            var arrayOfShiftsAndLastSymbols = new (string, char)[originalStringLength];
            string currentShift = originalString;
            string endOfCurrentShift = "";
            for (int i = 0; i < originalStringLength; i++)
            {
                currentShift = originalString.Substring(i) + endOfCurrentShift;
                endOfCurrentShift += currentShift[0];
                arrayOfShiftsAndLastSymbols[i] = (currentShift, currentShift[originalStringLength - 1]);
            }
            endIndexOfOriginalInTransformedString = originalStringLength - 1;
            Array.Sort(arrayOfShiftsAndLastSymbols, (x, y) => x.Item1.CompareTo(y.Item1));
            endIndexOfOriginalInTransformedString = Array.IndexOf(arrayOfShiftsAndLastSymbols, (originalString, originalString[originalStringLength - 1]));
            string transformedString = "";
            for (int i = 0; i < originalStringLength; i++)
                transformedString += arrayOfShiftsAndLastSymbols[i].Item2; //concatenation of sorted shifts prior
            return transformedString;
        }



        static (char, int)[] ArrayOfSymbolsInSortedStringWithTheirAmount(string sortedString,
            out int currIndex)
        {
            var uniqueLettersAndTheirAmount = new (char, int)[sortedString.Length];
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
            int transformedStringLenght = transformedString.Length;
            string sortedTransformedString = "";
            for (int i = 0; i < transformedStringLenght; i++)
            {
                sortedTransformedString += transformedString[i];
            }

            sortedTransformedString = string.Concat(sortedTransformedString.OrderBy(x => x).ToArray());
            int lastIndexWithLetter = 0;
            (char, int)[] uniqueLettersAndTheirAmount =
                ArrayOfSymbolsInSortedStringWithTheirAmount(sortedTransformedString,
                    out lastIndexWithLetter);
            string sortedDifferentLettersInTransformedString = "";

            for (int i = 0; i <= lastIndexWithLetter; i++)
            {
                sortedDifferentLettersInTransformedString += uniqueLettersAndTheirAmount[i].Item1;
            }

            var firstOccuranceOfSymbolInFirstColumn = new int[lastIndexWithLetter + 1];//первый столбец отсортированной матрицы сдвигов
            int sumOfPreviousChars = 0;
            for (int i = 0; i <= lastIndexWithLetter; i++)
            {
                sumOfPreviousChars += uniqueLettersAndTheirAmount[i].Item2;
                firstOccuranceOfSymbolInFirstColumn[i] = sumOfPreviousChars - uniqueLettersAndTheirAmount[i].Item2;
            }

            var vectorOfRetransformation = new int[transformedStringLenght];
            int indexOfSymbolInStringAlphabet;
            for (int i = 0; i < transformedStringLenght; i++)
            {
                indexOfSymbolInStringAlphabet = sortedDifferentLettersInTransformedString.IndexOf(transformedString[i]);
                vectorOfRetransformation[firstOccuranceOfSymbolInFirstColumn[indexOfSymbolInStringAlphabet]] = i;
                firstOccuranceOfSymbolInFirstColumn[indexOfSymbolInStringAlphabet]++;
            }

            string originalString = "";
            int currIndex = vectorOfRetransformation[endPositionOfOriginalInTransformedString];
            for (int i = 0; i < transformedStringLenght; i++)
            {
                originalString += transformedString[currIndex];
                currIndex = vectorOfRetransformation[currIndex];
            }

            return originalString;
        }
    }
}