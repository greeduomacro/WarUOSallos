// Decompiled with JetBrains decompiler
// Type: PlayUO.PPopupResponse
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PPopupResponse : Packet
  {
    public PPopupResponse(object o, int EntryID)
      : base((byte) 191)
    {
      this.m_Stream.Write((short) 21);
      this.m_Stream.Write(o is Item ? ((Agent) o).Serial : ((Agent) o).Serial);
      this.m_Stream.Write((short) EntryID);
    }
  }
}
