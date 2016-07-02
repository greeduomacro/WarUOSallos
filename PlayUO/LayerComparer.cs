// Decompiled with JetBrains decompiler
// Type: PlayUO.LayerComparer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayUO
{
  public class LayerComparer : IComparer<Item>
  {
    private static readonly int[][] maleDrawOrders = new int[8][]{ new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 20 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 21, 2, 20 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 21, 20, 2 }, new int[25]{ 25, 20, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 21, 20, 2 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 21, 2, 20 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 20 }, new int[25]{ 25, 5, 4, 3, 24, 13, 8, 9, 14, 15, 19, 7, 23, 17, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 20 } };
    private static readonly int[][] dollDrawOrders = new int[3][]{ new int[25]{ 20, 5, 4, 3, 24, 13, 17, 8, 14, 15, 19, 7, 23, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 9, 25 }, new int[25]{ 20, 5, 4, 3, 24, 19, 13, 17, 8, 14, 15, 7, 23, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 9, 25 }, new int[25]{ 20, 13, 5, 4, 3, 24, 17, 8, 14, 15, 19, 7, 23, 22, 12, 10, 11, 16, 18, 6, 1, 2, 21, 9, 25 } };
    private static readonly LayerComparer[] maleComparers = ((IEnumerable<int[]>) LayerComparer.maleDrawOrders).Select<int[], LayerComparer>((Func<int[], LayerComparer>) (x => new LayerComparer(x))).ToArray<LayerComparer>();
    public static readonly LayerComparer Paperdoll = new LayerComparer(LayerComparer.dollDrawOrders[1]);
    private IDictionary<int, int> map;

    public LayerComparer(int[] seq)
    {
      this.map = (IDictionary<int, int>) ((IEnumerable<int>) seq).Select((n, i) => new{ Index = i, Layer = n }).ToDictionary(x => x.Layer, x => x.Index);
    }

    public static LayerComparer FromDirection(int dir)
    {
      return LayerComparer.FromDirection((MobileDirection) dir);
    }

    public static LayerComparer FromDirection(MobileDirection dir)
    {
      return LayerComparer.maleComparers[(int) (dir & MobileDirection.Up)];
    }

    private int Sequence(Item item)
    {
      int num;
      if (!this.map.TryGetValue((int) item.Layer, out num))
        num = 30;
      return num;
    }

    public int Compare(Item a, Item b)
    {
      return this.Sequence(a).CompareTo(this.Sequence(b));
    }
  }
}
