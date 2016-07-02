// Decompiled with JetBrains decompiler
// Type: PlayUO.GPingDisplay
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GPingDisplay : GLabel
  {
    private IHue[] m_Hues;

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

    public GPingDisplay()
      : base("", Engine.DefaultFont, Engine.DefaultHue, 4, 4)
    {
      this.m_Hues = new IHue[7];
      this.m_Hues[0] = Hues.Load(68);
      this.m_Hues[1] = Hues.Load(63);
      this.m_Hues[2] = Hues.Load(58);
      this.m_Hues[3] = Hues.Load(53);
      this.m_Hues[4] = Hues.Load(48);
      this.m_Hues[5] = Hues.Load(43);
      this.m_Hues[6] = Hues.Load(38);
    }

    protected internal override void Render(int X, int Y)
    {
      if (!Engine.m_Ingame || !Renderer.DrawPing)
        return;
      int ping = Engine.Ping;
      string str = (ping / 5 * 5).ToString();
      int index = (ping - 25) / 75;
      if (index < 0)
        index = 0;
      else if (index > 6)
        index = 6;
      if (ping < 5)
        str = "below 5";
      else if (ping > 5000)
        str = "over 5000";
      this.Hue = this.m_Hues[index];
      this.Text = string.Format("Ping: {0}", (object) str);
      base.Render(X, Y);
      Stats.Add((Gump) this);
    }
  }
}
