// Decompiled with JetBrains decompiler
// Type: PlayUO.TravelAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO
{
  public class TravelAgent : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("travel", Construct, RunebookInfo.TypeCode);
    private RunebookInfoCollection m_Runebooks;

    public override PersistableType TypeID
    {
      get
      {
        return TravelAgent.TypeCode;
      }
    }

    public RunebookInfoCollection Runebooks
    {
      get
      {
        return this.m_Runebooks;
      }
    }

    public RunebookInfo this[PlayUO.Item runebook]
    {
      get
      {
        for (int index = 0; index < this.m_Runebooks.Count; ++index)
        {
          RunebookInfo runebookInfo = this.m_Runebooks[index];
          if (runebookInfo.Serial == runebook.Serial)
            return runebookInfo;
        }
        RunebookInfo runebookInfo1;
        this.m_Runebooks.Add(runebookInfo1 = new RunebookInfo(runebook));
        return runebookInfo1;
      }
    }

    public TravelAgent()
    {
      this.m_Runebooks = new RunebookInfoCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new TravelAgent();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Runebooks.Count; ++index)
        this.m_Runebooks[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.m_Runebooks.Add(ip.GetChild() as RunebookInfo);
    }
  }
}
