// Decompiled with JetBrains decompiler
// Type: PlayUO.MoveContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class MoveContext : ActionContext
  {
    protected int x = -1;
    protected int y = -1;
    protected Item pickUp;
    protected int amount;
    protected IEntity dropTo;
    protected bool liftFailed;
    protected bool clickFirst;

    protected override bool IsReady
    {
      get
      {
        return Engine.IsActionReady;
      }
    }

    public MoveContext(Item pickUp, int amount, IEntity dropTo, bool clickFirst)
    {
      this.pickUp = pickUp;
      this.amount = amount;
      this.dropTo = dropTo;
      this.clickFirst = clickFirst;
    }

    public MoveContext Locate(int x, int y)
    {
      this.x = x;
      this.y = y;
      return this;
    }

    protected override bool CheckQueue()
    {
      foreach (ActionContext actionContext in ActionContext.Queued)
      {
        MoveContext moveContext = actionContext as MoveContext;
        if (moveContext != null && moveContext.pickUp == this.pickUp && (moveContext.dropTo == this.dropTo && this is EquipContext == moveContext is EquipContext))
          return false;
      }
      return base.CheckQueue();
    }

    public void OnLiftFailed()
    {
      this.liftFailed = true;
    }

    public override void OnBegin()
    {
      base.OnBegin();
      this.liftFailed = false;
    }

    public bool TryEnqueue()
    {
      if (this.pickUp == this.dropTo || this.pickUp.Parent == this.dropTo && (this.x == -1 || this.pickUp.X == this.x) && (this.y == -1 || this.pickUp.Y == this.y))
        return false;
      return this.Enqueue();
    }

    public override void OnDispatch()
    {
      if (this.clickFirst)
        Network.Send((Packet) new PLookRequest((IEntity) this.pickUp));
      Network.Send((Packet) new PPickupItem(this.pickUp, this.amount));
    }

    public override void OnFinish()
    {
      base.OnFinish();
      if (!this.liftFailed)
      {
        this.SendDropPacket();
      }
      else
      {
        if (this.ShouldRepeat)
          return;
        Engine.AddTextMessage("Item movement failed.", Engine.DefaultFont, Hues.Load(38));
      }
    }

    protected virtual void SendDropPacket()
    {
      Network.Send((Packet) new PDropItem(this.pickUp.Serial, (int) (short) this.x, (int) (short) this.y, 0, this.dropTo.Serial));
    }
  }
}
