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
  public sealed class PhysicalAudioProvider : IAudioProvider
  {
    private const int HeaderSize = 40;
    private readonly DirectSound _soundDevice;
    private readonly WaveFormat _waveFormat;

    public PhysicalAudioProvider(DirectSound soundDevice)
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
        return null;
      return (SecondarySoundBuffer) Archives.Sound.Open<SecondarySoundBuffer>(GetFilePath(soundId), stream =>
      {
        SoundBufferDescription bufferDescription1 = new SoundBufferDescription();
        bufferDescription1.Format = _waveFormat;
        bufferDescription1.BufferBytes = checked ((int) (stream.Length - 40L));
        SoundBufferDescription bufferDescription2 = bufferDescription1;
        BufferFlags num1 = bufferDescription2.Flags | (BufferFlags) 128;
        bufferDescription2.Flags = num1;
        SoundBufferDescription bufferDescription3 = bufferDescription1;
        BufferFlags num2 = bufferDescription3.Flags | (BufferFlags) 32;
        bufferDescription3.Flags = num2;
        SoundBufferDescription bufferDescription4 = bufferDescription1;
        BufferFlags num3 = bufferDescription4.Flags | (BufferFlags) 64;
        bufferDescription4.Flags = num3;
        SecondarySoundBuffer secondarySoundBuffer = new SecondarySoundBuffer(_soundDevice, bufferDescription1);
        stream.Seek(40L, SeekOrigin.Begin);
        byte[] buffer = new byte[stream.Length - 40L];
        stream.Read(buffer, 0, buffer.Length);
        secondarySoundBuffer.Write( buffer, 0, (SharpDX.DirectSound.LockFlags) 2);
        return secondarySoundBuffer;
      });
    }

    public void Dispose()
    {
    }
  }
}
