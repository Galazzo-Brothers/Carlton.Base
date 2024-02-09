using System.Collections;

namespace Carlton.Core.Utilities.DataStructures;

public class CircularStack<T>(int size) : IEnumerable<T>
{
    private readonly T[] items = new T[size];
    private int head = 0; // Points to the next available slot
    private int count = 0; // Current number of elements in the stack

    public void Push(T item)
    {
        // If the stack is full, push out the oldest item by advancing the head pointer
        if (count < size)
            count++;

        items[head] = item;
        head = (head + 1) % size; // Move head circularly
    }

    public T Pop()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        head = (head == 0) ? (size - 1) : head - 1; // Move head circularly
        T item = items[head];
        items[head] = default; // Reset the value in the array
        count--;

        return item;
    }

    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        int peekIndex = (head == 0) ? (size - 1) : head - 1; // Move head circularly
        return items[peekIndex];
    }

    public int Count => count;

    public bool IsEmpty => count == 0;


    // Custom iterator method to iterate through the elements in the stack
    public IEnumerator<T> GetEnumerator()
    {
        // Calculate the index of the previous element of the head
        int startIndex = (head - 1 + items.Length) % items.Length;

        // Start the iteration from the previous element of the head
        for (int i = startIndex, j = 0; j < count; i = (i - 1 + items.Length) % items.Length, j++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}