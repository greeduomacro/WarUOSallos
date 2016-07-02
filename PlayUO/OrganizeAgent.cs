// Decompiled with JetBrains decompiler
// Type: PlayUO.OrganizeAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using Sallos;
using System.Collections.Generic;

namespace PlayUO
{
  public class OrganizeAgent : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("organize", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[1]{ ItemRef.TypeCode });

    public virtual PersistableType TypeID
    {
      get
      {
        return OrganizeAgent.TypeCode;
      }
    }

    public bool TemplateSet { get; set; }

    public ItemRef TargetContainer { get; set; }

    public Point BlackPearlPos { get; set; }

    public Point BloodMossPos { get; set; }

    public Point GarlicPos { get; set; }

    public Point GinsengPos { get; set; }

    public Point MandrakeRootPos { get; set; }

    public Point NightshadePos { get; set; }

    public Point SpidersSilkPos { get; set; }

    public Point SulfurousAshPos { get; set; }

    public Point HealPotionPos { get; set; }

    public Point CurePotionPos { get; set; }

    public Point RefreshPotionPos { get; set; }

    public Point StrengthPotionPos { get; set; }

    public Point AgilityPotionPos { get; set; }

    public Point ExplosionPotionPos { get; set; }

    public OrganizeAgent()
    {
      base.\u002Ector();
      this.TemplateSet = false;
      this.TargetContainer = new ItemRef(0);
      this.BlackPearlPos = new Point(0, 0);
      this.BloodMossPos = new Point(0, 0);
      this.GarlicPos = new Point(0, 0);
      this.GinsengPos = new Point(0, 0);
      this.MandrakeRootPos = new Point(0, 0);
      this.NightshadePos = new Point(0, 0);
      this.SpidersSilkPos = new Point(0, 0);
      this.SulfurousAshPos = new Point(0, 0);
      this.HealPotionPos = new Point(0, 0);
      this.CurePotionPos = new Point(0, 0);
      this.RefreshPotionPos = new Point(0, 0);
      this.StrengthPotionPos = new Point(0, 0);
      this.AgilityPotionPos = new Point(0, 0);
      this.ExplosionPotionPos = new Point(0, 0);
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new OrganizeAgent();
    }

    private void OrganizeStackableItem(int itemID, Point destination, Item targetContainer)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      ItemIDValidator itemIdValidator = new ItemIDValidator(new int[1]{ itemID });
      Item pickUp1 = (Item) null;
      Item[] items1 = backpack.FindItems((IItemValidator) itemIdValidator);
      Item[] items2 = targetContainer.FindItems((IItemValidator) itemIdValidator);
      if (items1 == null || items1.Length <= 0)
        return;
      if (items2 != null)
      {
        foreach (Item obj in items2)
        {
          if (obj.Parent == targetContainer && obj.X == destination.X && obj.Y == destination.Y)
          {
            pickUp1 = obj;
            break;
          }
        }
      }
      if (pickUp1 == null)
      {
        pickUp1 = items1[0];
        MoveContext moveContext = new MoveContext(pickUp1, pickUp1.Amount, (IEntity) targetContainer, false);
        moveContext.Locate(destination.X, destination.Y);
        moveContext.Enqueue();
      }
      foreach (Item pickUp2 in items1)
      {
        if (pickUp2 != pickUp1)
          new MoveContext(pickUp2, pickUp2.Amount, (IEntity) pickUp1, false).Enqueue();
      }
    }

    private void OrganizeNonstackableItem(int itemID, Point destination, Item targetContainer)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      Item[] items = backpack.FindItems((IItemValidator) new ItemIDValidator(new int[1]{ itemID }));
      if (items == null || items.Length <= 0)
        return;
      foreach (Item pickUp in items)
      {
        if (pickUp.Parent != targetContainer || pickUp.X != destination.X || pickUp.Y != destination.Y)
        {
          MoveContext moveContext = new MoveContext(pickUp, 1, (IEntity) targetContainer, false);
          moveContext.Locate(destination.X, destination.Y);
          moveContext.Enqueue();
        }
      }
    }

    public Dictionary<int, Point> GetTemplate()
    {
      Dictionary<int, Point> dictionary = new Dictionary<int, Point>();
      dictionary[3962] = this.BlackPearlPos;
      dictionary[3963] = this.BloodMossPos;
      dictionary[3972] = this.GarlicPos;
      dictionary[3973] = this.GinsengPos;
      dictionary[3974] = this.MandrakeRootPos;
      dictionary[3976] = this.NightshadePos;
      dictionary[3981] = this.SpidersSilkPos;
      dictionary[3980] = this.SulfurousAshPos;
      dictionary[3852] = this.HealPotionPos;
      dictionary[3847] = this.CurePotionPos;
      dictionary[3851] = this.RefreshPotionPos;
      dictionary[3849] = this.StrengthPotionPos;
      dictionary[3848] = this.AgilityPotionPos;
      dictionary[3853] = this.ExplosionPotionPos;
      return dictionary;
    }

    public void Invoke()
    {
      if (this.TemplateSet)
      {
        Item onPlayer = this.TargetContainer.FindOnPlayer();
        if (onPlayer != null)
        {
          Point point = new Point(0, 0);
          foreach (KeyValuePair<int, Point> keyValuePair in this.GetTemplate())
          {
            int key = keyValuePair.Key;
            if (keyValuePair.Key != 3853)
              this.OrganizeStackableItem(keyValuePair.Key, keyValuePair.Value, onPlayer);
            else
              this.OrganizeNonstackableItem(keyValuePair.Key, keyValuePair.Value, onPlayer);
          }
        }
        else
        {
          TargetManager.Client = (ClientTargetHandler) new SetOrganizeTargetTargetHandler(true);
          Engine.AddTextMessage("Target your lootbag.");
        }
      }
      else
      {
        TargetManager.Client = (ClientTargetHandler) new SetOrganizeTemplateTargetHandler(true);
        Engine.AddTextMessage("Target your template lootbag.");
      }
    }

    public void SetTemplate(Item item)
    {
      Item obj1 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3962 }));
      Item obj2 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3963 }));
      Item obj3 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3972 }));
      Item obj4 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3973 }));
      Item obj5 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3974 }));
      Item obj6 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3976 }));
      Item obj7 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3981 }));
      Item obj8 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3980 }));
      Item obj9 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3852 }));
      Item obj10 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3847 }));
      Item obj11 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3851 }));
      Item obj12 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3849 }));
      Item obj13 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3848 }));
      Item obj14 = item.FindItem((IItemValidator) new ItemIDValidator(new int[1]{ 3853 }));
      this.BlackPearlPos = obj1 != null ? new Point(obj1.X, obj1.Y) : new Point(0, 0);
      this.BloodMossPos = obj2 != null ? new Point(obj2.X, obj2.Y) : new Point(0, 0);
      this.GarlicPos = obj3 != null ? new Point(obj3.X, obj3.Y) : new Point(0, 0);
      this.GinsengPos = obj4 != null ? new Point(obj4.X, obj4.Y) : new Point(0, 0);
      this.MandrakeRootPos = obj5 != null ? new Point(obj5.X, obj5.Y) : new Point(0, 0);
      this.NightshadePos = obj6 != null ? new Point(obj6.X, obj6.Y) : new Point(0, 0);
      this.SpidersSilkPos = obj7 != null ? new Point(obj7.X, obj7.Y) : new Point(0, 0);
      this.SulfurousAshPos = obj8 != null ? new Point(obj8.X, obj8.Y) : new Point(0, 0);
      this.HealPotionPos = obj9 != null ? new Point(obj9.X, obj9.Y) : new Point(0, 0);
      this.CurePotionPos = obj10 != null ? new Point(obj10.X, obj10.Y) : new Point(0, 0);
      this.RefreshPotionPos = obj11 != null ? new Point(obj11.X, obj11.Y) : new Point(0, 0);
      this.StrengthPotionPos = obj12 != null ? new Point(obj12.X, obj12.Y) : new Point(0, 0);
      this.AgilityPotionPos = obj13 != null ? new Point(obj13.X, obj13.Y) : new Point(0, 0);
      this.ExplosionPotionPos = obj14 != null ? new Point(obj14.X, obj14.Y) : new Point(0, 0);
      this.TemplateSet = true;
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetBoolean("template-set", this.TemplateSet);
      op.SetPoint("black-pearl", (System.Drawing.Point) this.BlackPearlPos);
      op.SetPoint("blood-moss", (System.Drawing.Point) this.BloodMossPos);
      op.SetPoint("garlic", (System.Drawing.Point) this.GarlicPos);
      op.SetPoint("ginseng", (System.Drawing.Point) this.GinsengPos);
      op.SetPoint("mandrake-root", (System.Drawing.Point) this.MandrakeRootPos);
      op.SetPoint("nightshade", (System.Drawing.Point) this.NightshadePos);
      op.SetPoint("spiders-silk", (System.Drawing.Point) this.SpidersSilkPos);
      op.SetPoint("sulfurous-ash", (System.Drawing.Point) this.SulfurousAshPos);
      op.SetPoint("heal-potion", (System.Drawing.Point) this.HealPotionPos);
      op.SetPoint("cure-potion", (System.Drawing.Point) this.CurePotionPos);
      op.SetPoint("refresh-potion", (System.Drawing.Point) this.RefreshPotionPos);
      op.SetPoint("strength-potion", (System.Drawing.Point) this.StrengthPotionPos);
      op.SetPoint("agility-potion", (System.Drawing.Point) this.AgilityPotionPos);
      op.SetPoint("explosion-potion", (System.Drawing.Point) this.ExplosionPotionPos);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.TemplateSet = ip.GetBoolean("template-set");
      this.BlackPearlPos = (Point) ip.GetPoint("black-pearl");
      this.BloodMossPos = (Point) ip.GetPoint("blood-moss");
      this.GarlicPos = (Point) ip.GetPoint("garlic");
      this.GinsengPos = (Point) ip.GetPoint("ginseng");
      this.MandrakeRootPos = (Point) ip.GetPoint("mandrake-root");
      this.NightshadePos = (Point) ip.GetPoint("nightshade");
      this.SpidersSilkPos = (Point) ip.GetPoint("spiders-silk");
      this.SulfurousAshPos = (Point) ip.GetPoint("sulfurous-ash");
      this.HealPotionPos = (Point) ip.GetPoint("heal-potion");
      this.CurePotionPos = (Point) ip.GetPoint("cure-potion");
      this.RefreshPotionPos = (Point) ip.GetPoint("refresh-potion");
      this.StrengthPotionPos = (Point) ip.GetPoint("strength-potion");
      this.AgilityPotionPos = (Point) ip.GetPoint("agility-potion");
      this.ExplosionPotionPos = (Point) ip.GetPoint("explosion-potion");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.TargetContainer.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      this.TargetContainer = (ip.get_HasChild() ? ip.GetChild() as ItemRef : (ItemRef) null) ?? new ItemRef(0);
    }
  }
}
