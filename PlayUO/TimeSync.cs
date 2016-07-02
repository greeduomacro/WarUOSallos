// Decompiled with JetBrains decompiler
// Type: PlayUO.TimeSync
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class TimeSync
  {
    private double m_Duration;
    private double m_Start;
    private double m_rSeconds;

    public double Elapsed
    {
      get
      {
        double num1 = Engine.m_dTicks;
        if (!Engine.m_SetTicks)
          num1 = Engine.UpdateTicks();
        double num2 = (num1 - this.m_Start) / 1000.0;
        if (num2 < 0.0)
          num2 = 0.0;
        return num2;
      }
    }

    public double Normalized
    {
      get
      {
        double num1 = Engine.m_dTicks;
        if (!Engine.m_SetTicks)
          num1 = Engine.UpdateTicks();
        double num2 = (num1 - this.m_Start) * this.m_rSeconds;
        if (num2 < 0.0)
          num2 = 0.0;
        else if (num2 > 1.0)
          num2 = 1.0;
        return num2;
      }
    }

    public TimeSync(double duration)
    {
      this.Initialize(duration);
    }

    public void Initialize(double duration)
    {
      this.m_Duration = duration;
      this.m_Start = Engine.m_dTicks;
      if (!Engine.m_SetTicks)
        this.m_Start = Engine.UpdateTicks();
      this.m_rSeconds = 1.0 / (duration * 1000.0);
    }
  }
}
