// Decompiled with JetBrains decompiler
// Type: PlayUO.DynamicItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Data;

namespace PlayUO
{
  public class DynamicItem : AgentCell, IItem, ITile, ICell, IDisposable, IEntity
  {
    private static Type MyType = typeof (DynamicItem);
    public Item m_Item;
    private Texture m_LastImage;
    public IHue m_Hue;
    private IHue m_LastImageHue;
    public short m_ID;
    private short m_LastImageID;
    public byte m_Height;
    public sbyte m_Z;

    public ItemId ItemId
    {
      get
      {
        return this.m_Item.ItemId;
      }
    }

    public Type CellType
    {
      get
      {
        return DynamicItem.MyType;
      }
    }

    public byte CalcHeight
    {
      get
      {
        if (Map.m_ItemFlags[(int) this.m_ID][(TileFlag) 1024L])
          return (byte) ((uint) this.m_Height / 2U);
        return this.m_Height;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Item.Serial;
      }
    }

    public short ID
    {
      get
      {
        return this.m_ID;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public sbyte Z
    {
      get
      {
        return this.m_Z;
      }
    }

    public sbyte SortZ
    {
      get
      {
        return this.m_Z;
      }
      set
      {
      }
    }

    public byte Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public DynamicItem(Item i)
      : base((PhysicalAgent) i)
    {
      this.m_Item = i;
      this.Update();
    }

    void IDisposable.Dispose()
    {
    }

    public Texture GetItem(IHue hue, short itemID)
    {
      if (this.m_LastImageHue != hue || (int) this.m_LastImageID != (int) itemID)
      {
        this.m_LastImageHue = hue;
        this.m_LastImageID = itemID;
        this.m_LastImage = hue.GetItem((int) itemID);
      }
      return this.m_LastImage;
    }

    public override void Update()
    {
      this.m_ID = (short) this.m_Item.ID;
      this.m_Z = (sbyte) this.m_Item.Z;
      this.m_Hue = Hues.GetItemHue((int) this.m_ID, (int) this.m_Item.Hue);
      this.m_Height = (byte) this.m_Item.Height;
    }
  }
}
