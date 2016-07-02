// Decompiled with JetBrains decompiler
// Type: PlayUO.Assets.PhysicalAudioProvider
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX.DirectSound;
using SharpDX.Multimedia;
using System;
using System.IO;
using Ultima.Data;

namespace PlayUO.Assets
{
  public sealed class PhysicalAudioProvider : IAudioProvider, IDisposable
  {
    private const int HeaderSize = 40;
    private readonly SharpDX.DirectSound.DirectSound _soundDevice;
    private readonly WaveFormat _waveFormat;

    public PhysicalAudioProvider(SharpDX.DirectSound.DirectSound soundDevice)
    {
      if (soundDevice == null)
        throw new ArgumentNullException("soundDevice");
      this._soundDevice = soundDevice;
      this._waveFormat = this.GetWaveFormat();
    }

    private WaveFormat GetWaveFormat()
    {
      return WaveFormat.CreateCustomFormat((WaveFormatEncoding) 1, 22050, 1, 44100, 2, 16);
    }

    private static string GetFilePath(int soundId)
    {
      return string.Format("build/soundlegacymul/{0:00000000}.dat", (object) soundId);
    }

    public SecondarySoundBuffer Acquire(int soundId)
    {
      if (soundId < 0 || soundId >= 4096)
        return (SecondarySoundBuffer) null;
      return (SecondarySoundBuffer) Archives.Sound.Open<SecondarySoundBuffer>(PhysicalAudioProvider.GetFilePath(soundId), (Func<Stream, M0>) (stream =>
      {
        SoundBufferDescription bufferDescription1 = new SoundBufferDescription();
        bufferDescription1.Format = (__Null) this._waveFormat;
        bufferDescription1.BufferBytes = (__Null) checked ((int) (stream.Length - 40L));
        SoundBufferDescription bufferDescription2 = bufferDescription1;
        int num1 = bufferDescription2.Flags | 128;
        bufferDescription2.Flags = (__Null) num1;
        SoundBufferDescription bufferDescription3 = bufferDescription1;
        int num2 = bufferDescription3.Flags | 32;
        bufferDescription3.Flags = (__Null) num2;
        SoundBufferDescription bufferDescription4 = bufferDescription1;
        int num3 = bufferDescription4.Flags | 64;
        bufferDescription4.Flags = (__Null) num3;
        SecondarySoundBuffer secondarySoundBuffer = new SecondarySoundBuffer(this._soundDevice, bufferDescription1);
        stream.Seek(40L, SeekOrigin.Begin);
        byte[] buffer = new byte[stream.Length - 40L];
        stream.Read(buffer, 0, buffer.Length);
        ((SoundBuffer) secondarySoundBuffer).Write<byte>((M0[]) buffer, 0, (LockFlags) 2);
        return secondarySoundBuffer;
      }));
    }

    public void Dispose()
    {
    }
  }
}
