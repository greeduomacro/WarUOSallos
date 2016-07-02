// Decompiled with JetBrains decompiler
// Type: PlayUO.GTransparencyGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GTransparencyGump : GLabel
  {
    public override int Y
    {
      get
      {
        return Stats.yOffset;
      }
      set
      {
      }
    }

    public GTransparencyGump()
      : base("Transparency On", Engine.DefaultFont, Engine.DefaultHue, 4, 4)
    {
    }

    protected internal override void Render(int X, int Y)
    {
      if (!Engine.m_Ingame || !Renderer.Transparency)
        return;
      base.Render(X, Y);
      Stats.Add((Gump) this);
    }
  }
}
