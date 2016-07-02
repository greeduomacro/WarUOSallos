// Decompiled with JetBrains decompiler
// Type: PlayUO.GDamageLabel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GDamageLabel : GLabel
  {
    private TimeSync m_Sync;
    private Mobile m_Mobile;
    private Timer m_Timer;

    public GDamageLabel(int damage, Mobile m)
      : base(damage.ToString(), Engine.DefaultFont, Hues.Load(m.IsPoisoned ? 63 : 38), m.ScreenX, m.ScreenY - 30)
    {
      this.m_Mobile = m;
      this.m_Sync = new TimeSync(0.75);
      this.UpdatePosition();
    }

    private void Delete_OnTick(Timer t)
    {
      Gumps.Destroy((Gump) this);
    }

    public void UpdatePosition()
    {
      double normalized = this.m_Sync.Normalized;
      if (normalized >= 1.0)
      {
        if (this.m_Timer != null)
          return;
        this.m_Timer = new Timer(new OnTick(this.Delete_OnTick), 0, 1);
        this.m_Timer.Start(false);
      }
      else
      {
        if (normalized >= 0.5)
          this.Alpha = (float) (1.0 - (normalized - 0.5) * 2.0);
        int num1 = Renderer.m_xWorld;
        int num2 = Renderer.m_yWorld;
        int num3 = Renderer.m_zWorld;
        int x = this.m_Mobile.X;
        int y = this.m_Mobile.Y;
        int z = this.m_Mobile.Z;
        int num4 = x - num1;
        int num5 = y - num2;
        int num6 = z - num3;
        int num7 = (Engine.GameWidth >> 1) + (num4 - num5) * 22;
        int num8 = (Engine.GameHeight >> 1) + 22 + (num4 + num5) * 22 - num6 * 4;
        int num9 = num7 + Engine.GameX;
        int num10 = num8 + Engine.GameY;
        if (this.m_Mobile.Walking.Count > 0)
        {
          WalkAnimation walkAnimation = (WalkAnimation) this.m_Mobile.Walking.Peek();
          int xOffset = 0;
          int yOffset = 0;
          int fOffset = 0;
          if (walkAnimation.Snapshot(ref xOffset, ref yOffset, ref fOffset))
          {
            num9 += xOffset;
            num10 += yOffset;
          }
        }
        int num11 = num9 - Renderer.m_xScroll;
        int num12 = num10 - Renderer.m_yScroll;
        this.X = num11 - (this.Image.xMax - this.Image.xMin + 1) / 2 - this.Image.xMin;
        this.Y = num12 - 30 - (int) (normalized * 40.0);
        this.Scissor(new Clipper(Engine.GameX, Engine.GameY, Engine.GameWidth, Engine.GameHeight));
      }
    }

    protected internal override void Render(int X, int Y)
    {
      this.UpdatePosition();
      base.Render(X, Y);
    }
  }
}
