// Decompiled with JetBrains decompiler
// Type: System.EnumerableEx
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace System
{
  public static class EnumerableEx
  {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> thunk)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (thunk == null)
        throw new ArgumentNullException("thunk");
      foreach (T obj in source)
        thunk(obj);
    }

    public static IEnumerable<int> Interval(int min, int max)
    {
      for (int val = min; val <= max; ++val)
        yield return val;
    }
  }
}
