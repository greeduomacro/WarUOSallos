// Decompiled with JetBrains decompiler
// Type: PlayUO.FontInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class FontInfo
  {
    private UnicodeFont m_Font;
    private int m_Color;
    private IHue m_Hue;

    public UnicodeFont Font
    {
      get
      {
        return this.m_Font;
      }
    }

    public int Color
    {
      get
      {
        return this.m_Color;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public FontInfo(UnicodeFont font, int color)
    {
      this.m_Font = font;
      this.m_Color = color;
      this.m_Hue = (IHue) new Hues.ColorFillHue(color);
    }
  }
}
