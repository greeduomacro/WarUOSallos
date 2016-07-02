// Decompiled with JetBrains decompiler
// Type: PlayUO.PredicateValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class PredicateValidator : IItemValidator
  {
    private IItemValidator m_Parent;
    private Predicate<Item> predicate;

    public PredicateValidator(Predicate<Item> predicate)
      : this((IItemValidator) null, predicate)
    {
    }

    public PredicateValidator(IItemValidator parent, Predicate<Item> predicate)
    {
      this.m_Parent = parent;
      this.predicate = predicate;
    }

    public bool IsValid(Item check)
    {
      if (this.m_Parent != null && !this.m_Parent.IsValid(check))
        return false;
      return this.predicate(check);
    }
  }
}
