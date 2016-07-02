// Decompiled with JetBrains decompiler
// Type: PlayUO.GPropertyHuePicker
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GPropertyHuePicker : GAlphaBackground
  {
    private GHuePicker m_HuePicker;
    private GBrightnessBar m_Bar;
    private GPropertyEntry m_Entry;

    public GPropertyHuePicker(GPropertyEntry entry)
      : base(0, 0, 200, 150)
    {
      int num1 = (int) entry.Entry.Property.GetValue(entry.Object, (object[]) null);
      this.m_Entry = entry;
      this.m_CanDrag = false;
      this.FillColor = GumpColors.Control;
      this.BorderColor = GumpColors.ControlDarkDark;
      this.FillAlpha = 1f;
      GHuePicker Target = this.m_HuePicker = new GHuePicker(7, 7);
      Target.m_CanDrag = false;
      Target.OnHueSelect = new OnHueSelect(this.HueSelected);
      this.m_Children.Add((Gump) Target);
      GBrightnessBar gbrightnessBar = this.m_Bar = new GBrightnessBar(Target.X + Target.Width + 1, Target.Y, 15, Target.Height, Target);
      gbrightnessBar.m_CanDrag = false;
      this.m_Children.Add((Gump) gbrightnessBar);
      if (num1 >= 2 && num1 <= 1001)
      {
        int num2 = num1 - 2;
        Target.ShadeX = num2 / 5 % 20;
        Target.ShadeY = num2 / 5 / 20;
        Target.Brightness = num2 % 5;
        gbrightnessBar.Refresh();
      }
      this.m_Children.Add((Gump) new GSingleBorder(gbrightnessBar.X - 1, gbrightnessBar.Y, 1, gbrightnessBar.Height));
      this.Width = 7 + Target.Width + gbrightnessBar.Width + 7;
      this.Height = 7 + Target.Height + 7;
    }

    private void HueSelected(int hue, Gump g)
    {
      this.m_Entry.SetValue((object) hue);
    }

    private void HueReleased(int hue, Gump g)
    {
      this.m_Entry.SetValue((object) hue);
      Gumps.Destroy((Gump) this);
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      int x = this.m_HuePicker.X;
      int y = this.m_HuePicker.Y;
      int width1 = this.m_HuePicker.Width;
      int width2 = this.m_Bar.Width;
      int height = this.m_HuePicker.Height;
      Renderer.SetTexture((Texture) null);
      GumpPaint.DrawSunken3D(X + this.m_HuePicker.X - 2, Y + this.m_HuePicker.Y - 2, this.m_HuePicker.Width + 1 + this.m_Bar.Width + 4, this.m_HuePicker.Height + 4);
    }
  }
}
