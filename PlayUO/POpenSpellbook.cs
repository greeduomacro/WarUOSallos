// Decompiled with JetBrains decompiler
// Type: PlayUO.POpenSpellbook
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class POpenSpellbook : Packet
  {
    public POpenSpellbook(int num)
      : base((byte) 18)
    {
      this.m_Stream.Write((byte) 67);
      this.m_Stream.Write(num.ToString());
      this.m_Stream.Write((byte) 0);
    }
  }
}
