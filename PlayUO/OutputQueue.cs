// Decompiled with JetBrains decompiler
// Type: PlayUO.OutputQueue
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public sealed class OutputQueue : IConsolidator
  {
    private object _syncRoot;
    private IBufferPolicy _bufferPolicy;
    private bool _isWorking;
    private Queue<OutputQueue.Gram> _pending;
    private OutputQueue.Gram _buffered;

    public bool IsFlushReady
    {
      get
      {
        if (this._pending.Count == 0)
          return this._buffered != null;
        return false;
      }
    }

    public bool IsEmpty
    {
      get
      {
        if (this._pending.Count == 0)
          return this._buffered == null;
        return false;
      }
    }

    public OutputQueue(IBufferPolicy bufferPolicy)
    {
      if (bufferPolicy == null)
        throw new ArgumentNullException("bufferPolicy");
      this._syncRoot = new object();
      this._bufferPolicy = bufferPolicy;
      this._pending = new Queue<OutputQueue.Gram>();
    }

    public OutputQueue.Gram Flush()
    {
      lock (this._syncRoot)
      {
        OutputQueue.Gram local_0 = (OutputQueue.Gram) null;
        if (this._buffered != null)
        {
          if (this._pending.Count == 0)
            local_0 = this._buffered;
          this._pending.Enqueue(this._buffered);
          this._buffered = (OutputQueue.Gram) null;
        }
        this._isWorking = local_0 != null;
        return local_0;
      }
    }

    public OutputQueue.Gram Proceed()
    {
      lock (this._syncRoot)
      {
        OutputQueue.Gram local_0 = (OutputQueue.Gram) null;
        if (this._pending.Count > 0)
        {
          this._pending.Dequeue().Release();
          if (this._pending.Count > 0)
            local_0 = this._pending.Peek();
          else
            this._isWorking = false;
        }
        else
          this._isWorking = false;
        return local_0;
      }
    }

    public OutputQueue.Gram Query()
    {
      lock (this._syncRoot)
      {
        OutputQueue.Gram local_0 = (OutputQueue.Gram) null;
        if (this._pending.Count > 0 && !this._isWorking)
        {
          local_0 = this._pending.Peek();
          this._isWorking = true;
        }
        return local_0;
      }
    }

    public void Enqueue(byte[] buffer, int offset, int length)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0 || offset >= buffer.Length)
        throw new ArgumentOutOfRangeException("offset", (object) offset, "Offset must be greater than or equal to zero and less than the size of the buffer.");
      if (length < 0 || length > buffer.Length)
        throw new ArgumentOutOfRangeException("length", (object) length, "Length cannot be less than zero or greater than the size of the buffer.");
      if (buffer.Length - offset < length)
        throw new ArgumentException("Offset and length do not point to a valid segment within the buffer.");
      lock (this._syncRoot)
      {
        while (length > 0)
        {
          if (this._buffered == null)
            this._buffered = OutputQueue.Gram.Acquire(this._bufferPolicy);
          int local_0 = this._buffered.Write(buffer, offset, length);
          offset += local_0;
          length -= local_0;
          if (this._buffered.IsFull)
          {
            this._pending.Enqueue(this._buffered);
            this._buffered = (OutputQueue.Gram) null;
          }
        }
      }
    }

    public void Clear()
    {
      lock (this._syncRoot)
      {
        if (this._buffered != null)
        {
          this._buffered.Release();
          this._buffered = (OutputQueue.Gram) null;
        }
        while (this._pending.Count > 0)
          this._pending.Dequeue().Release();
        this._isWorking = false;
      }
    }

    public sealed class Gram
    {
      private static Stack<OutputQueue.Gram> _pool = new Stack<OutputQueue.Gram>();
      private IBufferPolicy _bufferPolicy;
      private byte[] _buffer;
      private int _length;

      public byte[] Buffer
      {
        get
        {
          return this._buffer;
        }
      }

      public int Length
      {
        get
        {
          return this._length;
        }
      }

      public int Available
      {
        get
        {
          return this._buffer.Length - this._length;
        }
      }

      public bool IsFull
      {
        get
        {
          return this._length == this._buffer.Length;
        }
      }

      private Gram()
      {
      }

      public static OutputQueue.Gram Acquire(IBufferPolicy bufferPolicy)
      {
        lock (OutputQueue.Gram._pool)
        {
          OutputQueue.Gram local_0 = OutputQueue.Gram._pool.Count <= 0 ? new OutputQueue.Gram() : OutputQueue.Gram._pool.Pop();
          local_0._bufferPolicy = bufferPolicy;
          local_0._buffer = bufferPolicy.Acquire();
          local_0._length = 0;
          return local_0;
        }
      }

      public int Write(byte[] buffer, int offset, int length)
      {
        int count = Math.Min(length, this.Available);
        Buffer.BlockCopy((Array) buffer, offset, (Array) this._buffer, this._length, count);
        this._length += count;
        return count;
      }

      public void Release()
      {
        lock (OutputQueue.Gram._pool)
        {
          OutputQueue.Gram._pool.Push(this);
          this._bufferPolicy.Release(this._buffer);
        }
      }
    }
  }
}
