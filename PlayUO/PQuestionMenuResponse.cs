// Decompiled with JetBrains decompiler
// Type: PlayUO.PQuestionMenuResponse
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PQuestionMenuResponse : Packet
  {
    public PQuestionMenuResponse(int serial, int menuID, int index, int itemID, int hue)
      : base((byte) 125, 13)
    {
      this.m_Stream.Write(serial);
      this.m_Stream.Write((short) menuID);
      this.m_Stream.Write((short) (index + 1));
      this.m_Stream.Write((short) itemID);
      this.m_Stream.Write((short) hue);
    }
  }
}
