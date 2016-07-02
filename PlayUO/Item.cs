// Decompiled with JetBrains decompiler
// Type: PlayUO.Item
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public class Item : PhysicalAgent, IComparable, IMessageOwner, IPoint2D, IAnimationOwner
  {
    private static List<Item> _emptyItems = new List<Item>();
    private int m_Revision = -1;
    private int m_LastSpell = -1;
    private int m_BookIconX = 25;
    private int m_BookIconY = 25;
    private Layer _layer;
    private int m_Props;
    private DateTime m_NextQueryProps;
    private ushort m_Hue;
    private byte m_Direction;
    private List<Item> m_CorpseItems;
    private int m_CorpseSerial;
    private ItemFlags m_Flags;
    private bool m_TradeCheck1;
    private bool m_TradeCheck2;
    private bool m_ShouldOverrideHue;
    private int m_RealHue;
    private IHue m_LastTextHue;
    public string m_LastText;
    private Multi m_Multi;
    private int m_MessageFrame;
    private int m_MessageX;
    private int m_MessageY;
    private int m_BottomY;
    private bool m_QueueOpenSB;
    private Mobile m_LastLooked;
    private Texture m_tPool;
    private VertexCache m_vCache;
    private IHue m_hAnimationPool;
    private int m_iAnimationPool;
    private Frames m_fAnimationPool;
    private RestoreInfo m_RestoreInfo;
    private int m_SpellbookOffset;
    private int m_SpellbookGraphic;
    private long m_SpellContained;
    private int m_Circle;
    private bool m_OpenSB;
    private List<Item> corpseSortedItems;
    private LayerComparer corpseSortComparer;
    private static Queue<Item> m_FindItem_Queue;
    private ushort amount;
    private ushort itemId;
    private ushort visage;
    private GPaperdollItem _paperdollItem;

    public ItemId ItemId
    {
      get
      {
        return (ItemId) (int) this.itemId;
      }
    }

    public unsafe ItemData* ItemDataPointer
    {
      get
      {
        return Map.GetItemDataPointer(this.ItemId);
      }
    }

    public unsafe int Height
    {
      get
      {
        return (int) this.ItemDataPointer->Height;
      }
    }

    public unsafe int AnimationId
    {
      get
      {
        if (this.Layer == Layer.Mount)
          return Engine.m_Animations.ConvertMountItemToBody(this.ID);
        return (int) this.ItemDataPointer->AnimationId;
      }
    }

    public unsafe Layer Layer
    {
      get
      {
        if (this._layer == Layer.Invalid)
          return (Layer) this.ItemDataPointer->quality_layer_light;
        return this._layer;
      }
      set
      {
        if (this._layer == value)
          return;
        this._layer = value;
        this.RaiseUpdateEvents();
      }
    }

    public int PropertyID
    {
      get
      {
        return this.m_Props;
      }
      set
      {
        if (this.m_Props != value)
          this.m_NextQueryProps = DateTime.Now;
        this.m_Props = value;
      }
    }

    public ObjectPropertyList PropertyList
    {
      get
      {
        return ObjectPropertyList.Find(this.Serial, this.m_Props);
      }
    }

    public bool HasContainerContent { get; set; }

    public long SpellContained
    {
      get
      {
        return this.m_SpellContained;
      }
      set
      {
        this.m_SpellContained = value;
      }
    }

    public int SpellbookOffset
    {
      get
      {
        return this.m_SpellbookOffset;
      }
      set
      {
        this.m_SpellbookOffset = value;
      }
    }

    public int SpellbookGraphic
    {
      get
      {
        return this.m_SpellbookGraphic;
      }
      set
      {
        this.m_SpellbookGraphic = value;
      }
    }

    public int Revision
    {
      get
      {
        return this.m_Revision;
      }
      set
      {
        this.m_Revision = value;
      }
    }

    public int BookIconX
    {
      get
      {
        return this.m_BookIconX;
      }
      set
      {
        this.m_BookIconX = value;
      }
    }

    public int BookIconY
    {
      get
      {
        return this.m_BookIconY;
      }
      set
      {
        this.m_BookIconY = value;
      }
    }

    public int Circle
    {
      get
      {
        return this.m_Circle;
      }
      set
      {
        this.m_Circle = value;
      }
    }

    public int LastSpell
    {
      get
      {
        return this.m_LastSpell;
      }
      set
      {
        this.m_LastSpell = value;
      }
    }

    public bool IsDoor
    {
      get
      {
        int num = (int) this.itemId;
        if ((this.TileFlags & 536870912L) != 0L || num == 1682 || (num == 2118 || num == 2163))
          return true;
        if (num >= 1781)
          return num <= 1782;
        return false;
      }
    }

    public RestoreInfo RestoreInfo
    {
      get
      {
        return this.m_RestoreInfo;
      }
      set
      {
        this.m_RestoreInfo = value;
      }
    }

    public Mobile LastLooked
    {
      get
      {
        return this.m_LastLooked;
      }
      set
      {
        this.m_LastLooked = value;
      }
    }

    public bool QueueOpenSB
    {
      get
      {
        return this.m_QueueOpenSB;
      }
      set
      {
        this.m_QueueOpenSB = value;
      }
    }

    public int MessageFrame
    {
      get
      {
        return this.m_MessageFrame;
      }
      set
      {
        this.m_MessageFrame = value;
      }
    }

    public int MessageX
    {
      get
      {
        return this.m_MessageX;
      }
      set
      {
        this.m_MessageX = value;
      }
    }

    public int MessageY
    {
      get
      {
        return this.m_MessageY;
      }
      set
      {
        this.m_MessageY = value;
      }
    }

    public int BottomY
    {
      get
      {
        return this.m_BottomY;
      }
      set
      {
        this.m_BottomY = value;
      }
    }

    public Multi Multi
    {
      get
      {
        return this.m_Multi;
      }
      set
      {
        this.m_Multi = value;
      }
    }

    public IHue LastTextHue
    {
      get
      {
        return this.m_LastTextHue;
      }
      set
      {
        this.m_LastTextHue = value;
      }
    }

    public bool IsCorpse
    {
      get
      {
        return (int) this.itemId == 8198;
      }
    }

    public bool IsBones
    {
      get
      {
        if ((int) this.itemId >= 3786)
          return (int) this.itemId <= 3794;
        return false;
      }
    }

    public bool TradeCheck1
    {
      get
      {
        return this.m_TradeCheck1;
      }
      set
      {
        this.m_TradeCheck1 = value;
      }
    }

    public bool TradeCheck2
    {
      get
      {
        return this.m_TradeCheck2;
      }
      set
      {
        this.m_TradeCheck2 = value;
      }
    }

    public bool OpenSB
    {
      get
      {
        return this.m_OpenSB;
      }
      set
      {
        this.m_OpenSB = value;
      }
    }

    public bool IsMulti
    {
      get
      {
        return this.Multi != null;
      }
    }

    public bool InTradeWindow
    {
      get
      {
        for (Item obj = this; obj != null; obj = obj.Parent as Item)
        {
          if (obj.Container != null && obj.Container != null && obj.Container.m_TradeContainer)
            return true;
        }
        return false;
      }
    }

    public GContainer Container
    {
      get
      {
        return (GContainer) this.ContainerView;
      }
    }

    public ItemFlags Flags
    {
      get
      {
        return this.m_Flags;
      }
      set
      {
        this.m_Flags = value;
        if ((this.m_Flags.Value & -161) == 0)
          return;
        string Message = string.Format("Unknown item flags: 0x{0:X2}", (object) this.m_Flags.Value);
        Debug.Trace(Message);
        Engine.AddTextMessage(Message);
      }
    }

    public int CorpseSerial
    {
      get
      {
        return this.m_CorpseSerial;
      }
      set
      {
        this.m_CorpseSerial = value;
      }
    }

    public List<Item> CorpseItems
    {
      get
      {
        return this.m_CorpseItems ?? Item._emptyItems;
      }
    }

    public unsafe int Weight
    {
      get
      {
        return (int) this.ItemDataPointer->Weight;
      }
    }

    public unsafe TileFlag TileFlags
    {
      get
      {
        return (TileFlag) this.ItemDataPointer->Flags;
      }
    }

    public bool IsStacked
    {
      get
      {
        if (this.IsStackable)
          return this.Amount > 1;
        return false;
      }
    }

    public bool IsPilable
    {
      get
      {
        return false;
      }
    }

    public bool IsMovable
    {
      get
      {
        if (!this.Flags[ItemFlag.CanMove])
          return this.Weight <= 90;
        return true;
      }
    }

    public bool IsWearable
    {
      get
      {
        return (this.TileFlags & 4194304L) != 0L;
      }
    }

    public bool IsContainer
    {
      get
      {
        return (this.TileFlags & 2097152L) != 0L;
      }
    }

    public bool IsStackable
    {
      get
      {
        return (this.TileFlags & 2048L) != 0L;
      }
    }

    public ushort Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if (this.m_ShouldOverrideHue)
        {
          this.m_RealHue = (int) value;
        }
        else
        {
          if ((int) this.m_Hue == (int) value)
            return;
          this.m_Hue = value;
          this.RaiseUpdateEvents();
        }
      }
    }

    public byte Direction
    {
      get
      {
        return this.m_Direction;
      }
      set
      {
        this.m_Direction = value;
      }
    }

    public int Amount
    {
      get
      {
        return (int) this.amount;
      }
      set
      {
        ushort num = checked ((ushort) value);
        if ((int) this.amount == (int) num)
          return;
        this.amount = num;
        this.RaiseUpdateEvents();
      }
    }

    public int ID
    {
      get
      {
        return (int) this.itemId;
      }
      set
      {
        ushort num = checked ((ushort) value);
        if ((int) this.itemId == (int) num)
          return;
        this.itemId = num;
        this.RaiseUpdateEvents();
      }
    }

    public GPaperdollItem PaperdollItem
    {
      get
      {
        return this._paperdollItem;
      }
      set
      {
        this._paperdollItem = value;
      }
    }

    public Item(int serial)
      : base(serial)
    {
      this.m_Flags = new ItemFlags();
    }

    public void QueryProperties()
    {
      if (!Engine.ServerFeatures.AOS || DateTime.Now < this.m_NextQueryProps)
        return;
      this.m_NextQueryProps = DateTime.Now + TimeSpan.FromSeconds(1.0);
      Network.Send((Packet) new PQueryProperties(this.Serial));
    }

    public void SetSpellContained(int index, bool value)
    {
      long num = 1L << index;
      if (value)
        this.m_SpellContained |= num;
      else
        this.m_SpellContained &= ~num;
    }

    protected override void OnDeleted()
    {
      base.OnDeleted();
      if (this.Multi != null)
      {
        Engine.Multis.Unregister(this);
        this.Multi = (Multi) null;
      }
      this.HasContainerContent = false;
    }

    public bool GetSpellContained(int index)
    {
      return (this.m_SpellContained & 1L << index) != 0L;
    }

    public void Query()
    {
    }

    Frames IAnimationOwner.GetOwnedFrames(IHue hue, int realID)
    {
      if (this.m_iAnimationPool == realID && this.m_hAnimationPool == hue && !this.m_fAnimationPool.Disposed)
        return this.m_fAnimationPool;
      this.m_fAnimationPool = hue.GetAnimation(realID);
      this.m_hAnimationPool = hue;
      this.m_iAnimationPool = realID;
      return this.m_fAnimationPool;
    }

    public void Draw(Texture t, int x, int y)
    {
      if (this.m_vCache == null)
        this.m_vCache = new VertexCache();
      if (this.m_tPool != t)
      {
        this.m_tPool = t;
        this.m_vCache.Invalidate();
      }
      this.m_vCache.Draw(t, x, y);
    }

    public void DrawGame(Texture t, int x, int y, int color)
    {
      if (this.m_vCache == null)
        this.m_vCache = new VertexCache();
      if (this.m_tPool != t)
      {
        this.m_tPool = t;
        this.m_vCache.Invalidate();
      }
      this.m_vCache.DrawGame(t, x, y, color);
    }

    public void OnSingleClick()
    {
      this.Look();
    }

    public void OnDoubleClick()
    {
      this.Use(true);
      PUseRequest.Last = (IEntity) this;
    }

    public void OnTarget()
    {
      TargetManager.Target((object) this);
    }

    public void OverrideHue(int hue)
    {
      bool flag = this.m_ShouldOverrideHue;
      this.m_ShouldOverrideHue = hue >= 0;
      if (this.m_ShouldOverrideHue)
      {
        if (!flag)
        {
          this.m_RealHue = (int) this.m_Hue;
          this.m_Hue = (ushort) hue;
        }
      }
      else if (flag)
        this.m_Hue = (ushort) this.m_RealHue;
      this.RaiseUpdateEvents();
    }

    public void Update()
    {
    }

    public bool Use()
    {
      return this.Use(false);
    }

    public bool Use(bool isManual)
    {
      new UseContext((IEntity) this, isManual).Enqueue();
      return true;
    }

    public bool SendUse()
    {
      return Network.Send((Packet) new PUseRequest((IEntity) this));
    }

    public bool Look()
    {
      return Network.Send((Packet) new PLookRequest((IEntity) this));
    }

    public int CompareTo(object x)
    {
      if (x == null)
        return 1;
      Item obj = (Item) x;
      int num = this.Y - obj.Y;
      if (num == 0)
      {
        num = this.X - obj.X;
        if (num == 0)
          num = this.Z - obj.Z;
      }
      return num;
    }

    public GContainer OpenContainer(int gumpId, IHue hue)
    {
      if (this.Container == null)
      {
        this.SetContainerView((IContainerView) new GContainer(this, gumpId, hue));
      }
      else
      {
        this.Container.GumpID = gumpId;
        this.Container.Hue = hue;
      }
      return this.Container;
    }

    protected override void OnChildAdded(Agent child)
    {
      base.OnChildAdded(child);
      if (!(child is Item))
        return;
      Item obj = (Item) child;
      if (obj.InTradeWindow)
        obj.QueryProperties();
      int num = obj.ID & 16383;
      if (num < 3570 || num >= 3574 || !obj.IsChildOf((Agent) World.Player))
        return;
      new LookContext((IEntity) obj).Enqueue();
    }

    protected override void OnChildRemoved(Agent child)
    {
      base.OnChildRemoved(child);
    }

    public void ClearCorpseItems()
    {
      if (this.m_CorpseItems != null)
        this.m_CorpseItems.Clear();
      if (this.corpseSortedItems == null)
        return;
      this.corpseSortedItems.Clear();
      this.corpseSortedItems = (List<Item>) null;
      this.corpseSortComparer = (LayerComparer) null;
    }

    public void AddCorpseItem(Item item)
    {
      if (this.m_CorpseItems == null)
        this.m_CorpseItems = new List<Item>();
      this.m_CorpseItems.Add(item);
      if (this.corpseSortedItems == null)
        return;
      int index = this.corpseSortedItems.BinarySearch(item, (IComparer<Item>) this.corpseSortComparer);
      if (index < 0)
        index = ~index;
      this.corpseSortedItems.Insert(index, item);
    }

    public List<Item> GetSortedCorpseItems()
    {
      return this.GetSortedCorpseItems((int) this.m_Direction);
    }

    public List<Item> GetSortedCorpseItems(int direction)
    {
      LayerComparer layerComparer = LayerComparer.FromDirection(direction);
      if (this.corpseSortedItems == null)
        this.corpseSortedItems = new List<Item>((IEnumerable<Item>) this.CorpseItems);
      if (layerComparer != this.corpseSortComparer)
      {
        this.corpseSortComparer = layerComparer;
        this.corpseSortedItems.Sort((IComparer<Item>) this.corpseSortComparer);
      }
      return this.corpseSortedItems;
    }

    protected override void OnLocationChanged()
    {
      base.OnLocationChanged();
      if (this.Multi == null)
        return;
      Map.Invalidate();
      GRadar.Invalidate();
    }

    public Gump OnBeginDrag()
    {
      if (this.IsStackable && (int) this.amount > 1 && (Control.ModifierKeys & Keys.Shift) == Keys.None)
      {
        GDragAmount gdragAmount = new GDragAmount(this);
        Gumps.Desktop.Children.Add((Gump) gdragAmount);
        return (Gump) gdragAmount;
      }
      Player current = Player.Current;
      if (current != null)
        current.EquipAgent.Dress.Remove(this);
      Network.Send((Packet) new PPickupItem(this, (int) this.amount));
      GDraggedItem gdraggedItem = new GDraggedItem(this);
      Gumps.Desktop.Children.Add((Gump) gdraggedItem);
      return (Gump) gdraggedItem;
    }

    public Item FindItem(IItemValidator iv)
    {
      return this.FindItem(new Predicate<Item>(iv.IsValid));
    }

    public Item FindItem(Predicate<Item> filter)
    {
      if (filter == null)
        throw new ArgumentNullException("filter");
      Queue<Item> objQueue = Item.m_FindItem_Queue;
      if (objQueue == null)
        objQueue = Item.m_FindItem_Queue = new Queue<Item>();
      else if (objQueue.Count > 0)
        objQueue.Clear();
      if (filter(this))
        return this;
      if (this.HasItems)
      {
        objQueue.Enqueue(this);
        while (objQueue.Count > 0)
        {
          List<Item> items = objQueue.Dequeue().Items;
          for (int index = 0; index < items.Count; ++index)
          {
            Item obj = items[index];
            if (filter(obj))
              return obj;
            if (obj.HasItems)
              objQueue.Enqueue(obj);
          }
        }
      }
      return (Item) null;
    }

    public IEnumerable<Item> GetItems(IItemValidator validator)
    {
      if (validator == null)
        throw new ArgumentNullException("validator");
      return this.GetItems(new Predicate<Item>(validator.IsValid));
    }

    public IEnumerable<Item> GetItems(Predicate<Item> filter)
    {
      if (filter == null)
        throw new ArgumentNullException("filter");
      if (this.HasItems)
      {
        using (ScratchQueue<Item> scratchQueue = new ScratchQueue<Item>())
        {
          Queue<Item> queue = scratchQueue.Value;
          queue.Enqueue(this);
          while (queue.Count > 0)
          {
            Item item = queue.Dequeue();
            List<Item> items = item.Items;
            for (int i = 0; i < items.Count; ++i)
            {
              Item child = items[i];
              if (filter(child))
                yield return child;
              if (child.HasItems)
                queue.Enqueue(child);
            }
          }
        }
      }
    }

    public Item[] FindItems(IItemValidator validator)
    {
      return ScratchList<Item>.ToArray(this.GetItems(validator));
    }

    public void AddTextMessage(string Name, string Message, IFont Font, IHue Hue, bool unremovable)
    {
      this.m_LastTextHue = Hue;
      this.m_LastText = Message;
      string text = Name.Length <= 0 ? Message : Name + ": " + Message;
      if (Message.Length <= 0)
        return;
      Engine.AddToJournal(new JournalEntry(text, Hue, this.Serial));
      Message = Engine.WrapText(Message, 200, Font).TrimEnd();
      if (Message.Length <= 0)
        return;
      MessageManager.AddMessage((IMessage) new GDynamicMessage(unremovable, this, Message, Font, Hue));
    }

    public GPaperdollItem GetPaperdollItem(Mobile mob, bool canLift)
    {
      if (this._paperdollItem == null)
      {
        this._paperdollItem = new GPaperdollItem(mob, this, canLift);
      }
      else
      {
        this._paperdollItem.Mobile = mob;
        this._paperdollItem.CanLift = canLift;
      }
      return this._paperdollItem;
    }

    protected override AgentCell CreateViewportCell()
    {
      return (AgentCell) new DynamicItem(this);
    }

    protected override IEnumerable<IAgentView> GetAgentViews()
    {
      List<IAgentView> agentViewList = new List<IAgentView>(base.GetAgentViews());
      if (this._paperdollItem != null)
        agentViewList.Add((IAgentView) this._paperdollItem);
      return (IEnumerable<IAgentView>) agentViewList;
    }
  }
}
