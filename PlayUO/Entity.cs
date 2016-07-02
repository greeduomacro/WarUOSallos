// Decompiled with JetBrains decompiler
// Type: PlayUO.Entity
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class Entity : IEntity
  {
    private int m_Serial;

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public Entity(int serial)
    {
      this.m_Serial = serial;
    }

    public static implicit operator Entity(int serial)
    {
      return new Entity(serial);
    }
  }
}
