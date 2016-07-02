// Decompiled with JetBrains decompiler
// Type: PlayUO.GSecureTrade
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GSecureTrade : GAlphaBackground
  {
    private int m_Serial;
    public Gump m_Container;
    private bool m_ShouldClose;

    public GSecureTrade(int serial, Gump container, string myName, string theirName)
      : base(50, 50, 281, 116)
    {
      this.m_Serial = serial;
      this.m_Container = container;
      this.m_CanDrop = true;
      this.FillAlpha = 0.5f;
      this.FillColor = 6324479;
      GBorder3D gborder3D1 = new GBorder3D(false, 0, 0, this.Width, this.Height);
      gborder3D1.FillAlpha = 0.0f;
      gborder3D1.ShouldHitTest = false;
      this.m_Children.Add((Gump) gborder3D1);
      GBorder3D gborder3D2 = new GBorder3D(true, 6, 6, 132, 104);
      gborder3D2.FillAlpha = 0.0f;
      gborder3D2.ShouldHitTest = false;
      this.m_Children.Add((Gump) gborder3D2);
      GBorder3D gborder3D3 = new GBorder3D(false, 7, 7, 130, 20);
      gborder3D3.ShouldHitTest = false;
      GLabel glabel1 = new GLabel(this.Truncate(myName, (IFont) Engine.GetUniFont(1), gborder3D3.Width - 28), (IFont) Engine.GetUniFont(1), Hues.Load(1), 0, 0);
      gborder3D3.Children.Add((Gump) glabel1);
      glabel1.Center();
      glabel1.X = 28 - glabel1.Image.xMin;
      this.m_Children.Add((Gump) gborder3D3);
      GBorder3D gborder3D4 = new GBorder3D(true, 143, 6, 132, 104);
      gborder3D4.FillAlpha = 0.0f;
      gborder3D4.ShouldHitTest = false;
      this.m_Children.Add((Gump) gborder3D4);
      GBorder3D gborder3D5 = new GBorder3D(false, 144, 7, 130, 20);
      gborder3D5.ShouldHitTest = false;
      GLabel glabel2 = new GLabel(this.Truncate(theirName, (IFont) Engine.GetUniFont(1), gborder3D5.Width - 28), (IFont) Engine.GetUniFont(1), Hues.Load(1), 0, 0);
      gborder3D5.Children.Add((Gump) glabel2);
      glabel2.Center();
      glabel2.X = gborder3D5.Width - 28 - glabel2.Image.xMax;
      this.m_Children.Add((Gump) gborder3D5);
      this.m_Children.Add((Gump) new GAlphaBackground(1, 1, 5, 114)
      {
        ShouldHitTest = false,
        BorderColor = 12632256,
        FillColor = 12632256,
        FillAlpha = 1f
      });
      this.m_Children.Add((Gump) new GAlphaBackground(275, 1, 5, 114)
      {
        ShouldHitTest = false,
        BorderColor = 12632256,
        FillColor = 12632256,
        FillAlpha = 1f
      });
      this.m_Children.Add((Gump) new GAlphaBackground(6, 1, 269, 5)
      {
        ShouldHitTest = false,
        BorderColor = 12632256,
        FillColor = 12632256,
        FillAlpha = 1f
      });
      this.m_Children.Add((Gump) new GAlphaBackground(6, 110, 269, 5)
      {
        ShouldHitTest = false,
        BorderColor = 12632256,
        FillColor = 12632256,
        FillAlpha = 1f
      });
      this.m_Children.Add((Gump) new GAlphaBackground(138, 6, 5, 104)
      {
        ShouldHitTest = false,
        BorderColor = 12632256,
        FillColor = 12632256,
        FillAlpha = 1f
      });
    }

    public string Truncate(string text, IFont font, int width)
    {
      if (font.GetStringWidth(text) > width)
      {
        while (text.Length > 0 && font.GetStringWidth(text + "...") > width)
          text = text.Substring(0, text.Length - 1);
        text += "...";
      }
      return text;
    }

    protected internal override void OnDragDrop(Gump g)
    {
      if (this.m_Container == null)
        return;
      this.m_Container.OnDragDrop(g);
    }

    public void Close()
    {
      Network.Send((Packet) new PCancelTrade(this.m_Serial));
    }

    protected internal override void OnMouseDown(int x, int y, MouseButtons mb)
    {
      this.BringToTop();
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      this.m_ShouldClose = true;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      if (this.m_ShouldClose && (mb & MouseButtons.Right) != MouseButtons.None)
        this.Close();
      this.m_ShouldClose = false;
    }

    protected internal override void OnMouseEnter(int x, int y, MouseButtons mb)
    {
      if (!this.m_ShouldClose || (mb & MouseButtons.Right) != MouseButtons.None)
        return;
      this.m_ShouldClose = false;
    }
  }
}
