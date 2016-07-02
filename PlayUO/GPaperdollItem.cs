// Decompiled with JetBrains decompiler
// Type: PlayUO.GPaperdollItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GPaperdollItem : GImage, IItemGump, IAgentView
  {
    private Mobile _mobile;
    private Item _item;
    private bool _canLift;
    private int m_xOffset;
    private int m_yOffset;

    public Mobile Mobile
    {
      get
      {
        return this._mobile;
      }
      set
      {
        this._mobile = value;
      }
    }

    public bool CanLift
    {
      get
      {
        return this._canLift;
      }
      set
      {
        this._canLift = value;
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
        return this.m_yOffset;
      }
    }

    public Item Item
    {
      get
      {
        return this._item;
      }
    }

    public GPaperdollItem(Mobile mob, Item item, bool canLift)
      : base(8, 19)
    {
      this._mobile = mob;
      this._item = item;
      this._canLift = canLift;
      this.Alpha = 1f;
      if (Engine.ServerFeatures.AOS)
        this.Tooltip = (ITooltip) new ItemTooltip(item);
      this.m_CanDrag = true;
      this.OnAgentUpdated();
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      if (TargetManager.IsActive)
        return;
      this._item.OnSingleClick();
      this.m_xOffset = x;
      this.m_yOffset = y;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) != MouseButtons.None)
      {
        Point client = this.m_Parent.PointToClient(this.PointToScreen(new Point(X, Y)));
        this.m_Parent.OnMouseUp(client.X, client.Y, mb);
      }
      else if (TargetManager.IsActive && (mb & MouseButtons.Left) != MouseButtons.None)
      {
        this._item.OnTarget();
        Engine.CancelClick();
      }
      else
      {
        if ((mb & MouseButtons.Left) == MouseButtons.None || (Control.ModifierKeys & Keys.Shift) == Keys.None)
          return;
        Network.Send((Packet) new PPopupRequest(this._item));
      }
    }

    protected internal override void OnDispose()
    {
      if (this._item.PaperdollItem != this)
        return;
      this._item.PaperdollItem = (GPaperdollItem) null;
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      this._item.OnDoubleClick();
      this.m_xOffset = X;
      this.m_yOffset = Y;
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
    }

    protected internal override void OnMouseLeave()
    {
      if (this.Tooltip == null)
        return;
      ((ItemTooltip) this.Tooltip).Gump = (Gump) null;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Parent.BringToTop();
    }

    protected internal override void OnDragStart()
    {
      int num = (int) this._item.Layer;
      if (this._canLift && num >= 1 && (num <= 24 && num != 11) && num != 16 && num != 21)
      {
        this.m_IsDragging = false;
        Gumps.LastOver = (Gump) null;
        Gumps.Drag = (Gump) null;
        Gump gump = this._item.OnBeginDrag();
        if (gump.GetType() == typeof (GDragAmount))
        {
          ((GDragAmount) gump).ToDestroy = (object) this;
        }
        else
        {
          this._item.RestoreInfo = new RestoreInfo(this._item);
          World.Remove(this._item);
          Gumps.Destroy((Gump) this);
        }
      }
      else
      {
        this.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        Point point = this.PointToScreen(new Point(0, 0)) - this.m_Parent.PointToScreen(new Point(0, 0));
        this.m_Parent.m_OffsetX = point.X + this.m_OffsetX;
        this.m_Parent.m_OffsetY = point.Y + this.m_OffsetY;
        this.m_Parent.m_IsDragging = true;
        Gumps.Drag = this.m_Parent;
      }
    }

    protected internal override void Draw(int x, int y)
    {
      base.Draw(x, y);
      if (!this.m_Draw)
        return;
      this._item.MessageX = x + this.m_xOffset;
      this._item.MessageY = y + this.m_yOffset;
      this._item.BottomY = y + this.m_yOffset;
      this._item.MessageFrame = Renderer.m_ActFrames;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Image == null || !this.m_Draw)
        return false;
      switch (this._item.Layer)
      {
        case Layer.Ring:
        case Layer.Neck:
        case Layer.Bracelet:
        case Layer.Earrings:
          int num1 = -3;
          int num2 = 0;
          for (; num1 <= 3; ++num1)
          {
            int num3 = -3;
            while (num3 <= 3)
            {
              if ((int) (Math.Sqrt((double) (num1 * num1 + num3 * num3)) + 0.5) <= 3 && X + num1 >= 0 && (X + num1 < this.m_Width && Y + num3 >= 0) && (Y + num3 < this.m_Height && this.m_Image.HitTest(X + num1, Y + num3)))
                return true;
              ++num3;
              ++num2;
            }
          }
          return false;
        default:
          return this.m_Image.HitTest(X, Y);
      }
    }

    public void OnAgentUpdated()
    {
      Item obj = this._item;
      int id = obj.ID;
      int hue = (int) obj.Hue;
      this.GumpID = Gumps.GetEquipGumpID(id, this._mobile.BodyGender, ref hue);
      this.Hue = Hues.GetItemHue(id, hue);
      int num = (int) obj.Layer;
      this.m_QuickDrag = num < 1 || num > 24 || num == 11 || num == 16;
    }

    public void OnAgentDeleted()
    {
      Gumps.Destroy((Gump) this);
    }
  }
}
