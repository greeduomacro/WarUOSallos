// Decompiled with JetBrains decompiler
// Type: PlayUO.GItemCounter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GItemCounter : GAlphaBackground
  {
    private IItemValidator m_Validator;
    private GItemArt m_Image;
    private GLabel m_Label;
    private int m_LastAmount;

    public GItemCounter(ItemIDValidator v)
      : base(0, 0, 100, 20)
    {
      this.m_Validator = (IItemValidator) v;
      this.m_Image = new GItemArt(3, 3, v.List[0]);
      this.m_Label = new GLabel("", Engine.DefaultFont, Hues.Bright, 0, 0);
      this.m_Image.X -= this.m_Image.Image.xMin;
      this.m_Image.Y -= this.m_Image.Image.yMin;
      this.m_Children.Add((Gump) this.m_Image);
      this.m_Children.Add((Gump) this.m_Label);
      this.m_LastAmount = int.MinValue;
    }

    public void Update(Item pack)
    {
      Item[] items = pack.FindItems(this.m_Validator);
      int num = 0;
      for (int index = 0; index < items.Length; ++index)
        num += (int) Math.Max((ushort) items[index].Amount, (ushort) 1);
      if (this.m_LastAmount == num)
        return;
      this.m_LastAmount = num;
      this.m_Label.Text = num.ToString();
      this.m_Label.Hue = num < 5 ? Hues.Load(34) : Hues.Bright;
      this.Size();
    }

    private void Size()
    {
      int num1 = this.m_Label.Image.xMax - this.m_Label.Image.xMin + 1;
      int num2 = this.m_Image.Image.yMax - this.m_Image.Image.yMin + 1;
      int num3 = this.m_Label.Image.yMax - this.m_Label.Image.yMin + 1;
      if (num3 > num2)
        num2 = num3;
      this.Height = num2 + 6;
      this.Width = num1 + 37;
      this.m_Label.X = 32 - this.m_Label.Image.xMin;
      this.m_Label.Y = (this.Height - num3) / 2 - this.m_Label.Image.yMin;
      this.m_Image.Y = (this.Height - (this.m_Image.Image.yMax - this.m_Image.Image.yMin + 1)) / 2 - this.m_Image.Image.yMin;
    }
  }
}
