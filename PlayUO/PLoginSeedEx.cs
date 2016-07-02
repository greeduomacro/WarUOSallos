// Decompiled with JetBrains decompiler
// Type: PlayUO.PLoginSeedEx
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class PLoginSeedEx : Packet
  {
    public PLoginSeedEx(uint seed)
      : base((byte) 239, 21)
    {
      Version version = Engine.GetVersion();
      this.m_Stream.Write(seed);
      this.m_Stream.Write(version.Major);
      this.m_Stream.Write(version.Minor);
      this.m_Stream.Write(version.Build);
      this.m_Stream.Write(version.Revision);
    }
  }
}
