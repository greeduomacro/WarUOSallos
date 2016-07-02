// Decompiled with JetBrains decompiler
// Type: PlayUO.NameSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public sealed class NameSorter : IComparer
  {
    public static readonly IComparer Comparer = (IComparer) new NameSorter();

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
      if (mobile1 == null || mobile2 == null)
        throw new ArgumentException();
      bool human1 = mobile1.Human;
      bool human2 = mobile2.Human;
      if (human1 && !human2)
        return -1;
      if (human2 && !human1)
        return 1;
      return mobile1.Serial.CompareTo(mobile2.Serial);
    }
  }
}
