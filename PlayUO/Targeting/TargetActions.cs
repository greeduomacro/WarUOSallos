// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.TargetActions
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Targeting
{
  internal class TargetActions
  {
    public static TimeSpan MarginOfError = TimeSpan.FromSeconds(2.5);
    private static TargetAction m_Lookahead;
    private static DateTime m_Creation;

    public static TargetAction Lookahead
    {
      get
      {
        return TargetActions.m_Lookahead;
      }
      set
      {
        TargetActions.m_Lookahead = value;
        TargetActions.m_Creation = DateTime.Now;
      }
    }

    public static void Identify()
    {
      ServerTargetHandler server = TargetManager.Server;
      if (TargetActions.m_Lookahead != TargetAction.Unknown)
      {
        server.Action = !(TargetActions.m_Creation + TargetActions.MarginOfError > DateTime.Now) || server == null || server.Aggression != TargetActions.GetFlags(TargetActions.m_Lookahead) ? TargetAction.Unknown : TargetActions.m_Lookahead;
        TargetActions.m_Lookahead = TargetAction.Unknown;
      }
      else if (server != null && server.Aggression == AggressionType.Defensive)
      {
        server.Action = TargetAction.GreaterHeal;
      }
      else
      {
        if (server == null)
          return;
        server.Action = TargetAction.Unknown;
      }
    }

    public static string GetName(TargetAction action)
    {
      if (action == TargetAction.Unknown)
        return (string) null;
      switch (action)
      {
        case TargetAction.Bandage:
          return "a bandage";
        case TargetAction.Bola:
          return "a bola";
        case TargetAction.PurplePotion:
          return "an explosion potion";
        default:
          string str = action.ToString();
          for (int index = 0; index < str.Length; ++index)
          {
            if (index > 0 && (int) str[index] >= 65 && (int) str[index] <= 90)
              str = str.Insert(index++, " ");
          }
          return str.ToLowerInvariant();
      }
    }

    public static void Identify(TargetAction action)
    {
      ServerTargetHandler server = TargetManager.Server;
      if (server != null && server.StartTime + TargetActions.MarginOfError > DateTime.Now && server.Aggression == TargetActions.GetFlags(action))
        server.Action = action;
      else if (server != null)
        server.Action = TargetAction.Unknown;
      TargetActions.m_Lookahead = TargetAction.Unknown;
    }

    public static AggressionType GetFlags(TargetAction action)
    {
      if (action >= TargetAction.DetectHidden)
        return AggressionType.Neutral;
      if (action >= TargetAction.Bola)
        return AggressionType.Offensive;
      if (action >= TargetAction.Bandage)
        return AggressionType.Defensive;
      switch (action)
      {
        case TargetAction.Clumsy:
        case TargetAction.Feeblemind:
        case TargetAction.MagicArrow:
        case TargetAction.Weaken:
        case TargetAction.Harm:
        case TargetAction.Fireball:
        case TargetAction.Poison:
        case TargetAction.Curse:
        case TargetAction.Lightning:
        case TargetAction.ManaDrain:
        case TargetAction.MindBlast:
        case TargetAction.Paralyze:
        case TargetAction.Dispel:
        case TargetAction.EnergyBolt:
        case TargetAction.Explosion:
        case TargetAction.FlameStrike:
        case TargetAction.ManaVampire:
          return AggressionType.Offensive;
        case TargetAction.Heal:
        case TargetAction.Agility:
        case TargetAction.Cunning:
        case TargetAction.Cure:
        case TargetAction.Strength:
        case TargetAction.Bless:
        case TargetAction.ArchCure:
        case TargetAction.ArchProtection:
        case TargetAction.GreaterHeal:
        case TargetAction.Invisibility:
        case TargetAction.Resurrection:
          return AggressionType.Defensive;
        default:
          return AggressionType.Neutral;
      }
    }
  }
}
