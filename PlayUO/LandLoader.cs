// Decompiled with JetBrains decompiler
// Type: PlayUO.LandLoader
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class LandLoader : ILoader
  {
    private int m_LandID;

    public LandLoader(int LandID)
    {
      this.m_LandID = LandID;
    }

    public void Load()
    {
      MapLighting.CheckStretchTable();
      if (!MapLighting.m_AlwaysStretch[this.m_LandID & 16383])
        Hues.Default.GetTerrainIsometric(this.m_LandID);
      int textureId = (int) Map.GetTexture(this.m_LandID);
      if (textureId <= 0 || textureId >= 16384)
        return;
      Hues.Default.GetTerrainTexture(textureId);
    }
  }
}
