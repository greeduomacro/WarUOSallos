// Decompiled with JetBrains decompiler
// Type: PlayUO.PQueryProperties
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PQueryProperties : Packet
  {
    public PQueryProperties(int serial)
      : base((byte) 214)
    {
      this.m_Stream.Write(serial);
    }
  }
}
