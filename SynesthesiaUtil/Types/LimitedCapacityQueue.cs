// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections;
using System.Collections.Generic;

namespace SynesthesiaUtil.Types;

public class LimitedCapacityQueue<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    public bool IsFull => Count == capacity;

    private readonly T[] array;
    private readonly int capacity;

    private int start, end;

    public LimitedCapacityQueue(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        this.capacity = capacity;
        array = new T[capacity];
        Clear();
    }

    public void Clear()
    {
        start = 0;
        end = -1;
        Count = 0;
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        var result = array[start];
        start = (start + 1) % capacity;
        Count--;
        return result;
    }

    public void Enqueue(T item)
    {
        end = (end + 1) % capacity;
        if (Count == capacity)
            start = (start + 1) % capacity;
        else
            Count++;
        array[end] = item;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return array[(start + index) % capacity];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (Count == 0)
            yield break;

        for (int i = 0; i < Count; i++)
            yield return array[(start + i) % capacity];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}