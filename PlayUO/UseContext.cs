// Decompiled with JetBrains decompiler
// Type: PlayUO.UseContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class UseContext : ActionContext
  {
    protected bool isManual;
    protected IEntity toUse;

    public IEntity ToUse
    {
      get
      {
        return this.toUse;
      }
    }

    public bool IsManual
    {
      get
      {
        return this.isManual;
      }
    }

    protected override bool IsReady
    {
      get
      {
        return Engine.IsActionReady;
      }
    }

    public UseContext(IEntity toUse, bool isManual)
    {
      this.toUse = toUse;
      this.isManual = isManual;
    }

    public override void OnDispatch()
    {
      if (this.toUse == null)
        return;
      if (this.toUse is Item)
      {
        Item obj = (Item) this.toUse;
        if (obj.ID == 8901 || obj.ID == 3643 || obj.ID == 3834)
          Network.Send((Packet) new PPE_QueryRunebookContent(obj));
      }
      Network.Send((Packet) new PUseRequest(this.toUse));
    }

    protected override bool CheckQueue()
    {
      if (this.toUse != null)
      {
        foreach (ActionContext actionContext in ActionContext.Queued)
        {
          UseContext useContext = actionContext as UseContext;
          if (useContext != null && useContext.toUse == this.toUse)
            return false;
        }
      }
      return base.CheckQueue();
    }

    protected override int CompareTo(ActionContext cmp)
    {
      int num = base.CompareTo(cmp);
      if (num == 0 && this.isManual)
      {
        UseContext useContext = cmp as UseContext;
        if (useContext == null || !useContext.isManual)
          num = -1;
      }
      return num;
    }
  }
}
