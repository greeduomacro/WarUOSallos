// Decompiled with JetBrains decompiler
// Type: PlayUO.Walking
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public static class Walking
  {
    private static float m_Speed = 0.4f;
    private const float DefaultSpeed = 0.4f;
    private const int PersonHeight = 16;
    private const int StepHeight = 2;
    private static DateTime m_LastLiftBlocker;
    private static bool m_Diag;

    public static float Speed
    {
      get
      {
        if (Walking.IsDefaultSpeed)
          return 0.4f;
        return Walking.m_Speed;
      }
      set
      {
        Walking.m_Speed = value;
      }
    }

    public static float WalkSpeed
    {
      get
      {
        return Walking.Speed;
      }
      set
      {
        Walking.m_Speed = value;
      }
    }

    public static float RunSpeed
    {
      get
      {
        return Walking.Speed / 2f;
      }
      set
      {
        Walking.m_Speed = value * 2f;
      }
    }

    public static bool IsDefaultSpeed
    {
      get
      {
        if (!Engine.GMPrivs)
          return true;
        return (double) Walking.m_Speed == 0.400000005960464;
      }
    }

    private static bool IsOk(bool ignoreMobs, bool ignoreDoors, int ourZ, int ourTop, ArrayList tiles)
    {
      for (int index = 0; index < tiles.Count; ++index)
      {
        ICell cell = (ICell) tiles[index];
        if (cell is StaticItem)
        {
          StaticItem staticItem = (StaticItem) cell;
          if (Map.m_ItemFlags[(int) staticItem.m_RealID][(TileFlag) 576L])
          {
            int num = (int) staticItem.m_Z;
            if (num + (int) staticItem.CalcHeight > ourZ && ourTop > num)
              return false;
          }
        }
        else if (cell is DynamicItem)
        {
          Item obj = ((DynamicItem) cell).m_Item;
          TileFlags tileFlags = Map.m_ItemFlags[obj.ID];
          if (tileFlags[(TileFlag) 576L] && (!obj.IsDoor || !ignoreDoors && Walking.m_Diag))
          {
            int z = obj.Z;
            int num = z;
            if ((!tileFlags[(TileFlag) 1024L] ? num + obj.Height : num + obj.Height / 2) > ourZ && ourTop > z)
            {
              if (!obj.IsMovable || (Control.ModifierKeys & Keys.Shift) == Keys.None || !(Walking.m_LastLiftBlocker + TimeSpan.FromSeconds(0.6) < DateTime.Now))
                return false;
              Walking.m_LastLiftBlocker = DateTime.Now;
              Network.Send((Packet) new PPickupItem(obj, obj.Amount));
              Network.Send((Packet) new PDropItem(obj.Serial, -1, -1, 0, World.Serial));
            }
          }
        }
        else if (!ignoreMobs && cell is MobileCell)
        {
          Mobile mobile = ((MobileCell) cell).m_Mobile;
          if (!mobile.Ghost && !mobile.IsDeadPet)
          {
            int z = mobile.Z;
            if (z + 16 > ourZ && ourTop > z)
              return false;
          }
        }
      }
      return true;
    }

    public static bool CheckMovement(int xStart, int yStart, int zStart, int dir, out int zNew)
    {
      Mobile player = World.Player;
      if (player == null)
      {
        zNew = 0;
        return false;
      }
      int x = xStart;
      int y = yStart;
      Walking.Offset(dir, ref x, ref y);
      MapPackage cache = Map.GetCache();
      int X = x - cache.CellX;
      int Y = y - cache.CellY;
      if (!Map.IsValid(X, Y))
      {
        zNew = 0;
        return false;
      }
      ArrayList tiles = cache.cells[X, Y];
      LandTile landTile1 = World.Viewport.GetLandTile(x, y, Engine.m_World);
      LandTile landTile2 = World.Viewport.GetLandTile(xStart, yStart, Engine.m_World);
      try
      {
        if (player.Notoriety == Notoriety.Murderer)
        {
          if (landTile1.m_Guarded)
          {
            if (!Options.Current.SiegeRuleset)
            {
              if (!landTile2.m_Guarded)
              {
                if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) != (Keys.Shift | Keys.Control))
                {
                  zNew = 0;
                  return false;
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      bool impassable = landTile1.Impassable;
      bool flag1 = (int) landTile1.m_ID != 2 && (int) landTile1.m_ID != 475 && ((int) landTile1.m_ID < 430 || (int) landTile1.m_ID > 437);
      int z1 = 0;
      int avg = 0;
      int top = 0;
      Map.GetAverageZ(x, y, ref z1, ref avg, ref top);
      int zLow;
      int zTop;
      Walking.GetStartZ(xStart, yStart, zStart, out zLow, out zTop);
      zNew = zLow;
      bool flag2 = false;
      int num1 = zTop + 2;
      int num2 = zLow + 16;
      bool ignoreDoors = player.Ghost || (int) player.Body == 987;
      bool ignoreMobs = ignoreDoors || player.CurrentStamina == player.MaximumStamina || Engine.m_World != 0;
      if (Engine.m_Stealth)
        ignoreMobs = false;
      for (int index = 0; index < tiles.Count; ++index)
      {
        ICell cell = (ICell) tiles[index];
        if (cell is StaticItem)
        {
          StaticItem staticItem = (StaticItem) cell;
          TileFlags tileFlags = Map.m_ItemFlags[(int) staticItem.m_RealID];
          if (tileFlags[(TileFlag) 512L] && !tileFlags[(TileFlag) 64L])
          {
            int num3 = (int) staticItem.m_Z;
            int num4 = num3;
            int ourZ = num3 + (int) staticItem.CalcHeight;
            int ourTop = num2;
            if (flag2)
            {
              int num5 = Math.Abs(ourZ - player.Z) - Math.Abs(zNew - player.Z);
              if (num5 > 0 || num5 == 0 && ourZ > zNew)
                continue;
            }
            if (ourZ + 16 > ourTop)
              ourTop = ourZ + 16;
            if (!tileFlags[(TileFlag) 1024L])
              num4 += (int) staticItem.Height;
            if (num1 >= num4)
            {
              int num5 = num3;
              int num6 = (int) staticItem.Height < 2 ? num5 + (int) staticItem.Height : num5 + 2;
              if ((!flag1 || num6 >= avg || (avg <= ourZ || ourTop <= z1)) && Walking.IsOk(ignoreMobs, ignoreDoors, ourZ, ourTop, tiles))
              {
                zNew = ourZ;
                flag2 = true;
              }
            }
          }
        }
        else if (cell is DynamicItem)
        {
          Item obj = ((DynamicItem) cell).m_Item;
          TileFlags tileFlags = Map.m_ItemFlags[obj.ID];
          if (tileFlags[(TileFlag) 512L] && !tileFlags[(TileFlag) 64L])
          {
            int z2 = obj.Z;
            int num3 = z2;
            int num4 = z2;
            int height = obj.Height;
            int ourZ = !tileFlags[(TileFlag) 1024L] ? num4 + height : num4 + height / 2;
            if (flag2)
            {
              int num5 = Math.Abs(ourZ - player.Z) - Math.Abs(zNew - player.Z);
              if (num5 > 0 || num5 == 0 && ourZ > zNew)
                continue;
            }
            int ourTop = num2;
            if (ourZ + 16 > ourTop)
              ourTop = ourZ + 16;
            if (!tileFlags[(TileFlag) 1024L])
              num3 += height;
            if (num1 >= num3)
            {
              int num5 = z2;
              int num6 = height < 2 ? num5 + height : num5 + 2;
              if ((!flag1 || num6 >= avg || (avg <= ourZ || ourTop <= z1)) && Walking.IsOk(ignoreMobs, ignoreDoors, ourZ, ourTop, tiles))
              {
                zNew = ourZ;
                flag2 = true;
              }
            }
          }
        }
      }
      if (flag1 && !impassable && num1 >= z1)
      {
        int ourZ = avg;
        int ourTop = num2;
        if (ourZ + 16 > ourTop)
          ourTop = ourZ + 16;
        bool flag3 = true;
        if (flag2)
        {
          int num3 = Math.Abs(ourZ - player.Z) - Math.Abs(zNew - player.Z);
          if (num3 > 0 || num3 == 0 && ourZ > zNew)
            flag3 = false;
        }
        if (flag3 && Walking.IsOk(ignoreMobs, ignoreDoors, ourZ, ourTop, tiles))
        {
          zNew = ourZ;
          flag2 = true;
        }
      }
      return flag2;
    }

    private static unsafe void GetStartZ(int xStart, int yStart, int zStart, out int zLow, out int zTop)
    {
      MapPackage cache = Map.GetCache();
      int X = xStart - cache.CellX;
      int Y = yStart - cache.CellY;
      if (!Map.IsValid(X, Y))
      {
        zLow = zStart;
        zTop = zStart;
      }
      else
      {
        LandTile landTile = World.Viewport.GetLandTile(xStart, yStart, Engine.m_World);
        ArrayList arrayList = cache.cells[X, Y];
        bool impassable = landTile.Impassable;
        bool flag1 = (int) landTile.m_ID != 2 && (int) landTile.m_ID != 475 && ((int) landTile.m_ID < 430 || (int) landTile.m_ID > 437);
        int z = 0;
        int avg = 0;
        int top = 0;
        Map.GetAverageZ(xStart, yStart, ref z, ref avg, ref top);
        int num1 = zLow = zTop = 0;
        bool flag2 = false;
        if (flag1 && !impassable && zStart >= avg && (!flag2 || avg >= num1))
        {
          zLow = z;
          num1 = avg;
          if (!flag2 || top > zTop)
            zTop = top;
          flag2 = true;
        }
        for (int index = 0; index < arrayList.Count; ++index)
        {
          IItem obj = arrayList[index] as IItem;
          if (obj != null)
          {
            ItemData* itemDataPointer = Map.GetItemDataPointer(obj.ItemId);
            TileFlag tileFlag = (TileFlag) itemDataPointer->Flags;
            if ((tileFlag & 512L) != 0L)
            {
              bool flag3 = (tileFlag & 1024L) != 0L;
              byte num2 = (byte) itemDataPointer->Height;
              int num3 = (int) obj.Z;
              int num4 = num3 + (flag3 ? (int) num2 / 2 : (int) num2);
              int num5 = num3 + itemDataPointer->Height;
              if (zStart >= num4 && (!flag2 || num4 >= num1))
              {
                num1 = num4;
                if (!flag2 || num5 > zTop)
                  zTop = num5;
                zLow = num3;
                flag2 = true;
              }
            }
          }
        }
        if (!flag2)
        {
          zLow = zTop = zStart;
        }
        else
        {
          if (zStart <= zTop)
            return;
          zTop = zStart;
        }
      }
    }

    public static bool Calculate(int x, int y, int z, Direction dir, out int newZ, out int newDir)
    {
      int dir1 = (int) Engine.GetWalkDirection(dir);
      newZ = z;
      newDir = dir1;
      if (!Walking.IsDiagonal(dir1))
      {
        int dir2 = Walking.Turn(dir1, 1);
        int dir3 = Walking.Turn(dir1, -1);
        Walking.m_Diag = true;
        int zNew1;
        bool flag1 = Walking.CheckMovement(x, y, z, dir2, out zNew1);
        int zNew2;
        bool flag2 = Walking.CheckMovement(x, y, z, dir3, out zNew2);
        Walking.m_Diag = false;
        int zNew3;
        if (Walking.CheckMovement(x, y, z, dir1, out zNew3) && ((int) World.Player.Body == 987 ? (flag1 ? 1 : (flag2 ? 1 : 0)) : (!flag1 ? 0 : (flag2 ? 1 : 0))) != 0)
          newZ = zNew3;
        else if (flag1)
        {
          newZ = zNew1;
          newDir = dir2;
        }
        else
        {
          if (!flag2)
            return false;
          newZ = zNew2;
          newDir = dir3;
        }
        return true;
      }
      return Walking.CheckMovement(x, y, z, dir1, out newZ);
    }

    public static bool IsDiagonal(int dir)
    {
      return (dir & 1) == 0;
    }

    public static int Turn(int dir, int offset)
    {
      return (dir & 7) + offset & 7 | dir & 128;
    }

    public static void Offset(int dir, ref int x, ref int y)
    {
      switch (dir & 7)
      {
        case 0:
          --y;
          break;
        case 1:
          ++x;
          --y;
          break;
        case 2:
          ++x;
          break;
        case 3:
          ++x;
          ++y;
          break;
        case 4:
          ++y;
          break;
        case 5:
          --x;
          ++y;
          break;
        case 6:
          --x;
          break;
        case 7:
          --x;
          --y;
          break;
      }
    }
  }
}
