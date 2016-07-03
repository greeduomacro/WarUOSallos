// Decompiled with JetBrains decompiler
// Type: PlayUO.LandTile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Client;
using Ultima.Client.Terrain;
using Ultima.Data;

namespace PlayUO
{
  public sealed class LandTile : ITile, ICell, IDisposable
  {
    private static readonly Type MyType = typeof (LandTile);
    public sbyte z00;
    public sbyte z10;
    public sbyte z11;
    public sbyte z01;
    private Point3D? normal;
    public int facet;
    public int ax;
    public int ay;
    public int x;
    public int y;
    public int rev;
    public int graphicId;
    public short m_ID;
    public sbyte m_Z;
    private sbyte m_SortZ;
    public byte m_Height;
    public Texture m_sDraw;
    public bool m_Guarded;
    public bool m_FoldLeftRight;
    public IHue _lastMeshHue;
    public IMeshProvider _lastMeshProvider;
    public TerrainMesh _mesh;

    public LandId LandId
    {
      get
      {
        return (LandId) (int) (ushort) this.m_ID;
      }
      set
      {
        int num = (int) value;
        this.m_ID = num < 0 || num >= 16384 ? (short) 580 : (short) num;
        this.graphicId = GraphicTranslators.Art.Convert((int) this.m_ID);
      }
    }

    public unsafe TileFlag LandFlags
    {
      get
      {
        return LandDataPointer->Flags;
      }
    }

    public unsafe LandData* LandDataPointer
    {
      get
      {
        return Map.GetLandDataPointer(this.LandId);
      }
    }

    public bool Impassable
    {
      get
      {
        return (this.LandFlags & (TileFlag) 64L) != 0L;
      }
    }

    public bool Ignored
    {
      get
      {
        if ((int) this.m_ID == 2 || (int) this.m_ID == 475)
          return true;
        if ((int) this.m_ID >= 430)
          return (int) this.m_ID <= 437;
        return false;
      }
    }

    public Type CellType
    {
      get
      {
        return LandTile.MyType;
      }
    }

    public short ID
    {
      get
      {
        return this.m_ID;
      }
    }

    public sbyte Z
    {
      get
      {
        return this.m_Z;
      }
    }

    public sbyte SortZ
    {
      get
      {
        return this.m_SortZ;
      }
      set
      {
        this.m_SortZ = value;
      }
    }

    public byte Height
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

    void IDisposable.Dispose()
    {
    }

    private static Point3D Average4(Point3D a, Point3D b, Point3D c, Point3D d)
    {
      return new Point3D((a.X + b.X + c.X + d.X) / 4, (a.Y + b.Y + c.Y + d.Y) / 4, (a.Z + b.Z + c.Z + d.Z) / 4);
    }

    private static Point3D Average(Point3D a, Point3D b)
    {
      return new Point3D((a.X + b.X) / 2, (a.Y + b.Y) / 2, (a.Z + b.Z) / 2);
    }

    private Point3D ComputeNormal()
    {
      Point3D a1 = new Point3D(22 * (this.x - this.y), 22 * (this.x + this.y), 4 * (int) this.z00);
      Point3D point3D1 = new Point3D(a1.X + 22, a1.Y + 22, 4 * (int) this.z10);
      Point3D point3D2 = new Point3D(a1.X, a1.Y + 44, 4 * (int) this.z11);
      Point3D c = new Point3D(a1.X - 22, a1.Y + 22, 4 * (int) this.z01);
      Point3D a2;
      Point3D b;
      if (this.m_FoldLeftRight)
      {
        a2 = Point3D.Cross(a1, point3D1, point3D2);
        b = Point3D.Cross(a1, point3D2, c);
      }
      else
      {
        a2 = Point3D.Cross(a1, point3D1, c);
        b = Point3D.Cross(point3D1, point3D2, c);
      }
      return LandTile.Average(a2, b);
    }

    public Point3D GetNormal()
    {
      if (!this.normal.HasValue)
        this.normal = new Point3D?(this.ComputeNormal());
      return this.normal.Value;
    }

    public static Point3D GetVertexNormal(int facet, int x, int y)
    {
      Viewport viewport = World.Viewport;
      return Point3D.Normalize256(LandTile.Average4(viewport.GetLandTile(x - 1, y - 1, facet).GetNormal(), viewport.GetLandTile(x, y - 1, facet).GetNormal(), viewport.GetLandTile(x, y, facet).GetNormal(), viewport.GetLandTile(x - 1, y, facet).GetNormal()));
    }

    public Point3D GetNormal(int xo, int yo)
    {
      return LandTile.GetVertexNormal(this.facet, this.ax + xo, this.ay + yo);
    }

    public void Prepare(int facet, int x, int y)
    {
      if (this.facet == facet && this.ax == x && this.ay == y)
        return;
      TileMatrix matrix = Map.GetMatrix(facet);
      Tile landTile1 = matrix.GetLandTile(x, y);
      Tile landTile2 = matrix.GetLandTile(x, y + 1);
      Tile landTile3 = matrix.GetLandTile(x + 1, y);
      Tile landTile4 = matrix.GetLandTile(x + 1, y + 1);
      this.LandId = landTile1.landId;
      this.facet = facet;
      this.ax = x;
      this.ay = y;
      this.z00 = landTile1.z;
      this.z01 = landTile2.z;
      this.z11 = landTile4.z;
      this.z10 = landTile3.z;
      this.m_Z = this.m_SortZ = this.z00;
      this.m_FoldLeftRight = Math.Abs((int) this.z00 - (int) this.z11) <= Math.Abs((int) this.z01 - (int) this.z10);
      this.m_SortZ = !this.m_FoldLeftRight ? (sbyte) Map.FloorAverage((int) this.z01, (int) this.z10) : (sbyte) Map.FloorAverage((int) this.z00, (int) this.z11);
      this.m_Guarded = Region.Find(Region.GuardedRegions, x, y, (int) this.z00, facet) != null;
      this.m_Height = (byte) 0;
      this.m_sDraw = (Texture) null;
      this.normal = new Point3D?();
      this._mesh = (TerrainMesh) null;
      this.rev = -1;
    }

    public void EnsureMesh(IHue hue, TerrainMeshProvider meshProvider)
    {
      if (this._mesh != null && this._lastMeshHue == hue && this._lastMeshProvider == meshProvider)
        return;
      this._lastMeshHue = hue;
      this._lastMeshProvider = (IMeshProvider) meshProvider;
      int landId = this.graphicId;
      int textureId = (int) Map.GetTexture(landId);
      Texture tex;
      if (textureId > 0 && textureId < 16384)
      {
        tex = hue.GetTerrainTexture(textureId);
        if (tex == null || tex.IsEmpty())
        {
          tex = hue.GetTerrainIsometric(landId);
          if (tex == null || tex.IsEmpty())
          {
            tex = hue.GetTerrainTexture(1);
            if (tex == null || tex.IsEmpty())
              return;
          }
        }
      }
      else
      {
        tex = hue.GetTerrainIsometric(landId);
        if (tex == null || tex.IsEmpty())
        {
          if (textureId > 0 && textureId < 16384)
            tex = hue.GetTerrainTexture(textureId);
          if (tex == null || tex.IsEmpty())
          {
            tex = hue.GetTerrainTexture(1);
            if (tex == null || tex.IsEmpty())
            {
              this._mesh = (TerrainMesh) null;
              return;
            }
          }
        }
      }
      this.m_sDraw = tex;
      if (tex.Width == 44 && !(this._mesh is TerrainMesh2D))
      {
        this._mesh = (TerrainMesh) new TerrainMesh2D();
        this._mesh.Load(this, tex);
      }
      else
      {
        if (tex.Width == 44 || this._mesh is TerrainMesh3D && ((TerrainMesh3D) this._mesh).Provider == meshProvider)
          return;
        this._mesh = (TerrainMesh) new TerrainMesh3D(meshProvider);
        this._mesh.Load(this, tex);
      }
    }
  }
}
