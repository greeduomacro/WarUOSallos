// Decompiled with JetBrains decompiler
// Type: PlayUO.GVirtueTrigger
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GVirtueTrigger : GClickable
  {
    private Mobile m_Mobile;

    public GVirtueTrigger(Mobile m)
      : base(80, 4, 113)
    {
      this.m_Mobile = m;
      this.Tooltip = (ITooltip) new Tooltip("Virtue System");
    }

    protected override void OnDoubleClicked()
    {
      Network.Send((Packet) new PVirtueTrigger(this.m_Mobile));
    }
  }
}
