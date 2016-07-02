// Decompiled with JetBrains decompiler
// Type: PlayUO.RestoreInfo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class RestoreInfo
  {
    public int m_X;
    public int m_Y;
    public int m_Z;
    public Agent m_Parent;

    public RestoreInfo(Item item)
    {
      this.m_X = item.X;
      this.m_Y = item.Y;
      this.m_Z = item.Z;
      this.m_Parent = item.Parent;
    }
  }
}
