// Decompiled with JetBrains decompiler
// Type: PlayUO.IMessageOwner
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public interface IMessageOwner : IPoint2D
  {
    int MessageX { get; set; }

    int MessageY { get; set; }

    int MessageFrame { get; set; }

    void OnSingleClick();

    void OnDoubleClick();

    void OnTarget();
  }
}
