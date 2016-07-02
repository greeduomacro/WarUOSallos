// Decompiled with JetBrains decompiler
// Type: PlayUO.ImageCache
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System;

namespace PlayUO
{
  public class ImageCache : IDisposable
  {
    private Texture _sallosDragon;
    private Texture _travelIcon;
    private Texture _lastTargetIcon;
    private Texture _playerHalo;
    private Texture _playerAlly;
    private Texture _playerEnemy;
    private Texture _targetCursorHighlight;
    private Texture _targetCursorLocal;

    public Texture SallosDragon
    {
      get
      {
        if (this._sallosDragon == null)
        {
          try
          {
            this._sallosDragon = Engine.LoadArchivedTexture(Archive.AcquireArchive("shell"), "graphics/gold_dragon.png");
          }
          catch
          {
          }
        }
        return this._sallosDragon;
      }
    }

    public Texture TravelIcon
    {
      get
      {
        return this.AcquireImage(ref this._travelIcon, "travel-icon.png");
      }
    }

    public Texture LastTargetIcon
    {
      get
      {
        return this.AcquireImage(ref this._lastTargetIcon, "last-target-icon.png");
      }
    }

    public Texture PlayerHalo
    {
      get
      {
        return this.AcquireImage(ref this._playerHalo, "halo.png");
      }
    }

    public Texture PlayerAlly
    {
      get
      {
        return this.AcquireImage(ref this._playerAlly, "ally.png");
      }
    }

    public Texture PlayerEnemy
    {
      get
      {
        return this.AcquireImage(ref this._playerEnemy, "enemy.png");
      }
    }

    public Texture TargetCursorHighlight
    {
      get
      {
        return this.AcquireImage(ref this._targetCursorHighlight, "target-cursor-highlight.png");
      }
    }

    public Texture TargetCursorLocal
    {
      get
      {
        return this.AcquireImage(ref this._targetCursorLocal, "target-cursor-local.png");
      }
    }

    public void Dispose()
    {
      this.Dispose(ref this._sallosDragon);
      this.Dispose(ref this._travelIcon);
      this.Dispose(ref this._lastTargetIcon);
      this.Dispose(ref this._playerHalo);
      this.Dispose(ref this._playerAlly);
      this.Dispose(ref this._playerEnemy);
      this.Dispose(ref this._targetCursorHighlight);
      this.Dispose(ref this._targetCursorLocal);
    }

    private void Dispose(ref Texture image)
    {
      if (image == null)
        return;
      image.Dispose();
      image = (Texture) null;
    }

    private Texture AcquireAlpha(ref Texture image, string path)
    {
      if (image == null)
        image = Engine.LoadImageAsAlpha(path);
      return image;
    }

    private Texture AcquireImage(ref Texture image, string path)
    {
      if (image == null)
        image = Engine.LoadArchivedTexture(path);
      return image;
    }
  }
}
