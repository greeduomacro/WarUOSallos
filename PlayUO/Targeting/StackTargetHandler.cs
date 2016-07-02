// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.StackTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class StackTargetHandler : ClientTargetHandler
  {
    protected override bool OnTarget(Item item)
    {
      Mobile player = World.Player;
      if (player != null)
      {
        Item backpack = player.Backpack;
        if (backpack != null)
        {
          foreach (Item pickUp in backpack.GetItems((IItemValidator) new ItemIDValidator(new int[1]{ item.ID })))
          {
            if (pickUp != item)
            {
              MoveContext moveContext = new MoveContext(pickUp, pickUp.Amount, (IEntity) item, false);
              moveContext.Locate(item.X, item.Y);
              moveContext.Enqueue();
            }
          }
        }
      }
      return true;
    }
  }
}
