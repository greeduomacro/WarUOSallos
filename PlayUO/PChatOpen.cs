// Decompiled with JetBrains decompiler
// Type: PlayUO.PChatOpen
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PChatOpen : Packet
  {
    public PChatOpen(string un)
      : base((byte) 181, 64)
    {
      this.m_Stream.Write((byte) 1);
      if (un.Length > 31)
        un = un.Substring(0, 31);
      else if (un.Length < 31)
        un += new string(char.MinValue, 31 - un.Length);
      this.m_Stream.WriteUnicode(un);
    }
  }
}
