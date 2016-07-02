// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.DynamicCacheGraphicProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO.Assets
{
  public sealed class DynamicCacheGraphicProvider : IGraphicProvider, IDisposable
  {
    private IGraphicProvider _provider;
    private Dictionary<int, Texture> _land;
    private Dictionary<int, Texture> _items;
    private Dictionary<int, Texture> _gumps;
    private Dictionary<int, Texture> _textures;
    private Dictionary<int, Texture> _lights;
    private Dictionary<int, Frames> _anims;

    public DynamicCacheGraphicProvider(IGraphicProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException("provider");
      this._provider = provider;
    }

    public Texture GetTerrainIsometric(int landId)
    {
      if (this._land == null)
        this._land = new Dictionary<int, Texture>();
      Texture terrainIsometric;
      if (!this._land.TryGetValue(landId, out terrainIsometric))
        this._land.Add(landId, terrainIsometric = this._provider.GetTerrainIsometric(landId));
      return terrainIsometric;
    }

    public Texture GetItem(int itemId)
    {
      if (this._items == null)
        this._items = new Dictionary<int, Texture>();
      Texture texture;
      if (!this._items.TryGetValue(itemId, out texture))
        this._items.Add(itemId, texture = this._provider.GetItem(itemId));
      return texture;
    }

    public Texture GetGump(int gumpId)
    {
      if (this._gumps == null)
        this._gumps = new Dictionary<int, Texture>();
      Texture gump;
      if (!this._gumps.TryGetValue(gumpId, out gump))
        this._gumps.Add(gumpId, gump = this._provider.GetGump(gumpId));
      return gump;
    }

    public Texture GetTerrainTexture(int textureId)
    {
      if (this._textures == null)
        this._textures = new Dictionary<int, Texture>();
      Texture terrainTexture;
      if (!this._textures.TryGetValue(textureId, out terrainTexture))
        this._textures.Add(textureId, terrainTexture = this._provider.GetTerrainTexture(textureId));
      return terrainTexture;
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
        this._lights = new Dictionary<int, Texture>();
      Texture light;
      if (!this._lights.TryGetValue(lightId, out light))
        this._lights.Add(lightId, light = this._provider.GetLight(lightId));
      return light;
    }

    public void Dispose()
    {
      if (this._land != null)
      {
        foreach (Texture texture in this._land.Values)
          texture.Dispose();
        this._land.Clear();
        this._land = (Dictionary<int, Texture>) null;
      }
      if (this._items != null)
      {
        foreach (Texture texture in this._items.Values)
          texture.Dispose();
        this._items.Clear();
        this._items = (Dictionary<int, Texture>) null;
      }
      if (this._gumps != null)
      {
        foreach (Texture texture in this._gumps.Values)
          texture.Dispose();
        this._gumps.Clear();
        this._gumps = (Dictionary<int, Texture>) null;
      }
      if (this._textures != null)
      {
        foreach (Texture texture in this._textures.Values)
          texture.Dispose();
        this._textures.Clear();
        this._textures = (Dictionary<int, Texture>) null;
      }
      if (this._lights != null)
      {
        foreach (Texture texture in this._lights.Values)
          texture.Dispose();
        this._lights.Clear();
        this._lights = (Dictionary<int, Texture>) null;
      }
      if (this._anims != null)
      {
        foreach (Frames frames in this._anims.Values)
          frames.Dispose();
        this._anims.Clear();
        this._anims = (Dictionary<int, Frames>) null;
      }
      if (this._provider == null)
        return;
      this._provider.Dispose();
      this._provider = (IGraphicProvider) null;
    }
  }
}
