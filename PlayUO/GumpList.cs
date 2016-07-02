// Decompiled with JetBrains decompiler
// Type: PlayUO.GumpList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public sealed class GumpList
  {
    private static Gump[] m_Empty = new Gump[0];
    private Gump m_Owner;
    private ArrayList m_List;
    private Gump[] m_Array;
    private int m_Count;

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public Gump this[int index]
    {
      get
      {
        return (Gump) this.m_List[index];
      }
    }

    public GumpList(Gump Owner)
    {
      this.m_List = new ArrayList(0);
      this.m_Owner = Owner;
      this.m_Array = GumpList.m_Empty;
    }

    public Gump[] ToArray()
    {
      if (this.m_Array == null)
        this.m_Array = this.m_Count != 0 ? (Gump[]) this.m_List.ToArray(typeof (Gump)) : GumpList.m_Empty;
      return this.m_Array;
    }

    public void Set(GumpList g)
    {
      this.m_List = new ArrayList((ICollection) g.m_List);
      foreach (Gump gump in this.ToArray())
        gump.Parent = this.m_Owner;
      this.m_Array = (Gump[]) null;
      this.m_Count = this.m_List.Count;
      Gumps.Invalidate();
    }

    public void Clear()
    {
      while (this.m_List.Count > 0)
      {
        Gump g = (Gump) this.m_List[0];
        Gumps.Destroy(g);
        this.m_List.Remove((object) g);
      }
      this.m_Count = 0;
      this.m_Array = (Gump[]) null;
    }

    public void Swap(int a, int b)
    {
      object obj = this.m_List[a];
      this.m_List[a] = this.m_List[b];
      this.m_List[b] = obj;
    }

    public int IndexOf(Gump Child)
    {
      return this.m_List.IndexOf((object) Child);
    }

    public void Remove(Gump ToRemove)
    {
      this.m_Array = (Gump[]) null;
      this.m_List.Remove((object) ToRemove);
      Gumps.Invalidate();
      this.m_Count = this.m_List.Count;
    }

    public void RemoveAt(int index)
    {
      this.m_Array = (Gump[]) null;
      this.m_List.RemoveAt(index);
      Gumps.Invalidate();
      --this.m_Count;
    }

    public int Add(Gump ToAdd)
    {
      this.m_Array = (Gump[]) null;
      Gumps.Invalidate();
      ToAdd.Parent = this.m_Owner;
      ++this.m_Count;
      return this.m_List.Add((object) ToAdd);
    }

    public void Add(GumpList list)
    {
      Gump[] array = list.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        array[index].Parent = this.m_Owner;
        this.m_List.Add((object) array[index]);
      }
      this.m_Array = (Gump[]) null;
      this.m_Count = this.m_List.Count;
      Gumps.Invalidate();
    }

    public void Insert(int Index, Gump ToAdd)
    {
      this.m_Array = (Gump[]) null;
      Gumps.Invalidate();
      ToAdd.Parent = this.m_Owner;
      if (Index >= 0 && Index < this.m_List.Count)
        this.m_List.Insert(Index, (object) ToAdd);
      else
        this.m_List.Add((object) ToAdd);
      ++this.m_Count;
    }

    public IEnumerator GetEnumerator()
    {
      return this.m_List.GetEnumerator();
    }

    public IEnumerator GetEnumerator(int index, int count)
    {
      return this.m_List.GetEnumerator(index, count);
    }
  }
}
