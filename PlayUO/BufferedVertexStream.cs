// Decompiled with JetBrains decompiler
// Type: PlayUO.BufferedVertexStream
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;
using SharpDX.Direct3D9;
using System.IO;

namespace PlayUO
{
  public class BufferedVertexStream
  {
    private int m_VertexBufferOffset;
    private int m_VertexBufferLength;
    private int m_SizePerVertex;
    private VertexBuffer m_Buffer;
    private DataStream m_Stream;

    public int Length
    {
      get
      {
        return this.m_VertexBufferLength;
      }
    }

    public BufferedVertexStream(VertexBuffer buffer, int vertexBufferLength, int sizePerVertex)
    {
      this.m_Buffer = buffer;
      this.m_VertexBufferLength = vertexBufferLength;
      this.m_SizePerVertex = sizePerVertex;
    }

    public void Unlock()
    {
      if (this.m_Stream == null)
        return;
      try
      {
        ((Stream) this.m_Stream).Close();
        this.m_Buffer.Unlock();
      }
      catch
      {
      }
      this.m_Stream = (DataStream) null;
    }

    public int Push(byte[] buffer, int vertexOffset, int vertexCount, bool unlock)
    {
      int num;
      if (this.m_VertexBufferLength >= this.m_VertexBufferOffset + vertexCount)
      {
        if (this.m_Stream == null)
          this.m_Stream = !unlock ? this.m_Buffer.Lock(this.m_VertexBufferOffset * this.m_SizePerVertex, (this.m_VertexBufferLength - this.m_VertexBufferOffset) * this.m_SizePerVertex, (LockFlags) 4096) : this.m_Buffer.Lock(this.m_VertexBufferOffset * this.m_SizePerVertex, vertexCount * this.m_SizePerVertex, (LockFlags) 4096);
        this.m_Stream.WriteRange<byte>((M0[]) buffer, vertexOffset * this.m_SizePerVertex, vertexCount * this.m_SizePerVertex);
        num = this.m_VertexBufferOffset;
        this.m_VertexBufferOffset += vertexCount;
        if (unlock)
          this.Unlock();
      }
      else if (vertexCount <= this.m_VertexBufferLength)
      {
        this.Unlock();
        if (this.m_Stream == null)
          this.m_Stream = !unlock ? this.m_Buffer.Lock(0, this.m_VertexBufferLength * this.m_SizePerVertex, (LockFlags) 8192) : this.m_Buffer.Lock(0, vertexCount * this.m_SizePerVertex, (LockFlags) 8192);
        this.m_Stream.WriteRange<byte>((M0[]) buffer, vertexOffset * this.m_SizePerVertex, vertexCount * this.m_SizePerVertex);
        num = 0;
        this.m_VertexBufferOffset = vertexCount;
        if (unlock)
          this.Unlock();
      }
      else
        num = -1;
      return num;
    }
  }
}
