// Decompiled with JetBrains decompiler
// Type: PlayUO.MacroConfig
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO
{
  public class MacroConfig : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("macroConfig", (ConstructCallback) null, MacroSet.TypeCode);
    private MacroSetCollection macroSets;

    public override PersistableType TypeID
    {
      get
      {
        return MacroConfig.TypeCode;
      }
    }

    public MacroSetCollection MacroSets
    {
      get
      {
        return this.macroSets;
      }
    }

    public MacroSet this[int serial, int server]
    {
      get
      {
        foreach (MacroSet macroSet in this.macroSets)
        {
          if (macroSet.Serial == serial && macroSet.Server == server)
            return macroSet;
        }
        return (MacroSet) null;
      }
    }

    public MacroConfig()
    {
      this.macroSets = new MacroSetCollection();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.macroSets.Count; ++index)
        this.macroSets[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.macroSets.Add(ip.GetChild() as MacroSet);
    }
  }
}
