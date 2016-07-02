// Decompiled with JetBrains decompiler
// Type: PlayUO.GContainerItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Assets;
using PlayUO.Targeting;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public class GContainerItem : Gump, IItemGump
  {
    private static VertexCachePool m_vPool = new VertexCachePool();
    private bool m_Draw;
    private Texture m_Image;
    private int m_Width;
    private int m_Height;
    private int m_State;
    private int m_TileID;
    private IHue m_Hue;
    private Item m_Item;
    private Item m_Container;
    private bool m_Double;
    private VertexCache m_vCache;
    private VertexCache m_vCacheDouble;
    private int m_xOffset;
    private int m_yOffset;

    protected VertexCachePool VCPool
    {
      get
      {
        return GContainerItem.m_vPool;
      }
    }

    public int xOffset
    {
      get
      {
        return this.m_xOffset;
      }
    }

    public int yOffset
    {
      get
      {
        return this.m_yOffset;
      }
    }

    public int yBottom
    {
      get
      {
        return this.m_Image.yMax;
      }
    }

    public bool Double
    {
      get
      {
        return this.m_Double;
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
    }

    public Item Item
    {
      get
      {
        return this.m_Item;
      }
    }

    public Item Container
    {
      get
      {
        return this.m_Container;
      }
    }

    public int TileID
    {
      get
      {
        return this.m_TileID;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width + (this.m_Double ? 5 : 0);
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height + (this.m_Double ? 5 : 0);
      }
    }

    public int State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        this.m_State = value;
        int itemId = this.m_TileID;
        int num1 = (int) (ushort) this.m_Item.Amount;
        if (this.m_Item != null)
        {
          this.m_Double = Map.m_ItemFlags[itemId & 16383][(TileFlag) 2048L] && num1 > 1;
          if (this.m_TileID >= 3818 && this.m_TileID <= 3826)
          {
            int num2 = (this.m_TileID - 3818) / 3 * 3 + 3818;
            this.m_Double = false;
            itemId = num1 > 1 ? (num1 < 2 || num1 > 5 ? num2 + 2 : num2 + 1) : num2;
          }
        }
        this.m_Image = (this.m_State == 0 ? (IGraphicProvider) this.m_Hue : (IGraphicProvider) Hues.Load(32821)).GetItem(itemId);
        if (this.m_Image != null && !this.m_Image.IsEmpty())
        {
          this.m_xOffset = this.m_Image.xMin + (this.m_Image.xMax - this.m_Image.xMin + (this.m_Double ? 6 : 1)) / 2;
          this.m_yOffset = this.m_Image.yMin;
          this.m_Width = this.m_Image.Width;
          this.m_Height = this.m_Image.Height;
          this.m_Draw = this.m_Item != null;
        }
        else
        {
          this.m_xOffset = this.m_yOffset = this.m_Width = this.m_Height = 0;
          this.m_Draw = false;
        }
        if (this.m_vCache != null)
          this.m_vCache.Invalidate();
        if (this.m_vCacheDouble == null)
          return;
        this.m_vCacheDouble.Invalidate();
      }
    }

    public override int Y
    {
      get
      {
        int num = this.m_Item.ID & 16383;
        int y = base.Y;
        if (num >= 13701 && num <= 13706)
          y -= 20;
        else if (num >= 13708 && num <= 13713)
          y -= 20;
        return y;
      }
      set
      {
        base.Y = value;
      }
    }

    public GContainerItem(Item Item, Item Container)
      : base(Item.X, Item.Y)
    {
      this.m_Item = Item;
      this.m_Container = Container;
      this.m_TileID = this.m_Item.ID;
      this.m_Hue = Hues.GetItemHue(this.m_TileID, (int) this.m_Item.Hue);
      this.State = 0;
      this.m_CanDrag = true;
      this.m_CanDrop = true;
      this.m_QuickDrag = false;
      this.m_DragCursor = false;
      if (!Engine.ServerFeatures.AOS)
        return;
      this.Tooltip = (ITooltip) new ItemTooltip(this.m_Item);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      this.State = 1;
    }

    protected internal override void OnMouseLeave()
    {
      this.State = 0;
      if (this.Tooltip == null)
        return;
      ((ItemTooltip) this.Tooltip).Gump = (Gump) null;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      if (TargetManager.IsActive)
        return;
      this.m_Item.OnSingleClick();
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) != MouseButtons.None)
      {
        Point client = this.m_Parent.PointToClient(this.PointToScreen(new Point(x, y)));
        this.m_Parent.OnMouseUp(client.X, client.Y, mb);
      }
      else if (TargetManager.IsActive && (mb & MouseButtons.Left) != MouseButtons.None)
      {
        this.m_Item.OnTarget();
        Engine.CancelClick();
      }
      else
      {
        if ((mb & MouseButtons.Left) == MouseButtons.None || (Control.ModifierKeys & Keys.Shift) == Keys.None)
          return;
        Network.Send((Packet) new PPopupRequest(this.m_Item));
      }
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      this.m_Item.OnDoubleClick();
    }

    protected internal override void OnDispose()
    {
      this.VCPool.ReleaseInstance(this.m_vCache);
      this.m_vCache = (VertexCache) null;
      this.VCPool.ReleaseInstance(this.m_vCacheDouble);
      this.m_vCacheDouble = (VertexCache) null;
    }

    public void Refresh()
    {
      this.m_TileID = this.m_Item.ID;
      this.m_Hue = Hues.GetItemHue(this.m_TileID, (int) this.m_Item.Hue);
      this.State = 0;
      this.m_CanDrag = true;
      this.m_CanDrop = true;
      this.m_QuickDrag = false;
      this.m_DragCursor = false;
    }

    protected internal override void OnDragStart()
    {
      if (this.m_Item == null)
        return;
      this.m_IsDragging = false;
      Gumps.LastOver = (Gump) null;
      Gumps.Drag = (Gump) null;
      this.State = 0;
      Gump gump = this.m_Item.OnBeginDrag();
      if (gump.GetType() == typeof (GDragAmount))
      {
        ((GDragAmount) gump).ToDestroy = (object) this;
      }
      else
      {
        this.m_Item.RestoreInfo = new RestoreInfo(this.m_Item);
        World.Remove(this.m_Item);
        gump.m_OffsetX = this.m_OffsetX;
        gump.m_OffsetY = this.m_OffsetY;
        gump.X = Engine.m_xMouse - this.m_OffsetX;
        gump.Y = Engine.m_yMouse - this.m_OffsetY;
        if (this.m_Parent is GContainer)
          ((GContainer) this.m_Parent).m_Hash[(object) this.m_Item] = (object) null;
        Gumps.Destroy((Gump) this);
      }
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (g == null || !(g.GetType() == typeof (GDraggedItem)))
        return;
      GDraggedItem gdraggedItem = (GDraggedItem) g;
      Item obj = gdraggedItem.Item;
      if (((GContainer) this.m_Parent).m_HitTest)
      {
        TileFlags tileFlags = Map.m_ItemFlags[this.m_Item.ID & 16383];
        if (tileFlags[(TileFlag) 2097152L])
        {
          Network.Send((Packet) new PDropItem(obj.Serial, -1, -1, 0, this.m_Item.Serial));
          Gumps.Destroy((Gump) gdraggedItem);
        }
        else if (tileFlags[(TileFlag) 2048L] && obj.ID == this.m_Item.ID && (int) obj.Hue == (int) this.m_Item.Hue)
        {
          Point point = ((GContainer) this.m_Parent).Clip(gdraggedItem.Image, gdraggedItem.Double, this.m_Parent.PointToClient(new Point(Engine.m_xMouse - gdraggedItem.m_OffsetX, Engine.m_yMouse - gdraggedItem.m_OffsetY)), gdraggedItem.m_OffsetX, gdraggedItem.m_OffsetY);
          Network.Send((Packet) new PDropItem(obj.Serial, (int) (short) point.X, (int) (short) point.Y, 0, this.m_Item.Serial));
          Gumps.Destroy((Gump) gdraggedItem);
        }
        else
          this.m_Parent.OnDragDrop((Gump) gdraggedItem);
      }
      else
        this.m_Parent.OnDragDrop((Gump) gdraggedItem);
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      if (this.m_vCache == null)
        this.m_vCache = this.VCPool.GetInstance();
      this.m_vCache.Draw(this.m_Image, X, Y);
      if (this.m_Double)
      {
        if (this.m_vCacheDouble == null)
          this.m_vCacheDouble = this.VCPool.GetInstance();
        this.m_vCacheDouble.Draw(this.m_Image, X + 5, Y + 5);
      }
      this.m_Item.MessageX = X + this.m_xOffset;
      this.m_Item.MessageY = Y + this.m_yOffset;
      this.m_Item.BottomY = Y + this.m_Image.yMax;
      this.m_Item.MessageFrame = Renderer.m_ActFrames;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Double)
      {
        if (!this.m_Draw)
          return false;
        if (X < this.m_Image.Width && Y < this.m_Image.Height && this.m_Image.HitTest(X, Y))
          return true;
        if (X >= 5 && Y >= 5)
          return this.m_Image.HitTest(X - 5, Y - 5);
        return false;
      }
      if (this.m_Draw)
        return this.m_Image.HitTest(X, Y);
      return false;
    }
  }
}
