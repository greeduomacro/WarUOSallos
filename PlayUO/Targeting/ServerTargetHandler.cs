// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.ServerTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using Ultima.Data;

namespace PlayUO.Targeting
{
  public class ServerTargetHandler : BaseTargetHandler
  {
    protected DateTime startTime;
    protected int targetID;
    protected bool canceledByServer;
    protected bool allowGround;
    protected AggressionType aggressionType;
    protected TargetAction action;

    public override AggressionType Aggression
    {
      get
      {
        return this.aggressionType;
      }
    }

    public DateTime StartTime
    {
      get
      {
        return this.startTime;
      }
    }

    public int TargetID
    {
      get
      {
        return this.targetID;
      }
    }

    public bool AllowGround
    {
      get
      {
        return this.allowGround;
      }
    }

    public TargetAction Action
    {
      get
      {
        return this.action;
      }
      set
      {
        this.action = value;
      }
    }

    public ServerTargetHandler(int targetID, bool allowGround, AggressionType aggressionType)
    {
      this.startTime = DateTime.Now;
      this.targetID = targetID;
      this.allowGround = allowGround;
      this.aggressionType = aggressionType;
    }

    protected override bool OnTarget(object targeted)
    {
      return base.OnTarget(targeted);
    }

    protected override bool OnTarget(Mobile mob)
    {
      if (this.CheckQuery(mob))
      {
        Gumps.Desktop.Children.Add((Gump) new GCriminalTargetQuery(mob, this));
        return true;
      }
      if (this.ShouldBlock(mob))
        return false;
      this.Announce(mob);
      this.Dispatch(0, mob.Serial, mob.X, mob.Y, mob.Z, (int) mob.Body & 16383);
      return true;
    }

    protected virtual bool CheckQuery(Mobile mob)
    {
      if (mob.Player)
        return false;
      Mobile player = World.Player;
      switch (Options.Current.NotorietyQuery)
      {
        case NotoQueryType.On:
          if (this.IsOffensive)
            return mob.Notoriety == Notoriety.Innocent;
          if (!this.IsDefensive || Options.Current.SiegeRuleset)
            return false;
          if (mob.Notoriety != Notoriety.Criminal)
            return mob.Notoriety == Notoriety.Murderer;
          return true;
        case NotoQueryType.Smart:
          if (!mob.IsGuarded && !player.IsGuarded)
            return false;
          goto case NotoQueryType.On;
        default:
          return false;
      }
    }

    protected virtual bool ShouldBlock(Mobile mob)
    {
      switch (this.action)
      {
        case TargetAction.Poison:
          if (Options.Current.ProtectPoisons && mob.IsPoisoned)
            return true;
          break;
        case TargetAction.GreaterHeal:
        case TargetAction.Heal:
          if (Options.Current.ProtectHeals && mob.IsPoisoned)
            return true;
          break;
        case TargetAction.Cure:
          if (Options.Current.ProtectCures && !mob.IsPoisoned)
            return true;
          break;
      }
      return false;
    }

    public virtual void Announce(Mobile mob)
    {
      string actionName = this.GetActionName(mob);
      if (actionName == null)
        return;
      string targetName = this.GetTargetName(mob);
      if (targetName == null)
        return;
      Party.SendAutomatedMessage("Targeting {0} with {1}", (object) targetName, (object) actionName);
    }

    protected virtual string GetActionName(Mobile mob)
    {
      return TargetActions.GetName(this.action);
    }

    protected virtual string GetTargetName(Mobile mob)
    {
      if (mob.Player)
        return "myself";
      string identifier = mob.Identifier;
      if (identifier != null)
        return identifier;
      if (mob.HumanOrGhost)
        return "someone";
      return (string) null;
    }

    protected override bool OnTarget(Item item)
    {
      int z = item.InWorld ? item.Z : 0;
      this.Dispatch(0, item.Serial, item.X, item.Y, z, item.ID);
      return true;
    }

    protected override bool OnTarget(GroundTarget groundTarget)
    {
      if (this.allowGround)
        this.Dispatch(1, 0, groundTarget.X, groundTarget.Y, groundTarget.Z, 0);
      return this.allowGround;
    }

    protected override unsafe bool OnTarget(StaticTarget staticTarget)
    {
      ItemData* itemDataPointer = Map.GetItemDataPointer((ItemId) (int) (ushort) staticTarget.RealID);
      int z = staticTarget.Z;
      if ((itemDataPointer->Flags & 512L) != 0L)
        z += (int) itemDataPointer->Height;
      this.Dispatch(1, 0, staticTarget.X, staticTarget.Y, z, staticTarget.RealID);
      return true;
    }

    protected virtual void Dispatch(int type, int serial, int x, int y, int z, int id)
    {
      Network.Send((Packet) new PTarget_Response(type, this, serial, x, y, z, id));
    }

    public void Cancel(bool canceledByServer)
    {
      this.canceledByServer = canceledByServer;
      this.Cancel();
    }

    protected override void OnCancel()
    {
      if (this.canceledByServer)
        return;
      Network.Send((Packet) new PTarget_Cancel(this));
    }
  }
}
