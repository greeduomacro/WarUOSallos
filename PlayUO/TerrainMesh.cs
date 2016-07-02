// Decompiled with JetBrains decompiler
// Type: PlayUO.TerrainMesh
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public abstract class TerrainMesh
  {
    protected LandTile tile;
    protected TransformedColoredTextured[] _mesh;
    protected int _x;
    protected int _y;
    protected int _color;
    protected int _xMin;
    protected int _xMax;
    protected int _yMin;
    protected int _yMax;

    public void Load(LandTile tile, Texture tex)
    {
      this.tile = tile;
      this._mesh = this.GetMesh(tile, tex);
      if (this._mesh.Length > 0)
      {
        this._yMax = int.MinValue;
        this._yMin = int.MaxValue;
        this._xMax = int.MinValue;
        this._xMin = int.MaxValue;
        for (int index = 0; index < this._mesh.Length; ++index)
        {
          int num1 = (int) this._mesh[index].X;
          int num2 = (int) this._mesh[index].Y;
          if (num2 < this._yMin)
            this._yMin = num2;
          if (num2 > this._yMax)
            this._yMax = num2;
          if (num1 < this._xMin)
            this._xMin = num1;
          if (num1 > this._xMax)
            this._xMax = num1;
        }
      }
      this._color = 0;
    }

    protected abstract TransformedColoredTextured[] GetMesh(LandTile tile, Texture tex);

    protected abstract void UpdateMesh(LandTile tile, int baseColor);

    public void Update(LandTile tile, int baseColor)
    {
      if (this._color == baseColor)
        return;
      this._color = baseColor;
      this.UpdateMesh(tile, baseColor);
    }

    public unsafe void Render(int x, int y)
    {
      if (y + this._yMax < Engine.GameY || y + this._yMin > Engine.GameY + Engine.GameHeight || (x + this._xMax < Engine.GameX || x + this._xMin > Engine.GameX + Engine.GameWidth))
        return;
      int length = this._mesh.Length;
      fixed (TransformedColoredTextured* pMesh = this._mesh)
      {
        if (this._x != x || this._y != y)
        {
          int num1 = x - this._x;
          int num2 = y - this._y;
          for (int index = 0; index < length; ++index)
          {
            IntPtr num3 = (IntPtr) (pMesh + index);
            double num4 = (double) ((TransformedColoredTextured*) num3)->X + (double) num1;
            ((TransformedColoredTextured*) num3)->X = (float) num4;
            IntPtr num5 = (IntPtr) (pMesh + index);
            double num6 = (double) ((TransformedColoredTextured*) num5)->Y + (double) num2;
            ((TransformedColoredTextured*) num5)->Y = (float) num6;
          }
          this._x = x;
          this._y = y;
        }
        this.RenderAux(pMesh);
      }
    }

    protected abstract unsafe void RenderAux(TransformedColoredTextured* pMesh);
  }
}
