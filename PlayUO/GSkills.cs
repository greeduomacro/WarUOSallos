// Decompiled with JetBrains decompiler
// Type: PlayUO.GSkills
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSkills : GAlphaBackground, IResizable
  {
    private GLabel m_Total;
    private GTextButton m_ValueType;
    private GSkillList m_SkillList;
    private bool m_ShowReal;
    private bool m_ShouldClose;

    public bool ShowReal
    {
      get
      {
        return this.m_ShowReal;
      }
    }

    public int MinWidth
    {
      get
      {
        return 225;
      }
    }

    public int MaxWidth
    {
      get
      {
        return Engine.ScreenWidth;
      }
    }

    public int MinHeight
    {
      get
      {
        return 43;
      }
    }

    public int MaxHeight
    {
      get
      {
        return Engine.ScreenHeight;
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
        this.m_Total.X = this.m_Width - 5 - this.m_Total.Image.xMax;
        this.m_SkillList.Width = this.m_Width - 8;
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
        this.m_Total.Y = this.m_Height - 5 - this.m_Total.Image.yMax;
        this.m_ValueType.Y = this.m_Height - 5 - this.m_ValueType.Image.yMax;
        int num = this.m_Total.Y + this.m_Total.Image.yMin;
        if (this.m_ValueType.Y + this.m_ValueType.Image.yMin < num)
          num = this.m_ValueType.Y + this.m_ValueType.Image.yMin;
        this.m_SkillList.Height = num - 7;
      }
    }

    public GSkills()
      : base(50, 50, 250, 125)
    {
      this.m_Children.Add((Gump) new GVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLResizer((IResizable) this));
      this.m_Children.Add((Gump) new GTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLVResizer((IResizable) this));
      this.m_ShowReal = false;
      this.m_Total = new GLabel(string.Format("Total: {0:F1}", (object) this.GetTotalSkillCount()), (IFont) Engine.GetUniFont(1), Hues.Bright, 0, 0);
      this.m_Children.Add((Gump) this.m_Total);
      this.m_ValueType = new GTextButton("Used Values", (IFont) Engine.GetUniFont(1), Hues.Bright, Hues.Load(53), 0, 0, new OnClick(this.ValueType_OnClick));
      this.m_ValueType.X = 4 - this.m_ValueType.Image.xMin;
      this.m_Children.Add((Gump) this.m_ValueType);
      this.m_SkillList = new GSkillList(this);
      this.m_Children.Add((Gump) this.m_SkillList);
      this.Width = 250;
      this.Height = 125;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      this.m_ShouldClose = true;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if (this.m_ShouldClose && (mb & MouseButtons.Right) != MouseButtons.None)
        this.ManualClose();
      else
        this.m_ShouldClose = false;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (!this.m_ShouldClose || (mb & MouseButtons.Right) != MouseButtons.None)
        return;
      this.m_ShouldClose = false;
    }

    protected internal override void OnDispose()
    {
      Engine.m_SkillsOpen = false;
      Engine.m_SkillsGump = (GSkills) null;
    }

    protected internal override void Render(int x, int y)
    {
      base.Render(x, y);
    }

    private float GetTotalSkillCount()
    {
      float num = 0.0f;
      Skills skills = Engine.Skills;
      for (int index = 0; index < 256; ++index)
      {
        Skill skill = skills[index];
        if (skill != null)
          num += skill.Real;
        else
          break;
      }
      return num;
    }

    private void ValueType_OnClick(Gump g)
    {
      this.m_ShowReal = !this.m_ShowReal;
      this.m_ValueType.Text = this.m_ShowReal ? "Real Values" : "Used Values";
      this.m_ValueType.X = 4 - this.m_ValueType.Image.xMin;
      this.m_ValueType.Y = this.m_Height - 5 - this.m_ValueType.Image.yMax;
      this.m_SkillList.ShowReal = this.m_ShowReal;
    }

    public void OnSkillChange(Skill skill)
    {
      this.m_SkillList.OnSkillChange(skill, this.m_ShowReal);
      this.UpdateTotal();
    }

    private void UpdateTotal()
    {
      this.m_Total.Text = string.Format("Total: {0:F1}", (object) this.GetTotalSkillCount());
      this.m_Total.X = this.m_Width - 5 - this.m_Total.Image.xMax;
      this.m_Total.Y = this.m_Height - 5 - this.m_Total.Image.yMax;
      int num = this.m_Total.Y + this.m_Total.Image.yMin;
      if (this.m_ValueType.Y + this.m_ValueType.Image.yMin < num)
        num = this.m_ValueType.Y + this.m_ValueType.Image.yMin;
      this.m_SkillList.Height = num - 7;
    }
  }
}
