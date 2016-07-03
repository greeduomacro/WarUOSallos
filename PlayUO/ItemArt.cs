// Decompiled with JetBrains decompiler
// Type: PlayUO.ItemArt
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using Ultima.Data;

namespace PlayUO
{
  public class ItemArt : IDisposable
  {
    private ItemArt.ItemFactory m_Factory;

    public void Dispose()
    {
    }

    public Texture ReadFromDisk(int ItemID, IHue Hue)
    {
      ItemID &= (int) ushort.MaxValue;
      if (ItemID >= 13700 && ItemID <= 13729)
        return Hue.GetGump(2331 + (ItemID - 13700));
      if (this.m_Factory == null)
        this.m_Factory = new ItemArt.ItemFactory(this);
      return this.m_Factory.Load(ItemID, Hue);
    }

    private class ItemFactory : TextureFactory
    {
      private static ushort[] _guassianBlurMatrix = new ushort[289]{ (ushort) 0, (ushort) 11, (ushort) 21, (ushort) 31, (ushort) 38, (ushort) 45, (ushort) 50, (ushort) 53, (ushort) 53, (ushort) 53, (ushort) 50, (ushort) 45, (ushort) 38, (ushort) 31, (ushort) 21, (ushort) 11, (ushort) 0, (ushort) 11, (ushort) 23, (ushort) 34, (ushort) 44, (ushort) 53, (ushort) 60, (ushort) 65, (ushort) 68, (ushort) 69, (ushort) 68, (ushort) 65, (ushort) 60, (ushort) 53, (ushort) 44, (ushort) 34, (ushort) 23, (ushort) 11, (ushort) 21, (ushort) 34, (ushort) 46, (ushort) 57, (ushort) 66, (ushort) 74, (ushort) 80, (ushort) 84, (ushort) 85, (ushort) 84, (ushort) 80, (ushort) 74, (ushort) 66, (ushort) 57, (ushort) 46, (ushort) 34, (ushort) 21, (ushort) 31, (ushort) 44, (ushort) 57, (ushort) 68, (ushort) 79, (ushort) 88, (ushort) 95, (ushort) 100, (ushort) 101, (ushort) 100, (ushort) 95, (ushort) 88, (ushort) 79, (ushort) 68, (ushort) 57, (ushort) 44, (ushort) 31, (ushort) 38, (ushort) 53, (ushort) 66, (ushort) 79, (ushort) 91, (ushort) 101, (ushort) 110, (ushort) 116, (ushort) 117, (ushort) 116, (ushort) 110, (ushort) 101, (ushort) 91, (ushort) 79, (ushort) 66, (ushort) 53, (ushort) 38, (ushort) 45, (ushort) 60, (ushort) 74, (ushort) 88, (ushort) 101, (ushort) 114, (ushort) 124, (ushort) 131, (ushort) 133, (ushort) 131, (ushort) 124, (ushort) 114, (ushort) 101, (ushort) 88, (ushort) 74, (ushort) 60, (ushort) 45, (ushort) 50, (ushort) 65, (ushort) 80, (ushort) 95, (ushort) 110, (ushort) 124, (ushort) 136, (ushort) 146, (ushort) 149, (ushort) 146, (ushort) 136, (ushort) 124, (ushort) 110, (ushort) 95, (ushort) 80, (ushort) 65, (ushort) 50, (ushort) 53, (ushort) 68, (ushort) 84, (ushort) 100, (ushort) 116, (ushort) 131, (ushort) 146, (ushort) 159, (ushort) 165, (ushort) 159, (ushort) 146, (ushort) 131, (ushort) 116, (ushort) 100, (ushort) 84, (ushort) 68, (ushort) 53, (ushort) 53, (ushort) 69, (ushort) 85, (ushort) 101, (ushort) 117, (ushort) 133, (ushort) 149, (ushort) 165, (ushort) 181, (ushort) 165, (ushort) 149, (ushort) 133, (ushort) 117, (ushort) 101, (ushort) 85, (ushort) 69, (ushort) 53, (ushort) 53, (ushort) 68, (ushort) 84, (ushort) 100, (ushort) 116, (ushort) 131, (ushort) 146, (ushort) 159, (ushort) 165, (ushort) 159, (ushort) 146, (ushort) 131, (ushort) 116, (ushort) 100, (ushort) 84, (ushort) 68, (ushort) 53, (ushort) 50, (ushort) 65, (ushort) 80, (ushort) 95, (ushort) 110, (ushort) 124, (ushort) 136, (ushort) 146, (ushort) 149, (ushort) 146, (ushort) 136, (ushort) 124, (ushort) 110, (ushort) 95, (ushort) 80, (ushort) 65, (ushort) 50, (ushort) 45, (ushort) 60, (ushort) 74, (ushort) 88, (ushort) 101, (ushort) 114, (ushort) 124, (ushort) 131, (ushort) 133, (ushort) 131, (ushort) 124, (ushort) 114, (ushort) 101, (ushort) 88, (ushort) 74, (ushort) 60, (ushort) 45, (ushort) 38, (ushort) 53, (ushort) 66, (ushort) 79, (ushort) 91, (ushort) 101, (ushort) 110, (ushort) 116, (ushort) 117, (ushort) 116, (ushort) 110, (ushort) 101, (ushort) 91, (ushort) 79, (ushort) 66, (ushort) 53, (ushort) 38, (ushort) 31, (ushort) 44, (ushort) 57, (ushort) 68, (ushort) 79, (ushort) 88, (ushort) 95, (ushort) 100, (ushort) 101, (ushort) 100, (ushort) 95, (ushort) 88, (ushort) 79, (ushort) 68, (ushort) 57, (ushort) 44, (ushort) 31, (ushort) 21, (ushort) 34, (ushort) 46, (ushort) 57, (ushort) 66, (ushort) 74, (ushort) 80, (ushort) 84, (ushort) 85, (ushort) 84, (ushort) 80, (ushort) 74, (ushort) 66, (ushort) 57, (ushort) 46, (ushort) 34, (ushort) 21, (ushort) 11, (ushort) 23, (ushort) 34, (ushort) 44, (ushort) 53, (ushort) 60, (ushort) 65, (ushort) 68, (ushort) 69, (ushort) 68, (ushort) 65, (ushort) 60, (ushort) 53, (ushort) 44, (ushort) 34, (ushort) 23, (ushort) 11, (ushort) 0, (ushort) 11, (ushort) 21, (ushort) 31, (ushort) 38, (ushort) 45, (ushort) 50, (ushort) 53, (ushort) 53, (ushort) 53, (ushort) 50, (ushort) 45, (ushort) 38, (ushort) 31, (ushort) 21, (ushort) 11, (ushort) 0 };
      private int m_ItemID;
      private IHue m_Hue;
      private int m_xMin;
      private int m_yMin;
      private int m_xMax;
      private int m_yMax;
      private int _averageColor;
      private ItemArt m_Items;
      private byte[] data;

      public override TextureTransparency Transparency
      {
        get
        {
          return TextureTransparency.Simple;
        }
      }

      public ItemFactory(ItemArt items)
      {
        this.m_Items = items;
      }

      public Texture Load(int itemID, IHue hue)
      {
        this.m_ItemID = itemID;
        this.m_Hue = hue;
        return this.Construct(false);
      }

      public override Texture Reconstruct(object[] args)
      {
        this.m_ItemID = (int) args[0];
        this.m_Hue = (IHue) args[1];
        return this.Construct(true);
      }

      protected override void CoreAssignArgs(Texture tex)
      {
        tex.m_Factory = (TextureFactory) this;
        tex.m_FactoryArgs = new object[2]
        {
          (object) this.m_ItemID,
          (object) this.m_Hue
        };
        tex.xMin = this.m_xMin;
        tex.yMin = this.m_yMin;
        tex.xMax = this.m_xMax;
        tex.yMax = this.m_yMax;
        tex._averageColor = this._averageColor;
        tex._shaderData = this.m_Hue.ShaderData;
      }

      private string GetFilePath(int tileId)
      {
        return string.Format("build/artlegacymul/{0:00000000}.tga", (object) tileId);
      }

      protected override bool CoreLookup()
      {
        Archives.TileArt.TryReadFile(this.GetFilePath(16384 + this.m_ItemID), out this.data);
        if (this.data != null)
          return this.data.Length > 8;
        return false;
      }

      protected override void CoreGetDimensions(out int width, out int height)
      {
        if (this.data == null)
          throw new InvalidOperationException();
        width = (int) BitConverter.ToInt16(this.data, 4);
        height = (int) BitConverter.ToInt16(this.data, 6);
        if (!(this.m_Hue is Hues.ShadowHue))
          return;
        width += 17;
        height += 17;
      }

      protected override unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta)
      {
        fixed (byte* numPtr1 = this.data)
        {
          short* numPtr2 = (short*) (numPtr1 + 8 + height * 2 - (m_Hue is Hues.ShadowHue ? 17 : 0) * 2);
          short* numPtr3 = (short*) (numPtr1 + 6);
          int num1 = width;
          int num2 = height;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          int num6 = 0;
          if (this.m_Hue is Hues.ShadowHue)
          {
            fixed (ushort* numPtr4 = ItemArt.ItemFactory._guassianBlurMatrix)
            {
              ushort* numPtr5 = pLine;
              ushort* numPtr6 = numPtr5;
              while (numPtr6 < pImageEnd)
                *numPtr6++ = (ushort) 0;
              pLine += 8 * lineEndDelta;
              int num7 = 0;
              int num8 = 0;
              int num9 = 0;
              int num10 = 0;
              while (num6 < height - 17)
              {
                short* numPtr7 = numPtr2 + *++numPtr3;
                ushort* numPtr8 = pLine + 8;
                num5 = 0;
                if ((int) *numPtr7 + (int) numPtr7[1] != 0 && (int) *numPtr7 < num1)
                  num1 = (int) *numPtr7;
                while (true)
                {
                  short* numPtr9 = numPtr7;
                  IntPtr num11 = new IntPtr(2);
                  short* numPtr10 = (short*)num11 + (short)numPtr9;
                  int num12;
                  int num13 = num12 = (int) *numPtr9;
                  short* numPtr11 = numPtr10;
                  IntPtr num14 = new IntPtr(2);
                  numPtr7 = (short*) num14 + (short)numPtr11;
                  int num15;
                  int num16 = num15 = (int) *numPtr11;
                  if (num12 + num15 != 0)
                  {
                    ushort* numPtr12 = numPtr8 + num13;
                    for (int index1 = 0; index1 < num16; ++index1)
                    {
                      short num17 = *numPtr7++;
                      num7 += (int) num17 >> 10 & 31;
                      num8 += (int) num17 >> 5 & 31;
                      num9 += (int) num17 & 31;
                      num10 += 32;
                      ushort* numPtr13 = numPtr4;
                      for (int index2 = -8; index2 <= 8; ++index2)
                      {
                        ushort* numPtr14 = numPtr12 + index2 * lineEndDelta + index1 - 8;
                        ushort* numPtr15 = numPtr14 + 17;
                        while (numPtr14 < numPtr15)
                        {
                          ushort* numPtr16 = numPtr14++;
                          int num18 = (int) (ushort) ((int) *numPtr16 + (int) *numPtr13++);
                          *numPtr16 = (ushort) num18;
                        }
                      }
                    }
                    numPtr8 = numPtr12 + num16;
                  }
                  else
                    break;
                }
                int num19;
                if ((num19 = (int) (numPtr8 - pLine)) > 8)
                {
                  if (num2 == height)
                    num2 = num6;
                  num4 = num6;
                  int num11 = num19 - 1;
                  if (num11 > num3)
                    num3 = num11;
                }
                ++num6;
                pLine += lineEndDelta;
              }
              num1 += 8;
              num2 += 8;
              num4 += 8;
              int num20;
              for (ushort* numPtr7 = numPtr5; numPtr7 < pImageEnd; *numPtr7++ = (ushort) (32768 | num20 << 10 | num20 << 5 | num20))
                num20 = (int) *numPtr7 * 31 / 22409;
              if (num10 > 0)
                this._averageColor = (num7 * (int) byte.MaxValue + num10 / 2) / num10 << 16 | (num8 * (int) byte.MaxValue + num10 / 2) / num10 << 8 | (num9 * (int) byte.MaxValue + num10 / 2) / num10;
            }
          }
          else
          {
            while (num6 < height)
            {
              short* numPtr4 = numPtr2 + *++numPtr3;
              ushort* numPtr5 = pLine;
              num5 = 0;
              if ((int) *numPtr4 + (int) numPtr4[1] != 0 && (int) *numPtr4 < num1)
                num1 = (int) *numPtr4;
              while (true)
              {
                short* numPtr6 = numPtr4;
                IntPtr num7 = new IntPtr(2);
                short* numPtr7 = (int) numPtr6 + (short*)num7;
                int num8;
                int num9 = num8 = (int) *numPtr6;
                short* numPtr8 = numPtr7;
                IntPtr num10 = new IntPtr(2);
                short* numPtr9 = (int) numPtr8 + (short*)num10;
                int num11;
                int Pixels = num11 = (int) *numPtr8;
                if (num8 + num11 != 0)
                {
                  ushort* numPtr10 = numPtr5 + num9;
                  this.m_Hue.CopyPixels((void*) numPtr9, (void*) numPtr10, Pixels);
                  numPtr5 = numPtr10 + Pixels;
                  numPtr4 = numPtr9 + Pixels;
                }
                else
                  break;
              }
              int num12;
              if ((num12 = (int) (numPtr5 - pLine)) > 0)
              {
                if (num2 == height)
                  num2 = num6;
                num4 = num6;
                int num7 = num12 - 1;
                if (num7 > num3)
                  num3 = num7;
              }
              ++num6;
              pLine += lineEndDelta;
            }
          }
          this.m_xMin = num1;
          this.m_yMin = num2;
          this.m_xMax = num3;
          this.m_yMax = num4;
        }
      }
    }
  }
}
