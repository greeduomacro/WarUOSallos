// Decompiled with JetBrains decompiler
// Type: PlayUO.EffectList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class EffectList : IEnumerable
  {
    private ArrayList m_List;

    public int Count
    {
      get
      {
        return this.m_List.Count;
      }
    }

    public Effect this[int Index]
    {
      get
      {
        return (Effect) this.m_List[Index];
      }
    }

    public EffectList()
    {
      this.m_List = new ArrayList(0);
    }

    public void Clear()
    {
      this.m_List.Clear();
    }

    public int IndexOf(Effect Child)
    {
      return this.m_List.IndexOf((object) Child);
    }

    public void Remove(Effect ToRemove)
    {
      this.m_List.Remove((object) ToRemove);
    }

    public void RemoveAt(int Index)
    {
      this.m_List.RemoveAt(Index);
    }

    public int Add(Effect ToAdd)
    {
      return this.m_List.Add((object) ToAdd);
    }

    public void Insert(int Index, Effect ToAdd)
    {
      if (Index >= 0 && Index < this.m_List.Count)
        this.m_List.Insert(Index, (object) ToAdd);
      else
        this.m_List.Add((object) ToAdd);
    }

    public IEnumerator GetEnumerator()
    {
      return this.m_List.GetEnumerator();
    }
  }
}
