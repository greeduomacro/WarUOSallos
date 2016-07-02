// Decompiled with JetBrains decompiler
// Type: PlayUO.PlayerDistanceValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PlayerDistanceValidator : IItemValidator
  {
    private int m_Range;
    private IItemValidator m_Parent;

    public PlayerDistanceValidator(int range)
      : this((IItemValidator) null, range)
    {
    }

    public PlayerDistanceValidator(IItemValidator parent, int range)
    {
      this.m_Parent = parent;
      this.m_Range = range;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent == null || this.m_Parent.IsValid(check))
        return check.InRange((IPoint2D) World.Player, this.m_Range);
      return false;
    }
  }
}
