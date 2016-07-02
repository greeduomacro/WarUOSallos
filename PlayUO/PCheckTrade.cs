// Decompiled with JetBrains decompiler
// Type: PlayUO.PCheckTrade
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PCheckTrade : Packet
  {
    public PCheckTrade(Item item, bool check1, bool check2)
      : base((byte) 111)
    {
      this.m_Stream.Write((byte) 2);
      this.m_Stream.Write(item.Serial);
      this.m_Stream.Write(check1 ? 1 : 0);
      this.m_Stream.Write(check2 ? 1 : 0);
      this.m_Stream.Write((byte) 0);
    }
  }
}
