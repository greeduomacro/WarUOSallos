// Decompiled with JetBrains decompiler
// Type: PlayUO.AmountSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public sealed class AmountSorter : IComparer
  {
    public static readonly IComparer Comparer = (IComparer) new AmountSorter();

    private AmountSorter()
    {
    }

    public int Compare(object x, object y)
    {
      if (x == null && y == null)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      Item obj1 = x as Item;
      Item obj2 = y as Item;
      if (obj1 == null || obj2 == null)
        throw new ArgumentException();
      return ((ushort) obj1.Amount).CompareTo((ushort) obj2.Amount);
    }
  }
}
