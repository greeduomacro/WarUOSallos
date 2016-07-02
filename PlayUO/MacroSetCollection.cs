// Decompiled with JetBrains decompiler
// Type: PlayUO.MacroSetCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class MacroSetCollection : CollectionBase
  {
    public MacroSet this[int index]
    {
      get
      {
        return (MacroSet) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(MacroSet value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(MacroSet value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(MacroSet value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(MacroSet value)
    {
      this.List.Remove((object) value);
    }

    public MacroSetCollection.MacroSetCollectionEnumerator GetEnumerator()
    {
      return new MacroSetCollection.MacroSetCollectionEnumerator(this);
    }

    public void Insert(int index, MacroSet value)
    {
      this.List.Insert(index, (object) value);
    }

    public class MacroSetCollectionEnumerator : IEnumerator
    {
      private int _index;
      private MacroSet _currentElement;
      private MacroSetCollection _collection;

      public MacroSet Current
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

      internal MacroSetCollectionEnumerator(MacroSetCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (MacroSet) null;
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
