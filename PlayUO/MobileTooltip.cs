// Decompiled with JetBrains decompiler
// Type: PlayUO.MobileTooltip
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class MobileTooltip : ITooltip
  {
    private Mobile m_Mobile;
    private Gump m_Gump;

    public Gump Gump
    {
      get
      {
        return this.m_Gump;
      }
      set
      {
        this.m_Gump = value;
      }
    }

    public float Delay
    {
      get
      {
        return 0.5f;
      }
      set
      {
      }
    }

    public MobileTooltip(Mobile mob)
    {
      this.m_Mobile = mob;
    }

    public Gump GetGump()
    {
      if (this.m_Gump != null)
        return this.m_Gump;
      if (this.m_Mobile.PropertyList != null)
        return this.m_Gump = (Gump) new GObjectProperties(-1, (object) this.m_Mobile, this.m_Mobile.PropertyList);
      this.m_Mobile.QueryProperties();
      return (Gump) null;
    }
  }
}
