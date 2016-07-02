// Decompiled with JetBrains decompiler
// Type: PlayUO.LookContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class LookContext : ActionContext
  {
    protected IEntity toObserve;

    public IEntity ToObserve
    {
      get
      {
        return this.toObserve;
      }
    }

    public LookContext(IEntity toObserve)
    {
      this.toObserve = toObserve;
    }

    public override void OnDispatch()
    {
      if (this.toObserve == null)
        return;
      Network.Send((Packet) new PLookRequest(this.toObserve));
    }

    protected override bool CheckQueue()
    {
      if (this.toObserve != null)
      {
        foreach (ActionContext actionContext in ActionContext.Queued)
        {
          LookContext lookContext = actionContext as LookContext;
          if (lookContext != null && lookContext.toObserve == this.toObserve)
            return false;
        }
      }
      return base.CheckQueue();
    }
  }
}
