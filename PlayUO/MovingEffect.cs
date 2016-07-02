// Decompiled with JetBrains decompiler
// Type: PlayUO.MovingEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Data;

namespace PlayUO
{
  public class MovingEffect : Effect
  {
    protected int _effectId;
    protected int m_Start;
    protected IHue m_Hue;
    protected TimeSync m_Sync;
    protected int m_ItemID;
    protected bool m_Animated;
    protected sbyte[] m_Animation;
    protected int m_FrameCount;
    protected int m_Delay;
    public int m_RenderMode;

    public int EffectId
    {
      get
      {
        return this._effectId;
      }
      set
      {
        this._effectId = value;
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
        this.m_Hue = value;
      }
    }

    public MovingEffect(int ItemID, IHue Hue)
    {
      this.m_Children = new EffectList();
      this.m_Hue = Hue;
      this.m_ItemID = ItemID;
      this.m_Animated = Map.m_ItemFlags[ItemID][(TileFlag) 16777216L];
      if (!this.m_Animated)
        return;
      AnimData anim = Map.GetAnim(ItemID);
      this.m_FrameCount = (int) anim.frameCount;
      this.m_Delay = (int) anim.frameInterval;
      this.m_Animation = new sbyte[64];
      for (int index = 0; index < 64; ++index)
        this.m_Animation[index] = anim[index];
      if (this.m_Delay == 0)
        this.m_Delay = 1;
      this.m_Animated = this.m_FrameCount > 0;
    }

    public MovingEffect(int Source, int Target, int xSource, int ySource, int zSource, int xTarget, int yTarget, int zTarget, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      Mobile mobile1 = World.FindMobile(Source);
      if (mobile1 != null)
      {
        this.SetSource(mobile1);
        if (!mobile1.Player && !mobile1.IsMoving && (xSource != 0 || ySource != 0 || zSource != 0))
        {
          mobile1.SetLocation(xSource, ySource, zSource);
          mobile1.Update();
          mobile1.UpdateReal();
        }
      }
      else
      {
        Item Source1 = World.FindItem(Source);
        if (Source1 != null)
        {
          this.SetSource(Source1);
          if (xSource != 0 || ySource != 0 || zSource != 0)
          {
            Source1.SetLocation(xSource, ySource, zSource);
            Source1.Update();
          }
        }
        else
          this.SetSource(xSource, ySource, zSource);
      }
      Mobile mobile2 = World.FindMobile(Target);
      if (mobile2 != null)
      {
        this.SetTarget(mobile2);
        if (mobile2.Player || mobile2.IsMoving || xTarget == 0 && yTarget == 0 && zTarget == 0)
          return;
        mobile2.SetLocation(xTarget, yTarget, zTarget);
        mobile2.Update();
        mobile2.UpdateReal();
      }
      else
      {
        Item Target1 = World.FindItem(Target);
        if (Target1 != null)
        {
          this.SetTarget(Target1);
          if (xTarget == 0 && yTarget == 0 && zTarget == 0)
            return;
          Target1.SetLocation(xTarget, yTarget, zTarget);
          Target1.Update();
        }
        else
          this.SetTarget(xTarget, yTarget, zTarget);
      }
    }

    public MovingEffect(int xSource, int ySource, int zSource, int xTarget, int yTarget, int zTarget, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(xSource, ySource, zSource);
      this.SetTarget(xTarget, yTarget, zTarget);
    }

    public MovingEffect(int xSource, int ySource, int zSource, Mobile Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(xSource, ySource, zSource);
      this.SetTarget(Target);
    }

    public MovingEffect(int xSource, int ySource, int zSource, Item Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(xSource, ySource, zSource);
      this.SetTarget(Target);
    }

    public MovingEffect(Mobile Source, int xTarget, int yTarget, int zTarget, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(xTarget, yTarget, zTarget);
    }

    public MovingEffect(Mobile Source, Mobile Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(Target);
    }

    public MovingEffect(Mobile Source, Item Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(Target);
    }

    public MovingEffect(Item Source, int xTarget, int yTarget, int zTarget, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(xTarget, yTarget, zTarget);
    }

    public MovingEffect(Item Source, Mobile Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(Target);
    }

    public MovingEffect(Item Source, Item Target, int ItemID, IHue Hue)
      : this(ItemID, Hue)
    {
      this.SetSource(Source);
      this.SetTarget(Target);
    }

    public override void OnStart()
    {
      double duration = this.m_ItemID != 3853 ? (this.m_Source != null || this._effectId != 9501 ? 0.5 : 0.1 + Engine.Random.NextDouble() * 0.4) : 1.0;
      this.m_Start = Renderer.m_Frames;
      this.m_Sync = new TimeSync(duration);
    }

    public override void OnStop()
    {
      base.OnStop();
      if (this._effectId != 9501)
        return;
      Engine.Sounds.PlaySound(285, -1, -1, -1, 0.75f, 0.0f);
    }

    public override void RenderLight()
    {
      double normalized = this.m_Sync.Normalized;
      if (normalized >= 1.0)
        return;
      int itemId = this.m_ItemID;
      int xSource;
      int ySource;
      int xTarget;
      int yTarget;
      int xPixel;
      int yPixel;
      this.GetRenderLocation(normalized, out xSource, out ySource, out xTarget, out yTarget, out xPixel, out yPixel);
      xPixel -= Engine.GameX;
      int y = yPixel - Engine.GameY;
      if (itemId != 14239 && (itemId < 14036 || itemId > 14051) && (itemId < 14052 || itemId > 14067))
        return;
      Renderer.RenderLight(this.m_Start, xPixel, y, itemId, 1);
    }

    private void GetRenderLocation(double n, out int xSource, out int ySource, out int xTarget, out int yTarget, out int xPixel, out int yPixel)
    {
      int num1 = Renderer.m_xWorld;
      int num2 = Renderer.m_yWorld;
      int num3 = Renderer.m_zWorld;
      int X1 = 0;
      int Y1 = 0;
      int Z1 = 0;
      int xOffset = 0;
      int yOffset = 0;
      int fOffset = 0;
      this.GetSource(out X1, out Y1, out Z1);
      int X2 = X1 - num1;
      int Y2 = Y1 - num2;
      int Z2 = Z1 - num3;
      xSource = (Engine.GameWidth >> 1) + (X2 - Y2) * 22;
      ySource = (Engine.GameHeight >> 1) + (X2 + Y2) * 22 - Z2 * 4;
      if (this.m_Source is Mobile)
        ySource -= 30;
      xSource += Engine.GameX;
      ySource += Engine.GameY;
      xSource -= Renderer.m_xScroll;
      ySource -= Renderer.m_yScroll;
      if (this.m_Source != null && this.m_Source.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Source;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          xSource += xOffset;
          ySource += yOffset;
        }
      }
      this.GetTarget(out X2, out Y2, out Z2);
      X2 -= num1;
      int num4 = Y2 - num2;
      Z2 -= num3;
      xTarget = (Engine.GameWidth >> 1) + (X2 - num4) * 22;
      yTarget = (Engine.GameHeight >> 1) + (X2 + num4) * 22 - Z2 * 4;
      if (this.m_Target is Mobile)
        yTarget -= 50;
      xTarget += Engine.GameX;
      yTarget += Engine.GameY;
      xTarget -= Renderer.m_xScroll;
      yTarget -= Renderer.m_yScroll;
      if (this.m_Target != null && this.m_Target.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Target;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          xTarget += xOffset;
          yTarget += yOffset;
        }
      }
      xPixel = xSource + (int) ((double) (xTarget - xSource) * n);
      yPixel = ySource + (int) ((double) (yTarget - ySource) * n);
    }

    public override bool Slice()
    {
      double normalized = this.m_Sync.Normalized;
      if (normalized >= 1.0)
        return false;
      int xSource;
      int ySource;
      int xTarget;
      int yTarget;
      int xPixel;
      int yPixel;
      this.GetRenderLocation(normalized, out xSource, out ySource, out xTarget, out yTarget, out xPixel, out yPixel);
      IHue hue = Renderer.m_Dead ? Hues.Grayscale : this.m_Hue;
      int num1 = this.m_ItemID;
      if (this.m_Animated)
        num1 += (int) this.m_Animation[(Renderer.m_Frames - this.m_Start) / this.m_Delay % this.m_FrameCount];
      Texture texture1 = hue.GetItem(num1);
      double angle = Math.Atan2((double) (ySource - yTarget), (double) (xSource - xTarget));
      double xCenter = (double) (texture1.xMin + texture1.xMax) * 0.5;
      double yCenter = (double) (texture1.yMin + texture1.yMax) * 0.5;
      Renderer.FilterEnable = true;
      if (num1 == 3853)
        angle += normalized * Math.PI * 4.0;
      else if (this._effectId == 9501)
      {
        xCenter = 12.0;
        yCenter = 10.0;
      }
      RenderEffect renderEffect = Effects.GetItemEffect(num1);
      if (renderEffect != null && renderEffect.Glow != null)
      {
        Texture texture2 = Hues.Shadow.GetItem(num1);
        if (texture2 != null && !texture2.IsEmpty())
        {
          Renderer.PushAlpha(renderEffect.Glow.Alpha);
          Renderer.SetBlendType(DrawBlendType.Additive);
          int input = renderEffect.Glow.Color ?? texture2._averageColor;
          if (this._effectId == 9501)
            input = 16737792;
          int color1 = hue.Pixel32(input);
          texture2.DrawRotated(xPixel, yPixel, angle, color1, xCenter + 8.0, yCenter + 8.0);
          int? color2 = renderEffect.Glow.Color;
          if ((color2.GetValueOrDefault() != 9203350 ? 0 : (color2.HasValue ? 1 : 0)) != 0)
          {
            Renderer.SetAlpha(renderEffect.Glow.Alpha * 0.5f);
            texture2.DrawRotated(xPixel, yPixel, angle, color1, xCenter + 8.0, yCenter + 8.0);
          }
          Renderer.SetBlendType(DrawBlendType.Normal);
          Renderer.PopAlpha();
        }
      }
      float num2 = 1f;
      if (this._effectId == 9501)
      {
        Texture texture2 = hue.GetItem(4972);
        if (texture2 != null && !texture2.IsEmpty())
        {
          Renderer.FilterEnable = true;
          texture2.DrawRotated(xPixel, yPixel, angle + normalized * Math.PI * 4.0, 13395507);
          Renderer.FilterEnable = false;
          num2 = 0.5f;
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
      Renderer.PushAlpha(renderEffect.Alpha * num2);
      Renderer.SetBlendType(renderEffect.BlendType);
      Renderer.FilterEnable = true;
      int color = 16777215;
      if (this._effectId == 9501)
        color = 16737792;
      texture1.DrawRotated(xPixel, yPixel, angle, color, xCenter, yCenter);
      Renderer.FilterEnable = false;
      Renderer.SetBlendType(DrawBlendType.Normal);
      Renderer.PopAlpha();
      return true;
    }
  }
}
