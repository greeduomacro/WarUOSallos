// Decompiled with JetBrains decompiler
// Type: PlayUO.Gump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections;
using System.Windows.Forms;

namespace PlayUO
{
  public class Gump
  {
    protected internal int m_DragClipX = 50;
    protected internal int m_DragClipY = 50;
    protected string m_GUID = "";
    protected internal bool m_DragCursor = true;
    protected internal bool m_OverridesCursor = true;
    protected internal bool m_Visible = true;
    protected internal int m_OverCursor = 9;
    protected bool m_Modal;
    protected int m_Handle;
    protected int m_X;
    protected int m_Y;
    protected GumpList m_Children;
    protected Gump m_Parent;
    private Hashtable m_Tags;
    protected internal int m_OffsetX;
    protected internal int m_OffsetY;
    protected internal bool m_CanDrag;
    protected internal bool m_IsDragging;
    protected internal bool m_CanDrop;
    protected internal bool m_QuickDrag;
    protected internal bool m_Restore;
    protected internal bool m_ITranslucent;
    protected internal ITooltip m_Tooltip;
    protected internal bool m_NonRestrictivePicking;
    protected internal bool m_Disposed;
    protected GumpLayout m_Layout;

    public bool Disposed
    {
      get
      {
        return this.m_Disposed;
      }
    }

    public bool Visible
    {
      get
      {
        return this.m_Visible;
      }
      set
      {
        this.m_Visible = value;
        Gumps.Invalidate();
      }
    }

    public ITooltip Tooltip
    {
      get
      {
        return this.m_Tooltip;
      }
      set
      {
        this.m_Tooltip = value;
      }
    }

    public string GUID
    {
      get
      {
        return this.m_GUID;
      }
      set
      {
        this.m_GUID = value;
        Gumps.Restore(this);
      }
    }

    public bool Modal
    {
      get
      {
        return this.m_Modal;
      }
      set
      {
        if (value)
        {
          if (!this.HasTag("Dispose"))
            this.SetTag("Dispose", (object) "Modal");
          Gumps.Modal = this;
        }
        else
        {
          if (this.HasTag("Dispose"))
            this.RemoveTag("Dispose");
          if (Gumps.Modal != this)
            return;
          Gumps.Modal = (Gump) null;
        }
      }
    }

    public Gump Parent
    {
      get
      {
        return this.m_Parent;
      }
      set
      {
        this.m_Parent = value;
        if (this.m_Parent == null)
          return;
        this.DefineLayout();
      }
    }

    public virtual int X
    {
      get
      {
        return this.m_X;
      }
      set
      {
        this.m_X = value;
        if (this.m_Layout == null)
          return;
        this.m_Layout.Update(this);
      }
    }

    public virtual int Y
    {
      get
      {
        return this.m_Y;
      }
      set
      {
        this.m_Y = value;
        if (this.m_Layout == null)
          return;
        this.m_Layout.Update(this);
      }
    }

    public GumpList Children
    {
      get
      {
        return this.m_Children;
      }
    }

    public virtual int Width
    {
      get
      {
        return Engine.ScreenWidth;
      }
      set
      {
      }
    }

    public virtual int Height
    {
      get
      {
        return Engine.ScreenHeight;
      }
      set
      {
      }
    }

    public Gump(int X, int Y)
    {
      this.m_Children = new GumpList(this);
      this.m_X = X;
      this.m_Y = Y;
    }

    public virtual Gump FindChild(Predicate<Gump> predicate)
    {
      if (predicate(this))
        return this;
      if (this.m_Children != null)
      {
        foreach (Gump mChild in this.m_Children)
        {
          Gump child = mChild.FindChild(predicate);
          if (child != null)
            return child;
        }
      }
      return (Gump) null;
    }

    public virtual void DefineLayout()
    {
      if (this.m_Layout != null)
        return;
      this.SetLayout(this.CreateLayout());
      if (this.m_Layout == null)
        return;
      this.m_Layout.Update(this);
      Preferences.Current.Layout.Gumps.Add(this.m_Layout);
    }

    public virtual GumpLayout CreateLayout()
    {
      return (GumpLayout) null;
    }

    public void SetLayout(GumpLayout layout)
    {
      this.m_Layout = layout;
    }

    public void ManualClose()
    {
      Gumps.Destroy(this);
      if (this.m_Layout == null)
        return;
      this.m_Layout.Remove();
    }

    public void BringToTop()
    {
      if (this.m_Parent == null)
        return;
      this.m_Parent.Children.RemoveAt(this.m_Parent.Children.IndexOf(this));
      this.m_Parent.Children.Add(this);
      this.m_Parent.BringToTop();
      if (this.m_Layout == null)
        return;
      this.m_Layout.BringToTop();
    }

    public void OffsetChildren(int xOffset, int yOffset)
    {
      foreach (Gump gump in this.m_Children.ToArray())
      {
        gump.X += xOffset;
        gump.Y += yOffset;
      }
    }

    public virtual void Center()
    {
      if (this.m_Parent == null)
      {
        this.X = (Engine.ScreenWidth - this.Width) / 2;
        this.Y = (Engine.ScreenHeight - this.Height) / 2;
      }
      else
      {
        this.X = (this.m_Parent.Width - this.Width) / 2;
        this.Y = (this.m_Parent.Height - this.Height) / 2;
      }
    }

    public void RemoveTag(string Name)
    {
      this.m_Tags.Remove((object) Name);
    }

    public object GetTag(string Name)
    {
      if (this.m_Tags == null)
        return (object) null;
      return this.m_Tags[(object) Name];
    }

    public void SetTag(string Name, object Value)
    {
      if (this.m_Tags == null)
        this.m_Tags = new Hashtable();
      this.m_Tags[(object) Name] = Value;
    }

    public bool HasTag(string Name)
    {
      if (this.m_Tags == null)
        return false;
      return this.m_Tags.Contains((object) Name);
    }

    public bool IsChildOf(Gump g)
    {
      for (Gump gump = this; gump != null; gump = gump.Parent)
      {
        if (gump == g)
          return true;
      }
      return false;
    }

    public Point PointToScreen(Point p)
    {
      int xOffset = 0;
      int yOffset = 0;
      for (Gump gump = this; gump != null; gump = gump.Parent)
      {
        xOffset += gump.X;
        yOffset += gump.Y;
      }
      return new Point(p, xOffset, yOffset);
    }

    public Point PointToClient(Point p)
    {
      int num1 = 0;
      int num2 = 0;
      for (Gump gump = this; gump != null; gump = gump.Parent)
      {
        num1 += gump.X;
        num2 += gump.Y;
      }
      return new Point(p, -num1, -num2);
    }

    protected internal virtual void Draw(int X, int Y)
    {
    }

    protected internal virtual void Render(int X, int Y)
    {
      if (!this.m_Visible)
        return;
      int X1 = X + this.X;
      int Y1 = Y + this.Y;
      this.Draw(X1, Y1);
      foreach (Gump gump in this.m_Children.ToArray())
        gump.Render(X1, Y1);
    }

    protected internal virtual bool HitTest(int X, int Y)
    {
      return false;
    }

    protected internal virtual void OnDispose()
    {
    }

    protected internal virtual void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
    }

    protected internal virtual void OnMouseDown(int X, int Y, MouseButtons mb)
    {
    }

    protected internal virtual void OnMouseMove(int X, int Y, MouseButtons mb)
    {
    }

    protected internal virtual void OnMouseUp(int X, int Y, MouseButtons mb)
    {
    }

    protected internal virtual void OnMouseWheel(int Delta)
    {
    }

    protected internal virtual void OnSingleClick(int X, int Y)
    {
    }

    protected internal virtual void OnDoubleClick(int X, int Y)
    {
    }

    protected internal virtual bool OnKeyDown(char Key)
    {
      return false;
    }

    protected internal virtual void OnMouseLeave()
    {
    }

    protected internal virtual void OnFocusChanged(Gump Focused)
    {
    }

    protected internal virtual void OnDragDrop(Gump g)
    {
    }

    protected internal virtual void OnDragEnter(Gump g)
    {
    }

    protected internal virtual void OnDragLeave(Gump g)
    {
    }

    protected internal virtual void OnDragMove()
    {
    }

    protected internal virtual void OnDragStart()
    {
    }
  }
}
