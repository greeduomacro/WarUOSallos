// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.VolumeData
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public abstract class VolumeData : PersistableObject
  {
    protected Volume m_Volume;

    public Volume Volume
    {
      get
      {
        return this.m_Volume;
      }
    }

    public VolumeData()
    {
      this.m_Volume = new Volume();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("volume", this.m_Volume.Scale);
      op.SetBoolean("mute", this.m_Volume.Mute);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_Volume.Scale = ip.GetInt32("volume");
      this.m_Volume.Mute = ip.GetBoolean("mute");
    }
  }
}
