// Decompiled with JetBrains decompiler
// Type: PlayUO.PGameSeed
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.IO;

namespace PlayUO
{
  internal class PGameSeed : Packet
  {
    public PGameSeed(uint gameSeed)
      : base((byte) 0, 4)
    {
      this.m_Encode = false;
      this.m_Stream.Seek(0L, SeekOrigin.Begin);
      this.m_Stream.Write(gameSeed);
    }
  }
}
