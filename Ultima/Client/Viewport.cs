// Decompiled with JetBrains decompiler
// Type: Ultima.Client.Viewport
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO;

namespace Ultima.Client
{
  public sealed class Viewport
  {
    private const int Size = 128;
    private const int Mask = 127;
    private readonly LandTile[] land;
    private int revision;

    public Viewport()
    {
      this.land = new LandTile[16384];
    }

    public void Invalidate()
    {
      ++this.revision;
    }

    private static int GetSlot(int x, int y)
    {
      return (y & (int) sbyte.MaxValue) * 128 + (x & (int) sbyte.MaxValue);
    }

    public LandTile GetLandTile(int x, int y, int facet)
    {
      int slot = Viewport.GetSlot(x, y);
      LandTile landTile = this.land[slot];
      if (landTile == null || landTile.ax != x || (landTile.ay != y || landTile.facet != facet) || landTile.rev != this.revision)
      {
        this.land[slot] = landTile = new LandTile();
        landTile.Prepare(facet, x, y);
        landTile.rev = this.revision;
      }
      return landTile;
    }

    public bool IsGuarded(int facet, int x, int y)
    {
      LandTile landTile = this.land[Viewport.GetSlot(x, y)];
      if (landTile != null && landTile.x == x && (landTile.y == y && landTile.facet == facet))
        return landTile.m_Guarded;
      int averageZ = Map.GetAverageZ(x, y);
      return Region.Find(Region.GuardedRegions, x, y, averageZ, facet) != null;
    }
  }
}
