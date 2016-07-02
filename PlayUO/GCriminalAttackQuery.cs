// Decompiled with JetBrains decompiler
// Type: PlayUO.GCriminalAttackQuery
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GCriminalAttackQuery : GMessageBoxYesNo
  {
    private Mobile m_Mobile;

    public GCriminalAttackQuery(Mobile m)
      : base("This may flag\nyou criminal!", true, (MBYesNoCallback) null)
    {
      this.m_Mobile = m;
    }

    protected override void OnSignal(bool response)
    {
      if (!response)
        return;
      this.m_Mobile.Attack();
    }
  }
}
