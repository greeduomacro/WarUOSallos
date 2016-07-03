// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.ManagedAudioProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;
using SharpDX.DirectSound;
using System;
using System.Collections.Generic;

namespace PlayUO.Assets
{
  public sealed class ManagedAudioProvider : IAudioProvider, IDisposable
  {
    private IAudioProvider _audioProvider;
    private ManagedAudioProvider.CacheEntry[] _cacheTable;

    public ManagedAudioProvider(IAudioProvider audioProvider)
    {
      if (audioProvider == null)
        throw new ArgumentNullException("audioProvider");
      this._audioProvider = audioProvider;
      this._cacheTable = new ManagedAudioProvider.CacheEntry[4096];
    }

    private bool Translate(ref int index)
    {
      GraphicTranslation graphicTranslation = GraphicTranslators.Sound[index];
      if (graphicTranslation == null)
        return false;
      index = graphicTranslation.FallbackId;
      return true;
    }

    public SecondarySoundBuffer Acquire(int soundId)
    {
      if (soundId < 0 || soundId >= this._cacheTable.Length)
        return (SecondarySoundBuffer) null;
      ManagedAudioProvider.CacheEntry cacheEntry = this._cacheTable[soundId];
      if (cacheEntry == null)
        this._cacheTable[soundId] = cacheEntry = new ManagedAudioProvider.CacheEntry(this._audioProvider, soundId);
      if (cacheEntry.Acquire() == null && this.Translate(ref soundId))
      {
        cacheEntry = this._cacheTable[soundId];
        if (cacheEntry == null)
          this._cacheTable[soundId] = cacheEntry = new ManagedAudioProvider.CacheEntry(this._audioProvider, soundId);
        cacheEntry.Acquire();
      }
      return cacheEntry.Acquire();
    }

    public void Dispose()
    {
      for (int index = 0; index < this._cacheTable.Length; ++index)
      {
        if (this._cacheTable[index] != null)
        {
          this._cacheTable[index].Dispose();
          this._cacheTable[index] = (ManagedAudioProvider.CacheEntry) null;
        }
      }
    }

    private sealed class CacheEntry : IDisposable
    {
      private IAudioProvider _audioProvider;
      private int _soundId;
      private List<SecondarySoundBuffer> _buffers;
      private bool _isEmpty;

      public CacheEntry(IAudioProvider audioProvider, int soundId)
      {
        this._audioProvider = audioProvider;
        this._soundId = soundId;
        this._buffers = new List<SecondarySoundBuffer>();
      }

      public SecondarySoundBuffer Acquire()
      {
        if (this._isEmpty)
          return (SecondarySoundBuffer) null;
        SecondarySoundBuffer soundBuffer = this.FindAvailableBuffer();
        if (soundBuffer == null)
        {
          soundBuffer = this.CloneExistingBuffer(Sounds.Device) ?? this.CreateNewBuffer();
          if (soundBuffer != null)
            this.Register(soundBuffer);
          else
            this._isEmpty = true;
        }
        return soundBuffer;
      }

      private SecondarySoundBuffer CreateNewBuffer()
      {
        return this._audioProvider.Acquire(this._soundId);
      }

      private SecondarySoundBuffer FindAvailableBuffer()
      {
        for (int index = 0; index < this._buffers.Count; ++index)
        {
          SecondarySoundBuffer secondarySoundBuffer = this._buffers[index];
          BufferStatus bufferStatus = (BufferStatus) ((SoundBuffer) secondarySoundBuffer).Status;
          if (((Enum) (object) bufferStatus).HasFlag((Enum) (object) (BufferStatus) 2))
          {
            this._buffers.RemoveAt(index--);
            ((DisposeBase) secondarySoundBuffer).Dispose();
          }
          else if (!((Enum) (object) bufferStatus).HasFlag((Enum) (object) (BufferStatus) 1))
            return secondarySoundBuffer;
        }
        return (SecondarySoundBuffer) null;
      }

      private SecondarySoundBuffer CloneExistingBuffer(SharpDX.DirectSound.DirectSound soundDevice)
      {
        for (int index = 0; index < this._buffers.Count; ++index)
        {
          SecondarySoundBuffer secondarySoundBuffer1 = this._buffers[index];
          try
          {
            SecondarySoundBuffer secondarySoundBuffer2 = (SecondarySoundBuffer) soundDevice.DuplicateSoundBuffer((SoundBuffer) secondarySoundBuffer1);
            if (secondarySoundBuffer2 != null)
              return secondarySoundBuffer2;
          }
          catch
          {
          }
        }
        return (SecondarySoundBuffer) null;
      }

      private void Register(SecondarySoundBuffer soundBuffer)
      {
        if (soundBuffer == null)
          return;
        this._buffers.Add(soundBuffer);
      }

      public void Dispose()
      {
        if (this._buffers.Count <= 0)
          return;
        for (int index = 0; index < this._buffers.Count; ++index)
          ((DisposeBase) this._buffers[index]).Dispose();
        this._buffers.Clear();
      }
    }
  }
}
