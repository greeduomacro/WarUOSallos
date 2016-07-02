// Decompiled with JetBrains decompiler
// Type: PlayUO.GProfileScroll
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GProfileScroll : GImage
  {
    private Mobile m_Mobile;

    public GProfileScroll(Mobile m)
      : base(2002, 24, 195)
    {
      this.m_Mobile = m;
      this.m_Tooltip = (ITooltip) new Tooltip("View Profile");
      this.m_CanDrag = true;
      this.m_QuickDrag = false;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw && Gumps.Drag == null)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void OnDragStart()
    {
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Parent.PointToScreen(new Point(0, 0));
      this.m_Parent.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Parent.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Parent.m_IsDragging = true;
      Gumps.Drag = this.m_Parent;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Parent.BringToTop();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Point client = this.m_Parent.PointToClient(this.PointToScreen(new Point(x, y)));
      this.m_Parent.OnMouseUp(client.X, client.Y, mb);
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      Network.Send((Packet) new PProfileRequest(this.m_Mobile));
    }
  }
}
