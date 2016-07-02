// Decompiled with JetBrains decompiler
// Type: PlayUO.Compression
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;
using System.Threading;

namespace PlayUO
{
  public static class Compression
  {
    private static short[] _sourceTable = new short[257]{ (short) 2, (short) 501, (short) 278, (short) 359, (short) 1399, (short) 86, (short) 886, (short) 615, (short) 120, (short) 1128, (short) 855, (short) 2536, (short) 5945, (short) 3736, (short) 342, (short) 1879, (short) 2280, (short) 3496, (short) 1657, (short) 3672, (short) 1319, (short) 1943, (short) 1640, (short) 3048, (short) 4409, (short) 3705, (short) 2023, (short) 297, (short) 2472, (short) 3560, (short) 2200, (short) 1592, (short) 726, (short) 3129, (short) 3944, (short) 6441, (short) 3306, (short) 1448, (short) 7530, (short) 234, (short) 601, (short) 2649, (short) 2089, (short) 15978, (short) 4569, (short) 153, (short) 7401, (short) 13290, (short) 6745, (short) 25, (short) 313, (short) 806, (short) 4313, (short) 5273, (short) 6553, (short) 5433, (short) 6617, (short) 2681, (short) 3113, (short) 4857, (short) 7017, (short) 1785, (short) 6697, (short) 5161, (short) 165, (short) 1688, (short) 920, (short) 6265, (short) 1849, (short) 7721, (short) 7737, (short) 4393, (short) 7209, (short) 3177, (short) 7641, (short) 4649, (short) 3433, (short) 1577, (short) 3833, (short) 6905, (short) 1065, (short) 1241, (short) 5593, (short) 5753, (short) 1960, (short) 553, (short) 6809, (short) 2521, (short) 2585, (short) 5673, (short) 2969, (short) 1817, (short) 8682, (short) 490, (short) 7225, (short) 3818, (short) 16106, (short) 375, (short) 7993, (short) 2840, (short) 632, (short) 1271, (short) 2793, (short) 4329, (short) 40, (short) 1047, (short) 6681, (short) 425, (short) 1384, (short) 2104, (short) 1143, (short) 247, (short) 2264, (short) 1625, (short) 1559, (short) 1768, (short) 823, (short) 1431, (short) 2601, (short) 982, (short) 2360, (short) 3689, (short) 4697, (short) 217, (short) 5994, (short) 7786, (short) 15994, (short) 5353, (short) 1337, (short) 1081, (short) 3225, (short) 103, (short) 872, (short) 4249, (short) 761, (short) 8426, (short) 11818, (short) 12714, (short) 10362, (short) 14074, (short) 12202, (short) 6186, (short) 14970, (short) 7322, (short) 935, (short) 408, (short) 3898, (short) 7482, (short) 9706, (short) 8105, (short) 16122, (short) 12010, (short) 11578, (short) 11738, (short) 13370, (short) 14378, (short) 1498, (short) 7930, (short) 10538, (short) 1514, (short) 4841, (short) 11498, (short) 11114, (short) 5337, (short) 4890, (short) 8666, (short) 15083, (short) 12779, (short) 9195, (short) 10266, (short) 5722, (short) 1178, (short) 11482, (short) 14106, (short) 20971, (short) 8858, (short) 9690, (short) 4762, (short) 2809, (short) 747, (short) 17643, (short) 3866, (short) 9451, (short) 21995, (short) 13082, (short) 3290, (short) 1259, (short) 14186, (short) 17131, (short) 4587, (short) 8730, (short) 12314, (short) 2713, (short) 8986, (short) 1003, (short) 13914, (short) 30187, (short) 4122, (short) 23275, (short) 12826, (short) 12954, (short) 3386, (short) 12058, (short) 16154, (short) 15674, (short) 3626, (short) 2170, (short) 3642, (short) 4010, (short) 5178, (short) 474, (short) 7066, (short) 666, (short) 25579, (short) 6778, (short) 15466, (short) 2074, (short) 6170, (short) 794, (short) 10650, (short) 4634, (short) 7962, (short) 6891, (short) 25835, (short) 17387, (short) 7914, (short) 13803, (short) 11834, (short) 21483, (short) 5099, (short) 14362, (short) 29163, (short) 15258, (short) 5611, (short) 56, (short) 4522, (short) 5882, (short) 2346, (short) 2922, (short) 2458, (short) 12090, (short) 8938, (short) 15722, (short) 7802, (short) 15514, (short) 26475, (short) 9370, (short) 10091, (short) 7385, (short) 538, (short) 5914, (short) 1898, (short) 31467, (short) 7274, (short) 3546, (short) 567, (short) 180 };
    public static UnpackLeaf[] m_Leaves = new UnpackLeaf[513];
    public static UnpackLeaf m_Tree;
    public static UnpackCacheEntry[] m_CacheEntries;
    public static byte[] m_OutputBuffer;
    public static int m_OutputIndex;
    private static bool m_UnpackCacheLoaded;

    static unsafe Compression()
    {
      int index1 = 0;
      Compression.m_Tree = new UnpackLeaf(index1);
      UnpackLeaf[] unpackLeafArray = Compression.m_Leaves;
      int index2 = index1;
      int num1 = 1;
      int index3 = index2 + num1;
      UnpackLeaf unpackLeaf1 = Compression.m_Tree;
      unpackLeafArray[index2] = unpackLeaf1;
      fixed (short* numPtr1 = Compression._sourceTable)
      {
        short* numPtr2 = numPtr1;
        for (short index4 = 0; (int) index4 <= 256; ++index4)
        {
          UnpackLeaf unpackLeaf2 = Compression.m_Tree;
          int num2 = (int) *numPtr2++;
          int num3 = num2 & 15;
          int num4 = num2 >> 4;
          while (--num3 >= 0)
          {
            switch (num4 & 1)
            {
              case 0:
                if (unpackLeaf2.m_Left == null)
                {
                  unpackLeaf2.m_Left = new UnpackLeaf(index3);
                  Compression.m_Leaves[index3++] = unpackLeaf2.m_Left;
                }
                unpackLeaf2 = unpackLeaf2.m_Left;
                break;
              case 1:
                if (unpackLeaf2.m_Right == null)
                {
                  unpackLeaf2.m_Right = new UnpackLeaf(index3);
                  Compression.m_Leaves[index3++] = unpackLeaf2.m_Right;
                }
                unpackLeaf2 = unpackLeaf2.m_Right;
                break;
            }
            num4 >>= 1;
          }
          unpackLeaf2.m_Value = index4;
        }
      }
      Compression._sourceTable = (short[]) null;
    }

    private static void Thread_LoadUnpackCache(object state)
    {
      ManualResetEvent manualResetEvent = (ManualResetEvent) state;
      Compression.LoadUnpackCache();
      manualResetEvent.Set();
    }

    public static unsafe void LoadUnpackCache()
    {
      Compression.m_OutputBuffer = new byte[100725];
      Compression.m_CacheEntries = new UnpackCacheEntry[65536];
      fixed (UnpackCacheEntry* unpackCacheEntryPtr1 = Compression.m_CacheEntries)
      {
        UnpackCacheEntry* unpackCacheEntryPtr2 = unpackCacheEntryPtr1;
        fixed (byte* numPtr1 = Compression.m_OutputBuffer)
        {
          Stack<UnpackLeaf> unpackLeafStack = new Stack<UnpackLeaf>();
          unpackLeafStack.Push(Compression.m_Tree);
          while (unpackLeafStack.Count > 0)
          {
            UnpackLeaf unpackLeaf1 = unpackLeafStack.Pop();
            if (unpackLeaf1.m_Left != null)
              unpackLeafStack.Push(unpackLeaf1.m_Left);
            if (unpackLeaf1.m_Right != null)
              unpackLeafStack.Push(unpackLeaf1.m_Right);
            int[] numArray = new int[256];
label_21:
            for (int index = 0; index < 256; ++index)
            {
              byte* numPtr2 = numPtr1 + Compression.m_OutputIndex;
              UnpackLeaf unpackLeaf2 = unpackLeaf1;
              int num1 = 0;
              int num2 = 8;
              while (--num2 >= 0)
              {
                switch (index >> num2 & 1)
                {
                  case 0:
                    unpackLeaf2 = unpackLeaf2.m_Left;
                    break;
                  case 1:
                    unpackLeaf2 = unpackLeaf2.m_Right;
                    break;
                }
                if (unpackLeaf2 != null)
                {
                  if ((int) unpackLeaf2.m_Value != -1)
                  {
                    switch ((int) unpackLeaf2.m_Value >> 8)
                    {
                      case 0:
                        numPtr2[num1++] = (byte) unpackLeaf2.m_Value;
                        break;
                      case 1:
                        num2 = 0;
                        break;
                    }
                    unpackLeaf2 = Compression.m_Tree;
                  }
                }
                else
                  goto label_21;
              }
              unpackCacheEntryPtr2->m_NextIndex = (int) unpackLeaf2.m_Index;
              unpackCacheEntryPtr2->m_ByteIndex = Compression.m_OutputIndex;
              unpackCacheEntryPtr2->m_ByteCount = num1;
              numArray[index] = (int) (unpackCacheEntryPtr2 - unpackCacheEntryPtr1);
              ++unpackCacheEntryPtr2;
              Compression.m_OutputIndex += num1;
            }
            unpackLeaf1.m_Cache = numArray;
          }
        }
      }
    }

    public static void CheckCache()
    {
      if (Compression.m_UnpackCacheLoaded)
        return;
      ManualResetEvent manualResetEvent = new ManualResetEvent(false);
      ThreadPool.QueueUserWorkItem(new WaitCallback(Compression.Thread_LoadUnpackCache), (object) manualResetEvent);
      do
      {
        Engine.DrawNow();
      }
      while (!manualResetEvent.WaitOne(10, false));
      manualResetEvent.Close();
      Compression.m_UnpackCacheLoaded = true;
    }
  }
}
