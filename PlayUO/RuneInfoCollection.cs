// Decompiled with JetBrains decompiler
// Type: PlayUO.RuneInfoCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class RuneInfoCollection : CollectionBase
  {
    public RuneInfo this[int index]
    {
      get
      {
        return (RuneInfo) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(RuneInfo value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(RuneInfo value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(RuneInfo value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(RuneInfo value)
    {
      this.List.Remove((object) value);
    }

    public RuneInfoCollection.RuneInfoCollectionEnumerator GetEnumerator()
    {
      return new RuneInfoCollection.RuneInfoCollectionEnumerator(this);
    }

    public void Insert(int index, RuneInfo value)
    {
      this.List.Insert(index, (object) value);
    }

    public class RuneInfoCollectionEnumerator : IEnumerator
    {
      private int _index;
      private RuneInfo _currentElement;
      private RuneInfoCollection _collection;

      public RuneInfo Current
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

      internal RuneInfoCollectionEnumerator(RuneInfoCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (RuneInfo) null;
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
