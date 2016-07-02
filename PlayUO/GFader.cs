// Decompiled with JetBrains decompiler
// Type: PlayUO.GFader
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GFader : GDragable
  {
    protected static bool m_Fade;
    private float m_fDuration;
    private float m_fFadeInDuration;
    private float m_fFadeTo;
    private float m_FadeTo;
    private float m_FadeFrom;
    private FadeState m_State;
    private TimeSync m_Sync;
    private Timer m_Timer;

    public static bool Fade
    {
      get
      {
        return GFader.m_Fade;
      }
      set
      {
        GFader.m_Fade = value;
      }
    }

    public FadeState State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        if (this.m_State == value)
          return;
        switch (value)
        {
          case FadeState.Faded:
            this.Alpha = this.m_fFadeTo;
            if (this.m_Timer != null)
              this.m_Timer.Delete();
            this.m_Sync = (TimeSync) null;
            this.m_Timer = (Timer) null;
            break;
          case FadeState.Opaque:
            this.Alpha = 1f;
            if (this.m_Timer != null)
              this.m_Timer.Delete();
            this.m_Sync = (TimeSync) null;
            this.m_Timer = (Timer) null;
            break;
          case FadeState.F2O:
            this.m_FadeTo = 1f;
            this.m_FadeFrom = this.Alpha;
            if (this.m_Timer != null)
              this.m_Timer.Delete();
            this.m_Timer = new Timer(new OnTick(this.Fade_OnTick), 0);
            this.m_Sync = new TimeSync((double) this.m_fFadeInDuration);
            this.m_Timer.Start(true);
            break;
          case FadeState.O2F:
            this.m_FadeTo = this.m_fFadeTo;
            this.m_FadeFrom = this.Alpha;
            if (this.m_Timer != null)
              this.m_Timer.Delete();
            this.m_Timer = new Timer(new OnTick(this.Fade_OnTick), 0);
            this.m_Sync = new TimeSync((double) this.m_fDuration);
            this.m_Timer.Start(true);
            break;
        }
        this.m_State = value;
      }
    }

    public GFader(float Duration, float FadeInDuration, float FadeTo, int GumpID, int X, int Y)
      : this(Duration, FadeInDuration, FadeTo, GumpID, X, Y, Hues.Default)
    {
    }

    public GFader(float Duration, float FadeInDuration, float FadeTo, int GumpID, int X, int Y, IHue h)
      : base(GumpID, h, X, Y)
    {
      this.m_fDuration = Duration;
      this.m_fFadeTo = FadeTo;
      this.m_fFadeInDuration = FadeInDuration;
      this.State = FadeState.O2F;
    }

    protected internal override void Render(int X, int Y)
    {
      if (!this.m_Visible)
        return;
      if (!GFader.m_Fade)
      {
        this.Alpha = 1f;
        this.m_State = FadeState.Opaque;
        base.Render(X, Y);
      }
      else
      {
        int X1 = X + this.X;
        int Y1 = Y + this.Y;
        this.Draw(X1, Y1);
        foreach (Gump gump in this.m_Children.ToArray())
        {
          if (gump.m_ITranslucent)
          {
            ITranslucent translucent = (ITranslucent) gump;
            float alpha = translucent.Alpha;
            translucent.Alpha *= this.m_fAlpha;
            gump.Render(X1, Y1);
            translucent.Alpha = alpha;
          }
          else
            gump.Render(X1, Y1);
        }
      }
    }

    protected void Fade_OnTick(Timer t)
    {
      if (this.m_Sync != null)
      {
        double normalized = this.m_Sync.Normalized;
        this.Alpha = this.m_FadeFrom + (float) (((double) this.m_FadeTo - (double) this.m_FadeFrom) * normalized);
        if (normalized < 1.0)
          return;
        this.m_Sync = (TimeSync) null;
        if (this.State == FadeState.F2O)
        {
          this.State = FadeState.Opaque;
        }
        else
        {
          if (this.State != FadeState.O2F)
            return;
          this.State = FadeState.Faded;
        }
      }
      else
      {
        t.Delete();
        this.m_Timer = (Timer) null;
      }
    }

    protected internal override void Draw(int X, int Y)
    {
      if (GFader.m_Fade)
      {
        if (Gumps.LastOver == null || !Gumps.LastOver.IsChildOf((Gump) this))
        {
          if (this.State != FadeState.O2F && this.State != FadeState.Faded)
            this.State = FadeState.O2F;
        }
        else if (this.State != FadeState.F2O && this.State != FadeState.Opaque)
          this.State = FadeState.F2O;
      }
      base.Draw(X, Y);
    }
  }
}
