// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.GiveRatioEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Targeting
{
  public sealed class GiveRatioEntry : GiveEntry
  {
    private int m_Ratio;

    public GiveRatioEntry(string name, int ratio, params IItemValidator[] validators)
      : base(name, validators)
    {
      this.m_Ratio = ratio;
    }

    public override int GetAmount(int currentAmount)
    {
      return Math.Max(1, (currentAmount * this.m_Ratio + 50) / 100);
    }
  }
}
