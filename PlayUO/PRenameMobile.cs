// Decompiled with JetBrains decompiler
// Type: PlayUO.PRenameMobile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PRenameMobile : Packet
  {
    public PRenameMobile(int Serial, string Name)
      : base((byte) 117, 35)
    {
      this.m_Stream.Write(Serial);
      this.m_Stream.Write(Name, 30);
    }
  }
}
