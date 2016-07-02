// Decompiled with JetBrains decompiler
// Type: PlayUO.ActionCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public class ActionCollection : CollectionBase
  {
    public Action this[int index]
    {
      get
      {
        return (Action) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Action value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(Action value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(Action value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(Action value)
    {
      this.List.Remove((object) value);
    }

    public ActionCollection.ActionCollectionEnumerator GetEnumerator()
    {
      return new ActionCollection.ActionCollectionEnumerator(this);
    }

    public void Insert(int index, Action value)
    {
      this.List.Insert(index, (object) value);
    }

    public class ActionCollectionEnumerator : IEnumerator
    {
      private int _index;
      private Action _currentElement;
      private ActionCollection _collection;

      public Action Current
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

      internal ActionCollectionEnumerator(ActionCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (Action) null;
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
