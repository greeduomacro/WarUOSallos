// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.StaticTarget
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  public class StaticTarget : IPoint3D, IPoint2D
  {
    private int m_X;
    private int m_Y;
    private int m_Z;
    private int m_ID;
    private int m_RealID;
    private IHue m_Hue;

    public int X
    {
      get
      {
        return this.m_X;
      }
    }

    public int Y
    {
      get
      {
        return this.m_Y;
      }
    }

    public int Z
    {
      get
      {
        return this.m_Z;
      }
    }

    public int ID
    {
      get
      {
        return this.m_ID;
      }
    }

    public int RealID
    {
      get
      {
        return this.m_RealID;
      }
    }

    public IHue Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public StaticTarget(int x, int y, int z, int id, int realID, IHue hue)
    {
      this.m_X = x;
      this.m_Y = y;
      this.m_Z = z;
      this.m_ID = id;
      this.m_RealID = realID;
      this.m_Hue = hue;
    }
  }
}
