// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.ProfileList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class ProfileList : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("profiles", Construct, new PersistableType[1]{ Profile.TypeCode });
    private ProfileCollection m_Profiles;

    public override PersistableType TypeID
    {
      get
      {
        return ProfileList.TypeCode;
      }
    }

    public Profile this[string name]
    {
      get
      {
        for (int index = 0; index < this.m_Profiles.Count; ++index)
        {
          Profile profile = this.m_Profiles[index];
          if (profile.Name == name)
            return profile;
        }
        Profile profile1;
        this.m_Profiles.Add(profile1 = new Profile(name));
        return profile1;
      }
    }

    public ProfileList()
    {
      this.m_Profiles = new ProfileCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ProfileList();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Profiles.Count; ++index)
        this.m_Profiles[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.m_Profiles.Add(ip.GetChild() as Profile);
    }
  }
}
