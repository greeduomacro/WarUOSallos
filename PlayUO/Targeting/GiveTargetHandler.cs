// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.GiveTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Targeting
{
  internal class GiveTargetHandler : ClientTargetHandler
  {
    private GiveEntry m_Entry;
    private int m_DesiredAmount;

    public GiveTargetHandler(GiveEntry entry, int desiredAmount)
    {
      this.m_Entry = entry;
      this.m_DesiredAmount = desiredAmount;
    }

    protected override bool OnTarget(Mobile mob)
    {
      this.GiveTo((IEntity) mob);
      return true;
    }

    protected override bool OnTarget(Item item)
    {
      if (!item.IsContainer)
        return false;
      this.GiveTo((IEntity) item);
      return true;
    }

    private void GiveTo(IEntity recipient)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      int num1 = this.m_DesiredAmount;
      IItemValidator[] validators = this.m_Entry.Validators;
      Item[][] objArray1 = new Item[validators.Length][];
      int[] numArray = new int[validators.Length];
      for (int index1 = 0; index1 < validators.Length; ++index1)
      {
        objArray1[index1] = backpack.FindItems(validators[index1]);
        int currentAmount = 0;
        for (int index2 = 0; index2 < objArray1[index1].Length; ++index2)
          currentAmount += Math.Max(1, objArray1[index1][index2].Amount);
        int num2 = num1 != -1 ? num1 : this.m_Entry.GetAmount(currentAmount);
        if (num2 > currentAmount)
        {
          Engine.AddTextMessage("You do not have enough to give them.");
          return;
        }
        numArray[index1] = num2;
      }
      for (int index1 = 0; index1 < validators.Length; ++index1)
      {
        Item[] objArray2 = objArray1[index1];
        int val1 = numArray[index1];
        for (int index2 = 0; index2 < objArray2.Length && val1 > 0; ++index2)
        {
          Item pickUp = objArray2[index2];
          int amount = Math.Min(val1, Math.Max(1, pickUp.Amount));
          new MoveContext(pickUp, amount, recipient, false).Enqueue();
          val1 -= amount;
        }
      }
    }
  }
}
