// Decompiled with JetBrains decompiler
// Type: PlayUO.GQuestionBackground
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GQuestionBackground : GBackground
  {
    private int m_xLast = int.MinValue;
    private int m_yLast = int.MinValue;
    private GQuestionMenuEntry[] m_Entries;

    public GQuestionBackground(GQuestionMenuEntry[] entries, int width, int height, int x, int y)
      : base(3004, width, height, x, y, true)
    {
      this.m_Entries = entries;
    }

    protected internal override void Draw(int x, int y)
    {
      base.Draw(x, y);
      if (this.m_xLast == x && this.m_yLast == y)
        return;
      this.m_xLast = x;
      this.m_yLast = y;
      Clipper clipper = new Clipper(x + this.OffsetX, y + this.OffsetY, this.UseWidth, this.UseHeight);
      for (int index = 0; index < this.m_Entries.Length; ++index)
        this.m_Entries[index].Clipper = clipper;
    }

    protected internal override void OnMouseWheel(int delta)
    {
      this.m_Parent.OnMouseWheel(delta);
    }
  }
}
