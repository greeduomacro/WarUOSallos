// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.BaseTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  public abstract class BaseTargetHandler
  {
    private bool hasActed;

    public virtual AggressionType Aggression
    {
      get
      {
        return AggressionType.Neutral;
      }
    }

    public bool IsOffensive
    {
      get
      {
        return this.Aggression == AggressionType.Offensive;
      }
    }

    public bool IsDefensive
    {
      get
      {
        return this.Aggression == AggressionType.Defensive;
      }
    }

    public bool IsNeutral
    {
      get
      {
        return this.Aggression == AggressionType.Neutral;
      }
    }

    public virtual void Clear()
    {
      if (TargetManager.Server == this)
        TargetManager.Server = (ServerTargetHandler) null;
      if (TargetManager.Client != this)
        return;
      TargetManager.Client = (ClientTargetHandler) null;
    }

    public virtual bool Target(object targeted)
    {
      if (this.hasActed || !this.OnTarget(targeted))
        return false;
      this.hasActed = true;
      this.Clear();
      return true;
    }

    protected virtual bool OnTarget(object targeted)
    {
      Mobile mob = targeted as Mobile;
      if (mob != null)
        return this.OnTarget(mob);
      Item obj = targeted as Item;
      if (obj != null)
        return this.OnTarget(obj);
      GroundTarget groundTarget = targeted as GroundTarget;
      if (groundTarget != null)
        return this.OnTarget(groundTarget);
      StaticTarget staticTarget = targeted as StaticTarget;
      if (staticTarget != null)
        return this.OnTarget(staticTarget);
      return false;
    }

    protected virtual bool OnTarget(Mobile mob)
    {
      return false;
    }

    protected virtual bool OnTarget(Item item)
    {
      return false;
    }

    protected virtual bool OnTarget(GroundTarget groundTarget)
    {
      return false;
    }

    protected virtual bool OnTarget(StaticTarget staticTarget)
    {
      return false;
    }

    public virtual void Cancel()
    {
      if (this.hasActed)
        return;
      this.hasActed = true;
      this.OnCancel();
      this.Clear();
    }

    protected virtual void OnCancel()
    {
    }
  }
}
