// Decompiled with JetBrains decompiler
// Type: PlayUO.ScavengerAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Sallos;
using System;
using System.Collections;

namespace PlayUO
{
  public class ScavengerAgent : PersistableObject, IItemValidator, IComparer
  {
    public static readonly PersistableType TypeCode = new PersistableType("scavenger", Construct, ItemRef.TypeCode);
    private ItemRefCollection m_Items;
    private ScavengerOptions m_Options;

    public override PersistableType TypeID
    {
      get
      {
        return ScavengerAgent.TypeCode;
      }
    }

    [Optionable("Reagents", "Scavenger", Default = true)]
    public bool Reagents
    {
      get
      {
        return this[ScavengerOptions.Reagents];
      }
      set
      {
        this[ScavengerOptions.Reagents] = value;
      }
    }

    [Optionable("Arrows & Bolts", "Scavenger", Default = false)]
    public bool Munitions
    {
      get
      {
        return this[ScavengerOptions.Munitions];
      }
      set
      {
        this[ScavengerOptions.Munitions] = value;
      }
    }

    [Optionable("Bolas", "Scavenger", Default = true)]
    public bool Bolas
    {
      get
      {
        return this[ScavengerOptions.Bolas];
      }
      set
      {
        this[ScavengerOptions.Bolas] = value;
      }
    }

    [Optionable("Artifacts", "Scavenger", Default = true, OnlyAOS = true)]
    public bool Artifacts
    {
      get
      {
        return this[ScavengerOptions.Artifacts];
      }
      set
      {
        this[ScavengerOptions.Artifacts] = value;
      }
    }

    public ItemRefCollection Items
    {
      get
      {
        return this.m_Items;
      }
    }

    public ItemRef this[PlayUO.Item item]
    {
      get
      {
        if (item != null)
        {
          foreach (ItemRef mItem in this.m_Items)
          {
            if (mItem.Serial == item.Serial)
              return mItem;
          }
        }
        return (ItemRef) null;
      }
    }

    public bool this[ScavengerOptions option]
    {
      get
      {
        return (this.m_Options & option) == option;
      }
      set
      {
        if (value)
          this.m_Options |= option;
        else
          this.m_Options &= ~option;
      }
    }

    public ScavengerAgent()
    {
      this.m_Items = new ItemRefCollection();
      this.m_Options = ScavengerOptions.Default;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ScavengerAgent();
    }

    public void Scavenge(bool isManual)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      PlayUO.Item[] items = World.FindItems((IItemValidator) new PlayerDistanceValidator((IItemValidator) new PickupValidator((IItemValidator) this), 2));
      if (items.Length == 0)
        return;
      Array.Sort((Array) items, (IComparer) this);
      bool clickFirst = false;
      for (int index = 0; index < items.Length; ++index)
      {
        PlayUO.Item pickUp = items[index];
        if (!Engine.Multis.RunUO_IsInside(pickUp.X, pickUp.Y, pickUp.Z) && new MoveContext(pickUp, pickUp.Amount, (IEntity) player, clickFirst).Enqueue())
        {
          int Amount = Math.Max(pickUp.Amount, 1);
          Engine.AddTextMessage(string.Format("Scavenging {0:N0} {1}", (object) Amount, (object) Map.ReplaceAmount(Map.GetTileName(pickUp.ID + 16384), Amount)), Engine.DefaultFont, Hues.Load(53));
        }
      }
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("options", (int) this.m_Options);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Options = (ScavengerOptions) ip.GetInt32("options");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Items.Count; ++index)
        this.m_Items[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild)
        this.m_Items.Add(ip.GetChild() as ItemRef);
    }

    public bool IsValid(PlayUO.Item check)
    {
      if (this.Reagents && ReagentValidator.Validator.IsValid(check) || this.Bolas && check.ID == 9900 || this.Munitions && (check.ID == 3903 || check.ID == 7163) || this.Artifacts && ArtifactValidator.Default.IsValid(check))
        return true;
      foreach (ItemRef mItem in this.m_Items)
      {
        if (mItem.ItemID == check.ID && (mItem.Serial == 0 || mItem.Serial == check.Serial))
          return true;
      }
      return false;
    }

    private int GetWorth(PlayUO.Item item)
    {
      return ReagentValidator.Validator.IsValid(item) ? 4 : 10;
    }

    public int Compare(object x, object y)
    {
      PlayUO.Item obj = x as PlayUO.Item;
      return this.GetWorth(y as PlayUO.Item) - this.GetWorth(obj);
    }
  }
}
