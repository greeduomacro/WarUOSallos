// Decompiled with JetBrains decompiler
// Type: PlayUO.BufferAllocator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public sealed class BufferAllocator : IBufferPolicy
  {
    private readonly int bufferSize;

    public int BufferSize
    {
      get
      {
        return this.bufferSize;
      }
    }

    public BufferAllocator(int bufferSize)
    {
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", (object) bufferSize, "Argument 'bufferSize' must be greater than zero.");
      this.bufferSize = bufferSize;
    }

    public byte[] Acquire()
    {
      return new byte[this.bufferSize];
    }

    public void Release(byte[] buffer)
    {
    }
  }
}
