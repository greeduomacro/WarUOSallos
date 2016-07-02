// Decompiled with JetBrains decompiler
// Type: PlayUO.CustomMultiEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos.Compression;
using System;
using System.Collections;

namespace PlayUO
{
  public class CustomMultiEntry
  {
    private int m_Serial;
    private int m_Revision;
    private Multi m_Multi;
    private static byte[] m_InflateBuffer;

    public Multi Multi
    {
      get
      {
        return this.m_Multi;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public int Revision
    {
      get
      {
        return this.m_Revision;
      }
    }

    public CustomMultiEntry(int ser, int rev, Multi baseMulti, int compressionType, byte[] buffer)
    {
      this.m_Serial = ser;
      this.m_Revision = rev;
      int xMin;
      int yMin;
      int xMax;
      int yMax;
      baseMulti.GetBounds(out xMin, out yMin, out xMax, out yMax);
      ArrayList list = new ArrayList();
      try
      {
        switch (compressionType)
        {
          case 0:
            CustomMultiEntry.LoadUncompressed(buffer, list);
            break;
          case 3:
            CustomMultiEntry.LoadDeflated(xMin, yMin, xMax, yMax, buffer, list);
            break;
        }
      }
      catch (Exception ex)
      {
        Debug.Error(ex);
      }
      this.m_Multi = new Multi(list);
    }

    public static void LoadUncompressed(byte[] buffer, ArrayList list)
    {
      int num1 = buffer.Length / 5;
      int num2 = 0;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        byte[] numArray1 = buffer;
        int index2 = num2;
        int num3 = 1;
        int num4 = index2 + num3;
        int num5 = (int) numArray1[index2] << 8;
        byte[] numArray2 = buffer;
        int index3 = num4;
        int num6 = 1;
        int num7 = index3 + num6;
        int num8 = (int) numArray2[index3];
        int num9 = num5 | num8;
        byte[] numArray3 = buffer;
        int index4 = num7;
        int num10 = 1;
        int num11 = index4 + num10;
        int num12 = (int) (sbyte) numArray3[index4];
        byte[] numArray4 = buffer;
        int index5 = num11;
        int num13 = 1;
        int num14 = index5 + num13;
        int num15 = (int) (sbyte) numArray4[index5];
        byte[] numArray5 = buffer;
        int index6 = num14;
        int num16 = 1;
        num2 = index6 + num16;
        int num17 = (int) (sbyte) numArray5[index6];
        list.Add((object) new MultiItem()
        {
          Flags = 1,
          ItemID = (short) num9,
          X = (short) num12,
          Y = (short) num15,
          Z = (short) num17
        });
      }
    }

    public static unsafe void LoadDeflated(int xMin, int yMin, int xMax, int yMax, byte[] buffer, ArrayList list)
    {
      int num1 = yMax - yMin + 1;
      fixed (byte* numPtr1 = buffer)
      {
        IntPtr num2 = new IntPtr(1);
        byte* numPtr2 = numPtr1 + num2.ToInt64();
        int num3 = (int) numPtr1[0];
        for (int index1 = 0; index1 < num3; ++index1)
        {
          int num4 = (int) *numPtr2 >> 4 & 15;
          int num5 = (int) *numPtr2 & 15;
          int length1 = (int) numPtr2[1] | (int) numPtr2[3] << 4 & 3840;
          int length2 = (int) numPtr2[2] | (int) numPtr2[3] << 8 & 3840;
          numPtr2 += 4;
          if (CustomMultiEntry.m_InflateBuffer == null || CustomMultiEntry.m_InflateBuffer.Length < length1)
            CustomMultiEntry.m_InflateBuffer = new byte[length1];
          fixed (byte* numPtr3 = CustomMultiEntry.m_InflateBuffer)
          {
            byte[] numArray = new byte[length2];
            for (int index2 = 0; index2 < numArray.Length; ++index2)
              numArray[index2] = *numPtr2++;
            ZLib.Decompress(CustomMultiEntry.m_InflateBuffer, ref length1, numArray, numArray.Length);
            byte* numPtr4 = numPtr3 + length1;
            switch (num4)
            {
              case 0:
                while (numPtr3 < numPtr4)
                {
                  MultiItem multiItem = new MultiItem();
                  multiItem.Flags = 1;
                  multiItem.ItemID = (short) ((int) numPtr3[0] << 8 | (int) numPtr3[1]);
                  multiItem.X = (short) (sbyte) numPtr3[2];
                  multiItem.Y = (short) (sbyte) numPtr3[3];
                  multiItem.Z = (short) (sbyte) numPtr3[4];
                  numPtr3 += 5;
                  if ((int) multiItem.ItemID != 0)
                    list.Add((object) multiItem);
                }
                break;
              case 1:
                int num6 = 0;
                switch (num5)
                {
                  case 0:
                    num6 = 0;
                    break;
                  case 1:
                  case 5:
                    num6 = 7;
                    break;
                  case 2:
                  case 6:
                    num6 = 27;
                    break;
                  case 3:
                  case 7:
                    num6 = 47;
                    break;
                  case 4:
                  case 8:
                    num6 = 67;
                    break;
                }
                while (numPtr3 < numPtr4)
                {
                  MultiItem multiItem = new MultiItem();
                  multiItem.Flags = 1;
                  multiItem.ItemID = (short) ((int) numPtr3[0] << 8 | (int) numPtr3[1]);
                  multiItem.X = (short) (sbyte) numPtr3[2];
                  multiItem.Y = (short) (sbyte) numPtr3[3];
                  multiItem.Z = (short) (sbyte) num6;
                  numPtr3 += 4;
                  if ((int) multiItem.ItemID != 0)
                    list.Add((object) multiItem);
                }
                break;
              case 2:
                int num7 = 0;
                switch (num5)
                {
                  case 0:
                    num7 = 0;
                    break;
                  case 1:
                  case 5:
                    num7 = 7;
                    break;
                  case 2:
                  case 6:
                    num7 = 27;
                    break;
                  case 3:
                  case 7:
                    num7 = 47;
                    break;
                  case 4:
                  case 8:
                    num7 = 67;
                    break;
                }
                int num8;
                int num9;
                int num10;
                if (num5 <= 0)
                {
                  num8 = xMin;
                  num9 = yMin;
                  num10 = num1 + 1;
                }
                else if (num5 <= 4)
                {
                  num8 = xMin + 1;
                  num9 = yMin + 1;
                  num10 = num1 - 1;
                }
                else
                {
                  num8 = xMin;
                  num9 = yMin;
                  num10 = num1;
                }
                int num11 = 0;
                while (numPtr3 < numPtr4)
                {
                  short num12 = (short) ((int) numPtr3[0] << 8 | (int) numPtr3[1]);
                  ++num11;
                  numPtr3 += 2;
                  if ((int) num12 != 0)
                    list.Add((object) new MultiItem()
                    {
                      Flags = 1,
                      ItemID = num12,
                      X = (short) (num8 + (num11 - 1) / num10),
                      Y = (short) (sbyte) (num9 + (num11 - 1) % num10),
                      Z = (short) (sbyte) num7
                    });
                }
                break;
            }
          }
        }
      }
    }
  }
}
