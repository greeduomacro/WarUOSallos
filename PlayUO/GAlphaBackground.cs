// Decompiled with JetBrains decompiler
// Type: PlayUO.GAlphaBackground
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;

namespace PlayUO
{
  public class GAlphaBackground : Gump
  {
    protected float m_FillAlpha = 0.4f;
    protected bool m_ShouldHitTest = true;
    protected bool m_DrawBorder = true;
    protected int m_Width;
    protected int m_Height;
    protected int m_FillColor;
    protected int m_RightColor;
    protected int m_BorderColor;
    protected OnDispose m_OnDispose;
    protected Clipper m_Clipper;

    public bool DrawBorder
    {
      get
      {
        return this.m_DrawBorder;
      }
      set
      {
        this.m_DrawBorder = value;
      }
    }

    public bool ShouldHitTest
    {
      get
      {
        return this.m_ShouldHitTest;
      }
      set
      {
        this.m_ShouldHitTest = value;
      }
    }

    public int FillColor
    {
      get
      {
        return this.m_FillColor;
      }
      set
      {
        this.m_FillColor = value;
        this.m_RightColor = value;
      }
    }

    public int RightColor
    {
      get
      {
        return this.m_RightColor;
      }
      set
      {
        this.m_RightColor = value;
      }
    }

    public int BorderColor
    {
      get
      {
        return this.m_BorderColor;
      }
      set
      {
        this.m_BorderColor = value;
      }
    }

    public float FillAlpha
    {
      get
      {
        return this.m_FillAlpha;
      }
      set
      {
        this.m_FillAlpha = value;
      }
    }

    public OnDispose Disposer
    {
      get
      {
        return this.m_OnDispose;
      }
      set
      {
        this.m_OnDispose = value;
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

    public Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
      }
    }

    public GAlphaBackground(int X, int Y, int Width, int Height)
      : base(X, Y)
    {
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_Width = Width;
      this.m_Height = Height;
    }

    protected internal override void OnDispose()
    {
      if (this.m_OnDispose == null)
        return;
      this.m_OnDispose((Gump) this);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (!this.m_ShouldHitTest || Engine.amMoving)
        return false;
      return !TargetManager.IsActive;
    }

    protected internal override void Draw(int X, int Y)
    {
      ClipType clipType = ClipType.Inside;
      if (this.m_Clipper != null)
        clipType = this.m_Clipper.Evaluate(X, Y, this.m_Width, this.m_Height);
      Renderer.SetTexture((Texture) null);
      if (clipType == ClipType.Inside)
      {
        Renderer.PushAlpha(this.m_FillAlpha);
        if (this.m_FillColor == this.m_RightColor)
          Renderer.SolidRect(this.m_FillColor, X + 1, Y + 1, this.m_Width - 2, this.m_Height - 2);
        else
          Renderer.GradientRectLR(this.m_FillColor, this.m_RightColor, X + 1, Y + 1, this.m_Width - 2, this.m_Height - 2);
        Renderer.PopAlpha();
        if (!this.m_DrawBorder)
          return;
        Renderer.TransparentRect(this.m_BorderColor, X, Y, this.m_Width, this.m_Height);
      }
      else
      {
        if (clipType != ClipType.Partial)
          return;
        Renderer.PushAlpha(this.m_FillAlpha);
        Renderer.SolidRect(this.m_FillColor, X + 1, Y + 1, this.m_Width - 2, this.m_Height - 2, this.m_Clipper);
        Renderer.PopAlpha();
        if (!this.m_DrawBorder)
          return;
        Renderer.TransparentRect(this.m_BorderColor, X, Y, this.m_Width, this.m_Height, this.m_Clipper);
      }
    }
  }
}
