// Decompiled with JetBrains decompiler
// Type: PlayUO.GBandageTimer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GBandageTimer : GLabel
  {
    private static bool m_Active;
    private static DateTime m_Start;

    public override int Y
    {
      get
      {
        return Stats.yOffset;
      }
      set
      {
      }
    }

    public GBandageTimer()
      : base("", Engine.DefaultFont, Engine.DefaultHue, 4, 4)
    {
    }

    public static void Start()
    {
      GBandageTimer.m_Active = true;
      GBandageTimer.m_Start = DateTime.Now;
    }

    public static void Stop()
    {
      GBandageTimer.m_Active = false;
    }

    protected internal override void Render(int X, int Y)
    {
      if (!GBandageTimer.m_Active || !Engine.m_Ingame)
        return;
      TimeSpan timeSpan = DateTime.Now - GBandageTimer.m_Start;
      if (timeSpan >= TimeSpan.FromSeconds(20.0))
      {
        GBandageTimer.m_Active = false;
      }
      else
      {
        this.Text = string.Format("Bandage: {0} seconds elapsed", (object) (int) timeSpan.TotalSeconds);
        base.Render(X, Y);
        Stats.Add((Gump) this);
      }
    }
  }
}
