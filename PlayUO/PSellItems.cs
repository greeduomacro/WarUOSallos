// Decompiled with JetBrains decompiler
// Type: PlayUO.PSellItems
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  internal class PSellItems : Packet
  {
    public PSellItems(int serial, SellInfo[] info)
      : base((byte) 159)
    {
      ArrayList dataStore = Engine.GetDataStore();
      for (int index = 0; index < info.Length; ++index)
      {
        if (info[index].ToSell > 0)
          dataStore.Add((object) info[index]);
      }
      this.m_Stream.Write(serial);
      this.m_Stream.Write((ushort) dataStore.Count);
      for (int index = 0; index < dataStore.Count; ++index)
      {
        SellInfo sellInfo = (SellInfo) dataStore[index];
        this.m_Stream.Write(sellInfo.Item.Serial);
        this.m_Stream.Write((ushort) sellInfo.ToSell);
      }
      Engine.ReleaseDataStore(dataStore);
    }
  }
}
