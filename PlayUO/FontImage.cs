// Decompiled with JetBrains decompiler
// Type: PlayUO.FontImage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class FontImage
  {
    public int xWidth;
    public int yHeight;
    public int xDelta;
    public byte[] xyPixels;

    public FontImage(int xWidth, int yHeight)
    {
      this.xWidth = xWidth;
      this.yHeight = yHeight;
      this.xDelta = xWidth + (-xWidth & 3);
      this.xyPixels = new byte[this.xDelta * yHeight];
    }
  }
}
