// Decompiled with JetBrains decompiler
// Type: PlayUO.GServerCheckBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GServerCheckBox : GCheckBox, IRelayedSwitch
  {
    private GServerGump m_Owner;
    private int m_RelayID;

    int IRelayedSwitch.RelayID
    {
      get
      {
        return this.m_RelayID;
      }
    }

    bool IRelayedSwitch.Active
    {
      get
      {
        return this.Checked;
      }
    }

    public GServerCheckBox(GServerGump owner, LayoutEntry le)
      : base(le[2], le[3], le[4] != 0, le[0], le[1])
    {
      this.m_Owner = owner;
      this.m_RelayID = le[5];
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Owner.BringToTop();
      base.OnMouseDown(x, y, mb);
    }
  }
}
