// Decompiled with JetBrains decompiler
// Type: PlayUO.PBuyItems
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PBuyItems : Packet
  {
    public PBuyItems(int serial, BuyInfo[] info)
      : base((byte) 59)
    {
      this.m_Stream.Write(serial);
      this.m_Stream.Write((byte) 2);
      for (int index = 0; index < info.Length; ++index)
      {
        if (info[index].ToBuy > 0)
        {
          this.m_Stream.Write((byte) 26);
          this.m_Stream.Write(info[index].Item.Serial);
          this.m_Stream.Write((short) info[index].ToBuy);
        }
      }
    }
  }
}
