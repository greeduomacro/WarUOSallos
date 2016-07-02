// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.NotorietyHues
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class NotorietyHues : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("notoHues", new ConstructCallback((object) null, __methodptr(Construct)));
    private int[] m_Hues;

    public virtual PersistableType TypeID
    {
      get
      {
        return NotorietyHues.TypeCode;
      }
    }

    [OptionHue]
    [Optionable("Innocent", "Notoriety Hues", Default = 89)]
    public int Innocent
    {
      get
      {
        return this[Notoriety.Innocent];
      }
      set
      {
        this[Notoriety.Innocent] = value;
      }
    }

    [OptionHue]
    [Optionable("Ally", "Notoriety Hues", Default = 63)]
    public int Ally
    {
      get
      {
        return this[Notoriety.Ally];
      }
      set
      {
        this[Notoriety.Ally] = value;
      }
    }

    [Optionable("Attackable", "Notoriety Hues", Default = 1303)]
    [OptionHue]
    public int Attackable
    {
      get
      {
        return this[Notoriety.Attackable];
      }
      set
      {
        this[Notoriety.Attackable] = value;
      }
    }

    [OptionHue]
    [Optionable("Criminal", "Notoriety Hues", Default = 946)]
    public int Criminal
    {
      get
      {
        return this[Notoriety.Criminal];
      }
      set
      {
        this[Notoriety.Criminal] = value;
      }
    }

    [Optionable("Enemy", "Notoriety Hues", Default = 144)]
    [OptionHue]
    public int Enemy
    {
      get
      {
        return this[Notoriety.Enemy];
      }
      set
      {
        this[Notoriety.Enemy] = value;
      }
    }

    [Optionable("Murderer", "Notoriety Hues", Default = 34)]
    [OptionHue]
    public int Murderer
    {
      get
      {
        return this[Notoriety.Murderer];
      }
      set
      {
        this[Notoriety.Murderer] = value;
      }
    }

    [OptionHue]
    [Optionable("Vendor", "Notoriety Hues", Default = 53)]
    public int Vendor
    {
      get
      {
        return this[Notoriety.Vendor];
      }
      set
      {
        this[Notoriety.Vendor] = value;
      }
    }

    public int this[Notoriety notoriety]
    {
      get
      {
        return this.m_Hues[(int) (notoriety - (byte) 1)];
      }
      set
      {
        this.m_Hues[(int) (notoriety - (byte) 1)] = value;
      }
    }

    public NotorietyHues()
    {
      base.\u002Ector();
      this.m_Hues = new int[7]
      {
        89,
        63,
        1303,
        946,
        144,
        34,
        53
      };
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new NotorietyHues();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("innocent", this.Innocent);
      op.SetInt32("ally", this.Ally);
      op.SetInt32("attackable", this.Attackable);
      op.SetInt32("criminal", this.Criminal);
      op.SetInt32("enemy", this.Enemy);
      op.SetInt32("murderer", this.Murderer);
      op.SetInt32("vendor", this.Vendor);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.Innocent = ip.GetInt32("innocent");
      this.Ally = ip.GetInt32("ally");
      this.Attackable = ip.GetInt32("attackable");
      this.Criminal = ip.GetInt32("criminal");
      this.Enemy = ip.GetInt32("enemy");
      this.Murderer = ip.GetInt32("murderer");
      this.Vendor = ip.GetInt32("vendor");
    }
  }
}
