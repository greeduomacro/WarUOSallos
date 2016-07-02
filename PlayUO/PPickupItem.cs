// Decompiled with JetBrains decompiler
// Type: PlayUO.PPickupItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  internal class PPickupItem : Packet
  {
    public static Item m_Item;

    public PPickupItem(Item item, int amount)
      : base((byte) 7, 7)
    {
      PPickupItem.m_Item = item;
      Engine.m_LastAction = DateTime.Now;
      this.m_Stream.Write(item.Serial);
      this.m_Stream.Write(checked ((ushort) amount));
    }
  }
}
