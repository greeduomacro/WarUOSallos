// Decompiled with JetBrains decompiler
// Type: PlayUO.GSlider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSlider : Gump
  {
    private bool m_Draw;
    private Texture m_Gump;
    private int m_HalfWidth;
    private int m_yOffset;
    private int m_Width;
    private int m_Height;
    private int m_Position;
    private double m_Start;
    private double m_End;
    private double m_Increase;
    private VertexCache m_vCache;
    private OnValueChange m_OnValueChange;

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

    public GSlider(int SliderID, int X, int Y, int Width, int Height, double Value, double Start, double End, double Increase)
      : this(SliderID, Hues.Default, X, Y, Width, Height, Value, Start, End, Increase)
    {
    }

    public GSlider(int SliderID, IHue Hue, int X, int Y, int Width, int Height, double Value, double Start, double End, double Increase)
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
        this.m_HalfWidth = this.m_Gump.Width / 2;
        this.m_yOffset = (this.m_Height - this.m_Gump.Height) / 2;
        this.m_Draw = true;
      }
      this.SetValue(Value, false);
    }

    public void Slide(int X)
    {
      Gumps.Capture = (Gump) this;
      int num1 = this.m_Position;
      double Old = this.GetValue();
      int num2 = X;
      this.m_Position = X;
      if (this.m_Position < 0)
        this.m_Position = 0;
      else if (this.m_Position >= this.m_Width)
        this.m_Position = this.m_Width - 1;
      double num3 = this.m_End - this.m_Start + 1.0 - 1E-13;
      double num4 = (double) this.m_Position / (double) (this.m_Width - 1);
      if (this.m_Position == 0)
        num4 = 0.0;
      else if (this.m_Position == this.m_Width - 1)
        num4 = 1.0;
      double num5 = (double) (int) (num4 * num3 + this.m_Start);
      if (Old == num5 && num1 == this.m_Position && num2 == num1)
        return;
      if (this.m_OnValueChange != null)
        this.m_OnValueChange(num5, Old, (Gump) this);
      Engine.Redraw();
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(X);
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(X);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.Slide(X);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.None)
        this.Slide(X);
      Gumps.Capture = (Gump) null;
    }

    protected internal override void Draw(int X, int Y)
    {
      if (!this.m_Draw)
        return;
      this.m_vCache.Draw(this.m_Gump, X + this.m_Position - this.m_HalfWidth, Y + this.m_yOffset);
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
      double num = (Value - this.m_Start) / (this.m_End - this.m_Start);
      if (num < 0.0)
        num = 0.0;
      else if (num > 1.0)
        num = 1.0;
      this.m_Position = (int) (num * (double) (this.m_Width - 1));
      if (!CallOnChange || this.m_OnValueChange == null)
        return;
      this.m_OnValueChange(Value, Old, (Gump) this);
    }

    public double GetValue()
    {
      return this.GetValue(this.m_Position);
    }

    public double GetValue(int Position)
    {
      if (Position < 0)
        Position = 0;
      else if (Position >= this.m_Width)
        Position = this.m_Width - 1;
      double num = this.m_End - this.m_Start + 1.0 - 1E-13;
      return (double) (int) ((double) Position / (double) (this.m_Width - 1) * num + this.m_Start);
    }
  }
}
