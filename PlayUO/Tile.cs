// Decompiled with JetBrains decompiler
// Type: PlayUO.Tile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Runtime.InteropServices;
using Ultima.Data;

namespace PlayUO
{
  [StructLayout(LayoutKind.Sequential, Size = 3, Pack = 1)]
  public struct Tile
  {
    public LandId landId;
    public sbyte z;

    public bool Ignored
    {
      get
      {
          if ((int)landId == 2 || (int)landId == 475)
          return true;
          if ((int)landId >= 430)
          return (int)landId <= 437;
        return false;
      }
    }

    public bool Visible
    {
      get
      {
          if ((int)landId == 2 || (int)landId == 475)
          return false;
          if ((int)landId >= 430)
              return (int)landId > 437;
        return true;
      }
    }
  }
}
