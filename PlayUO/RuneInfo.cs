// Decompiled with JetBrains decompiler
// Type: PlayUO.RuneInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;

namespace PlayUO
{
  public class RuneInfo : PersistableObject, IEquatable<RuneInfo>
  {
    public static readonly PersistableType TypeCode = new PersistableType("rune", new ConstructCallback((object) null, __methodptr(Construct)));
    private string m_Name;
    private Point3D m_Point;
    private int m_Facet;

    public virtual PersistableType TypeID
    {
      get
      {
        return RuneInfo.TypeCode;
      }
    }

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public Point3D Point
    {
      get
      {
        return this.m_Point;
      }
    }

    public int Facet
    {
      get
      {
        return this.m_Facet;
      }
    }

    private RuneInfo()
    {
      base.\u002Ector();
    }

    public RuneInfo(string name, Point3D p, int f)
    {
      base.\u002Ector();
      this.m_Name = name;
      this.m_Point = p;
      this.m_Facet = f;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new RuneInfo();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetString("name", this.m_Name);
      op.SetInt32("point-x", this.m_Point.X);
      op.SetInt32("point-y", this.m_Point.Y);
      if (this.m_Point.Z != 0)
        op.SetInt32("point-z", this.m_Point.Z);
      if (this.m_Facet == 0)
        return;
      op.SetInt32("point-f", this.m_Facet);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Name = ip.GetString("name");
      this.m_Point = new Point3D(ip.GetInt32("point-x"), ip.GetInt32("point-y"), ip.GetInt32("point-z"));
      this.m_Facet = ip.GetInt32("point-f");
    }

    public bool Equals(RuneInfo other)
    {
      if (other != null && this.m_Name == other.m_Name && this.m_Point == other.m_Point)
        return this.m_Facet == other.m_Facet;
      return false;
    }

    public virtual int GetHashCode()
    {
      return this.m_Name.GetHashCode() ^ this.m_Point.GetHashCode() ^ this.m_Facet.GetHashCode();
    }

    public virtual bool Equals(object obj)
    {
      return this.Equals(obj as RuneInfo);
    }
  }
}
