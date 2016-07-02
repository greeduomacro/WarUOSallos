// Decompiled with JetBrains decompiler
// Type: PlayUO.GSkinHuePickerAll
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSkinHuePickerAll : Gump
  {
    private const int xShades = 7;
    private const int yShades = 1;
    private const int zShades = 8;
    private int xSize;
    private int ySize;
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

    public int Hue
    {
      get
      {
        return 1002 + (this.m_yShade * 7 + this.m_xShade) * 8 + this.m_zShade;
      }
    }

    public override int Width
    {
      get
      {
        return this.xSize * 7;
      }
    }

    public override int Height
    {
      get
      {
        return this.ySize * 8;
      }
    }

    public GSkinHuePickerAll(int X, int Y, int Width, int Height)
      : base(X, Y)
    {
      do
      {
        ++this.xSize;
      }
      while (this.xSize * 7 <= Width);
      --this.xSize;
      do
      {
        ++this.ySize;
      }
      while (this.ySize * 8 <= Height);
      --this.ySize;
      this.m_ColorTable = new int[7, 1, 8];
      for (int index1 = 0; index1 < 7; ++index1)
      {
        for (int index2 = 0; index2 < 1; ++index2)
        {
          for (int index3 = 0; index3 < 8; ++index3)
          {
            ushort num1 = Hues.GetData(1001 + (index2 * 7 + index1) * 8 + index3).colors[48];
            int num2 = (int) ((double) ((int) num1 >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num1 >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num1 & 31) * 8.22580623626709);
            this.m_ColorTable[index1, index2, index3] = num2;
          }
        }
      }
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

    private void ChangeShade(int X, int Y)
    {
      int num1 = this.m_xShade;
      int num2 = this.m_yShade;
      int num3 = this.m_zShade;
      this.m_xShade = X / this.xSize;
      this.m_yShade = Y / this.ySize;
      this.m_zShade = this.m_yShade / 1;
      this.m_yShade %= 1;
      if (this.m_xShade == num1 && this.m_yShade == num2 && this.m_zShade == num3)
        return;
      if (this.m_OnHueSelect != null)
        this.m_OnHueSelect(this.Hue, (Gump) this);
      Engine.Redraw();
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      for (int index1 = 0; index1 < 7; ++index1)
      {
        for (int index2 = 0; index2 < 1; ++index2)
        {
          for (int index3 = 0; index3 < 8; ++index3)
            Renderer.SolidRect(this.m_ColorTable[index1, index2, index3], X + index1 * this.xSize, Y + (index3 + index2) * this.ySize, this.xSize, this.ySize);
        }
      }
      Renderer.SolidRect(8454143, X + this.m_xShade * this.xSize + (this.xSize - 3) / 2, Y + (this.m_zShade + this.m_yShade) * this.ySize + (this.ySize - 1) / 2, 3, 1);
      Renderer.SolidRect(8454143, X + this.m_xShade * this.xSize + (this.xSize - 1) / 2, Y + (this.m_zShade + this.m_yShade) * this.ySize + (this.ySize - 3) / 2, 1, 3);
    }
  }
}
