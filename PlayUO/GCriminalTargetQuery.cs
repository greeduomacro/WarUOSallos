// Decompiled with JetBrains decompiler
// Type: PlayUO.GCriminalTargetQuery
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;

namespace PlayUO
{
  internal class GCriminalTargetQuery : GMessageBoxYesNo
  {
    private Mobile m_Mobile;
    private ServerTargetHandler m_Handler;

    public GCriminalTargetQuery(Mobile m, ServerTargetHandler handler)
      : base("This may flag\nyou criminal!", true, (MBYesNoCallback) null)
    {
      this.m_Mobile = m;
      this.m_Handler = handler;
    }

    protected override void OnSignal(bool response)
    {
      if (response)
        Network.Send((Packet) new PTarget_Response(0, this.m_Handler, this.m_Mobile.Serial, this.m_Mobile.X, this.m_Mobile.Y, this.m_Mobile.Z, (int) this.m_Mobile.Body));
      else
        Network.Send((Packet) new PTarget_Cancel(this.m_Handler));
    }
  }
}
