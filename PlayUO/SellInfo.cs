// Decompiled with JetBrains decompiler
// Type: PlayUO.SellInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Data;

namespace PlayUO
{
  public class SellInfo : IComparable
  {
    private Item m_Item;
    private int m_ItemID;
    private IHue m_Hue;
    private int m_Amount;
    private int m_Price;
    private string m_Name;
    private double m_ToSell;
    private GSellGump_InventoryItem m_InventoryGump;
    private GSellGump_OfferedItem m_OfferedGump;

    public GSellGump_OfferedItem OfferedGump
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

    public GSellGump_InventoryItem InventoryGump
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

    public int ToSell
    {
      get
      {
        return (int) (this.m_ToSell + 0.5);
      }
      set
      {
        this.m_ToSell = (double) value;
      }
    }

    public double dToSell
    {
      get
      {
        return this.m_ToSell;
      }
      set
      {
        this.m_ToSell = value;
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
        return this.m_ItemID;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public int Amount
    {
      get
      {
        return this.m_Amount;
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

    public SellInfo(Item item, int itemID, int hue, int amount, int price, string name)
    {
      this.m_Item = item;
      this.m_ItemID = itemID;
      this.m_Amount = amount;
      this.m_Price = price;
      try
      {
        this.m_Name = Localization.GetString(Convert.ToInt32(name));
      }
      catch
      {
        this.m_Name = name;
      }
      if (!Map.m_ItemFlags[itemID & 16383][(TileFlag) 262144L])
        hue ^= 32768;
      this.m_Hue = Hues.Load(hue);
    }

    int IComparable.CompareTo(object x)
    {
      if (x == null)
        return 1;
      SellInfo sellInfo = x as SellInfo;
      if (sellInfo == null)
        throw new ArgumentException();
      int num = Map.GetQuality(this.m_Item.ID).CompareTo(Map.GetQuality(sellInfo.m_Item.ID));
      if (num == 0)
      {
        num = this.m_Item.ID.CompareTo(sellInfo.m_Item.ID);
        if (num == 0)
          num = this.m_Item.Serial.CompareTo(sellInfo.m_Item.Serial);
      }
      return num;
    }
  }
}
