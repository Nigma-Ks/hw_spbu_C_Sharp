
namespace hw6_1
{
    public class ListLambdaFunctionsApplier<T>
    {
        public static List<T> Map(List<T>? list, Func<T, T> lambdaFunction)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var mappedList = new List<T>();

            for (var i = 0; i < list.Count; i++)
            {
                mappedList.Add(lambdaFunction(list[i]));
            }

            return mappedList;
        }

        public static List<T> Filter(List<T>? list, Func<T, bool> lambdaFunction)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var filteredList = new List<T>();

            foreach (var item in list)
            {
                if (lambdaFunction(item))
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }

        public static T Fold(List<T>? list, T initialValue, Func<T, T, T> lambdaFunction)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var value = initialValue;
            foreach (var item in list)
            {
                value = lambdaFunction(value, item);
            }

            return value;
        }
    }
}

