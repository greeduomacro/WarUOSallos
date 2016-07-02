// Decompiled with JetBrains decompiler
// Type: PlayUO.SkillTooltip
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class SkillTooltip : Tooltip
  {
    private Skill m_Skill;
    private float m_LastReal;
    private float m_LastUsed;

    public SkillTooltip(Skill s)
      : base("Value: 0.0 (0.0)", true)
    {
      this.m_Skill = s;
      this.m_Delay = 0.5f;
    }

    public override Gump GetGump()
    {
      if ((double) this.m_LastReal != (double) this.m_Skill.Real || (double) this.m_LastUsed != (double) this.m_Skill.Value)
      {
        this.m_LastReal = this.m_Skill.Real;
        this.m_LastUsed = this.m_Skill.Value;
        this.Text = string.Format("Value: {0:F1} ({1:F1})", (object) this.m_LastUsed, (object) this.m_LastReal);
      }
      return base.GetGump();
    }
  }
}
