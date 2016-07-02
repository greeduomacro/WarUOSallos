// Decompiled with JetBrains decompiler
// Type: PlayUO.LightningEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class LightningEffect : Effect
  {
    private static Texture[] _lightningTextures;
    protected int m_Start;
    protected IHue m_Hue;
    protected VertexCache m_vCache;
    protected Texture m_tCache;
    protected TimeSync _sync;

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        this.m_Hue = value;
      }
    }

    public LightningEffect(IHue Hue)
    {
      this.m_vCache = new VertexCache();
      this.m_Children = new EffectList();
      this.m_Hue = Hue;
    }

    public LightningEffect(int Source, int xSource, int ySource, int zSource, IHue Hue)
      : this(Hue)
    {
      Mobile mobile = World.FindMobile(Source);
      if (mobile != null)
      {
        this.SetSource(mobile);
      }
      else
      {
        Item Source1 = World.FindItem(Source);
        if (Source1 != null)
          this.SetSource(Source1);
        else
          this.SetSource(xSource, ySource, zSource);
      }
    }

    public LightningEffect(int xSource, int ySource, int zSource, IHue Hue)
      : this(Hue)
    {
      this.SetSource(xSource, ySource, zSource);
    }

    public LightningEffect(Mobile Source, IHue Hue)
      : this(Hue)
    {
      this.SetSource(Source);
    }

    public LightningEffect(Item Source, IHue Hue)
      : this(Hue)
    {
      this.SetSource(Source);
    }

    private static Texture[] GetTextures()
    {
      if (LightningEffect._lightningTextures == null)
        LightningEffect._lightningTextures = Texture.FromImageSet("play/images/lightning.png", 64, 512, 8, 1);
      return LightningEffect._lightningTextures;
    }

    public override void OnStart()
    {
      this._sync = new TimeSync(0.5);
      this.m_Start = Renderer.m_Frames;
    }

    public override bool Slice()
    {
      Texture[] textures = LightningEffect.GetTextures();
      double normalized = this._sync.Normalized;
      if (normalized >= 1.0)
        return false;
      int num1 = Renderer.m_xWorld;
      int num2 = Renderer.m_yWorld;
      int num3 = Renderer.m_zWorld;
      int X = 0;
      int Y = 0;
      int Z = 0;
      int xOffset = 0;
      int yOffset = 0;
      int fOffset = 0;
      this.GetSource(out X, out Y, out Z);
      int num4 = X - num1;
      Y -= num2;
      int num5 = Z - num3;
      int num6 = (Engine.GameWidth >> 1) + (num4 - Y) * 22;
      int num7 = (Engine.GameHeight >> 1) + (num4 + Y) * 22 - num5 * 4;
      int num8 = num6 + Engine.GameX;
      int num9 = num7 + Engine.GameY;
      int x = num8 - Renderer.m_xScroll;
      int num10 = num9 - Renderer.m_yScroll;
      if (this.m_Source != null && this.m_Source.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Source;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          x += xOffset;
          num10 += yOffset;
        }
      }
      int index = Math.Max(0, Math.Min(textures.Length, (int) (normalized * (double) textures.Length)));
      Texture texture = textures[index];
      if (this.m_tCache != texture)
      {
        this.m_tCache = texture;
        this.m_vCache.Invalidate();
      }
      Renderer.SetBlendType(DrawBlendType.Additive);
      Renderer.FilterEnable = true;
      texture.DrawScaled(x, num10 - 5, 16777215, (float) (texture.Width / 2), (float) (texture.Height - 20), 2f, 2f);
      Renderer.FilterEnable = false;
      Renderer.SetBlendType(DrawBlendType.Normal);
      return true;
    }
  }
}
