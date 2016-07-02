// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.PlayerCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO.Profiles
{
  public class PlayerCollection : CollectionBase
  {
    public Player this[int index]
    {
      get
      {
        return (Player) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Player value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(Player value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(Player value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(Player value)
    {
      this.List.Remove((object) value);
    }

    public PlayerCollection.PlayerCollectionEnumerator GetEnumerator()
    {
      return new PlayerCollection.PlayerCollectionEnumerator(this);
    }

    public void Insert(int index, Player value)
    {
      this.List.Insert(index, (object) value);
    }

    public class PlayerCollectionEnumerator : IEnumerator
    {
      private int _index;
      private Player _currentElement;
      private PlayerCollection _collection;

      public Player Current
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

      internal PlayerCollectionEnumerator(PlayerCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (Player) null;
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
