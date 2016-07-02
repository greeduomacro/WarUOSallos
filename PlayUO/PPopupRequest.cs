// Decompiled with JetBrains decompiler
// Type: PlayUO.PPopupRequest
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PPopupRequest : Packet
  {
    public PPopupRequest(Mobile Target)
      : this(Target.Serial)
    {
    }

    public PPopupRequest(Item Target)
      : this(Target.Serial)
    {
    }

    public PPopupRequest(MobileCell Target)
      : this(Target.m_Mobile.Serial)
    {
    }

    protected PPopupRequest(int Serial)
      : base((byte) 191)
    {
      this.m_Stream.Write((short) 19);
      this.m_Stream.Write(Serial);
    }
  }
}
