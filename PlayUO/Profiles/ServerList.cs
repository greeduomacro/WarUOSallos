// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.ServerList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class ServerList : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("servers", new ConstructCallback((object) null, _methodptr(Construct)), new PersistableType[1]{ Server.TypeCode });
    private ServerCollection m_Servers;

    public virtual PersistableType TypeID
    {
      get
      {
        return ServerList.TypeCode;
      }
    }

    public Server this[string name]
    {
      get
      {
        for (int index = 0; index < this.m_Servers.Count; ++index)
        {
          Server server = this.m_Servers[index];
          if (server.Name == name)
            return server;
        }
        Server server1;
        this.m_Servers.Add(server1 = new Server(name));
        return server1;
      }
    }

    public ServerList()
    {
      base.\u002Ector();
      this.m_Servers = new ServerCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ServerList();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Servers.Count; ++index)
        this.m_Servers[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.m_Servers.Add(ip.GetChild() as Server);
    }
  }
}
