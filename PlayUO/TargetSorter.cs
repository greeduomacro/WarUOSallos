// Decompiled with JetBrains decompiler
// Type: PlayUO.TargetSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public sealed class TargetSorter : IComparer<Mobile>
  {
    public static readonly IComparer<Mobile> Comparer = (IComparer<Mobile>) new TargetSorter();

    public int Compare(Mobile x, Mobile y)
    {
      if (x == null && y == null)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      int num = -x.Human.CompareTo(y.Human);
      if (num == 0 && num == 0)
      {
        Mobile player = World.Player;
        if (player != null)
          num = x.DistanceSqrt((IPoint2D) player).CompareTo(y.DistanceSqrt((IPoint2D) player));
      }
      return num;
    }
  }
}
