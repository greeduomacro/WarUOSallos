// Decompiled with JetBrains decompiler
// Type: PlayUO.UnpackLeaf
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class UnpackLeaf
  {
    public short m_Value = -1;
    public UnpackLeaf m_Left;
    public UnpackLeaf m_Right;
    public int[] m_Cache;
    public short m_Index;

    public UnpackLeaf(int index)
    {
      this.m_Index = (short) index;
    }
  }
}
