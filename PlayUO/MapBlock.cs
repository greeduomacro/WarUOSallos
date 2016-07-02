// Decompiled with JetBrains decompiler
// Type: PlayUO.MapBlock
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class MapBlock
  {
    public Tile[] m_LandTiles;
    public HuedTile[][][] m_StaticTiles;

    public MapBlock(Tile[] landTiles, HuedTile[][][] staticTiles)
    {
      this.m_LandTiles = landTiles;
      this.m_StaticTiles = staticTiles;
    }
  }
}
