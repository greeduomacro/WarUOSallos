// Decompiled with JetBrains decompiler
// Type: PlayUO.PCancelTrade
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PCancelTrade : Packet
  {
    public PCancelTrade(int Serial)
      : base((byte) 111)
    {
      this.m_Stream.Write((byte) 1);
      this.m_Stream.Write(Serial);
      this.m_Stream.Write(0);
      this.m_Stream.Write(0);
      this.m_Stream.Write((byte) 0);
    }
  }
}
