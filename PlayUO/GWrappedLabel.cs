// Decompiled with JetBrains decompiler
// Type: PlayUO.GWrappedLabel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GWrappedLabel : GLabel
  {
    protected int m_WrapWidth;

    public GWrappedLabel(string text, IFont font, IHue hue, int x, int y, int width)
      : base(x, y)
    {
      this.m_WrapWidth = width;
      this.m_Text = text;
      this.m_Font = font;
      this.m_Hue = hue;
      this.m_ITranslucent = true;
      this.Refresh();
    }

    public override void Refresh()
    {
      if (!this.m_Invalidated)
        return;
      this.m_Image = this.m_Font.GetString(Engine.WrapText(this.m_Text, this.m_WrapWidth, this.m_Font), this.m_Hue);
      if (this.m_Draw = this.m_Image != null && !this.m_Image.IsEmpty())
      {
        this.m_Width = this.m_Image.Width;
        this.m_Height = this.m_Image.Height;
      }
      this.m_Invalidated = false;
      if (this.m_vCache == null)
        return;
      this.m_vCache.Invalidate();
    }
  }
}
