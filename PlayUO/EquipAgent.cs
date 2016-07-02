// Decompiled with JetBrains decompiler
// Type: PlayUO.EquipAgent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Sallos;
using System.Collections.Generic;

namespace PlayUO
{
  public class EquipAgent : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("equip", Construct, ArmingAgent.TypeCode, DressAgent.TypeCode, ItemRef.TypeCode);
    private EquipAgent.ArmingAgent arms;
    private EquipAgent.DressAgent dress;
    private ItemRef mount;

    public override PersistableType TypeID
    {
      get
      {
        return EquipAgent.TypeCode;
      }
    }

    public EquipAgent.ArmingAgent Arms
    {
      get
      {
        return this.arms;
      }
    }

    public EquipAgent.DressAgent Dress
    {
      get
      {
        return this.dress;
      }
    }

    public ItemRef Mount
    {
      get
      {
        return this.mount;
      }
    }

    public EquipAgent()
    {
      this.arms = new EquipAgent.ArmingAgent();
      this.dress = new EquipAgent.DressAgent();
      this.mount = new ItemRef(0);
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new EquipAgent();
    }

    public void UpdateEquipment()
    {
      Mobile player = World.Player;
      if (player == null || player.Ghost)
        return;
      foreach (Item obj in player.Items)
      {
        Layer layer = obj.Layer;
        if (layer >= Layer.Shoes && layer <= Layer.InnerLegs && (layer != Layer.Backpack && layer != Layer.FacialHair) && layer != Layer.Hair)
          this.dress.Populate(layer, obj);
        else if (layer == Layer.Mount)
          this.mount.Serial = obj.Serial;
      }
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this.arms.Serialize(op);
      this.dress.Serialize(op);
      if (this.mount.IsNull)
        return;
      this.mount.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      this.arms = ip.GetChild() as EquipAgent.ArmingAgent;
      this.dress = ip.GetChild() as EquipAgent.DressAgent;
      if (ip.HasChild)
        this.mount = ip.GetChild() as ItemRef;
      else
        this.mount = new ItemRef(0);
    }

    public class ArmingAgent : PersistableObject
    {
      public static readonly PersistableType TypeCode = new PersistableType("arms", Construct, Slot.TypeCode);
      private EquipAgent.ArmingAgent.Slot[] slots;

      public override PersistableType TypeID
      {
        get
        {
          return EquipAgent.ArmingAgent.TypeCode;
        }
      }

      public ArmingAgent()
      {
        this.slots = new EquipAgent.ArmingAgent.Slot[10];
      }

      private static PersistableObject Construct()
      {
        return (PersistableObject) new EquipAgent.ArmingAgent();
      }

      public void Dequip()
      {
        this.Dequip(true);
      }

      public void Dequip(bool message)
      {
        Mobile player = World.Player;
        if (player == null)
          return;
        if (player.Ghost)
        {
          if (!message)
            return;
          Engine.AddTextMessage("You are dead.");
        }
        else if (Gumps.Drag != null && Gumps.Drag.GetType() == typeof (GDraggedItem))
        {
          if (!message)
            return;
          Engine.AddTextMessage("You are already dragging an item.");
        }
        else
        {
          Item pickUp = player.FindEquip(Layer.TwoHanded) ?? player.FindEquip(Layer.OneHanded);
          if (pickUp == null)
          {
            if (!message)
              return;
            Engine.AddTextMessage("You are not holding anything.");
          }
          else
            new MoveContext(pickUp, pickUp.Amount, (IEntity) player, false).Enqueue();
        }
      }

      public void Equip(int index)
      {
        Mobile player = World.Player;
        if (player == null)
          return;
        if (player.Ghost)
          Engine.AddTextMessage("You are dead.");
        else if (Gumps.Drag != null && Gumps.Drag.GetType() == typeof (GDraggedItem))
        {
          Engine.AddTextMessage("You are already dragging an item.");
        }
        else
        {
          EquipAgent.ArmingAgent.Slot slot = this.slots[index];
          if (slot == null)
            return;
          Item onPlayer = slot.FindOnPlayer();
          if (onPlayer == null)
          {
            Engine.AddTextMessage("Equipment not found.");
          }
          else
          {
            if (onPlayer.Parent == player)
              return;
            new EquipContext(onPlayer, onPlayer.Amount, player, false).Enqueue();
          }
        }
      }

      public void Assign(int index, Item item)
      {
        this.slots[index] = new EquipAgent.ArmingAgent.Slot(index, item);
      }

      protected virtual void SerializeChildren(PersistanceWriter op)
      {
        for (int index = 0; index < this.slots.Length; ++index)
        {
          if (this.slots[index] != null)
            this.slots[index].Serialize(op);
        }
      }

      protected virtual void DeserializeChildren(PersistanceReader ip)
      {
        while (ip.HasChild)
        {
          EquipAgent.ArmingAgent.Slot slot = ip.GetChild() as EquipAgent.ArmingAgent.Slot;
          this.slots[slot.Index] = slot;
        }
      }

      private class Slot : ItemRef
      {
        public new static readonly PersistableType TypeCode = new PersistableType("slot", Construct);
        private int index;

        public override PersistableType TypeID
        {
          get
          {
            return EquipAgent.ArmingAgent.Slot.TypeCode;
          }
        }

        public int Index
        {
          get
          {
            return this.index;
          }
        }

        public Slot()
        {
        }

        public Slot(int index, Item item)
          : base(item)
        {
          this.index = index;
        }

        private static PersistableObject Construct()
        {
          return (PersistableObject) new EquipAgent.ArmingAgent.Slot();
        }

        protected override void SerializeAttributes(PersistanceWriter op)
        {
          op.SetInt32("index", this.index);
          base.SerializeAttributes(op);
        }

        protected override void DeserializeAttributes(PersistanceReader ip)
        {
          this.index = ip.GetInt32("index");
          base.DeserializeAttributes(ip);
        }
      }
    }

    public class DressAgent : PersistableObject
    {
      public static readonly PersistableType TypeCode = new PersistableType("dress", Construct, Slot.TypeCode);
      private List<EquipAgent.DressAgent.Slot> slots;

      public override PersistableType TypeID
      {
        get
        {
          return EquipAgent.DressAgent.TypeCode;
        }
      }

      public DressAgent()
      {
        this.slots = new List<EquipAgent.DressAgent.Slot>();
      }

      private static PersistableObject Construct()
      {
        return (PersistableObject) new EquipAgent.DressAgent();
      }

      public void Remove(Item item)
      {
        foreach (EquipAgent.DressAgent.Slot slot in this.slots)
        {
          if (slot.Serial == item.Serial)
          {
            this.slots.Remove(slot);
            break;
          }
        }
      }

      public void EnsureDressed()
      {
        Mobile player = World.Player;
        if (player == null)
          return;
        if (player.Ghost)
        {
          Engine.AddTextMessage("You are dead.");
        }
        else
        {
          foreach (ItemRef slot in this.slots)
          {
            Item onPlayer = slot.FindOnPlayer();
            if (onPlayer != null && !player.HasEquip(onPlayer))
              new EquipContext(onPlayer, onPlayer.Amount, player, false).Enqueue();
          }
        }
      }

      public void Populate(Layer layer, Item item)
      {
        bool flag = false;
        foreach (EquipAgent.DressAgent.Slot slot in this.slots)
        {
          if (slot.Layer == layer)
          {
            slot.Serial = item.Serial;
            slot.ItemID = item.ID;
            flag = true;
            break;
          }
        }
        if (flag)
          return;
        this.slots.Add(new EquipAgent.DressAgent.Slot(layer, item));
      }

      protected virtual void SerializeChildren(PersistanceWriter op)
      {
        foreach (PersistableObject slot in this.slots)
          slot.Serialize(op);
      }

      protected virtual void DeserializeChildren(PersistanceReader ip)
      {
        while (ip.HasChild)
          this.slots.Add(ip.GetChild() as EquipAgent.DressAgent.Slot);
      }

      private class Slot : ItemRef
      {
        public new static readonly PersistableType TypeCode = new PersistableType("slot", Construct);
        private Layer layer;

        public override PersistableType TypeID
        {
          get
          {
            return EquipAgent.DressAgent.Slot.TypeCode;
          }
        }

        public Layer Layer
        {
          get
          {
            return this.layer;
          }
        }

        public Slot()
        {
        }

        public Slot(Layer layer, Item item)
          : base(item)
        {
          this.layer = layer;
        }

        private static PersistableObject Construct()
        {
          return (PersistableObject) new EquipAgent.DressAgent.Slot();
        }

        protected override void SerializeAttributes(PersistanceWriter op)
        {
          op.SetInt32("layer", (int) this.layer);
          base.SerializeAttributes(op);
        }

        protected override void DeserializeAttributes(PersistanceReader ip)
        {
          this.layer = (Layer) ip.GetInt32("layer");
          base.DeserializeAttributes(ip);
        }
      }
    }
  }
}
