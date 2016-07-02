// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.GumpLayoutCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO.Profiles
{
  public class GumpLayoutCollection : CollectionBase
  {
    public GumpLayout this[int index]
    {
      get
      {
        return (GumpLayout) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(GumpLayout value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(GumpLayout value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(GumpLayout value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(GumpLayout value)
    {
      this.List.Remove((object) value);
    }

    public GumpLayoutCollection.GumpLayoutCollectionEnumerator GetEnumerator()
    {
      return new GumpLayoutCollection.GumpLayoutCollectionEnumerator(this);
    }

    public void Insert(int index, GumpLayout value)
    {
      this.List.Insert(index, (object) value);
    }

    public class GumpLayoutCollectionEnumerator : IEnumerator
    {
      private int _index;
      private GumpLayout _currentElement;
      private GumpLayoutCollection _collection;

      public GumpLayout Current
      {
        get
        {
          if (this._index == -1 || this._index >= this._collection.Count)
            throw new IndexOutOfRangeException("Enumerator not started.");
          return this._currentElement;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          if (this._index == -1 || this._index >= this._collection.Count)
            throw new IndexOutOfRangeException("Enumerator not started.");
          return (object) this._currentElement;
        }
      }

      internal GumpLayoutCollectionEnumerator(GumpLayoutCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (GumpLayout) null;
      }

      public bool MoveNext()
      {
        if (this._index < this._collection.Count - 1)
        {
          this._index = this._index + 1;
          this._currentElement = this._collection[this._index];
          return true;
        }
        this._index = this._collection.Count;
        return false;
      }
    }
  }
}
