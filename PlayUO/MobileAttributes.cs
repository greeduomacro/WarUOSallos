// Decompiled with JetBrains decompiler
// Type: PlayUO.MobileAttributes
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class MobileAttributes
  {
    public static MobileAttributes Default = new MobileAttributes();
    private MobileAttributes.LocalAttributes localAttributes;
    private int currentHitPoints;
    private int maximumHitPoints;
    private bool isControlable;

    public bool IsControlable
    {
      get
      {
        return this.isControlable;
      }
      set
      {
        this.isControlable = value;
      }
    }

    public int Gender
    {
      get
      {
        return this.QueryLocalAttributes().Gender;
      }
      set
      {
        this.EnsureLocalAttributes().Gender = value;
      }
    }

    public int Strength
    {
      get
      {
        return this.QueryLocalAttributes().Strength;
      }
      set
      {
        this.EnsureLocalAttributes().Strength = value;
      }
    }

    public int CurrentHitPoints
    {
      get
      {
        return this.currentHitPoints;
      }
      set
      {
        this.currentHitPoints = value;
      }
    }

    public int MaximumHitPoints
    {
      get
      {
        return this.maximumHitPoints;
      }
      set
      {
        this.maximumHitPoints = value;
      }
    }

    public int Dexterity
    {
      get
      {
        return this.QueryLocalAttributes().Dexterity;
      }
      set
      {
        this.EnsureLocalAttributes().Dexterity = value;
      }
    }

    public int CurrentStamina
    {
      get
      {
        return this.QueryLocalAttributes().CurrentStamina;
      }
      set
      {
        this.EnsureLocalAttributes().CurrentStamina = value;
      }
    }

    public int MaximumStamina
    {
      get
      {
        return this.QueryLocalAttributes().MaximumStamina;
      }
      set
      {
        this.EnsureLocalAttributes().MaximumStamina = value;
      }
    }

    public int Intelligence
    {
      get
      {
        return this.QueryLocalAttributes().Intelligence;
      }
      set
      {
        this.EnsureLocalAttributes().Intelligence = value;
      }
    }

    public int CurrentMana
    {
      get
      {
        return this.QueryLocalAttributes().CurrentMana;
      }
      set
      {
        this.EnsureLocalAttributes().CurrentMana = value;
      }
    }

    public int MaximumMana
    {
      get
      {
        return this.QueryLocalAttributes().MaximumMana;
      }
      set
      {
        this.EnsureLocalAttributes().MaximumMana = value;
      }
    }

    public int Armor
    {
      get
      {
        return this.QueryLocalAttributes().Armor;
      }
      set
      {
        this.EnsureLocalAttributes().Armor = value;
      }
    }

    public int Weight
    {
      get
      {
        return this.QueryLocalAttributes().Weight;
      }
      set
      {
        this.EnsureLocalAttributes().Weight = value;
      }
    }

    public int Gold
    {
      get
      {
        return this.QueryLocalAttributes().Gold;
      }
      set
      {
        this.EnsureLocalAttributes().Gold = value;
      }
    }

    public int StatCap
    {
      get
      {
        return this.QueryLocalAttributes().StatCap;
      }
      set
      {
        this.EnsureLocalAttributes().StatCap = value;
      }
    }

    public int CurrentFollowers
    {
      get
      {
        return this.QueryLocalAttributes().CurrentFollowers;
      }
      set
      {
        this.EnsureLocalAttributes().CurrentFollowers = value;
      }
    }

    public int MaximumFollowers
    {
      get
      {
        return this.QueryLocalAttributes().MaximumFollowers;
      }
      set
      {
        this.EnsureLocalAttributes().MaximumFollowers = value;
      }
    }

    public int Luck
    {
      get
      {
        return this.QueryLocalAttributes().Luck;
      }
      set
      {
        this.EnsureLocalAttributes().Luck = value;
      }
    }

    public int MinimumDamage
    {
      get
      {
        return this.QueryLocalAttributes().MinimumDamage;
      }
      set
      {
        this.EnsureLocalAttributes().MinimumDamage = value;
      }
    }

    public int MaximumDamage
    {
      get
      {
        return this.QueryLocalAttributes().MaximumDamage;
      }
      set
      {
        this.EnsureLocalAttributes().MaximumDamage = value;
      }
    }

    public int TithingPoints
    {
      get
      {
        return this.QueryLocalAttributes().TithingPoints;
      }
      set
      {
        this.EnsureLocalAttributes().TithingPoints = value;
      }
    }

    public int FireResist
    {
      get
      {
        return this.QueryLocalAttributes().FireResist;
      }
      set
      {
        this.EnsureLocalAttributes().FireResist = value;
      }
    }

    public int ColdResist
    {
      get
      {
        return this.QueryLocalAttributes().ColdResist;
      }
      set
      {
        this.EnsureLocalAttributes().ColdResist = value;
      }
    }

    public int PoisonResist
    {
      get
      {
        return this.QueryLocalAttributes().PoisonResist;
      }
      set
      {
        this.EnsureLocalAttributes().PoisonResist = value;
      }
    }

    public int EnergyResist
    {
      get
      {
        return this.QueryLocalAttributes().EnergyResist;
      }
      set
      {
        this.EnsureLocalAttributes().EnergyResist = value;
      }
    }

    private MobileAttributes.LocalAttributes QueryLocalAttributes()
    {
      return this.localAttributes ?? MobileAttributes.LocalAttributes.Default;
    }

    private MobileAttributes.LocalAttributes EnsureLocalAttributes()
    {
      if (this.localAttributes == null)
        this.localAttributes = new MobileAttributes.LocalAttributes();
      return this.localAttributes;
    }

    private sealed class LocalAttributes
    {
      public static MobileAttributes.LocalAttributes Default = new MobileAttributes.LocalAttributes();
      private int statCap = 225;
      private int maximumFollowers = 5;
      private MobileAttributes.LocalAttributes.AosAttributes aosAttributes;
      private int gender;
      private int strength;
      private int dexterity;
      private int intelligence;
      private int currentStamina;
      private int maximumStamina;
      private int currentMana;
      private int maximumMana;
      private int armor;
      private int weight;
      private int gold;
      private int currentFollowers;

      public int Gender
      {
        get
        {
          return this.gender;
        }
        set
        {
          this.gender = value;
        }
      }

      public int Strength
      {
        get
        {
          return this.strength;
        }
        set
        {
          this.strength = value;
        }
      }

      public int Dexterity
      {
        get
        {
          return this.dexterity;
        }
        set
        {
          this.dexterity = value;
        }
      }

      public int CurrentStamina
      {
        get
        {
          return this.currentStamina;
        }
        set
        {
          this.currentStamina = value;
        }
      }

      public int MaximumStamina
      {
        get
        {
          return this.maximumStamina;
        }
        set
        {
          this.maximumStamina = value;
        }
      }

      public int Intelligence
      {
        get
        {
          return this.intelligence;
        }
        set
        {
          this.intelligence = value;
        }
      }

      public int CurrentMana
      {
        get
        {
          return this.currentMana;
        }
        set
        {
          this.currentMana = value;
        }
      }

      public int MaximumMana
      {
        get
        {
          return this.maximumMana;
        }
        set
        {
          this.maximumMana = value;
        }
      }

      public int Armor
      {
        get
        {
          return this.armor;
        }
        set
        {
          this.armor = value;
        }
      }

      public int Weight
      {
        get
        {
          return this.weight;
        }
        set
        {
          this.weight = value;
        }
      }

      public int Gold
      {
        get
        {
          return this.gold;
        }
        set
        {
          this.gold = value;
        }
      }

      public int StatCap
      {
        get
        {
          return this.statCap;
        }
        set
        {
          this.statCap = value;
        }
      }

      public int CurrentFollowers
      {
        get
        {
          return this.currentFollowers;
        }
        set
        {
          this.currentFollowers = value;
        }
      }

      public int MaximumFollowers
      {
        get
        {
          return this.maximumFollowers;
        }
        set
        {
          this.maximumFollowers = value;
        }
      }

      public int Luck
      {
        get
        {
          return this.QueryAosAttributes().Luck;
        }
        set
        {
          this.EnsureAosAttributes().Luck = value;
        }
      }

      public int MinimumDamage
      {
        get
        {
          return this.QueryAosAttributes().MinimumDamage;
        }
        set
        {
          this.EnsureAosAttributes().MinimumDamage = value;
        }
      }

      public int MaximumDamage
      {
        get
        {
          return this.QueryAosAttributes().MaximumDamage;
        }
        set
        {
          this.EnsureAosAttributes().MaximumDamage = value;
        }
      }

      public int TithingPoints
      {
        get
        {
          return this.QueryAosAttributes().TithingPoints;
        }
        set
        {
          this.EnsureAosAttributes().TithingPoints = value;
        }
      }

      public int FireResist
      {
        get
        {
          return this.QueryAosAttributes().FireResist;
        }
        set
        {
          this.EnsureAosAttributes().FireResist = value;
        }
      }

      public int ColdResist
      {
        get
        {
          return this.QueryAosAttributes().ColdResist;
        }
        set
        {
          this.EnsureAosAttributes().ColdResist = value;
        }
      }

      public int PoisonResist
      {
        get
        {
          return this.QueryAosAttributes().PoisonResist;
        }
        set
        {
          this.EnsureAosAttributes().PoisonResist = value;
        }
      }

      public int EnergyResist
      {
        get
        {
          return this.QueryAosAttributes().EnergyResist;
        }
        set
        {
          this.EnsureAosAttributes().EnergyResist = value;
        }
      }

      private MobileAttributes.LocalAttributes.AosAttributes QueryAosAttributes()
      {
        return this.aosAttributes ?? MobileAttributes.LocalAttributes.AosAttributes.Default;
      }

      private MobileAttributes.LocalAttributes.AosAttributes EnsureAosAttributes()
      {
        if (this.aosAttributes == null)
          this.aosAttributes = new MobileAttributes.LocalAttributes.AosAttributes();
        return this.aosAttributes;
      }

      private sealed class AosAttributes
      {
        public static MobileAttributes.LocalAttributes.AosAttributes Default = new MobileAttributes.LocalAttributes.AosAttributes();
        private int luck;
        private int minimumDamage;
        private int maximumDamage;
        private int tithingPoints;
        private int fireResist;
        private int coldResist;
        private int poisonResist;
        private int energyResist;

        public int Luck
        {
          get
          {
            return this.luck;
          }
          set
          {
            this.luck = value;
          }
        }

        public int MinimumDamage
        {
          get
          {
            return this.minimumDamage;
          }
          set
          {
            this.minimumDamage = value;
          }
        }

        public int MaximumDamage
        {
          get
          {
            return this.maximumDamage;
          }
          set
          {
            this.maximumDamage = value;
          }
        }

        public int TithingPoints
        {
          get
          {
            return this.tithingPoints;
          }
          set
          {
            this.tithingPoints = value;
          }
        }

        public int FireResist
        {
          get
          {
            return this.fireResist;
          }
          set
          {
            this.fireResist = value;
          }
        }

        public int ColdResist
        {
          get
          {
            return this.coldResist;
          }
          set
          {
            this.coldResist = value;
          }
        }

        public int PoisonResist
        {
          get
          {
            return this.poisonResist;
          }
          set
          {
            this.poisonResist = value;
          }
        }

        public int EnergyResist
        {
          get
          {
            return this.energyResist;
          }
          set
          {
            this.energyResist = value;
          }
        }
      }
    }
  }
}
