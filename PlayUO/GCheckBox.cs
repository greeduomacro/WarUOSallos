// Decompiled with JetBrains decompiler
// Type: PlayUO.GCheckBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GCheckBox : GImage
  {
    protected bool m_Checked;
    protected int[] m_GumpIDs;

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
        this.GumpID = this.m_GumpIDs[value ? 1 : 0];
      }
    }

    public GCheckBox(int inactiveID, int activeID, bool initialState, int x, int y)
      : base(initialState ? activeID : inactiveID, x, y)
    {
      this.m_GumpIDs = new int[2]
      {
        inactiveID,
        activeID
      };
      this.m_Checked = initialState;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      this.Checked = !this.Checked;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }
  }
}
