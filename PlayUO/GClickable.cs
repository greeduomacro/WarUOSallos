// Decompiled with JetBrains decompiler
// Type: PlayUO.GClickable
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GClickable : GImage
  {
    public event EventHandler Clicked;

    public event EventHandler DoubleClicked;

    public GClickable(int x, int y, int gumpID)
      : base(gumpID, x, y)
    {
    }

    public GClickable(int x, int y, int gumpID, IHue hue)
      : base(gumpID, hue, x, y)
    {
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw)
        return this.m_Image.HitTest(x, y);
      return false;
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      this.InternalOnSingleClicked();
    }

    protected internal override void OnDoubleClick(int x, int y)
    {
      this.InternalOnDoubleClicked();
    }

    private void InternalOnSingleClicked()
    {
      this.OnSingleClicked();
      if (this.Clicked == null)
        return;
      this.Clicked((object) this, EventArgs.Empty);
    }

    protected virtual void OnSingleClicked()
    {
    }

    private void InternalOnDoubleClicked()
    {
      this.OnDoubleClicked();
      if (this.DoubleClicked == null)
        return;
      this.DoubleClicked((object) this, EventArgs.Empty);
    }

    protected virtual void OnDoubleClicked()
    {
    }
  }
}
