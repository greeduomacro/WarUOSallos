// Decompiled with JetBrains decompiler
// Type: PlayUO.ServerFeatures
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class ServerFeatures
  {
    private bool m_ContextMenus;
    private bool m_SingleChar;
    private bool m_AOS;

    public bool ContextMenus
    {
      get
      {
        return this.m_ContextMenus;
      }
      set
      {
        this.m_ContextMenus = value;
      }
    }

    public bool SingleChar
    {
      get
      {
        return this.m_SingleChar;
      }
      set
      {
        this.m_SingleChar = value;
      }
    }

    public bool AOS
    {
      get
      {
        return this.m_AOS;
      }
      set
      {
        this.m_AOS = value;
      }
    }

    public ServerFeatures()
    {
      this.m_ContextMenus = false;
      this.m_SingleChar = false;
      this.m_AOS = false;
    }
  }
}
