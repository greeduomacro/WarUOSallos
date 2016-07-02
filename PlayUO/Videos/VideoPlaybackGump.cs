// Decompiled with JetBrains decompiler
// Type: PlayUO.Videos.VideoPlaybackGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Drawing;

namespace PlayUO.Videos
{
  public sealed class VideoPlaybackGump : Gump
  {
    private Playback _video;
    private Texture _texture;

    public override int Width
    {
      get
      {
        if (this._texture == null)
          return 0;
        return this._texture.Width;
      }
      set
      {
      }
    }

    public override int Height
    {
      get
      {
        if (this._texture == null)
          return 0;
        return this._texture.Height;
      }
      set
      {
      }
    }

    public VideoPlaybackGump(Playback video)
      : base(0, 0)
    {
      this._video = video;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this._texture = Engine.LoadArchivedTexture("video-window.png");
      this.Children.Add((Gump) new VideoPlaybackGump.VideoSpeedBar(this));
      this.Children.Add((Gump) new VideoPlaybackGump.VideoTimeLabel(this));
      this.Children.Add((Gump) new VideoPlaybackGump.VideoProgressBar(this));
      using (Bitmap buttonSource = Engine.LoadArchivedBitmap("video-buttons.png"))
      {
        using (Bitmap iconSource = Engine.LoadArchivedBitmap("video-button-status.png"))
        {
          for (int type = 1; type < 4; ++type)
            this.Children.Add((Gump) new VideoPlaybackGump.VideoPlaybackButton(this, buttonSource, iconSource, type));
        }
      }
    }

    protected internal override void Draw(int X, int Y)
    {
      this._texture.Draw(X, Y);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (X >= 0 && Y >= 0 && X < this.Width)
        return Y < this.Height;
      return false;
    }

    protected internal override void OnDispose()
    {
      base.OnDispose();
      if (this._texture != null)
      {
        this._texture.Dispose();
        this._texture = (Texture) null;
      }
      if (this._video == null)
        return;
      this._video.Dispose();
      this._video = (Playback) null;
    }

    private sealed class VideoPlaybackButton : GButtonNew
    {
      private static PlaybackState[] states = new PlaybackState[3]{ PlaybackState.Playing, PlaybackState.Paused, PlaybackState.Stopped };
      private VideoPlaybackGump _owner;
      private int _type;
      private Texture[] _textures;
      private Texture _activeIcon;

      public VideoPlaybackButton(VideoPlaybackGump owner, Bitmap buttonSource, Bitmap iconSource, int type)
        : base(0, 0, 0, 0, 0)
      {
        this._owner = owner;
        this._type = type;
        this._textures = new Texture[3];
        int x = type * 30;
        this.X = 6 + x;
        this.Y = 94;
        int[] numArray = new int[3]{ 40, 120, 80 };
        for (int index = 0; index < this._textures.Length; ++index)
        {
          int y = numArray[index];
          using (Bitmap bitmap = new Bitmap(30, 30))
          {
            using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            {
              if (y != 0)
                graphics.DrawImage((Image) buttonSource, new Rectangle(0, 0, 30, 30), new Rectangle(x, 0, 30, 30), GraphicsUnit.Pixel);
              graphics.DrawImage((Image) buttonSource, new Rectangle(0, 0, 30, 30), new Rectangle(x, y, 30, 30), GraphicsUnit.Pixel);
            }
            this._textures[index] = Texture.FromBitmap(bitmap);
          }
        }
        using (Bitmap bitmap = new Bitmap(30, 30))
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            graphics.DrawImage((Image) iconSource, new Rectangle(0, 0, 30, 30), new Rectangle(type * 30, (type - 1) * 40, 30, 30), GraphicsUnit.Pixel);
          this._activeIcon = Texture.FromBitmap(bitmap);
        }
      }

      protected override void OnClicked()
      {
        base.OnClicked();
        switch (this._type)
        {
          case 1:
            this._owner._video.Play();
            break;
          case 2:
            this._owner._video.Pause();
            break;
          case 3:
            this._owner._video.Stop();
            break;
        }
      }

      protected internal override void Draw(int x, int y)
      {
        base.Draw(x, y);
        if (this.m_State >= 2 || this._owner._video.State != VideoPlaybackGump.VideoPlaybackButton.states[this._type - 1])
          return;
        this._activeIcon.Draw(x, y);
      }

      protected override void Refresh()
      {
        this.m_Image = this._textures[this.m_State];
        if (this.m_Image != null && !this.m_Image.IsEmpty())
        {
          this.m_Width = this.m_Image.Width;
          this.m_Height = this.m_Image.Height;
          this.m_Draw = true;
        }
        else
        {
          this.m_Width = 0;
          this.m_Height = 0;
          this.m_Draw = false;
        }
        this.m_Invalidated = false;
        if (this.m_vCache == null)
          return;
        this.m_vCache.Invalidate();
      }

      protected internal override void OnDispose()
      {
        base.OnDispose();
        if (this._textures != null)
        {
          for (int index = 0; index < this._textures.Length; ++index)
            this._textures[index].Dispose();
          this._textures = (Texture[]) null;
        }
        if (this._activeIcon == null)
          return;
        this._activeIcon.Dispose();
        this._activeIcon = (Texture) null;
      }
    }

    private sealed class VideoTimeLabel : Gump
    {
      private static int[] mods = new int[4]{ 10, 6, 10, 6 };
      private VideoPlaybackGump _owner;
      private Texture[] _textures;
      private int[] digits;

      public override int Width
      {
        get
        {
          return 100;
        }
        set
        {
        }
      }

      public override int Height
      {
        get
        {
          return 20;
        }
        set
        {
        }
      }

      public VideoTimeLabel(VideoPlaybackGump owner)
        : base(36, 24)
      {
        this._owner = owner;
        this._textures = Texture.FromImageSet("play/images/video-time-font.png", 13, 20, 16, 1);
        int num1 = (int) Math.Ceiling(this._owner._video.Duration.TotalSeconds);
        int length = 3;
        int num2 = num1 / 60;
        if (num2 > 9)
        {
          ++length;
          int num3 = num2 / 60;
          if (num3 > 0)
            length += (num3 + 9) / 10;
        }
        this.digits = new int[length];
      }

      protected internal override void Draw(int X, int Y)
      {
        TimeSpan timeSpan = this._owner._video.State != PlaybackState.Stopped ? this._owner._video.Elapsed : this._owner._video.Duration;
        if (timeSpan < TimeSpan.Zero)
          timeSpan = TimeSpan.Zero;
        int num1 = (int) timeSpan.TotalSeconds;
        for (int index = 0; index < this.digits.Length; ++index)
        {
          int num2 = index < VideoPlaybackGump.VideoTimeLabel.mods.Length ? VideoPlaybackGump.VideoTimeLabel.mods[index] : 10;
          this.digits[index] = num1 % num2;
          num1 /= num2;
        }
        int num3 = this.digits.Length * 13;
        if (this.digits.Length > 2)
        {
          num3 += 4;
          if (this.digits.Length > 4)
            num3 += 4;
        }
        int num4 = num3;
        for (int index = 0; index < this.digits.Length; ++index)
        {
          if (index == 2 || index == 4)
          {
            Texture texture = this._textures[12];
            num4 -= 5;
            texture.Draw(X + num4, Y);
          }
          Texture texture1 = this._textures[this.digits[index]];
          num4 -= 13;
          texture1.Draw(X + num4, Y);
        }
      }

      protected internal override void OnDispose()
      {
        base.OnDispose();
        if (this._textures == null)
          return;
        for (int index = 0; index < this._textures.Length; ++index)
          this._textures[index].Dispose();
        this._textures = (Texture[]) null;
      }

      private Texture Extract(Bitmap source, int x, int y, int width, int height)
      {
        using (Bitmap bitmap = new Bitmap(width, height))
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            graphics.DrawImage((Image) source, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
          return Texture.FromBitmap(bitmap);
        }
      }
    }

    private sealed class VideoSpeedBar : GThumbSlider
    {
      private VideoPlaybackGump _owner;
      private Texture[] _textures;

      public override int Width
      {
        get
        {
          return 9;
        }
        set
        {
        }
      }

      public override int Height
      {
        get
        {
          return 76;
        }
        set
        {
        }
      }

      public VideoSpeedBar(VideoPlaybackGump owner)
        : base(343, 11, SliderOrientation.Vertical)
      {
        this.Minimum = -5;
        this.Maximum = 5;
        this._owner = owner;
        this._textures = new Texture[2];
        using (Bitmap source = Engine.LoadArchivedBitmap("video-rate-slider.png"))
        {
          this._textures[0] = this.Extract(source, 0, 0, 16, 23);
          this._textures[1] = this.Extract(source, 18, 0, 16, 23);
        }
      }

      private Texture Extract(Bitmap source, int x, int y, int width, int height)
      {
        using (Bitmap bitmap = new Bitmap(width, height))
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            graphics.DrawImage((Image) source, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
          return Texture.FromBitmap(bitmap);
        }
      }

      protected override void OnChanged(int oldValue)
      {
        base.OnChanged(oldValue);
        int num = -this.Value;
        if (num >= 0)
          this._owner._video.UpdateSpeed(1 + num, 1);
        else
          this._owner._video.UpdateSpeed(1, 1 - num);
      }

      protected override int GetThumbSize()
      {
        return 19;
      }

      protected override void DrawThumb(int x, int y, int p)
      {
        this._textures[0].Draw(x - 2, y + p - 1);
      }

      protected internal override void OnDispose()
      {
        base.OnDispose();
        if (this._textures == null)
          return;
        for (int index = 0; index < this._textures.Length; ++index)
          this._textures[index].Dispose();
        this._textures = (Texture[]) null;
      }
    }

    private sealed class VideoProgressBar : Gump
    {
      private VideoPlaybackGump _owner;
      private Texture[] _textures;

      public override int Width
      {
        get
        {
          return 301;
        }
        set
        {
        }
      }

      public override int Height
      {
        get
        {
          return 4;
        }
        set
        {
        }
      }

      public VideoProgressBar(VideoPlaybackGump owner)
        : base(9, 82)
      {
        this._owner = owner;
        this._textures = new Texture[3];
        using (Bitmap source = Engine.LoadArchivedBitmap("video-progress.png"))
        {
          this._textures[0] = this.Extract(source, 0, 0, 2, source.Height);
          this._textures[1] = this.Extract(source, 2, 0, source.Width - 4, source.Height);
          this._textures[2] = this.Extract(source, source.Width - 2, 0, 2, source.Height);
        }
      }

      private Texture Extract(Bitmap source, int x, int y, int width, int height)
      {
        using (Bitmap bitmap = new Bitmap(width, height))
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            graphics.DrawImage((Image) source, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
          return Texture.FromBitmap(bitmap);
        }
      }

      protected internal override void Draw(int x, int y)
      {
        Playback playback = this._owner._video;
        int num = (int) (playback.Elapsed.Ticks * (long) this.Width / playback.Duration.Ticks);
        if (num > this.Width)
          num = this.Width;
        if (num <= 4)
          return;
        this._textures[0].Draw(x, y);
        this._textures[1].Draw(x + 2, y, num - 4, this._textures[1].Height);
        this._textures[2].Draw(x + num - 2, y);
      }

      protected internal override void OnDispose()
      {
        base.OnDispose();
        if (this._textures == null)
          return;
        for (int index = 0; index < this._textures.Length; ++index)
          this._textures[index].Dispose();
        this._textures = (Texture[]) null;
      }
    }
  }
}
