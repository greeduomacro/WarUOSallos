// Decompiled with JetBrains decompiler
// Type: PlayUO.PaperdollImage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class PaperdollImage
  {
    private int _gumpId;
    private int? _hueId;
    private float _alpha;

    public int GumpId
    {
      get
      {
        return this._gumpId;
      }
    }

    public int? HueId
    {
      get
      {
        return this._hueId;
      }
    }

    public float Alpha
    {
      get
      {
        return this._alpha;
      }
    }

    public PaperdollImage(int gumpId, int? hueId, float alpha)
    {
      this._gumpId = gumpId;
      this._hueId = hueId;
      this._alpha = alpha;
    }
  }
}
