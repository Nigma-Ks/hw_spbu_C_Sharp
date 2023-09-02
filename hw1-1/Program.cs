using System;
namespace hw1_1
{
    class Program
    {
        const float accuracy = 0.001f;
        static void Main()
        {
            Console.WriteLine($"Эта программа сортирует введенный массив из десятичных дробей пузырьком c точностью {accuracy}");
            Console.Write("Введите длину массива: ");
            int amountOfElementsInArray = Convert.ToInt32(Console.ReadLine());
            float[] arrayForSort = new float [amountOfElementsInArray];
            for (int i = 0; i < amountOfElementsInArray; i++)
            {
                Console.Write("Введите элемент массива: ");
                arrayForSort[i] = Convert.ToSingle(Console.ReadLine());
            }
            BubbleSort(arrayForSort);
            Console.Write("Отсортированный массив: ");
            for (int i = 0; i < amountOfElementsInArray; i++)
            {
                Console.Write(arrayForSort[i] + " ");
            }
        }
        static void BubbleSort(float[] arrayForSort)
        {
            int arrayLenght = arrayForSort.Length;
            for (int i = 0; i < arrayLenght - 1; i++)
            {
                for (int j = 0; j < arrayLenght - i - 1; j++)
                {
                    if (arrayForSort[j + 1] - arrayForSort[j] < accuracy)
                    {
                        float forChange = arrayForSort[j];
                        arrayForSort[j] = arrayForSort[j + 1];
                        arrayForSort[j + 1] = forChange;
                    }
                }
            }
        }
    }
}
