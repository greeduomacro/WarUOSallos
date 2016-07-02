// Decompiled with JetBrains decompiler
// Type: PlayUO.GSystemButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GSystemButton : GAlphaBackground
  {
    private Color m_InactiveColor;
    private Color m_ActiveColor;
    private Color m_PressedColor;
    private Color m_ForeColor;
    private GSystemButton.ButtonState m_State;
    private OnClick m_OnClick;

    public string Text
    {
      get
      {
        GLabel glabel = (GLabel) null;
        if (this.m_Children.Count > 0)
          glabel = this.m_Children[0] as GLabel;
        if (glabel != null)
          return glabel.Text;
        return "";
      }
      set
      {
        GLabel glabel = (GLabel) null;
        if (this.m_Children.Count > 0)
          glabel = this.m_Children[0] as GLabel;
        if (glabel == null)
          return;
        glabel.Text = value;
      }
    }

    public OnClick OnClick
    {
      get
      {
        return this.m_OnClick;
      }
      set
      {
        this.m_OnClick = value;
      }
    }

    public Color InactiveColor
    {
      get
      {
        return this.m_InactiveColor;
      }
      set
      {
        this.m_InactiveColor = value;
        this.UpdateColors();
      }
    }

    public Color ActiveColor
    {
      get
      {
        return this.m_ActiveColor;
      }
      set
      {
        this.m_ActiveColor = value;
        this.UpdateColors();
      }
    }

    public Color PressedColor
    {
      get
      {
        return this.m_PressedColor;
      }
      set
      {
        this.m_PressedColor = value;
        this.UpdateColors();
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.m_ForeColor;
      }
      set
      {
        this.m_ForeColor = value;
        this.UpdateColors();
      }
    }

    public virtual float Darkness
    {
      get
      {
        return 0.5f;
      }
    }

    public GSystemButton(int x, int y, int width, int height, Color backColor, Color foreColor, string text, IFont font)
      : base(x, y, width, height)
    {
      this.m_InactiveColor = backColor;
      this.m_ActiveColor = ControlPaint.Dark(backColor, -this.Darkness);
      this.m_PressedColor = ControlPaint.Light(backColor);
      this.m_ForeColor = foreColor;
      this.m_CanDrag = false;
      GLabel glabel = new GLabel(text, font, Hues.Default, 0, 0);
      this.m_Children.Add((Gump) glabel);
      glabel.Center();
      this.FillAlpha = 1f;
      this.UpdateColors();
    }

    public void UpdateColors()
    {
      switch (this.m_State)
      {
        case GSystemButton.ButtonState.Inactive:
          this.FillColor = this.m_InactiveColor.ToArgb() & 16777215;
          break;
        case GSystemButton.ButtonState.Active:
          this.FillColor = this.m_ActiveColor.ToArgb() & 16777215;
          break;
        case GSystemButton.ButtonState.Pressed:
          this.FillColor = this.m_PressedColor.ToArgb() & 16777215;
          break;
      }
      GLabel glabel = (GLabel) null;
      if (this.m_Children.Count > 0)
        glabel = this.m_Children[0] as GLabel;
      if (glabel == null)
        return;
      glabel.Hue = (IHue) new Hues.ColorFillHue(this.m_ForeColor.ToArgb() & 16777215);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      this.m_State = mb != MouseButtons.Left ? GSystemButton.ButtonState.Active : GSystemButton.ButtonState.Pressed;
      this.UpdateColors();
    }

    protected internal override void OnMouseLeave()
    {
      this.m_State = GSystemButton.ButtonState.Inactive;
      this.UpdateColors();
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.m_State = mb != MouseButtons.Left ? GSystemButton.ButtonState.Active : GSystemButton.ButtonState.Pressed;
      this.UpdateColors();
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.Left && this.m_OnClick != null)
        this.m_OnClick((Gump) this);
      this.m_State = GSystemButton.ButtonState.Active;
      this.UpdateColors();
    }

    public void SetBackColor(Color backColor)
    {
      this.m_InactiveColor = backColor;
      this.m_ActiveColor = ControlPaint.Dark(backColor, -this.Darkness);
      this.m_PressedColor = ControlPaint.Light(backColor);
      this.UpdateColors();
    }

    private enum ButtonState
    {
      Inactive,
      Active,
      Pressed,
    }
  }
}
