// Decompiled with JetBrains decompiler
// Type: PlayUO.Scratch`1
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public class Scratch<T> : IDisposable where T : new()
  {
    [ThreadStatic]
    private static Queue<T> queue;
    private T value;
    private Scratch<T>.State state;

    public T Value
    {
      get
      {
        if (this.state == Scratch<T>.State.Empty)
        {
          this.state = Scratch<T>.State.Acquired;
          this.value = this.Acquire();
        }
        else if (this.state == Scratch<T>.State.Released)
          throw new ObjectDisposedException("this");
        return this.value;
      }
    }

    protected virtual T Acquire()
    {
      if (Scratch<T>.queue == null)
        Scratch<T>.queue = new Queue<T>();
      if (Scratch<T>.queue.Count > 0)
        return Scratch<T>.queue.Dequeue();
      return new T();
    }

    protected virtual void Release(T value)
    {
      if (Scratch<T>.queue == null)
        Scratch<T>.queue = new Queue<T>();
      Scratch<T>.queue.Enqueue(value);
    }

    void IDisposable.Dispose()
    {
      if (this.state != Scratch<T>.State.Acquired)
        return;
      this.state = Scratch<T>.State.Released;
      this.Release(this.value);
    }

    private enum State
    {
      Empty,
      Acquired,
      Released,
    }
  }
}
