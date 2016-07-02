// Decompiled with JetBrains decompiler
// Type: PlayUO.PCastSpell
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PCastSpell : Packet
  {
    public static int m_LastSpellID = -1;

    public PCastSpell(int spellID)
      : base((byte) 191)
    {
      PCastSpell.m_LastSpellID = spellID;
      this.m_Stream.Write((ushort) 28);
      this.m_Stream.Write((ushort) 0);
      this.m_Stream.Write((ushort) spellID);
    }

    public static void SendLast()
    {
      if (PCastSpell.m_LastSpellID < 0)
        return;
      Network.Send((Packet) new PCastSpell(PCastSpell.m_LastSpellID));
    }
  }
}
