// Decompiled with JetBrains decompiler
// Type: PlayUO.Mobile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayUO
{
  public class Mobile : PhysicalAgent, IMessageOwner, IPoint2D, IAnimationOwner, IRadarTrackable
  {
    private static string[] m_NotorietyStrings = new string[7]{ "You are now innocent.", "You are now innocent.", "You may now be attacked freely, but are not a criminal.", "You are now a criminal.", "You are now innocent.", "You are now a murderer.", "You are now invulnerable." };
    private static Layer[] m_DisturbLayers = new Layer[6]{ Layer.Ring, Layer.Bracelet, Layer.Earrings, Layer.Neck, Layer.Gloves, Layer.OuterLegs };
    private string m_Name = "";
    private int m_LastFrame = -12345;
    private int m_PaperdollX = int.MaxValue;
    private int m_PaperdollY = int.MaxValue;
    private byte greenHealthLevel;
    private byte yellowHealthLevel;
    private byte redHealthLevel;
    private short m_XReal;
    private short m_YReal;
    private short m_ZReal;
    private byte m_DReal;
    public DateTime m_LastDamage;
    private bool m_OpenedStatus;
    private string guild;
    private Faction faction;
    private string guessedName;
    private ushort m_Body;
    private byte m_Direction;
    private ushort m_Hue;
    private MobileFlags m_Flags;
    private bool m_IsMoving;
    private int m_MovedTiles;
    private int m_LastWalk;
    private bool m_Human;
    private bool m_Ghost;
    private bool m_HumanOrGhost;
    private Queue m_Walking;
    private int m_CorpseSerial;
    private Notoriety m_Notoriety;
    private IMobileStatus m_StatusBar;
    private Animation m_Animation;
    private bool m_Refresh;
    private bool m_BigStatus;
    private int m_MessageFrame;
    private int m_MessageX;
    private int m_MessageY;
    private int m_ScreenX;
    private int m_ScreenY;
    private AnimationVertexCache m_vCache;
    private IHue m_hAnimationPool;
    private int m_iAnimationPool;
    private Frames m_fAnimationPool;
    private bool _isDeadPet;
    private int m_LightLevel;
    private int m_Props;
    private bool _meditating;
    public int m_KUOC_X;
    public int m_KUOC_Y;
    public int m_KUOC_Z;
    public int m_KUOC_F;
    private DateTime m_NextQueryProps;
    private int m_Sounds;
    private int m_HorseFootsteps;
    private bool m_IsIgnored;
    private List<Item> _sortedItems;
    private LayerComparer _sortComparer;
    public bool m_IsFriend;
    public string lastSpell;
    private DateTime m_LastDisturb;
    private DateTime _radarExpirationTime;
    private MobileAttributes attributes;

    public int AnimationId
    {
      get
      {
        return (int) this.m_Body;
      }
    }

    public int Speed
    {
      get
      {
        int num = 1;
        if (this.IsMounted)
          num *= 2;
        if ((int) this.Body == 987)
          num *= 4;
        return num;
      }
    }

    public bool HasName
    {
      get
      {
        if (this.m_Name != null)
          return this.m_Name.Length > 0;
        return false;
      }
    }

    public bool IsInParty
    {
      get
      {
        if (Party.State != PartyState.Joined)
          return false;
        return Array.IndexOf<Mobile>(Party.Members, this) >= 0;
      }
    }

    public bool IsFriend
    {
      get
      {
        return this.m_IsFriend;
      }
    }

    public bool IsDead
    {
      get
      {
        if (!this.Ghost)
          return this.IsDeadPet;
        return true;
      }
    }

    public bool IsMounted
    {
      get
      {
        return this.FindEquip(Layer.Mount) != null;
      }
    }

    public bool IsGuarded
    {
      get
      {
        return World.Viewport.IsGuarded(Engine.m_World, this.X, this.Y);
      }
    }

    public bool IsPoisoned
    {
      get
      {
        if (!this.m_Flags[MobileFlag.Poisoned])
          return (int) this.greenHealthLevel != 0;
        return true;
      }
    }

    public bool IsInvulnerable
    {
      get
      {
        if (!this.m_Flags[MobileFlag.YellowHits])
          return (int) this.yellowHealthLevel != 0;
        return true;
      }
    }

    public short XReal
    {
      get
      {
        return this.m_XReal;
      }
      set
      {
        this.m_XReal = value;
      }
    }

    public short YReal
    {
      get
      {
        return this.m_YReal;
      }
      set
      {
        this.m_YReal = value;
      }
    }

    public short ZReal
    {
      get
      {
        return this.m_ZReal;
      }
      set
      {
        this.m_ZReal = value;
      }
    }

    public byte DReal
    {
      get
      {
        return this.m_DReal;
      }
      set
      {
        this.m_DReal = value;
      }
    }

    public string Guild
    {
      get
      {
        return this.guild;
      }
      set
      {
        if (value != null && value.Length == 0)
          value = (string) null;
        this.guild = value;
        if (this.guild == null)
          return;
        if (Party.CheckAutomatedAccept())
        {
          Network.Send((Packet) new PParty_Accept(Party.Leader));
        }
        else
        {
          if (this.Player || !Party.CheckAutomatedInvite(this) || TargetManager.Server != null)
            return;
          new Mobile.PartyAction(this).Enqueue();
        }
      }
    }

    public Faction Faction
    {
      get
      {
        return this.faction;
      }
      set
      {
        this.faction = value;
      }
    }

    public string GuessedName
    {
      get
      {
        return this.guessedName;
      }
      set
      {
        this.guessedName = value;
      }
    }

    public string Identifier
    {
      get
      {
        string str = this.m_Name;
        if (string.IsNullOrEmpty(str))
        {
          str = this.guessedName;
          if (string.IsNullOrEmpty(str))
            return (string) null;
        }
        if (this.guild != null && this.faction != null)
          return string.Format("{0} [{1}, {2}]", (object) str, (object) this.guild, (object) this.faction.Abbreviation);
        if (this.guild != null)
          return string.Format("{0} [{1}]", (object) str, (object) this.guild);
        if (this.faction != null)
          return string.Format("{0} [{1}]", (object) str, (object) this.faction.Abbreviation);
        return str;
      }
    }

    public int LastSeen { get; set; }

    public bool Meditating
    {
      get
      {
        return this._meditating;
      }
      set
      {
        this._meditating = true;
      }
    }

    public int PropertyID
    {
      get
      {
        return this.m_Props;
      }
      set
      {
        if (this.m_Props != value)
          this.m_NextQueryProps = DateTime.Now;
        this.m_Props = value;
      }
    }

    public ObjectPropertyList PropertyList
    {
      get
      {
        return ObjectPropertyList.Find(this.Serial, this.m_Props);
      }
    }

    public int LightLevel
    {
      get
      {
        if (Preferences.Current.Options.AlwaysLight)
          return 100;
        return this.m_LightLevel;
      }
      set
      {
        this.m_LightLevel = value;
      }
    }

    public bool IsDeadPet
    {
      get
      {
        return this._isDeadPet;
      }
      set
      {
        this._isDeadPet = value;
      }
    }

    public int ScreenX
    {
      get
      {
        return this.m_ScreenX;
      }
      set
      {
        this.m_ScreenX = value;
      }
    }

    public int ScreenY
    {
      get
      {
        return this.m_ScreenY;
      }
      set
      {
        this.m_ScreenY = value;
      }
    }

    public bool IsIgnored
    {
      get
      {
        return this.m_IsIgnored;
      }
      set
      {
        this.m_IsIgnored = value;
      }
    }

    public int MessageFrame
    {
      get
      {
        return this.m_MessageFrame;
      }
      set
      {
        this.m_MessageFrame = value;
      }
    }

    public int MessageX
    {
      get
      {
        return this.m_MessageX;
      }
      set
      {
        this.m_MessageX = value;
      }
    }

    public int MessageY
    {
      get
      {
        return this.m_MessageY;
      }
      set
      {
        this.m_MessageY = value;
      }
    }

    public int PaperdollX
    {
      get
      {
        return this.m_PaperdollX;
      }
      set
      {
        this.m_PaperdollX = value;
      }
    }

    public int PaperdollY
    {
      get
      {
        return this.m_PaperdollY;
      }
      set
      {
        this.m_PaperdollY = value;
      }
    }

    public bool Warmode
    {
      get
      {
        if (this.m_Flags[MobileFlag.Warmode])
          return !this.Ghost;
        return false;
      }
    }

    public Item MountItem
    {
      get
      {
        return this.FindEquip(Layer.Mount);
      }
    }

    public Item Backpack
    {
      get
      {
        return this.FindEquip(Layer.Backpack);
      }
    }

    public int HorseFootsteps
    {
      get
      {
        return this.m_HorseFootsteps;
      }
      set
      {
        this.m_HorseFootsteps = value;
      }
    }

    public int Sounds
    {
      get
      {
        return this.m_Sounds;
      }
      set
      {
        this.m_Sounds = value;
      }
    }

    public int LastFrame
    {
      get
      {
        return this.m_LastFrame;
      }
      set
      {
        this.m_LastFrame = value;
      }
    }

    public bool Player
    {
      get
      {
        return this.Serial == World.Serial;
      }
    }

    public bool OpenedStatus
    {
      get
      {
        return this.m_OpenedStatus;
      }
      set
      {
        this.m_OpenedStatus = value;
      }
    }

    public bool BigStatus
    {
      get
      {
        return this.m_BigStatus;
      }
      set
      {
        this.m_BigStatus = value;
      }
    }

    public Queue Walking
    {
      get
      {
        return this.m_Walking;
      }
    }

    public int CorpseSerial
    {
      get
      {
        return this.m_CorpseSerial;
      }
      set
      {
        this.m_CorpseSerial = value;
      }
    }

    public Animation Animation
    {
      get
      {
        return this.m_Animation;
      }
      set
      {
        if (this.m_Animation == value)
          return;
        if (this.m_Animation != null && this.m_Animation.OnAnimationEnd != null)
          this.m_Animation.OnAnimationEnd(this.m_Animation, this);
        this.m_Animation = value;
      }
    }

    public Notoriety Notoriety
    {
      get
      {
        return this.m_Notoriety;
      }
      set
      {
        if (this.m_Notoriety == value)
          return;
        this.m_Notoriety = value;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnNotorietyChange(value);
        if (!this.Player || !Engine.m_Ingame || (this.m_Notoriety < Notoriety.Innocent || this.m_Notoriety > Notoriety.Vendor))
          return;
        Engine.AddTextMessage(Mobile.m_NotorietyStrings[(int) (this.m_Notoriety - (byte) 1)], Engine.DefaultFont, Hues.GetNotoriety(this.m_Notoriety));
      }
    }

    public int NotorietyPriority
    {
      get
      {
        Mobile player = World.Player;
        bool flag = player != null && player.Notoriety == Notoriety.Murderer;
        switch (this.m_Notoriety)
        {
          case Notoriety.Innocent:
            return !flag ? 10 : 90;
          case Notoriety.Attackable:
            return 95;
          case Notoriety.Criminal:
            return 50;
          case Notoriety.Enemy:
            return 100;
          case Notoriety.Murderer:
            return !flag ? 90 : 10;
          default:
            return 0;
        }
      }
    }

    public IMobileStatus StatusBar
    {
      get
      {
        return this.m_StatusBar;
      }
      set
      {
        this.m_StatusBar = value;
      }
    }

    public GPaperdoll Paperdoll
    {
      get
      {
        return (GPaperdoll) this.ContainerView;
      }
    }

    public MobileFlags Flags
    {
      get
      {
        return this.m_Flags;
      }
      set
      {
        this.InternalOnFlagsChanged(value);
      }
    }

    public bool IsMoving
    {
      get
      {
        return this.m_IsMoving;
      }
      set
      {
        this.m_IsMoving = value;
      }
    }

    public int MovedTiles
    {
      get
      {
        return this.m_MovedTiles;
      }
      set
      {
        this.m_MovedTiles = value;
      }
    }

    public int LastWalk
    {
      get
      {
        return this.m_LastWalk;
      }
      set
      {
        this.m_LastWalk = value;
      }
    }

    public byte Direction
    {
      get
      {
        return this.m_Direction;
      }
      set
      {
        this.m_Direction = value;
      }
    }

    public ushort Body
    {
      get
      {
        return this.m_Body;
      }
      set
      {
        if ((int) this.m_Body == (int) value)
          return;
        this.m_Body = value;
        int bodyID = (int) this.m_Body;
        Engine.m_Animations.Translate(ref bodyID);
        this.m_Human = bodyID == 400 || bodyID == 401 || (bodyID == 991 || bodyID == 987) || bodyID == 990;
        this.m_Ghost = bodyID == 402 || bodyID == 403;
        this.m_HumanOrGhost = this.m_Human || this.m_Ghost;
      }
    }

    public ushort Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if ((int) this.m_Hue == (int) value)
          return;
        this.m_Hue = value;
        this.RaiseUpdateEvents();
      }
    }

    public bool HumanOrGhost
    {
      get
      {
        if (this.m_HumanOrGhost)
          return true;
        if (this.Player)
          return Renderer.m_DeathOverride;
        return false;
      }
    }

    public bool Ghost
    {
      get
      {
        if (this.m_Ghost)
          return true;
        if (this.Player)
          return Renderer.m_DeathOverride;
        return false;
      }
    }

    public bool Human
    {
      get
      {
        return this.m_Human;
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
        if (!(this.m_Name != value))
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnNameChange(value);
        this.m_Name = value;
        if (!this.Player)
          return;
        string str1 = this.m_Name;
        string str2;
        string str3;
        if (str1 == null || (str2 = str1.Trim()).Length <= 0)
        {
          str3 = "Ultima Online";
        }
        else
        {
          str3 = "Ultima Online - " + str2;
          PlayUO.Profiles.Player.Current.Name = str2;
        }
        Engine.m_Display.Text = str3;
      }
    }

    public bool Refresh
    {
      get
      {
        return this.m_Refresh;
      }
      set
      {
        if (this.m_Refresh && !value && this.m_StatusBar != null)
          this.m_StatusBar.OnRefresh();
        this.m_Refresh = value;
      }
    }

    int IRadarTrackable.X
    {
      get
      {
        if (!this.Player && !this.Visible)
          return this.m_KUOC_X;
        return this.X;
      }
    }

    int IRadarTrackable.Y
    {
      get
      {
        if (!this.Player && !this.Visible)
          return this.m_KUOC_Y;
        return this.Y;
      }
    }

    string IRadarTrackable.Name
    {
      get
      {
        if (!this.Player && !this.IsInParty)
          return (string) null;
        return this.Name;
      }
    }

    int IRadarTrackable.Facet
    {
      get
      {
        if (!this.Player && !this.Visible)
          return this.m_KUOC_F;
        return Engine.m_World;
      }
    }

    int IRadarTrackable.Color
    {
      get
      {
        if (this.Player)
          return 16777215;
        return !this.IsInParty ? 16737843 : 3407718;
      }
    }

    bool IRadarTrackable.HasExpired
    {
      get
      {
        if (this.IsInParty)
          return false;
        if (!this.Player && !this.Visible)
          return DateTime.UtcNow >= this._radarExpirationTime;
        this.UpdateRadarExpiration();
        return false;
      }
    }

    public int BodyGender
    {
      get
      {
        switch (this.m_Body)
        {
          case 400:
          case 402:
            return 0;
          case 401:
          case 403:
            return 1;
          default:
            return this.Gender;
        }
      }
    }

    public int Gender
    {
      get
      {
        return this.QueryAttributes().Gender;
      }
      set
      {
        if (this.Gender == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnGenderChange(value);
        this.EnsureAttributes().Gender = value;
      }
    }

    public bool IsPet
    {
      get
      {
        return this.QueryAttributes().IsControlable;
      }
      set
      {
        this.EnsureAttributes().IsControlable = value;
      }
    }

    public int Strength
    {
      get
      {
        return this.QueryAttributes().Strength;
      }
      set
      {
        int strength = this.Strength;
        if (strength == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnStrChange(value);
        if (this.Player && Engine.m_Ingame && strength != 0)
          this.StatChange("strength", strength, value);
        this.EnsureAttributes().Strength = value;
      }
    }

    public int CurrentHitPoints
    {
      get
      {
        return this.QueryAttributes().CurrentHitPoints;
      }
      set
      {
        if (this.CurrentHitPoints == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnHPCurChange(value);
        this.EnsureAttributes().CurrentHitPoints = value;
      }
    }

    public int MaximumHitPoints
    {
      get
      {
        return this.QueryAttributes().MaximumHitPoints;
      }
      set
      {
        if (this.MaximumHitPoints == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnHPMaxChange(value);
        this.EnsureAttributes().MaximumHitPoints = value;
      }
    }

    public int Dexterity
    {
      get
      {
        return this.QueryAttributes().Dexterity;
      }
      set
      {
        int dexterity = this.Dexterity;
        if (dexterity == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnDexChange(value);
        if (this.Player && Engine.m_Ingame && dexterity != 0)
          this.StatChange("dexterity", dexterity, value);
        this.EnsureAttributes().Dexterity = value;
      }
    }

    public int CurrentStamina
    {
      get
      {
        return this.QueryAttributes().CurrentStamina;
      }
      set
      {
        if (this.CurrentStamina == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnStamCurChange(value);
        this.EnsureAttributes().CurrentStamina = value;
      }
    }

    public int MaximumStamina
    {
      get
      {
        return this.QueryAttributes().MaximumStamina;
      }
      set
      {
        if (this.MaximumStamina == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnStamMaxChange(value);
        this.EnsureAttributes().MaximumStamina = value;
      }
    }

    public int Intelligence
    {
      get
      {
        return this.QueryAttributes().Intelligence;
      }
      set
      {
        int intelligence = this.Intelligence;
        if (intelligence == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnIntChange(value);
        if (this.Player && Engine.m_Ingame && intelligence != 0)
          this.StatChange("intelligence", intelligence, value);
        this.EnsureAttributes().Intelligence = value;
      }
    }

    public int CurrentMana
    {
      get
      {
        return this.QueryAttributes().CurrentMana;
      }
      set
      {
        if (this.CurrentMana == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnManaCurChange(value);
        this.EnsureAttributes().CurrentMana = value;
      }
    }

    public int MaximumMana
    {
      get
      {
        return this.QueryAttributes().MaximumMana;
      }
      set
      {
        if (this.MaximumMana == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnManaMaxChange(value);
        this.EnsureAttributes().MaximumMana = value;
      }
    }

    public int StatCap
    {
      get
      {
        return this.QueryAttributes().StatCap;
      }
      set
      {
        if (this.StatCap == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnStatCapChange(value);
        this.EnsureAttributes().StatCap = value;
      }
    }

    public int FollowersCur
    {
      get
      {
        return this.QueryAttributes().CurrentFollowers;
      }
      set
      {
        if (this.FollowersCur == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnFollCurChange(value);
        this.EnsureAttributes().CurrentFollowers = value;
      }
    }

    public int FollowersMax
    {
      get
      {
        return this.QueryAttributes().MaximumFollowers;
      }
      set
      {
        if (this.FollowersMax == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnFollMaxChange(value);
        this.EnsureAttributes().MaximumFollowers = value;
      }
    }

    public int Armor
    {
      get
      {
        return this.QueryAttributes().Armor;
      }
      set
      {
        if (this.Armor == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnArmorChange(value);
        this.EnsureAttributes().Armor = value;
      }
    }

    public int Weight
    {
      get
      {
        return this.QueryAttributes().Weight;
      }
      set
      {
        if (this.Weight == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnWeightChange(value);
        this.EnsureAttributes().Weight = value;
      }
    }

    public int Gold
    {
      get
      {
        return this.QueryAttributes().Gold;
      }
      set
      {
        if (this.Gold == value)
          return;
        if (!this.m_Refresh && this.m_StatusBar != null)
          this.m_StatusBar.OnGoldChange(value);
        this.EnsureAttributes().Gold = value;
      }
    }

    public int Luck
    {
      get
      {
        return this.QueryAttributes().Luck;
      }
      set
      {
        if (this.Luck == value)
          return;
        this.EnsureAttributes().Luck = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnLuckChange();
      }
    }

    public int TithingPoints
    {
      get
      {
        return this.QueryAttributes().TithingPoints;
      }
      set
      {
        if (this.TithingPoints == value)
          return;
        this.EnsureAttributes().TithingPoints = value;
      }
    }

    public int DamageMin
    {
      get
      {
        return this.QueryAttributes().MinimumDamage;
      }
      set
      {
        if (this.DamageMin == value)
          return;
        this.EnsureAttributes().MinimumDamage = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnDamageChange();
      }
    }

    public int DamageMax
    {
      get
      {
        return this.QueryAttributes().MaximumDamage;
      }
      set
      {
        if (this.DamageMax == value)
          return;
        this.EnsureAttributes().MaximumDamage = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnDamageChange();
      }
    }

    public int FireResist
    {
      get
      {
        return this.QueryAttributes().FireResist;
      }
      set
      {
        if (this.FireResist == value)
          return;
        this.EnsureAttributes().FireResist = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnFireChange();
      }
    }

    public int ColdResist
    {
      get
      {
        return this.QueryAttributes().ColdResist;
      }
      set
      {
        if (this.ColdResist == value)
          return;
        this.EnsureAttributes().ColdResist = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnColdChange();
      }
    }

    public int PoisonResist
    {
      get
      {
        return this.QueryAttributes().PoisonResist;
      }
      set
      {
        if (this.PoisonResist == value)
          return;
        this.EnsureAttributes().PoisonResist = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnPoisonChange();
      }
    }

    public int EnergyResist
    {
      get
      {
        return this.QueryAttributes().EnergyResist;
      }
      set
      {
        if (this.EnergyResist == value)
          return;
        this.EnsureAttributes().EnergyResist = value;
        if (this.m_Refresh || this.m_StatusBar == null)
          return;
        this.m_StatusBar.OnEnergyChange();
      }
    }

    public Mobile(int serial)
      : base(serial)
    {
      this.m_Flags = new MobileFlags(this);
      this.m_Walking = new Queue(0);
    }

    protected override AgentCell CreateViewportCell()
    {
      return (AgentCell) new MobileCell(this);
    }

    public void SetHealthLevel(int type, byte value)
    {
      switch (type)
      {
        case 1:
          this.greenHealthLevel = value;
          break;
        case 2:
          this.yellowHealthLevel = value;
          break;
        case 3:
          this.redHealthLevel = value;
          break;
      }
    }

    public void QueryProperties()
    {
      if (!Engine.ServerFeatures.AOS || DateTime.Now < this.m_NextQueryProps)
        return;
      this.m_NextQueryProps = DateTime.Now + TimeSpan.FromSeconds(1.0);
      Network.Send((Packet) new PQueryProperties(this.Serial));
    }

    Frames IAnimationOwner.GetOwnedFrames(IHue hue, int realID)
    {
      if (this.m_iAnimationPool == realID && this.m_hAnimationPool == hue && !this.m_fAnimationPool.Disposed)
        return this.m_fAnimationPool;
      this.m_fAnimationPool = hue.GetAnimation(realID);
      this.m_hAnimationPool = hue;
      this.m_iAnimationPool = realID;
      return this.m_fAnimationPool;
    }

    public void Draw(Texture t, int x, int y)
    {
      if (this.m_vCache == null)
        this.m_vCache = new AnimationVertexCache();
      this.m_vCache.Draw(t, x, y);
    }

    public void DrawGame(Texture t, int x, int y, int color)
    {
      if (this.m_vCache == null)
        this.m_vCache = new AnimationVertexCache();
      this.m_vCache.DrawGame(t, x, y, color);
    }

    public void OnTarget()
    {
      TargetManager.Target((object) this);
    }

    public void OnSingleClick()
    {
      this.Look();
    }

    public void OnDoubleClick()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      if (player.Flags[MobileFlag.Warmode] && !player.Ghost && (!this.Ghost && !this.Player))
      {
        NotoQueryType notorietyQuery = Options.Current.NotorietyQuery;
        if (this.m_Notoriety == Notoriety.Innocent && (notorietyQuery == NotoQueryType.On || notorietyQuery == NotoQueryType.Smart && (this.IsGuarded || player.IsGuarded)))
          Gumps.Desktop.Children.Add((Gump) new GCriminalAttackQuery(this));
        else
          this.Attack();
      }
      else
      {
        this.Use();
        PUseRequest.Last = (IEntity) this;
      }
    }

    public bool Attack()
    {
      return Network.Send((Packet) new PAttackRequest(this));
    }

    public bool Use()
    {
      return Network.Send((Packet) new PUseRequest((IEntity) this));
    }

    public bool Look()
    {
      if (this.IsIgnored)
        this.AddTextMessage("", "[Ignored]", (IFont) Engine.GetFont(3), Hues.Load(946), true);
      return Network.Send((Packet) new PLookRequest((IEntity) this));
    }

    public void Update()
    {
    }

    public bool QueryStats()
    {
      this.m_OpenedStatus = true;
      return Network.Send((Packet) new PQueryStats(this.Serial));
    }

    public Item FindEquip(Layer layer)
    {
      List<Item> items = this.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        Item obj = items[index];
        if (obj.Layer == layer)
          return obj;
      }
      return (Item) null;
    }

    public bool HasEquipOnLayer(Layer check)
    {
      return this.FindEquip(check) != null;
    }

    public bool UsingTwoHandedWeapon()
    {
      Item equip = this.FindEquip(Layer.TwoHanded);
      if (equip == null)
        return false;
      int num = equip.ID & 16383;
      return num < 7026 || num > 7035 || (num < 7107 || num > 7111);
    }

    public Item FindEquip(IItemValidator check)
    {
      List<Item> items = this.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        Item check1 = items[index];
        if (check.IsValid(check1))
          return check1;
      }
      return (Item) null;
    }

    public bool HasEquip(Item check)
    {
      if (check != null)
        return check.Parent == this;
      return false;
    }

    public void EquipRemoved()
    {
      GCombatGump.Update();
    }

    public void EquipChanged()
    {
      GCombatGump.Update();
      if (!this.Player)
        return;
      PlayUO.Profiles.Player.Current.EquipAgent.UpdateEquipment();
    }

    protected override void OnChildRemoved(Agent child)
    {
      base.OnChildRemoved(child);
      if (!(child is Item))
        return;
      Item obj = (Item) child;
      if (this._sortedItems == null)
        return;
      this._sortedItems.Remove(obj);
    }

    protected override void OnLocationChanged()
    {
      base.OnLocationChanged();
      if (!this.InWorld || !this.Visible)
        return;
      this.m_KUOC_X = this.X;
      this.m_KUOC_Y = this.Y;
      this.m_KUOC_Z = this.Z;
      this.m_KUOC_F = Engine.m_World;
    }

    protected override void OnChildAdded(Agent child)
    {
      base.OnChildAdded(child);
      if (!(child is Item))
        return;
      Item obj = (Item) child;
      if (this._sortedItems == null)
        return;
      int index = this._sortedItems.BinarySearch(obj, (IComparer<Item>) this._sortComparer);
      if (index < 0)
        index = ~index;
      this._sortedItems.Insert(index, obj);
    }

    public List<Item> GetSortedItems()
    {
      return this.GetSortedItems((int) this.m_Direction);
    }

    public List<Item> GetSortedItems(int direction)
    {
      if (this.CorpseSerial != 0)
      {
        Item obj = World.FindItem(this.CorpseSerial);
        if (obj != null && obj.CorpseItems != null)
          return obj.GetSortedCorpseItems((int) this.m_Direction);
      }
      LayerComparer layerComparer = LayerComparer.FromDirection((int) this.m_Direction);
      if (this._sortedItems == null)
        this._sortedItems = new List<Item>((IEnumerable<Item>) this.Items);
      if (layerComparer != this._sortComparer)
      {
        this._sortComparer = layerComparer;
        this._sortedItems.Sort((IComparer<Item>) this._sortComparer);
      }
      return this._sortedItems;
    }

    public void UpdateReal()
    {
      this.SetReal(this.X, this.Y, this.Z, (int) this.m_Direction);
    }

    public void SetReal(int x, int y, int z, int d)
    {
      bool visible1 = this.Visible;
      this.m_XReal = (short) x;
      this.m_YReal = (short) y;
      this.m_ZReal = (short) z;
      this.m_DReal = (byte) d;
      bool visible2 = this.Visible;
      if (visible1 == visible2 || this.m_StatusBar == null || this.m_Refresh)
        return;
      this.m_StatusBar.OnRefresh();
    }

    private void StatChange(string name, int oldValue, int newValue)
    {
      int num = newValue - oldValue;
      if (num == 0)
        return;
      Engine.AddTextMessage(string.Format("Your {0} has {1} by {2}. It is now {3}.", (object) name, num > 0 ? (object) "increased" : (object) "decreased", (object) Math.Abs(num), (object) newValue), (IFont) Engine.GetFont(3), Hues.Load(368));
    }

    protected override void OnDeleted()
    {
      base.OnDeleted();
      if (Engine.m_Highlight == this)
        Engine.m_Highlight = (object) null;
      this.greenHealthLevel = (byte) 0;
      this.yellowHealthLevel = (byte) 0;
      this.redHealthLevel = (byte) 0;
      this.IsDeadPet = false;
      bool flag = false;
      if (this.m_CorpseSerial != 0)
      {
        Item obj = World.FindItem(this.m_CorpseSerial);
        if (obj != null && obj.CorpseSerial == this.Serial)
          obj.CorpseSerial = 0;
      }
      if (this.StatusBar != null && !(this.StatusBar is GPartyHealthBar))
      {
        this.StatusBar.Close();
        this.StatusBar = (IMobileStatus) null;
        this.OpenedStatus = false;
        flag = true;
      }
      else if (this.OpenedStatus)
      {
        this.OpenedStatus = false;
        flag = true;
      }
      if (this.StatusBar != null)
        this.StatusBar.OnRefresh();
      if (!flag)
        return;
      Network.Send((Packet) new PCloseStatus(this));
    }

    internal void OnFlagsChanged()
    {
      this.InternalOnFlagsChanged(this.m_Flags);
    }

    private void InternalOnFlagsChanged(MobileFlags flags)
    {
      if (!this.m_Refresh && this.m_StatusBar != null)
        this.m_StatusBar.OnFlagsChange(flags);
      this.m_Flags = flags;
      if (!this.Player || (this.m_Flags.Value & 128) != 0)
        return;
      Engine.m_Stealth = false;
      Engine.m_StealthSteps = 0;
    }

    public void AddTextMessage(string Name, string Message, IFont Font, IHue Hue, bool unremovable)
    {
      string text = Name.Length <= 0 ? Message : Name + ": " + Message;
      if (Message.Length <= 0)
        return;
      Engine.AddToJournal(new JournalEntry(text, Hue, this.Serial));
      Message = Engine.WrapText(Message, 200, Font).TrimEnd();
      if (Message.Length <= 0)
        return;
      MessageManager.AddMessage((IMessage) new GDynamicMessage(unremovable, this, Message, Font, Hue));
    }

    public void Disturb()
    {
      if (this.m_LastDisturb + TimeSpan.FromSeconds(0.5) >= DateTime.Now)
        return;
      this.m_LastDisturb = DateTime.Now;
      Mobile player = World.Player;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      Item pickUp = (Item) null;
      for (int index = 0; index < Mobile.m_DisturbLayers.Length; ++index)
      {
        pickUp = player.FindEquip(Mobile.m_DisturbLayers[index]);
        if (pickUp != null)
          break;
      }
      if (pickUp == null)
        pickUp = backpack.FindItem((IItemValidator) new ItemIDValidator(new int[2]{ 3921, 3922 }));
      if (pickUp != null)
      {
        this.AddTextMessage(this.m_Name, "- disrupt -", Engine.DefaultFont, Hues.Load(53), true);
        new EquipContext(pickUp, pickUp.Amount, this, false).Enqueue();
      }
      else
      {
        Item obj = backpack.FindItem((IItemValidator) new ItemIDValidator(new int[4]{ 2575, 2594, 2597, 2600 }));
        if (obj == null)
          return;
        this.AddTextMessage(this.m_Name, "- disrupt -", Engine.DefaultFont, Hues.Load(53), true);
        obj.Use();
      }
    }

    public void OpenStatus(bool Drag)
    {
      int num1 = 0;
      int num2 = 0;
      bool flag1 = this.m_StatusBar != null;
      bool flag2 = flag1 && Gumps.Drag == this.m_StatusBar.Gump;
      bool flag3 = flag1 && Gumps.StartDrag == this.m_StatusBar.Gump;
      int num3 = flag1 ? this.m_StatusBar.Gump.m_OffsetX : 0;
      int num4 = flag1 ? this.m_StatusBar.Gump.m_OffsetY : 0;
      bool flag4 = !flag1 || Drag;
      if (flag1)
      {
        num1 = this.m_StatusBar.Gump.X;
        num2 = this.m_StatusBar.Gump.Y;
        this.m_StatusBar.Close();
      }
      this.m_StatusBar = !this.m_BigStatus ? (Party.State != PartyState.Joined || Array.IndexOf<Mobile>(Party.Members, this) < 0 ? (IMobileStatus) new GPartyHealthBar(this, num1, num2) : (IMobileStatus) new GPartyHealthBar(this, num1, num2)) : (IMobileStatus) new GStatusBar(this, num1, num2);
      if (flag4)
      {
        this.m_StatusBar.Gump.X = Engine.m_xMouse - this.m_StatusBar.Gump.Width / 2;
        this.m_StatusBar.Gump.Y = Engine.m_yMouse - this.m_StatusBar.Gump.Height / 2;
      }
      if (flag2 || Drag)
      {
        if (Drag)
        {
          this.m_StatusBar.Gump.m_OffsetX = this.m_StatusBar.Gump.Width / 2;
          this.m_StatusBar.Gump.m_OffsetY = this.m_StatusBar.Gump.Height / 2;
        }
        else
        {
          this.m_StatusBar.Gump.m_OffsetX = num3;
          this.m_StatusBar.Gump.m_OffsetY = num4;
        }
        this.m_StatusBar.Gump.m_IsDragging = true;
        Gumps.Drag = this.m_StatusBar.Gump;
      }
      else if (flag3)
      {
        this.m_StatusBar.Gump.m_OffsetX = num3;
        this.m_StatusBar.Gump.m_OffsetY = num4;
        Gumps.StartDrag = this.m_StatusBar.Gump;
      }
      Gumps.Desktop.Children.Add(this.m_StatusBar.Gump);
      this.m_OpenedStatus = true;
    }

    public void UpdateRadarExpiration()
    {
      this._radarExpirationTime = DateTime.UtcNow + TimeSpan.FromSeconds(2.0);
    }

    protected MobileAttributes QueryAttributes()
    {
      return this.attributes ?? MobileAttributes.Default;
    }

    protected MobileAttributes EnsureAttributes()
    {
      if (this.attributes == null)
        this.attributes = new MobileAttributes();
      return this.attributes;
    }

    private class PartyAction : TargetContext
    {
      public override bool Spoof
      {
        get
        {
          return true;
        }
      }

      public PartyAction(Mobile mob)
        : base((object) mob)
      {
      }

      public override void OnDispatch()
      {
        Network.Send((Packet) new PParty_AddMember());
        base.OnDispatch();
      }
    }
  }
}
