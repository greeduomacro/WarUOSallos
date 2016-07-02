// Decompiled with JetBrains decompiler
// Type: PlayUO.HealSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public sealed class HealSorter : IComparer
  {
    public static readonly IComparer Comparer = (IComparer) new HealSorter();

    public int Compare(object x, object y)
    {
      if (x == null && y == null)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      Mobile mobile1 = x as Mobile;
      Mobile mobile2 = y as Mobile;
      return (mobile1.MaximumHitPoints <= 0 ? 100 : mobile1.CurrentHitPoints * 100 / mobile1.MaximumHitPoints) - (mobile2.MaximumHitPoints <= 0 ? 100 : mobile2.CurrentHitPoints * 100 / mobile2.MaximumHitPoints);
    }
  }
}
