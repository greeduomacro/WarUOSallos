// Decompiled with JetBrains decompiler
// Type: PlayUO.GIdleWarning
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GIdleWarning : GBackground
  {
    public GIdleWarning()
      : base(2604, 100, 100, 0, 0, true)
    {
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      GWrappedLabel gwrappedLabel = new GWrappedLabel("You have been idle for too long. If you do not do anything in the next minute, you will be logged out.", (IFont) Engine.GetFont(2), Hues.Load(1109), this.OffsetX, this.OffsetY - 12, 275);
      this.m_Children.Add((Gump) gwrappedLabel);
      GButtonNew gbuttonNew = new GButtonNew(1153, 0, gwrappedLabel.Y + gwrappedLabel.Height + 4);
      gbuttonNew.Clicked += new EventHandler(this.Check_Clicked);
      this.m_Children.Add((Gump) gbuttonNew);
      this.Width = this.Width - this.UseWidth + gwrappedLabel.Width;
      this.Height = this.Height - this.UseHeight + gwrappedLabel.Height - 12;
      gbuttonNew.X = this.OffsetX + (this.UseWidth - gbuttonNew.Width) / 2;
      this.Center();
    }

    private void Check_Clicked(object sender, EventArgs e)
    {
      Gumps.Destroy((Gump) this);
    }
  }
}
