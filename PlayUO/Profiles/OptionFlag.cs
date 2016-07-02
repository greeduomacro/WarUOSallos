// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.OptionFlag
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO.Profiles
{
  [Flags]
  public enum OptionFlag
  {
    None = 0,
    AlwaysRun = 1,
    IncomingNames = 2,
    NotorietyHalos = 4,
    ProtectHeals = 8,
    ProtectCures = 16,
    ProtectPoisons = 32,
    SiegeRuleset = 64,
    QueueTargets = 128,
    Scavenger = 256,
    Screenshots = 512,
    MiniHealth = 1024,
    ContainerGrid = 2048,
    SmoothWalk = 4096,
    KeyPassthrough = 8192,
    MoongateConfirmation = 16384,
    AlwaysLight = 32768,
    HotkeysEnabled = 65536,
    ClearHandsBeforeCast = 131072,
    Default = KeyPassthrough | SmoothWalk | ContainerGrid | MiniHealth | Screenshots | Scavenger | ProtectPoisons | ProtectCures | ProtectHeals | NotorietyHalos | IncomingNames,
  }
}
