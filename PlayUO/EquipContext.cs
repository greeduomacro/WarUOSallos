// Decompiled with JetBrains decompiler
// Type: PlayUO.EquipContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class EquipContext : MoveContext
  {
    public EquipContext(Item pickUp, int amount, Mobile dropTo, bool clickFirst)
      : base(pickUp, amount, (IEntity) dropTo, clickFirst)
    {
    }

    protected override void SendDropPacket()
    {
      Network.Send((Packet) new PEquipItem(this.pickUp, this.dropTo as Mobile));
    }
  }
}
