// Decompiled with JetBrains decompiler
// Type: PlayUO.GServerTextBox
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GServerTextBox : GTextBox
  {
    private int m_RelayID;

    public int RelayID
    {
      get
      {
        return this.m_RelayID;
      }
    }

    public GServerTextBox(string initialText, LayoutEntry le)
      : base(0, false, le[0], le[1], le[2], le[3], initialText, (IFont) Engine.GetUniFont(1), Hues.Load(le[4] + 1), Hues.Load(le[4] + 1), Hues.Load(le[4] + 1))
    {
      this.m_RelayID = le[5];
      this.MaxChars = 239;
    }
  }
}
