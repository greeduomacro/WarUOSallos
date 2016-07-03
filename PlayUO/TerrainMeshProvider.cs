// Decompiled with JetBrains decompiler
// Type: PlayUO.TerrainMeshProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Client.Terrain;

namespace PlayUO
{
  public sealed class TerrainMeshProvider : IMeshProvider
  {
    private readonly IMeshProvider source;
    private readonly int[] leftRightIndices;
    private readonly int[] topBottomIndices;

    public int Divisions
    {
      get
      {
        return this.source.Divisions;
      }
    }

    public int Size
    {
      get
      {
        return this.source.Size;
      }
    }

    public int Stride
    {
      get
      {
        return this.source.Stride;
      }
    }

    public TerrainMeshProvider(IMeshProvider source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      this.source = source;
      this.leftRightIndices = this.CreateIndices(true);
      this.topBottomIndices = this.CreateIndices(false);
    }

    public unsafe void Sample(int* pInput, float* pOutput)
    {
      this.source.Sample(pInput, pOutput);
    }

    public int[] GetIndices(bool leftRight)
    {
      if (!leftRight)
        return this.topBottomIndices;
      return this.leftRightIndices;
    }

    private int[] CreateIndices(bool leftRight)
    {
      int divisions = this.Divisions;
      int stride = this.Stride;
      int[] numArray1 = new int[divisions * divisions * 6];
      int num1 = 0;
      for (int index1 = 0; index1 < divisions; ++index1)
      {
        for (int index2 = 0; index2 < divisions; ++index2)
        {
          int num2 = index1 * stride + index2;
          if (divisions > 1)
            leftRight = (index2 + index1 & 1) == 0;
          if (leftRight)
          {
            int[] numArray2 = numArray1;
            int index3 = num1;
            int num3 = 1;
            int num4 = index3 + num3;
            int num5 = num2 + stride;
            numArray2[index3] = num5;
            int[] numArray3 = numArray1;
            int index4 = num4;
            int num6 = 1;
            int num7 = index4 + num6;
            int num8 = num2;
            numArray3[index4] = num8;
            int[] numArray4 = numArray1;
            int index5 = num7;
            int num9 = 1;
            int num10 = index5 + num9;
            int num11 = num2 + stride + 1;
            numArray4[index5] = num11;
            int[] numArray5 = numArray1;
            int index6 = num10;
            int num12 = 1;
            int num13 = index6 + num12;
            int num14 = num2;
            numArray5[index6] = num14;
            int[] numArray6 = numArray1;
            int index7 = num13;
            int num15 = 1;
            int num16 = index7 + num15;
            int num17 = num2 + 1;
            numArray6[index7] = num17;
            int[] numArray7 = numArray1;
            int index8 = num16;
            int num18 = 1;
            num1 = index8 + num18;
            int num19 = num2 + stride + 1;
            numArray7[index8] = num19;
          }
          else
          {
            int[] numArray2 = numArray1;
            int index3 = num1;
            int num3 = 1;
            int num4 = index3 + num3;
            int num5 = num2 + stride;
            numArray2[index3] = num5;
            int[] numArray3 = numArray1;
            int index4 = num4;
            int num6 = 1;
            int num7 = index4 + num6;
            int num8 = num2;
            numArray3[index4] = num8;
            int[] numArray4 = numArray1;
            int index5 = num7;
            int num9 = 1;
            int num10 = index5 + num9;
            int num11 = num2 + 1;
            numArray4[index5] = num11;
            int[] numArray5 = numArray1;
            int index6 = num10;
            int num12 = 1;
            int num13 = index6 + num12;
            int num14 = num2 + stride;
            numArray5[index6] = num14;
            int[] numArray6 = numArray1;
            int index7 = num13;
            int num15 = 1;
            int num16 = index7 + num15;
            int num17 = num2 + 1;
            numArray6[index7] = num17;
            int[] numArray7 = numArray1;
            int index8 = num16;
            int num18 = 1;
            num1 = index8 + num18;
            int num19 = num2 + stride + 1;
            numArray7[index8] = num19;
          }
        }
      }
      return numArray1;
    }
  }
}
