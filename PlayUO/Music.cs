// Decompiled with JetBrains decompiler
// Type: PlayUO.Music
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using SharpDX;
using SharpDX.IO;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PlayUO
{
  public class Music
  {
    private static PMediaPlayerState m_State = (PMediaPlayerState) 1;
    private static AutoResetEvent m_BufferEndEvent = new AutoResetEvent(false);
    private static ManualResetEvent m_PlayEvent = new ManualResetEvent(false);
    private static ManualResetEvent m_WaitForPlayToOutput = new ManualResetEvent(false);
    private static Stopwatch m_Clock = new Stopwatch();
    private const int WaitPrecision = 1;
    private static SharpDX.XAudio2.XAudio2 m_XAudio2;
    private static MasteringVoice m_MasteringVoice;
    private static NativeFileStream m_FileStream;
    private static AudioDecoder m_AudioDecoder;
    private static SourceVoice m_SourceVoice;
    private static AudioBuffer[] m_AudioBuffers;
    private static DataPointer[] m_MemoryBuffers;
    private static int m_PlayCounter;
    private static TimeSpan m_PlayPosition;
    private static TimeSpan m_NextPlayPosition;
    private static TimeSpan m_PlayPositionStart;
    private static Task m_PlayingTask;
    private static string m_FileName;

    public static TimeSpan Length
    {
      get
      {
        return Music.m_AudioDecoder.get_Duration();
      }
    }

    public static bool Playing
    {
      get
      {
        return ((Enum) (object) Music.m_State).HasFlag((Enum) (object) (PMediaPlayerState) 2);
      }
    }

    public static bool Stopped
    {
      get
      {
        return ((Enum) (object) Music.m_State).HasFlag((Enum) (object) (PMediaPlayerState) 1);
      }
    }

    public static bool Paused
    {
      get
      {
        return ((Enum) (object) Music.m_State).HasFlag((Enum) (object) (PMediaPlayerState) 3);
      }
    }

    public static PMediaPlayerState State { get; private set; }

    public static bool IsRepeating { get; set; }

    public static bool IsDisposed { get; private set; }

    public double Volume { get; set; }

    public static void Reset()
    {
      TimeSpan timeSpan;
      Music.m_NextPlayPosition = timeSpan = TimeSpan.Zero;
      Music.m_PlayPositionStart = timeSpan;
      Music.m_PlayPosition = timeSpan;
      Music.m_Clock.Restart();
      ++Music.m_PlayCounter;
    }

    private static void PlayAsync()
    {
      int index = 0;
      try
      {
        while (true)
        {
          bool flag1;
          do
          {
            do
              ;
            while (!Music.IsDisposed && !Music.m_PlayEvent.WaitOne(1));
            if (!Music.IsDisposed)
            {
              Music.m_Clock.Restart();
              Music.m_PlayPositionStart = Music.m_NextPlayPosition;
              Music.m_PlayPosition = Music.m_PlayPositionStart;
              int num1 = Music.m_PlayCounter;
              IEnumerator<DataPointer> enumerator = Music.m_AudioDecoder.GetSamples(Music.m_PlayPositionStart).GetEnumerator();
              bool flag2 = true;
              flag1 = false;
              while (true)
              {
                do
                  ;
                while (!Music.IsDisposed && !Music.Stopped && !Music.m_PlayEvent.WaitOne(1));
                if (!Music.IsDisposed && !Music.Stopped)
                {
                  if (num1 == Music.m_PlayCounter)
                  {
                    while (Music.m_SourceVoice.get_State().BuffersQueued == Music.m_AudioBuffers.Length && !Music.IsDisposed && !Music.Stopped)
                      Music.m_BufferEndEvent.WaitOne(1);
                    if (!Music.IsDisposed && !Music.Stopped)
                    {
                      if (((IEnumerator) enumerator).MoveNext())
                      {
                        DataPointer current = enumerator.Current;
                        if (num1 == Music.m_PlayCounter)
                        {
                          if (current.Size > Music.m_MemoryBuffers[index].Size)
                          {
                            if ((IntPtr) Music.m_MemoryBuffers[index].Pointer != IntPtr.Zero)
                              Utilities.FreeMemory((IntPtr) Music.m_MemoryBuffers[index].Pointer);
                            Music.m_MemoryBuffers[index].Pointer = (__Null) Utilities.AllocateMemory((int) current.Size, 16);
                            Music.m_MemoryBuffers[index].Size = current.Size;
                          }
                          Utilities.CopyMemory((IntPtr) Music.m_MemoryBuffers[index].Pointer, (IntPtr) current.Pointer, (int) current.Size);
                          Music.m_AudioBuffers[index].AudioDataPointer = Music.m_MemoryBuffers[index].Pointer;
                          Music.m_AudioBuffers[index].AudioBytes = current.Size;
                          if (flag2)
                          {
                            Music.m_Clock.Restart();
                            flag2 = false;
                            Music.m_WaitForPlayToOutput.Set();
                          }
                          Music.m_PlayPosition = new TimeSpan(Music.m_PlayPositionStart.Ticks + Music.m_Clock.Elapsed.Ticks);
                          Music.m_SourceVoice.SubmitSourceBuffer(Music.m_AudioBuffers[index], (uint[]) null);
                          int num2;
                          index = (num2 = index + 1) % Music.m_AudioBuffers.Length;
                        }
                        else
                          goto label_23;
                      }
                      else
                        goto label_14;
                    }
                    else
                      goto label_12;
                  }
                  else
                    goto label_23;
                }
                else
                  break;
              }
              Music.m_NextPlayPosition = TimeSpan.Zero;
              goto label_23;
label_12:
              Music.m_NextPlayPosition = TimeSpan.Zero;
              goto label_23;
label_14:
              flag1 = true;
label_23:;
            }
            else
              goto label_25;
          }
          while (Music.IsDisposed || !flag1 || (Music.IsRepeating || !Music.Playing));
          Music.Stop();
        }
label_25:;
      }
      finally
      {
        Music.Stop();
      }
    }

    public static void Stop()
    {
      if (Music.m_XAudio2 == null || Music.Stopped)
        return;
      Music.m_PlayPosition = TimeSpan.Zero;
      Music.m_NextPlayPosition = TimeSpan.Zero;
      Music.m_PlayPositionStart = TimeSpan.Zero;
      Music.m_Clock.Stop();
      ++Music.m_PlayCounter;
      Music.m_State = (PMediaPlayerState) 1;
      Music.m_PlayEvent.Reset();
      if (Music.m_FileStream != null)
      {
        ((Stream) Music.m_FileStream).Close();
        ((Stream) Music.m_FileStream).Dispose();
        Music.m_FileStream = (NativeFileStream) null;
      }
      if (Music.m_SourceVoice != null)
      {
        Music.m_SourceVoice.Stop();
        Music.m_SourceVoice.FlushSourceBuffers();
      }
      if (Music.m_AudioBuffers == null)
        return;
      for (int index = 0; index < Music.m_AudioBuffers.Length; ++index)
      {
        Utilities.FreeMemory((IntPtr) Music.m_MemoryBuffers[index].Pointer);
        Music.m_MemoryBuffers[index].Pointer = (__Null) IntPtr.Zero;
      }
    }

    public static void Destroy()
    {
      if (Music.m_XAudio2 == null)
        return;
      Music.Stop();
      if (Music.m_SourceVoice != null)
      {
        Music.m_SourceVoice.Stop();
        ((Voice) Music.m_SourceVoice).DestroyVoice();
        Music.m_SourceVoice = (SourceVoice) null;
      }
      if (Music.m_AudioDecoder != null)
      {
        ((Component) Music.m_AudioDecoder).Dispose();
        Music.m_AudioDecoder = (AudioDecoder) null;
      }
      ((Voice) Music.m_MasteringVoice).DestroyVoice();
      ((DisposeBase) Music.m_MasteringVoice).Dispose();
      Music.m_MasteringVoice = (MasteringVoice) null;
      ((DisposeBase) Music.m_XAudio2).Dispose();
      Music.m_XAudio2 = (SharpDX.XAudio2.XAudio2) null;
      MediaFactory.Shutdown();
      Music.IsDisposed = true;
      Music.m_PlayingTask.Wait();
    }

    public static void Dispose()
    {
      Music.Stop();
    }

    public static void UpdateVolume()
    {
    }

    public static void Play(string fileName)
    {
      if (!Music.Stopped && Music.m_FileName == fileName)
        return;
      if (Music.m_XAudio2 == null)
      {
        MediaFactory.Startup(131184, 1);
        Music.m_XAudio2 = new SharpDX.XAudio2.XAudio2();
        Music.m_XAudio2.StartEngine();
        Music.m_MasteringVoice = new MasteringVoice(Music.m_XAudio2, 0, 0, 0);
        Music.m_AudioDecoder = new AudioDecoder();
        Music.m_PlayingTask = Task.Factory.StartNew(new System.Action(Music.PlayAsync), TaskCreationOptions.LongRunning);
      }
      string path = Engine.FileManager.ResolveMUL(string.Format("music/{0}", (object) fileName));
      if (!File.Exists(path))
        return;
      if (!Music.Stopped)
        Music.Stop();
      Music.m_FileName = fileName;
      Music.m_FileStream = new NativeFileStream(path, (NativeFileMode) 3, (NativeFileAccess) int.MinValue, (NativeFileShare) 1);
      Music.m_AudioDecoder.SetSourceStream((Stream) Music.m_FileStream);
      if (Music.m_SourceVoice == null)
      {
        Music.m_SourceVoice = new SourceVoice(Music.m_XAudio2, Music.m_AudioDecoder.get_WaveFormat());
        Music.m_SourceVoice.add_BufferEnd(new System.Action<IntPtr>(Music.SourceVoice_BufferEnd));
      }
      Music.m_SourceVoice.Start();
      Music.m_AudioBuffers = new AudioBuffer[3];
      Music.m_MemoryBuffers = new DataPointer[Music.m_AudioBuffers.Length];
      for (int index = 0; index < Music.m_AudioBuffers.Length; ++index)
      {
        Music.m_AudioBuffers[index] = new AudioBuffer();
        Music.m_MemoryBuffers[index].Size = (__Null) 32768;
        Music.m_MemoryBuffers[index].Pointer = (__Null) Utilities.AllocateMemory((int) Music.m_MemoryBuffers[index].Size, 16);
      }
      PlayUO.Volume volume = Preferences.Current.Music.Volume;
      if (volume != null && volume.Mute)
        return;
      if (volume != null)
        ((Voice) Music.m_SourceVoice).SetVolume(1f, 0);
      bool flag = false;
      if (Music.Stopped)
      {
        ++Music.m_PlayCounter;
        Music.m_WaitForPlayToOutput.Reset();
        flag = true;
      }
      else
        Music.m_Clock.Start();
      Music.m_State = (PMediaPlayerState) 2;
      Music.m_PlayEvent.Set();
      if (!flag)
        return;
      Music.m_WaitForPlayToOutput.WaitOne();
    }

    private static void SourceVoice_BufferEnd(IntPtr obj)
    {
      Music.m_BufferEndEvent.Set();
    }
  }
}
