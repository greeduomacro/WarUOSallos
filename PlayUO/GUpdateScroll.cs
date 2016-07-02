// Decompiled with JetBrains decompiler
// Type: PlayUO.GUpdateScroll
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GUpdateScroll : GBackground
  {
    public GUpdateScroll(string text)
      : base(5058, 100, 100, 40, 30, true)
    {
      GLabel glabel = new GLabel("Updates", Engine.DefaultFont, Hues.Load(496), this.OffsetX, this.OffsetY);
      GBackground gbackground = new GBackground(3004, 100, 100, this.OffsetX, glabel.Y + glabel.Height + 4, true);
      GWrappedLabel gwrappedLabel = new GWrappedLabel(text, (IFont) Engine.GetFont(1), Hues.Load(1109), gbackground.OffsetX + 2, gbackground.OffsetY + 2, 250);
      gbackground.Width = gbackground.Width - gbackground.UseWidth + gwrappedLabel.Width + 6;
      gbackground.Height = gbackground.Height - gbackground.UseHeight + gwrappedLabel.Height + 2;
      gbackground.Children.Add((Gump) gwrappedLabel);
      this.Width = this.Width - this.UseWidth + gbackground.Width;
      this.Height = this.Height - this.UseHeight + glabel.Height + 4 + gbackground.Height;
      glabel.X += (this.UseWidth - glabel.Width) / 2;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.CanClose = true;
      gbackground.SetMouseOverride((Gump) this);
      this.m_Children.Add((Gump) glabel);
      this.m_Children.Add((Gump) gbackground);
    }
  }
}
