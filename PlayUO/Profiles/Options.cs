// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Options
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;

namespace PlayUO.Profiles
{
  public class Options : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("options", new ConstructCallback((object) null, __methodptr(Construct)));
    private OptionFlag m_Flags;
    private NotoQueryType m_NotoQuery;
    private int m_HouseLevel;

    public virtual PersistableType TypeID
    {
      get
      {
        return Options.TypeCode;
      }
    }

    public static Options Current
    {
      get
      {
        return Profile.Current.Preferences.Options;
      }
    }

    [Optionable("Always Run", "Options", Default = false)]
    public bool AlwaysRun
    {
      get
      {
        return this[OptionFlag.AlwaysRun];
      }
      set
      {
        this[OptionFlag.AlwaysRun] = value;
      }
    }

    [Optionable("Incoming Names", "Options", Default = true)]
    public bool IncomingNames
    {
      get
      {
        return this[OptionFlag.IncomingNames];
      }
      set
      {
        this[OptionFlag.IncomingNames] = value;
      }
    }

    [Optionable("Notoriety Halos", "Options", Default = true)]
    public bool NotorietyHalos
    {
      get
      {
        return this[OptionFlag.NotorietyHalos];
      }
      set
      {
        this[OptionFlag.NotorietyHalos] = value;
      }
    }

    [Optionable("Protect Heals", "Options", Default = true)]
    public bool ProtectHeals
    {
      get
      {
        return this[OptionFlag.ProtectHeals];
      }
      set
      {
        this[OptionFlag.ProtectHeals] = value;
      }
    }

    [Optionable("Protect Cures", "Options", Default = true)]
    public bool ProtectCures
    {
      get
      {
        return this[OptionFlag.ProtectCures];
      }
      set
      {
        this[OptionFlag.ProtectCures] = value;
      }
    }

    [Optionable("Protect Poisons", "Options", Default = true)]
    public bool ProtectPoisons
    {
      get
      {
        return this[OptionFlag.ProtectPoisons];
      }
      set
      {
        this[OptionFlag.ProtectPoisons] = value;
      }
    }

    [Optionable("Siege Ruleset", "Options", Default = false)]
    public bool SiegeRuleset
    {
      get
      {
        return this[OptionFlag.SiegeRuleset];
      }
      set
      {
        this[OptionFlag.SiegeRuleset] = value;
      }
    }

    [Optionable("Queue Targets", "Options", Default = false)]
    public bool QueueTargets
    {
      get
      {
        return this[OptionFlag.QueueTargets];
      }
      set
      {
        this[OptionFlag.QueueTargets] = value;
      }
    }

    [Optionable("Enabled", "Scavenger", Default = true)]
    public bool Scavenger
    {
      get
      {
        return this[OptionFlag.Scavenger];
      }
      set
      {
        this[OptionFlag.Scavenger] = value;
      }
    }

    [Optionable("Screenshots", "Options", Default = true)]
    public bool Screenshots
    {
      get
      {
        return this[OptionFlag.Screenshots];
      }
      set
      {
        this[OptionFlag.Screenshots] = value;
      }
    }

    [Optionable("Health Icons", "Options", Default = true)]
    public bool MiniHealth
    {
      get
      {
        return this[OptionFlag.MiniHealth];
      }
      set
      {
        this[OptionFlag.MiniHealth] = value;
      }
    }

    [Optionable("Container Grid", "Options", Default = true)]
    public bool ContainerGrid
    {
      get
      {
        return this[OptionFlag.ContainerGrid];
      }
      set
      {
        this[OptionFlag.ContainerGrid] = value;
      }
    }

    [Optionable("Smooth Movement", "Options", Default = true)]
    public bool SmoothWalk
    {
      get
      {
        return this[OptionFlag.SmoothWalk];
      }
      set
      {
        this[OptionFlag.SmoothWalk] = value;
      }
    }

    [Optionable("Key Passthrough", "Options", Default = true)]
    public bool KeyPassthrough
    {
      get
      {
        return this[OptionFlag.KeyPassthrough];
      }
      set
      {
        this[OptionFlag.KeyPassthrough] = value;
      }
    }

    [Optionable("Moongate Confirmation", "Options", Default = false)]
    public bool MoongateConfirmation
    {
      get
      {
        return this[OptionFlag.MoongateConfirmation];
      }
      set
      {
        this[OptionFlag.MoongateConfirmation] = value;
      }
    }

    [Optionable("Always Light", "Options", Default = false)]
    public bool AlwaysLight
    {
      get
      {
        return this[OptionFlag.AlwaysLight];
      }
      set
      {
        this[OptionFlag.AlwaysLight] = value;
      }
    }

    [Optionable("Hotkeys Enabled", "Options", Default = true)]
    public bool HotkeysEnabled
    {
      get
      {
        return this[OptionFlag.HotkeysEnabled];
      }
      set
      {
        this[OptionFlag.HotkeysEnabled] = value;
      }
    }

    [Optionable("Clear Hands Before Cast", "Options", Default = false)]
    public bool ClearHandsBeforeCast
    {
      get
      {
        return this[OptionFlag.ClearHandsBeforeCast];
      }
      set
      {
        this[OptionFlag.ClearHandsBeforeCast] = value;
      }
    }

    public bool this[OptionFlag flag]
    {
      get
      {
        return (this.m_Flags & flag) == flag;
      }
      set
      {
        if (value)
          this.m_Flags |= flag;
        else
          this.m_Flags &= ~flag;
        string str = (string) null;
        switch (flag)
        {
          case OptionFlag.AlwaysLight:
            str = "Always light is";
            break;
          case OptionFlag.HotkeysEnabled:
            str = "Hotkeys are";
            break;
          case OptionFlag.ClearHandsBeforeCast:
            str = "Clear hands before cast is";
            break;
          case OptionFlag.SmoothWalk:
            str = "Smooth movement is";
            break;
          case OptionFlag.KeyPassthrough:
            str = "Key passthrough is";
            break;
          case OptionFlag.MoongateConfirmation:
            str = "Moongate confirmation is";
            break;
          case OptionFlag.Screenshots:
            str = "Death screenshots are";
            break;
          case OptionFlag.MiniHealth:
            str = "Health icons are";
            break;
          case OptionFlag.ContainerGrid:
            str = "Container grids are";
            break;
          case OptionFlag.AlwaysRun:
            str = "Always run is";
            break;
          case OptionFlag.IncomingNames:
            str = "Incoming names are";
            break;
          case OptionFlag.NotorietyHalos:
            str = "Notoriety halos are";
            break;
          case OptionFlag.QueueTargets:
            str = "Target queueing is";
            break;
          case OptionFlag.Scavenger:
            str = "Scavenging is";
            break;
        }
        if (str == null)
          return;
        Engine.AddTextMessage(string.Format("{0} now {1}.", (object) str, value ? (object) "enabled" : (object) "disabled"));
      }
    }

    [Optionable("House Level", "Options", Default = 1)]
    public int HouseLevel
    {
      get
      {
        return this.m_HouseLevel;
      }
      set
      {
        this.m_HouseLevel = Math.Min(Math.Max(1, value), 5);
        Map.Invalidate();
      }
    }

    [Optionable("Notoriety Query", "Options", Default = NotoQueryType.On)]
    public NotoQueryType NotorietyQuery
    {
      get
      {
        return this.m_NotoQuery;
      }
      set
      {
        this.m_NotoQuery = value;
        string str = "Notoriety query is";
        if (str == null)
          return;
        Engine.AddTextMessage(string.Format("{0} now {1}.", (object) str, (object) value.ToString().ToLower()));
      }
    }

    public Options()
    {
      this.m_Flags = OptionFlag.Default;
      this.m_NotoQuery = NotoQueryType.On;
      this.m_HouseLevel = 1;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Options();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      OptionFlag optionFlag = this.m_Flags | OptionFlag.HotkeysEnabled;
      op.SetInt32("flags", (int) optionFlag);
      op.SetInt32("notoQuery", (int) this.m_NotoQuery);
      op.SetInt32("houseLevel", this.m_HouseLevel);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Flags = (OptionFlag) ip.GetInt32("flags");
      this.m_NotoQuery = (NotoQueryType) ip.GetInt32("notoQuery");
      this.m_HouseLevel = Math.Min(Math.Max(1, ip.GetInt32("houseLevel")), 5);
      this.m_Flags |= OptionFlag.HotkeysEnabled;
    }
  }
}
