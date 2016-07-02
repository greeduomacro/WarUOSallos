// Decompiled with JetBrains decompiler
// Type: PlayUO.TerrainMeshProviders
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using Ultima.Client.Terrain;

namespace PlayUO
{
  public static class TerrainMeshProviders
  {
    public static readonly TerrainMeshProvider Low = new TerrainMeshProvider((IMeshProvider) new LowMeshProvider());
    public static readonly TerrainMeshProvider Medium = new TerrainMeshProvider((IMeshProvider) new MediumMeshProvider());
    public static readonly TerrainMeshProvider High = new TerrainMeshProvider((IMeshProvider) new HighMeshProvider());
    private static TerrainMeshProvider _current;

    public static TerrainMeshProvider Current
    {
      get
      {
        if (TerrainMeshProviders._current == null)
          TerrainMeshProviders._current = TerrainMeshProviders.Acquire();
        return TerrainMeshProviders._current;
      }
    }

    public static void Reset()
    {
      TerrainMeshProviders._current = (TerrainMeshProvider) null;
    }

    private static TerrainMeshProvider Acquire()
    {
      switch (Preferences.Current.RenderSettings.TerrainQuality)
      {
        case 0:
          return TerrainMeshProviders.Low;
        case 1:
          return TerrainMeshProviders.Medium;
        case 2:
          return TerrainMeshProviders.High;
        default:
          throw new InvalidOperationException();
      }
    }

    public static int[] GetTriangleOffsets(TerrainMeshProvider provider, bool foldLeftRight)
    {
      return provider.GetIndices(foldLeftRight);
    }
  }
}
