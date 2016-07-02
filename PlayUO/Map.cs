// Decompiled with JetBrains decompiler
// Type: PlayUO.Map
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ultima.Client;
using Ultima.Data;

namespace PlayUO
{
  public class Map
  {
    private static int[] m_InvalidLandTiles = new int[1]{ 580 };
    private static Point3DList m_PathList = new Point3DList();
    private static int m_MaxLOSDistance = 18;
    private static Queue m_LockQueue = new Queue();
    private static Type tLandTile = typeof (LandTile);
    private static Type tDynamicItem = typeof (DynamicItem);
    private static Type tStaticItem = typeof (StaticItem);
    private static Type tMobileCell = typeof (MobileCell);
    private const string RelativeApplicationDataPath = "Sallos/Ultima Online/Cache/TileData";
    private const string RelativeLegacyPath = "data/ultima/cache/tiledata.uoi";
    private static Vector m_vLight;
    private static int m_X;
    private static int m_Y;
    public static int m_Width;
    public static int m_Height;
    private static int m_World;
    private static MapPackage m_Cached;
    private static bool m_IsCached;
    private static readonly TileData tileData;
    private static AnimData[] m_Anim;
    public static TileFlags[] m_ItemFlags;
    private static unsafe sbyte* m_pAnims;
    private static TileMatrix m_Felucca;
    private static TileMatrix m_Trammel;
    private static TileMatrix m_Ilshenar;
    private static TileMatrix m_Malas;
    private static TileMatrix m_Tokuno;
    private static bool m_Locked;
    private static bool m_QueueInvalidate;
    private static ArrayList[,] m_CellPool;
    private static byte[,] m_FlagPool;
    private static byte[,] m_IndexPool;
    private static LandTile[,] m_LandTiles;
    private static MapBlock[] m_StrongReferences;

    public static int[] InvalidLandTiles
    {
      get
      {
        return Map.m_InvalidLandTiles;
      }
      set
      {
        Map.m_InvalidLandTiles = value;
      }
    }

    public static int MaxLOSDistance
    {
      get
      {
        return Map.m_MaxLOSDistance;
      }
      set
      {
        Map.m_MaxLOSDistance = value;
      }
    }

    public static TileMatrix Felucca
    {
      get
      {
        if (Map.m_Felucca == null)
          Map.m_Felucca = new TileMatrix(0, 0, 7168, 4096);
        return Map.m_Felucca;
      }
    }

    public static TileMatrix Trammel
    {
      get
      {
        if (Map.m_Trammel == null)
          Map.m_Trammel = new TileMatrix(0, 1, 7168, 4096);
        return Map.m_Trammel;
      }
    }

    public static TileMatrix Ilshenar
    {
      get
      {
        if (Map.m_Ilshenar == null)
          Map.m_Ilshenar = new TileMatrix(2, 2, 2304, 1600);
        return Map.m_Ilshenar;
      }
    }

    public static TileMatrix Malas
    {
      get
      {
        if (Map.m_Malas == null)
          Map.m_Malas = new TileMatrix(3, 3, 2560, 2048);
        return Map.m_Malas;
      }
    }

    public static TileMatrix Tokuno
    {
      get
      {
        if (Map.m_Tokuno == null)
          Map.m_Tokuno = new TileMatrix(4, 4, 1448, 1448);
        return Map.m_Tokuno;
      }
    }

    static unsafe Map()
    {
      Debug.TimeBlock("Initializing Map");
      Map.m_pAnims = (sbyte*) Memory.Alloc(1048576);
      string cachePath = Map.GetCachePath();
      if (!File.Exists(cachePath))
      {
        string str = Engine.FileManager.BasePath("data/ultima/cache/tiledata.uoi");
        if (File.Exists(str))
        {
          try
          {
            File.Move(str, cachePath);
          }
          catch
          {
            File.Copy(str, cachePath, false);
          }
        }
      }
      Map.tileData = new TileData(Engine.FileManager.ResolveMUL(Files.Tiledata));
      Map.m_Anim = new AnimData[16384];
      int count = 1122304;
      byte[] buffer = new byte[count];
      Stream stream = Engine.FileManager.OpenMUL(Files.Animdata);
      stream.Read(buffer, 0, count);
      stream.Close();
      fixed (AnimData* animDataPtr1 = Map.m_Anim)
      {
        AnimData* animDataPtr2 = animDataPtr1;
        fixed (byte* numPtr1 = buffer)
        {
          byte* numPtr2 = numPtr1;
          int num1 = 0;
          sbyte* numPtr3 = Map.m_pAnims;
          while (num1++ < 2048)
          {
            numPtr2 += 4;
            int num2 = 0;
            while (num2++ < 8)
            {
              animDataPtr2->pvFrames = numPtr3;
              numPtr3 += 64;
              sbyte* numPtr4 = (sbyte*) numPtr2;
              for (int index = 0; index < 64; ++index)
                animDataPtr2->pvFrames[index] = *numPtr4++;
              byte* numPtr5 = numPtr2 + 64;
              AnimData* animDataPtr3 = animDataPtr2;
              byte* numPtr6 = numPtr5;
              IntPtr num3 = new IntPtr(1);
              byte* numPtr7 = numPtr6 + num3.ToInt64();
              int num4 = (int) *numPtr6;
              animDataPtr3->unknown = (byte) num4;
              AnimData* animDataPtr4 = animDataPtr2;
              byte* numPtr8 = numPtr7;
              IntPtr num5 = new IntPtr(1);
              byte* numPtr9 = numPtr8 + num5.ToInt64();
              int num6 = (int) *numPtr8;
              animDataPtr4->frameCount = (byte) num6;
              AnimData* animDataPtr5 = animDataPtr2;
              byte* numPtr10 = numPtr9;
              IntPtr num7 = new IntPtr(1);
              byte* numPtr11 = numPtr10 + num7.ToInt64();
              int num8 = (int) *numPtr10;
              animDataPtr5->frameInterval = (byte) num8;
              AnimData* animDataPtr6 = animDataPtr2;
              byte* numPtr12 = numPtr11;
              IntPtr num9 = new IntPtr(1);
              numPtr2 = numPtr12 + num9.ToInt64();
              int num10 = (int) *numPtr12;
              animDataPtr6->frameStartInterval = (byte) num10;
              ++animDataPtr2;
            }
          }
        }
      }
      Map.Patch();
      Map.m_ItemFlags = new TileFlags[65500];
      for (int itemId = 0; itemId < Map.m_ItemFlags.Length; ++itemId)
        Map.m_ItemFlags[itemId] = new TileFlags((TileFlag) Map.GetItemDataPointer(itemId)->Flags);
      Debug.EndBlock();
    }

    public static bool InRange(IPoint2D p)
    {
      if (p.X >= Map.m_Cached.CellX && p.X <= Map.m_Cached.CellX + Renderer.cellWidth && p.Y >= Map.m_Cached.CellY)
        return p.Y <= Map.m_Cached.CellY + Renderer.cellHeight;
      return false;
    }

    public static int GetZ(int x, int y, int w)
    {
      return (int) Map.GetMatrix(w).GetLandTile(x, y).z;
    }

    public static int FloorAverage(int a, int b)
    {
      int num = a + b;
      if (num < 0)
        --num;
      return num / 2;
    }

    public static void GetAverageZ(int x, int y, ref int z, ref int avg, ref int top)
    {
      int z1 = Map.GetZ(x, y, Engine.m_World);
      int z2 = Map.GetZ(x, y + 1, Engine.m_World);
      int z3 = Map.GetZ(x + 1, y, Engine.m_World);
      int z4 = Map.GetZ(x + 1, y + 1, Engine.m_World);
      z = z1;
      if (z2 < z)
        z = z2;
      if (z3 < z)
        z = z3;
      if (z4 < z)
        z = z4;
      top = z1;
      if (z2 > top)
        top = z2;
      if (z3 > top)
        top = z3;
      if (z4 > top)
        top = z4;
      if (Math.Abs(z1 - z4) > Math.Abs(z2 - z3))
        avg = Map.FloorAverage(z2, z3);
      else
        avg = Map.FloorAverage(z1, z4);
    }

    public static int GetAverageZ(int x, int y)
    {
      int z = 0;
      int avg = 0;
      int top = 0;
      Map.GetAverageZ(x, y, ref z, ref avg, ref top);
      return avg;
    }

    public static AnimData GetAnim(int ItemID)
    {
      return Map.m_Anim[ItemID & 16383];
    }

    [Obsolete("etc", false)]
    public static unsafe short GetTexture(int landId)
    {
      return (short) ((LandData) (IntPtr) Map.GetLandDataPointer(landId)).get_TextureId();
    }

    [Obsolete("etc", false)]
    private static unsafe LandData* GetLandDataPointer(int landId)
    {
      return Map.GetLandDataPointer((LandId) (int) (ushort) landId);
    }

    public static unsafe LandData* GetLandDataPointer(LandId landId)
    {
      return Map.tileData.GetLandDataPointer(landId);
    }

    [Obsolete("etc", false)]
    public static unsafe ItemData* GetItemDataPointer(int itemId)
    {
      return Map.GetItemDataPointer((ItemId) (int) (ushort) itemId);
    }

    public static unsafe ItemData* GetItemDataPointer(ItemId itemId)
    {
      return Map.tileData.GetItemDataPointer(itemId);
    }

    [Obsolete("etc", false)]
    public static unsafe TileFlags GetLandFlags(int landId)
    {
      return new TileFlags(((LandData) (IntPtr) Map.GetLandDataPointer(landId)).get_Flags());
    }

    [Obsolete("etc", false)]
    public static unsafe int GetWeight(int itemId)
    {
      return (int) Map.GetItemDataPointer(itemId)->Weight;
    }

    [Obsolete("etc", false)]
    public static TileFlags GetTileFlags(int TileID)
    {
      if (TileID >= 16384)
        return Map.m_ItemFlags[TileID & 16383];
      return Map.GetLandFlags(TileID);
    }

    public static unsafe byte GetItemHeight(ItemId itemId)
    {
      return (byte) Map.GetItemDataPointer(itemId)->Height;
    }

    [Obsolete("etc", false)]
    public static unsafe byte GetHeight(int tileId)
    {
      if (tileId >= 16384)
        return (byte) Map.GetItemDataPointer((ItemId) (int) (ushort) (tileId - 16384))->Height;
      return 0;
    }

    [Obsolete("Use GetQuality( ItemId ) instead.", false)]
    public static unsafe byte GetQuality(int tileId)
    {
      return (byte) Map.GetItemDataPointer((ItemId) (int) (ushort) (tileId & 16383))->quality_layer_light;
    }

    [Obsolete("Use GetAnimation( ItemId ) instead.", false)]
    public static unsafe short GetAnimation(int itemId)
    {
      return (short) Map.GetItemDataPointer(itemId)->AnimationId;
    }

    public static unsafe void Shutdown()
    {
      if ((IntPtr) Map.m_pAnims != IntPtr.Zero)
        Memory.Free((void*) Map.m_pAnims);
      for (int index = 0; index < 16384; ++index)
        Map.m_Anim[index].pvFrames = (sbyte*) null;
      Map.m_Anim = (AnimData[]) null;
      Map.m_CellPool = (ArrayList[,]) null;
      if (Map.tileData == null)
        return;
      Map.tileData.Dispose();
    }

    public static int GetDispID(int id, int amount, ref bool xDouble)
    {
      xDouble = amount > 1 && Map.m_ItemFlags[id][(TileFlag) 2048L];
      if (id >= 3818 && id <= 3826)
      {
        int num = (id - 3818) / 3 * 3 + 3818;
        xDouble = false;
        id = amount > 1 ? (amount > 5 ? num + 2 : num + 1) : num;
      }
      return id;
    }

    public static bool LineOfSight(object from, object dest)
    {
      if (from == dest)
        return true;
      return Map.LineOfSight(Map.GetPoint(from, true), Map.GetPoint(dest, false));
    }

    public static unsafe Point3D GetPoint(object o, bool eye)
    {
      Point3D point3D;
      if (o is Mobile)
      {
        Mobile mobile = (Mobile) o;
        point3D = new Point3D(mobile.X, mobile.Y, mobile.Z);
        point3D.Z += 14;
      }
      else if (o is Item)
      {
        Item obj = (Item) o;
        point3D = new Point3D(obj.X, obj.Y, obj.Z);
        point3D.Z += obj.ItemDataPointer->Height / 2 + 1;
      }
      else if (o is Point3D)
        point3D = (Point3D) o;
      else if (o is IPoint3D)
      {
        point3D = new Point3D((IPoint3D) o);
      }
      else
      {
        Console.WriteLine("Warning: Invalid object ({0}) in line of sight", o);
        point3D = new Point3D(0, 0, 0);
      }
      return point3D;
    }

    public static bool LineOfSight(Mobile from, Point3D target)
    {
      Point3D org = new Point3D(from.X, from.Y, from.Z);
      org.Z += 14;
      return Map.LineOfSight(org, target);
    }

    public static bool CanFit(int x, int y, int z, int height, bool checkMobiles, bool requireSurface)
    {
      MapPackage cache = Map.GetCache();
      int index1 = x - cache.CellX;
      int index2 = y - cache.CellY;
      if (index1 < 0 || index2 < 0 || (index1 >= Renderer.cellWidth || index2 >= Renderer.cellHeight))
        return false;
      bool flag1 = !requireSurface;
      ArrayList arrayList = cache.cells[index1, index2];
      for (int index3 = 0; index3 < arrayList.Count; ++index3)
      {
        object obj = arrayList[index3];
        if (obj is LandTile)
        {
          LandTile landTile = obj as LandTile;
          int z1 = 0;
          int avg = 0;
          int top = 0;
          Map.GetAverageZ(x, y, ref z1, ref avg, ref top);
          TileFlags landFlags = Map.GetLandFlags((int) landTile.ID & 16383);
          if (landFlags[(TileFlag) 64L] && avg > z && z + height > z1)
            return false;
          if (!landFlags[(TileFlag) 64L] && z == avg && !landTile.Ignored)
            flag1 = true;
        }
        else if (obj is StaticItem)
        {
          StaticItem staticItem = obj as StaticItem;
          TileFlags tileFlags = Map.m_ItemFlags[(int) staticItem.ID & 16383];
          bool flag2 = tileFlags[(TileFlag) 512L];
          bool flag3 = tileFlags[(TileFlag) 64L];
          if ((flag2 || flag3) && ((int) staticItem.Z + (int) staticItem.CalcHeight > z && z + height > (int) staticItem.Z))
            return false;
          if (flag2 && !flag3 && z == (int) staticItem.Z + (int) staticItem.CalcHeight)
            flag1 = true;
        }
        else if (obj is DynamicItem)
        {
          DynamicItem dynamicItem = obj as DynamicItem;
          TileFlags tileFlags = Map.m_ItemFlags[(int) dynamicItem.ID & 16383];
          bool flag2 = tileFlags[(TileFlag) 512L];
          bool flag3 = tileFlags[(TileFlag) 64L];
          if ((flag2 || flag3) && ((int) dynamicItem.Z + (int) dynamicItem.CalcHeight > z && z + height > (int) dynamicItem.Z))
            return false;
          if (flag2 && !flag3 && z == (int) dynamicItem.Z + (int) dynamicItem.CalcHeight)
            flag1 = true;
        }
        else if (checkMobiles && obj is MobileCell)
        {
          MobileCell mobileCell = obj as MobileCell;
          if ((int) mobileCell.Z + 16 > z && z + (int) mobileCell.Height > (int) mobileCell.Z)
            return false;
        }
      }
      return flag1;
    }

    public static bool LineOfSight(Mobile from, Mobile to)
    {
      if (from == to)
        return true;
      Point3D org = new Point3D(from.X, from.Y, from.Z);
      Point3D dest = new Point3D(to.X, to.Y, to.Z);
      org.Z += 14;
      dest.Z += 14;
      return Map.LineOfSight(org, dest);
    }

    public static bool NumberBetween(double num, int bound1, int bound2, double allowance)
    {
      if (bound1 > bound2)
      {
        int num1 = bound1;
        bound1 = bound2;
        bound2 = num1;
      }
      if (num < (double) bound2 + allowance)
        return num > (double) bound1 - allowance;
      return false;
    }

    public static void FixPoints(ref Point3D top, ref Point3D bottom)
    {
      if (bottom.X < top.X)
      {
        int num = top.X;
        top.X = bottom.X;
        bottom.X = num;
      }
      if (bottom.Y < top.Y)
      {
        int num = top.Y;
        top.Y = bottom.Y;
        bottom.Y = num;
      }
      if (bottom.Z >= top.Z)
        return;
      int num1 = top.Z;
      top.Z = bottom.Z;
      bottom.Z = num1;
    }

    public static unsafe bool LineOfSight(Point3D org, Point3D dest)
    {
      if (!World.InRange(org, dest, Map.m_MaxLOSDistance))
        return false;
      Point3D point3D1 = dest;
      if (org.X > dest.X || org.X == dest.X && org.Y > dest.Y || org.X == dest.X && org.Y == dest.Y && org.Z > dest.Z)
      {
        Point3D point3D2 = org;
        org = dest;
        dest = point3D2;
      }
      Point3DList point3Dlist = Map.m_PathList;
      if (org == dest)
        return true;
      if (point3Dlist.Count > 0)
        point3Dlist.Clear();
      int num1 = dest.X - org.X;
      int num2 = dest.Y - org.Y;
      int num3 = dest.Z - org.Z;
      double num4 = Math.Sqrt((double) (num1 * num1 + num2 * num2));
      double num5 = num3 == 0 ? num4 : Math.Sqrt(num4 * num4 + (double) (num3 * num3));
      double num6 = (double) num2 / num5;
      double num7 = (double) num1 / num5;
      double num8 = (double) num3 / num5;
      double num9 = (double) org.Y;
      double num10 = (double) org.Z;
      double num11 = (double) org.X;
      Point3D last;
      while (Map.NumberBetween(num11, dest.X, org.X, 0.5) && Map.NumberBetween(num9, dest.Y, org.Y, 0.5) && Map.NumberBetween(num10, dest.Z, org.Z, 0.5))
      {
        int x = (int) Math.Round(num11);
        int y = (int) Math.Round(num9);
        int z = (int) Math.Round(num10);
        if (point3Dlist.Count > 0)
        {
          last = point3Dlist.Last;
          if (last.X != x || last.Y != y || last.Z != z)
            point3Dlist.Add(x, y, z);
        }
        else
          point3Dlist.Add(x, y, z);
        num11 += num7;
        num9 += num6;
        num10 += num8;
      }
      if (point3Dlist.Count == 0)
        return true;
      last = point3Dlist.Last;
      if (last != dest)
        point3Dlist.Add(dest);
      Point3D top1 = org;
      Point3D bottom = dest;
      Map.FixPoints(ref top1, ref bottom);
      int count = point3Dlist.Count;
      MapPackage cache = Map.GetCache();
      for (int index1 = 0; index1 < count; ++index1)
      {
        Point3D point3D2 = point3Dlist[index1];
        int index2 = point3D2.X - cache.CellX;
        int index3 = point3D2.Y - cache.CellY;
        if (index2 < 0 || index3 < 0 || (index2 >= Renderer.cellWidth || index3 >= Renderer.cellHeight))
          return false;
        ArrayList arrayList = cache.cells[index2, index3];
        bool flag1 = false;
        bool flag2 = false;
        for (int index4 = 0; index4 < arrayList.Count; ++index4)
        {
          ICell cell = (ICell) arrayList[index4];
          if (cell is LandTile)
          {
            LandTile landTile = (LandTile) cell;
            for (int index5 = 0; index5 < Map.m_InvalidLandTiles.Length; ++index5)
            {
              if ((int) landTile.ID == Map.m_InvalidLandTiles[index5])
              {
                flag1 = true;
                break;
              }
            }
            int z = 0;
            int avg = 0;
            int top2 = 0;
            Map.GetAverageZ(point3D2.X, point3D2.Y, ref z, ref avg, ref top2);
            if (z <= point3D2.Z && top2 >= point3D2.Z && (point3D2.X != point3D1.X || point3D2.Y != point3D1.Y || (z > point3D1.Z || top2 < point3D1.Z)) && !landTile.Ignored)
              return false;
          }
          else if (cell is StaticItem)
          {
            flag2 = true;
            StaticItem staticItem = (StaticItem) cell;
            ItemData* itemDataPointer = staticItem.ItemDataPointer;
            TileFlag tileFlag = (TileFlag) itemDataPointer->Flags;
            int num12 = (int) itemDataPointer->Height;
            if ((tileFlag & 1024L) != 0L)
              num12 /= 2;
            if ((int) staticItem.m_Z <= point3D2.Z && (int) staticItem.m_Z + num12 >= point3D2.Z && (tileFlag & 12288L) != 0L && (point3D2.X != point3D1.X || point3D2.Y != point3D1.Y || ((int) staticItem.m_Z > point3D1.Z || (int) staticItem.m_Z + num12 < point3D1.Z)))
              return false;
          }
          else if (cell is DynamicItem)
          {
            flag2 = true;
            DynamicItem dynamicItem = (DynamicItem) cell;
            ItemData* itemDataPointer = Map.GetItemDataPointer(dynamicItem.ItemId);
            TileFlag tileFlag = (TileFlag) itemDataPointer->Flags;
            int num12 = (int) itemDataPointer->Height;
            if ((tileFlag & 1024L) != 0L)
              num12 /= 2;
            if ((int) dynamicItem.m_Z <= point3D2.Z && (int) dynamicItem.m_Z + num12 >= point3D2.Z && (tileFlag & 12288L) != 0L && (point3D2.X != point3D1.X || point3D2.Y != point3D1.Y || ((int) dynamicItem.m_Z > point3D1.Z || (int) dynamicItem.m_Z + num12 < point3D1.Z)))
              return false;
          }
        }
        if (flag1 && !flag2)
          return false;
      }
      return true;
    }

    private static string GetCachePath()
    {
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Sallos/Ultima Online/Cache/TileData");
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      return path;
    }

    private static void Patch(IEnumerable<int> source, System.Action<int> thunk)
    {
      foreach (int num in source)
        thunk(num);
    }

    private static IEnumerable<int> GetNoDrawItemIds()
    {
      yield return 1;
      for (int itemId = 8600; itemId <= 8612; ++itemId)
        yield return itemId;
      yield return 8636;
      yield return 22160;
    }

    private static unsafe System.Action<int> SetTileFlag(TileFlag flag)
    {
      return (System.Action<int>) (itemId =>
      {
        ItemData* itemDataPointer = Map.GetItemDataPointer((ItemId) (int) (ushort) itemId);
        // ISSUE: variable of the null type
        __Null local = itemDataPointer->Flags | flag;
        itemDataPointer->Flags = local;
      });
    }

    private static unsafe System.Action<int> ClearTileFlag(TileFlag flag)
    {
      return (System.Action<int>) (itemId =>
      {
        ItemData* itemDataPointer = Map.GetItemDataPointer((ItemId) (int) (ushort) itemId);
        // ISSUE: variable of the null type
        __Null local = itemDataPointer->Flags & ~flag;
        itemDataPointer->Flags = local;
      });
    }

    private static void Patch(System.Action<int> thunk, int itemId)
    {
      thunk(itemId);
    }

    private static void Patch(System.Action<int> thunk, IEnumerable<int> itemIds)
    {
      foreach (int itemId in itemIds)
        thunk(itemId);
    }

    private static void Patch()
    {
      Map.Patch(Map.SetTileFlag((TileFlag) 65536L), Map.GetNoDrawItemIds());
      Map.Patch(Map.ClearTileFlag((TileFlag) 65536L), 8198);
      Map.Patch(Map.SetTileFlag((TileFlag) 8388608L), Enumerable.Range(14612, 24));
      Map.Patch(Map.ClearTileFlag((TileFlag) 2048L), 3853);
    }

    public static TileMatrix GetMatrix(int world)
    {
      switch (world)
      {
        case 0:
          return Map.Felucca;
        case 1:
          return Map.Trammel;
        case 2:
          return Map.Ilshenar;
        case 3:
          return Map.Malas;
        case 4:
          return Map.Tokuno;
        default:
          return Map.Felucca;
      }
    }

    public static string ReplaceAmount(string Name, int Amount)
    {
      if (Name.IndexOf('%') == -1)
        return Name;
      Match match = Regex.Match(Name, "(?<1>[^%]*)%(?<2>[^%/]*)(?<3>/[^%]*)?%");
      if (Amount == 1)
        return match.Groups[1].Value + (match.Groups[3].Value.Length > 0 ? match.Groups[3].Value.Substring(1) : match.Groups[3].Value);
      if (match.Groups[2].Success)
        return match.Groups[1].Value + match.Groups[2].Value;
      return match.Groups[1].Value;
    }

    public static string GetTileProperName(int TileID)
    {
      string str = Map.ReplaceAmount(Map.GetTileName(TileID), 1);
      TileFlags tileFlags = Map.GetTileFlags(TileID);
      bool flag1 = tileFlags[(TileFlag) 16384L];
      bool flag2 = tileFlags[(TileFlag) 32768L];
      if (flag1 && flag2)
        return string.Format("the {0}", (object) str);
      if (flag1)
        return string.Format("a {0}", (object) str);
      if (flag2)
        return string.Format("an {0}", (object) str);
      return str;
    }

    public static unsafe string GetItemName(ItemId itemId)
    {
      return ((ItemData) (IntPtr) Map.GetItemDataPointer(itemId)).get_Name();
    }

    public static unsafe string GetLandName(LandId landId)
    {
      return ((LandData) (IntPtr) Map.GetLandDataPointer(landId)).get_Name();
    }

    [Obsolete("don't use me", false)]
    public static string GetTileName(int tileId)
    {
      if (tileId < 16384)
        return Map.GetLandName((LandId) (int) (ushort) tileId);
      return Map.GetItemName((ItemId) (int) (ushort) (tileId - 16384));
    }

    public static bool IsValid(int X, int Y)
    {
      if (X >= 0 && X < Map.m_Width << 3 && Y >= 0)
        return Y < Map.m_Height << 3;
      return false;
    }

    public static void Lock()
    {
      Map.m_Locked = true;
    }

    public static void Unlock()
    {
      Map.m_Locked = false;
      while (Map.m_LockQueue.Count > 0)
        ((ILocked) Map.m_LockQueue.Dequeue()).Invoke();
    }

    public static void Sort(int X, int Y)
    {
      if (Map.m_Locked)
      {
        Map.m_LockQueue.Enqueue((object) new SortLock(X, Y));
      }
      else
      {
        ArrayList[,] arrayListArray = Map.m_Cached.cells;
        if (arrayListArray == null)
          return;
        ArrayList arrayList = arrayListArray[X, Y];
        if (arrayList.Count <= 1)
          return;
        arrayList.Sort(TileSorter.Comparer);
      }
    }

    private static ArrayList GetList(int x, int y)
    {
      int num1 = Map.m_X << 3;
      int num2 = Map.m_Y << 3;
      x -= num1;
      y -= num2;
      if (Map.IsValid(x, y))
      {
        ArrayList[,] arrayListArray = Map.m_Cached.cells;
        if (arrayListArray != null)
          return arrayListArray[x, y];
      }
      return (ArrayList) null;
    }

    public static void Update(PhysicalAgent agent)
    {
      if (!Map.m_Locked)
      {
        AgentCell agentCell = agent.AcquireViewportCell();
        agentCell.Update();
        ArrayList owner = agentCell.Owner;
        ArrayList arrayList = agent.InWorld ? Map.GetList(agent.X, agent.Y) : (ArrayList) null;
        if (arrayList != owner)
        {
          if (owner != null)
            owner.Remove((object) agentCell);
          if (arrayList != null)
          {
            int index = arrayList.BinarySearch((object) agentCell, TileSorter.Comparer);
            if (index < 0)
              index = ~index;
            arrayList.Insert(index, (object) agentCell);
          }
          agentCell.Owner = arrayList;
        }
        else
        {
          if (arrayList == null)
            return;
          arrayList.Sort(TileSorter.Comparer);
        }
      }
      else
        Map.m_LockQueue.Enqueue((object) new UpdateAgentLock(agent));
    }

    public static void QueueInvalidate()
    {
      Map.m_QueueInvalidate = true;
    }

    public static void Invalidate()
    {
      Map.m_IsCached = false;
      Engine.Redraw();
    }

    public static MapPackage GetCache()
    {
      return Map.m_Cached;
    }

    public static MapPackage GetMap(int X, int Y, int W, int H)
    {
      if (Map.m_X == X && Map.m_Y == Y && (Map.m_Width == W && Map.m_Height == H) && (Map.m_World == Engine.m_World && Map.m_IsCached && !Map.m_QueueInvalidate))
        return Map.m_Cached;
      Map.m_QueueInvalidate = false;
      if (Map.m_Cached.cells != null)
      {
        int length1 = Map.m_Cached.cells.GetLength(0);
        int length2 = Map.m_Cached.cells.GetLength(1);
        for (int index1 = 0; index1 < length1; ++index1)
        {
          for (int index2 = 0; index2 < length2; ++index2)
          {
            ArrayList arrayList = Map.m_Cached.cells[index1, index2];
            if (arrayList != null)
            {
              int count = arrayList.Count;
              for (int index3 = 0; index3 < count; ++index3)
                ((IDisposable) arrayList[index3]).Dispose();
            }
          }
        }
      }
      Map.m_X = X;
      Map.m_Y = Y;
      Map.m_Width = W;
      Map.m_Height = H;
      Map.m_World = Engine.m_World;
      if (Map.m_StrongReferences == null)
        Map.m_StrongReferences = new MapBlock[W * H];
      int length3 = W << 3;
      int length4 = H << 3;
      if (Map.m_CellPool == null)
      {
        Map.m_CellPool = new ArrayList[length3, length4];
        for (int index1 = 0; index1 < length3; ++index1)
        {
          for (int index2 = 0; index2 < length4; ++index2)
            Map.m_CellPool[index1, index2] = new ArrayList(4);
        }
      }
      else
      {
        for (int index1 = 0; index1 < length3; ++index1)
        {
          for (int index2 = 0; index2 < length4; ++index2)
          {
            ArrayList arrayList = Map.m_CellPool[index1, index2];
            for (int index3 = 0; index3 < arrayList.Count; ++index3)
            {
              AgentCell agentCell = arrayList[index3] as AgentCell;
              if (agentCell != null)
                agentCell.Owner = (ArrayList) null;
            }
            arrayList.Clear();
          }
        }
      }
      if (Map.m_LandTiles == null)
      {
        Map.m_LandTiles = new LandTile[length3, length4];
        for (int index1 = 0; index1 < length3; ++index1)
        {
          for (int index2 = 0; index2 < length4; ++index2)
          {
            Map.m_LandTiles[index1, index2] = new LandTile();
            Map.m_LandTiles[index1, index2].x = index1;
            Map.m_LandTiles[index1, index2].y = index2;
          }
        }
      }
      if (Map.m_IndexPool == null)
        Map.m_IndexPool = new byte[length3, length4];
      if (Map.m_FlagPool == null)
        Map.m_FlagPool = new byte[length3, length4];
      ArrayList[,] arrayListArray = Map.m_CellPool;
      IComparer comparer = TileSorter.Comparer;
      Engine.Multis.Update(new MapPackage()
      {
        cells = arrayListArray,
        CellX = X << 3,
        CellY = Y << 3
      });
      int num1 = Engine.m_World;
      TileMatrix matrix = Map.GetMatrix(num1);
      Viewport viewport = World.Viewport;
      int num2 = 0;
      int x1 = X;
      while (num2 < W)
      {
        int num3 = 0;
        int y1 = Y;
        while (num3 < H)
        {
          MapBlock block = matrix.GetBlock(x1, y1);
          Map.m_StrongReferences[num3 * W + num2] = block;
          HuedTile[][][] huedTileArray1 = block == null ? matrix.EmptyStaticBlock : block.m_StaticTiles;
          if (block != null)
          {
            Tile[] tileArray = block.m_LandTiles;
          }
          else
          {
            Tile[] invalidLandBlock = matrix.InvalidLandBlock;
          }
          int index1 = 0;
          int x2 = x1 << 3;
          int index2 = num2 << 3;
          while (index1 < 8)
          {
            int index3 = 0;
            int y2 = y1 << 3;
            int index4 = num3 << 3;
            while (index3 < 8)
            {
              HuedTile[] huedTileArray2 = huedTileArray1[index1][index3];
              for (int influence = 0; influence < huedTileArray2.Length; ++influence)
                arrayListArray[index2, index4].Add((object) StaticItem.Instantiate(huedTileArray2[influence], influence, x2 * matrix.Height + y2 | influence << 25));
              LandTile landTile = viewport.GetLandTile(x2, y2, num1);
              landTile.x = index2;
              landTile.y = index4;
              arrayListArray[index2, index4].Add((object) landTile);
              ++index3;
              ++y2;
              ++index4;
            }
            ++index1;
            ++x2;
            ++index2;
          }
          ++num3;
          ++y1;
        }
        ++num2;
        ++x1;
      }
      int num4 = X << 3;
      int num5 = Y << 3;
      foreach (Item obj in World.Items.Values)
      {
        if (obj.InWorld && obj.Visible && !obj.IsMulti)
        {
          int index1 = obj.X - num4;
          int index2 = obj.Y - num5;
          if (index1 >= 0 && index1 < length3 && (index2 >= 0 && index2 < length4))
          {
            AgentCell agentCell = obj.AcquireViewportCell();
            arrayListArray[index1, index2].Add((object) agentCell);
            agentCell.Owner = arrayListArray[index1, index2];
          }
        }
      }
      foreach (Mobile mobile in World.Mobiles.Values)
      {
        if (mobile.InWorld && mobile.Visible)
        {
          int index1 = mobile.X - num4;
          int index2 = mobile.Y - num5;
          if (index1 >= 0 && index1 < length3 && (index2 >= 0 && index2 < length4))
          {
            AgentCell agentCell = mobile.AcquireViewportCell();
            arrayListArray[index1, index2].Add((object) agentCell);
            agentCell.Owner = arrayListArray[index1, index2];
          }
        }
      }
      for (int index1 = 0; index1 < length3; ++index1)
      {
        for (int index2 = 0; index2 < length4; ++index2)
        {
          ArrayList arrayList = arrayListArray[index1, index2];
          if (arrayList.Count > 1)
            arrayList.Sort(comparer);
        }
      }
      MapPackage mapPackage = new MapPackage();
      mapPackage.cells = arrayListArray;
      mapPackage.CellX = X << 3;
      mapPackage.CellY = Y << 3;
      Map.m_Cached = mapPackage;
      Map.m_IsCached = true;
      for (int index = -1; index <= H; ++index)
        Engine.QueueMapLoad(X - 1, Y + index, matrix);
      for (int index = 0; index < W; ++index)
      {
        Engine.QueueMapLoad(X + index, Y - 1, matrix);
        Engine.QueueMapLoad(X + index, Y + H, matrix);
      }
      for (int index = -1; index <= H; ++index)
        Engine.QueueMapLoad(X + W, Y + index, matrix);
      return mapPackage;
    }
  }
}
