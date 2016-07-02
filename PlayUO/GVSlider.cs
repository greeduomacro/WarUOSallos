// Decompiled with JetBrains decompiler
// Type: PlayUO.GVSlider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GVSlider : Gump
  {
    protected double m_ScrollOffset = 5.0;
    private bool m_Draw;
    private Texture m_Gump;
    private int m_HalfHeight;
    private int m_xOffset;
    private int m_Width;
    private int m_Height;
    private int m_Position;
    private double m_Start;
    private double m_End;
    private double m_Increase;
    private OnValueChange m_OnValueChange;
    private VertexCache m_vCache;

    public double ScrollOffset
    {
      get
      {
        return this.m_ScrollOffset;
      }
      set
      {
        this.m_ScrollOffset = value;
      }
    }

    public OnValueChange OnValueChange
    {
      get
      {
        return this.m_OnValueChange;
      }
      set
      {
        this.m_OnValueChange = value;
      }
    }

    public int HalfHeight
    {
      get
      {
        return this.m_HalfHeight;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
        this.m_xOffset = (this.m_Width - this.m_Gump.Width) / 2;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
      }
    }

    public double Start
    {
      get
      {
        return this.m_Start;
      }
      set
      {
        this.m_Start = value;
      }
    }

    public double End
    {
      get
      {
        return this.m_End;
      }
      set
      {
        this.m_End = value;
      }
    }

    public double Increase
    {
      get
      {
        return this.m_Increase;
      }
      set
      {
        this.m_Increase = value;
      }
    }

    public int Position
    {
      get
      {
        return this.m_Position;
      }
      set
      {
        this.m_Position = value;
      }
    }

    public GVSlider(int SliderID, int X, int Y, int Width, int Height, double Value, double Start, double End, double Increase)
      : this(SliderID, Hues.Default, X, Y, Width, Height, Value, Start, End, Increase)
    {
    }

    public GVSlider(int SliderID, IHue Hue, int X, int Y, int Width, int Height, double Value, double Start, double End, double Increase)
      : base(X, Y)
    {
      this.m_vCache = new VertexCache();
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Start = Start;
      this.m_End = End;
      this.m_Increase = Increase;
      this.m_Gump = Hue.GetGump(SliderID);
      if (this.m_Gump != null && !this.m_Gump.IsEmpty())
      {
        this.m_HalfHeight = this.m_Gump.Height / 2;
        this.m_xOffset = (this.m_Width - this.m_Gump.Width) / 2;
        this.m_Draw = true;
      }
      this.SetValue(Value, false);
    }

    private void Slide(int Y)
    {
      Gumps.Capture = (Gump) this;
      if (Y < 0)
        Y = 0;
      if (Y >= this.m_Height)
        Y = this.m_Height - 1;
      int num = this.m_Position;
      double Old = this.GetValue();
      this.m_Position = Y;
      if (num == Y)
        return;
      if (this.m_OnValueChange != null)
        this.m_OnValueChange(this.GetValue(), Old, (Gump) this);
      Engine.Redraw();
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(Y);
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(Y);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(Y);
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      this.SetValue(this.GetValue() + (double) -Math.Sign(Delta) * this.m_ScrollOffset * this.m_Increase, true);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.None)
        this.Slide(Y);
      Gumps.Capture = (Gump) null;
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      this.m_vCache.Draw(this.m_Gump, X + this.m_xOffset, Y + this.m_Position - this.m_HalfHeight);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    public void SetValue(double Value, bool CallOnChange)
    {
      if (Value > this.m_End)
        Value = this.m_End;
      else if (Value < this.m_Start)
        Value = this.m_Start;
      double Old = this.GetValue();
      double num = (Value - this.m_Start) / ((this.m_End - this.m_Start + 1.0) / this.m_Increase);
      if (num < 0.0)
        num = 0.0;
      else if (num > 1.0)
        num = 1.0;
      this.m_Position = (int) (num * (double) this.m_Height + 0.5);
      if (Value == this.m_End && Value != this.m_Start)
        this.m_Position = this.m_Height - 1;
      if (this.m_Position < 0)
        this.m_Position = 0;
      else if (this.m_Position >= this.m_Height)
        this.m_Position = this.m_Height - 1;
      if (CallOnChange && this.m_OnValueChange != null)
        this.m_OnValueChange(Value, Old, (Gump) this);
      this.GetValue();
    }

    public double GetValue()
    {
      return this.GetValue(this.m_Position);
    }

    public double GetValue(int Position)
    {
      if (Position < 0)
        return this.m_Start;
      if (Position >= this.m_Height - 1)
        return this.m_End;
      double num = (double) (int) ((double) Position / (double) this.m_Height * ((this.m_End - this.m_Start + 1.0) / this.m_Increase) + 0.5) * this.m_Increase + this.m_Start;
      if (num > this.m_End)
        num = this.m_End;
      else if (num < this.m_Start)
        num = this.m_Start;
      return num;
    }
  }
}
