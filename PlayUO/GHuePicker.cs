// Decompiled with JetBrains decompiler
// Type: PlayUO.GHuePicker
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GHuePicker : Gump
  {
    private const int xShades = 20;
    private const int yShades = 10;
    private const int zShades = 5;
    private const int xSize = 8;
    private const int ySize = 8;
    private int m_xShade;
    private int m_yShade;
    private int m_zShade;
    private int[,,] m_ColorTable;
    private OnHueSelect m_OnHueRelease;
    private OnHueSelect m_OnHueSelect;

    public OnHueSelect OnHueSelect
    {
      get
      {
        return this.m_OnHueSelect;
      }
      set
      {
        this.m_OnHueSelect = value;
      }
    }

    public OnHueSelect OnHueRelease
    {
      get
      {
        return this.m_OnHueRelease;
      }
      set
      {
        this.m_OnHueRelease = value;
      }
    }

    public int ShadeX
    {
      set
      {
        this.m_xShade = value;
        if (this.m_xShade < 0)
          this.m_xShade = 0;
        if (this.m_xShade >= 20)
          this.m_xShade = 19;
        if (this.m_OnHueSelect != null)
          this.m_OnHueSelect(this.Hue, (Gump) this);
        if (Engine.GMPrivs)
          ((Tooltip) this.m_Tooltip).Text = string.Format("0x{0:X}", (object) this.Hue);
        Engine.Redraw();
      }
    }

    public int ShadeY
    {
      set
      {
        this.m_yShade = value;
        if (this.m_yShade < 0)
          this.m_yShade = 0;
        if (this.m_yShade >= 10)
          this.m_yShade = 9;
        if (this.m_OnHueSelect != null)
          this.m_OnHueSelect(this.Hue, (Gump) this);
        if (Engine.GMPrivs)
          ((Tooltip) this.m_Tooltip).Text = string.Format("0x{0:X}", (object) this.Hue);
        Engine.Redraw();
      }
    }

    public int Brightness
    {
      get
      {
        return this.m_zShade;
      }
      set
      {
        this.m_zShade = value;
        if (this.m_zShade < 0)
          this.m_zShade = 0;
        if (this.m_zShade >= 5)
          this.m_zShade = 4;
        if (this.m_OnHueSelect != null)
          this.m_OnHueSelect(this.Hue, (Gump) this);
        if (Engine.GMPrivs)
          ((Tooltip) this.m_Tooltip).Text = string.Format("0x{0:X}", (object) this.Hue);
        Engine.Redraw();
      }
    }

    public int Hue
    {
      get
      {
        return 2 + (this.m_yShade * 20 + this.m_xShade) * 5 + this.m_zShade;
      }
    }

    public override int Width
    {
      get
      {
        return 160;
      }
    }

    public override int Height
    {
      get
      {
        return 80;
      }
    }

    public GHuePicker(int X, int Y)
      : base(X, Y)
    {
      this.m_Tooltip = (ITooltip) new Tooltip("");
      this.m_ColorTable = new int[20, 10, 5];
      for (int index1 = 0; index1 < 20; ++index1)
      {
        for (int index2 = 0; index2 < 10; ++index2)
        {
          for (int index3 = 0; index3 < 5; ++index3)
          {
            ushort num1 = Hues.GetData(1 + (index2 * 20 + index1) * 5 + index3).colors[48];
            int num2 = (int) ((double) ((int) num1 >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num1 >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num1 & 31) * 8.22580623626709);
            this.m_ColorTable[index1, index2, index3] = num2;
          }
        }
      }
    }

    public int Color(int Index)
    {
      return this.m_ColorTable[this.m_xShade, this.m_yShade, Index];
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.ChangeShade(X, Y);
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      this.ChangeShade(X, Y);
      if (this.m_OnHueRelease == null)
        return;
      this.m_OnHueRelease(this.Hue, (Gump) this);
    }

    protected internal override void OnMouseMove(int X, int Y, MouseButtons mb)
    {
      if (mb == MouseButtons.None)
        return;
      this.ChangeShade(X, Y);
    }

    private int ColorAt(int X, int Y)
    {
      if (X >= 20)
        X = 19;
      if (Y >= 10)
        Y = 9;
      return this.m_ColorTable[X, Y, this.m_zShade];
    }

    private void ChangeShade(int X, int Y)
    {
      int num1 = this.m_xShade;
      int num2 = this.m_yShade;
      this.m_xShade = X / 8;
      this.m_yShade = Y / 8;
      if (this.m_xShade == num1 && this.m_yShade == num2)
        return;
      if (this.m_OnHueSelect != null)
        this.m_OnHueSelect(this.Hue, (Gump) this);
      if (Engine.GMPrivs)
        ((Tooltip) this.m_Tooltip).Text = string.Format("0x{0:X}", (object) this.Hue);
      Engine.Redraw();
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      int Width1 = 4;
      int Width2 = 8 - Width1;
      int Height1 = 4;
      int Height2 = 8 - Height1;
      Renderer.SolidRect(this.ColorAt(0, 0), X, Y, Width1, Height1);
      Renderer.SolidRect(this.ColorAt(0, 10), X, Y + 80 - Height2, Width1, Height2);
      Renderer.SolidRect(this.ColorAt(20, 0), X + 160 - Width2, Y, Width2, Height1);
      Renderer.SolidRect(this.ColorAt(20, 10), X + 160 - Width2, Y + 80 - Height2, Width2, Height2);
      for (int X1 = 0; X1 < 19; ++X1)
      {
        int num1 = this.ColorAt(X1, 0);
        int num2 = this.ColorAt(X1 + 1, 0);
        Renderer.GradientRect4(num1, num2, num2, num1, X + X1 * 8 + Width1, Y, 8, Height1);
        int num3 = this.ColorAt(X1, 10);
        int num4 = this.ColorAt(X1 + 1, 10);
        Renderer.GradientRect4(num3, num4, num4, num3, X + X1 * 8 + Width1, Y + 80 - Height2, 8, Height2);
        for (int Y1 = 0; Y1 < 9; ++Y1)
        {
          int c00 = this.ColorAt(X1, Y1);
          int c10 = this.ColorAt(X1 + 1, Y1);
          int c11 = this.ColorAt(X1 + 1, Y1 + 1);
          int c01 = this.ColorAt(X1, Y1 + 1);
          int num5 = X + X1 * 8;
          int num6 = Y + Y1 * 8;
          Renderer.GradientRect4(c00, c10, c11, c01, num5 + Width1, num6 + Height1, 8, 8);
        }
      }
      for (int Y1 = 0; Y1 < 9; ++Y1)
      {
        int num1 = this.ColorAt(0, Y1);
        int num2 = this.ColorAt(0, Y1 + 1);
        Renderer.GradientRect4(num1, num1, num2, num2, X, Y + Y1 * 8 + Height1, Width1, 8);
        int num3 = this.ColorAt(20, Y1);
        int num4 = this.ColorAt(20, Y1 + 1);
        Renderer.GradientRect4(num3, num3, num4, num4, X + 160 - Width2, Y + Y1 * 8 + Height1, Width2, 8);
      }
      Renderer.PushAlpha(0.5f);
      Renderer.SolidRect(8454143, X + this.m_xShade * 8 + 2, Y + this.m_yShade * 8 + 3, 3, 1);
      Renderer.SolidRect(8454143, X + this.m_xShade * 8 + 3, Y + this.m_yShade * 8 + 2, 1, 3);
      Renderer.PopAlpha();
      Renderer.SolidRect(8454143, X + this.m_xShade * 8 + 3, Y + this.m_yShade * 8 + 3, 1, 1);
    }
  }
}
