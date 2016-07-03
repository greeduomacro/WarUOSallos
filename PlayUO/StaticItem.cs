// Decompiled with JetBrains decompiler
// Type: PlayUO.StaticItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using Ultima.Data;

namespace PlayUO
{
  public sealed class StaticItem : IItem, ITile, ICell, IDisposable
  {
    private static Type MyType = typeof (StaticItem);
    private static Queue m_InstancePool = new Queue();
    public int Serial;
    public int m_SortInfluence;
    private Texture m_LastImage;
    private IHue m_LastImageHue;
    public Texture m_sDraw;
    public IHue m_Hue;
    public TransformedColoredTextured[] m_vPool;
    private short m_LastImageID;
    public short m_ID;
    public short m_RealID;
    public byte m_Height;
    public float m_fAlpha;
    public sbyte m_Z;
    public bool m_bDraw;
    public bool m_bInit;

    public ItemId ItemId
    {
      get
      {
        return (ItemId) (int) (ushort) this.m_ID;
      }
    }

    public unsafe ItemData* ItemDataPointer
    {
      get
      {
        return Map.GetItemDataPointer(this.ItemId);
      }
    }

    public Type CellType
    {
      get
      {
        return StaticItem.MyType;
      }
    }

    public int SortInfluence
    {
      get
      {
        return this.m_SortInfluence;
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

    public unsafe TileFlag TileFlags
    {
      get
      {
        return (TileFlag) this.ItemDataPointer->Flags;
      }
    }

    public bool IsBridge
    {
      get
      {
        return (this.TileFlags & (TileFlag) 1024L) != 0L;
      }
    }

    public byte CalcHeight
    {
      get
      {
        byte height = this.Height;
        if (this.IsBridge)
          height /= (byte) 2;
        return height;
      }
    }

    public byte Height
    {
      get
      {
        return this.m_Height;
      }
    }

    private StaticItem(HuedTile tile, int influence, int serial)
    {
      this.m_ID = (short) tile.itemId;
      this.m_Z = tile.z;
      this.m_RealID = this.m_ID;
      this.m_Hue = Hues.GetItemHue((int) this.m_ID, (int) tile.hueId);
      this.m_Height = Map.GetItemHeight(tile.itemId);
      this.m_SortInfluence = influence;
      this.Serial = serial;
      this.m_vPool = VertexConstructor.Create();
    }

    private StaticItem(short ItemID, sbyte Z, int serial)
    {
      this.m_RealID = ItemID;
      this.m_ID = this.m_RealID;
      this.m_Z = Z;
      this.m_Height = Map.GetItemHeight((ItemId) (int) (ushort) this.m_RealID);
      this.m_Hue = Hues.Default;
      this.Serial = serial;
      this.m_vPool = VertexConstructor.Create();
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

    public static StaticItem Instantiate(HuedTile tile, int influence, int serial)
    {
      if (StaticItem.m_InstancePool.Count <= 0)
        return new StaticItem(tile, influence, serial);
      StaticItem staticItem = (StaticItem) StaticItem.m_InstancePool.Dequeue();
      staticItem.m_RealID = (short) tile.itemId;
      staticItem.m_ID = staticItem.m_RealID;
      staticItem.m_Z = tile.z;
      staticItem.m_Hue = Hues.GetItemHue((int) staticItem.m_ID, (int) tile.hueId);
      staticItem.m_Height = Map.GetItemHeight(tile.itemId);
      staticItem.m_SortInfluence = influence;
      staticItem.Serial = serial;
      staticItem.m_LastImage = (Texture) null;
      staticItem.m_LastImageHue = (IHue) null;
      staticItem.m_LastImageID = (short) 0;
      staticItem.m_fAlpha = 0.0f;
      staticItem.m_bDraw = false;
      staticItem.m_bInit = false;
      return staticItem;
    }

    public static StaticItem Instantiate(short itemID, short realID, sbyte z, int serial)
    {
      if (StaticItem.m_InstancePool.Count <= 0)
        return new StaticItem(itemID, z, serial);
      StaticItem staticItem = (StaticItem) StaticItem.m_InstancePool.Dequeue();
      staticItem.m_RealID = realID;
      staticItem.m_ID = itemID;
      staticItem.m_Z = z;
      staticItem.m_Hue = Hues.Default;
      staticItem.m_Height = Map.GetItemHeight((ItemId) (int) (ushort) realID);
      staticItem.m_SortInfluence = 0;
      staticItem.Serial = serial;
      staticItem.m_LastImage = (Texture) null;
      staticItem.m_LastImageHue = (IHue) null;
      staticItem.m_LastImageID = (short) 0;
      staticItem.m_fAlpha = 0.0f;
      staticItem.m_bDraw = false;
      staticItem.m_bInit = false;
      return staticItem;
    }

    public static StaticItem Instantiate(short itemID, sbyte z, int serial)
    {
      if (StaticItem.m_InstancePool.Count <= 0)
        return new StaticItem(itemID, z, serial);
      StaticItem staticItem = (StaticItem) StaticItem.m_InstancePool.Dequeue();
      staticItem.m_RealID = itemID;
      staticItem.m_ID = staticItem.m_RealID;
      staticItem.m_Z = z;
      staticItem.m_Hue = Hues.Default;
      staticItem.m_Height = Map.GetItemHeight((ItemId) (int) (ushort) staticItem.m_RealID);
      staticItem.m_SortInfluence = 0;
      staticItem.Serial = serial;
      staticItem.m_LastImage = (Texture) null;
      staticItem.m_LastImageHue = (IHue) null;
      staticItem.m_LastImageID = (short) 0;
      staticItem.m_fAlpha = 0.0f;
      staticItem.m_bDraw = false;
      staticItem.m_bInit = false;
      return staticItem;
    }

    void IDisposable.Dispose()
    {
      StaticItem.m_InstancePool.Enqueue((object) this);
    }
  }
}
