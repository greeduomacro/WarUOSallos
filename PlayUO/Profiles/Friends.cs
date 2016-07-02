// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Friends
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Friends : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("friends", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ Character.TypeCode });
    private CharacterCollection m_Characters;

    public virtual PersistableType TypeID
    {
      get
      {
        return Friends.TypeCode;
      }
    }

    public CharacterCollection Characters
    {
      get
      {
        return this.m_Characters;
      }
    }

    public Character this[Mobile mob]
    {
      get
      {
        if (mob != null)
        {
          foreach (Character mCharacter in this.m_Characters)
          {
            if (mCharacter.Serial == mob.Serial)
              return mCharacter;
          }
        }
        return (Character) null;
      }
    }

    public Friends()
    {
      base.\u002Ector();
      this.m_Characters = new CharacterCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Friends();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Characters.Count; ++index)
        this.m_Characters[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
        this.m_Characters.Add(ip.GetChild() as Character);
    }
  }
}
