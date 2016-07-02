// Decompiled with JetBrains decompiler
// Type: PlayUO.GVResizer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GVResizer : Gump
  {
    protected IResizable m_Target;
    protected int m_yOffset;

    public override int Width
    {
      get
      {
        return this.m_Target.Width;
      }
    }

    public override int Height
    {
      get
      {
        return 6;
      }
    }

    public GVResizer(IResizable Target)
      : base(0, 0)
    {
      this.m_Target = Target;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      Gumps.Capture = (Gump) this;
      this.m_yOffset = Y;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture != this)
        return;
      Point screen = ((Gump) this.m_Target).PointToScreen(new Point(0, 0));
      int num = this.PointToScreen(new Point(X, Y)).Y - screen.Y + 6 - this.m_yOffset;
      if (num < this.m_Target.MinHeight)
        num = this.m_Target.MinHeight;
      else if (num > this.m_Target.MaxHeight)
        num = this.m_Target.MaxHeight;
      this.m_Target.Height = num;
      Engine.Redraw();
    }

    protected internal override void Draw(int X, int Y)
    {
      this.m_Y = this.m_Target.Height - 5;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (!Engine.amMoving)
        return !TargetManager.IsActive;
      return false;
    }
  }
}
