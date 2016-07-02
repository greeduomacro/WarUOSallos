// Decompiled with JetBrains decompiler
// Type: PlayUO.Videos.Playback
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace PlayUO.Videos
{
  public sealed class Playback : IDisposable
  {
    private int _speedNumerator = 1;
    private int _speedDenominator = 1;
    private const int Version = 4;
    private static Playback _video;
    private GZBlockIn _stream;
    private int _version;
    private byte[] _hashCode;
    private DateTime _timeStamp;
    private TimeSpan _duration;
    private string _playerName;
    private string _serverName;
    private IPAddress _ipAddress;
    private Stopwatch _stopwatch;
    private PlaybackState _state;
    private long _origin;
    private int _elapsed;
    private bool _isStreaming;
    private int _next;
    private bool _hasNext;
    private int _last;
    private int _speedBase;

    public static Playback Video
    {
      get
      {
        return Playback._video;
      }
      set
      {
        if (Playback._video == null)
          return;
        if (Playback._video != null)
          Playback._video.Dispose();
        Playback._video = value;
      }
    }

    public static bool Active
    {
      get
      {
        if (Playback._video != null)
          return Playback._video.State > PlaybackState.Stopped;
        return false;
      }
    }

    public byte[] HashCode
    {
      get
      {
        return this._hashCode;
      }
    }

    public DateTime TimeStamp
    {
      get
      {
        return this._timeStamp;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return this._duration;
      }
    }

    public string PlayerName
    {
      get
      {
        return this._playerName;
      }
    }

    public string ServerName
    {
      get
      {
        return this._serverName;
      }
    }

    public IPAddress IpAddress
    {
      get
      {
        return this._ipAddress;
      }
    }

    public TimeSpan Elapsed
    {
      get
      {
        return TimeSpan.FromMilliseconds((double) this._elapsed);
      }
    }

    public bool IsPlaying
    {
      get
      {
        return this._state == PlaybackState.Playing;
      }
    }

    public bool IsPaused
    {
      get
      {
        return this._state == PlaybackState.Paused;
      }
    }

    public PlaybackState State
    {
      get
      {
        return this._state;
      }
    }

    public Playback(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      this._stopwatch = new Stopwatch();
      this.Open(stream);
    }

    public static void Download(Uri uri)
    {
      WebRequest req = WebRequest.Create(uri);
      req.BeginGetResponse((AsyncCallback) (asyncResult =>
      {
        try
        {
          MemoryStream memoryStream = new MemoryStream();
          using (WebResponse response = req.EndGetResponse(asyncResult))
          {
            Stream responseStream = response.GetResponseStream();
            byte[] buffer = new byte[2048];
            int count;
            while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
              memoryStream.Write(buffer, 0, count);
            memoryStream.Seek(0L, SeekOrigin.Begin);
          }
          Playback video = new Playback((Stream) memoryStream);
          Engine.AddTextMessage("Download complete.");
          Gumps.Desktop.Children.Add((Gump) new VideoPlaybackGump(video));
          Playback._video = video;
        }
        catch (Exception ex)
        {
          Engine.AddTextMessage(ex.ToString());
        }
      }), (object) null);
    }

    public void Play()
    {
      if (this._state != PlaybackState.Stopped)
        return;
      this.Start();
      this._state = PlaybackState.Playing;
    }

    public void Pause()
    {
      if (this._state == PlaybackState.Playing)
      {
        this._stopwatch.Stop();
        this._state = PlaybackState.Paused;
      }
      else
      {
        if (this._state != PlaybackState.Paused)
          return;
        this._stopwatch.Start();
        this._state = PlaybackState.Playing;
      }
    }

    public void Start()
    {
      Cursor.Visible = false;
      Engine.Effects.Add((Fade) new Playback.FadeEffect(this));
    }

    private void BeginStreaming()
    {
      PlayUO.Debug.Trace(string.Format("{0}", (object) this._stopwatch));
      PlayUO.Debug.Trace(string.Format("{0}", (object) this._stream));
      if (this._stream == null)
        return;
      this._isStreaming = true;
      this._stream.Seek(this._origin, SeekOrigin.Begin);
      this.ReadInitialState();
    }

    public void Stop()
    {
      this._stopwatch.Stop();
      this._state = PlaybackState.Stopped;
      this.Dispose();
      if (Playback._video != this)
        return;
      Playback._video = (Playback) null;
    }

    private int GetTimeSample()
    {
      return this._speedBase + (int) (this._stopwatch.ElapsedMilliseconds * (long) this._speedNumerator / (long) this._speedDenominator);
    }

    public void UpdateSpeed(int numerator, int denominator)
    {
      if (this._speedNumerator == numerator && this._speedDenominator == denominator)
        return;
      this._speedBase = this.GetTimeSample();
      this._speedNumerator = numerator;
      this._speedDenominator = denominator;
      this._stopwatch.Reset();
      this._stopwatch.Start();
    }

    public void Cycle()
    {
      if (!this._isStreaming)
        return;
label_2:
      int index1;
      while (true)
      {
        if (!this._hasNext)
        {
          if (!this._stream.EndOfFile)
          {
            this._next = this._last + this._stream.Compressed.ReadInt32();
            this._last = this._next;
            this._hasNext = true;
          }
          else
            break;
        }
        this._elapsed = Math.Max(this.GetTimeSample(), this._elapsed);
        if (this._elapsed >= this._next)
        {
          this._hasNext = false;
          index1 = (int) this._stream.Compressed.ReadByte();
          if (index1 >= 0 && index1 < (int) byte.MaxValue)
          {
            PacketHandler packetHandler = PacketHandlers.m_Handlers[index1];
            if (packetHandler != null)
            {
              int length = packetHandler.Length;
              if (length < 0)
                length = (int) this._stream.Compressed.ReadByte() << 8 | (int) this._stream.Compressed.ReadByte();
              byte[] numArray1 = new byte[length];
              int num1 = 0;
              byte[] numArray2 = numArray1;
              int index2 = num1;
              int num2 = 1;
              int index3 = index2 + num2;
              int num3 = (int) (byte) index1;
              numArray2[index2] = (byte) num3;
              if (packetHandler.Length < 0)
              {
                byte[] numArray3 = numArray1;
                int index4 = index3;
                int num4 = 1;
                int num5 = index4 + num4;
                int num6 = (int) (byte) (length >> 8);
                numArray3[index4] = (byte) num6;
                byte[] numArray4 = numArray1;
                int index5 = num5;
                int num7 = 1;
                index3 = index5 + num7;
                int num8 = (int) (byte) length;
                numArray4[index5] = (byte) num8;
              }
              this._stream.Compressed.Read(numArray1, index3, length - index3);
              packetHandler.Handle(new PacketReader(numArray1, 0, numArray1.Length, packetHandler.Length >= 0, (byte) index1, packetHandler.Callback.Method.Name));
            }
            else
              goto label_14;
          }
          else
            goto label_22;
        }
        else
          goto label_1;
      }
      this.Stop();
      return;
label_1:
      return;
label_14:
      PlayUO.Debug.Trace("Bad packet {{ packetID: 0x{0:X2}; }}", (object) index1);
      try
      {
        while (!this._stream.EndOfFile)
        {
          int num = this._stream.Compressed.ReadInt32();
          if (num < 1000)
          {
            this._hasNext = true;
            this._next = this._elapsed + num;
          }
          else
            this._stream.Seek(-3L, SeekOrigin.Current);
        }
        goto label_2;
      }
      catch
      {
        this.Stop();
        return;
      }
label_22:
      this.Stop();
    }

    private void ReadInitialState()
    {
      int num = this._stream.Compressed.ReadInt32() + (int) this._stream.Position;
      World.Clear();
      Map.Invalidate();
      this.ReadMobile(this._stream.Compressed, true);
      while (this._stream.Position < (long) num)
      {
        switch (this._stream.Compressed.ReadByte())
        {
          case 0:
            this.ReadItem(this._stream.Compressed);
            continue;
          case 1:
            this.ReadMobile(this._stream.Compressed, false);
            continue;
          default:
            continue;
        }
      }
    }

    private Mobile ReadMobile(BinaryReader ip, bool isPlayer)
    {
      Mobile mobile = World.WantMobile(ip.ReadInt32());
      if (isPlayer)
        World.Serial = mobile.Serial;
      int x = ip.ReadInt32();
      int y = ip.ReadInt32();
      int z = ip.ReadInt32();
      if (isPlayer)
        World.SetLocation(x, y, z);
      mobile.SetLocation((Agent) World.Agent, x, y, z);
      mobile.Refresh = true;
      mobile.Hue = ip.ReadUInt16();
      mobile.Body = ip.ReadUInt16();
      mobile.Direction = ip.ReadByte();
      mobile.Name = ip.ReadString();
      mobile.Notoriety = (Notoriety) ip.ReadByte();
      mobile.Flags.Value = (int) ip.ReadByte();
      mobile.MaximumHitPoints = (int) ip.ReadUInt16();
      mobile.CurrentHitPoints = (int) ip.ReadUInt16();
      mobile.UpdateReal();
      int num1 = (int) ip.ReadByte();
      int num2 = ip.ReadInt32();
      while (num2-- > 0)
      {
        int num3 = (int) ip.ReadUInt32();
      }
      if (isPlayer)
      {
        mobile.Strength = (int) ip.ReadUInt16();
        mobile.Dexterity = (int) ip.ReadUInt16();
        mobile.Intelligence = (int) ip.ReadUInt16();
        mobile.MaximumStamina = (int) ip.ReadUInt16();
        mobile.CurrentStamina = (int) ip.ReadUInt16();
        mobile.MaximumMana = (int) ip.ReadUInt16();
        mobile.CurrentMana = (int) ip.ReadUInt16();
        int num4 = (int) ip.ReadByte();
        int num5 = (int) ip.ReadByte();
        int num6 = (int) ip.ReadByte();
        mobile.Gold = ip.ReadInt32();
        mobile.Weight = (int) ip.ReadUInt16();
        if (this._version < 4)
          throw new InvalidOperationException();
        int num7 = (int) ip.ReadByte();
        for (int index = 0; index < num7; ++index)
        {
          Skill skill = Engine.Skills[index];
          if (skill != null)
          {
            skill.Real = (float) ip.ReadUInt16() / 10f;
            int num8 = (int) ip.ReadUInt16();
            skill.Value = (float) ip.ReadUInt16() / 10f;
            skill.Lock = (SkillLock) ip.ReadByte();
          }
          else
          {
            int num8 = (int) ip.ReadInt16();
            int num9 = (int) ip.ReadInt16();
            int num10 = (int) ip.ReadInt16();
            int num11 = (int) ip.ReadByte();
          }
        }
        mobile.Armor = (int) ip.ReadUInt16();
        mobile.StatCap = (int) ip.ReadUInt16();
        mobile.FollowersCur = (int) ip.ReadByte();
        mobile.FollowersMax = (int) ip.ReadByte();
        mobile.TithingPoints = ip.ReadInt32();
        mobile.LightLevel = (int) ip.ReadSByte();
        Engine.Effects.GlobalLight = (int) ip.ReadByte();
        int num12 = (int) ip.ReadUInt16();
        int num13 = (int) ip.ReadByte();
        int num14 = (int) ip.ReadByte();
        while (num14-- > 0)
          ip.ReadInt32();
      }
      mobile.Refresh = false;
      mobile.Update();
      return mobile;
    }

    private Item ReadItem(BinaryReader ip)
    {
      Item obj = World.WantItem(ip.ReadInt32());
      int x = ip.ReadInt32();
      int y = ip.ReadInt32();
      int z = ip.ReadInt32();
      obj.Hue = ip.ReadUInt16();
      obj.ID = (int) ip.ReadInt16();
      obj.Amount = (int) ip.ReadInt16();
      obj.Direction = ip.ReadByte();
      obj.Flags.Value = (int) ip.ReadByte();
      obj.Layer = (Layer) ip.ReadByte();
      ip.ReadString();
      int serial = ip.ReadInt32();
      Agent parent = (Agent) World.Agent;
      if (serial >= 1073741824)
        parent = (Agent) World.WantItem(serial);
      else if (serial >= 1)
        parent = (Agent) World.WantMobile(serial);
      obj.SetLocation(parent, x, y, z);
      int num1 = ip.ReadInt32();
      while (num1-- > 0)
      {
        int num2 = (int) ip.ReadUInt32();
      }
      if (this._version > 2 && ip.ReadInt32() != 0)
        ip.ReadBytes((int) ip.ReadUInt16());
      return obj;
    }

    private void Open(Stream stream)
    {
      this._stream = new GZBlockIn(stream);
      this._version = (int) this._stream.Raw.ReadByte();
      if (this._version > 4)
      {
        this.Dispose();
        throw new VersionMismatchException();
      }
      this._stream.IsCompressed = this._version > 1;
      this._hashCode = this._stream.Raw.ReadBytes(16);
      this._timeStamp = DateTime.FromFileTime(this._stream.Raw.ReadInt64());
      this._duration = TimeSpan.FromMilliseconds((double) this._stream.Raw.ReadInt32());
      this._playerName = this._stream.Compressed.ReadString();
      this._serverName = this._stream.Compressed.ReadString();
      if (this._version > 1)
        this._ipAddress = new IPAddress((long) this._stream.Compressed.ReadUInt32());
      this._origin = this._stream.Position;
      long position = this._stream.RawStream.Position;
      this._stream.RawStream.Seek(17L, SeekOrigin.Begin);
      using (MD5 md5 = MD5.Create())
      {
        byte[] hash = md5.ComputeHash(this._stream.RawStream);
        for (int index = 0; index < hash.Length; ++index)
        {
          if ((int) hash[index] != (int) this.HashCode[index])
          {
            this.Dispose();
            throw new HashCodeMismatchException();
          }
        }
      }
      this._stream.RawStream.Seek(position, SeekOrigin.Begin);
    }

    public void Dispose()
    {
      if (this._stream == null)
        return;
      this._stream.Close();
      this._stream = (GZBlockIn) null;
    }

    public class FadeEffect : Fade
    {
      private Playback _video;

      public FadeEffect(Playback video)
        : base(0, 0.0f, 1f, 2f)
      {
        this._video = video;
      }

      protected internal override void OnFadeComplete()
      {
        this._video.BeginStreaming();
        Engine.Effects.Add((Fade) new Playback.MiddleFadeEffect(this._video, new string[5]
        {
          "",
          this._video.ServerName,
          "",
          this._video.TimeStamp.ToLocalTime().ToString("MMMM dd, yyyy"),
          ""
        }, 0));
      }
    }

    public class MiddleFadeEffect : Fade
    {
      private Playback _video;
      private GLabel m_Label;
      private string[] _text;
      private int _index;
      private int gx;
      private int gy;

      public MiddleFadeEffect(Playback video, string[] text, int index)
        : base(0, 1f, 1f, text[index] == "" ? 1f : 4f)
      {
        this._video = video;
        this._text = text;
        this._index = index;
        this.m_Label = new GLabel(this._text[this._index], (IFont) Engine.GetFont(4), Hues.Load(33921), 0, 0);
        this.m_Label.Alpha = 0.0f;
        this.m_Label.X = (Engine.GameWidth - this.m_Label.Width) / 2;
        this.m_Label.Y = (Engine.GameHeight - this.m_Label.Height) / 2;
        Gumps.Desktop.Children.Add((Gump) this.m_Label);
      }

      public override bool Evaluate(ref double Alpha)
      {
        int num1 = Engine.GameX - this.gx;
        int num2 = Engine.GameY - this.gy;
        this.m_Label.X += num1;
        this.m_Label.Y += num2;
        this.gx += num1;
        this.gy += num2;
        double normalized = this.m_Sync.Normalized;
        this.m_Label.Alpha = normalized >= 0.35 ? (normalized >= 0.7 ? (normalized >= 1.0 ? 0.0f : (float) ((1.0 - normalized) / 0.3)) : 1f) : (float) (normalized / 0.35);
        return base.Evaluate(ref Alpha);
      }

      protected internal override void OnFadeComplete()
      {
        Gumps.Destroy((Gump) this.m_Label);
        ++this._index;
        if (this._index < this._text.Length)
        {
          Engine.Effects.Add((Fade) new Playback.MiddleFadeEffect(this._video, this._text, this._index));
        }
        else
        {
          Cursor.Visible = true;
          this._video._stopwatch.Start();
          Engine.Effects.Add((Fade) new Playback.EndDeathEffect());
        }
      }
    }

    public class EndDeathEffect : Fade
    {
      public EndDeathEffect()
        : base(0, 1f, 0.0f, 1f)
      {
      }

      protected internal override void OnFadeComplete()
      {
      }
    }
  }
}
