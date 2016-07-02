// Decompiled with JetBrains decompiler
// Type: PlayUO.GLResizer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GLResizer : Gump
  {
    protected IResizable m_Target;
    protected int m_xOffset;

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
        return this.m_Target.Height;
      }
    }

    public GLResizer(IResizable Target)
      : base(0, 0)
    {
      this.m_Target = Target;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      Gumps.Capture = (Gump) this;
      this.m_xOffset = X;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (Gumps.Capture != this)
        return;
      Point screen1 = ((Gump) this.m_Target).PointToScreen(new Point(this.m_Target.Width, 0));
      Point screen2 = this.PointToScreen(new Point(X, Y));
      int num = screen1.X - (screen2.X - this.m_xOffset);
      if (num < this.m_Target.MinWidth || num > this.m_Target.MaxWidth)
        return;
      this.m_Target.Width = num;
      ((Gump) this.m_Target).X = screen2.X - this.m_xOffset;
      Engine.Redraw();
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (!Engine.amMoving)
        return !TargetManager.IsActive;
      return false;
    }
  }
}
