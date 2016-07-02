// Decompiled with JetBrains decompiler
// Type: PlayUO.TargetContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;

namespace PlayUO
{
  internal class TargetContext : UseContext
  {
    protected object toTarget;
    protected ServerTargetHandler lastHandler;

    public virtual bool Spoof
    {
      get
      {
        return false;
      }
    }

    public virtual AggressionType SpoofFlags
    {
      get
      {
        return AggressionType.Neutral;
      }
    }

    public TargetContext(object toTarget)
      : base((IEntity) null, false)
    {
      this.toTarget = toTarget;
    }

    public override void OnDispatch()
    {
      base.OnDispatch();
      if (!this.Spoof)
        return;
      PTarget_Spoof ptargetSpoof = (PTarget_Spoof) null;
      if (this.toTarget == null)
        ptargetSpoof = new PTarget_Spoof(0, 12648430, this.SpoofFlags, 0, -1, -1, 0, 0);
      else if (this.toTarget is Mobile)
      {
        Mobile mobile = this.toTarget as Mobile;
        ptargetSpoof = new PTarget_Spoof(0, 12648430, this.SpoofFlags, mobile.Serial, mobile.X, mobile.Y, mobile.Z, (int) mobile.Body);
      }
      else if (this.toTarget is Item)
      {
        Item obj = this.toTarget as Item;
        ptargetSpoof = new PTarget_Spoof(0, 12648430, this.SpoofFlags, obj.Serial, obj.X, obj.Y, obj.Z, obj.ID);
      }
      else if (this.toTarget is GroundTarget)
      {
        GroundTarget groundTarget = this.toTarget as GroundTarget;
        ptargetSpoof = new PTarget_Spoof(1, 12648430, this.SpoofFlags, 0, groundTarget.X, groundTarget.Y, groundTarget.Z, 0);
      }
      else if (this.toTarget is StaticTarget)
      {
        StaticTarget staticTarget = this.toTarget as StaticTarget;
        ptargetSpoof = new PTarget_Spoof(1, 12648430, this.SpoofFlags, 0, staticTarget.X, staticTarget.Y, staticTarget.Z, staticTarget.RealID);
      }
      Network.Send((Packet) ptargetSpoof);
    }

    public override void OnBegin()
    {
      this.lastHandler = TargetManager.Server;
    }

    public override void OnFinish()
    {
      ServerTargetHandler server = TargetManager.Server;
      if (server == null || server == this.lastHandler)
        return;
      this.OnSuccess(server);
    }

    public virtual void OnSuccess(ServerTargetHandler targetHandler)
    {
      if (!this.Spoof)
      {
        if (this.toTarget == null)
          targetHandler.Cancel();
        else
          targetHandler.Target(this.toTarget);
      }
      else
        targetHandler.Clear();
    }
  }
}
