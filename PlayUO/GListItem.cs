// Decompiled with JetBrains decompiler
// Type: PlayUO.GListItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GListItem : GTextButton
  {
    private int m_Index;
    private GListBox m_Owner;

    public int Index
    {
      get
      {
        return this.m_Index;
      }
    }

    public GListItem(string Text, int Index, GListBox Owner)
      : base(Text, Owner.Font, Owner.HRegular, Owner.HOver, Owner.OffsetX, Owner.OffsetY, (OnClick) null)
    {
      this.m_Index = Index;
      this.m_Owner = Owner;
      this.Layout();
    }

    public void Layout()
    {
      this.m_Visible = this.m_Index >= this.m_Owner.StartIndex && this.m_Index < this.m_Owner.StartIndex + this.m_Owner.ItemCount;
      this.m_Y = this.m_Owner.OffsetY + (this.m_Index - this.m_Owner.StartIndex) * 18;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      this.m_Owner.OnListItemClick(this);
    }
  }
}
