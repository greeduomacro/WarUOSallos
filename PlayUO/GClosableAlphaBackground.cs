// Decompiled with JetBrains decompiler
// Type: PlayUO.GClosableAlphaBackground
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Windows.Forms;

namespace PlayUO
{
  public class GClosableAlphaBackground : GAlphaBackground
  {
    public GClosableAlphaBackground(int X, int Y, int Width, int Height)
      : base(X, Y, Width, Height)
    {
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if ((mb & MouseButtons.Right) == MouseButtons.None)
        return;
      Gumps.Destroy((Gump) this);
    }
  }
}
