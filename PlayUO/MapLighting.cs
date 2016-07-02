// Decompiled with JetBrains decompiler
// Type: PlayUO.MapLighting
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public static class MapLighting
  {
    private const int SunX = 100;
    private const int SunY = -30;
    private const int SunZ = 23;
    public static bool[] m_AlwaysStretch;

    public static int GetShadow(Point3D normal)
    {
      return (int) byte.MaxValue - Math.Min(Math.Max(0, (normal.X * 100 + normal.Y * -30 + normal.Z * 23) / 1000), 15) * 9;
    }

    public static int GetShadow(int facet, int x, int y)
    {
      return MapLighting.GetShadow(LandTile.GetVertexNormal(facet, x, y));
    }

    public static unsafe void AccumulateNormals(int* pIndices, Point3D* pVertices, Point3D* pNormals, int indexCount, int vertexCount)
    {
      int* numPtr = pIndices + indexCount;
      while (pIndices < numPtr)
      {
        Point3D point3D1 = Point3D.Cross(pVertices[*pIndices], pVertices[pIndices[1]], pVertices[pIndices[2]]);
        IntPtr num1 = (IntPtr) (pNormals + *pIndices);
        Point3D point3D2 = *(Point3D*) num1 + point3D1;
        *(Point3D*) num1 = point3D2;
        IntPtr num2 = (IntPtr) (pNormals + pIndices[1]);
        Point3D point3D3 = *(Point3D*) num2 + point3D1;
        *(Point3D*) num2 = point3D3;
        IntPtr num3 = (IntPtr) (pNormals + pIndices[2]);
        Point3D point3D4 = *(Point3D*) num3 + point3D1;
        *(Point3D*) num3 = point3D4;
        pIndices += 3;
      }
      Point3D* point3DPtr = pNormals + vertexCount;
      while (pNormals < point3DPtr)
        ++pNormals;
    }

    public static void CheckStretchTable()
    {
      if (MapLighting.m_AlwaysStretch != null)
        return;
      MapLighting.m_AlwaysStretch = new bool[16384];
      MapLighting.SetAlwaysStretch(3, 167);
      MapLighting.SetAlwaysStretch(172, 301);
      MapLighting.SetAlwaysStretch(321, 427);
      MapLighting.SetAlwaysStretch(441, 499);
      MapLighting.SetAlwaysStretch(543, 585);
      MapLighting.SetAlwaysStretch(602, 1029);
      MapLighting.SetAlwaysStretch(1094, 1145);
      MapLighting.SetAlwaysStretch(1281, 1296);
      MapLighting.SetAlwaysStretch(1351, 2539);
    }

    private static void SetAlwaysStretch(int start, int end)
    {
      for (int landId = start; landId <= end; ++landId)
      {
        if ((int) Map.GetTexture(landId) > 1)
          MapLighting.m_AlwaysStretch[landId] = true;
      }
    }
  }
}
