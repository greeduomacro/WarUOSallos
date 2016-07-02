// Decompiled with JetBrains decompiler
// Type: PlayUO.PMultiTarget_Response
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PMultiTarget_Response : Packet
  {
    public PMultiTarget_Response(int targetID, int x, int y, int z, int id)
      : base((byte) 108, 19)
    {
      this.m_Stream.Write((byte) 1);
      this.m_Stream.Write(targetID);
      this.m_Stream.Write((byte) 0);
      this.m_Stream.Write(0);
      this.m_Stream.Write((short) x);
      this.m_Stream.Write((short) y);
      this.m_Stream.Write((short) z);
      this.m_Stream.Write((short) id);
    }
  }
}
