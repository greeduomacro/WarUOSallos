// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.MoveTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class MoveTargetHandler : ClientTargetHandler
  {
    private Item m_Item;
    private int? m_Amount;

    public MoveTargetHandler(int? amount)
    {
      this.m_Amount = amount;
    }

    protected override bool OnTarget(Item item)
    {
      if (this.m_Item == null)
      {
        this.m_Item = item;
        Engine.AddTextMessage("Target the destination container.");
        return false;
      }
      if (!item.IsContainer)
      {
        Engine.AddTextMessage("That is not a container.");
        return false;
      }
      Mobile player = World.Player;
      if (player != null)
      {
        Item backpack = player.Backpack;
        if (backpack != null)
        {
          int num1 = 0;
          foreach (Item pickUp in backpack.GetItems((IItemValidator) new ItemIDValidator(new int[1]{ this.m_Item.ID })))
          {
            if (this.m_Amount.HasValue)
            {
              int num2 = num1++;
              int? nullable = this.m_Amount;
              if ((num2 != nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
                break;
            }
            if (pickUp.Parent != item)
              new MoveContext(pickUp, pickUp.Amount, (IEntity) item, false).Enqueue();
          }
        }
      }
      return true;
    }
  }
}
