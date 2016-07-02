// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.CharacterCollection
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO.Profiles
{
  public class CharacterCollection : CollectionBase
  {
    public Character this[int index]
    {
      get
      {
        return (Character) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Character value)
    {
      return this.List.Add((object) value);
    }

    public bool Contains(Character value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(Character value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Remove(Character value)
    {
      this.List.Remove((object) value);
    }

    public CharacterCollection.CharacterCollectionEnumerator GetEnumerator()
    {
      return new CharacterCollection.CharacterCollectionEnumerator(this);
    }

    public void Insert(int index, Character value)
    {
      this.List.Insert(index, (object) value);
    }

    public class CharacterCollectionEnumerator : IEnumerator
    {
      private int _index;
      private Character _currentElement;
      private CharacterCollection _collection;

      public Character Current
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

      internal CharacterCollectionEnumerator(CharacterCollection collection)
      {
        this._index = -1;
        this._collection = collection;
      }

      public void Reset()
      {
        this._index = -1;
        this._currentElement = (Character) null;
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
