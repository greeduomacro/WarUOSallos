// Decompiled with JetBrains decompiler
// Type: PlayUO.GImage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GImage : Gump, ITranslucent, IClipable
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    protected float m_fAlpha = 1f;
    protected bool m_Draw;
    protected bool m_Invalidated;
    protected Texture m_Image;
    protected IHue m_Hue;
    protected int m_GumpID;
    protected int m_Width;
    protected int m_Height;
    private VertexCache m_vCache;
    protected Clipper m_Clipper;

    protected VertexCachePool VCPool
    {
      get
      {
        return GImage.m_vPool;
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

    public int GumpID
    {
      get
      {
        return this.m_GumpID;
      }
      set
      {
        if (this.m_GumpID == value)
          return;
        this.m_GumpID = value;
        this.Invalidate();
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if (this.m_Hue == value)
          return;
        this.m_Hue = value;
        this.Invalidate();
      }
    }

    public float Alpha
    {
      get
      {
        return this.m_fAlpha;
      }
      set
      {
        this.m_fAlpha = value;
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

    public GImage(int x, int y)
      : this(0, x, y)
    {
    }

    public GImage(int gumpID, int x, int y)
      : this(gumpID, Hues.Default, x, y)
    {
    }

    public GImage(int gumpID, IHue hue, int x, int y)
      : base(x, y)
    {
      this.m_GumpID = gumpID;
      this.m_Hue = hue;
      this.Invalidate();
      this.m_ITranslucent = true;
    }

    protected void Invalidate()
    {
      this.m_Invalidated = true;
    }

    protected void Refresh()
    {
      if (!this.m_Invalidated)
        return;
      if (this.m_vCache != null)
        this.m_vCache.Invalidate();
      this.m_Image = this.m_Hue.GetGump(this.m_GumpID);
      if (this.m_Image != null && !this.m_Image.IsEmpty())
      {
        this.m_Width = this.m_Image.Width;
        this.m_Height = this.m_Image.Height;
        this.m_Draw = true;
      }
      else
      {
        this.m_Width = this.m_Height = 0;
        this.m_Draw = false;
      }
      this.m_Invalidated = false;
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (!this.m_Draw)
        return;
      Renderer.PushAlpha(this.m_fAlpha);
      if (this.m_Clipper == null)
      {
        if (this.m_vCache == null)
          this.m_vCache = this.VCPool.GetInstance();
        this.m_vCache.Draw(this.m_Image, x, y);
      }
      else
        this.m_Image.DrawClipped(x, y, this.m_Clipper);
      Renderer.PopAlpha();
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }
  }
}
