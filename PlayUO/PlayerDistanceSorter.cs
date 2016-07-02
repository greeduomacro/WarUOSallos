// Decompiled with JetBrains decompiler
// Type: PlayUO.PlayerDistanceSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public sealed class PlayerDistanceSorter : IComparer
  {
    public static readonly IComparer Comparer = (IComparer) new PlayerDistanceSorter();

    public int Compare(object x, object y)
    {
      if (x == null && y == null)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      IPoint2D p1 = x as IPoint2D;
      IPoint2D p2 = y as IPoint2D;
      if (p1 == null || p2 == null)
        throw new ArgumentException();
      Mobile player = World.Player;
      return player.DistanceSqrt(p1).CompareTo(player.DistanceSqrt(p2));
    }
  }
}
