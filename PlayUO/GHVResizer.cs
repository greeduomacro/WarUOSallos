// Decompiled with JetBrains decompiler
// Type: PlayUO.GHVResizer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GHVResizer : Gump
  {
    protected IResizable m_Target;
    protected int m_xOffset;
    protected int m_yOffset;

    public override int Width
    {
      get
      {
        return 6;
      }
    }

    public override int Height
    {
      get
      {
        return 6;
      }
    }

    public GHVResizer(IResizable Target)
      : base(0, 0)
    {
      this.m_Target = Target;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      Gumps.Capture = (Gump) this;
      this.m_xOffset = X;
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
      Point screen1 = ((Gump) this.m_Target).PointToScreen(new Point(0, 0));
      Point screen2 = this.PointToScreen(new Point(X, Y));
      int num1 = screen2.X - screen1.X + 6 - this.m_xOffset;
      if (num1 < this.m_Target.MinWidth)
        num1 = this.m_Target.MinWidth;
      else if (num1 > this.m_Target.MaxWidth)
        num1 = this.m_Target.MaxWidth;
      int num2 = screen2.Y - screen1.Y + 6 - this.m_yOffset;
      if (num2 < this.m_Target.MinHeight)
        num2 = this.m_Target.MinHeight;
      else if (num2 > this.m_Target.MaxHeight)
        num2 = this.m_Target.MaxHeight;
      this.m_Target.Width = num1;
      this.m_Target.Height = num2;
      Engine.Redraw();
    }

    protected internal override void Draw(int X, int Y)
    {
      this.m_X = this.m_Target.Width - 5;
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
