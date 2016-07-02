// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.TargetManager
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections.Generic;

namespace PlayUO.Targeting
{
  public static class TargetManager
  {
    private static object smartSentinel = new object();
    private static ClientTargetHandler clientHandler;
    private static ServerTargetHandler serverHandler;
    private static object lastTarget;
    private static Mobile lastOffensive;
    private static Mobile lastDefensive;
    private static object queuedTarget;

    public static BaseTargetHandler Active
    {
      get
      {
        if (TargetManager.clientHandler != null)
          return (BaseTargetHandler) TargetManager.clientHandler;
        return (BaseTargetHandler) TargetManager.serverHandler;
      }
    }

    public static bool IsActive
    {
      get
      {
        return TargetManager.Active != null;
      }
    }

    public static ClientTargetHandler Client
    {
      get
      {
        return TargetManager.clientHandler;
      }
      set
      {
        if (TargetManager.clientHandler == value)
          return;
        if (TargetManager.clientHandler != null)
          TargetManager.clientHandler.Cancel();
        TargetManager.clientHandler = value;
      }
    }

    public static ServerTargetHandler Server
    {
      get
      {
        return TargetManager.serverHandler;
      }
      set
      {
        if (TargetManager.serverHandler == value)
          return;
        if (TargetManager.serverHandler != null)
          TargetManager.serverHandler.Cancel(true);
        TargetManager.serverHandler = value;
      }
    }

    public static int Depth
    {
      get
      {
        int num = 0;
        if (TargetManager.clientHandler != null)
          ++num;
        if (TargetManager.serverHandler != null)
          ++num;
        return num;
      }
    }

    public static object LastTarget
    {
      get
      {
        return TargetManager.lastTarget;
      }
      set
      {
        TargetManager.lastTarget = value;
      }
    }

    public static Mobile LastOffensiveTarget
    {
      get
      {
        return TargetManager.lastOffensive;
      }
      set
      {
        TargetManager.lastOffensive = value;
      }
    }

    public static Mobile LastDefensiveTarget
    {
      get
      {
        return TargetManager.lastDefensive;
      }
      set
      {
        TargetManager.lastDefensive = value;
      }
    }

    public static bool IsQueued
    {
      get
      {
        return TargetManager.queuedTarget != null;
      }
    }

    public static void ProcessQueue()
    {
      ServerTargetHandler server = TargetManager.Server;
      if (server == null || TargetManager.queuedTarget == null)
        return;
      if (TargetManager.queuedTarget == TargetManager.smartSentinel)
      {
        if (server.IsOffensive)
          server.Target((object) TargetManager.lastOffensive);
        else if (server.IsDefensive)
          server.Target((object) TargetManager.lastDefensive);
        else
          server.Target(TargetManager.lastTarget);
      }
      else
        server.Target(TargetManager.queuedTarget);
      TargetManager.queuedTarget = (object) null;
    }

    public static void ClearQueue()
    {
      Mobile player = World.Player;
      if (player != null)
        player.AddTextMessage("", "- cleared target queue -", Engine.DefaultFont, Hues.Load(89), true);
      TargetManager.queuedTarget = (object) null;
    }

    public static void QueueSmart()
    {
      TargetManager.Queue(TargetManager.smartSentinel);
    }

    public static void Queue(object toTarget)
    {
      if (toTarget == null)
        return;
      if (TargetManager.Active != null)
      {
        if (toTarget == TargetManager.smartSentinel)
          TargetManager.TargetSmart();
        else
          TargetManager.Target(toTarget);
      }
      else
      {
        if (TargetManager.queuedTarget == toTarget)
          return;
        object obj = toTarget;
        string str;
        if (toTarget == TargetManager.smartSentinel)
        {
          str = "Smart target queued";
          obj = (object) World.Player;
        }
        else
          str = toTarget != World.Player ? "Target queued" : "Target self queued";
        if (obj is Mobile)
          (obj as Mobile).AddTextMessage("", string.Format("- {0} -", (object) str.ToLowerInvariant()), Engine.DefaultFont, Hues.Load(89), false);
        else if (obj is Item)
          (obj as Item).AddTextMessage("", string.Format("- {0} -", (object) str.ToLowerInvariant()), Engine.DefaultFont, Hues.Load(89), false);
        else
          Engine.AddTextMessage(string.Format("{0}.", (object) str), Engine.DefaultFont, Hues.Load(89));
        TargetManager.queuedTarget = toTarget;
      }
    }

    public static bool IsAcquirable(Mobile me, Mobile mob)
    {
      return TargetManager.IsAcquirable(me, mob, false);
    }

    public static bool IsAcquirable(Mobile me, Mobile mob, bool isBola)
    {
      if (mob.IsFriend || mob.IsInParty || (int) mob.Body == 987 || isBola && !mob.IsMounted)
        return false;
      switch (mob.Notoriety)
      {
        case Notoriety.Innocent:
          if (me.Notoriety == Notoriety.Murderer)
            return !mob.IsGuarded;
          return false;
        case Notoriety.Ally:
        case Notoriety.Vendor:
          return false;
        default:
          return true;
      }
    }

    public static void Reacquire()
    {
      Mobile mobile = TargetManager.Acquire((Predicate<Mobile>) null);
      if (mobile == null)
        return;
      TargetManager.LastTarget = (object) mobile;
      TargetManager.LastOffensiveTarget = mobile;
      mobile.AddTextMessage("", "Last target set.", Engine.DefaultFont, Hues.Load(89), false);
      string identifier = mobile.Identifier;
      if (identifier == null)
        return;
      Party.SendAutomatedMessage("Changing last target to {0}", (object) identifier);
    }

    public static Mobile Acquire(Predicate<Mobile> validator)
    {
      Mobile player = World.Player;
      if (player != null)
      {
        using (ScratchList<Mobile> scratchList = new ScratchList<Mobile>())
        {
          List<Mobile> mobileList = scratchList.Value;
          bool isBola = false;
          int range = Engine.ServerFeatures.AOS ? 11 : 12;
          ServerTargetHandler serverTargetHandler = TargetManager.Active as ServerTargetHandler;
          if (serverTargetHandler != null && serverTargetHandler.Action == TargetAction.Bola)
          {
            isBola = true;
            range = 8;
          }
          foreach (Mobile mobile in World.Mobiles.Values)
          {
            if (mobile.Visible && !mobile.Player && (!mobile.IsDead && TargetManager.IsAcquirable(player, mobile, isBola)) && (player.InRange((IPoint2D) mobile, range) && Map.LineOfSight(player, mobile) && (validator == null || validator(mobile))))
              mobileList.Add(mobile);
          }
          if (mobileList.Count > 0)
          {
            mobileList.Sort(TargetSorter.Comparer);
            return mobileList[0];
          }
        }
      }
      return (Mobile) null;
    }

    public static void TargetSelf()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      TargetManager.Target((object) player);
    }

    public static void TargetAcquire()
    {
      Mobile mobile = TargetManager.Acquire((Predicate<Mobile>) null);
      if (mobile == null)
        return;
      TargetManager.Target((object) mobile);
    }

    public static void TargetSmart()
    {
      if (TargetManager.IsActive)
      {
        if (TargetManager.Active.IsOffensive)
          TargetManager.Target((object) TargetManager.lastOffensive);
        else if (TargetManager.Active.IsDefensive)
          TargetManager.Target((object) TargetManager.lastDefensive);
        else
          TargetManager.Target(TargetManager.lastTarget);
      }
      else
      {
        if (!Options.Current.QueueTargets)
          return;
        TargetManager.Queue(TargetManager.smartSentinel);
      }
    }

    public static void Target(object targeted)
    {
      if (targeted == null)
        return;
      if (TargetManager.IsActive)
      {
        BaseTargetHandler active = TargetManager.Active;
        if (active is ServerTargetHandler && !TargetManager.TargetIsInRange(targeted) || !active.Target(targeted))
          return;
        if (active is ServerTargetHandler)
          TargetManager.queuedTarget = (object) null;
        Mobile mobile = targeted as Mobile;
        if (mobile != null && mobile.Player)
          return;
        TargetManager.lastTarget = targeted;
        if (mobile == null)
          return;
        if (active is AcquireTargetHandler && (mobile.IsFriend || mobile.IsInParty))
          TargetManager.lastDefensive = mobile;
        else if (active.IsOffensive)
        {
          TargetManager.lastOffensive = mobile;
        }
        else
        {
          if (!active.IsDefensive)
            return;
          TargetManager.lastDefensive = mobile;
        }
      }
      else
      {
        if (!Options.Current.QueueTargets)
          return;
        TargetManager.Queue(targeted);
      }
    }

    public static bool TargetIsInRange()
    {
      return TargetManager.TargetIsInRange(TargetManager.LastTarget);
    }

    public static bool TargetIsInRange(object targeted)
    {
      if (targeted == null || TargetManager.Server == null)
        return false;
      TargetAction action = TargetManager.Server.Action;
      if (action == TargetAction.Unknown)
        return true;
      int num;
      switch (action)
      {
        case TargetAction.Bola:
          num = 8;
          break;
        case TargetAction.Stealing:
        case TargetAction.Resurrection:
        case TargetAction.Bandage:
          num = 1;
          break;
        default:
          num = 12;
          break;
      }
      Mobile player = World.Player;
      if (player == null)
        return false;
      if (targeted is Item && !((Agent) targeted).InWorld)
      {
        Agent worldRoot = ((Agent) targeted).WorldRoot;
        if (worldRoot == null || player.DistanceTo(worldRoot.X, worldRoot.Y) > num)
          return false;
      }
      else if (targeted is IPoint3D)
      {
        IPoint3D point3D = (IPoint3D) targeted;
        if (player.DistanceTo(point3D.X, point3D.Y) > num)
          return false;
      }
      return true;
    }
  }
}
