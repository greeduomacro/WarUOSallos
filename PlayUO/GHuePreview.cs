// Decompiled with JetBrains decompiler
// Type: PlayUO.GHuePreview
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GHuePreview : Gump
  {
    private int m_Width;
    private int m_Height;
    private int m_Hue;
    private bool m_Solid;
    private float m_xRun;
    private int[] m_Colors;

    public int Hue
    {
      get
      {
        return this.m_Hue;
      }
      set
      {
        if (this.m_Hue == value)
          return;
        this.m_Hue = value;
        if (!this.m_Solid)
        {
          this.m_xRun = 31f / (float) this.Width;
          this.m_Colors = new int[32];
          for (int index = 0; index < 32; ++index)
          {
            ushort num1 = Hues.GetData(this.m_Hue - 1 & (int) short.MaxValue).colors[32 + index];
            int num2 = (int) ((double) ((int) num1 >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num1 >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num1 & 31) * 8.22580623626709);
            this.m_Colors[index] = num2;
          }
        }
        else
        {
          ushort num = Hues.GetData(this.m_Hue - 1 & (int) short.MaxValue).colors[48];
          this.m_Colors = new int[1]
          {
            (int) ((double) ((int) num >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num & 31) * 8.22580623626709)
          };
        }
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

    public GHuePreview(int X, int Y, int Width, int Height, int Hue, bool Solid)
      : base(X, Y)
    {
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Hue = Hue;
      this.m_Solid = Solid;
      if (!Solid)
      {
        this.m_xRun = 31f / (float) Width;
        this.m_Colors = new int[32];
        for (int index = 0; index < 32; ++index)
        {
          ushort num1 = Hues.GetData(Hue - 1 & (int) short.MaxValue).colors[32 + index];
          int num2 = (int) ((double) ((int) num1 >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num1 >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num1 & 31) * 8.22580623626709);
          this.m_Colors[index] = num2;
        }
      }
      else
      {
        ushort num = Hues.GetData(Hue - 1 & (int) short.MaxValue).colors[48];
        this.m_Colors = new int[1]
        {
          (int) ((double) ((int) num >> 10 & 31) * 8.22580623626709) << 16 | (int) ((double) ((int) num >> 5 & 31) * 8.22580623626709) << 8 | (int) ((double) ((int) num & 31) * 8.22580623626709)
        };
      }
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      if (this.m_Solid)
        Renderer.SolidRect(this.m_Colors[0], X, Y, this.m_Width, this.m_Height);
      else
        Renderer.GradientRectLR(this.m_Colors[0], this.m_Colors[31], X, Y, this.m_Width, this.m_Height);
    }
  }
}
