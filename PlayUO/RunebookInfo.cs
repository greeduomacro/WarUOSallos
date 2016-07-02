// Decompiled with JetBrains decompiler
// Type: PlayUO.RunebookInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Sallos;

namespace PlayUO
{
  public class RunebookInfo : ItemRef
  {
    public new static readonly PersistableType TypeCode = new PersistableType("runebook", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ RuneInfo.TypeCode });
    private RuneInfoCollection m_Runes;

    public override PersistableType TypeID
    {
      get
      {
        return RunebookInfo.TypeCode;
      }
    }

    public RuneInfoCollection Runes
    {
      get
      {
        return this.m_Runes;
      }
    }

    public bool IsValid
    {
      get
      {
        Mobile player = World.Player;
        if (player != null)
        {
          Item obj = this.Find();
          if (obj != null)
            return obj.InRange((IPoint2D) player, 1);
        }
        return false;
      }
    }

    private RunebookInfo()
      : this((Item) null)
    {
    }

    public RunebookInfo(Item item)
      : base(item)
    {
      this.m_Runes = new RuneInfoCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new RunebookInfo();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Runes.Count; ++index)
        this.m_Runes[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
        this.m_Runes.Add(ip.GetChild() as RuneInfo);
    }
  }
}
