// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.SetEquipTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO.Targeting
{
  internal class SetEquipTargetHandler : ClientTargetHandler
  {
    private int m_Index;

    public SetEquipTargetHandler(int index)
    {
      this.m_Index = index;
    }

    protected override bool OnTarget(Item item)
    {
      if (item.IsWearable)
      {
        switch (Map.GetQuality(item.ID))
        {
          case 1:
          case 2:
            Player.Current.EquipAgent.Arms.Assign(this.m_Index, item);
            return true;
        }
      }
      return false;
    }
  }
}
