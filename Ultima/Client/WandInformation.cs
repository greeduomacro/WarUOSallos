// Decompiled with JetBrains decompiler
// Type: Ultima.Client.WandInformation
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace Ultima.Client
{
  internal struct WandInformation
  {
    private readonly WandEffect effect;
    private readonly int charges;

    public WandEffect Effect
    {
      get
      {
        return this.effect;
      }
    }

    public int Charges
    {
      get
      {
        return this.charges;
      }
    }

    public WandInformation(WandEffect effect, int charges)
    {
      this.effect = effect;
      this.charges = charges;
    }

    public static WandEffect? GetEffectByLabel(int number)
    {
      switch (number)
      {
        case 3002022:
          return new WandEffect?(WandEffect.Harming);
        case 3002028:
          return new WandEffect?(WandEffect.Fireball);
        case 3002039:
          return new WandEffect?(WandEffect.GreaterHealing);
        case 3002040:
          return new WandEffect?(WandEffect.Lightning);
        case 3002041:
          return new WandEffect?(WandEffect.ManaDraining);
        case 1044063:
          return new WandEffect?(WandEffect.Identification);
        case 3002011:
          return new WandEffect?(WandEffect.Clumsiness);
        case 3002013:
          return new WandEffect?(WandEffect.Feeblemindedness);
        case 3002014:
          return new WandEffect?(WandEffect.Healing);
        case 3002015:
          return new WandEffect?(WandEffect.MagicArrow);
        case 3002018:
          return new WandEffect?(WandEffect.Weakness);
        default:
          return new WandEffect?();
      }
    }
  }
}
