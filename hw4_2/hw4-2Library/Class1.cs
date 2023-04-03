namespace hw4_2Library;

public class List<T>
{
    protected ListElement? LastAdded;

    protected class ListElement
    {
        public T Value;
        public ListElement? Prev;

        public ListElement(T value, ListElement prev)
        {
            Value = value;
            Prev = prev;
        }
    }

    public bool IsEmpty()
    {
        return (LastAdded == null);
    }

    public virtual void Append(T value)
    {
        if (LastAdded != null)
        {
            ListElement newElement = new(value, LastAdded);
            LastAdded = newElement;
        }
        else
        {
            LastAdded = new ListElement(value, null);
        }
    }

    public virtual T GetValue(int index)
    {
        if (index < 0)
        {
            throw new IncorrectIndexException();
        }

        if (LastAdded == null)
        {
            throw new IndexOutOfRangeException();
        }

        ListElement curr = LastAdded;
        int currIndex = 0;
        while (curr != null && index != 0)
        {
            curr = curr.Prev;
            index--;
        }

        if (curr != null)
        {
            return curr.Value;
        }

        throw new IndexOutOfRangeException();
    }

    public virtual void DeleteByValue(T value)
    {
        bool isValueInList = false;
        ListElement curr = LastAdded;
        while (LastAdded != null && Equals(curr.Value, value))
        {
            isValueInList = true;
            curr = curr.Prev;
            LastAdded = curr;
        }

        ListElement preCurr = curr; //unnecessary initialization
        while (curr != null)
        {
            if (Equals(curr.Value, value))
            {
                isValueInList = true;
                preCurr.Prev = curr.Prev; //last cycle ended if curr is null then that cycle
            }

            preCurr = curr; // that cycle skips, if not equal, then at least first if skips
            curr = curr.Prev;
        }

        if (!isValueInList)
        {
            throw new NoneExistentElementRemovalException();
        }
    }

    public virtual void ChangeByPosition(int position, T value)
    {
        if (position < 0)
        {
            throw new IncorrectIndexException();
        }

        if (LastAdded == null)
        {
            throw new IndexOutOfRangeException();
        }

        ListElement curr = LastAdded;
        int currIndex = 0;
        while (curr != null && position != 0)
        {
            curr = curr.Prev;
        }

        if (curr != null)
        {
            curr.Value = value;
            return;
        }

        throw new IndexOutOfRangeException();
    }
}

public class UniqueList<T> : List<T>
{
    public override void Append(T value)
    {
        if (LastAdded != null)
        {
            ListElement curr = LastAdded;
            while (curr != null)
            {
                if (Equals(curr.Value, value))
                {
                    throw new AddingAlreadyExistedElementException();
                }

                curr = curr.Prev;
            }

            ListElement newElement = new(value, LastAdded);
            LastAdded = newElement;
        }
        else
        {
            LastAdded = new ListElement(value, null);
        }
    }

    public override T GetValue(int index)
    {
        if (index < 0)
        {
            throw new IncorrectIndexException();
        }

        if (LastAdded == null)
        {
            throw new IndexOutOfRangeException();
        }

        ListElement curr = LastAdded;
        int currIndex = 0;
        while (curr != null && index != 0)
        {
            curr = curr.Prev;
            index--;
        }

        if (curr != null)
        {
            return curr.Value;
        }

        throw new IndexOutOfRangeException();
    }

    public override void DeleteByValue(T value)
    {
        ListElement curr = LastAdded;
        while (LastAdded != null && Equals(curr.Value, value))
        {
            curr = curr.Prev;
            LastAdded = curr;
            return;
        }

        ListElement preCurr = curr; //unnecessary initialization
        while (curr != null)
        {
            if (Equals(curr.Value, value))
            {
                preCurr.Prev = curr.Prev; //last cycle ended if curr is null then that cycle
                return;
            }

            preCurr = curr; // that cycle skips, if not equal, then at least first if skips
            curr = curr.Prev;
        }

        throw new NoneExistentElementRemovalException();
    }

    public override void ChangeByPosition(int position, T value)
    {
        if (position < 0)
        {
            throw new IncorrectIndexException();
        }

        if (LastAdded == null)
        {
            throw new IndexOutOfRangeException();
        }

        ListElement curr = LastAdded;
        int currIndex = 0;
        while (curr != null && position != 0)
        {
            curr = curr.Prev;
        }

        if (curr != null)
        {
            curr.Value = value;
            return;
        }

        throw new IndexOutOfRangeException();
    }
}

public class NoneExistentElementRemovalException : InvalidOperationException
{
    public NoneExistentElementRemovalException()
    {
    }

    public NoneExistentElementRemovalException(string message) : base(message)
    {
    }

    public NoneExistentElementRemovalException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class IndexOutOfRangeException : InvalidOperationException
{
    public IndexOutOfRangeException()
    {
    }

    public IndexOutOfRangeException(string message) : base(message)
    {
    }

    public IndexOutOfRangeException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class IncorrectIndexException : ArgumentException
{
    public IncorrectIndexException()
    {
    }

    public IncorrectIndexException(string message) : base(message)
    {
    }

    public IncorrectIndexException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class AddingAlreadyExistedElementException : InvalidOperationException
{
    public AddingAlreadyExistedElementException()
    {
    }

    public AddingAlreadyExistedElementException(string message) : base(message)
    {
    }

    public AddingAlreadyExistedElementException(string message, Exception inner) : base(message, inner)
    {
    }
}