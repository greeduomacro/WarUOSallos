// Decompiled with JetBrains decompiler
// Type: PlayUO.GBuyGump_OfferMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GBuyGump_OfferMenu : GImage
  {
    private int m_xLast;
    private int m_yLast;
    private GBuyAccept m_Accept;
    private GBuyClear m_Clear;
    private GBuyGump m_Owner;
    private GLabel m_Total;
    private GLabel m_Signature;
    private int m_yOffset;
    private Clipper m_ContentClipper;
    private GVSlider m_Slider;
    private TimeSync m_SignatureAnimation;
    private int m_LastHeight;
    private int m_LastOffset;

    public Clipper ContentClipper
    {
      get
      {
        return this.m_ContentClipper;
      }
    }

    public int yOffset
    {
      get
      {
        return this.m_yOffset;
      }
      set
      {
        this.m_yOffset = value;
      }
    }

    public GBuyGump_OfferMenu(GBuyGump owner)
      : base(2161, 170, 214)
    {
      this.m_Owner = owner;
      Mobile player = World.Player;
      string name;
      string Text;
      if (player != null && (name = player.Name) != null && (Text = name.Trim()).Length > 0)
      {
        this.m_Signature = new GLabel(Text, (IFont) Engine.GetFont(5), Hues.Load(1109), 72, 194);
        this.m_Signature.Visible = false;
        this.m_Children.Add((Gump) this.m_Signature);
      }
      this.m_Children.Add((Gump) new GLabel(player != null ? player.Gold.ToString() : "0", (IFont) Engine.GetFont(6), Hues.Default, 188, 167));
      this.m_Total = new GLabel("0", (IFont) Engine.GetFont(6), Hues.Default, 68, 167);
      this.m_Accept = new GBuyAccept(owner);
      this.m_Clear = new GBuyClear(owner);
      this.m_Children.Add((Gump) this.m_Total);
      this.m_Children.Add((Gump) this.m_Accept);
      this.m_Children.Add((Gump) this.m_Clear);
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      GVSlider gvSlider = new GVSlider(2088, 237, 81, 34, 58, 0.0, 0.0, 50.0, 1.0);
      this.m_Slider = gvSlider;
      this.m_Children.Add((Gump) gvSlider);
      this.m_Children.Add((Gump) new GHotspot(237, 66, 34, 84, (Gump) gvSlider));
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Gumps.Destroy((Gump) this.m_Owner);
    }

    public void UpdateTotal()
    {
      this.m_Total.Text = this.m_Owner.ComputeTotalCost().ToString();
    }

    public void WriteSignature()
    {
      if (this.m_Signature == null)
        return;
      this.m_Signature.Visible = true;
      this.m_SignatureAnimation = new TimeSync(0.5);
    }

    protected internal override void OnDragStart()
    {
      this.m_IsDragging = false;
      Gumps.Drag = (Gump) null;
      Point point = this.PointToScreen(new Point(0, 0)) - this.m_Owner.PointToScreen(new Point(0, 0));
      this.m_Owner.m_OffsetX = point.X + this.m_OffsetX;
      this.m_Owner.m_OffsetY = point.Y + this.m_OffsetY;
      this.m_Owner.m_IsDragging = true;
      Gumps.Drag = (Gump) this.m_Owner;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      return this.m_Image.HitTest(x, y);
    }

    protected internal override void Render(int x, int y)
    {
      base.Render(x, y);
      int num = this.m_yOffset + this.m_LastOffset - 67;
      if (num > 90)
        this.m_LastHeight = num - 90;
      else
        this.m_LastHeight = -1;
    }

    protected internal override void Draw(int x, int y)
    {
      if (this.m_xLast != x || this.m_yLast != y)
      {
        this.m_xLast = x;
        this.m_yLast = y;
        Clipper clipper = new Clipper(x + 32, y + 66, 196, 92);
        this.m_ContentClipper = clipper;
        foreach (Gump gump in this.m_Children.ToArray())
        {
          if (gump is GBuyGump_OfferedItem)
            ((GRegion) gump).Clipper = clipper;
        }
      }
      if (this.m_Signature != null && this.m_SignatureAnimation != null)
      {
        double normalized = this.m_SignatureAnimation.Normalized;
        if (normalized >= 1.0)
        {
          this.m_Signature.Scissor((Clipper) null);
          this.m_SignatureAnimation = (TimeSync) null;
        }
        else
          this.m_Signature.Scissor(0, 0, (int) (normalized * (double) this.m_Signature.Width), this.m_Signature.Height);
        Engine.Redraw();
      }
      this.m_yOffset = this.m_LastHeight < 0 ? 67 : 67 - (int) (this.m_Slider.GetValue() / 50.0 * (double) this.m_LastHeight);
      this.m_LastOffset = 67 - this.m_yOffset;
      base.Draw(x, y);
    }
  }
}
