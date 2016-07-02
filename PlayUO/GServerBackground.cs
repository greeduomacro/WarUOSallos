// Decompiled with JetBrains decompiler
// Type: PlayUO.GServerBackground
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GServerBackground : GBackground
  {
    private GServerGump m_Owner;

    public GServerBackground(GServerGump owner, int x, int y, int width, int height, int gumpID, bool border)
      : base(gumpID, width, height, x, y, border)
    {
      this.m_Owner = owner;
      this.m_QuickDrag = true;
      this.m_CanDrag = owner.CanMove;
    }

    protected internal override void OnDragStart()
    {
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Owner.PointToScreen(new Point(0, 0));
      this.m_Owner.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Owner.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Owner.m_IsDragging = true;
      Gumps.Drag = (Gump) this.m_Owner;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Owner.BringToTop();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None || !this.m_Owner.CanClose)
        return;
      GServerGump.ClearCachedLocation(this.m_Owner.DialogID);
      Network.Send((Packet) new PGumpButton(this.m_Owner, 0));
      Gumps.Destroy(this.m_Parent);
    }
  }
}
