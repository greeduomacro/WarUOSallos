// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.MultiTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class MultiTargetHandler : ServerTargetHandler
  {
    public MultiTargetHandler(int targetID)
      : base(targetID, true, AggressionType.Neutral)
    {
    }

    protected override bool OnTarget(Mobile mob)
    {
      return false;
    }

    protected override bool OnTarget(Item item)
    {
      return false;
    }

    protected override void Dispatch(int type, int serial, int x, int y, int z, int id)
    {
      Network.Send((Packet) new PMultiTarget_Response(this.targetID, x, y, z, id));
      Engine.m_MultiPreview = false;
    }

    protected override void OnCancel()
    {
      Engine.m_MultiPreview = false;
      Network.Send((Packet) new PMultiTarget_Cancel(this.targetID));
    }
  }
}
