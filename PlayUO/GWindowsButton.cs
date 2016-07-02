// Decompiled with JetBrains decompiler
// Type: PlayUO.GWindowsButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GWindowsButton : Gump, IClickable
  {
    protected int m_ImageColor = -1;
    protected WindowsButtonStyle m_Style;
    protected OnClick m_OnClick;
    protected bool m_Enabled;
    protected bool m_Invalidated;
    protected GLabel m_CaptionLabel;
    protected Texture m_Image;
    protected int m_Width;
    protected int m_Height;
    protected bool m_CanEnter;
    protected int m_State;
    protected VertexCache m_vCache;
    private bool m_CaptionDown;

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

    public int ImageColor
    {
      get
      {
        return this.m_ImageColor;
      }
      set
      {
        this.m_ImageColor = value;
        if (this.m_vCache == null)
          return;
        this.m_vCache.Invalidate();
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
      set
      {
        this.m_Image = value;
      }
    }

    public string Text
    {
      get
      {
        return this.m_CaptionLabel.Text;
      }
      set
      {
        this.CaptionDown = false;
        this.m_CaptionLabel.Text = value;
        this.m_CaptionLabel.Center();
      }
    }

    public WindowsButtonStyle Style
    {
      get
      {
        return this.m_Style;
      }
      set
      {
        this.m_Style = value;
      }
    }

    public bool CanEnter
    {
      get
      {
        return this.m_CanEnter;
      }
      set
      {
        this.m_CanEnter = value;
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
        this.m_State = value;
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

    public bool Enabled
    {
      get
      {
        return this.m_Enabled;
      }
      set
      {
        if (this.m_Enabled == value)
          return;
        this.m_Enabled = value;
        if (this.m_Enabled)
          return;
        this.State = 0;
      }
    }

    public bool CaptionDown
    {
      get
      {
        return this.m_CaptionDown;
      }
      set
      {
        if (this.m_CaptionDown == value)
          return;
        this.m_CaptionDown = value;
        int num = this.m_CaptionDown ? 1 : -1;
        this.m_CaptionLabel.X += num;
        this.m_CaptionLabel.Y += num;
      }
    }

    public event EventHandler Clicked;

    public GWindowsButton(string text, int x, int y)
      : this(text, x, y, 100, 20)
    {
    }

    public GWindowsButton(string text, int x, int y, int width, int height)
      : base(x, y)
    {
      this.m_Width = width;
      this.m_Height = height;
      this.m_Enabled = true;
      this.m_CaptionLabel = new GLabel(text, (IFont) Engine.GetUniFont(2), GumpHues.ControlText, 5, 5);
      this.m_Children.Add((Gump) this.m_CaptionLabel);
      this.m_CaptionLabel.Center();
    }

    protected internal override bool OnKeyDown(char c)
    {
      if (!this.m_CanEnter || (int) c != 13)
        return false;
      this.Click();
      return true;
    }

    protected internal override void Draw(int x, int y)
    {
      Renderer.SetTexture((Texture) null);
      int num = 0;
      switch (this.m_Style)
      {
        case WindowsButtonStyle.Normal:
          switch (this.m_State)
          {
            case 0:
              GumpPaint.DrawRaised3D(x, y, this.m_Width, this.m_Height);
              this.CaptionDown = false;
              break;
            case 1:
              GumpPaint.DrawRaised3D(x, y, this.m_Width, this.m_Height);
              this.CaptionDown = false;
              break;
            case 2:
              GumpPaint.DrawFlat(x, y, this.m_Width, this.m_Height, GumpColors.ControlDark, GumpColors.Control);
              this.CaptionDown = true;
              num = 1;
              break;
          }
        case WindowsButtonStyle.Flat:
          switch (this.m_State)
          {
            case 0:
              GumpPaint.DrawFlat(x, y, this.m_Width, this.m_Height, GumpColors.ControlDarkDark, GumpColors.Control);
              this.CaptionDown = false;
              break;
            case 1:
              GumpPaint.DrawFlat(x, y, this.m_Width, this.m_Height, GumpColors.ControlDarkDark, GumpColors.ControlAlternate);
              this.CaptionDown = false;
              break;
            case 2:
              GumpPaint.DrawFlat(x, y, this.m_Width, this.m_Height, GumpColors.ControlDarkDark, GumpPaint.Blend(GumpColors.ControlAlternate, GumpColors.ControlLightLight, 128));
              this.CaptionDown = false;
              break;
          }
      }
      if (this.m_Image == null)
        return;
      if (this.m_vCache == null)
        this.m_vCache = new VertexCache();
      if (this.m_ImageColor == -1)
        this.m_vCache.Draw(this.m_Image, num + x + (this.m_Width - (this.m_Image.xMax - this.m_Image.xMin + 1)) / 2 - this.m_Image.xMin, num + y + (this.m_Height - (this.m_Image.yMax - this.m_Image.yMin + 1)) / 2 - this.m_Image.yMin);
      else
        this.m_vCache.Draw(this.m_Image, num + x + (this.m_Width - (this.m_Image.xMax - this.m_Image.xMin + 1)) / 2 - this.m_Image.xMin, num + y + (this.m_Height - (this.m_Image.yMax - this.m_Image.yMin + 1)) / 2 - this.m_Image.yMin, this.m_ImageColor);
    }

    protected internal override bool HitTest(int x, int y)
    {
      return true;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (!this.m_Enabled)
        return;
      this.State = (mb & MouseButtons.Left) != MouseButtons.None ? 2 : 1;
    }

    protected internal override void OnMouseLeave()
    {
      if (!this.m_Enabled)
        return;
      this.State = 0;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.OnMouseEnter(x, y, mb);
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if (!this.m_Enabled || (mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.State = 1;
      this.InternalOnClicked();
    }

    public void Click()
    {
      for (int index = 1; index <= 3; ++index)
      {
        this.State = index % 3;
        Engine.DrawNow();
      }
      this.InternalOnClicked();
    }

    private void InternalOnClicked()
    {
      this.OnClicked();
      if (this.Clicked != null)
        this.Clicked((object) this, EventArgs.Empty);
      if (this.m_OnClick == null)
        return;
      this.m_OnClick((Gump) this);
    }

    protected virtual void OnClicked()
    {
    }
  }
}
