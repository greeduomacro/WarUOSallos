// Decompiled with JetBrains decompiler
// Type: PlayUO.IgnoreList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using PlayUO.Profiles;
using Sallos;

namespace PlayUO
{
  public class IgnoreList : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("ignore",  Construct, Character.TypeCode);
    private CharacterCollection m_Characters;

    public override PersistableType TypeID
    {
      get
      {
        return IgnoreList.TypeCode;
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

    public IgnoreList()
    {
      this.m_Characters = new CharacterCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new IgnoreList();
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      foreach (PersistableObject mCharacter in this.m_Characters)
        mCharacter.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.m_Characters.Add(ip.GetChild() as Character);
    }
  }
}
