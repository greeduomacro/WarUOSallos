// Decompiled with JetBrains decompiler
// Type: PlayUO.AgentCell
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public abstract class AgentCell : IAgentView
  {
    private PhysicalAgent _agent;
    private ArrayList _owner;

    public PhysicalAgent Agent
    {
      get
      {
        return this._agent;
      }
    }

    public ArrayList Owner
    {
      get
      {
        return this._owner;
      }
      set
      {
        this._owner = value;
      }
    }

    public AgentCell(PhysicalAgent agent)
    {
      this._agent = agent;
    }

    public abstract void Update();

    public void OnAgentUpdated()
    {
      Map.Update(this._agent);
    }

    public void OnAgentDeleted()
    {
      Map.Update(this._agent);
    }
  }
}
