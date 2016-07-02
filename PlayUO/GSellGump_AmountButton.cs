// Decompiled with JetBrains decompiler
// Type: PlayUO.GSellGump_AmountButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GSellGump_AmountButton : GImage
  {
    private int m_InitialOffset;
    private double m_Offset;
    private double m_Last;
    private bool m_Scrolling;
    private GSellGump m_Owner;
    private SellInfo m_Info;

    public GSellGump_AmountButton(GSellGump owner, SellInfo info, int offset, int gumpID, int x)
      : base(gumpID, x, 0)
    {
      this.m_Owner = owner;
      this.m_Info = info;
      this.m_InitialOffset = offset;
      this.m_Offset = (double) offset;
      this.m_Last = -1234.56;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw && this.m_Clipper != null)
        return this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)));
      return false;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.m_Scrolling = true;
      Engine.ResetTicks();
      this.m_Last = Engine.dTicks + 150.0;
      this.m_Info.ToSell += Math.Sign(this.m_Offset);
      this.AmountChanged();
    }

    private void AmountChanged()
    {
      if (this.m_Info.ToSell <= 0)
      {
        this.m_Info.ToSell = 0;
        this.m_Info.OfferedGump = (GSellGump_OfferedItem) null;
        Gumps.Destroy(this.m_Parent);
      }
      else if (this.m_Info.ToSell > this.m_Info.Amount)
        this.m_Info.ToSell = this.m_Info.Amount;
      this.m_Info.InventoryGump.Available = this.m_Info.Amount - this.m_Info.ToSell;
      if (this.m_Info.OfferedGump != null)
        this.m_Info.OfferedGump.Amount = this.m_Info.ToSell;
      this.m_Owner.OfferMenu.UpdateTotal();
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.OnMouseEnter(x, y, mb);
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.m_Scrolling = false;
      this.m_Offset = (double) this.m_InitialOffset;
    }

    protected internal override void OnMouseLeave()
    {
      this.m_Scrolling = false;
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_Scrolling && Gumps.LastOver == this && (this.m_Last != -1234.56 && Engine.dTicks > this.m_Last))
      {
        this.m_Info.dToSell += (Engine.dTicks - this.m_Last) / 1000.0 * this.m_Offset;
        this.m_Offset += (double) Math.Sign(this.m_Offset) * ((Engine.dTicks - this.m_Last) / 1000.0) * 5.0;
        this.AmountChanged();
        this.m_Last = Engine.dTicks;
      }
      base.Draw(x, y);
    }
  }
}
