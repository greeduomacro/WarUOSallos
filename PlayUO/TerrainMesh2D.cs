// Decompiled with JetBrains decompiler
// Type: PlayUO.TerrainMesh2D
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class TerrainMesh2D : TerrainMesh
  {
    protected override unsafe TransformedColoredTextured[] GetMesh(LandTile tile, Texture tex)
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = VertexConstructor.Create(4);
      fixed (TransformedColoredTextured* transformedColoredTexturedPtr = transformedColoredTexturedArray)
      {
        float num1 = -0.5f;
        float num2 = (float) ((int) tile.Z * -4) - 0.5f;
        transformedColoredTexturedPtr->X = transformedColoredTexturedPtr[1].X = num1 + (float) tex.Width;
        transformedColoredTexturedPtr->Y = transformedColoredTexturedPtr[2].Y = num2 + (float) tex.Height;
        transformedColoredTexturedPtr[1].Y = transformedColoredTexturedPtr[3].Y = num2;
        transformedColoredTexturedPtr[2].X = transformedColoredTexturedPtr[3].X = num1;
        float maxTu = tex.MaxTU;
        float maxTv = tex.MaxTV;
        transformedColoredTexturedPtr->Tv = maxTv;
        transformedColoredTexturedPtr->Tu = maxTu;
        transformedColoredTexturedPtr[1].Tu = maxTu;
        transformedColoredTexturedPtr[2].Tv = maxTv;
      }
      return transformedColoredTexturedArray;
    }

    protected override unsafe void RenderAux(TransformedColoredTextured* pMesh)
    {
      Renderer.DrawQuadPrecalc(pMesh);
    }

    protected override unsafe void UpdateMesh(LandTile tile, int baseColor)
    {
      fixed (TransformedColoredTextured* transformedColoredTexturedPtr = this._mesh)
      {
        int quadColor = Renderer.GetQuadColor(baseColor);
        transformedColoredTexturedPtr->Color = quadColor;
        transformedColoredTexturedPtr[1].Color = quadColor;
        transformedColoredTexturedPtr[2].Color = quadColor;
        transformedColoredTexturedPtr[3].Color = quadColor;
      }
    }
  }
}
