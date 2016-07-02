// Decompiled with JetBrains decompiler
// Type: PlayUO.GBrightnessBar
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GBrightnessBar : Gump
  {
    private int m_Width;
    private int m_Height;
    private int[] m_ChunkHeights;
    private GHuePicker m_Target;
    private int m_Position;

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
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
        this.FillHeights();
      }
    }

    public GBrightnessBar(int X, int Y, int Width, int Height, GHuePicker Target)
      : base(X, Y)
    {
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Target = Target;
      this.FillHeights();
    }

    public void Refresh()
    {
      this.m_Position = (int) ((double) this.m_Target.Brightness / 4.0 * (double) (this.m_Height - 1));
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

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.None)
        this.Slide(Y);
      Gumps.Capture = (Gump) null;
    }

    protected internal override void OnMouseWheel(int Delta)
    {
      if (Delta > 0 && this.m_Target.Brightness > 0)
        --this.m_Target.Brightness;
      else if (Delta < 0 && this.m_Target.Brightness < 4)
        ++this.m_Target.Brightness;
      this.m_Position = (int) ((double) this.m_Target.Brightness / 4.0 * (double) (this.m_Height - 1));
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    public void Slide(int Y)
    {
      Gumps.Capture = (Gump) this;
      this.m_Position = Y;
      if (this.m_Position < 0)
        this.m_Position = 0;
      else if (this.m_Position >= this.m_Height)
        this.m_Position = this.m_Height - 1;
      int num = (int) (((double) this.m_Position / (double) (this.m_Height - 1) - 1E-13) * 5.0);
      if (this.m_Target.Brightness == num)
        return;
      this.m_Target.Brightness = num;
    }

    private void FillHeights()
    {
      this.m_ChunkHeights = new int[4];
      for (int index = 0; index < 4; ++index)
        this.m_ChunkHeights[index] = this.m_Height / 4;
      for (int index = 0; index < this.m_Height % 4; ++index)
        ++this.m_ChunkHeights[index];
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      int num = 0;
      for (int Index = 0; Index < 4; ++Index)
      {
        Renderer.GradientRect(this.m_Target.Color(Index), this.m_Target.Color(Index + 1), X, Y + num, this.m_Width, this.m_ChunkHeights[Index]);
        num += this.m_ChunkHeights[Index];
      }
      Engine.m_Slider.Draw(X, Y + this.m_Position - 1, 0);
    }
  }
}
