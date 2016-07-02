// Decompiled with JetBrains decompiler
// Type: PlayUO.Packet
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.IO;

namespace PlayUO
{
  public class Packet
  {
    protected bool m_Encode = true;
    protected int m_Length;
    public PacketWriter m_Stream;

    public bool Encode
    {
      get
      {
        return this.m_Encode;
      }
    }

    public Packet(byte packetID)
    {
      this.m_Stream = new PacketWriter();
      this.m_Stream.Write(packetID);
      this.m_Stream.Write((short) 0);
    }

    public Packet(byte packetID, int length)
    {
      this.m_Length = length;
      this.m_Stream = new PacketWriter(length);
      this.m_Stream.Write(packetID);
    }

    public void Dispose()
    {
      this.m_Stream.Close();
      this.m_Stream = (PacketWriter) null;
    }

    public byte[] Compile()
    {
      this.m_Stream.Flush();
      if (this.m_Length == 0)
      {
        long length = this.m_Stream.Length;
        this.m_Stream.Seek(1L, SeekOrigin.Begin);
        this.m_Stream.Write((ushort) length);
        this.m_Stream.Flush();
      }
      return this.m_Stream.Compile();
    }
  }
}
