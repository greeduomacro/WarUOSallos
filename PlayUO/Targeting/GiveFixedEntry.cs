// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.GiveFixedEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  public sealed class GiveFixedEntry : GiveEntry
  {
    private int m_Amount;

    public GiveFixedEntry(string name, int amount, params IItemValidator[] validators)
      : base(name, validators)
    {
      this.m_Amount = amount;
    }

    public override int GetAmount(int currentAmount)
    {
      return this.m_Amount;
    }
  }
}
