// Decompiled with JetBrains decompiler
// Type: PlayUO.UseOnceAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Sallos;

namespace PlayUO
{
  public class UseOnceAgent : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("useOnce", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ ItemRef.TypeCode });
    private ItemRefCollection m_Items;
    private int m_Index;

    public virtual PersistableType TypeID
    {
      get
      {
        return UseOnceAgent.TypeCode;
      }
    }

    public ItemRefCollection Items
    {
      get
      {
        return this.m_Items;
      }
    }

    public int Index
    {
      get
      {
        return this.m_Index;
      }
      set
      {
        this.m_Index = value;
      }
    }

    public ItemRef this[PlayUO.Item item]
    {
      get
      {
        if (item != null)
        {
          foreach (ItemRef mItem in this.m_Items)
          {
            if (mItem.Serial == item.Serial)
              return mItem;
          }
        }
        return (ItemRef) null;
      }
    }

    public UseOnceAgent()
    {
      base.\u002Ector();
      this.m_Items = new ItemRefCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new UseOnceAgent();
    }

    public void Validate()
    {
      for (int index = 0; index < this.m_Items.Count; ++index)
      {
        if (this.m_Items[index].FindOnPlayer() == null)
          this.m_Items.RemoveAt(index--);
      }
    }

    public void Use()
    {
      if (World.Player == null)
        return;
      for (int index = 0; index < this.m_Items.Count; ++index)
      {
        PlayUO.Item onPlayer = this.m_Items[(this.m_Index + index) % this.m_Items.Count].FindOnPlayer();
        if (onPlayer != null)
        {
          this.m_Index += index;
          onPlayer.Use();
          ++this.m_Index;
          this.m_Index %= this.m_Items.Count;
          return;
        }
      }
      if (this.m_Items.Count == 0)
        Engine.AddTextMessage("There are no items in your use-once list.");
      else
        Engine.AddTextMessage("No use-once items were found on your person.");
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("index", this.m_Index);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Index = ip.GetInt32("index");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Items.Count; ++index)
        this.m_Items[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
        this.m_Items.Add(ip.GetChild() as ItemRef);
    }
  }
}
