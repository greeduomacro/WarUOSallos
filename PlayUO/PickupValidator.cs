// Decompiled with JetBrains decompiler
// Type: PlayUO.PickupValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PickupValidator : IItemValidator
  {
    private IItemValidator m_Parent;

    public PickupValidator()
      : this((IItemValidator) null)
    {
    }

    public PickupValidator(IItemValidator parent)
    {
      this.m_Parent = parent;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent != null && !this.m_Parent.IsValid(check))
        return false;
      return check.IsMovable;
    }
  }
}
