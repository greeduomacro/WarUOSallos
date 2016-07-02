// Decompiled with JetBrains decompiler
// Type: PlayUO.ScratchList`1
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public class ScratchList<T> : Scratch<List<T>>
  {
    protected override void Release(List<T> value)
    {
      this.Release(value);
      value.Clear();
    }

    public static T[] ToArray(IEnumerable<T> collection)
    {
      using (ScratchList<T> scratchList = new ScratchList<T>())
      {
        List<T> objList = scratchList.Value;
        objList.AddRange(collection);
        return objList.ToArray();
      }
    }
  }
}
