// Decompiled with JetBrains decompiler
// Type: PlayUO.TextureEffects
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public static class TextureEffects
  {
    private static byte[] _screenTable;

    private static byte[] GetScreenTable()
    {
      if (TextureEffects._screenTable == null)
      {
        TextureEffects._screenTable = new byte[1024];
        for (int index1 = 0; index1 < 32; ++index1)
        {
          for (int index2 = 0; index2 < 32; ++index2)
          {
            double d = (double) index1 / 31.0;
            double num1 = (double) index2 / 31.0;
            double num2 = 1.0 - (1.0 - d) * (1.0 - num1);
            int num3 = (int) ((d * 0.25 + num2 * 0.5 + num1 * 0.25 + Math.Sqrt(d) * 0.1) * 31.0 + 0.5);
            if (num3 < 0)
              num3 = 0;
            else if (num3 > 31)
              num3 = 31;
            TextureEffects._screenTable[index1 << 5 | index2] = (byte) num3;
          }
        }
      }
      return TextureEffects._screenTable;
    }

    public static unsafe void MedianBlur(ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta, int width, int height)
    {
    }

    private static unsafe ushort Median(ushort* pInput, int x, int y, int width, int height, int stride)
    {
      ushort* numPtr1 = stackalloc ushort[32];
      ushort* numPtr2 = stackalloc ushort[32];
      ushort* numPtr3 = stackalloc ushort[32];
      int num1 = 0;
      for (int index1 = -16; index1 <= 16; ++index1)
      {
        for (int index2 = -16; index2 <= 16; ++index2)
        {
          if (index1 * index1 + index2 * index2 < 256)
          {
            int num2 = x + index2;
            int num3 = y + index1;
            if (num2 >= 0 && num2 < width && (num3 >= 0 && num3 < height))
            {
              ushort num4 = pInput[num3 * stride + num2];
              if (((int) num4 & 32768) == 32768)
              {
                IntPtr num5 = (IntPtr) (numPtr1 + ((int) num4 >> 10 & 31));
                int num6 = (int) (ushort) ((uint) *(ushort*) num5 + 1U);
                *(short*) num5 = (short) num6;
                IntPtr num7 = (IntPtr) (numPtr2 + ((int) num4 >> 5 & 31));
                int num8 = (int) (ushort) ((uint) *(ushort*) num7 + 1U);
                *(short*) num7 = (short) num8;
                IntPtr num9 = (IntPtr) (numPtr3 + ((int) num4 & 31));
                int num10 = (int) (ushort) ((uint) *(ushort*) num9 + 1U);
                *(short*) num9 = (short) num10;
                ++num1;
              }
            }
          }
        }
      }
      if (num1 == 0)
        return 0;
      ushort num11 = 0;
      ushort num12 = 0;
      ushort num13 = 0;
      int num14 = num1 / 8;
      ushort num15 = 0;
      ushort num16 = 0;
      ushort num17 = 0;
      while ((int) num11 < 31 && (int) num15 < num14)
        num15 += numPtr1[num11++];
      while ((int) num12 < 31 && (int) num16 < num14)
        num16 += numPtr2[num12++];
      while ((int) num13 < 31 && (int) num17 < num14)
        num17 += numPtr3[num13++];
      return (ushort) ((uint) (32768 | (int) num11 << 10 | (int) num12 << 5) | (uint) num13);
    }
  }
}
