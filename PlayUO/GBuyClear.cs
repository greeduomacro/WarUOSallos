// Decompiled with JetBrains decompiler
// Type: PlayUO.GBuyClear
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GBuyClear : GRegion
  {
    private GBuyGump m_Owner;

    public GBuyClear(GBuyGump owner)
      : base(169, 199, 55, 35)
    {
      this.m_Owner = owner;
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      this.m_Owner.Clear();
    }
  }
}
