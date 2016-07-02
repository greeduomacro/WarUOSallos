// Decompiled with JetBrains decompiler
// Type: PlayUO.GMenuItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GMenuItem : GAlphaBackground
  {
    private string m_Text;
    private Color m_DefaultColor;
    private Color m_OverColor;
    private Color m_ExpandedColor;
    private bool m_DropDown;
    private bool m_MakeTopmost;

    public bool MakeTopmost
    {
      get
      {
        return this.m_MakeTopmost;
      }
      set
      {
        this.m_MakeTopmost = value;
      }
    }

    public bool DropDown
    {
      get
      {
        return this.m_DropDown;
      }
      set
      {
        this.m_DropDown = value;
        this.Layout();
      }
    }

    public Color DefaultColor
    {
      get
      {
        return this.m_DefaultColor;
      }
      set
      {
        this.m_DefaultColor = value;
      }
    }

    public Color OverColor
    {
      get
      {
        return this.m_OverColor;
      }
      set
      {
        this.m_OverColor = value;
      }
    }

    public Color ExpandedColor
    {
      get
      {
        return this.m_ExpandedColor;
      }
      set
      {
        this.m_ExpandedColor = value;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
      set
      {
        if (this.m_Text == value)
          return;
        this.m_Text = value;
        GLabel glabel = (GLabel) null;
        if (this.m_Children.Count > 0)
          glabel = this.m_Children[0] as GLabel;
        if (glabel == null)
          return;
        glabel.Text = this.m_Text;
        glabel.Center();
        glabel.X = 4 - glabel.Image.xMin;
      }
    }

    public GMenuItem(string text)
      : base(0, 50, 120, 24)
    {
      this.m_Text = text;
      this.m_DefaultColor = Color.FromArgb(192, 192, 192);
      this.m_OverColor = Color.FromArgb(32, 64, 128);
      this.m_ExpandedColor = Color.FromArgb(32, 64, 128);
      this.FillAlpha = 0.25f;
      this.m_CanDrag = false;
      GLabel glabel = new GLabel(text, Engine.DefaultFont, Hues.Load(1153), 0, 0);
      this.m_Children.Add((Gump) glabel);
      glabel.Center();
      glabel.X = 4 - glabel.Image.xMin;
      this.m_NonRestrictivePicking = true;
    }

    public void SetHue(IHue hue)
    {
      GLabel glabel = (GLabel) null;
      if (this.m_Children.Count > 0)
        glabel = this.m_Children[0] as GLabel;
      if (glabel == null)
        return;
      glabel.Hue = hue;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Left)
        return;
      this.OnClick();
      if (!(this.GetType() != typeof (GMenuItem)))
        return;
      this.Unexpand();
    }

    public void Unexpand()
    {
      this.FillColor = this.m_DefaultColor.ToArgb() & 16777215;
      if (this.Width != 120)
        this.Width = 120;
      Gump[] array = this.m_Children.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] is GMenuItem)
          array[index].Visible = false;
      }
      if (!(this.m_Parent is GMenuItem))
        return;
      ((GMenuItem) this.m_Parent).Unexpand();
    }

    protected internal override void Render(int rx, int ry)
    {
      GMenuItem gmenuItem1 = Gumps.LastOver as GMenuItem;
      int num1;
      bool flag1;
      if (gmenuItem1 == null)
      {
        num1 = this.m_DefaultColor.ToArgb() & 16777215;
        flag1 = true;
      }
      else
      {
        GMenuItem gmenuItem2 = gmenuItem1;
        while (gmenuItem2 != null && gmenuItem2 != this)
          gmenuItem2 = gmenuItem2.Parent as GMenuItem;
        flag1 = gmenuItem2 != this;
        num1 = !flag1 ? (gmenuItem1 != this ? this.m_ExpandedColor.ToArgb() & 16777215 : this.m_OverColor.ToArgb() & 16777215) : this.m_DefaultColor.ToArgb() & 16777215;
      }
      this.FillColor = num1;
      if (flag1)
      {
        if (this.Width != 120)
          this.Width = 120;
        Gump[] array = this.m_Children.ToArray();
        for (int index = 0; index < array.Length; ++index)
        {
          if (array[index] is GMenuItem)
            array[index].Visible = false;
        }
      }
      else
      {
        bool flag2 = false;
        Gump[] array = this.m_Children.ToArray();
        for (int index = 0; index < array.Length; ++index)
        {
          if (array[index] is GMenuItem)
          {
            array[index].Visible = true;
            flag2 = true;
          }
        }
        int num2 = !flag2 || this.m_DropDown ? 120 : 125;
        if (this.Width != num2)
          this.Width = num2;
        if (flag2 && this.m_MakeTopmost)
          this.BringToTop();
        this.Layout();
      }
      base.Render(rx, ry);
    }

    public bool Contains(GMenuItem child)
    {
      return this.m_Children.IndexOf((Gump) child) >= 0;
    }

    public void Add(GMenuItem child)
    {
      if (child == this || this.Contains(child))
        return;
      this.m_Children.Add((Gump) child);
      child.Visible = false;
      this.Layout();
    }

    public void Remove(GMenuItem child)
    {
      if (child == this || !this.Contains(child))
        return;
      this.m_Children.Remove((Gump) child);
      child.Visible = false;
      this.Layout();
    }

    public void Layout()
    {
      int num1 = 0;
      Gump[] array = this.m_Children.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] is GMenuItem)
          ++num1;
      }
      int num2;
      int num3;
      if (this.m_DropDown)
      {
        num2 = 0;
        num3 = this.Height - 1;
      }
      else
      {
        Gump desktop = Gumps.Desktop;
        num2 = 124;
        num3 = 0;
        if (desktop != null)
        {
          Point screen = this.PointToScreen(new Point(0, 0));
          int y = desktop.PointToClient(screen).Y;
          int num4 = (desktop.Height - y - 1) / 23;
          if (num4 < 1)
            num4 = 1;
          if (num4 < num1)
            num3 = this.Height - (num1 - num4 + 1) * 23 - 1;
          if (y + num3 < 0)
            num3 = -y;
        }
      }
      for (int index = 0; index < array.Length; ++index)
      {
        GMenuItem gmenuItem = array[index] as GMenuItem;
        if (gmenuItem != null)
        {
          if (gmenuItem.X != num2)
            gmenuItem.X = num2;
          if (gmenuItem.Y != num3)
            gmenuItem.Y = num3;
          num3 += 23;
        }
      }
    }

    public virtual void OnClick()
    {
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return !Engine.amMoving;
    }
  }
}
