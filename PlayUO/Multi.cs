// Decompiled with JetBrains decompiler
// Type: PlayUO.Multi
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using Ultima.Data;

namespace PlayUO
{
  public class Multi
  {
    private int m_MultiID;
    private int m_xMin;
    private int m_yMin;
    private int m_xMax;
    private int m_yMax;
    private ArrayList m_List;
    private short[][] m_Radar;
    private sbyte[][] m_Inside;
    private sbyte[][] m_RunUO_Inside;

    public sbyte[][] Inside
    {
      get
      {
        return this.m_Inside;
      }
    }

    public sbyte[][] RunUO_Inside
    {
      get
      {
        return this.m_RunUO_Inside;
      }
    }

    public short[][] Radar
    {
      get
      {
        return this.m_Radar;
      }
    }

    public ArrayList List
    {
      get
      {
        return this.m_List;
      }
    }

    public int MultiID
    {
      get
      {
        return this.m_MultiID;
      }
    }

    public Multi(ArrayList list)
    {
      this.m_MultiID = 0;
      int count = list.Count;
      int index = 0;
      this.m_xMin = 1000;
      this.m_yMin = 1000;
      this.m_xMax = -1000;
      this.m_yMax = -1000;
      for (; index < count; ++index)
      {
        MultiItem multiItem = (MultiItem) list[index];
        if ((int) multiItem.X < this.m_xMin)
          this.m_xMin = (int) multiItem.X;
        if ((int) multiItem.Y < this.m_yMin)
          this.m_yMin = (int) multiItem.Y;
        if ((int) multiItem.X > this.m_xMax)
          this.m_xMax = (int) multiItem.X;
        if ((int) multiItem.Y > this.m_yMax)
          this.m_yMax = (int) multiItem.Y;
      }
      this.m_List = list;
      this.UpdateRadar();
    }

    public Multi(int MultiID)
    {
      this.m_MultiID = MultiID;
      ArrayList arrayList = Engine.Multis.Load(MultiID);
      int count = arrayList.Count;
      int index = 0;
      this.m_xMin = 1000;
      this.m_yMin = 1000;
      this.m_xMax = -1000;
      this.m_yMax = -1000;
      for (; index < count; ++index)
      {
        MultiItem multiItem = (MultiItem) arrayList[index];
        if ((int) multiItem.X < this.m_xMin)
          this.m_xMin = (int) multiItem.X;
        if ((int) multiItem.Y < this.m_yMin)
          this.m_yMin = (int) multiItem.Y;
        if ((int) multiItem.X > this.m_xMax)
          this.m_xMax = (int) multiItem.X;
        if ((int) multiItem.Y > this.m_yMax)
          this.m_yMax = (int) multiItem.Y;
      }
      this.m_List = arrayList;
      this.UpdateRadar();
    }

    public void UpdateRadar()
    {
      int length1 = this.m_xMax - this.m_xMin + 1;
      int length2 = this.m_yMax - this.m_yMin + 1;
      if (length1 <= 0 || length2 <= 0)
        return;
      int[][] numArray1 = new int[length2][];
      int[][] numArray2 = new int[length2][];
      this.m_Inside = new sbyte[length2][];
      this.m_RunUO_Inside = new sbyte[length2][];
      this.m_Radar = new short[length2][];
      for (int index1 = 0; index1 < length2; ++index1)
      {
        this.m_Radar[index1] = new short[length1];
        this.m_Inside[index1] = new sbyte[length1];
        this.m_RunUO_Inside[index1] = new sbyte[length1];
        numArray1[index1] = new int[length1];
        numArray2[index1] = new int[length1];
        for (int index2 = 0; index2 < length1; ++index2)
          numArray1[index1][index2] = int.MinValue;
        for (int index2 = 0; index2 < length1; ++index2)
          numArray2[index1][index2] = int.MinValue;
        for (int index2 = 0; index2 < length1; ++index2)
          this.m_Inside[index1][index2] = sbyte.MaxValue;
        for (int index2 = 0; index2 < length1; ++index2)
          this.m_RunUO_Inside[index1][index2] = sbyte.MaxValue;
      }
      for (int index1 = 0; index1 < this.m_List.Count; ++index1)
      {
        MultiItem multiItem = (MultiItem) this.m_List[index1];
        int index2 = (int) multiItem.X - this.m_xMin;
        int index3 = (int) multiItem.Y - this.m_yMin;
        if (index2 >= 0 && index2 < length1 && (index3 >= 0 && index3 < length2))
        {
          int num1 = (int) multiItem.Z;
          int num2 = num1 + (int) Map.GetHeight((int) multiItem.ItemID);
          int num3 = numArray2[index3][index2];
          int num4 = numArray1[index3][index2];
          int num5 = (int) multiItem.ItemID;
          if ((num2 > num4 || num2 == num4 && num1 > num3) && (num5 != 1 && num5 != 6038 && (num5 != 8612 && num5 != 8600)) && (num5 != 8636 && num5 != 8601))
          {
            this.m_Radar[index3][index2] = multiItem.ItemID;
            numArray2[index3][index2] = num1;
            numArray1[index3][index2] = num2;
          }
          if (!Map.GetTileFlags((int) multiItem.ItemID)[(TileFlag) 268435456L])
          {
            int num6 = (int) multiItem.ItemID & 16383;
            sbyte num7 = (sbyte) multiItem.Z;
            if ((int) num7 < (int) this.m_Inside[index3][index2])
              this.m_Inside[index3][index2] = num7;
            if ((num6 < 2965 || num6 > 3086) && (num6 < 3139 || num6 > 3140) && (int) num7 < (int) this.m_RunUO_Inside[index3][index2])
              this.m_RunUO_Inside[index3][index2] = num7;
          }
        }
      }
    }

    public void GetBounds(out int xMin, out int yMin, out int xMax, out int yMax)
    {
      xMin = this.m_xMin;
      yMin = this.m_yMin;
      xMax = this.m_xMax;
      yMax = this.m_yMax;
    }

    public bool Remove(int x, int y, int z, int itemID)
    {
      if (x < this.m_xMin || y < this.m_yMin || (x > this.m_xMax || y > this.m_yMax))
        return false;
      bool flag = false;
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        MultiItem multiItem = (MultiItem) this.m_List[index];
        if ((int) multiItem.X == x && (int) multiItem.Y == y && ((int) multiItem.Z == z && (int) multiItem.ItemID == itemID))
        {
          this.m_List.RemoveAt(index--);
          flag = true;
        }
      }
      return flag;
    }

    public void Add(int itemID, int x, int y, int z)
    {
      if (x < this.m_xMin || y < this.m_yMin || (x > this.m_xMax || y > this.m_yMax))
        return;
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        MultiItem multiItem = (MultiItem) this.m_List[index];
        if ((int) multiItem.X == x && (int) multiItem.Y == y && ((int) multiItem.Z == z && (int) Map.GetHeight((int) multiItem.ItemID) > 0 == (int) Map.GetHeight(itemID) > 0))
          this.m_List.RemoveAt(index--);
      }
      this.m_List.Add((object) new MultiItem()
      {
        Flags = 1,
        ItemID = (short) itemID,
        X = (short) x,
        Y = (short) y,
        Z = (short) z
      });
    }
  }
}
