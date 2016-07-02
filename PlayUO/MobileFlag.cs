// Decompiled with JetBrains decompiler
// Type: PlayUO.MobileFlag
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  [Flags]
  public enum MobileFlag
  {
    None = 0,
    Frozen = 1,
    Female = 2,
    Poisoned = 4,
    YellowHits = 8,
    FactionShop = 16,
    Warmode = 64,
    Hidden = 128,
    ValidMask = Hidden | Warmode | FactionShop | YellowHits | Poisoned | Female | Frozen,
    InvalidMask = -224,
  }
}
