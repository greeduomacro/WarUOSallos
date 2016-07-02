// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.ItemRef
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class ItemRef : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("item", new ConstructCallback((object) null, __methodptr(Construct)));
    private int serial;
    private int itemId;

    public virtual PersistableType TypeID
    {
      get
      {
        return ItemRef.TypeCode;
      }
    }

    public int Serial
    {
      get
      {
        return this.serial;
      }
      set
      {
        this.serial = value;
      }
    }

    public int ItemID
    {
      get
      {
        return this.itemId;
      }
      set
      {
        this.itemId = value;
      }
    }

    public bool IsNull
    {
      get
      {
        if (this.serial == 0)
          return this.itemId == 0;
        return false;
      }
    }

    protected ItemRef()
    {
      base.\u002Ector();
    }

    public ItemRef(Item item)
    {
      base.\u002Ector();
      if (item == null)
        return;
      this.serial = item.Serial;
      this.itemId = item.ID;
    }

    public ItemRef(int itemId)
    {
      base.\u002Ector();
      this.itemId = itemId;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ItemRef();
    }

    public Item Find()
    {
      Item obj = World.FindItem(this.serial);
      if (obj != null && (this.itemId == 0 || obj.ID == this.itemId))
        return obj;
      return (Item) null;
    }

    public Item FindOnPlayer()
    {
      Item obj = this.Find();
      if (obj != null)
      {
        Mobile player = World.Player;
        if (player == null || !obj.IsChildOf((Agent) player))
          obj = (Item) null;
      }
      return obj;
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      if (this.IsNull)
        return;
      if (this.serial != 0)
        op.SetInt32("serial", this.serial);
      if (this.itemId == 0)
        return;
      op.SetInt32("itemID", this.itemId);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.serial = ip.GetInt32("serial");
      this.itemId = ip.GetInt32("itemID");
    }
  }
}
