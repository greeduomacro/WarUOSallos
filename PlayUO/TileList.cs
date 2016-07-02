// Decompiled with JetBrains decompiler
// Type: PlayUO.TileList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class TileList
  {
    private static HuedTile[] m_Empty = new HuedTile[0];
    private HuedTile[] m_Tiles;
    private int m_Count;

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public TileList()
    {
      this.m_Tiles = new HuedTile[8];
      this.m_Count = 0;
    }

    public void Add(HuedTile tile)
    {
      if (this.m_Count + 1 > this.m_Tiles.Length)
      {
        HuedTile[] huedTileArray = this.m_Tiles;
        this.m_Tiles = new HuedTile[huedTileArray.Length * 2];
        for (int index = 0; index < huedTileArray.Length; ++index)
          this.m_Tiles[index] = huedTileArray[index];
      }
      this.m_Tiles[this.m_Count++] = tile;
    }

    public HuedTile[] ToArray()
    {
      if (this.m_Count == 0)
        return TileList.m_Empty;
      HuedTile[] huedTileArray = new HuedTile[this.m_Count];
      for (int index = 0; index < this.m_Count; ++index)
        huedTileArray[index] = this.m_Tiles[index];
      this.m_Count = 0;
      return huedTileArray;
    }
  }
}
