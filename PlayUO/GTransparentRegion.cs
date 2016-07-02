// Decompiled with JetBrains decompiler
// Type: PlayUO.GTransparentRegion
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GTransparentRegion : GEmpty
  {
    private GServerGump m_Owner;

    public GTransparentRegion(GServerGump owner, int x, int y, int w, int h)
      : base(x, y, w, h)
    {
      this.m_Owner = owner;
    }

    protected internal override bool HitTest(int x, int y)
    {
      return false;
    }
  }
}
