// Decompiled with JetBrains decompiler
// Type: PlayUO.Multis
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System.Collections;
using System.IO;

namespace PlayUO
{
  public class Multis
  {
    private ArrayList[] m_Cache;
    private ArrayList m_Items;

    public ArrayList Items
    {
      get
      {
        return this.m_Items;
      }
    }

    public Multis()
    {
      this.m_Items = new ArrayList();
      this.m_Cache = new ArrayList[8192];
    }

    public bool RunUO_IsInside(Item item, Multi m, int px, int py, int pz)
    {
      int xMin;
      int yMin;
      int xMax;
      int yMax;
      m.GetBounds(out xMin, out yMin, out xMax, out yMax);
      int num1 = px - item.X;
      int num2 = py - item.Y;
      if (num1 >= xMin && num1 <= xMax && (num2 >= yMin && num2 <= yMax))
      {
        if (item.Multi != m && num2 < yMax)
          return true;
        int index1 = num1 - xMin;
        int index2 = num2 - yMin;
        if (m.RunUO_Inside == null)
          m.UpdateRadar();
        int num3 = (int) m.RunUO_Inside[index2][index1] + item.Z;
        if (pz == num3 || pz + 16 > num3)
          return true;
      }
      return false;
    }

    public bool RunUO_IsInside(int px, int py, int pz)
    {
      for (int index = 0; index < this.m_Items.Count; ++index)
      {
        Item obj = (Item) this.m_Items[index];
        if (obj.InWorld && obj.IsMulti)
        {
          CustomMultiEntry customMulti = CustomMultiLoader.GetCustomMulti(obj.Serial, obj.Revision);
          Multi m = (Multi) null;
          if (customMulti != null)
            m = customMulti.Multi;
          if (m == null)
            m = obj.Multi;
          if (m != null && this.RunUO_IsInside(obj, m, px, py, pz))
            return true;
        }
      }
      return false;
    }

    public void Clear()
    {
      this.m_Items.Clear();
    }

    public void Sort()
    {
      this.m_Items.Sort((IComparer) Multis.MultiComparer.Instance);
    }

    public ArrayList Load(int multiID)
    {
      multiID &= 8191;
      return this.m_Cache[multiID] ?? (this.m_Cache[multiID] = this.ReadFromDisk(multiID));
    }

    public void Dispose()
    {
      this.m_Cache = (ArrayList[]) null;
      this.m_Items.Clear();
      this.m_Items = (ArrayList) null;
    }

    public void Register(Item item)
    {
      if (!this.m_Items.Contains((object) item))
      {
        this.m_Items.Add((object) item);
        this.m_Items.Sort((IComparer) Multis.MultiComparer.Instance);
      }
      Map.Invalidate();
      GRadar.Invalidate();
    }

    public void Unregister(Item item)
    {
      if (this.m_Items.Contains((object) item))
        this.m_Items.Remove((object) item);
      Map.Invalidate();
      GRadar.Invalidate();
    }

    public void Update(MapPackage map)
    {
      int count1 = this.m_Items.Count;
      if (count1 == 0)
        return;
      int length1 = map.cells.GetLength(0);
      int length2 = map.cells.GetLength(1);
      int num1 = map.CellX;
      int num2 = map.CellY;
      int num3 = num1 + length1;
      int num4 = num2 + length2;
      int houseLevel = Options.Current.HouseLevel;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        Item obj = (Item) this.m_Items[index1];
        if (obj.InWorld)
        {
          CustomMultiEntry customMulti = CustomMultiLoader.GetCustomMulti(obj.Serial, obj.Revision);
          Multi multi = (Multi) null;
          if (customMulti != null)
            multi = customMulti.Multi;
          if (multi == null)
            multi = obj.Multi;
          if (multi != null)
          {
            int xMin;
            int yMin;
            int xMax;
            int yMax;
            multi.GetBounds(out xMin, out yMin, out xMax, out yMax);
            xMin += obj.X;
            yMin += obj.Y;
            xMax += obj.X;
            yMax += obj.Y;
            if (xMin < num3 && xMax >= num1 && (yMin < num4 && yMax >= num2))
            {
              ArrayList list = multi.List;
              int count2 = list.Count;
              int num5 = int.MinValue | index1;
              for (int index2 = 0; index2 < count2; ++index2)
              {
                MultiItem multiItem = (MultiItem) list[index2];
                if (multiItem.Flags != 0 || index2 == 0)
                {
                  int num6 = obj.X + (int) multiItem.X;
                  int num7 = obj.Y + (int) multiItem.Y;
                  int index3 = num6 - num1;
                  int index4 = num7 - num2;
                  if (index3 >= 0 && index3 < length1 && (index4 >= 0 && index4 < length2))
                  {
                    bool flag = true;
                    int num8 = (int) multiItem.ItemID;
                    if (flag)
                      map.cells[index3, index4].Add((object) StaticItem.Instantiate((short) num8, multiItem.ItemID, (sbyte) (obj.Z + (int) multiItem.Z), num5 | index2 << 16));
                  }
                }
              }
            }
          }
        }
      }
    }

    private unsafe ArrayList ReadFromDisk(int multiID)
    {
      BinaryReader binaryReader = new BinaryReader(Engine.FileManager.OpenMUL(Files.MultiIdx));
      binaryReader.BaseStream.Seek((long) (multiID * 12), SeekOrigin.Begin);
      int num = binaryReader.ReadInt32();
      int length1 = binaryReader.ReadInt32();
      binaryReader.Close();
      if (num == -1)
        return new ArrayList();
      Stream stream = Engine.FileManager.OpenMUL(Files.MultiMul);
      stream.Seek((long) num, SeekOrigin.Begin);
      byte[] buffer = new byte[length1];
      UnsafeMethods.ReadFile((FileStream) stream, buffer, 0, buffer.Length);
      stream.Close();
      int length2 = length1 / sizeof (MultiItem);
      MultiItem[] multiItemArray = new MultiItem[length2];
      fixed (byte* numPtr = buffer)
      {
        MultiItem* multiItemPtr1 = (MultiItem*) numPtr;
        MultiItem* multiItemPtr2 = multiItemPtr1 + length2;
        fixed (MultiItem* multiItemPtr3 = multiItemArray)
        {
          MultiItem* multiItemPtr4 = multiItemPtr3;
          while (multiItemPtr1 < multiItemPtr2)
            *multiItemPtr4++ = *multiItemPtr1++;
        }
      }
      return new ArrayList((ICollection) multiItemArray);
    }

    private class MultiComparer : IComparer
    {
      public static readonly Multis.MultiComparer Instance = new Multis.MultiComparer();

      public int Compare(object x, object y)
      {
        Item obj1 = (Item) x;
        Item obj2 = (Item) y;
        if (obj1.Y > obj2.Y)
          return 1;
        if (obj1.Y < obj2.Y)
          return -1;
        if (obj1.X > obj2.X)
          return 1;
        return obj1.X < obj2.X ? -1 : 0;
      }
    }
  }
}
