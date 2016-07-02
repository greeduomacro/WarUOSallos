// Decompiled with JetBrains decompiler
// Type: PlayUO.GHuePickerAll
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GHuePickerAll : Gump
  {
    private const int xShades = 20;
    private const int yShades = 10;
    private const int zShades = 5;
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
        return 2 + (this.m_yShade * 20 + this.m_xShade) * 5 + this.m_zShade;
      }
    }

    public override int Width
    {
      get
      {
        return this.xSize * 20;
      }
    }

    public override int Height
    {
      get
      {
        return this.ySize * 50;
      }
    }

    public GHuePickerAll(int X, int Y, int Width, int Height)
      : base(X, Y)
    {
      do
      {
        ++this.xSize;
      }
      while (this.xSize * 20 <= Width);
      --this.xSize;
      do
      {
        ++this.ySize;
      }
      while (this.ySize * 50 <= Height);
      --this.ySize;
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
      this.m_zShade = this.m_yShade / 10;
      this.m_yShade %= 10;
      if (this.m_xShade == num1 && this.m_yShade == num2 && this.m_zShade == num3)
        return;
      if (this.m_OnHueSelect != null)
        this.m_OnHueSelect(this.Hue, (Gump) this);
      Engine.Redraw();
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        for (int index2 = 0; index2 < 10; ++index2)
        {
          for (int index3 = 0; index3 < 5; ++index3)
            Renderer.SolidRect(this.m_ColorTable[index1, index2, index3], X + index1 * this.xSize, Y + (index3 * 10 + index2) * this.ySize, this.xSize, this.ySize);
        }
      }
      Renderer.SolidRect(8454143, X + this.m_xShade * this.xSize + (this.xSize - 3) / 2, Y + (this.m_zShade * 10 + this.m_yShade) * this.ySize + (this.ySize - 1) / 2, 3, 1);
      Renderer.SolidRect(8454143, X + this.m_xShade * this.xSize + (this.xSize - 1) / 2, Y + (this.m_zShade * 10 + this.m_yShade) * this.ySize + (this.ySize - 3) / 2, 1, 3);
    }
  }
}
