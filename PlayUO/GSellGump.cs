// Decompiled with JetBrains decompiler
// Type: PlayUO.GSellGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GSellGump : GDragable
  {
    private int m_Serial;
    private SellInfo[] m_Info;
    private SellInfo m_Selected;
    private GSellGump_OfferMenu m_OfferMenu;
    private int m_xLast;
    private int m_yLast;

    public GSellGump_OfferMenu OfferMenu
    {
      get
      {
        return this.m_OfferMenu;
      }
    }

    public SellInfo Selected
    {
      get
      {
        return this.m_Selected;
      }
      set
      {
        if (this.m_Selected == value)
          return;
        if (this.m_Selected != null)
          this.m_Selected.InventoryGump.Selected = false;
        this.m_Selected = value;
        this.m_Selected.InventoryGump.Selected = true;
      }
    }

    public GSellGump(int serial, SellInfo[] info)
      : base(2162, 15, 15)
    {
      this.m_GUID = string.Format("GSellGump-{0}", (object) serial);
      this.m_Serial = serial;
      this.m_Info = info;
      Engine.GetUniFont(3);
      Hues.Load(648);
      Array.Sort<SellInfo>(info);
      int y = 66;
      for (int index = 0; index < info.Length; ++index)
      {
        bool seperate = index != info.Length - 1;
        SellInfo si = info[index];
        GSellGump_InventoryItem gumpInventoryItem = new GSellGump_InventoryItem(this, si, y, seperate);
        this.m_Children.Add((Gump) gumpInventoryItem);
        si.InventoryGump = gumpInventoryItem;
        y += gumpInventoryItem.Height;
        if (seperate)
          y += 16;
      }
      if (y > 230)
      {
        GVSlider gvSlider = new GVSlider(2088, 237, 81, 34, 92, 0.0, 0.0, (double) (y - 230), 1.0);
        gvSlider.OnValueChange = new OnValueChange(this.Slider_OnValueChange);
        this.m_Children.Add((Gump) gvSlider);
        this.m_Children.Add((Gump) new GHotspot(237, 66, 34, 122, (Gump) gvSlider));
      }
      this.m_NonRestrictivePicking = true;
      this.m_OfferMenu = new GSellGump_OfferMenu(this);
      this.m_Children.Add((Gump) this.m_OfferMenu);
      this.m_X = (Engine.ScreenWidth - (this.m_OfferMenu.X + this.m_OfferMenu.Width)) / 2;
      this.m_Y = (Engine.ScreenHeight - (this.m_OfferMenu.Y + this.m_OfferMenu.Height)) / 2;
    }

    public int ComputeTotalCost()
    {
      int num = 0;
      for (int index = 0; index < this.m_Info.Length; ++index)
        num += this.m_Info[index].ToSell * this.m_Info[index].Price;
      return num;
    }

    public void Accept()
    {
      Network.Send((Packet) new PSellItems(this.m_Serial, this.m_Info));
      this.m_OfferMenu.WriteSignature();
    }

    public void Clear()
    {
      for (int index = 0; index < this.m_Info.Length; ++index)
      {
        SellInfo sellInfo = this.m_Info[index];
        if (sellInfo.ToSell > 0)
        {
          sellInfo.ToSell = 0;
          sellInfo.InventoryGump.Available = sellInfo.Amount;
          Gumps.Destroy((Gump) sellInfo.OfferedGump);
          sellInfo.OfferedGump = (GSellGump_OfferedItem) null;
        }
      }
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_xLast != x || this.m_yLast != y)
      {
        this.m_xLast = x;
        this.m_yLast = y;
        Clipper clipper = new Clipper(x + 31, y + 59, 197, 177);
        foreach (Gump gump in this.m_Children.ToArray())
        {
          if (gump is GSellGump_InventoryItem)
            ((GRegion) gump).Clipper = clipper;
        }
      }
      base.Draw(x, y);
    }

    private void Slider_OnValueChange(double v, double o, Gump slider)
    {
      Gump[] array = this.m_Children.ToArray();
      int num = -(int) v;
      for (int index = 0; index < array.Length; ++index)
      {
        Gump gump = array[index];
        if (gump is GSellGump_InventoryItem)
          ((GSellGump_InventoryItem) gump).Offset = num;
      }
    }
  }
}
