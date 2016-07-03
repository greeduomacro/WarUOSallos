// Decompiled with JetBrains decompiler
// Type: PlayUO.TileMatrix
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;
using System.IO;
using Ultima.Data;
using Archive = Sallos.Archive;

namespace PlayUO
{
  public class TileMatrix
  {
    private WeakReference[][] m_Blocks;
    private Tile[] m_InvalidLandBlock;
    private HuedTile[][][] m_EmptyStaticBlock;
    private Stream m_Index;
    private BinaryReader m_IndexReader;
    private Stream m_Statics;
    private int m_BlockWidth;
    private int m_BlockHeight;
    private int m_Width;
    private int m_Height;
    private readonly int fileIndex;
    private readonly Archive archive;
    private static TileList[][] m_Lists;

    public int BlockWidth
    {
      get
      {
        return this.m_BlockWidth;
      }
    }

    public int BlockHeight
    {
      get
      {
        return this.m_BlockHeight;
      }
    }

    public int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public Tile[] InvalidLandBlock
    {
      get
      {
        return this.m_InvalidLandBlock;
      }
    }

    public HuedTile[][][] EmptyStaticBlock
    {
      get
      {
        return this.m_EmptyStaticBlock;
      }
    }

    public TileMatrix(int fileIndex, int mapID, int width, int height)
    {
      this.m_Width = width;
      this.m_Height = height;
      this.m_BlockWidth = width >> 3;
      this.m_BlockHeight = height >> 3;
      this.fileIndex = fileIndex;
      if (fileIndex != (int) sbyte.MaxValue)
      {
        this.m_Index = this.Open("staidx", fileIndex);
        if (this.m_Index != null)
          this.m_IndexReader = new BinaryReader(this.m_Index);
        this.m_Statics = this.Open("statics", fileIndex);
      }
      this.m_EmptyStaticBlock = new HuedTile[8][][];
      for (int index1 = 0; index1 < 8; ++index1)
      {
        this.m_EmptyStaticBlock[index1] = new HuedTile[8][];
        for (int index2 = 0; index2 < 8; ++index2)
          this.m_EmptyStaticBlock[index1][index2] = new HuedTile[0];
      }
      this.m_InvalidLandBlock = new Tile[196];
      this.m_Blocks = new WeakReference[this.m_BlockWidth][];
      this.archive = new Archive(Engine.FileManager.ResolveMUL("map" + (object) fileIndex + "LegacyMUL.uop"));
    }

    public bool CheckLoaded(int x, int y)
    {
      if (x >= 0 && x < this.m_BlockWidth && (y >= 0 && y < this.m_BlockHeight) && this.m_Blocks[x] != null)
        return this.m_Blocks[x][y] != null;
      return false;
    }

    private Stream Open(string name, int fileIndex)
    {
      string path2 = name + (object) fileIndex + ".mul";
      Stream stream = (Stream) null;
      if (!string.IsNullOrEmpty(Engine._ticket._contentArchive))
        stream = Archives.Download(Path.Combine(Engine._ticket._contentArchive, path2));
      if (stream == null)
      {
        string path = Path.Combine(Engine.FileManager.FilePath, path2);
        if (File.Exists(path))
          stream = (Stream) new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
      }
      return stream;
    }

    public MapBlock GetBlock(int x, int y)
    {
      if (x < 0 || y < 0 || (x >= this.m_BlockWidth || y >= this.m_BlockHeight))
        return (MapBlock) null;
      if (this.m_Blocks[x] == null)
        this.m_Blocks[x] = new WeakReference[this.m_BlockHeight];
      WeakReference weakReference = this.m_Blocks[x][y];
      MapBlock mapBlock;
      if (weakReference == null)
        this.m_Blocks[x][y] = new WeakReference((object) (mapBlock = this.LoadBlock(x, y)));
      else if (!weakReference.IsAlive)
        weakReference.Target = (object) (mapBlock = this.LoadBlock(x, y));
      else
        mapBlock = (MapBlock) weakReference.Target;
      return mapBlock;
    }

    public HuedTile[][][] GetStaticBlock(int x, int y)
    {
      if (x < 0 || y < 0 || (x >= this.m_BlockWidth || y >= this.m_BlockHeight) || (this.m_Statics == null || this.m_Index == null))
        return this.m_EmptyStaticBlock;
      return this.GetBlock(x, y).m_StaticTiles;
    }

    public HuedTile[] GetStaticTiles(int x, int y)
    {
      return this.GetStaticBlock(x >> 3, y >> 3)[x & 7][y & 7];
    }

    public Tile[] GetLandBlock(int x, int y)
    {
      if (x < 0 || y < 0 || (x >= this.m_BlockWidth || y >= this.m_BlockHeight))
        return this.m_InvalidLandBlock;
      return this.GetBlock(x, y).m_LandTiles;
    }

    public Tile GetLandTile(int x, int y)
    {
      return this.GetLandBlock(x >> 3, y >> 3)[((y & 7) << 3) + (x & 7)];
    }

    private MapBlock LoadBlock(int x, int y)
    {
      return new MapBlock(this.ReadLandBlock(x, y), this.ReadStaticBlock(x, y));
    }

    private unsafe HuedTile[][][] ReadStaticBlock(int x, int y)
    {
      BinaryReader binaryReader = this.m_IndexReader;
      Stream stream = this.m_Statics;
      if (binaryReader == null || stream == null)
        return this.m_EmptyStaticBlock;
      binaryReader.BaseStream.Seek((long) ((x * this.m_BlockHeight + y) * 12), SeekOrigin.Begin);
      int num = binaryReader.ReadInt32();
      int size = binaryReader.ReadInt32();
      if (num < 0 || size <= 0)
        return this.m_EmptyStaticBlock;
      int count = size / 7;
      FileStream file = stream as FileStream;
      if (file != null)
      {
        fixed (StaticTile* staticTiles = new StaticTile[count])
        {
          stream.Seek((long) num, SeekOrigin.Begin);
          UnsafeMethods.ReadFile(file, (void*) staticTiles, size);
          return this.GetTilesFromBlock(staticTiles, count);
        }
      }
      else
      {
        MemoryStream memoryStream = stream as MemoryStream;
        if (memoryStream == null)
          throw new NotSupportedException();
        fixed (byte* numPtr = memoryStream.GetBuffer())
          return this.GetTilesFromBlock((StaticTile*) (numPtr + num), count);
      }
    }

    private unsafe HuedTile[][][] GetTilesFromBlock(StaticTile* staticTiles, int count)
    {
      if (TileMatrix.m_Lists == null)
      {
        TileMatrix.m_Lists = new TileList[8][];
        for (int index1 = 0; index1 < 8; ++index1)
        {
          TileMatrix.m_Lists[index1] = new TileList[8];
          for (int index2 = 0; index2 < 8; ++index2)
            TileMatrix.m_Lists[index1][index2] = new TileList();
        }
      }
      TileList[][] tileListArray = TileMatrix.m_Lists;
      StaticTile* staticTilePtr1 = staticTiles;
      for (StaticTile* staticTilePtr2 = staticTiles + count; staticTilePtr1 < staticTilePtr2; ++staticTilePtr1)
        tileListArray[(int) staticTilePtr1->x & 7][(int) staticTilePtr1->y & 7].Add(new HuedTile(*staticTilePtr1));
      HuedTile[][][] huedTileArray = new HuedTile[8][][];
      for (int index1 = 0; index1 < 8; ++index1)
      {
        huedTileArray[index1] = new HuedTile[8][];
        for (int index2 = 0; index2 < 8; ++index2)
          huedTileArray[index1][index2] = tileListArray[index1][index2].ToArray();
      }
      return huedTileArray;
    }

    private unsafe Tile[] ReadLandBlock(int x, int y)
    {
      int num1 = x * this.m_BlockHeight + y;
      int num2 = num1 >> 12;
      int entry = num1 & 4095;
      return (Tile[]) this.archive.Open<Tile[]>(string.Format("build/map{0}legacymul/{1:00000000}.dat", (object) this.fileIndex, (object) num2), (Func<Stream, M0>) (data =>
      {
        Tile[] tileArray = new Tile[64];
        UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream) data;
        fixed (Tile* tilePtr = tileArray)
        {
          byte* positionPointer = unmanagedMemoryStream.PositionPointer;
          UnsafeMethods.CopyMemory((void*) tilePtr, (void*) (positionPointer + ((IntPtr) entry * 196).ToInt64() + 4), 192);
        }
        return tileArray;
      })) ?? new Tile[64];
    }

    public void Dispose()
    {
      if (this.m_Statics != null)
        this.m_Statics.Close();
      if (this.m_IndexReader == null)
        return;
      this.m_IndexReader.Close();
    }
  }
}
