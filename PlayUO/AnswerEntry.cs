// Decompiled with JetBrains decompiler
// Type: PlayUO.AnswerEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class AnswerEntry
  {
    private int m_Index;
    private int m_ItemID;
    private int m_Hue;
    private string m_Text;

    public int Index
    {
      get
      {
        return this.m_Index;
      }
    }

    public int ItemID
    {
      get
      {
        return this.m_ItemID;
      }
    }

    public int Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
    }

    public AnswerEntry(int index, int itemID, int hue, string text)
    {
      this.m_Index = index;
      this.m_ItemID = itemID;
      this.m_Hue = hue;
      this.m_Text = text;
    }
  }
}
