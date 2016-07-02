// Decompiled with JetBrains decompiler
// Type: PlayUO.Vector
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public struct Vector
  {
    public float m_X;
    public float m_Y;
    public float m_Z;

    public Vector(float X, float Y, float Z)
    {
      this.m_X = X;
      this.m_Y = Y;
      this.m_Z = Z;
    }

    public static bool operator ==(Vector v1, Vector v2)
    {
      if ((double) v1.m_X == (double) v2.m_X && (double) v1.m_Y == (double) v2.m_Y)
        return (double) v1.m_Z == (double) v2.m_Z;
      return false;
    }

    public static bool operator !=(Vector v1, Vector v2)
    {
      if ((double) v1.m_X == (double) v2.m_X && (double) v1.m_Y == (double) v2.m_Y)
        return (double) v1.m_Z != (double) v2.m_Z;
      return true;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
      return new Vector(v1.m_X + v2.m_X, v1.m_Y + v2.m_Y, v1.m_Z + v2.m_Z);
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
      return new Vector(v1.m_X - v2.m_X, v1.m_Y - v2.m_Y, v1.m_Z - v2.m_Z);
    }

    public static Vector operator *(Vector v1, Vector v2)
    {
      return new Vector(v1.m_X * v2.m_X, v1.m_Y * v2.m_Y, v1.m_Z * v2.m_Z);
    }

    public static Vector operator *(Vector v1, float f)
    {
      return new Vector(v1.m_X * f, v1.m_Y * f, v1.m_Z * f);
    }

    public static Vector operator /(Vector v1, Vector v2)
    {
      return new Vector(v1.m_X / v2.m_X, v1.m_Y / v2.m_Y, v1.m_Z / v2.m_Z);
    }

    public static Vector operator /(Vector v1, float f)
    {
      return new Vector(v1.m_X / f, v1.m_Y / f, v1.m_Z / f);
    }

    public override bool Equals(object o)
    {
      return this == (Vector) o;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("( {0:F2}, {1:F2}, {2:F2} )", (object) this.m_X, (object) this.m_Y, (object) this.m_Z);
    }

    public float Dot(Vector v)
    {
      return (float) ((double) this.m_X * (double) v.m_X + (double) this.m_Y * (double) v.m_Y + (double) this.m_Z * (double) v.m_Z);
    }

    public Vector Cross(Vector v)
    {
      return new Vector((float) ((double) this.m_Y * (double) v.m_Z - (double) this.m_Z * (double) v.m_Y), (float) ((double) this.m_Z * (double) v.m_X - (double) this.m_X * (double) v.m_Z), (float) ((double) this.m_X * (double) v.m_Y - (double) this.m_Y * (double) v.m_X));
    }

    public Vector Normalize()
    {
      float num = (float) Math.Sqrt((double) this.m_X * (double) this.m_X + (double) this.m_Y * (double) this.m_Y + (double) this.m_Z * (double) this.m_Z);
      return new Vector(this.m_X / num, this.m_Y / num, this.m_Z / num);
    }
  }
}
