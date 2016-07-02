// Decompiled with JetBrains decompiler
// Type: PlayUO.Party
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public static class Party
  {
    private static Mobile[] m_Members = new Mobile[0];
    private static PartyState m_State;
    private static Mobile m_Leader;

    public static PartyState State
    {
      get
      {
        return Party.m_State;
      }
      set
      {
        Party.m_State = value;
        if (Party.m_State == PartyState.Joined)
          return;
        Party.m_Members = new Mobile[0];
        Party.m_Leader = (Mobile) null;
      }
    }

    public static bool IsLeader
    {
      get
      {
        if (Party.m_Leader != null)
          return Party.m_Leader.Player;
        return false;
      }
    }

    public static int Index
    {
      get
      {
        return Array.IndexOf<Mobile>(Party.m_Members, World.Player);
      }
    }

    public static Mobile Leader
    {
      get
      {
        return Party.m_Leader;
      }
      set
      {
        Party.m_Leader = value;
        if (Party.m_Members.Length <= 0)
          return;
        Party.m_Members[0] = Party.m_Leader;
      }
    }

    public static Mobile[] Members
    {
      get
      {
        return Party.m_Members;
      }
      set
      {
        Party.m_Members = value;
        Party.m_Leader = Party.m_Members.Length > 0 ? Party.m_Members[0] : (Mobile) null;
        Party.m_State = Party.m_Members.Length <= 0 || Party.Index < 0 ? PartyState.Alone : PartyState.Joined;
      }
    }

    public static void SendAutomatedMessage(string text)
    {
    }

    public static void SendAutomatedMessage(string format, params object[] args)
    {
      Party.SendAutomatedMessage(string.Format(format, args));
    }

    public static bool CheckAutomatedAccept()
    {
      if (Party.State == PartyState.Joining)
      {
        Mobile player = World.Player;
        if (player != null && player.Guild != null)
        {
          Mobile leader = Party.Leader;
          if (leader != null && leader.Guild == player.Guild)
            return true;
        }
      }
      return false;
    }

    public static bool CheckAutomatedInvite(Mobile mob)
    {
      if (Party.State == PartyState.Joined && Party.IsLeader && Party.Members.Length < 10)
      {
        Mobile player = World.Player;
        if (player != null && player.Guild != null && (mob != null && mob.Guild == player.Guild) && (!mob.IsInParty && mob.InRange((IPoint2D) player, 8)))
          return true;
      }
      return false;
    }
  }
}
