// Decompiled with JetBrains decompiler
// Type: PlayUO.Point
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class Point : IPoint2D
  {
    private int m_X;
    private int m_Y;

    public int X
    {
      get
      {
        return this.m_X;
      }
      set
      {
        this.m_X = value;
      }
    }

    public int Y
    {
      get
      {
        return this.m_Y;
      }
      set
      {
        this.m_Y = value;
      }
    }

    public Point(int X, int Y)
    {
      this.m_X = X;
      this.m_Y = Y;
    }

    public Point(Point p, int xOffset, int yOffset)
    {
      this.m_X = p.m_X + xOffset;
      this.m_Y = p.m_Y + yOffset;
    }

    public static implicit operator System.Drawing.Point(Point p)
    {
      return new System.Drawing.Point(p.m_X, p.m_Y);
    }

    public static implicit operator Point(System.Drawing.Point p)
    {
      return new Point(p.X, p.Y);
    }

    public static bool operator ==(Point p1, Point p2)
    {
      if (p1.m_X == p2.m_X)
        return p1.m_Y == p2.m_Y;
      return false;
    }

    public static bool operator !=(Point p1, Point p2)
    {
      if (p1.m_X == p2.m_X)
        return p1.m_Y != p2.m_Y;
      return true;
    }

    public static int operator ^(Point p1, Point p2)
    {
      int num1 = Math.Abs(p2.X - p1.X);
      int num2 = Math.Abs(p2.Y - p1.Y);
      return (int) Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }

    public static Point operator -(Point l, Point r)
    {
      return new Point(l.X - r.X, l.Y - r.Y);
    }

    public static Point operator +(Point l, Point r)
    {
      return new Point(l.X + r.X, l.Y + r.Y);
    }

    public static Point operator /(Point l, int r)
    {
      return new Point(l.X / r, l.Y / r);
    }

    public override bool Equals(object o)
    {
      if (o == null || o.GetType() != typeof (Point))
        return false;
      Point point = (Point) o;
      if (this.m_X == point.m_X)
        return this.m_Y == point.m_Y;
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
