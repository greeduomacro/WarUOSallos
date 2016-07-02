// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.ShadedGraphicProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Assets
{
  public sealed class ShadedGraphicProvider : IGraphicProvider, IDisposable
  {
    private IGraphicProvider _provider;
    private ShaderData _shaderData;

    public ShadedGraphicProvider(ShaderData shaderData, IGraphicProvider provider)
    {
      if (shaderData == null)
        throw new ArgumentNullException("shaderData");
      if (provider == null)
        throw new ArgumentNullException("provider");
      this._shaderData = shaderData;
      this._provider = provider;
    }

    public Texture GetTerrainIsometric(int landId)
    {
      return Texture.Clone(this._provider.GetTerrainIsometric(landId), this._shaderData);
    }

    public Texture GetItem(int itemId)
    {
      return Texture.Clone(this._provider.GetItem(itemId), this._shaderData);
    }

    public Texture GetGump(int gumpId)
    {
      return Texture.Clone(this._provider.GetGump(gumpId), this._shaderData);
    }

    public Texture GetTerrainTexture(int textureId)
    {
      return Texture.Clone(this._provider.GetTerrainTexture(textureId), this._shaderData);
    }

    public Frames GetAnimation(int animationId)
    {
      return Frames.Clone(this._provider.GetAnimation(animationId), this._shaderData);
    }

    public Texture GetLight(int lightId)
    {
      return Texture.Clone(this._provider.GetLight(lightId), this._shaderData);
    }

    public void Dispose()
    {
      if (this._provider == null)
        return;
      this._provider.Dispose();
      this._provider = (IGraphicProvider) null;
    }
  }
}
