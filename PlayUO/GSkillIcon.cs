// Decompiled with JetBrains decompiler
// Type: PlayUO.GSkillIcon
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GSkillIcon : GAlphaBackground
  {
    private Skill m_Skill;

    public int SkillID
    {
      get
      {
        return this.m_Skill.ID;
      }
    }

    public GSkillIcon(Skill skill)
      : base(0, 0, 100, 20)
    {
      this.m_Skill = skill;
      GLabel glabel = (GLabel) new GSkillIcon.GSkillIconName(skill);
      this.m_Width = glabel.Image.xMax - glabel.Image.xMin + 9;
      this.m_Height = glabel.Image.yMax - glabel.Image.yMin + 9;
      this.m_Children.Add((Gump) glabel);
      this.m_DragClipX = this.m_Width - 1;
      this.m_DragClipY = this.m_Height - 1;
    }

    public override GumpLayout CreateLayout()
    {
      return (GumpLayout) new SkillIconLayout();
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      this.ManualClose();
    }

    private class GSkillIconName : GTextButton
    {
      private Skill m_Skill;

      public GSkillIconName(Skill skill)
        : base(skill.Name, (IFont) Engine.GetUniFont(0), Hues.Bright, Hues.Load(53), 4, 4, (OnClick) null)
      {
        this.m_Skill = skill;
        this.m_CanDrag = true;
        this.m_QuickDrag = false;
        this.X -= this.Image.xMin;
        this.Y -= this.Image.yMin;
        this.m_Tooltip = (ITooltip) new SkillTooltip(skill);
      }

      protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
      {
        this.BringToTop();
      }

      protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
      {
        if ((mb & MouseButtons.Right) != MouseButtons.None)
        {
          this.m_Parent.ManualClose();
        }
        else
        {
          if ((mb & MouseButtons.Left) == MouseButtons.None)
            return;
          this.m_Skill.Use();
        }
      }

      protected internal override void OnDragStart()
      {
        this.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        this.m_Parent.m_IsDragging = true;
        this.m_Parent.m_OffsetX = this.m_X + this.m_OffsetX;
        this.m_Parent.m_OffsetY = this.m_Y + this.m_OffsetY;
        this.m_Parent.X = Engine.m_xMouse - this.m_Parent.m_OffsetX;
        this.m_Parent.Y = Engine.m_yMouse - this.m_Parent.m_OffsetY;
        Gumps.Drag = this.m_Parent;
      }

      protected internal override bool HitTest(int x, int y)
      {
        if (!Engine.amMoving && !TargetManager.IsActive && (x >= this.m_Image.xMin && x <= this.m_Image.xMax) && y >= this.m_Image.yMin)
          return y <= this.m_Image.yMax;
        return false;
      }
    }
  }
}
