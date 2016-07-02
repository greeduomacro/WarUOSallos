// Decompiled with JetBrains decompiler
// Type: PlayUO.LandArt
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Data;

namespace PlayUO
{
  public class LandArt
  {
    private LandArt.LandFactory m_Factory;

    public void Dispose()
    {
    }

    public Texture ReadFromDisk(int LandID, IHue Hue)
    {
      if (this.m_Factory == null)
        this.m_Factory = new LandArt.LandFactory(this);
      return this.m_Factory.Load(LandID, Hue);
    }

    private class LandFactory : TextureFactory
    {
      private int m_LandID;
      private IHue m_Hue;
      private int[] m_Offset;
      private int[] m_Length;
      private LandArt m_Land;
      private byte[] data;

      public override TextureTransparency Transparency
      {
        get
        {
          return TextureTransparency.Simple;
        }
      }

      public LandFactory(LandArt land)
      {
        this.m_Land = land;
        this.m_Length = new int[44]
        {
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8,
          9,
          10,
          11,
          12,
          13,
          14,
          15,
          16,
          17,
          18,
          19,
          20,
          21,
          22,
          22,
          21,
          20,
          19,
          18,
          17,
          16,
          15,
          14,
          13,
          12,
          11,
          10,
          9,
          8,
          7,
          6,
          5,
          4,
          3,
          2,
          1
        };
        this.m_Offset = new int[44]
        {
          21,
          20,
          19,
          18,
          17,
          16,
          15,
          14,
          13,
          12,
          11,
          10,
          9,
          8,
          7,
          6,
          5,
          4,
          3,
          2,
          1,
          0,
          0,
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8,
          9,
          10,
          11,
          12,
          13,
          14,
          15,
          16,
          17,
          18,
          19,
          20,
          21
        };
      }

      private string GetFilePath(int tileId)
      {
        return string.Format("build/artlegacymul/{0:00000000}.tga", (object) tileId);
      }

      public Texture Load(int landID, IHue hue)
      {
        this.m_LandID = landID & 16383;
        this.m_Hue = hue;
        return this.Construct(false);
      }

      public override Texture Reconstruct(object[] args)
      {
        this.m_LandID = (int) args[0];
        this.m_Hue = (IHue) args[1];
        return this.Construct(true);
      }

      protected override void CoreAssignArgs(Texture tex)
      {
        tex.m_Factory = (TextureFactory) this;
        tex.m_FactoryArgs = new object[2]
        {
          (object) this.m_LandID,
          (object) this.m_Hue
        };
        tex._shaderData = this.m_Hue.ShaderData;
      }

      protected override bool CoreLookup()
      {
        Archives.TileArt.TryReadFile(this.GetFilePath(this.m_LandID), ref this.data);
        if (this.data != null)
          return this.data.Length == 2048;
        return false;
      }

      protected override void CoreGetDimensions(out int width, out int height)
      {
        if (this.data == null)
          throw new InvalidOperationException();
        width = height = 44;
      }

      protected override unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta)
      {
        fixed (byte* numPtr1 = this.data)
        {
          int* numPtr2 = (int*) numPtr1;
          int num1 = 11;
          fixed (int* numPtr3 = this.m_Offset)
            fixed (int* numPtr4 = this.m_Length)
            {
              int* numPtr5 = numPtr3;
              int* numPtr6 = numPtr4;
              while (--num1 >= 0)
              {
                int* numPtr7 = (int*) (pLine + *numPtr5);
                int num2 = *numPtr6;
                this.m_Hue.CopyPixels((void*) numPtr2, (void*) numPtr7, num2 << 1);
                int* numPtr8 = numPtr2 + num2;
                pLine += lineEndDelta;
                int* numPtr9 = (int*) (pLine + numPtr5[1]);
                int num3 = numPtr6[1];
                this.m_Hue.CopyPixels((void*) numPtr8, (void*) numPtr9, num3 << 1);
                int* numPtr10 = numPtr8 + num3;
                pLine += lineEndDelta;
                int* numPtr11 = (int*) (pLine + numPtr5[2]);
                int num4 = numPtr6[2];
                this.m_Hue.CopyPixels((void*) numPtr10, (void*) numPtr11, num4 << 1);
                int* numPtr12 = numPtr10 + num4;
                pLine += lineEndDelta;
                int* numPtr13 = (int*) (pLine + numPtr5[3]);
                int num5 = numPtr6[3];
                this.m_Hue.CopyPixels((void*) numPtr12, (void*) numPtr13, num5 << 1);
                numPtr2 = numPtr12 + num5;
                pLine += lineEndDelta;
                numPtr5 += 4;
                numPtr6 += 4;
              }
            }
        }
      }
    }
  }
}
