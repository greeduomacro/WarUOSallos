// Decompiled with JetBrains decompiler
// Type: PlayUO.BuyInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class BuyInfo : IComparable
  {
    private Item m_Item;
    private string m_Name;
    private int m_Price;
    private double m_ToBuy;
    private GBuyGump_InventoryItem m_InventoryGump;
    private GBuyGump_OfferedItem m_OfferedGump;

    public GBuyGump_OfferedItem OfferedGump
    {
      get
      {
        return this.m_OfferedGump;
      }
      set
      {
        this.m_OfferedGump = value;
      }
    }

    public GBuyGump_InventoryItem InventoryGump
    {
      get
      {
        return this.m_InventoryGump;
      }
      set
      {
        this.m_InventoryGump = value;
      }
    }

    public int ToBuy
    {
      get
      {
        return (int) (this.m_ToBuy + 0.5);
      }
      set
      {
        this.m_ToBuy = (double) value;
      }
    }

    public double dToBuy
    {
      get
      {
        return this.m_ToBuy;
      }
      set
      {
        this.m_ToBuy = value;
      }
    }

    public Item Item
    {
      get
      {
        return this.m_Item;
      }
    }

    public int ItemID
    {
      get
      {
        return this.m_Item.ID & 16383;
      }
    }

    public IHue Hue
    {
      get
      {
        return Hues.GetItemHue(this.m_Item.ID, (int) this.m_Item.Hue);
      }
    }

    public int Amount
    {
      get
      {
        return this.m_Item.Amount;
      }
    }

    public int Price
    {
      get
      {
        return this.m_Price;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public BuyInfo(Item item, int price, string name)
    {
      this.m_Item = item;
      this.m_Price = price;
      try
      {
        this.m_Name = Localization.GetString(Convert.ToInt32(name));
      }
      catch
      {
        this.m_Name = name;
      }
    }

    int IComparable.CompareTo(object x)
    {
      if (x == null)
        return 1;
      BuyInfo buyInfo = x as BuyInfo;
      if (buyInfo == null)
        throw new ArgumentException();
      int num = Map.GetQuality(this.m_Item.ID).CompareTo(Map.GetQuality(buyInfo.m_Item.ID));
      if (num == 0)
      {
        num = this.m_Item.ID.CompareTo(buyInfo.m_Item.ID);
        if (num == 0)
          num = this.m_Item.Serial.CompareTo(buyInfo.m_Item.Serial);
      }
      return num;
    }
  }
}
