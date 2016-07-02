// Decompiled with JetBrains decompiler
// Type: PlayUO.GBorder3D
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GBorder3D : GAlphaBackground
  {
    protected bool m_Inset;

    public bool Inset
    {
      get
      {
        return this.m_Inset;
      }
      set
      {
        this.m_Inset = value;
      }
    }

    public GBorder3D(bool inset, int x, int y, int width, int height)
      : base(x, y, width, height)
    {
      this.m_Inset = inset;
      this.FillAlpha = 1f;
      this.FillColor = 12632256;
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(this.m_FillAlpha);
      if ((double) this.m_FillAlpha == 1.0)
        Renderer.SolidRect(this.m_FillColor, X + 1, Y + 1, this.m_Width - 2, this.m_Height - 2);
      else
        Renderer.SolidRect(this.m_FillColor, X + 1, Y + 1, this.m_Width - 2, this.m_Height - 2);
      Renderer.PopAlpha();
      Renderer.SolidRect(this.m_Inset ? 4210752 : 16777215, X, Y, this.m_Width - 1, 1);
      Renderer.SolidRect(this.m_Inset ? 4210752 : 16777215, X, Y + 1, 1, this.m_Height - 2);
      Renderer.SolidRect(12632256, X, Y + this.m_Height - 1, 1, 1);
      Renderer.SolidRect(12632256, X + this.m_Width - 1, Y, 1, 1);
      Renderer.SolidRect(this.m_Inset ? 16777215 : 4210752, X + 1, Y + this.m_Height - 1, this.m_Width - 1, 1);
      Renderer.SolidRect(this.m_Inset ? 16777215 : 4210752, X + this.m_Width - 1, Y + 1, 1, this.m_Height - 2);
    }
  }
}
