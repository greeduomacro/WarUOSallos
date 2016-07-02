// Decompiled with JetBrains decompiler
// Type: PlayUO.RenderProfile
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Diagnostics;

namespace PlayUO
{
  public class RenderProfile
  {
    public int _pushes;
    public int _draws;
    public int _tex0;
    public int _tex1;
    public int _psh;
    public Stopwatch _drawTime;
    public Stopwatch _composeTime;
    public Stopwatch _acquireTime;
    public Stopwatch _storeTime;
    public Stopwatch _pushTime;
    public Stopwatch _worldTime;
    public Stopwatch _gumpTime;

    public RenderProfile()
    {
      this._drawTime = new Stopwatch();
      this._composeTime = new Stopwatch();
      this._acquireTime = new Stopwatch();
      this._storeTime = new Stopwatch();
      this._pushTime = new Stopwatch();
      this._worldTime = new Stopwatch();
      this._gumpTime = new Stopwatch();
    }

    public void Reset()
    {
      this._drawTime.Reset();
      this._composeTime.Reset();
      this._acquireTime.Reset();
      this._storeTime.Reset();
      this._pushTime.Reset();
      this._worldTime.Reset();
      this._gumpTime.Reset();
      this._pushes = 0;
      this._draws = 0;
      this._tex0 = 0;
      this._tex1 = 0;
      this._psh = 0;
    }

    public override string ToString()
    {
      return string.Format("{{ compose = {0:P}; acquire={1:P}; store = {2:P}; push = {3:P}; }}\n{{ world = {4:P}; gumps = {5:P}; }}", (object) ((this._composeTime.Elapsed - this._storeTime.Elapsed - this._acquireTime.Elapsed).TotalSeconds / this._drawTime.Elapsed.TotalSeconds), (object) (this._storeTime.Elapsed.TotalSeconds / this._drawTime.Elapsed.TotalSeconds), (object) (this._acquireTime.Elapsed.TotalSeconds / this._drawTime.Elapsed.TotalSeconds), (object) (this._pushTime.Elapsed.TotalSeconds / this._drawTime.Elapsed.TotalSeconds), (object) (this._worldTime.Elapsed.TotalSeconds / this._drawTime.Elapsed.TotalSeconds), (object) (this._gumpTime.Elapsed.TotalSeconds / this._drawTime.Elapsed.TotalSeconds));
    }
  }
}
