// Decompiled with JetBrains decompiler
// Type: PlayUO.PopupEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PopupEntry
  {
    private int m_EntryID;
    private string m_Text;
    private int m_Flags;

    public int EntryID
    {
      get
      {
        return this.m_EntryID;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
    }

    public int Flags
    {
      get
      {
        return this.m_Flags;
      }
    }

    public PopupEntry(int EntryID, int StringID, int Flags)
    {
      this.m_EntryID = EntryID;
      this.m_Flags = Flags;
      this.m_Text = Localization.GetString(3000000 + StringID);
      if ((Flags & -34) == 0)
        return;
      this.m_Text = string.Format("0x{0:X4} {1}", (object) Flags, (object) this.m_Text);
    }
  }
}
