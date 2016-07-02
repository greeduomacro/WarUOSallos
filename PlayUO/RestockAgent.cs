// Decompiled with JetBrains decompiler
// Type: PlayUO.RestockAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using Sallos;
using System;
using System.Collections.Generic;
using System.Linq;
using Ultima.Client;

namespace PlayUO
{
  public class RestockAgent : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("restock", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ ItemRef.TypeCode });

    public virtual PersistableType TypeID
    {
      get
      {
        return RestockAgent.TypeCode;
      }
    }

    public int ReagentCount { get; set; }

    public int HealPotionCount { get; set; }

    public int CurePotionCount { get; set; }

    public int RefreshPotionCount { get; set; }

    public int StrengthPotionCount { get; set; }

    public int AgilityPotionCount { get; set; }

    public int ExplosionPotionCount { get; set; }

    public ItemRef TargetContainer { get; set; }

    public ItemRef SourceContainer { get; set; }

    public RestockAgent()
    {
      base.\u002Ector();
      this.ReagentCount = 100;
      this.HealPotionCount = 15;
      this.CurePotionCount = 15;
      this.RefreshPotionCount = 15;
      this.StrengthPotionCount = 10;
      this.AgilityPotionCount = 10;
      this.ExplosionPotionCount = 0;
      this.TargetContainer = new ItemRef(0);
      this.SourceContainer = new ItemRef(0);
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new RestockAgent();
    }

    private static void Transfer(Item sourceContainer, Item targetContainer, int amountDesired, int itemId, IItemValidator predicate)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      Item pickUp1 = targetContainer.FindItem(predicate);
      Point point;
      bool flag = Player.Current.OrganizeAgent.GetTemplate().TryGetValue(itemId, out point) && point.X != 0 && point.Y != 0;
      int num = 0;
      foreach (Item pickUp2 in backpack.FindItems(predicate))
      {
        Item obj = pickUp1 == null || !pickUp1.IsStackable ? targetContainer : pickUp1;
        MoveContext moveContext = new MoveContext(pickUp2, pickUp2.Amount, (IEntity) obj, false);
        if (flag)
          moveContext.Locate(point.X, point.Y);
        moveContext.TryEnqueue();
        num += pickUp2.Amount;
        if (pickUp1 == null)
          pickUp1 = pickUp2;
      }
      if (num > amountDesired)
      {
        new MoveContext(pickUp1, num - amountDesired, (IEntity) sourceContainer, false).Enqueue();
      }
      else
      {
        if (num >= amountDesired)
          return;
        Item equip = player.FindEquip(Layer.Bank);
        foreach (Item pickUp2 in sourceContainer.FindItems(predicate))
        {
          if (!pickUp2.IsChildOf((Agent) player) || pickUp2.IsChildOf((Agent) equip))
          {
            int amount = Math.Min(pickUp2.Amount, amountDesired - num);
            MoveContext moveContext = new MoveContext(pickUp2, amount, (IEntity) targetContainer, false);
            if (flag)
              moveContext.Locate(point.X, point.Y);
            moveContext.Enqueue();
            num += amount;
            if (num == amountDesired)
              break;
          }
        }
        if (num >= amountDesired)
          return;
        Engine.AddTextMessage(string.Format("Unable to find sufficient quantity of {0}.", (object) Localization.GetString(1020000 + (itemId & 16383))), Engine.DefaultFont, Hues.Load(38));
      }
    }

    public void Invoke()
    {
      Mobile me = World.Player;
      if (me == null)
        return;
      Item obj = this.SourceContainer.Find();
      if (obj != null)
      {
        if (obj.HasContainerContent)
        {
          Item onPlayer = this.TargetContainer.FindOnPlayer();
          if (onPlayer != null)
          {
            if (onPlayer.HasContainerContent)
            {
              if (this.ReagentCount <= 0)
                return;
              foreach (var data in ((IEnumerable<int>) ReagentValidator.Validator.List).Select(itemId => new{ ItemId = itemId, Quantity = this.ReagentCount }).Concat(((IEnumerable<int>) PotionValidator.Validator.List).Select(itemId => new{ ItemId = itemId, Quantity = 15 })).Concat(((IEnumerable<int>) new int[1]{ 4129 }).Select(itemId => new{ ItemId = itemId, Quantity = 5 })))
              {
                IItemValidator predicate = (IItemValidator) new PredicateValidator((IItemValidator) new PlayerDistanceValidator((IItemValidator) new PickupValidator((IItemValidator) new ItemIDValidator(new int[1]{ data.ItemId })), 2), (Predicate<Item>) (item =>
                {
                  if (item.Parent == null)
                    return false;
                  if (!(item.WorldRoot is Item))
                    return item.WorldRoot == me;
                  return true;
                }));
                RestockAgent.Transfer(obj, onPlayer, data.Quantity, data.ItemId, predicate);
              }
            }
            else
              new OpenRestockContainerContext(onPlayer).Enqueue();
          }
          else
          {
            TargetManager.Client = (ClientTargetHandler) new SetRestockTargetTargetHandler(true);
            Engine.AddTextMessage("Target your lootbag.");
          }
        }
        else
          new OpenRestockContainerContext(obj).Enqueue();
      }
      else
      {
        TargetManager.Client = (ClientTargetHandler) new SetRestockSourceTargetHandler(true);
        Engine.AddTextMessage("Target your restock source container.");
      }
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("reagent-count", this.ReagentCount);
      op.SetInt32("heal-potion-count", this.HealPotionCount);
      op.SetInt32("cure-potion-count", this.CurePotionCount);
      op.SetInt32("refresh-potion-count", this.RefreshPotionCount);
      op.SetInt32("strength-potion-count", this.StrengthPotionCount);
      op.SetInt32("agility-potion-count", this.AgilityPotionCount);
      op.SetInt32("explosion-potion-count", this.ExplosionPotionCount);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.ReagentCount = ip.GetInt32("reagent-count");
      this.HealPotionCount = ip.GetInt32("heal-potion-count");
      this.CurePotionCount = ip.GetInt32("cure-potion-count");
      this.RefreshPotionCount = ip.GetInt32("refresh-potion-count");
      this.StrengthPotionCount = ip.GetInt32("strength-potion-count");
      this.AgilityPotionCount = ip.GetInt32("agility-potion-count");
      this.ExplosionPotionCount = ip.GetInt32("explosion-potion-count");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.TargetContainer.Serialize(op);
      this.SourceContainer.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      this.TargetContainer = (ip.get_HasChild() ? ip.GetChild() as ItemRef : (ItemRef) null) ?? new ItemRef(0);
      this.SourceContainer = (ip.get_HasChild() ? ip.GetChild() as ItemRef : (ItemRef) null) ?? new ItemRef(0);
    }
  }
}
