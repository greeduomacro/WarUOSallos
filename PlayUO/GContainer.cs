// Decompiled with JetBrains decompiler
// Type: PlayUO.GContainer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GContainer : GFader, IContainerView, IAgentView
  {
    public bool m_HitTest = true;
    protected internal Hashtable m_Hash = new Hashtable();
    private Item m_Item;
    private int m_xBoundLeft;
    private int m_yBoundTop;
    private int m_xBoundRight;
    private int m_yBoundBottom;
    public bool m_TradeContainer;
    public bool m_NoDrop;

    public Item Item
    {
      get
      {
        return this.m_Item;
      }
    }

    public Gump Gump
    {
      get
      {
        return (Gump) this;
      }
    }

    public GContainer(Item container, int gumpID)
      : this(container, gumpID, Hues.Default)
    {
    }

    public GContainer(Item container, int gumpID, IHue hue)
      : base(0.25f, 0.25f, 0.6f, gumpID, 50, 50, hue)
    {
      this.m_Item = container;
      this.m_CanDrop = true;
      this.GetBounds(gumpID);
      this.m_NonRestrictivePicking = true;
      foreach (Item obj in container.Items)
      {
        if (this.m_GumpID != 9 || (obj.ID & 16383) < 8198 || (obj.ID & 16383) > 8272)
        {
          Gump ToAdd = (Gump) new GContainerItem(obj, this.m_Item);
          this.m_Hash[(object) obj] = (object) ToAdd;
          ToAdd.m_CanDrag = !this.m_TradeContainer;
          this.m_Children.Add(ToAdd);
        }
      }
    }

    public void Close()
    {
      Engine.Sounds.PlayContainerClose(this.m_GumpID);
      Gumps.Destroy((Gump) this);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (!this.m_CanClose || mb != MouseButtons.Right)
        return;
      if (this.m_TradeContainer)
        ((GSecureTrade) this.m_Parent).Close();
      else
        this.Close();
    }

    public void OnChildUpdated(Item item)
    {
      if (this.m_GumpID == 9 && (item.ID & 16383) >= 8198 && (item.ID & 16383) <= 8272)
      {
        this.OnChildRemoved(item);
      }
      else
      {
        GContainerItem gcontainerItem = (GContainerItem) this.m_Hash[(object) item];
        if (gcontainerItem == null)
        {
          gcontainerItem = new GContainerItem(item, this.m_Item);
          this.m_Hash[(object) item] = (object) gcontainerItem;
          this.m_Children.Add((Gump) gcontainerItem);
        }
        else
          gcontainerItem.Refresh();
        gcontainerItem.m_CanDrag = !this.m_TradeContainer;
      }
    }

    public void OnChildAdded(Item item)
    {
      if (this.m_GumpID == 9 && (item.ID & 16383) >= 8198 && (item.ID & 16383) <= 8272)
        return;
      GContainerItem gcontainerItem1 = (GContainerItem) this.m_Hash[(object) item];
      if (gcontainerItem1 != null)
        Gumps.Destroy((Gump) gcontainerItem1);
      GContainerItem gcontainerItem2;
      this.m_Hash[(object) item] = (object) (gcontainerItem2 = new GContainerItem(item, this.m_Item));
      gcontainerItem2.m_CanDrag = !this.m_TradeContainer;
      this.m_Children.Add((Gump) gcontainerItem2);
    }

    public void OnChildRemoved(Item item)
    {
      GContainerItem gcontainerItem = (GContainerItem) this.m_Hash[(object) item];
      if (gcontainerItem == null)
        return;
      Gumps.Destroy((Gump) gcontainerItem);
      this.m_Hash[(object) item] = (object) null;
    }

    private void GetBounds(int gumpID)
    {
      Rectangle rectangle = Engine.ContainerBoundsTable.Translate(gumpID);
      this.m_xBoundLeft = rectangle.X;
      this.m_yBoundTop = rectangle.Y;
      this.m_xBoundRight = rectangle.Right - 1;
      this.m_yBoundBottom = rectangle.Bottom - 1;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_HitTest)
        return base.HitTest(x, y);
      return false;
    }

    protected internal override void OnDispose()
    {
      if (this.m_Item.ContainerView != this)
        return;
      this.m_Item.SetContainerView((IContainerView) null);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_TradeContainer)
        base.Draw(x, y);
      if (this.m_GumpID == 2330 || this.m_GumpID == 2350 || (this.m_GumpID == 82 || !Options.Current.ContainerGrid))
        return;
      Rectangle grid = this.GetGrid();
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(this.m_fAlpha * (float) Math.Sqrt((double) this.m_fAlpha));
      int num1 = 0;
      int num2 = x + grid.Left;
      while (num1 <= grid.Width)
      {
        Renderer.DrawLine(num2 - 1, y + grid.Top - 1, num2 - 1, y + grid.Top + grid.Height * 21);
        ++num1;
        num2 += 21;
      }
      int num3 = 0;
      int num4 = y + grid.Top;
      while (num3 <= grid.Height)
      {
        Renderer.DrawLine(x + grid.Left - 1, num4 - 1, x + grid.Left + grid.Width * 21, num4 - 1);
        ++num3;
        num4 += 21;
      }
      Renderer.PopAlpha();
    }

    protected internal override void OnDragStart()
    {
      if (!this.m_TradeContainer)
        return;
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point client = this.m_Parent.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse));
      this.m_Parent.m_OffsetX = client.X;
      this.m_Parent.m_OffsetY = client.Y;
      this.m_Parent.m_IsDragging = true;
      Gumps.Drag = this.m_Parent;
    }

    public Rectangle GetGrid()
    {
      int num1 = this.m_xBoundRight - this.m_xBoundLeft + 1;
      int num2 = this.m_yBoundBottom - this.m_yBoundTop + 1;
      int width = 0;
      int num3 = 0;
      while (num3 + 20 < num1)
      {
        num3 += 21;
        ++width;
      }
      int height = 0;
      int num4 = 0;
      while (num4 + 20 < num2)
      {
        num4 += 21;
        ++height;
      }
      return new Rectangle(this.m_xBoundLeft + (num1 - width * 21) / 2, this.m_yBoundTop + (num2 - height * 21) / 2, width, height);
    }

    public Point Clip(Texture img, bool xDouble, Point p, int xOffset, int yOffset)
    {
      if (this.m_GumpID == 2330 || this.m_GumpID == 2350)
        return p;
      if (Options.Current.ContainerGrid)
      {
        Rectangle grid = this.GetGrid();
        Point point1 = (new Point(p, xOffset, yOffset) - new Point(grid.Left, grid.Top)) / 21;
        if (point1.X < 0)
          point1.X = 0;
        if (point1.Y < 0)
          point1.Y = 0;
        if (point1.X >= grid.Width)
          point1.X = grid.Width - 1;
        if (point1.Y >= grid.Height)
          point1.Y = grid.Height - 1;
        Point point2 = new Point(grid.X + point1.X * 21 + (20 - ((xDouble ? 6 : 1) + (img.xMax - img.xMin))) / 2, grid.Y + point1.Y * 21 + (20 - ((xDouble ? 6 : 1) + (img.yMax - img.yMin))) / 2);
        point2.X -= img.xMin;
        point2.Y -= img.yMin;
        return point2;
      }
      Point point = new Point(p.X, p.Y);
      int num1 = p.X + img.xMin;
      int num2 = p.Y + img.yMin;
      int num3 = p.X + img.xMax;
      int num4 = p.Y + img.yMax;
      if (num1 < this.m_xBoundLeft)
        point.X = this.m_xBoundLeft - img.xMin;
      if (num2 < this.m_yBoundTop)
        point.Y = this.m_yBoundTop - img.yMin;
      if (xDouble)
      {
        int num5 = num3 + 5;
        int num6 = num4 + 5;
        if (num5 > this.m_xBoundRight)
          point.X = this.m_xBoundRight - img.xMax - 5;
        if (num6 > this.m_yBoundBottom)
          point.Y = this.m_yBoundBottom - img.yMax - 5;
      }
      else
      {
        if (num3 > this.m_xBoundRight)
          point.X = this.m_xBoundRight - img.xMax;
        if (num4 > this.m_yBoundBottom)
          point.Y = this.m_yBoundBottom - img.yMax;
      }
      return point;
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (!this.m_HitTest)
      {
        this.m_Parent.OnDragDrop(g);
      }
      else
      {
        if (g == null || !(g.GetType() == typeof (GDraggedItem)))
          return;
        GDraggedItem gdraggedItem = (GDraggedItem) g;
        Point point = this.Clip(gdraggedItem.Image, gdraggedItem.Double, this.PointToClient(new Point(Engine.m_xMouse - g.m_OffsetX, Engine.m_yMouse - g.m_OffsetY)), g.m_OffsetX, g.m_OffsetY);
        int num = gdraggedItem.Item.ID & 16383;
        if (num >= 13701 && num <= 13706)
          point.Y += 20;
        else if (num >= 13708 && num <= 13713)
          point.Y += 20;
        Gumps.Destroy((Gump) gdraggedItem);
        Network.Send((Packet) new PDropItem(gdraggedItem.Item.Serial, (int) (short) point.X, (int) (short) point.Y, 0, this.m_Item.Serial));
      }
    }

    public void OnAgentUpdated()
    {
    }

    public void OnAgentDeleted()
    {
      this.Close();
    }
  }
}
