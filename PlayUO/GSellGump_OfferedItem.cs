// Decompiled with JetBrains decompiler
// Type: PlayUO.GSellGump_OfferedItem
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GSellGump_OfferedItem : GRegion
  {
    private GLabel m_Amount;
    private GLabel m_Description;
    private GSellGump_AmountButton m_More;
    private GSellGump_AmountButton m_Less;
    private int m_xAmount;
    private GSellGump_OfferMenu m_OfferMenu;

    public override Clipper Clipper
    {
      get
      {
        return this.m_Clipper;
      }
      set
      {
        this.m_Clipper = value;
        this.m_Amount.Clipper = value;
        this.m_Description.Clipper = value;
        this.m_More.Clipper = value;
        this.m_Less.Clipper = value;
      }
    }

    public int Amount
    {
      get
      {
        return this.m_xAmount;
      }
      set
      {
        if (this.m_xAmount == value)
          return;
        this.m_xAmount = value;
        this.m_Amount.Text = this.m_xAmount.ToString();
        this.m_Amount.X = -this.m_Amount.Image.xMin;
        this.m_Amount.Y = (this.m_Height - (this.m_Amount.Image.yMax - this.m_Amount.Image.yMin + 1)) / 2;
        int num = this.m_Description.Y + this.m_Description.Image.yMin;
        if (this.m_Amount.Y > num)
          this.m_Amount.Y = num;
        this.m_Amount.Y -= this.m_Amount.Image.yMin;
      }
    }

    public GSellGump_OfferedItem(GSellGump owner, SellInfo si)
      : base(32, 67, 196, 0)
    {
      this.m_OfferMenu = owner.OfferMenu;
      IFont font = (IFont) Engine.GetUniFont(3);
      IHue hue = Hues.Load(648);
      this.m_xAmount = si.ToSell;
      this.m_Amount = new GLabel(si.ToSell.ToString(), font, hue, 0, 0);
      this.m_Description = (GLabel) new GWrappedLabel(string.Format("{0} at {1} gp", (object) si.Name, (object) si.Price), font, hue, 41, 0, 105);
      this.m_More = new GSellGump_AmountButton(owner, si, 5, 55, 155);
      this.m_Less = new GSellGump_AmountButton(owner, si, -5, 56, 173);
      this.m_Height = this.m_Amount.Image.yMax - this.m_Amount.Image.yMin + 1;
      int num = this.m_Description.Image.yMax - this.m_Description.Image.yMin + 1;
      if (num > this.m_Height)
        this.m_Height = num;
      int height1 = this.m_More.Height;
      if (height1 > this.m_Height)
        this.m_Height = height1;
      int height2 = this.m_Less.Height;
      if (height2 > this.m_Height)
        this.m_Height = height2;
      this.m_Amount.X -= this.m_Amount.Image.xMin;
      this.m_Amount.Y = (this.m_Height - (this.m_Amount.Image.yMax - this.m_Amount.Image.yMin + 1)) / 2;
      this.m_Description.X -= this.m_Description.Image.xMin;
      this.m_Description.Y = (this.m_Height - (this.m_Description.Image.yMax - this.m_Description.Image.yMin + 1)) / 2;
      if (this.m_Amount.Y > this.m_Description.Y)
        this.m_Amount.Y = this.m_Description.Y;
      this.m_Amount.Y -= this.m_Amount.Image.yMin;
      this.m_Description.Y -= this.m_Description.Image.yMin;
      this.m_More.Y = (this.m_Height - this.m_More.Height) / 2;
      this.m_Less.Y = (this.m_Height - this.m_Less.Height) / 2;
      this.m_Children.Add((Gump) this.m_Amount);
      this.m_Children.Add((Gump) this.m_Description);
      this.m_Children.Add((Gump) this.m_More);
      this.m_Children.Add((Gump) this.m_Less);
      this.Clipper = this.m_OfferMenu.ContentClipper;
      if (!Engine.ServerFeatures.AOS)
        return;
      this.Tooltip = (ITooltip) new ItemTooltip(si.Item);
    }

    protected internal override void Render(int x, int y)
    {
      this.m_Y = this.m_OfferMenu.yOffset;
      this.m_OfferMenu.yOffset += this.m_Height + 2;
      base.Render(x, y);
    }
  }
}
