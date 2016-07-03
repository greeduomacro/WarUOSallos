// Decompiled with JetBrains decompiler
// Type: PlayUO.TextureVB
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Runtime.InteropServices;

namespace PlayUO
{
  public class TextureVB : IVertexStorage
  {
      private static readonly int VertexSize = Marshal.SizeOf(typeof(TransformedColoredTextured));
    public static ContiguousAllocator[] _allocators = new ContiguousAllocator[7]{ new ContiguousAllocator(4 * TextureVB.VertexSize, 4096), new ContiguousAllocator(8 * TextureVB.VertexSize, 2048), new ContiguousAllocator(16 * TextureVB.VertexSize, 1024), new ContiguousAllocator(32 * TextureVB.VertexSize, 512), new ContiguousAllocator(64 * TextureVB.VertexSize, 256), new ContiguousAllocator(128 * TextureVB.VertexSize, 128), new ContiguousAllocator(256 * TextureVB.VertexSize, 64) };
    public int _misses;
    public byte[] _buffer;
    public int _length;
    public int m_Count;
    public int m_Frame;

    public TextureVB()
    {
      this._buffer = TextureVB._allocators[this._misses].Acquire();
      this.m_Frame = -1;
    }

    public void Release()
    {
      if (this._misses >= TextureVB._allocators.Length)
        return;
      TextureVB._allocators[this._misses].Release(this._buffer);
    }

    public ArraySegment<byte> Store(int vertexCount, int primitiveCount)
    {
      if (Renderer._profile != null)
        Renderer._profile._storeTime.Start();
      if (this.m_Frame != Renderer._renderCount)
      {
        this.m_Frame = Renderer._renderCount;
        this.m_Count = 0;
        this._length = 0;
      }
      this.m_Count += primitiveCount;
      int count = vertexCount * TextureVB.VertexSize;
      this._length += count;
      if (this._length > this._buffer.Length)
      {
        byte[] buffer = this._buffer;
        do
        {
          if (this._misses < TextureVB._allocators.Length)
            TextureVB._allocators[this._misses].Release(buffer);
          ++this._misses;
          buffer = this._misses >= TextureVB._allocators.Length ? new byte[buffer.Length * 2] : TextureVB._allocators[this._misses].Acquire();
        }
        while (this._length > buffer.Length);
        Buffer.BlockCopy((Array) this._buffer, 0, (Array) buffer, 0, this._length - count);
        this._buffer = buffer;
      }
      ArraySegment<byte> arraySegment = new ArraySegment<byte>(this._buffer, this._length - count, count);
      if (Renderer._profile != null)
        Renderer._profile._storeTime.Stop();
      return arraySegment;
    }
  }
}
