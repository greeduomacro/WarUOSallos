// Decompiled with JetBrains decompiler
// Type: PlayUO.GRegion
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GRegion : Gump, IClipable
  {
    protected int m_Width;
    protected int m_Height;
    protected Clipper m_Clipper;

    public virtual Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
      }
    }

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

    public GRegion(int x, int y, int width, int height)
      : base(x, y)
    {
      this.m_Width = width;
      this.m_Height = height;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Clipper != null)
        return this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)));
      return true;
    }
  }
}
