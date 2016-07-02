// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Server
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Server : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("server", Construct, IgnoreList.TypeCode, TravelAgent.TypeCode, Player.TypeCode);
    private string m_Name;
    private IgnoreList m_IgnoreList;
    private TravelAgent m_TravelAgent;
    private PlayerCollection m_Players;

    public override PersistableType TypeID
    {
      get
      {
        return Server.TypeCode;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public IgnoreList IgnoreList
    {
      get
      {
        return this.m_IgnoreList;
      }
    }

    public TravelAgent TravelAgent
    {
      get
      {
        return this.m_TravelAgent;
      }
    }

    public Player this[Mobile mob]
    {
      get
      {
        for (int index = 0; index < this.m_Players.Count; ++index)
        {
          Player player = this.m_Players[index];
          if (player.Serial == mob.Serial)
            return player;
        }
        Player player1;
        this.m_Players.Add(player1 = new Player(mob));
        return player1;
      }
    }

    private Server()
    {
      this.m_Players = new PlayerCollection();
    }

    public Server(string name)
      : this()
    {
      this.m_Name = name;
      this.m_IgnoreList = new IgnoreList();
      this.m_TravelAgent = new TravelAgent();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Server();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetString("name", this.m_Name);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Name = ip.GetString("name");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.m_IgnoreList.Serialize(op);
      this.m_TravelAgent.Serialize(op);
      foreach (PersistableObject mPlayer in this.m_Players)
        mPlayer.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
      {
        object obj = (object) ip.GetChild();
        if (obj is Player)
          this.m_Players.Add(obj as Player);
        else if (obj is IgnoreList)
          this.m_IgnoreList = obj as IgnoreList;
        else if (obj is TravelAgent)
          this.m_TravelAgent = obj as TravelAgent;
      }
      if (this.m_IgnoreList == null)
        this.m_IgnoreList = new IgnoreList();
      if (this.m_TravelAgent != null)
        return;
      this.m_TravelAgent = new TravelAgent();
    }
  }
}
