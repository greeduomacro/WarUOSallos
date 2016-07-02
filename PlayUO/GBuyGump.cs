// Decompiled with JetBrains decompiler
// Type: PlayUO.GBuyGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GBuyGump : GDragable
  {
    private int m_Serial;
    private BuyInfo[] m_Info;
    private BuyInfo m_Selected;
    private GBuyGump_OfferMenu m_OfferMenu;
    private int m_xLast;
    private int m_yLast;

    public GBuyGump_OfferMenu OfferMenu
    {
      get
      {
        return this.m_OfferMenu;
      }
    }

    public BuyInfo Selected
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

    public GBuyGump(int serial, BuyInfo[] info)
      : base(2160, 15, 15)
    {
      this.m_GUID = string.Format("GBuyGump-{0}", (object) serial);
      this.m_Serial = serial;
      this.m_Info = info;
      Engine.GetUniFont(3);
      Hues.Load(648);
      Array.Sort<BuyInfo>(info);
      int y = 66;
      for (int index = 0; index < info.Length; ++index)
      {
        bool seperate = index != info.Length - 1;
        BuyInfo si = info[index];
        GBuyGump_InventoryItem gumpInventoryItem = new GBuyGump_InventoryItem(this, si, y, seperate);
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
      this.m_OfferMenu = new GBuyGump_OfferMenu(this);
      this.m_Children.Add((Gump) this.m_OfferMenu);
      this.m_X = (Engine.ScreenWidth - (this.m_OfferMenu.X + this.m_OfferMenu.Width)) / 2;
      this.m_Y = (Engine.ScreenHeight - (this.m_OfferMenu.Y + this.m_OfferMenu.Height)) / 2;
    }

    public int ComputeTotalCost()
    {
      int num = 0;
      for (int index = 0; index < this.m_Info.Length; ++index)
        num += this.m_Info[index].ToBuy * this.m_Info[index].Price;
      return num;
    }

    public void Accept()
    {
      Network.Send((Packet) new PBuyItems(this.m_Serial, this.m_Info));
      this.m_OfferMenu.WriteSignature();
    }

    public void Clear()
    {
      for (int index = 0; index < this.m_Info.Length; ++index)
      {
        BuyInfo buyInfo = this.m_Info[index];
        if (buyInfo.ToBuy > 0)
        {
          buyInfo.ToBuy = 0;
          buyInfo.InventoryGump.Available = buyInfo.Amount;
          Gumps.Destroy((Gump) buyInfo.OfferedGump);
          buyInfo.OfferedGump = (GBuyGump_OfferedItem) null;
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
          if (gump is GBuyGump_InventoryItem)
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
        if (gump is GBuyGump_InventoryItem)
          ((GBuyGump_InventoryItem) gump).Offset = num;
      }
    }
  }
}
