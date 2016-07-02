// Decompiled with JetBrains decompiler
// Type: PlayUO.GSkillList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GSkillList : GSingleBorder
  {
    private static Type tGLabel = typeof (GLabel);
    private static Type tGSkillGump = typeof (GSkillGump);
    private GAlphaVSlider m_Slider;
    private GSingleBorder m_SliderBorder;
    private GHotspot m_Hotspot;
    private int m_xLast;
    private int m_yLast;
    private int m_xWidthLast;
    private int m_yHeightLast;
    private GSkills m_Owner;
    private GSkillGump[] m_SkillGumps;

    public bool ShowReal
    {
      set
      {
        Skills skills = Engine.Skills;
        if (!value)
        {
          for (int index = 0; index < this.m_SkillGumps.Length && this.m_SkillGumps[index] != null; ++index)
          {
            Skill skill = skills[index];
            this.m_SkillGumps[index].OnSkillChange((double) skill.Value, skill.Lock);
          }
        }
        else
        {
          for (int index = 0; index < this.m_SkillGumps.Length && this.m_SkillGumps[index] != null; ++index)
          {
            Skill skill = skills[index];
            this.m_SkillGumps[index].OnSkillChange((double) skill.Real, skill.Lock);
          }
        }
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
        this.m_Slider.X = this.m_Width - 15;
        this.m_SliderBorder.X = this.m_Width - 16;
        this.m_Hotspot.X = this.m_Width - 16;
        for (int index = 0; index < this.m_SkillGumps.Length && this.m_SkillGumps[index] != null; ++index)
          this.m_SkillGumps[index].Width = this.m_Width - 20;
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
        double num = this.m_Slider.GetValue();
        this.m_Slider.Height = this.m_Height - 11;
        this.m_Slider.SetValue(num, true);
        this.m_SliderBorder.Height = this.m_Height;
        this.m_Hotspot.Height = this.m_Height;
      }
    }

    public GSkillList(GSkills owner)
      : base(4, 4, 250, 50)
    {
      this.m_Owner = owner;
      this.m_CanDrag = false;
      Skills skills = Engine.Skills;
      this.m_SkillGumps = new GSkillGump[256];
      int num = 4;
      for (int index1 = 0; index1 < skills.Groups.Length; ++index1)
      {
        SkillGroup skillGroup = skills.Groups[index1];
        GLabel glabel = new GLabel(skillGroup.Name, (IFont) Engine.GetUniFont(1), Hues.Bright, 4, num);
        glabel.X -= glabel.Image.xMin;
        glabel.Y -= glabel.Image.yMin;
        glabel.SetTag("yBase", (object) glabel.Y);
        this.m_Children.Add((Gump) glabel);
        num += 4 + (glabel.Image.yMax - glabel.Image.yMin);
        for (int index2 = 0; index2 < skillGroup.Skills.Count; ++index2)
        {
          Skill skill = (Skill) skillGroup.Skills[index2];
          GSkillGump gskillGump = new GSkillGump(skill, num, this.m_Width - 20, this.m_Owner.ShowReal);
          this.m_SkillGumps[skill.ID] = gskillGump;
          this.m_Children.Add((Gump) gskillGump);
          num += 4 + gskillGump.Height;
        }
      }
      this.m_SliderBorder = new GSingleBorder(0, 0, 16, 100);
      this.m_Children.Add((Gump) this.m_SliderBorder);
      this.m_Slider = new GAlphaVSlider(0, 6, 16, 100, 0.0, 0.0, (double) (num + 1), 1.0);
      this.m_Slider.SetTag("Max", (object) (num + 1));
      this.m_Slider.OnValueChange += new OnValueChange(this.Slider_OnValueChange);
      this.m_Children.Add((Gump) this.m_Slider);
      this.m_Hotspot = new GHotspot(0, 0, 16, 100, (Gump) this.m_Slider);
      this.m_Children.Add((Gump) this.m_Hotspot);
    }

    private void Slider_OnValueChange(double vNew, double vOld, Gump sender)
    {
      double num1 = (double) (int) vNew / (double) (int) sender.GetTag("Max");
      int num2 = (int) -((double) ((int) sender.GetTag("Max") - this.m_Height) * num1);
      foreach (Gump gump in this.m_Children.ToArray())
      {
        Type type = gump.GetType();
        if (type == GSkillList.tGLabel)
          gump.Y = num2 + (int) gump.GetTag("yBase");
        else if (type == GSkillList.tGSkillGump)
          gump.Y = num2 + ((GSkillGump) gump).yBase;
      }
    }

    public void OnSkillChange(Skill skill, bool showReal)
    {
      GSkillGump gskillGump = this.m_SkillGumps[skill.ID];
      if (gskillGump == null)
        return;
      gskillGump.OnSkillChange(showReal ? (double) skill.Real : (double) skill.Value, skill.Lock);
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_xLast != x || this.m_yLast != y || (this.m_xWidthLast != this.m_Width || this.m_yHeightLast != this.m_Height))
      {
        Clipper clipper = new Clipper(x + 1, y + 1, this.m_Width - 17, this.m_Height - 2);
        foreach (Gump gump in this.m_Children.ToArray())
        {
          Type type = gump.GetType();
          if (type == GSkillList.tGLabel)
            ((GLabel) gump).Scissor(clipper);
          else if (type == GSkillList.tGSkillGump)
            ((GSkillGump) gump).Scissor(clipper);
        }
        this.m_xLast = x;
        this.m_yLast = y;
        this.m_xWidthLast = this.m_Width;
        this.m_yHeightLast = this.m_Height;
      }
      base.Draw(x, y);
    }
  }
}
