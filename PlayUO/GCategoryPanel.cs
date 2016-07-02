// Decompiled with JetBrains decompiler
// Type: PlayUO.GCategoryPanel
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class GCategoryPanel : GAlphaBackground
  {
    private GLabel m_Label;
    private GPropertyEntry[] m_Entries;

    public string Label
    {
      get
      {
        return this.m_Label.Text;
      }
      set
      {
        this.m_Label.Text = value;
      }
    }

    public GPropertyEntry[] Entries
    {
      get
      {
        return this.m_Entries;
      }
      set
      {
        this.m_Entries = value;
      }
    }

    public GCategoryPanel(object obj, string category, ArrayList entries)
      : base(0, 0, 279, 22)
    {
      this.FillColor = GumpColors.Control;
      this.BorderColor = GumpColors.ControlDarkDark;
      this.FillAlpha = 1f;
      this.ShouldHitTest = false;
      this.m_NonRestrictivePicking = true;
      this.m_Label = new GLabel(category, (IFont) Engine.GetUniFont(1), GumpHues.ControlText, 0, 0);
      this.m_Label.X = 5 - this.m_Label.Image.xMin;
      this.m_Label.Y = (22 - (this.m_Label.Image.yMax - this.m_Label.Image.yMin + 1)) / 2 - this.m_Label.Image.yMin;
      this.m_Children.Add((Gump) this.m_Label);
      entries.Sort();
      this.m_Entries = new GPropertyEntry[entries.Count];
      int num = 21;
      for (int index = 0; index < entries.Count; ++index)
      {
        ObjectEditorEntry entry = entries[index] as ObjectEditorEntry;
        this.m_Entries[index] = new GPropertyEntry(entry.Object, entry);
        this.m_Entries[index].Y = num;
        num += 21;
        this.m_Children.Add((Gump) this.m_Entries[index]);
      }
      this.Height = num + 1;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      Renderer.SetTexture((Texture) null);
      if (this.m_Clipper == null)
        Renderer.SolidRect(GumpColors.ControlDark, X + 1, Y + 21, this.Width - 2, this.Height - 22);
      else
        Renderer.SolidRect(GumpColors.ControlDark, X + 1, Y + 21, this.Width - 2, this.Height - 22, this.m_Clipper);
    }

    public void Reset()
    {
      for (int index = 0; index < this.m_Entries.Length; ++index)
        this.m_Entries[index].Reset();
    }

    public void SetClipper(Clipper c)
    {
      this.Clipper = c;
      this.m_Label.Clipper = c;
      for (int index = 0; index < this.m_Entries.Length; ++index)
        this.m_Entries[index].SetClipper(c);
    }
  }
}
