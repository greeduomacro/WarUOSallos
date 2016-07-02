// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.FriendTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class FriendTargetHandler : ClientTargetHandler
  {
    protected override bool OnTarget(Mobile mob)
    {
      FriendContext friendContext = new FriendContext(mob);
      if (mob.HasName)
        friendContext.OnFinish();
      else
        friendContext.Dispatch();
      return true;
    }

    public void OnCancel(TargetCancelType type)
    {
    }
  }
}
