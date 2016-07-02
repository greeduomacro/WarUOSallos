// Decompiled with JetBrains decompiler
// Type: PlayUO.Clipper
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Clipper
  {
    private static Clipper m_Clipper = new Clipper(0, 0, 0, 0);
    protected int m_xStart;
    protected int m_yStart;
    protected int m_xEnd;
    protected int m_yEnd;

    public int xStart
    {
      get
      {
        return this.m_xStart;
      }
    }

    public int yStart
    {
      get
      {
        return this.m_yStart;
      }
    }

    public int xEnd
    {
      get
      {
        return this.m_xEnd;
      }
    }

    public int yEnd
    {
      get
      {
        return this.m_yEnd;
      }
    }

    public Clipper(int xStart, int yStart, int xWidth, int yHeight)
    {
      this.m_xStart = xStart;
      this.m_yStart = yStart;
      this.m_xEnd = xStart + xWidth;
      this.m_yEnd = yStart + yHeight;
    }

    public static Clipper TemporaryInstance(int x, int y, int width, int height)
    {
      Clipper.m_Clipper.m_xStart = x;
      Clipper.m_Clipper.m_yStart = y;
      Clipper.m_Clipper.m_xEnd = x + width;
      Clipper.m_Clipper.m_yEnd = y + height;
      return Clipper.m_Clipper;
    }

    public bool Evaluate(Point p)
    {
      if (p.X >= this.m_xStart && p.X < this.m_xEnd && p.Y >= this.m_yStart)
        return p.Y < this.m_yEnd;
      return false;
    }

    public bool Evaluate(int xPoint, int yPoint)
    {
      if (xPoint >= this.m_xStart && yPoint >= this.m_yStart && xPoint < this.m_xEnd)
        return yPoint < this.m_yEnd;
      return false;
    }

    public ClipType Evaluate(int xStart, int yStart, int xWidth, int yHeight)
    {
      int num1 = xStart + xWidth;
      int num2 = yStart + yHeight;
      if (num1 <= this.m_xStart || num2 <= this.m_yStart || (xStart >= this.m_xEnd || yStart >= this.m_yEnd))
        return ClipType.Outside;
      return xStart >= this.m_xStart && yStart >= this.m_yStart && (num1 <= this.m_xEnd && num2 <= this.m_yEnd) ? ClipType.Inside : ClipType.Partial;
    }

    public bool Clip(int xStart, int yStart, int xWidth, int yHeight, TransformedColoredTextured[] Vertices)
    {
      switch (this.Evaluate(xStart, yStart, xWidth, yHeight))
      {
        case ClipType.Outside:
          return false;
        case ClipType.Inside:
          float num1 = (float) xStart - 0.5f;
          float num2 = (float) yStart - 0.5f;
          float num3 = num1 + (float) xWidth;
          float num4 = num2 + (float) yHeight;
          Vertices[0].X = Vertices[1].X = num3;
          Vertices[0].Y = Vertices[2].Y = num4;
          Vertices[1].Y = Vertices[3].Y = num2;
          Vertices[2].X = Vertices[3].X = num1;
          Vertices[0].Tu = Vertices[0].Tv = Vertices[1].Tu = Vertices[2].Tv = 1f;
          Vertices[1].Tv = Vertices[2].Tu = Vertices[3].Tu = Vertices[3].Tv = 0.0f;
          return true;
        case ClipType.Partial:
          int num5 = xStart;
          int num6 = yStart;
          int num7 = xStart + xWidth;
          int num8 = yStart + yHeight;
          if (xStart < this.m_xStart)
            num5 = this.m_xStart;
          if (yStart < this.m_yStart)
            num6 = this.m_yStart;
          if (num7 > this.m_xEnd)
            num7 = this.m_xEnd;
          if (num8 > this.m_yEnd)
            num8 = this.m_yEnd;
          Vertices[0].X = Vertices[1].X = (float) num7 - 0.5f;
          Vertices[0].Y = Vertices[2].Y = (float) num8 - 0.5f;
          Vertices[1].Y = Vertices[3].Y = (float) num6 - 0.5f;
          Vertices[2].X = Vertices[3].X = (float) num5 - 0.5f;
          double num9 = 1.0 / (double) xWidth;
          double num10 = 1.0 / (double) yHeight;
          Vertices[0].Tu = Vertices[1].Tu = (float) num9 * (float) (num7 - xStart);
          Vertices[0].Tv = Vertices[2].Tv = (float) num10 * (float) (num8 - yStart);
          Vertices[1].Tv = Vertices[3].Tv = (float) num10 * (float) (num6 - yStart);
          Vertices[2].Tu = Vertices[3].Tu = (float) num9 * (float) (num5 - xStart);
          return true;
        default:
          return false;
      }
    }

    public override bool Equals(object Target)
    {
      if (Target == null || Target.GetType() != typeof (Clipper))
        return false;
      Clipper clipper = (Clipper) Target;
      if (clipper == this)
        return true;
      if (this.m_xStart == clipper.m_xStart && this.m_yStart == clipper.m_yStart && this.m_xEnd == clipper.m_xEnd)
        return this.m_yEnd == clipper.m_yEnd;
      return false;
    }

    public override int GetHashCode()
    {
      return this.m_xStart ^ this.m_yStart ^ this.m_xEnd ^ this.m_yEnd;
    }
  }
}
