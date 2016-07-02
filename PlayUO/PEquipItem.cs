// Decompiled with JetBrains decompiler
// Type: PlayUO.PEquipItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PEquipItem : Packet
  {
    public PEquipItem(Item toEquip, Mobile target)
      : base((byte) 19, 10)
    {
      this.m_Stream.Write(toEquip.Serial);
      this.m_Stream.Write(Map.GetQuality(toEquip.ID & 16383));
      this.m_Stream.Write(target.Serial);
    }
  }
}
