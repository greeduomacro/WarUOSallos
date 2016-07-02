// Decompiled with JetBrains decompiler
// Type: PlayUO.GBuyGump_InventoryItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GBuyGump_InventoryItem : GRegion
  {
    private GBuyGump m_Owner;
    private BuyInfo m_Info;
    private GItemArt m_Image;
    private GLabel m_Description;
    private GLabel m_Available;
    private GImage[] m_Separator;
    private bool m_Selected;
    private int m_xAvailable;
    private int m_yBase;

    public override Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
        this.m_Image.Clipper = value;
        this.m_Description.Clipper = value;
        this.m_Available.Clipper = value;
        for (int index = 0; index < this.m_Separator.Length; ++index)
          this.m_Separator[index].Clipper = value;
      }
    }

    public bool Selected
    {
      get
      {
        return this.m_Selected;
      }
      set
      {
        if (this.m_Selected == value)
          return;
        this.m_Selected = value;
        GLabel glabel1 = this.m_Description;
        GLabel glabel2 = this.m_Available;
        int hueId = value ? 1644 : 648;
        IHue hue1;
        IHue hue2 = hue1 = Hues.Load(hueId);
        glabel2.Hue = hue1;
        IHue hue3 = hue2;
        glabel1.Hue = hue3;
      }
    }

    public int Available
    {
      get
      {
        return this.m_xAvailable;
      }
      set
      {
        if (this.m_xAvailable == value)
          return;
        this.m_xAvailable = value;
        this.m_Available.Text = value.ToString();
        this.m_Available.X = 195 - this.m_Available.Image.xMax - 1;
        this.m_Available.Y = (this.m_Height - (this.m_Available.Image.yMax - this.m_Available.Image.yMin + 1)) / 2 - this.m_Available.Image.yMin;
      }
    }

    public int Offset
    {
      set
      {
        this.m_Y = this.m_yBase + value;
      }
    }

    public GBuyGump_InventoryItem(GBuyGump owner, BuyInfo si, int y, bool seperate)
      : base(32, y, 195, 0)
    {
      this.m_Owner = owner;
      this.m_yBase = y;
      this.m_Info = si;
      IFont font = (IFont) Engine.GetUniFont(3);
      IHue hue = Hues.Load(648);
      this.m_Image = new GItemArt(0, 0, si.ItemID, si.Hue);
      this.m_Description = (GLabel) new GWrappedLabel(string.Format("{0} at {1} gp", (object) si.Name, (object) si.Price), font, hue, 58, 0, 105);
      this.m_Available = new GLabel(si.Amount.ToString(), font, hue, 195, 0);
      this.m_Height = this.m_Image.Image.yMax - this.m_Image.Image.yMin + 1;
      int num1 = this.m_Description.Image.yMax - this.m_Description.Image.yMin + 1;
      if (num1 > this.m_Height)
        this.m_Height = num1;
      int num2 = this.m_Available.Image.yMax - this.m_Available.Image.yMin + 1;
      if (num2 > this.m_Height)
        this.m_Height = num2;
      this.m_Image.X += (56 - (this.m_Image.Image.xMax - this.m_Image.Image.xMin + 1)) / 2;
      this.m_Image.Y += (this.m_Height - (this.m_Image.Image.yMax - this.m_Image.Image.yMin + 1)) / 2;
      this.m_Image.X -= this.m_Image.Image.xMin;
      this.m_Image.Y -= this.m_Image.Image.yMin;
      this.m_Description.X -= this.m_Description.Image.xMin;
      this.m_Description.Y += (this.m_Height - (this.m_Description.Image.yMax - this.m_Description.Image.yMin + 1)) / 2;
      this.m_Description.Y -= this.m_Description.Image.yMin;
      this.m_Available.X -= this.m_Available.Image.xMax + 1;
      this.m_Available.Y += (this.m_Height - (this.m_Available.Image.yMax - this.m_Available.Image.yMin + 1)) / 2;
      this.m_Available.Y -= this.m_Available.Image.yMin;
      this.m_Children.Add((Gump) this.m_Image);
      this.m_Children.Add((Gump) this.m_Description);
      this.m_Children.Add((Gump) this.m_Available);
      this.m_xAvailable = si.Amount;
      if (seperate)
      {
        this.m_Separator = new GImage[11];
        this.m_Children.Add((Gump) (this.m_Separator[0] = new GImage(57, 0, this.m_Height)));
        for (int index = 0; index < 9; ++index)
          this.m_Children.Add((Gump) (this.m_Separator[index + 1] = new GImage(58, 30 + index * 16, this.m_Height)));
        this.m_Children.Add((Gump) (this.m_Separator[10] = new GImage(59, 165, this.m_Height)));
      }
      else
        this.m_Separator = new GImage[0];
      if (!Engine.ServerFeatures.AOS)
        return;
      this.Tooltip = (ITooltip) new ItemTooltip(si.Item);
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Left) != MouseButtons.None)
        this.m_Owner.Selected = this.m_Info;
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Gumps.Destroy((Gump) this.m_Owner);
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      if (this.m_Info.ToBuy >= this.m_Info.Amount)
        return;
      if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
        this.m_Info.ToBuy = this.m_Info.Amount;
      else
        ++this.m_Info.ToBuy;
      if (this.m_Info.OfferedGump == null)
      {
        this.m_Info.OfferedGump = new GBuyGump_OfferedItem(this.m_Owner, this.m_Info);
        this.m_Owner.OfferMenu.Children.Add((Gump) this.m_Info.OfferedGump);
      }
      this.m_Info.OfferedGump.Amount = this.m_Info.ToBuy;
      this.Available = this.m_Info.Amount - this.m_Info.ToBuy;
      this.m_Owner.OfferMenu.UpdateTotal();
    }
  }
}
