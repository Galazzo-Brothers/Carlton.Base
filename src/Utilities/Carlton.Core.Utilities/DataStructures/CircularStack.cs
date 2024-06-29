using System.Collections;
namespace Carlton.Core.Utilities.DataStructures;

/// <summary>
/// Represents a circular stack data structure that stores elements of type T.
/// </summary>
/// <typeparam name="T">The type of elements stored in the stack.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="CircularStack{T}"/> class with the specified size.
/// </remarks>
/// <param name="size">The maximum capacity of the circular stack.</param>
public class CircularStack<T>(int size) : IEnumerable<T>
{
    private readonly T[] items = new T[size];
    private int head = 0; // Points to the next available slot
    private int count = 0; // Current number of elements in the stack

    /// <summary>
    /// Pushes an item onto the top of the stack.
    /// </summary>
    /// <param name="item">The item to push onto the stack.</param>
    public void Push(T item)
    {
        // If the stack is full, push out the oldest item by advancing the head pointer
        if (count < size)
            count++;

        items[head] = item;
        head = (head + 1) % size; // Move head circularly
    }

    /// <summary>
    /// Removes and returns the item at the top of the stack.
    /// </summary>
    /// <returns>The item at the top of the stack.</returns>
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

    /// <summary>
    /// Returns the item at the top of the stack without removing it.
    /// </summary>
    /// <returns>The item at the top of the stack.</returns>
    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Stack is empty");

        int peekIndex = (head == 0) ? (size - 1) : head - 1; // Move head circularly
        return items[peekIndex];
    }

    /// <summary>
    /// Gets the number of elements contained in the stack.
    /// </summary>
    public int Count => count;

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    public bool IsEmpty => count == 0;

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the stack.</returns>
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

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the stack.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}