// Decompiled with JetBrains decompiler
// Type: Ultima.Data.Archives
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO;
using System;

namespace Ultima.Data
{
  public static class Archives
  {
    private static readonly Lazy<Archive> sound = Archives.LazyArchive("sound");
    private static readonly Lazy<Archive> tileArt = Archives.LazyArchive("art");
    private static readonly Lazy<Archive> gumpArt = Archives.LazyArchive("gumpart");

    public static Archive Sound
    {
      get
      {
        return Archives.sound.Value;
      }
    }

    public static Archive TileArt
    {
      get
      {
        return Archives.tileArt.Value;
      }
    }

    public static Archive GumpArt
    {
      get
      {
        return Archives.gumpArt.Value;
      }
    }

    private static Lazy<Archive> LazyArchive(string type)
    {
      return new Lazy<Archive>((Func<Archive>) (() => new Archive(Engine.FileManager.ResolveMUL(type + "LegacyMUL.uop"))));
    }

    public static void Shutdown()
    {
      if (Archives.sound.IsValueCreated)
        Archives.sound.Value.Dispose();
      if (Archives.tileArt.IsValueCreated)
        Archives.tileArt.Value.Dispose();
      if (!Archives.gumpArt.IsValueCreated)
        return;
      Archives.gumpArt.Value.Dispose();
    }
  }
}
