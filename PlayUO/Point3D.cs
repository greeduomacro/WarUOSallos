// Decompiled with JetBrains decompiler
// Type: PlayUO.Point3D
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public struct Point3D : IEquatable<Point3D>
  {
    public int X;
    public int Y;
    public int Z;

    public Point3D(int x, int y, int z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public Point3D(IPoint3D p)
    {
      this.X = p.X;
      this.Y = p.Y;
      this.Z = p.Z;
    }

    public static bool operator ==(Point3D l, Point3D r)
    {
      if (l.X == r.X && l.Y == r.Y)
        return l.Z == r.Z;
      return false;
    }

    public static bool operator !=(Point3D l, Point3D r)
    {
      if (l.X == r.X && l.Y == r.Y)
        return l.Z != r.Z;
      return true;
    }

    public static bool operator ==(Point3D l, IPoint3D r)
    {
      if (l.X == r.X && l.Y == r.Y)
        return l.Z == r.Z;
      return false;
    }

    public static bool operator !=(Point3D l, IPoint3D r)
    {
      if (l.X == r.X && l.Y == r.Y)
        return l.Z != r.Z;
      return true;
    }

    public static Point3D operator +(Point3D lhs, Point3D rhs)
    {
      return new Point3D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
    }

    public override bool Equals(object o)
    {
      if (o == null || !(o is IPoint3D))
        return false;
      IPoint3D point3D = (IPoint3D) o;
      if (this.X == point3D.X && this.Y == point3D.Y)
        return this.Z == point3D.Z;
      return false;
    }

    public override int GetHashCode()
    {
      return this.X ^ this.Y ^ this.Z;
    }

    public static Point3D Parse(string value)
    {
      int num1 = value.IndexOf('(');
      int num2 = value.IndexOf(',', num1 + 1);
      string str1 = value.Substring(num1 + 1, num2 - (num1 + 1)).Trim();
      int num3 = num2;
      int num4 = value.IndexOf(',', num3 + 1);
      string str2 = value.Substring(num3 + 1, num4 - (num3 + 1)).Trim();
      int num5 = num4;
      int num6 = value.IndexOf(')', num5 + 1);
      string str3 = value.Substring(num5 + 1, num6 - (num5 + 1)).Trim();
      return new Point3D(Convert.ToInt32(str1), Convert.ToInt32(str2), Convert.ToInt32(str3));
    }

    public bool Equals(Point3D other)
    {
      if (this.X == other.X && this.Y == other.Y)
        return this.Z == other.Z;
      return false;
    }

    public static Point3D Cross(Point3D a, Point3D b, Point3D c)
    {
      return new Point3D((c.Z - a.Z) * (b.Y - a.Y) - (c.Y - a.Y) * (b.Z - a.Z), (b.Z - a.Z) * (c.X - a.X) - (c.Z - a.Z) * (b.X - a.X), (c.Y - a.Y) * (b.X - a.X) - (b.Y - a.Y) * (c.X - a.X));
    }

    public static Point3D Normalize256(Point3D a)
    {
      double num = 256.0 / Math.Sqrt((double) (a.X * a.X + a.Y * a.Y + a.Z * a.Z));
      return new Point3D((int) ((double) a.X * num), (int) ((double) a.Y * num), (int) ((double) a.Z * num));
    }
  }
}
