// Decompiled with JetBrains decompiler
// Type: PlayUO.GHotspot
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;

namespace PlayUO
{
  public class GHotspot : Gump
  {
    private bool m_NormalHit = true;
    private int m_Width;
    private int m_Height;
    private Gump m_Target;

    public bool NormalHit
    {
      get
      {
        return this.m_NormalHit;
      }
      set
      {
        this.m_NormalHit = value;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
      }
    }

    public GHotspot(int X, int Y, int Width, int Height, Gump Target)
      : base(X, Y)
    {
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Target = Target;
    }

    protected internal override void Draw(int X, int Y)
    {
    }

    protected internal override void OnDragStart()
    {
      if (this.m_Target == null)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Target.PointToScreen(new Point(0, 0));
      this.m_Target.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Target.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Target.m_IsDragging = true;
      Gumps.Drag = this.m_Target;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_NormalHit)
        return true;
      if (!Engine.amMoving)
        return !TargetManager.IsActive;
      return false;
    }

    protected internal override void OnSingleClick(int X, int Y)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnSingleClick(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y);
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnDoubleClick(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y);
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseMove(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseDown(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseEnter(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseUp(this.m_X - this.m_Target.X + X, this.m_Y - this.m_Target.Y + Y, mb);
    }

    protected internal override void OnMouseLeave()
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseLeave();
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      if (this.m_Target == null)
        return;
      this.m_Target.OnMouseWheel(Delta);
    }
  }
}
