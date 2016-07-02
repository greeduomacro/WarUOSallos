// Decompiled with JetBrains decompiler
// Type: PlayUO.TileGroup
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class TileGroup
  {
    private int m_Start;
    private int m_Count;

    public TileGroup(int start, int count)
    {
      this.m_Start = start;
      this.m_Count = count;
    }

    public static unsafe int GetBrightness(Texture tex, int xStart, int yStart, int xStep, int yStep, int count)
    {
      LockData lockData = tex.Lock(LockFlags.ReadOnly);
      short* numPtr1 = (short*) lockData.pvSrc;
      int num1 = lockData.Pitch >> 1;
      short* numPtr2 = numPtr1 + yStart * num1 + xStart;
      int num2 = num1 * yStep + xStep;
      int num3 = 0;
      for (int index = 0; index < count; ++index)
      {
        short num4 = *numPtr2;
        num3 = num3 + ((int) num4 & 31) * 114 + ((int) num4 >> 5 & 31) * 587 + ((int) num4 >> 10 & 31) * 299;
        numPtr2 += num2;
      }
      tex.Unlock();
      return (num3 << 3) / count / 1000;
    }
  }
}
