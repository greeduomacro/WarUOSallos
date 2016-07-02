// Decompiled with JetBrains decompiler
// Type: PlayUO.GThumbSlider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public abstract class GThumbSlider : GSliderBase
  {
    private SliderOrientation _orientation;
    private SliderState _state;
    private int _seekOffset;

    public SliderOrientation Orientation
    {
      get
      {
        return this._orientation;
      }
      set
      {
        this._orientation = value;
      }
    }

    public SliderState State
    {
      get
      {
        return this._state;
      }
      set
      {
        this._state = value;
      }
    }

    public GThumbSlider(int x, int y, SliderOrientation orientation)
      : base(x, y)
    {
      this._orientation = orientation;
    }

    protected virtual int GetTrackSize()
    {
      switch (this._orientation)
      {
        case SliderOrientation.Horizontal:
          return this.Width;
        case SliderOrientation.Vertical:
          return this.Height;
        default:
          throw new InvalidOperationException();
      }
    }

    protected virtual int GetPosition(int x, int y)
    {
      switch (this._orientation)
      {
        case SliderOrientation.Horizontal:
          return x;
        case SliderOrientation.Vertical:
          return y;
        default:
          throw new InvalidOperationException();
      }
    }

    protected abstract int GetThumbSize();

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      int trackSize = this.GetTrackSize();
      int thumbSize = this.GetThumbSize();
      int position1 = this.GetPosition(trackSize - thumbSize);
      switch (this._state)
      {
        case SliderState.SmallUp:
          this.Value -= this.SmallOffset;
          break;
        case SliderState.SmallDown:
          this.Value += this.SmallOffset;
          break;
        case SliderState.LargeUp:
          Point client1 = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse));
          int position2 = this.GetPosition(client1.X, client1.Y);
          if (position1 > position2)
          {
            this.Value -= this.LargeOffset;
            break;
          }
          this._state = SliderState.Idle;
          break;
        case SliderState.LargeDown:
          Point client2 = this.PointToClient(new Point(Engine.m_xMouse, Engine.m_yMouse));
          int position3 = this.GetPosition(client2.X, client2.Y);
          if (position1 + thumbSize < position3)
          {
            this.Value += this.LargeOffset;
            break;
          }
          this._state = SliderState.Idle;
          break;
      }
      this.DrawThumb(X, Y, position1);
    }

    protected abstract void DrawThumb(int x, int y, int p);

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (this._state == SliderState.Seek && Gumps.Capture == this)
      {
        int trackSize = this.GetTrackSize();
        int thumbSize = this.GetThumbSize();
        this.Value = this.GetValue(this.GetPosition(X, Y) - this._seekOffset, trackSize - thumbSize);
      }
      this._state = SliderState.Idle;
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (this._state != SliderState.Seek || Gumps.Capture != this)
        return;
      int trackSize = this.GetTrackSize();
      int thumbSize = this.GetThumbSize();
      this.Value = this.GetValue(this.GetPosition(X, Y) - this._seekOffset, trackSize - thumbSize);
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      int trackSize = this.GetTrackSize();
      int thumbSize = this.GetThumbSize();
      int position = this.GetPosition(trackSize - thumbSize);
      int num = this.GetPosition(X, Y) - position;
      SliderState sliderState;
      if (num < 0)
        sliderState = SliderState.LargeUp;
      else if (num >= thumbSize)
      {
        sliderState = SliderState.LargeDown;
      }
      else
      {
        sliderState = SliderState.Seek;
        this._seekOffset = num;
      }
      this.State = sliderState;
      Gumps.Capture = (Gump) this;
    }
  }
}
