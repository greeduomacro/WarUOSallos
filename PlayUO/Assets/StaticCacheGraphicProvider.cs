// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.StaticCacheGraphicProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO.Assets
{
  public sealed class StaticCacheGraphicProvider : IGraphicProvider, IDisposable
  {
    private IGraphicProvider _provider;
    private Texture[] _land;
    private Texture[] _items;
    private Texture[] _gumps;
    private Texture[] _textures;
    private Texture[] _lights;
    private Dictionary<int, Frames> _anims;

    public StaticCacheGraphicProvider(IGraphicProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException("provider");
      this._provider = provider;
    }

    public Texture GetTerrainIsometric(int landId)
    {
      if (this._land == null)
        this._land = new Texture[16384];
      landId &= 16383;
      Texture texture = this._land[landId];
      if (texture == null)
        this._land[landId] = texture = this._provider.GetTerrainIsometric(landId);
      return texture;
    }

    public Texture GetItem(int itemId)
    {
      if (this._items == null)
        this._items = new Texture[65536];
      itemId &= (int) ushort.MaxValue;
      Texture texture = this._items[itemId];
      if (texture == null)
        this._items[itemId] = texture = this._provider.GetItem(itemId);
      return texture;
    }

    public Texture GetGump(int gumpId)
    {
      if (this._gumps == null)
        this._gumps = new Texture[65536];
      gumpId &= (int) ushort.MaxValue;
      Texture texture = this._gumps[gumpId];
      if (texture == null)
        this._gumps[gumpId] = texture = this._provider.GetGump(gumpId);
      return texture;
    }

    public Texture GetTerrainTexture(int textureId)
    {
      if (this._textures == null)
        this._textures = new Texture[16384];
      textureId &= 16383;
      Texture texture = this._textures[textureId];
      if (texture == null)
        this._textures[textureId] = texture = this._provider.GetTerrainTexture(textureId);
      return texture;
    }

    public Frames GetAnimation(int animationId)
    {
      if (this._anims == null)
        this._anims = new Dictionary<int, Frames>();
      Frames animation;
      if (!this._anims.TryGetValue(animationId, out animation))
        this._anims.Add(animationId, animation = this._provider.GetAnimation(animationId));
      return animation;
    }

    public Texture GetLight(int lightId)
    {
      if (this._lights == null)
        this._lights = new Texture[100];
      if (lightId >= 0 && lightId < this._lights.Length)
        return this._lights[lightId] ?? (this._lights[lightId] = this._provider.GetLight(lightId));
      return (Texture) null;
    }

    public void Dispose()
    {
      if (this._land != null)
      {
        for (int index = 0; index < this._land.Length; ++index)
        {
          if (this._land[index] != null)
            this._land[index].Dispose();
        }
        this._land = (Texture[]) null;
      }
      if (this._items != null)
      {
        for (int index = 0; index < this._items.Length; ++index)
        {
          if (this._items[index] != null)
            this._items[index].Dispose();
        }
        this._items = (Texture[]) null;
      }
      if (this._gumps != null)
      {
        for (int index = 0; index < this._gumps.Length; ++index)
        {
          if (this._gumps[index] != null)
            this._gumps[index].Dispose();
        }
        this._gumps = (Texture[]) null;
      }
      if (this._textures != null)
      {
        for (int index = 0; index < this._textures.Length; ++index)
        {
          if (this._textures[index] != null)
            this._textures[index].Dispose();
        }
        this._textures = (Texture[]) null;
      }
      if (this._anims != null)
      {
        foreach (Frames frames in this._anims.Values)
          frames.Dispose();
        this._anims.Clear();
        this._anims = (Dictionary<int, Frames>) null;
      }
      if (this._lights != null)
      {
        for (int index = 0; index < this._lights.Length; ++index)
        {
          if (this._lights[index] != null)
            this._lights[index].Dispose();
        }
        this._lights = (Texture[]) null;
      }
      if (this._provider == null)
        return;
      this._provider.Dispose();
      this._provider = (IGraphicProvider) null;
    }
  }
}
