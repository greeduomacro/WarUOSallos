// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.PhysicalGraphicProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Assets
{
  public sealed class PhysicalGraphicProvider : IGraphicProvider, IDisposable
  {
    private static readonly LightReader lightReader = new LightReader();
    private IHue _hue;

    public PhysicalGraphicProvider(IHue hue)
    {
      if (hue == null)
        throw new ArgumentNullException("hue");
      this._hue = hue;
    }

    public Texture GetTerrainIsometric(int landId)
    {
      return Engine.LandArt.ReadFromDisk(landId, this._hue);
    }

    public Texture GetItem(int itemId)
    {
      return Engine.ItemArt.ReadFromDisk(itemId, this._hue);
    }

    public Texture GetGump(int gumpId)
    {
      return Engine.m_Gumps.ReadFromDisk(gumpId, this._hue);
    }

    public Texture GetTerrainTexture(int textureId)
    {
      return Engine.TextureArt.ReadFromDisk(textureId, this._hue);
    }

    public Frames GetAnimation(int animationId)
    {
      return Engine.m_Animations.Create(animationId, this._hue);
    }

    public Texture GetLight(int lightId)
    {
      return PhysicalGraphicProvider.lightReader.ReadLight(lightId);
    }

    public void Dispose()
    {
    }
  }
}
