namespace hw2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BorStringStorage storage = new();
            PrintStorageCommands();
            Console.Write("Write number 1, 2, 3, 4, 5 or 6: ");
            string? choice = Console.ReadLine();
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter string which you want to add: ");
                        string? stringToAdd = Console.ReadLine();
                        if (stringToAdd == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringToAdd = Console.ReadLine();
                        }

                        bool isNewString = storage.Add(stringToAdd);
                        if (isNewString)
                        {
                            Console.WriteLine("String successfully added!");
                        }
                        else
                        {
                            Console.WriteLine("This string already in storage!");
                        }

                        break;
                    case "2":
                        Console.Write("Enter string which you want to remove: ");
                        string? stringToRemove = Console.ReadLine();
                        if (stringToRemove == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringToRemove = Console.ReadLine();
                        }

                        bool wasInStorage = storage.Remove(stringToRemove);
                        if (wasInStorage)
                        {
                            Console.WriteLine("String successfully removed!");
                        }
                        else
                        {
                            Console.WriteLine("This string wasn't in storage!");
                        }

                        break;
                    case "3":
                        Console.WriteLine($"Strings in storage: {storage.Size}");
                        break;
                    case "4":
                        Console.Write("Enter prefix: ");
                        string? prefix = Console.ReadLine();
                        if (prefix == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            prefix = Console.ReadLine();
                        }

                        Console.WriteLine(
                            $"There are {storage.HowManyStartsWithPrefix(prefix)} strings which start with this prefix");
                        break;
                    case "5":
                        Console.Write("Enter string which you want check if it is in storage: ");
                        string? stringForCheckingIfItInStorage = Console.ReadLine();
                        if (stringForCheckingIfItInStorage == null)
                        {
                            Console.Write("Wrong! There are no symbols! Enter string which you want to add: ");
                            stringForCheckingIfItInStorage = Console.ReadLine();
                        }

                        if (storage.Contains(stringForCheckingIfItInStorage))
                        {
                            Console.WriteLine("There is entered string in storage!");
                        }
                        else
                        {
                            Console.WriteLine("There isn't entered string in storage!");
                        }

                        break;
                    default:
                        Console.WriteLine("\nYou entered something wrong!");
                        break;
                }

                PrintStorageCommands();
                Console.Write("Write number 1, 2, 3, 4, 5 or 6: ");
                choice = Console.ReadLine();
            }
        }

        static void PrintStorageCommands()
        {
            Console.WriteLine("You can choose one of the options:\n" +
                                  "1. Add string\n" +
                                  "2. Remove string\n" +
                                  "3. Find out how many strings are in storage\n" +
                                  "4. Find out how many strings in storage starts with your entered string\n" +
                                  "5. Find out if storage contains entered string\n" +
                                  "6. Exit\n");
        }
    }
}