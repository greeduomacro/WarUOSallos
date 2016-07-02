// Decompiled with JetBrains decompiler
// Type: PlayUO.GSellAccept
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GSellAccept : GRegion
  {
    private GSellGump m_Owner;

    public GSellAccept(GSellGump owner)
      : base(30, 193, 63, 42)
    {
      this.m_Owner = owner;
    }

    protected internal override void OnSingleClick(int x, int y)
    {
      this.m_Owner.Accept();
    }
  }
}
