// Decompiled with JetBrains decompiler
// Type: PlayUO.GEditorPanel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class GEditorPanel : GEmpty
  {
    private GCategoryPanel[] m_Panels;
    private GEditorScroller m_Scroller;
    private int m_xLast;
    private int m_yLast;
    private Clipper m_Clipper;

    public GCategoryPanel[] Panels
    {
      get
      {
        return this.m_Panels;
      }
    }

    public GEditorPanel(ArrayList panels, int height)
      : base(0, 0, 0, height)
    {
      this.m_Panels = (GCategoryPanel[]) panels.ToArray(typeof (GCategoryPanel));
      this.m_NonRestrictivePicking = true;
      this.Layout();
    }

    public void Layout()
    {
      int num1 = 5;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      if (this.m_Scroller != null)
        num2 = -this.m_Scroller.Value;
      for (int index = 0; index < this.m_Panels.Length; ++index)
      {
        GCategoryPanel gcategoryPanel = this.m_Panels[index];
        gcategoryPanel.X = 5;
        gcategoryPanel.Y = num1 + num2;
        if (this.m_Scroller == null)
          this.m_Children.Add((Gump) gcategoryPanel);
        if (gcategoryPanel.Width > num3)
          num3 = gcategoryPanel.Width;
        if (num1 + gcategoryPanel.Height > num4)
          num4 = num1 + gcategoryPanel.Height;
        num1 += gcategoryPanel.Height - 1;
      }
      int num5 = num3 + 26;
      this.Width = num5;
      if (this.m_Scroller != null)
        return;
      this.m_Scroller = new GEditorScroller(this);
      this.m_Scroller.X = num5 - 16;
      this.m_Scroller.Y = 0;
      this.m_Scroller.Height = this.Height;
      this.m_Scroller.Width = 16;
      this.m_Scroller.Maximum = num4 - this.Height + 5;
      this.m_Children.Insert(0, (Gump) this.m_Scroller);
    }

    public void Reset()
    {
      for (int index = 0; index < this.m_Panels.Length; ++index)
        this.m_Panels[index].Reset();
    }

    protected internal override void Draw(int X, int Y)
    {
      if (this.m_xLast != X || this.m_yLast != Y)
      {
        this.m_xLast = X;
        this.m_yLast = Y;
        this.m_Clipper = new Clipper(this.m_xLast + 5, this.m_yLast, this.Width - 10, this.Height);
        for (int index = 0; index < this.m_Panels.Length; ++index)
          this.m_Panels[index].SetClipper(this.m_Clipper);
      }
      Renderer.SetTexture((Texture) null);
      GumpPaint.DrawSunken3D(X - 2, Y - 2, this.Width + 4, this.Height + 4);
    }
  }
}
