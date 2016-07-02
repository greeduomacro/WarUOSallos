// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.AddAutoUseTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO.Targeting
{
  internal class AddAutoUseTargetHandler : ClientTargetHandler
  {
    protected override bool OnTarget(Item item)
    {
      UseOnceAgent useOnceAgent = Player.Current.UseOnceAgent;
      ItemRef itemRef = useOnceAgent[item];
      if (itemRef != null)
      {
        item.OverrideHue(-1);
        useOnceAgent.Items.Remove(itemRef);
      }
      else
      {
        item.OverrideHue(34);
        useOnceAgent.Items.Add(new ItemRef(item));
      }
      return true;
    }
  }
}
