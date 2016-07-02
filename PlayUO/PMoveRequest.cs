// Decompiled with JetBrains decompiler
// Type: PlayUO.PMoveRequest
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  internal class PMoveRequest : Packet
  {
    public PMoveRequest(int dir, int seq, uint key, int x, int y, int z, TimeSpan speed)
      : base((byte) 2, 7)
    {
      this.m_Stream.Write((byte) dir);
      this.m_Stream.Write((byte) seq);
      this.m_Stream.Write(key);
      PacketHandlers.AddSequence(seq, x, y, z, speed);
    }
  }
}
