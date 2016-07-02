// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.AcquireTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class AcquireTargetHandler : ClientTargetHandler
  {
    public override AggressionType Aggression
    {
      get
      {
        return AggressionType.Offensive;
      }
    }

    protected override bool OnTarget(Mobile mob)
    {
      mob.AddTextMessage("", "- last target set -", Engine.DefaultFont, Hues.Load(89), false);
      if (Party.State == PartyState.Joined)
      {
        string identifier = mob.Identifier;
        if (identifier != null)
          Party.SendAutomatedMessage("Changing last target to {0}", (object) identifier);
      }
      return true;
    }

    protected override bool OnTarget(Item item)
    {
      item.AddTextMessage("", "- last target set -", Engine.DefaultFont, Hues.Load(89), false);
      return true;
    }

    protected override bool OnTarget(GroundTarget groundTarget)
    {
      Engine.AddTextMessage("Last target set.", Engine.DefaultFont, Hues.Load(89));
      return true;
    }

    protected override bool OnTarget(StaticTarget staticTarget)
    {
      Engine.AddTextMessage("Last target set.", Engine.DefaultFont, Hues.Load(89));
      return true;
    }
  }
}
