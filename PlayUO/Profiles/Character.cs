// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Character
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Character : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("character", new ConstructCallback((object) null, __methodptr(Construct)));
    private int m_Serial;
    private string m_Name;

    public virtual PersistableType TypeID
    {
      get
      {
        return Character.TypeCode;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
      set
      {
        this.m_Name = value;
      }
    }

    public bool IsNull
    {
      get
      {
        if (this.m_Name == null)
          return this.m_Serial == 0;
        return false;
      }
    }

    protected Character()
    {
      base.\u002Ector();
    }

    public Character(Mobile mob)
    {
      base.\u002Ector();
      if (mob == null)
        return;
      this.m_Name = mob.Name;
      this.m_Serial = mob.Serial;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Character();
    }

    public Mobile Find()
    {
      return World.FindMobile(this.m_Serial);
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      if (this.IsNull)
        return;
      op.SetString("name", this.m_Name);
      op.SetInt32("serial", this.m_Serial);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Name = ip.GetString("name");
      this.m_Serial = ip.GetInt32("serial");
    }
  }
}
