// Decompiled with JetBrains decompiler
// Type: PlayUO.PUseSkill
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class PUseSkill : Packet
  {
    private static Skill m_LastSkill;

    public PUseSkill(Skill skill)
      : base((byte) 18)
    {
      PUseSkill.m_LastSkill = skill;
      this.m_Stream.Write((byte) 36);
      this.m_Stream.Write(string.Format("{0} 0", (object) skill.ID));
      this.m_Stream.Write((byte) 0);
    }

    public static void SendLast()
    {
      if (PUseSkill.m_LastSkill == null)
        return;
      Network.Send((Packet) new PUseSkill(PUseSkill.m_LastSkill));
    }
  }
}
