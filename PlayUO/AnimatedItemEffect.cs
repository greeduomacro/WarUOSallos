// Decompiled with JetBrains decompiler
// Type: PlayUO.AnimatedItemEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class AnimatedItemEffect : Effect
  {
    protected int m_Start;
    protected IHue m_Hue;
    protected int m_ItemID;
    protected bool m_Animated;
    protected sbyte[] m_Animation;
    protected int m_FrameCount;
    protected int m_Delay;
    protected int m_Duration;
    public int m_RenderMode;
    protected VertexCache m_vCache;

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

    public AnimatedItemEffect(int ItemID, IHue Hue, int duration)
    {
      if (ItemID == 14013 && duration == 10)
        duration = 14;
      this.m_Children = new EffectList();
      this.m_Duration = duration;
      this.m_Hue = Hue;
      this.m_ItemID = ItemID;
      this.m_Animated = true;
      AnimData anim = Map.GetAnim(ItemID);
      this.m_FrameCount = (int) anim.frameCount;
      this.m_Delay = (int) anim.frameInterval;
      this.m_Animation = new sbyte[64];
      for (int index = 0; index < 64; ++index)
        this.m_Animation[index] = anim[index];
      if (this.m_FrameCount == 0)
      {
        this.m_FrameCount = 1;
        this.m_Animation[0] = (sbyte) 0;
      }
      if (this.m_Delay != 0)
        return;
      this.m_Delay = 1;
    }

    public AnimatedItemEffect(int Source, int ItemID, IHue Hue, int duration)
      : this(Source, 0, 0, 0, ItemID, Hue, duration)
    {
    }

    public AnimatedItemEffect(int Source, int xSource, int ySource, int zSource, int ItemID, IHue Hue, int duration)
      : this(ItemID, Hue, duration)
    {
      Mobile mobile = World.FindMobile(Source);
      if (mobile != null)
      {
        this.SetSource(mobile);
        if (mobile.Player || mobile.IsMoving || xSource == 0 && ySource == 0 && zSource == 0)
          return;
        mobile.SetLocation(xSource, ySource, zSource);
        mobile.Update();
        mobile.UpdateReal();
      }
      else
      {
        Item Source1 = World.FindItem(Source);
        if (Source1 != null)
        {
          this.SetSource(Source1);
          if (xSource == 0 && ySource == 0 && zSource == 0)
            return;
          Source1.SetLocation(xSource, ySource, zSource);
          Source1.Update();
        }
        else
          this.SetSource(xSource, ySource, zSource);
      }
    }

    public AnimatedItemEffect(int xSource, int ySource, int zSource, int ItemID, IHue Hue, int duration)
      : this(ItemID, Hue, duration)
    {
      this.SetSource(xSource, ySource, zSource);
    }

    public AnimatedItemEffect(Mobile Source, int ItemID, IHue Hue, int duration)
      : this(ItemID, Hue, duration)
    {
      this.SetSource(Source);
    }

    public AnimatedItemEffect(Item Source, int ItemID, IHue Hue, int duration)
      : this(ItemID, Hue, duration)
    {
      this.SetSource(Source);
    }

    public override void RenderLight()
    {
      if (Renderer.m_Frames - this.m_Start >= this.m_Duration)
        return;
      int xPixel;
      int yPixel;
      this.GetRenderLocation(out xPixel, out yPixel);
      int x = xPixel - Engine.GameX;
      int num1 = yPixel - Engine.GameY;
      int num2 = this.m_ItemID;
      if (this.m_Animated)
        num2 += (int) this.m_Animation[(Renderer.m_Frames - this.m_Start) / this.m_Delay % this.m_FrameCount];
      if (this.m_ItemID != 14000 && this.m_ItemID != 14013)
        return;
      int num3 = num2 - this.m_ItemID;
      if (num3 < 0 || num3 >= 12)
        return;
      int y = num1 - 65;
      float alpha = (float) (Renderer.m_Frames - this.m_Start) / (float) this.m_Duration * 2f;
      if ((double) alpha > 1.0)
        alpha = 2f - alpha;
      Renderer.RenderLight(this.m_Start, x, y, num2 & 16383, num3 < 3 ? 1 : 29, 0, alpha);
    }

    public override void OnStart()
    {
      this.m_Start = Renderer.m_Frames;
    }

    public override void OnStop()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
    }

    private void GetRenderLocation(out int xPixel, out int yPixel)
    {
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
      int num5 = Y - num2;
      int num6 = Z - num3;
      xPixel = (Engine.GameWidth >> 1) + (num4 - num5) * 22;
      yPixel = (Engine.GameHeight >> 1) + 22 + (num4 + num5) * 22 - num6 * 4;
      xPixel += Engine.GameX;
      yPixel += Engine.GameY;
      if (this.m_Source != null && this.m_Source.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Source;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          xPixel += xOffset;
          yPixel += yOffset;
        }
      }
      xPixel -= Renderer.m_xScroll;
      yPixel -= Renderer.m_yScroll;
    }

    public override bool Slice()
    {
      if (Renderer.m_Frames - this.m_Start >= this.m_Duration)
        return false;
      int xPixel;
      int yPixel;
      this.GetRenderLocation(out xPixel, out yPixel);
      IHue hue = Renderer.m_Dead ? Hues.Grayscale : this.m_Hue;
      int num1 = this.m_ItemID;
      if (this.m_Animated)
        num1 += (int) this.m_Animation[(Renderer.m_Frames - this.m_Start) / this.m_Delay % this.m_FrameCount];
      Texture tex = hue.GetItem(num1);
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      int num2 = (Renderer.m_Frames - this.m_Start) / this.m_Duration;
      RenderEffect renderEffect = Effects.GetItemEffect(num1);
      if (renderEffect != null && renderEffect.Glow != null)
      {
        Texture texture = Hues.Shadow.GetItem(num1);
        if (texture != null && !texture.IsEmpty())
        {
          Renderer.PushAlpha(renderEffect.Glow.Alpha);
          Renderer.SetBlendType(DrawBlendType.Additive);
          int color1 = hue.Pixel32(renderEffect.Glow.Color ?? texture._averageColor);
          texture.DrawGame(xPixel - tex.Width / 2 - 8, yPixel - tex.Height - 8, color1);
          int? color2 = renderEffect.Glow.Color;
          if ((color2.GetValueOrDefault() != 9203350 ? 0 : (color2.HasValue ? 1 : 0)) != 0)
          {
            Renderer.SetAlpha(renderEffect.Glow.Alpha * 0.5f);
            texture.DrawGame(xPixel - tex.Width / 2 - 8, yPixel - tex.Height - 8, color1);
          }
          Renderer.SetBlendType(DrawBlendType.Normal);
          Renderer.PopAlpha();
        }
      }
      switch (this.m_RenderMode)
      {
        case 2:
        case 3:
          renderEffect = RenderEffects.Additive;
          break;
        case 4:
          renderEffect = RenderEffects.HalfAdditive;
          break;
      }
      if (renderEffect == null)
        renderEffect = RenderEffects.Default;
      Renderer.PushAlpha(renderEffect.Alpha);
      Renderer.SetBlendType(renderEffect.BlendType);
      this.m_vCache.DrawGame(tex, xPixel - tex.Width / 2, yPixel - tex.Height);
      Renderer.SetBlendType(DrawBlendType.Normal);
      Renderer.PopAlpha();
      return true;
    }
  }
}
