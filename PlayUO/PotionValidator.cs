// Decompiled with JetBrains decompiler
// Type: PlayUO.PotionValidator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PotionValidator : ItemIDValidator
  {
    public static readonly PotionValidator Validator = new PotionValidator();

    public PotionValidator()
      : this((IItemValidator) null)
    {
    }

    public PotionValidator(IItemValidator parent)
      : base(parent, 3847, 3848, 3849, 3851, 3852, 3853)
    {
    }
  }
}
