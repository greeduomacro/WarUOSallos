// Decompiled with JetBrains decompiler
// Type: PlayUO.GTextButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GTextButton : GLabel
  {
    protected bool m_CanHitTest = true;
    private IHue[] m_Hues;
    private int m_State;
    private OnClick m_OnClick;
    private OnHighlight m_OnHighlight;

    public bool CanHitTest
    {
      get
      {
        return this.m_CanHitTest;
      }
      set
      {
        this.m_CanHitTest = value;
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

    public OnHighlight OnHighlight
    {
      get
      {
        return this.m_OnHighlight;
      }
      set
      {
        this.m_OnHighlight = value;
      }
    }

    public IHue DefaultHue
    {
      get
      {
        return this.m_Hues[0];
      }
      set
      {
        if (this.m_Hues[0] == value)
          return;
        this.m_Hues[0] = value;
        if (this.m_State != 0)
          return;
        this.m_Hue = value;
        this.Invalidate();
      }
    }

    public IHue FocusHue
    {
      get
      {
        return this.m_Hues[1];
      }
      set
      {
        if (this.m_Hues[1] == value)
          return;
        this.m_Hues[1] = value;
        if (this.m_State != 1)
          return;
        this.m_Hue = value;
        this.Invalidate();
      }
    }

    public int State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        if (this.m_State == value)
          return;
        this.m_State = value;
        if (this.m_State == 1 && this.m_OnHighlight != null)
          this.m_OnHighlight((Gump) this);
        this.m_Hue = this.m_Hues[this.m_State];
        this.Invalidate();
      }
    }

    public GTextButton(string text, IFont font, IHue defaultHue, IHue focusHue, int x, int y, OnClick onClick)
      : base(x, y)
    {
      this.m_Hues = new IHue[2]
      {
        defaultHue,
        focusHue
      };
      this.m_OnClick = onClick;
      this.m_Text = text;
      this.m_Font = font;
      this.m_Hue = defaultHue;
      this.m_ITranslucent = true;
      this.Refresh();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if (this.m_OnClick == null)
        return;
      this.SetTag("Buttons", (object) mb);
      this.m_OnClick((Gump) this);
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      this.State = 1;
    }

    protected internal override void OnMouseLeave()
    {
      this.State = 0;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (!this.m_CanHitTest)
        return false;
      if (!this.m_Clip)
        return true;
      if (this.m_Relative)
        return Clipper.TemporaryInstance(this.m_xClipOffset, this.m_yClipOffset, this.m_xClipWidth, this.m_yClipHeight).Evaluate(x, y);
      if (this.m_Clipper != null)
        return this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)));
      return true;
    }
  }
}
