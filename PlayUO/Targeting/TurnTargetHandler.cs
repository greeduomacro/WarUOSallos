// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.TurnTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Targeting
{
  internal class TurnTargetHandler : ClientTargetHandler
  {
    protected override bool OnTarget(object targeted)
    {
      IPoint3D point3D = targeted as IPoint3D;
      if (point3D != null)
      {
        Mobile player = World.Player;
        if (player != null)
        {
          int dir = (int) Engine.GetWalkDirection(Engine.GetDirection(player.X, player.Y, point3D.X, point3D.Y)) & 7;
          if (((int) player.Direction & 7) != dir)
          {
            player.Direction = (byte) dir;
            Engine.SendMovementRequest(dir, player.X, player.Y, player.Z, TimeSpan.FromSeconds(0.1));
          }
        }
      }
      return true;
    }
  }
}
