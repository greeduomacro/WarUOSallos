// Decompiled with JetBrains decompiler
// Type: PlayUO.DragEffect
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class DragEffect : MovingEffect
  {
    protected bool m_Double;
    private VertexCache m_vCache;
    private VertexCache m_vCacheDouble;

    public DragEffect(int itemID, int sourceSerial, int xSource, int ySource, int zSource, int targetSerial, int xTarget, int yTarget, int zTarget, IHue hue, bool shouldDouble)
      : base(sourceSerial, targetSerial, xSource, ySource, zSource, xTarget, yTarget, zTarget, itemID, hue)
    {
      this.m_Double = shouldDouble;
    }

    public override void OnStart()
    {
      this.m_Start = Renderer.m_Frames;
      this.m_Sync = new TimeSync(0.25);
    }

    public override void OnStop()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCacheDouble = (VertexCache) null;
    }

    public override bool Slice()
    {
      double normalized = this.m_Sync.Normalized;
      if (normalized >= 1.0)
        return false;
      int num1 = Renderer.m_xWorld;
      int num2 = Renderer.m_yWorld;
      int num3 = Renderer.m_zWorld;
      int X1 = 0;
      int Y = 0;
      int Z1 = 0;
      int xOffset = 0;
      int yOffset = 0;
      int fOffset = 0;
      this.GetSource(out X1, out Y, out Z1);
      int X2 = X1 - num1;
      Y -= num2;
      int Z2 = Z1 - num3;
      int num4 = (Engine.GameWidth >> 1) + (X2 - Y) * 22;
      int num5 = (Engine.ScreenHeight >> 1) + (X2 + Y) * 22 - Z2 * 4;
      int num6 = num4 + Engine.GameX;
      int num7 = num5 + Engine.GameY;
      int num8 = num6 - Renderer.m_xScroll;
      int num9 = num7 - Renderer.m_yScroll;
      if (this.m_Source != null && this.m_Source.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Source;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          num8 += xOffset;
          num9 += yOffset;
        }
        num9 -= 30;
      }
      this.GetTarget(out X2, out Y, out Z2);
      int num10 = X2 - num1;
      Y -= num2;
      Z2 -= num3;
      int num11 = (Engine.GameWidth >> 1) + (num10 - Y) * 22;
      int num12 = (Engine.GameHeight >> 1) + (num10 + Y) * 22 - Z2 * 4;
      int num13 = num11 + Engine.GameX;
      int num14 = num12 + Engine.GameY;
      int num15 = num13 - Renderer.m_xScroll;
      int num16 = num14 - Renderer.m_yScroll;
      if (this.m_Target != null && this.m_Target.GetType() == typeof (Mobile))
      {
        Mobile mobile = (Mobile) this.m_Target;
        if (mobile.Walking.Count > 0 && ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          num15 += xOffset;
          num16 += yOffset;
        }
        num16 -= 30;
      }
      Texture tex = !this.m_Animated ? (!Renderer.m_Dead ? this.m_Hue.GetItem(this.m_ItemID) : Hues.Grayscale.GetItem(this.m_ItemID)) : (!Renderer.m_Dead ? this.m_Hue.GetItem(this.m_ItemID + (int) this.m_Animation[(Renderer.m_Frames - this.m_Start) / this.m_Delay % this.m_FrameCount]) : Hues.Grayscale.GetItem(this.m_ItemID + (int) this.m_Animation[(Renderer.m_Frames - this.m_Start) / this.m_Delay % this.m_FrameCount]));
      int num17;
      int num18;
      if (this.m_Source == null)
      {
        num17 = num8 - tex.Width / 2;
        num18 = num9 + (22 - tex.Height);
      }
      else
      {
        num17 = num8 - (tex.xMin + (tex.xMax - tex.xMin) / 2);
        num18 = num9 - (tex.yMin + (tex.yMax - tex.yMin) / 2);
      }
      int num19;
      int num20;
      if (this.m_Target == null)
      {
        num19 = num15 - tex.Width / 2;
        num20 = num16 + (22 - tex.Height);
      }
      else
      {
        num19 = num15 - (tex.xMin + (tex.xMax - tex.xMin) / 2);
        num20 = num16 - (tex.yMin + (tex.yMax - tex.yMin) / 2);
      }
      int x = num17 + (int) ((double) (num19 - num17) * normalized);
      int y = num18 + (int) ((double) (num20 - num18) * normalized);
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      this.m_vCache.DrawGame(tex, x, y);
      if (this.m_Double)
      {
        if (this.m_vCacheDouble == null)
          this.m_vCacheDouble = this.VCPool.GetInstance();
        this.m_vCacheDouble.DrawGame(tex, x + 5, y + 5);
      }
      return true;
    }
  }
}
