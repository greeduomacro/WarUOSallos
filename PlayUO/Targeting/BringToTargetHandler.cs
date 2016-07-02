// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.BringToTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class BringToTargetHandler : ClientTargetHandler
  {
    private int m_xOffset;
    private int m_yOffset;

    public BringToTargetHandler(int xOffset, int yOffset)
    {
      this.m_xOffset = xOffset;
      this.m_yOffset = yOffset;
    }

    protected override bool OnTarget(Item item)
    {
      Mobile player = World.Player;
      if (player != null)
      {
        Item backpack = player.Backpack;
        if (backpack != null)
        {
          if (item.InWorld)
            return false;
          int x = item.X + this.m_xOffset;
          int y = item.Y + this.m_yOffset;
          foreach (Item pickUp in backpack.GetItems((IItemValidator) new ItemIDValidator(new int[1]{ item.ID })))
          {
            if (pickUp != item)
            {
              if (pickUp.X != x || pickUp.Y != y)
              {
                MoveContext moveContext = new MoveContext(pickUp, pickUp.Amount, (IEntity) item.Parent, false);
                moveContext.Locate(x, y);
                moveContext.Enqueue();
              }
              x += this.m_xOffset;
              y += this.m_yOffset;
            }
          }
        }
      }
      return true;
    }
  }
}
