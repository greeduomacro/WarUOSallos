// Decompiled with JetBrains decompiler
// Type: PlayUO.TimeDelay
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class TimeDelay
  {
    private int m_End;
    private int m_Duration;

    public bool Elapsed
    {
      get
      {
        return Engine.Ticks >= this.m_End;
      }
    }

    public TimeDelay(float Duration)
    {
      this.m_Duration = (int) ((double) Duration * 1000.0);
      this.m_End = Engine.Ticks + this.m_Duration;
    }

    public TimeDelay(int msDuration)
    {
      this.m_Duration = msDuration;
      this.m_End = Engine.Ticks + this.m_Duration;
    }

    public void Reset()
    {
      this.m_End = Engine.Ticks + this.m_Duration;
    }

    public bool ElapsedReset()
    {
      int ticks = Engine.Ticks;
      if (ticks < this.m_End)
        return false;
      this.m_End = ticks + this.m_Duration;
      return true;
    }
  }
}
