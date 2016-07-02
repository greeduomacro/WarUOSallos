// Decompiled with JetBrains decompiler
// Type: PlayUO.PClientVersion
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PClientVersion : Packet
  {
    public PClientVersion(string version)
      : base((byte) 189)
    {
      this.m_Stream.Write(version);
      this.m_Stream.Write((byte) 0);
    }
  }
}
