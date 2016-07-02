// Decompiled with JetBrains decompiler
// Type: PlayUO.GSingleBorder
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GSingleBorder : Gump
  {
    protected int m_Width;
    protected int m_Height;

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
      }
    }

    public GSingleBorder(int X, int Y, int Width, int Height)
      : base(X, Y)
    {
      this.m_Width = Width;
      this.m_Height = Height;
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      Renderer.TransparentRect(0, X, Y, this.m_Width, this.m_Height);
    }
  }
}
