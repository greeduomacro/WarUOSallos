// Decompiled with JetBrains decompiler
// Type: PlayUO.GDraggedItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public class GDraggedItem : Gump, IItemGump
  {
    private bool m_Draw;
    private Texture m_Image;
    private int m_Width;
    private int m_Height;
    private Item m_Item;
    private IHue m_Hue;
    private bool m_Double;
    private int m_xOffset;
    private int m_yOffset;
    private VertexCache m_vCache;
    private VertexCache m_vCacheDouble;

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

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public Item Item
    {
      get
      {
        return this.m_Item;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
    }

    public GDraggedItem(Item item)
      : base(0, 0)
    {
      this.m_vCache = new VertexCache();
      this.m_Item = item;
      int index = this.m_Item.ID & 16383;
      int num1 = (int) (ushort) this.m_Item.Amount;
      this.m_Double = Map.m_ItemFlags[index][(TileFlag) 2048L] && num1 > 1;
      if (index >= 3818 && index <= 3826)
      {
        int num2 = (index - 3818) / 3 * 3 + 3818;
        this.m_Double = false;
        index = num1 > 1 ? (num1 < 2 || num1 > 5 ? num2 + 2 : num2 + 1) : num2;
      }
      this.m_Hue = Hues.GetItemHue(index, (int) this.m_Item.Hue);
      this.m_Image = this.m_Hue.GetItem(index);
      if (this.m_Image != null && !this.m_Image.IsEmpty())
      {
        this.m_Draw = true;
        this.m_Width = this.m_Image.Width;
        this.m_Height = this.m_Image.Height;
        int num2 = this.m_Double ? 6 : 1;
        this.m_xOffset = this.m_OffsetX = this.m_Image.xMin + (this.m_Image.xMax - this.m_Image.xMin + num2) / 2;
        this.m_yOffset = this.m_Image.yMin;
        this.m_OffsetY = this.m_yOffset + (this.m_Image.yMax - this.m_Image.yMin + num2) / 2;
        if (this.m_Double)
        {
          this.m_Width += 5;
          this.m_Height += 5;
        }
      }
      this.m_DragCursor = false;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_IsDragging = true;
      Gumps.Drag = (Gump) this;
      Gumps.LastOver = (Gump) this;
      this.m_X = Engine.m_xMouse - this.m_OffsetX;
      this.m_Y = Engine.m_yMouse - this.m_OffsetY;
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
      if (!TargetManager.IsActive || (mb & MouseButtons.Left) == MouseButtons.None)
        return;
      this.m_Item.OnTarget();
      Engine.CancelClick();
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      this.m_Item.OnDoubleClick();
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Double)
      {
        if (!this.m_Draw)
          return false;
        if (this.X < this.m_Image.Width && this.Y < this.m_Image.Height && this.m_Image.HitTest(x, y))
          return true;
        if (this.X >= 5 && this.Y >= 5)
          return this.m_Image.HitTest(x - 5, y - 5);
        return false;
      }
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_Draw)
        return;
      this.m_vCache.Draw(this.m_Image, x, y);
      if (this.m_Double)
      {
        if (this.m_vCacheDouble == null)
          this.m_vCacheDouble = new VertexCache();
        this.m_vCacheDouble.Draw(this.m_Image, x + 5, y + 5);
      }
      this.m_Item.MessageX = this.X + this.m_xOffset;
      this.m_Item.MessageY = this.Y + this.m_yOffset;
      this.m_Item.BottomY = this.Y + this.m_Image.yMax;
      this.m_Item.MessageFrame = Renderer.m_ActFrames;
    }
  }
}
