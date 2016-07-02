// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Profile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Profile : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("profile", Construct, Preferences.TypeCode);
    private static Player m_Player;
    private static Profile m_Current;
    private string m_Name;
    private Preferences m_Preferences;

    public override PersistableType TypeID
    {
      get
      {
        return Profile.TypeCode;
      }
    }

    public static Profile Current
    {
      get
      {
        Player current = Player.Current;
        if (Profile.m_Current == null || current != Profile.m_Player)
        {
          Profile.m_Player = current;
          Profile.m_Current = current == null ? Config.Current.Profiles["Default"] : Config.Current.Profiles[current.Profile];
        }
        return Profile.m_Current;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public Preferences Preferences
    {
      get
      {
        return this.m_Preferences;
      }
    }

    private Profile()
    {
    }

    public Profile(string name)
    {
      this.m_Name = name;
      this.m_Preferences = new Preferences();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Profile();
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
      this.m_Preferences.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
      {
        object obj = (object) ip.GetChild();
        if (obj is Preferences)
          this.m_Preferences = obj as Preferences;
      }
    }
  }
}
