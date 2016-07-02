// Decompiled with JetBrains decompiler
// Type: PlayUO.ShadowManager
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Ultima.Data;

namespace PlayUO
{
  public static class ShadowManager
  {
    private static int[] _shadowBits;

    public static bool HasShadow(int itemID)
    {
      if (ShadowManager._shadowBits == null)
        ShadowManager._shadowBits = ShadowManager.CreateShadowBits();
      itemID &= 16383;
      return (ShadowManager._shadowBits[itemID >> 5] & 1 << itemID) != 0;
    }

    private static int[] CreateShadowBits()
    {
      int[] bits = new int[512];
      ShadowManager.SetShadowBit(bits, 3220, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3221, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3222, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3225, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3226, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3227, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3228, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3229, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3230, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3231, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3232, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3233, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3234, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3235, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3236, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3237, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3238, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3240, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3241, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3242, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3243, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3272, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3273, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3274, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3275, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3276, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3277, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3278, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3279, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3280, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3281, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3282, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3283, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3284, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3285, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3286, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3287, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3288, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3289, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3290, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3291, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3292, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3293, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3294, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3295, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3296, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3297, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3298, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3299, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3300, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3301, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3302, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3303, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3304, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3305, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3306, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3320, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3321, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3322, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3323, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3324, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3325, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3326, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3327, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3328, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3329, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3330, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3331, ShadowManager.ShadowType.Foliage);
      ShadowManager.SetShadowBit(bits, 3365, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3366, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3367, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3381, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3383, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3384, ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 3391, ShadowManager.ShadowType.Vegetation);
      ShadowManager.SetShadowBit(bits, 3392, ShadowManager.ShadowType.Vegetation);
      for (int itemId = 3393; itemId <= 3499; ++itemId)
        ShadowManager.SetShadowBit(bits, itemId, Map.m_ItemFlags[itemId][(TileFlag) 131072L] ? ShadowManager.ShadowType.Foliage : ShadowManager.ShadowType.Tree);
      for (int itemId = 4789; itemId <= 4807; ++itemId)
        ShadowManager.SetShadowBit(bits, itemId, Map.m_ItemFlags[itemId][(TileFlag) 131072L] ? ShadowManager.ShadowType.Foliage : ShadowManager.ShadowType.Tree);
      ShadowManager.SetShadowBit(bits, 4945, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4948, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4950, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4953, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4955, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4958, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4959, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4960, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 4962, ShadowManager.ShadowType.Rock);
      for (int itemId = 6001; itemId <= 6012; ++itemId)
        ShadowManager.SetShadowBit(bits, itemId, ShadowManager.ShadowType.Rock);
      ShadowManager.SetShadowBit(bits, 7038, ShadowManager.ShadowType.Tree);
      for (int itemId = 9325; itemId <= 9342; ++itemId)
        ShadowManager.SetShadowBit(bits, itemId, Map.m_ItemFlags[itemId][(TileFlag) 131072L] ? ShadowManager.ShadowType.Foliage : ShadowManager.ShadowType.Tree);
      for (int itemId = 9965; itemId <= 9971; ++itemId)
        ShadowManager.SetShadowBit(bits, itemId, Map.m_ItemFlags[itemId][(TileFlag) 131072L] ? ShadowManager.ShadowType.Foliage : ShadowManager.ShadowType.Tree);
      return bits;
    }

    private static void SetShadowBit(int[] bits, int itemId, ShadowManager.ShadowType type)
    {
      bits[itemId >> 5] |= 1 << itemId;
    }

    private enum ShadowType
    {
      Tree,
      Foliage,
      Vegetation,
      Rock,
    }
  }
}
