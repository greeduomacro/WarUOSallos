// Decompiled with JetBrains decompiler
// Type: PlayUO.GItemListEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GItemListEntry : Gump
  {
    private AnswerEntry m_Entry;
    private bool m_Draw;
    private IHue m_Hue;
    private Texture m_Image;
    private int m_Width;
    private int m_Height;
    private Clipper m_Clipper;
    private int m_xOffset;
    private int m_ImageOffsetX;
    private int m_ImageOffsetY;
    private GItemList m_Owner;

    public override int X
    {
      get
      {
        return this.m_X + this.m_xOffset;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
      }
    }

    public int xOffset
    {
      get
      {
        return this.m_xOffset;
      }
      set
      {
        this.m_xOffset = value;
      }
    }

    public GItemListEntry(int x, AnswerEntry entry, GItemList owner)
      : base(x, 45)
    {
      this.m_Entry = entry;
      this.m_Owner = owner;
      int hue = entry.Hue;
      if (hue > 0)
        ++hue;
      this.m_Hue = Hues.GetItemHue(entry.ItemID, hue);
      this.m_Image = this.m_Hue.GetItem(entry.ItemID);
      if (this.m_Image == null || this.m_Image.IsEmpty())
        return;
      this.m_Draw = true;
      this.m_Height = 47;
      int num = this.m_Image.xMax - this.m_Image.xMin + 1;
      this.m_Width = 47;
      if (num > this.m_Width)
        this.m_Width = num;
      this.m_ImageOffsetX = (this.m_Width - (this.m_Image.xMax - this.m_Image.xMin + 1)) / 2 - this.m_Image.xMin;
      this.m_ImageOffsetY = (this.m_Height - (this.m_Image.yMax - this.m_Image.yMin + 1)) / 2 - this.m_Image.yMin;
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.m_Owner.BringToTop();
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      Network.Send((Packet) new PQuestionMenuResponse(this.m_Owner.Serial, this.m_Owner.MenuID, this.m_Entry.Index, this.m_Entry.ItemID, this.m_Entry.Hue));
      Gumps.Destroy((Gump) this.m_Owner);
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      this.m_Image = Hues.Load(32821).GetItem(this.m_Entry.ItemID);
      this.m_Draw = this.m_Image != null && !this.m_Image.IsEmpty();
      this.m_Owner.EntryLabel.Text = this.m_Entry.Text;
    }

    protected internal override void OnMouseLeave()
    {
      this.m_Image = this.m_Hue.GetItem(this.m_Entry.ItemID);
      this.m_Draw = this.m_Image != null && !this.m_Image.IsEmpty();
      this.m_Owner.EntryLabel.Text = "";
    }

    protected internal override void Draw(int x, int y)
    {
      if (!this.m_Draw || this.m_Clipper == null)
        return;
      this.m_Image.DrawClipped(x + this.m_ImageOffsetX, y + this.m_ImageOffsetY, this.m_Clipper);
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Draw && this.m_Clipper != null)
        return this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)));
      return false;
    }
  }
}
