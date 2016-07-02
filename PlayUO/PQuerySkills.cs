// Decompiled with JetBrains decompiler
// Type: PlayUO.PQuerySkills
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PQuerySkills : Packet
  {
    public PQuerySkills()
      : base((byte) 52, 10)
    {
      this.m_Stream.Write(-303174163);
      this.m_Stream.Write((byte) 5);
      this.m_Stream.Write(World.Serial);
    }
  }
}
