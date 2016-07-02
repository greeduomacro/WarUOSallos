// Decompiled with JetBrains decompiler
// Type: PlayUO.Palette
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Palette
  {
    private int _hueId;
    private ushort[] _colors;

    public int HueId
    {
      get
      {
        return this._hueId;
      }
    }

    public ushort[] Colors
    {
      get
      {
        return this._colors;
      }
    }

    public Palette(int hueId, ushort[] colors)
    {
      this._hueId = hueId;
      this._colors = colors;
    }
  }
}
