// Decompiled with JetBrains decompiler
// Type: PlayUO.PGumpButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  internal class PGumpButton : Packet
  {
    public PGumpButton(int serial, int dialogID, int buttonID)
      : base((byte) 177)
    {
      this.m_Stream.Write(serial);
      this.m_Stream.Write(dialogID);
      this.m_Stream.Write(buttonID);
      this.m_Stream.Write(0);
      this.m_Stream.Write(0);
    }

    public PGumpButton(GServerGump gump, int buttonID)
      : base((byte) 177)
    {
      this.m_Stream.Write(gump.Serial);
      this.m_Stream.Write(gump.DialogID);
      this.m_Stream.Write(buttonID);
      ArrayList dataStore1 = Engine.GetDataStore();
      ArrayList dataStore2 = Engine.GetDataStore();
      Gump[] array = gump.Children.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] is IRelayedSwitch)
        {
          IRelayedSwitch relayedSwitch = (IRelayedSwitch) array[index];
          if (relayedSwitch.Active)
            dataStore1.Add((object) relayedSwitch.RelayID);
        }
        else if (array[index] is GServerTextBox)
          dataStore2.Add((object) array[index]);
      }
      this.m_Stream.Write(dataStore1.Count);
      for (int index = 0; index < dataStore1.Count; ++index)
        this.m_Stream.Write((int) dataStore1[index]);
      this.m_Stream.Write(dataStore2.Count);
      for (int index = 0; index < dataStore2.Count; ++index)
      {
        GServerTextBox gserverTextBox = (GServerTextBox) dataStore2[index];
        this.m_Stream.Write((short) gserverTextBox.RelayID);
        this.m_Stream.Write((short) gserverTextBox.String.Length);
        this.m_Stream.WriteUnicode(gserverTextBox.String);
      }
      Engine.ReleaseDataStore(dataStore2);
      Engine.ReleaseDataStore(dataStore1);
    }
  }
}
