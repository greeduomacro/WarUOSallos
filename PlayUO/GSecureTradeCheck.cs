// Decompiled with JetBrains decompiler
// Type: PlayUO.GSecureTradeCheck
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GSecureTradeCheck : GButtonNew
  {
    private Item m_Item;
    private bool m_Checked;
    private GSecureTradeCheck m_Partner;

    public bool Checked
    {
      get
      {
        return this.m_Checked;
      }
      set
      {
        if (this.m_Checked == value)
          return;
        this.m_Checked = value;
        if (this.m_Checked)
        {
          this.m_GumpID[0] = 2153;
          this.m_GumpID[1] = 2154;
          this.m_GumpID[2] = 2154;
        }
        else
        {
          this.m_GumpID[0] = 2151;
          this.m_GumpID[1] = 2152;
          this.m_GumpID[2] = 2152;
        }
        this.Invalidate();
      }
    }

    public GSecureTradeCheck(int x, int y, Item item, GSecureTradeCheck partner)
      : base(2151, 2152, 2152, x, y)
    {
      this.m_Item = item;
      this.m_Partner = partner;
      this.Enabled = this.m_Item != null;
    }

    protected override void OnClicked()
    {
      if (this.m_Item == null)
        return;
      Network.Send((Packet) new PCheckTrade(this.m_Item, !this.m_Checked, this.m_Partner.m_Checked));
    }
  }
}
