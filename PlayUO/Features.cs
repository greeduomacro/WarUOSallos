// Decompiled with JetBrains decompiler
// Type: PlayUO.Features
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Features
  {
    private bool m_Chat;
    private bool m_LBR;
    private bool m_AOS;

    public bool Chat
    {
      get
      {
        return this.m_Chat;
      }
      set
      {
        this.m_Chat = value;
      }
    }

    public bool LBR
    {
      get
      {
        return this.m_LBR;
      }
      set
      {
        this.m_LBR = value;
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
  }
}
