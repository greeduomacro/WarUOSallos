// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.SetOrganizeTemplateTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Ultima.Data;

namespace PlayUO.Targeting
{
  internal class SetOrganizeTemplateTargetHandler : ClientTargetHandler
  {
    private readonly bool m_Invoking;

    public SetOrganizeTemplateTargetHandler(bool invoking)
    {
      this.m_Invoking = invoking;
    }

    protected override bool OnTarget(Item item)
    {
      if (item != null && Map.m_ItemFlags[item.ID & 16383][(TileFlag) 2097152L])
      {
        if (item.IsChildOf((Agent) World.Player) || item.InRange((IPoint2D) World.Player, 2))
        {
          Player.Current.OrganizeAgent.SetTemplate(item);
          if (this.m_Invoking)
            Player.Current.OrganizeAgent.Invoke();
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
