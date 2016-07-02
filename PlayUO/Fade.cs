// Decompiled with JetBrains decompiler
// Type: PlayUO.Fade
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Fade
  {
    protected int m_Color;
    protected double m_From;
    protected double m_Delta;
    protected TimeSync m_Sync;

    public int Color
    {
      get
      {
        return this.m_Color;
      }
    }

    public Fade(int Color, float From, float To, float Duration)
    {
      this.m_Color = Color;
      this.m_From = (double) From;
      this.m_Delta = (double) To - (double) From;
      this.m_Sync = new TimeSync((double) Duration);
    }

    public virtual bool Evaluate(ref double Alpha)
    {
      double normalized = this.m_Sync.Normalized;
      Alpha = this.m_From + this.m_Delta * normalized;
      return normalized < 1.0;
    }

    protected internal virtual void OnFadeComplete()
    {
    }
  }
}
