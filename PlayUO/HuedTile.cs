// Decompiled with JetBrains decompiler
// Type: PlayUO.HuedTile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Runtime.InteropServices;
using Ultima.Data;

namespace PlayUO
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct HuedTile
  {
    public ItemId itemId;
    public ushort hueId;
    public sbyte z;

    public HuedTile(StaticTile source)
    {
      this.itemId = source.itemId;
      this.hueId = source.hueId;
      this.z = source.z;
    }
  }
}
