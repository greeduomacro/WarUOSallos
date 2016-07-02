// Decompiled with JetBrains decompiler
// Type: PlayUO.ContiguousAllocator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public sealed class ContiguousAllocator : IBufferPolicy
  {
    private object _syncRoot;
    private int _bufferSize;
    private Stack<byte[]> _stack;
    private int _capacity;
    private int _misses;

    public int BufferSize
    {
      get
      {
        return this._bufferSize;
      }
    }

    public int Available
    {
      get
      {
        return this._stack.Count;
      }
    }

    public int Capacity
    {
      get
      {
        return this._capacity;
      }
    }

    public int Misses
    {
      get
      {
        return this._misses;
      }
    }

    public ContiguousAllocator(int bufferSize, int initialCapacity)
    {
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", (object) bufferSize, "Buffer size must be greater than zero.");
      if (initialCapacity <= 0)
        throw new ArgumentOutOfRangeException("initialCapacity", (object) initialCapacity, "Initial capacity must be greater than zero.");
      this._syncRoot = new object();
      this._bufferSize = bufferSize;
      this._stack = new Stack<byte[]>(initialCapacity);
      this.EnsureCapacity(initialCapacity);
    }

    private void EnsureCapacity(int capacity)
    {
      for (; this._capacity < capacity; ++this._capacity)
        this._stack.Push(new byte[this._bufferSize]);
    }

    public byte[] Acquire()
    {
      lock (this._syncRoot)
      {
        if (this._stack.Count == 0)
        {
          ++this._misses;
          this.EnsureCapacity(this._capacity * 2);
        }
        return this._stack.Pop();
      }
    }

    public void Release(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      lock (this._syncRoot)
        this._stack.Push(buffer);
    }
  }
}
