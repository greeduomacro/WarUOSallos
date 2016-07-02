// Decompiled with JetBrains decompiler
// Type: PlayUO.Point3DList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Point3DList
  {
    private static Point3D[] m_EmptyList = new Point3D[0];
    private Point3D[] m_List;
    private int m_Count;

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public Point3D Last
    {
      get
      {
        return this.m_List[this.m_Count - 1];
      }
    }

    public Point3D this[int index]
    {
      get
      {
        return this.m_List[index];
      }
    }

    public Point3DList()
    {
      this.m_List = new Point3D[8];
      this.m_Count = 0;
    }

    public void Clear()
    {
      this.m_Count = 0;
    }

    public void Add(int x, int y, int z)
    {
      if (this.m_Count + 1 > this.m_List.Length)
      {
        Point3D[] point3DArray = this.m_List;
        this.m_List = new Point3D[point3DArray.Length * 2];
        for (int index = 0; index < point3DArray.Length; ++index)
          this.m_List[index] = point3DArray[index];
      }
      this.m_List[this.m_Count].X = x;
      this.m_List[this.m_Count].Y = y;
      this.m_List[this.m_Count].Z = z;
      ++this.m_Count;
    }

    public void Add(Point3D p)
    {
      if (this.m_Count + 1 > this.m_List.Length)
      {
        Point3D[] point3DArray = this.m_List;
        this.m_List = new Point3D[point3DArray.Length * 2];
        for (int index = 0; index < point3DArray.Length; ++index)
          this.m_List[index] = point3DArray[index];
      }
      this.m_List[this.m_Count].X = p.X;
      this.m_List[this.m_Count].Y = p.Y;
      this.m_List[this.m_Count].Z = p.Z;
      ++this.m_Count;
    }

    public Point3D[] ToArray()
    {
      if (this.m_Count == 0)
        return Point3DList.m_EmptyList;
      Point3D[] point3DArray = new Point3D[this.m_Count];
      for (int index = 0; index < this.m_Count; ++index)
        point3DArray[index] = this.m_List[index];
      this.m_Count = 0;
      return point3DArray;
    }
  }
}
