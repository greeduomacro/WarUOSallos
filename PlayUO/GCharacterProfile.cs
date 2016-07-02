// Decompiled with JetBrains decompiler
// Type: PlayUO.GCharacterProfile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GCharacterProfile : GBackground
  {
    public GCharacterProfile(Mobile owner, string header, string body, string footer)
      : base(5058, 100, 100, 25, 25, true)
    {
      string GUID = string.Format("Profile-{0}", (object) owner.Serial);
      Gump gumpByGuid = Gumps.FindGumpByGUID(GUID);
      if (gumpByGuid != null)
      {
        this.m_IsDragging = gumpByGuid.m_IsDragging;
        this.m_OffsetX = gumpByGuid.m_OffsetX;
        this.m_OffsetY = gumpByGuid.m_OffsetY;
        if (Gumps.Drag == gumpByGuid)
          Gumps.Drag = (Gump) this;
        if (Gumps.LastOver == gumpByGuid)
          Gumps.LastOver = (Gump) this;
        if (Gumps.Focus == gumpByGuid)
          Gumps.Focus = (Gump) this;
        this.m_X = gumpByGuid.X;
        this.m_Y = gumpByGuid.Y;
        Gumps.Destroy(gumpByGuid);
      }
      this.m_GUID = GUID;
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.CanClose = true;
      Gump label1 = this.CreateLabel(header, false);
      Gump label2 = this.CreateLabel(body, true);
      Gump label3 = this.CreateLabel(footer, false);
      label1.X = this.OffsetX;
      label1.Y = this.OffsetY;
      label2.X = label1.X;
      label2.Y = label1.Y + label1.Height;
      label3.X = label2.X;
      label3.Y = label2.Y + label2.Height;
      this.Height = this.Height - this.UseHeight + label1.Height + label2.Height + label3.Height;
      this.Width = label1.Width;
      if (label2.Width > this.Width)
        this.Width = label2.Width;
      if (label3.Width > this.Width)
        this.Width = label3.Width;
      this.Width += this.Width - this.UseWidth;
      this.m_Children.Add(label1);
      this.m_Children.Add(label2);
      this.m_Children.Add(label3);
    }

    private Gump CreateLabel(string text, bool scroll)
    {
      text = text.Replace('\r', '\n');
      GBackground gbackground = new GBackground(3004, 200, 100, true);
      GWrappedLabel gwrappedLabel = new GWrappedLabel(text, (IFont) Engine.GetFont(1), Hues.Load(1109), gbackground.OffsetX, gbackground.OffsetY, gbackground.UseWidth);
      gbackground.Height = gwrappedLabel.Height + (gbackground.Height - gbackground.UseHeight);
      gbackground.Children.Add((Gump) gwrappedLabel);
      gwrappedLabel.Center();
      gbackground.SetMouseOverride((Gump) this);
      return (Gump) gbackground;
    }
  }
}
