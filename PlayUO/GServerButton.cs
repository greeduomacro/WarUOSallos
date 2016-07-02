// Decompiled with JetBrains decompiler
// Type: PlayUO.GServerButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GServerButton : GButtonNew
  {
    private int m_Type;
    private int m_Param;
    private int m_RelayID;
    private GServerGump m_Owner;

    public int RelayID
    {
      get
      {
        return this.m_RelayID;
      }
    }

    public GServerButton(GServerGump owner, LayoutEntry le)
      : base(le[2], le[2], le[3], le[0], le[1])
    {
      this.m_Owner = owner;
      this.m_Type = le[4];
      this.m_Param = le[5];
      this.m_RelayID = le[6];
    }

    protected internal override bool HitTest(int x, int y)
    {
      return true;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Owner.BringToTop();
      base.OnMouseDown(x, y, mb);
    }

    protected override void OnClicked()
    {
      switch (this.m_Type)
      {
        case 0:
          this.m_Owner.Page = this.m_Param;
          break;
        case 1:
        case 4:
          if (this.m_RelayID != 0)
            GServerGump.SetCachedLocation(this.m_Owner.DialogID, this.m_Owner.X, this.m_Owner.Y);
          Network.Send((Packet) new PGumpButton(this.m_Owner, this.m_RelayID));
          Gumps.Destroy(this.m_Parent);
          break;
      }
    }
  }
}
