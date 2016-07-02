// Decompiled with JetBrains decompiler
// Type: PlayUO.GLabel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GLabel : Gump, ITranslucent, IClipable
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    protected bool m_Invalidated = true;
    protected string m_Text = "";
    protected float m_fAlpha = 1f;
    protected bool m_Draw;
    protected Texture m_Image;
    protected int m_Width;
    protected int m_Height;
    protected IFont m_Font;
    protected IHue m_Hue;
    protected bool m_bAlpha;
    protected bool m_Clip;
    protected bool m_Relative;
    protected Clipper m_Clipper;
    protected int m_xClipOffset;
    protected int m_yClipOffset;
    protected int m_xClipWidth;
    protected int m_yClipHeight;
    protected VertexCache m_vCache;
    protected bool m_Underline;
    protected int m_SpaceWidth;

    public bool Underline
    {
      get
      {
        return this.m_Underline;
      }
      set
      {
        this.m_Underline = value;
        this.Invalidate();
      }
    }

    public int SpaceWidth
    {
      get
      {
        return this.m_SpaceWidth;
      }
      set
      {
        this.m_SpaceWidth = value;
        this.Invalidate();
      }
    }

    protected VertexCachePool VCPool
    {
      get
      {
        return GLabel.m_vPool;
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
        this.m_Relative = false;
        this.m_Clip = value != null;
        this.m_Clipper = value;
      }
    }

    public Texture Image
    {
      get
      {
        if (this.m_Invalidated)
          this.Refresh();
        return this.m_Image;
      }
    }

    public virtual float Alpha
    {
      get
      {
        return this.m_fAlpha;
      }
      set
      {
        this.m_fAlpha = value;
        this.m_bAlpha = (double) this.m_fAlpha != 1.0;
      }
    }

    public override int Width
    {
      get
      {
        if (this.m_Invalidated)
          this.Refresh();
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        if (this.m_Invalidated)
          this.Refresh();
        return this.m_Height;
      }
    }

    public virtual string Text
    {
      get
      {
        return this.m_Text;
      }
      set
      {
        if (!(this.m_Text != value))
          return;
        this.Invalidate();
        this.m_Text = value;
      }
    }

    public virtual IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if (this.m_Hue == value)
          return;
        this.Invalidate();
        this.m_Hue = value;
      }
    }

    public virtual IFont Font
    {
      get
      {
        return this.m_Font;
      }
      set
      {
        if (this.m_Font == value)
          return;
        this.Invalidate();
        this.m_Font = value;
      }
    }

    protected GLabel(int X, int Y)
      : base(X, Y)
    {
    }

    public GLabel(string Text, IFont Font, IHue Hue, int X, int Y)
      : base(X, Y)
    {
      this.m_Text = Text;
      this.m_Font = Font;
      this.m_Hue = Hue;
      this.m_ITranslucent = true;
      this.Refresh();
    }

    public void Scissor(Clipper Clipper)
    {
      this.m_Relative = false;
      this.m_Clip = Clipper != null;
      this.m_Clipper = Clipper;
    }

    public void Scissor(int xOffset, int yOffset, int xWidth, int yHeight)
    {
      this.m_Relative = true;
      this.m_Clip = true;
      this.m_Clipper = (Clipper) null;
      this.m_xClipOffset = xOffset;
      this.m_yClipOffset = yOffset;
      this.m_xClipWidth = xWidth;
      this.m_yClipHeight = yHeight;
    }

    public override void Center()
    {
      if (this.m_Parent == null)
      {
        base.Center();
      }
      else
      {
        if (this.m_Invalidated)
          this.Refresh();
        this.m_X = (this.m_Parent.Width - (this.m_Image.xMax - this.m_Image.xMin)) / 2 - this.m_Image.xMin;
        this.m_Y = (this.m_Parent.Height - (this.m_Image.yMax - this.m_Image.yMin)) / 2 - this.m_Image.yMin;
      }
    }

    public virtual void Invalidate()
    {
      this.m_Invalidated = true;
    }

    public virtual void Refresh()
    {
      if (!this.m_Invalidated)
        return;
      UnicodeFont unicodeFont = this.m_Font as UnicodeFont;
      bool flag = unicodeFont != null && unicodeFont.Underline;
      int num = unicodeFont == null ? 0 : unicodeFont.SpaceWidth;
      if (unicodeFont != null)
        unicodeFont.Underline = this.m_Underline;
      if (unicodeFont != null && this.m_SpaceWidth > 0)
        unicodeFont.SpaceWidth = this.m_SpaceWidth;
      this.m_Image = this.m_Font.GetString(this.m_Text, this.m_Hue);
      if (this.m_Image != null && !this.m_Image.IsEmpty())
      {
        this.m_Width = this.m_Image.Width;
        this.m_Height = this.m_Image.Height;
        this.m_Draw = true;
      }
      else
        this.m_Draw = false;
      this.m_Invalidated = false;
      if (this.m_vCache != null)
        this.m_vCache.Invalidate();
      if (unicodeFont != null)
        unicodeFont.Underline = flag;
      if (unicodeFont == null)
        return;
      unicodeFont.SpaceWidth = num;
    }

    protected internal override void Draw(int X, int Y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (!this.m_Draw)
        return;
      Renderer.PushAlpha(this.m_fAlpha);
      if (!this.m_Clip)
      {
        if (this.m_vCache == null)
          this.m_vCache = this.VCPool.GetInstance();
        this.m_vCache.Draw(this.m_Image, X, Y);
      }
      else if (!this.m_Relative)
        this.m_Image.DrawClipped(X, Y, this.m_Clipper);
      else
        this.m_Image.DrawClipped(X, Y, Clipper.TemporaryInstance(X + this.m_xClipOffset, Y + this.m_yClipOffset, this.m_xClipWidth, this.m_yClipHeight));
      Renderer.PopAlpha();
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }
  }
}
