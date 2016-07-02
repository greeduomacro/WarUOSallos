// Decompiled with JetBrains decompiler
// Type: PlayUO.PDropItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PDropItem : Packet
  {
    public PDropItem(int Serial, int X, int Y, int Z, int DestSerial)
      : base((byte) 8, 15)
    {
      this.m_Stream.Write(Serial);
      this.m_Stream.Write((short) X);
      this.m_Stream.Write((short) Y);
      this.m_Stream.Write((sbyte) Z);
      this.m_Stream.Write((sbyte) -1);
      this.m_Stream.Write(DestSerial);
    }
  }
}
