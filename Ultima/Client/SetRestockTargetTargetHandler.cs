// Decompiled with JetBrains decompiler
// Type: Ultima.Client.SetRestockTargetTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO;
using PlayUO.Profiles;
using PlayUO.Targeting;
using Ultima.Data;

namespace Ultima.Client
{
  internal class SetRestockTargetTargetHandler : ClientTargetHandler
  {
    private readonly bool invoking;

    public SetRestockTargetTargetHandler(bool invoking)
    {
      this.invoking = invoking;
    }

    protected override bool OnTarget(Item item)
    {
      if (item != null && Map.m_ItemFlags[item.ID & 16383][(TileFlag) 2097152L])
      {
        if (item.IsChildOf((Agent) World.Player))
        {
          RestockAgent restockAgent = Player.Current.RestockAgent;
          restockAgent.TargetContainer = new ItemRef(item);
          if (this.invoking)
            restockAgent.Invoke();
          return true;
        }
        Engine.AddTextMessage("Container must be on your person.");
        return false;
      }
      Engine.AddTextMessage("Target a container.");
      return false;
    }
  }
}
