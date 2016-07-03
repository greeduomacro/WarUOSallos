// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Player
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Player : Character
  {
    public new static readonly PersistableType TypeCode = new PersistableType("player", Construct, new PersistableType[6]{ Friends.TypeCode, TravelAgent.TypeCode, UseOnceAgent.TypeCode, EquipAgent.TypeCode, RestockAgent.TypeCode, OrganizeAgent.TypeCode });
    private static Server m_Server;
    private static Player m_Player;
    private string m_Profile;
    private Friends m_Friends;
    private UseOnceAgent m_UseOnceAgent;
    private EquipAgent equipAgent;

    public override PersistableType TypeID
    {
      get
      {
        return Player.TypeCode;
      }
    }

    public static Player Current
    {
      get
      {
        Mobile index1 = World.Player;
        string index2 = Engine.m_ServerName;
        if (index1 == null)
          index2 = (string) null;
        if (index2 == null)
          index1 = (Mobile) null;
        if (Player.m_Server != null && Player.m_Server.Name != index2)
        {
          Player.m_Server = (Server) null;
          Player.m_Player = (Player) null;
        }
        if (Player.m_Player != null && (index1 == null || Player.m_Player.Serial != index1.Serial))
          Player.m_Player = (Player) null;
        if (Player.m_Player == null && index1 != null)
        {
          Player.m_Server = Config.Current.Servers[index2];
          Player.m_Player = Player.m_Server[index1];
          foreach (Character character in Player.m_Player.Friends.Characters)
            World.WantMobile(character.Serial).m_IsFriend = true;
          foreach (Character character in Player.m_Server.IgnoreList.Characters)
            World.WantMobile(character.Serial).IsIgnored = true;
          foreach (ItemRef itemRef in Player.m_Player.UseOnceAgent.Items)
            World.WantItem(itemRef.Serial).OverrideHue(34);
        }
        return Player.m_Player;
      }
    }

    public Server Server
    {
      get
      {
        return Player.m_Server;
      }
    }

    public string Profile
    {
      get
      {
        return this.m_Profile;
      }
    }

    public Friends Friends
    {
      get
      {
        return this.m_Friends;
      }
    }

    public IgnoreList IgnoreList
    {
      get
      {
        return Player.m_Server.IgnoreList;
      }
    }

    public TravelAgent TravelAgent
    {
      get
      {
        return Player.m_Server.TravelAgent;
      }
    }

    public UseOnceAgent UseOnceAgent
    {
      get
      {
        return this.m_UseOnceAgent;
      }
    }

    public RestockAgent RestockAgent { get; private set; }

    public OrganizeAgent OrganizeAgent { get; private set; }

    public EquipAgent EquipAgent
    {
      get
      {
        return this.equipAgent;
      }
    }

    private Player()
    {
    }

    public Player(Mobile mob)
      : base(mob)
    {
      this.m_Profile = "Default";
      this.m_Friends = new Friends();
      this.m_UseOnceAgent = new UseOnceAgent();
      this.equipAgent = new EquipAgent();
      this.RestockAgent = new RestockAgent();
      this.OrganizeAgent = new OrganizeAgent();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Player();
    }

    protected override void SerializeAttributes(PersistanceWriter op)
    {
      base.SerializeAttributes(op);
      op.SetString("profile", this.m_Profile);
    }

    protected override void DeserializeAttributes(PersistanceReader ip)
    {
      base.DeserializeAttributes(ip);
      this.m_Profile = ip.GetString("profile");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.m_Friends.Serialize(op);
      this.m_UseOnceAgent.Serialize(op);
      this.equipAgent.Serialize(op);
      this.RestockAgent.Serialize(op);
      this.OrganizeAgent.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
      {
        object obj = (object) ip.GetChild();
        if (obj is Friends)
          this.m_Friends = obj as Friends;
        else if (!(obj is TravelAgent))
        {
          if (obj is UseOnceAgent)
            this.m_UseOnceAgent = obj as UseOnceAgent;
          else if (obj is EquipAgent)
            this.equipAgent = obj as EquipAgent;
          else if (obj is RestockAgent)
            this.RestockAgent = obj as RestockAgent;
          else if (obj is OrganizeAgent)
            this.OrganizeAgent = obj as OrganizeAgent;
        }
      }
      if (this.m_Friends == null)
        this.m_Friends = new Friends();
      if (this.m_UseOnceAgent == null)
        this.m_UseOnceAgent = new UseOnceAgent();
      if (this.equipAgent == null)
        this.equipAgent = new EquipAgent();
      if (this.RestockAgent == null)
        this.RestockAgent = new RestockAgent();
      if (this.OrganizeAgent != null)
        return;
      this.OrganizeAgent = new OrganizeAgent();
    }
  }
}
