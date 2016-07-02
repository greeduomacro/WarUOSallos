// Decompiled with JetBrains decompiler
// Type: PlayUO.Sounds
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Assets;
using PlayUO.Profiles;
using SharpDX;
using SharpDX.DirectSound;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PlayUO
{
  public class Sounds
  {
    private bool m_Enabled = true;
    private int[] m_SOC_SoundTable = new int[4]{ 72, 47, 79, 45 };
    private int[] m_SOC_TransTable = new int[22]{ 0, 0, 1, 2, 4, 2, 3, 3, 3, 4, 4, 4, 1, 3, 3, 3, 3, 1, 3, 3, 4, 1 };
    private int[] m_SCC_SoundTable = new int[3]{ 88, 46, 44 };
    private int[] m_SCC_TransTable = new int[22]{ 0, 0, 1, 0, 3, 0, 2, 2, 2, 3, 3, 3, 1, 2, 2, 2, 2, 1, 2, 2, 3, 1 };
    public const int TableSize = 4096;
    private static SharpDX.DirectSound.DirectSound m_Device;
    private IAudioProvider _audioProvider;
    private BlockingCollection<Sounds.SoundRequest> queue;
    private Task worker;

    public static SharpDX.DirectSound.DirectSound Device
    {
      get
      {
        return Sounds.m_Device;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.m_Enabled;
      }
      set
      {
        this.m_Enabled = value;
      }
    }

    public Sounds()
    {
      try
      {
        Sounds.m_Device = new SharpDX.DirectSound.DirectSound();
        ((DirectSoundBase) Sounds.m_Device).SetCooperativeLevel(Engine.m_Display.Handle, (CooperativeLevel) 2);
      }
      catch (Exception ex)
      {
        Debug.Trace("Error constructing sound factory");
        Debug.Error(ex);
        Sounds.m_Device = (SharpDX.DirectSound.DirectSound) null;
      }
      this._audioProvider = (IAudioProvider) new ManagedAudioProvider((IAudioProvider) new PhysicalAudioProvider(Sounds.m_Device));
      this.queue = new BlockingCollection<Sounds.SoundRequest>((IProducerConsumerCollection<Sounds.SoundRequest>) new ConcurrentQueue<Sounds.SoundRequest>());
      this.worker = this.SpawnWorker();
    }

    public void Dispose()
    {
      if (this.queue != null)
      {
        this.queue.CompleteAdding();
        this.worker.Wait();
        this.queue.Dispose();
        this.queue = (BlockingCollection<Sounds.SoundRequest>) null;
        this.worker = (Task) null;
      }
      if (this._audioProvider != null)
        this._audioProvider.Dispose();
      if (Sounds.m_Device == null)
        return;
      ((DisposeBase) Sounds.m_Device).Dispose();
      Sounds.m_Device = (SharpDX.DirectSound.DirectSound) null;
    }

    public void PlayContainerOpen(int GumpID)
    {
      if (GumpID == 10851)
      {
        Engine.Sounds.PlaySound(391, -1, -1, -1, 0.75f, 0.0f);
      }
      else
      {
        GumpID -= 60;
        if (GumpID < 0 || GumpID > 21)
          return;
        int index = this.m_SOC_TransTable[GumpID];
        if (index >= this.m_SOC_SoundTable.Length)
          return;
        this.PlaySound(this.m_SOC_SoundTable[index], -1, -1, -1, 0.75f, 0.0f);
      }
    }

    public void PlayContainerClose(int GumpID)
    {
      if (GumpID == 10851)
      {
        Engine.Sounds.PlaySound(457, -1, -1, -1, 0.75f, 0.0f);
      }
      else
      {
        GumpID -= 60;
        if (GumpID < 0 || GumpID > 21)
          return;
        int index = this.m_SCC_TransTable[GumpID];
        if (index >= this.m_SCC_SoundTable.Length)
          return;
        this.PlaySound(this.m_SCC_SoundTable[index], -1, -1, -1, 0.75f, 0.0f);
      }
    }

    public void PlaySound(int SoundID, int X = -1, int Y = -1, int Z = -1, float Volume = 0.75f, float Frequency = 0.0f)
    {
      if (Preferences.Current.Sound.Volume.Mute)
        return;
      this.queue.Add(new Sounds.SoundRequest()
      {
        soundId = SoundID,
        x = X,
        y = Y,
        z = Z,
        volume = Volume,
        frequency = Frequency
      });
    }

    private Task SpawnWorker()
    {
      return Task.Factory.StartNew((System.Action) (() =>
      {
        foreach (Sounds.SoundRequest consuming in this.queue.GetConsumingEnumerable())
          this.PlaySoundCore(consuming);
      }));
    }

    private void PlaySoundCore(Sounds.SoundRequest req)
    {
      if (Sounds.m_Device == null)
        return;
      if (!this.m_Enabled)
        return;
      try
      {
        SecondarySoundBuffer secondarySoundBuffer = this._audioProvider.Acquire(req.soundId);
        if (secondarySoundBuffer == null)
          return;
        Mobile player = World.Player;
        if (req.x == -1 && req.y == -1)
        {
          if (req.z == -1)
            goto label_7;
        }
        if (player != null)
        {
          int num1 = Math.Abs((req.x - player.X) * 11);
          int num2 = Math.Abs((req.y - player.Y) * 11);
          int num3 = Math.Abs(req.z - player.Z);
          int num4 = (int) Math.Sqrt((double) (num1 * num1 + num2 * num2 + num3 * num3));
          int num5 = (req.x - req.y - (player.X - player.Y)) * 350;
          int num6 = (-(num4 * 10) - (int) (5000.0 * (1.0 - (double) req.volume)) + 10000) * Preferences.Current.Sound.Volume.Value / 10000 - 10000;
          if (num6 > 0)
            num6 = 0;
          else if (num6 < -10000)
            num6 = -10000;
          if (num5 > 10000)
            num5 = 10000;
          else if (num5 < -10000)
            num5 = -10000;
          try
          {
            ((SoundBuffer) secondarySoundBuffer).set_Pan(num5);
          }
          catch
          {
          }
          try
          {
            ((SoundBuffer) secondarySoundBuffer).set_Volume(num6);
            goto label_27;
          }
          catch
          {
            goto label_27;
          }
        }
label_7:
        try
        {
          ((SoundBuffer) secondarySoundBuffer).set_Pan(0);
        }
        catch
        {
        }
        int num = Preferences.Current.Sound.Volume.Value - (int) (5000.0 * (1.0 - (double) req.volume)) - 10000;
        if (num > 0)
          num = 0;
        else if (num < -10000)
          num = -10000;
        try
        {
          ((SoundBuffer) secondarySoundBuffer).set_Volume(num);
        }
        catch
        {
        }
label_27:
        try
        {
          ((SoundBuffer) secondarySoundBuffer).set_Frequency((int) (22048.0 * (1.0 + (double) req.frequency)));
        }
        catch
        {
        }
        ((SoundBuffer) secondarySoundBuffer).set_CurrentPosition(0);
        ((SoundBuffer) secondarySoundBuffer).Play(0, (PlayFlags) 0);
      }
      catch (Exception ex)
      {
        Debug.Error(ex);
      }
    }

    private struct SoundRequest
    {
      public int soundId;
      public int x;
      public int y;
      public int z;
      public float volume;
      public float frequency;

      public SoundRequest(int soundId, int x, int y, int z, float volume, float frequency)
      {
        this.soundId = soundId;
        this.x = x;
        this.y = y;
        this.z = z;
        this.volume = volume;
        this.frequency = frequency;
      }
    }
  }
}
