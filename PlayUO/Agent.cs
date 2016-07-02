// Decompiled with JetBrains decompiler
// Type: PlayUO.Agent
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public abstract class Agent : IEntity, IPoint3D, IPoint2D
  {
    private static List<Item> _emptyItems = new List<Item>();
    private int _serial;
    private Agent _parent;
    private int _x;
    private int _y;
    private int _z;
    private List<Mobile> _mobiles;
    private List<Item> _items;

    public int Serial
    {
      get
      {
        return this._serial;
      }
    }

    public Agent Parent
    {
      get
      {
        return this._parent;
      }
    }

    public int X
    {
      get
      {
        return this._x;
      }
    }

    public int Y
    {
      get
      {
        return this._y;
      }
    }

    public int Z
    {
      get
      {
        return this._z;
      }
    }

    public bool InWorld
    {
      get
      {
        return this._parent is WorldAgent;
      }
    }

    public Agent WorldRoot
    {
      get
      {
        for (Agent agent = this; agent._parent != null; agent = agent._parent)
        {
          if (agent.InWorld)
            return agent;
        }
        return (Agent) null;
      }
    }

    public bool HasItems
    {
      get
      {
        if (this._items != null)
          return this._items.Count > 0;
        return false;
      }
    }

    public List<Item> Items
    {
      get
      {
        if (this._items != null)
          return this._items;
        return Agent._emptyItems;
      }
    }

    public Agent(int serial)
    {
      this._serial = serial;
    }

    public void SetLocation(int x, int y, int z)
    {
      this.SetLocation(this._parent, x, y, z);
    }

    public void SetLocation(Agent parent, int x, int y, int z)
    {
      if (this._parent != parent)
      {
        if (this._parent != null)
          this._parent.RemoveChild(this);
        this._x = x;
        this._y = y;
        this._z = z;
        this.OnLocationChanged();
        if (parent == null)
          return;
        parent.AddChild(this);
      }
      else
      {
        this._x = x;
        this._y = y;
        this._z = z;
        this.OnLocationChanged();
      }
    }

    public bool IsChildOf(Agent agent)
    {
      if (agent == null)
        return false;
      for (Agent agent1 = this._parent; agent1 != null; agent1 = agent1._parent)
      {
        if (agent1 == agent)
          return true;
      }
      return false;
    }

    public bool GetWorldLocation(out int x, out int y, out int z)
    {
      Agent worldRoot = this.WorldRoot;
      if (worldRoot != null)
      {
        x = worldRoot.X;
        y = worldRoot.Y;
        z = worldRoot.Z;
        return true;
      }
      x = -1;
      y = -1;
      z = 0;
      return false;
    }

    public double DistanceSqrt(IPoint2D p)
    {
      int num1 = this._x - p.X;
      int num2 = this._y - p.Y;
      return Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }

    public int DistanceTo(int xTile, int yTile)
    {
      int num1 = this._x - xTile;
      int num2 = this._y - yTile;
      return (int) Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }

    public bool InRange(IPoint2D p, int range)
    {
      if (p != null)
        return this.InRange(p.X, p.Y, range);
      return false;
    }

    public bool InRange(int x, int y, int range)
    {
      int x1;
      int y1;
      int z;
      if (this.GetWorldLocation(out x1, out y1, out z) && x1 >= x - range && (x1 <= x + range && y1 >= y - range))
        return y1 <= y + range;
      return false;
    }

    private void ClearChildren()
    {
      if (this._items != null)
        this._items.Clear();
      if (this._mobiles == null)
        return;
      this._mobiles.Clear();
    }

    private void AddChild(Agent child)
    {
      if (child.Parent == this)
        return;
      if (child.Parent != null)
        child.Parent.RemoveChild(child);
      child._parent = this;
      if (child is Item)
      {
        if (this._items == null)
          this._items = new List<Item>();
        this._items.Add((Item) child);
      }
      else if (child is Mobile)
      {
        if (this._mobiles == null)
          this._mobiles = new List<Mobile>();
        this._mobiles.Add((Mobile) child);
      }
      this.OnChildAdded(child);
      child.OnParentChanged(this);
    }

    private void RemoveChild(Agent child)
    {
      if (child.Parent != this)
        return;
      child._parent = (Agent) null;
      if (this._items != null && child is Item)
        this._items.Remove((Item) child);
      if (this._mobiles != null && child is Mobile)
        this._mobiles.Remove((Mobile) child);
      this.OnChildRemoved(child);
      child.OnParentChanged((Agent) null);
    }

    public void Delete()
    {
      this.OnDeleted();
      while (this._items != null && this._items.Count > 0)
        this._items[this._items.Count - 1].Delete();
      while (this._mobiles != null && this._mobiles.Count > 0)
        this._mobiles[this._mobiles.Count - 1].Delete();
    }

    protected virtual void OnDeleted()
    {
      if (this._parent == null)
        return;
      this._parent.RemoveChild(this);
    }

    protected virtual void OnLocationChanged()
    {
    }

    protected virtual void OnParentChanged(Agent parent)
    {
    }

    protected virtual void OnChildAdded(Agent child)
    {
    }

    protected virtual void OnChildRemoved(Agent child)
    {
    }
  }
}
