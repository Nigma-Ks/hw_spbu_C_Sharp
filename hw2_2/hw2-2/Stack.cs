
namespace hw2_2
{
    public class FloatStackBasedOnArray : IStack
    {
        private const int defaultArraySize = 20;

        private float[] _ArrayOfOperands;


        public int Size { get; private set; } = 0;

        public FloatStackBasedOnArray()
        {
            _ArrayOfOperands = new float[defaultArraySize];
        }

        private float[] ExpandArrayOnDefaultSize(float[] arrayToExpand)
        {
            float[] newExpandedArray = new float[arrayToExpand.Length + defaultArraySize];
            for (int i = 0; i < arrayToExpand.Length; i++)
            {
                newExpandedArray[i] = arrayToExpand[i];
            }

            return newExpandedArray;
        }

        public void Add(float value)
        {
            if (Size == _ArrayOfOperands.Length)
            {
                _ArrayOfOperands = ExpandArrayOnDefaultSize(_ArrayOfOperands);
            }

            _ArrayOfOperands[Size] = value;
            Size = ++Size;
        }

        public (float value, bool isEmpty) Remove()
        { 
            if (Size < 1)
            {
                return (0, true);
            }

            float value = _ArrayOfOperands[Size - 1]; //get head of the stack
            _ArrayOfOperands[Size - 1] = 0;
            Size--;
            return (value, false);
        }
    }

    public class FloatStackBasedOnList : IStack
    {
        private List<float> _listOfOperands;

        private int size = 0;

        public int Size { get; private set; }

        public FloatStackBasedOnList()
        {
            _listOfOperands = new List<float>();
        }

        public void Add(float value)
        {
            _listOfOperands.Add(value);
            Size = ++Size;
        }

        public (float value, bool isEmpty) Remove()
        {
            if (Size < 1)
            {
                return (0, true);
            }

            float value = _listOfOperands[Size - 1]; //get head of the stack
            _listOfOperands.RemoveAt(Size - 1);
            Size--;
            return (value, false);
        }
    }


    public interface IStack
    {
        void Add(float value);

        (float value, bool isEmpty) Remove();
    }
}

