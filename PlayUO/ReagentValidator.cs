// Decompiled with JetBrains decompiler
// Type: PlayUO.ReagentValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ReagentValidator : ItemIDValidator
  {
    public static readonly ReagentValidator Validator = new ReagentValidator();

    public ReagentValidator()
      : this((IItemValidator) null)
    {
    }

    public ReagentValidator(IItemValidator parent)
      : base(parent, 3962, 3963, 3972, 3973, 3974, 3976, 3980, 3981)
    {
    }
  }
}
