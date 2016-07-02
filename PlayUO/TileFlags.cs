// Decompiled with JetBrains decompiler
// Type: PlayUO.TileFlags
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Ultima.Data;

namespace PlayUO
{
  public struct TileFlags
  {
    private TileFlag value;

    public TileFlag Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public ulong Value64
    {
      get
      {
        return (ulong) this.value;
      }
    }

    public bool this[TileFlag flag]
    {
      get
      {
        return (this.value & flag) != 0L;
      }
      set
      {
        if (value)
        {
          TileFlags& local = @this;
          TileFlag tileFlag = (^local).value | flag;
          (^local).value = tileFlag;
        }
        else
        {
          TileFlags& local = @this;
          TileFlag tileFlag = (^local).value & ~flag;
          (^local).value = tileFlag;
        }
      }
    }

    public TileFlags(TileFlag value)
    {
      this.value = value;
    }

    public override string ToString()
    {
      return ((object) this.value).ToString();
    }
  }
}
