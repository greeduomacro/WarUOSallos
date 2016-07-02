// Decompiled with JetBrains decompiler
// Type: PlayUO.PSetWarMode
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PSetWarMode : Packet
  {
    public PSetWarMode(bool warMode, short unk1, byte unk2)
      : base((byte) 114, 5)
    {
      this.m_Stream.Write(warMode);
      this.m_Stream.Write(unk1);
      this.m_Stream.Write(unk2);
    }
  }
}
