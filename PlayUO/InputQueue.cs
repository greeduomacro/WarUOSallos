// Decompiled with JetBrains decompiler
// Type: PlayUO.InputQueue
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public sealed class InputQueue : IConsolidator
  {
    private int _head;
    private int _tail;
    private int _size;
    private byte[] _buffer;

    public int Length
    {
      get
      {
        return this._size;
      }
    }

    public InputQueue()
    {
      this._buffer = new byte[2048];
    }

    public void Clear()
    {
      this._head = 0;
      this._tail = 0;
      this._size = 0;
    }

    private void SetCapacity(int capacity)
    {
      byte[] numArray = new byte[capacity];
      if (this._size > 0)
      {
        if (this._head < this._tail)
        {
          Buffer.BlockCopy((Array) this._buffer, this._head, (Array) numArray, 0, this._size);
        }
        else
        {
          Buffer.BlockCopy((Array) this._buffer, this._head, (Array) numArray, 0, this._buffer.Length - this._head);
          Buffer.BlockCopy((Array) this._buffer, 0, (Array) numArray, this._buffer.Length - this._head, this._tail);
        }
      }
      this._head = 0;
      this._tail = this._size;
      this._buffer = numArray;
    }

    public int GetPacketId()
    {
      if (this._size >= 1)
        return (int) this._buffer[this._head];
      return -1;
    }

    public int GetPacketLength()
    {
      if (this._size >= 3)
        return (int) this._buffer[(this._head + 1) % this._buffer.Length] << 8 | (int) this._buffer[(this._head + 2) % this._buffer.Length];
      return -1;
    }

    public ArraySegment<byte> Dequeue(int size)
    {
      if (size > this._size)
        size = this._size;
      ArraySegment<byte> arraySegment;
      if (size > 0)
      {
        if (this._head < this._tail)
        {
          arraySegment = new ArraySegment<byte>(this._buffer, this._head, size);
        }
        else
        {
          int num = this._buffer.Length - this._head;
          if (num >= size)
          {
            arraySegment = new ArraySegment<byte>(this._buffer, this._head, size);
          }
          else
          {
            byte[] array = new byte[size];
            Buffer.BlockCopy((Array) this._buffer, this._head, (Array) array, 0, num);
            Buffer.BlockCopy((Array) this._buffer, 0, (Array) array, num, size - num);
            arraySegment = new ArraySegment<byte>(array, 0, array.Length);
          }
        }
        this._head = (this._head + size) % this._buffer.Length;
        this._size -= size;
        if (this._size == 0)
        {
          this._head = 0;
          this._tail = 0;
        }
      }
      else
        arraySegment = new ArraySegment<byte>(this._buffer, 0, 0);
      return arraySegment;
    }

    public void Enqueue(byte[] buffer, int offset, int size)
    {
      if (this._size + size > this._buffer.Length)
        this.SetCapacity(this._size + size + 2047 & -2048);
      if (this._head < this._tail)
      {
        int count = this._buffer.Length - this._tail;
        if (count >= size)
        {
          Buffer.BlockCopy((Array) buffer, offset, (Array) this._buffer, this._tail, size);
        }
        else
        {
          Buffer.BlockCopy((Array) buffer, offset, (Array) this._buffer, this._tail, count);
          Buffer.BlockCopy((Array) buffer, offset + count, (Array) this._buffer, 0, size - count);
        }
      }
      else
        Buffer.BlockCopy((Array) buffer, offset, (Array) this._buffer, this._tail, size);
      this._tail = (this._tail + size) % this._buffer.Length;
      this._size += size;
    }
  }
}
