// Decompiled with JetBrains decompiler
// Type: PlayUO.Targeting.HuePickerTargetHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Targeting
{
  internal class HuePickerTargetHandler : ClientTargetHandler
  {
    private GHuePicker m_Picker;
    private GBrightnessBar m_Bar;

    public HuePickerTargetHandler(GHuePicker picker, GBrightnessBar bar)
    {
      this.m_Picker = picker;
      this.m_Bar = bar;
    }

    protected override bool OnTarget(Item item)
    {
      this.UpdateHue((int) item.Hue);
      return true;
    }

    protected override bool OnTarget(StaticTarget staticTarget)
    {
      this.UpdateHue(staticTarget.Hue.HueID());
      return true;
    }

    private void UpdateHue(int desiredHue)
    {
      desiredHue &= 16383;
      if (desiredHue >= 2 && desiredHue < 1002)
      {
        desiredHue -= 2;
        int num1 = desiredHue % 5;
        desiredHue /= 5;
        int num2 = desiredHue % 20;
        desiredHue /= 20;
        int num3 = desiredHue;
        this.m_Picker.Brightness = num1;
        this.m_Picker.ShadeX = num2;
        this.m_Picker.ShadeY = num3;
        this.m_Bar.Refresh();
      }
      else if (desiredHue >= 2)
        Engine.AddTextMessage("You cannot figure out the proper dye mixture for that color.");
      else
        Engine.AddTextMessage("Do you think that is colorful?");
    }
  }
}
