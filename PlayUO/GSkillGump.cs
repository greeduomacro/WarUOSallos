// Decompiled with JetBrains decompiler
// Type: PlayUO.GSkillGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSkillGump : Gump
  {
    private Skill m_Skill;
    private GLabel m_Name;
    private GLabel m_Value;
    private GThreeToggle m_Lock;
    private int m_Width;
    private int m_Height;
    private int m_yBase;

    public int yBase
    {
      get
      {
        return this.m_yBase;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
        this.m_Value.X = this.m_Width - 24 - this.m_Value.Image.xMax;
        this.m_Lock.X = this.m_Width - 4 - 8 - this.m_Lock.Width / 2;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
      }
    }

    public GSkillGump(Skill skill, int y, int width, bool showReal)
      : base(4, y)
    {
      this.m_yBase = y;
      this.m_Skill = skill;
      this.m_Name = !skill.Action ? new GLabel(skill.Name, (IFont) Engine.GetUniFont(1), Hues.Bright, 20, 0) : (GLabel) new GSkillGump.GUsableSkillName(skill);
      this.m_Name.X -= this.m_Name.Image.xMin;
      this.m_Name.Y -= this.m_Name.Image.yMin;
      this.m_Width = width;
      this.m_Height = this.m_Name.Image.yMax - this.m_Name.Image.yMin;
      this.m_Children.Add((Gump) this.m_Name);
      this.m_Value = new GLabel((showReal ? this.m_Skill.Real : this.m_Skill.Value).ToString("F1"), (IFont) Engine.GetUniFont(1), Hues.Bright, 0, 0);
      this.m_Value.X = this.m_Width - 24 - this.m_Value.Image.xMax;
      this.m_Value.Y -= this.m_Value.Image.yMin;
      if (this.m_Value.Image.yMax - this.m_Value.Image.yMin > this.m_Height)
        this.m_Height = this.m_Value.Image.yMax - this.m_Value.Image.yMin;
      this.m_Children.Add((Gump) this.m_Value);
      this.m_Lock = new GThreeToggle(Engine.m_SkillUp, Engine.m_SkillDown, Engine.m_SkillLocked, (int) this.m_Skill.Lock, this.m_Width - 4, 0);
      this.m_Lock.OnStateChange = new OnStateChange(this.Lock_OnStateChange);
      this.UpdateLock();
      this.m_Children.Add((Gump) this.m_Lock);
    }

    public void Scissor(Clipper c)
    {
      this.m_Name.Scissor(c);
      this.m_Value.Scissor(c);
      this.m_Lock.Scissor(c);
    }

    private void Lock_OnStateChange(int state, Gump g)
    {
      SkillLock skillLock = (SkillLock) state;
      if (this.m_Skill.Lock == skillLock)
        return;
      this.m_Skill.Lock = skillLock;
      Network.Send((Packet) new PUpdateSkillLock(this.m_Skill));
    }

    private void UpdateLock()
    {
      this.m_Lock.X = this.m_Width - 4 - 8 - this.m_Lock.Width / 2;
      this.m_Lock.Y = (this.m_Height - this.m_Lock.Height) / 2;
    }

    public void OnSkillChange(double newValue, SkillLock newLock)
    {
      this.m_Value.Text = newValue.ToString("F1");
      this.m_Value.X = this.m_Width - 24 - this.m_Value.Image.xMax;
      this.m_Value.Y = -this.m_Value.Image.yMin;
      this.m_Lock.State = (int) newLock;
      this.UpdateLock();
    }

    private class GUsableSkillName : GTextButton
    {
      private Skill m_Skill;

      public GUsableSkillName(Skill skill)
        : base(skill.Name, (IFont) Engine.GetUniFont(1), Hues.Bright, Hues.Load(53), 20, 0, (OnClick) null)
      {
        this.m_Skill = skill;
        this.m_CanDrag = true;
        this.m_QuickDrag = false;
      }

      protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
      {
        this.m_Parent.BringToTop();
      }

      protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
      {
        this.m_Parent.BringToTop();
        if ((mb & MouseButtons.Left) == MouseButtons.None)
          return;
        this.m_Skill.Use();
      }

      protected internal override void OnDragStart()
      {
        this.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        GSkillIcon gskillIcon = new GSkillIcon(this.m_Skill);
        gskillIcon.m_IsDragging = true;
        gskillIcon.m_OffsetX = gskillIcon.Width / 2;
        gskillIcon.m_OffsetY = gskillIcon.Height / 2;
        gskillIcon.X = Engine.m_xMouse - gskillIcon.m_OffsetX;
        gskillIcon.Y = Engine.m_yMouse - gskillIcon.m_OffsetY;
        Gumps.Desktop.Children.Add((Gump) gskillIcon);
        Gumps.Drag = (Gump) gskillIcon;
      }
    }
  }
}
