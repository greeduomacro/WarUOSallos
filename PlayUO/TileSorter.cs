// Decompiled with JetBrains decompiler
// Type: PlayUO.TileSorter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using Ultima.Data;

namespace PlayUO
{
  public class TileSorter : IComparer
  {
    private static Type tLandTile = typeof (LandTile);
    private static Type tDynamicItem = typeof (DynamicItem);
    private static Type tStaticItem = typeof (StaticItem);
    private static Type tMobileCell = typeof (MobileCell);
    public static readonly IComparer Comparer = (IComparer) new TileSorter();

    private TileSorter()
    {
    }

    public int Compare(object x, object y)
    {
      int z1;
      int treshold1;
      int type1;
      int tiebreaker1;
      this.GetStats(x, out z1, out treshold1, out type1, out tiebreaker1);
      int z2;
      int treshold2;
      int type2;
      int tiebreaker2;
      this.GetStats(y, out z2, out treshold2, out type2, out tiebreaker2);
      int num = z1 + treshold1 - (z2 + treshold2);
      if (num == 0)
        num = type1 - type2;
      if (num == 0)
        num = treshold1 - treshold2;
      if (num == 0)
        num = tiebreaker1 - tiebreaker2;
      return num;
    }

    public void GetStats(object obj, out int z, out int treshold, out int type, out int tiebreaker)
    {
      if (obj is MobileCell)
      {
        MobileCell mobileCell = (MobileCell) obj;
        z = (int) mobileCell.Z;
        treshold = 2;
        type = 3;
        if (mobileCell.m_Mobile.Player)
          tiebreaker = 1073741824;
        else
          tiebreaker = mobileCell.Serial;
      }
      else if (obj is LandTile)
      {
        LandTile landTile = (LandTile) obj;
        z = (int) landTile.SortZ;
        treshold = 0;
        type = 0;
        tiebreaker = 0;
      }
      else if (obj is DynamicItem)
      {
        DynamicItem dynamicItem = (DynamicItem) obj;
        z = (int) dynamicItem.Z;
        int num = !Map.m_ItemFlags[(int) dynamicItem.ID & 16383][(TileFlag) 1L] ? 1 : 0;
        treshold = (int) dynamicItem.Height == 0 ? num : num + 1;
        type = ((int) dynamicItem.ID & 16383) == 8198 ? 4 : 2;
        tiebreaker = dynamicItem.Serial;
      }
      else if (obj is StaticItem)
      {
        StaticItem staticItem = (StaticItem) obj;
        z = (int) staticItem.Z;
        int num = !Map.m_ItemFlags[(int) staticItem.ID & 16383][(TileFlag) 1L] ? 1 : 0;
        treshold = (int) staticItem.Height == 0 ? num : num + 1;
        type = 1;
        tiebreaker = staticItem.m_SortInfluence;
      }
      else
      {
        z = 0;
        treshold = 0;
        type = 0;
        tiebreaker = 0;
      }
    }
  }
}
