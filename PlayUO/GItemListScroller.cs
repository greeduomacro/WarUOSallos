// Decompiled with JetBrains decompiler
// Type: PlayUO.GItemListScroller
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GItemListScroller : GHitspot
  {
    private GItemList m_Owner;
    private int m_Offset;
    private double m_Last;
    private bool m_Scrolling;

    public GItemListScroller(int x, GItemList owner, int offset)
      : base(x, 59, 12, 19, (OnClick) null)
    {
      this.m_Owner = owner;
      this.m_Offset = offset;
      this.m_Last = -1234.56;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.m_Scrolling = true;
      this.m_Last = Engine.dTicks;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Owner.BringToTop();
      this.OnMouseEnter(x, y, mb);
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.m_Scrolling = false;
      this.m_Last = -1234.56;
    }

    protected internal override void OnMouseLeave()
    {
      this.m_Scrolling = false;
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_Scrolling || Gumps.LastOver != this)
        return;
      if (this.m_Last != -1234.56)
        this.m_Owner.xOffset += (Engine.dTicks - this.m_Last) / 1000.0 * (double) this.m_Offset;
      this.m_Last = Engine.dTicks;
    }
  }
}
