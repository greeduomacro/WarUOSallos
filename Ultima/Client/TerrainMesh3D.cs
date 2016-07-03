// Decompiled with JetBrains decompiler
// Type: Ultima.Client.TerrainMesh3D
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO;
using System;

namespace Ultima.Client
{
  public sealed class TerrainMesh3D : TerrainMesh
  {
    private TerrainMeshProvider _meshProvider;
    private bool _foldLeftRight;
    private Point3D[] _normals;

    public TerrainMeshProvider Provider
    {
      get
      {
        return this._meshProvider;
      }
    }

    public TerrainMesh3D(TerrainMeshProvider meshProvider)
    {
      this._meshProvider = meshProvider;
    }

    private static unsafe void UpdateSimpleMesh(TransformedColoredTextured* pMesh, int s00, int s10, int s11, int s01)
    {
      pMesh->Color = Renderer.GetQuadColor(65793 * s00);
      pMesh[1].Color = Renderer.GetQuadColor(65793 * s10);
      pMesh[2].Color = Renderer.GetQuadColor(65793 * s01);
      pMesh[3].Color = Renderer.GetQuadColor(65793 * s11);
    }

    private void AddNormals(Vector[] normals, int dx, int dy)
    {
    }

    private unsafe void UpdateMeshComplex(int s00, int s10, int s11, int s01)
    {
      int stride = this._meshProvider.Stride;
      int divisions = this._meshProvider.Divisions;
      int[] indices;
      if ((indices = this._meshProvider.GetIndices(this._foldLeftRight)) != null && indices.Length != 0)
      {
        fixed (int* numPtr = &indices[0])
          ;
      }
      fixed (TransformedColoredTextured* transformedColoredTexturedPtr1 = this._mesh)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1;
        int num1 = 0;
        int num2 = divisions;
        while (num1 <= divisions)
        {
          int num3 = (s00 * num2 + s01 * num1) / divisions;
          int num4 = (s10 * num2 + s11 * num1) / divisions;
          int num5 = 0;
          int num6 = divisions;
          while (num5 <= divisions)
          {
            int num7 = (num3 * num6 + num4 * num5) / divisions;
            Point3D a = this._normals[num1 * stride + num5];
            int num8 = num5 == 0 ? -1 : (num5 == divisions ? 1 : 0);
            int num9 = num1 == 0 ? -1 : (num1 == divisions ? 1 : 0);
            int num10 = 65793;
            if (num8 != 0)
            {
              LandTile landTile = World.Viewport.GetLandTile(this.tile.ax + num8, this.tile.ay, Engine.m_World);
              if ((!this.tile.m_Guarded ? 0 : (!this.tile.Impassable ? 1 : 0)) != (!landTile.m_Guarded ? 0 : (!landTile.Impassable ? 1 : 0)))
                num10 = !this.tile.m_Guarded || this.tile.Impassable ? 65536 : 256;
              landTile.EnsureMesh(landTile._lastMeshHue ?? this.tile._lastMeshHue ?? Hues.Default, this._meshProvider);
              TerrainMesh3D terrainMesh3D = landTile._mesh as TerrainMesh3D;
              if (terrainMesh3D != null)
              {
                terrainMesh3D.Update(terrainMesh3D.tile, terrainMesh3D._color);
                a += terrainMesh3D._normals[num1 * stride + num6];
              }
            }
            if (num9 != 0)
            {
              LandTile landTile = World.Viewport.GetLandTile(this.tile.ax, this.tile.ay + num9, Engine.m_World);
              if ((!this.tile.m_Guarded ? 0 : (!this.tile.Impassable ? 1 : 0)) != (!landTile.m_Guarded ? 0 : (!landTile.Impassable ? 1 : 0)))
                num10 = !this.tile.m_Guarded || this.tile.Impassable ? 65536 : 256;
              landTile.EnsureMesh(landTile._lastMeshHue ?? this.tile._lastMeshHue ?? Hues.Default, this._meshProvider);
              TerrainMesh3D terrainMesh3D = landTile._mesh as TerrainMesh3D;
              if (terrainMesh3D != null)
              {
                terrainMesh3D.Update(terrainMesh3D.tile, terrainMesh3D._color);
                a += terrainMesh3D._normals[num2 * stride + num5];
              }
            }
            if (num8 != 0 && num9 != 0)
            {
              LandTile landTile = World.Viewport.GetLandTile(this.tile.ax + num8, this.tile.ay + num9, Engine.m_World);
              if ((!this.tile.m_Guarded ? 0 : (!this.tile.Impassable ? 1 : 0)) != (!landTile.m_Guarded ? 0 : (!landTile.Impassable ? 1 : 0)))
                num10 = !this.tile.m_Guarded || this.tile.Impassable ? 65536 : 256;
              landTile.EnsureMesh(landTile._lastMeshHue ?? this.tile._lastMeshHue ?? Hues.Default, this._meshProvider);
              TerrainMesh3D terrainMesh3D = landTile._mesh as TerrainMesh3D;
              if (terrainMesh3D != null)
              {
                terrainMesh3D.Update(terrainMesh3D.tile, terrainMesh3D._color);
                a += terrainMesh3D._normals[num2 * stride + num6];
              }
            }
            Point3D normal = Point3D.Normalize256(a);
            int num11 = (num7 * 2 + MapLighting.GetShadow(normal) + 2) / 3;
            transformedColoredTexturedPtr2++->Color = Renderer.GetQuadColor(num10 * num11);
            ++num5;
            --num6;
          }
          ++num1;
          --num2;
        }
        // ISSUE: fixed variable is out of scope
        // ISSUE: __unpin statement
      }
    }

    protected override unsafe void UpdateMesh(LandTile tile, int baseColor)
    {
      int shadow1 = MapLighting.GetShadow(tile.facet, tile.ax, tile.ay);
      int shadow2 = MapLighting.GetShadow(tile.facet, tile.ax + 1, tile.ay);
      int shadow3 = MapLighting.GetShadow(tile.facet, tile.ax + 1, tile.ay + 1);
      int shadow4 = MapLighting.GetShadow(tile.facet, tile.ax, tile.ay + 1);
      fixed (TransformedColoredTextured* pMesh = this._mesh)
      {
        TransformedColoredTextured[] transformedColoredTexturedArray = this._mesh;
        int divisions = this._meshProvider.Divisions;
        int stride = this._meshProvider.Stride;
        if (divisions == 1)
          TerrainMesh3D.UpdateSimpleMesh(pMesh, shadow1, shadow2, shadow3, shadow4);
        else
          this.UpdateMeshComplex(shadow1, shadow2, shadow3, shadow4);
      }
    }

    private TransformedColoredTextured[] AllocateMesh(int size)
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = this._mesh;
      if (transformedColoredTexturedArray == null || transformedColoredTexturedArray.Length != size)
        this._mesh = transformedColoredTexturedArray = new TransformedColoredTextured[size];
      return transformedColoredTexturedArray;
    }

    private Point3D[] AllocateNormals(int size)
    {
      Point3D[] point3DArray = this._normals;
      if (point3DArray == null || point3DArray.Length != size)
        this._normals = point3DArray = new Point3D[size];
      return point3DArray;
    }

    private int[] GetIndices(out int indexCount)
    {
      int[] indices = this._meshProvider.GetIndices(this._foldLeftRight);
      indexCount = indices.Length;
      return indices;
    }

    private unsafe void ComputeMeshCore(float* pHeightmap, TransformedColoredTextured* pMesh, int divisions)
    {
      float num1 = 22f / (float) divisions;
      for (int index1 = 0; index1 <= divisions; ++index1)
      {
        float num2 = (float) index1 / (float) divisions;
        for (int index2 = 0; index2 <= divisions; ++index2)
        {
          float num3 = (float) index2 / (float) divisions;
          pMesh->Rhw = 1f;
          pMesh->Tu = num3;
          pMesh->Tv = num2;
          pMesh->X = (float) (21.5 + (double) (index2 - index1) * (double) num1);
          pMesh->Y = (float) ((double) (index2 + index1) * (double) num1 - 0.5 - (double) *pHeightmap * 4.0);
          ++pMesh;
          ++pHeightmap;
        }
      }
    }

    private unsafe void ComputeMesh(float* pHeightmap)
    {
      int size = this._meshProvider.Size;
      int divisions = this._meshProvider.Divisions;
      fixed (TransformedColoredTextured* pMesh = this.AllocateMesh(size))
        this.ComputeMeshCore(pHeightmap, pMesh, divisions);
    }

    private unsafe void ComputeNormals(float* pHeightmap)
    {
      int size = this._meshProvider.Size;
      int divisions = this._meshProvider.Divisions;
      Point3D* pVertices = stackalloc Point3D[size];
      this.ComputeVerticesCore(pHeightmap, pVertices, divisions);
      fixed (Point3D* pNormals = this.AllocateNormals(size))
      {
        int indexCount;
        fixed (int* pIndices = this.GetIndices(out indexCount))
          MapLighting.AccumulateNormals(pIndices, pVertices, pNormals, indexCount, size);
      }
    }

    private unsafe void ComputeVerticesCore(float* pHeightmap, Point3D* pVertices, int divisions)
    {
      for (int index1 = 0; index1 <= divisions; ++index1)
      {
        for (int index2 = 0; index2 <= divisions; ++index2)
        {
          pVertices->X = 22 * (index2 - index1);
          pVertices->Y = 22 * (index2 + index1);
          pVertices->Z = (int) ((double) (4 * divisions) * (double) *pHeightmap);
          ++pVertices;
          ++pHeightmap;
        }
      }
    }

    protected override unsafe TransformedColoredTextured[] GetMesh(LandTile tile, Texture tex)
    {
      int* pInput = stackalloc int[16];
      float* numPtr1 = stackalloc float[this._meshProvider.Size];
      // ISSUE: untyped stack allocation
      IntPtr num = new IntPtr(checked (unchecked ((uint) _meshProvider.Size) * sizeof (Point3D)));
      TileMatrix matrix = Map.GetMatrix(Engine.m_World);
      int* numPtr2 = pInput;
      for (int index1 = -1; index1 < 3; ++index1)
      {
        for (int index2 = -1; index2 < 3; ++index2)
          *numPtr2++ = (int) matrix.GetLandTile(tile.ax + index2, tile.ay + index1).z;
      }
      this._meshProvider.Sample(pInput, numPtr1);
      this._foldLeftRight = tile.m_FoldLeftRight;
      this.ComputeMesh(numPtr1);
      this.ComputeNormals(numPtr1);
      return this._mesh;
    }

    protected override unsafe void RenderAux(TransformedColoredTextured* pMesh)
    {
      Renderer.FilterEnable = true;
      Renderer.PushVertices(pMesh, this._mesh.Length, this._foldLeftRight ? 2 : 3);
      Renderer.FilterEnable = false;
    }
  }
}
