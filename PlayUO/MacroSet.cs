// Decompiled with JetBrains decompiler
// Type: PlayUO.MacroSet
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO
{
  public class MacroSet : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("macroSet", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ MacroData.TypeCode });
    private int serial;
    private int server;
    private MacroCollection macros;

    public virtual PersistableType TypeID
    {
      get
      {
        return MacroSet.TypeCode;
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

    public int Server
    {
      get
      {
        return this.server;
      }
      set
      {
        this.server = value;
      }
    }

    public MacroCollection Macros
    {
      get
      {
        return this.macros;
      }
    }

    public MacroSet()
    {
      base.\u002Ector();
      this.macros = new MacroCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new MacroSet();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("serial", this.serial);
      op.SetInt32("server", this.server);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.serial = ip.GetInt32("serial");
      this.server = ip.GetInt32("server");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.macros.Count; ++index)
        this.macros[index].Data.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
        this.macros.Add(new Macro(ip.GetChild() as MacroData));
    }
  }
}
