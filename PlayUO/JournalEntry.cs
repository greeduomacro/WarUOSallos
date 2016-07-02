// Decompiled with JetBrains decompiler
// Type: PlayUO.JournalEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class JournalEntry
  {
    private Texture m_Image;
    private IHue m_Hue;
    private string m_Text;
    private int m_Width;
    private int m_Serial;
    private DateTime m_Time;

    public DateTime Time
    {
      get
      {
        return this.m_Time;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
      set
      {
        this.m_Image = value;
      }
    }

    public int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
      set
      {
        this.m_Text = value;
      }
    }

    public JournalEntry(string text, IHue hue, int serial)
    {
      this.m_Text = text;
      this.m_Hue = hue;
      this.m_Serial = serial;
      this.m_Time = DateTime.Now;
    }
  }
}
