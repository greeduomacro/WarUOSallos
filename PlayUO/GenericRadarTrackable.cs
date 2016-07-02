// Decompiled with JetBrains decompiler
// Type: PlayUO.GenericRadarTrackable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GenericRadarTrackable : IRadarTrackable
  {
    private int _x;
    private int _y;
    private int _facet;
    private int _color;
    private string _name;
    private DateTime _createdOn;
    private TimeSpan _duration;

    public int X
    {
      get
      {
        return this._x;
      }
      set
      {
        this._x = value;
      }
    }

    public int Y
    {
      get
      {
        return this._y;
      }
      set
      {
        this._y = value;
      }
    }

    public int Facet
    {
      get
      {
        return this._facet;
      }
      set
      {
        this._facet = value;
      }
    }

    public int Color
    {
      get
      {
        return this._color;
      }
      set
      {
        this._color = value;
      }
    }

    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return this._duration;
      }
      set
      {
        this._duration = value;
      }
    }

    public bool HasExpired
    {
      get
      {
        return DateTime.UtcNow >= this._createdOn + this._duration;
      }
    }

    public GenericRadarTrackable(TimeSpan duration)
    {
      this._duration = duration;
    }

    public GenericRadarTrackable(TimeSpan duration, int x, int y, int facet, int color, string name)
      : this(duration)
    {
      this._x = x;
      this._y = y;
      this._facet = facet;
      this._color = color;
      this._name = name;
      this.Refresh();
    }

    public void Refresh()
    {
      this._createdOn = DateTime.UtcNow;
    }
  }
}
