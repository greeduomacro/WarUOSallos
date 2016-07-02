// Decompiled with JetBrains decompiler
// Type: PlayUO.GDragAmount
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;

namespace PlayUO
{
  public class GDragAmount : GDragable
  {
    private bool m_First = true;
    private Item m_Item;
    private int m_Amount;
    private GButtonNew m_Okay;
    private GSlider m_Slider;
    private GTextBox m_TextBox;
    private object m_ToDestroy;

    public object ToDestroy
    {
      get
      {
        return this.m_ToDestroy;
      }
      set
      {
        this.m_ToDestroy = value;
      }
    }

    public Item Item
    {
      get
      {
        return this.m_Item;
      }
    }

    public GDragAmount(Item item)
      : base(2140, 0, 0)
    {
      this.m_Item = item;
      int num = (int) (ushort) this.m_Item.Amount;
      this.m_Amount = num;
      this.m_Okay = new GButtonNew(2074, 2076, 2075, 102, 37);
      this.m_Okay.CanEnter = true;
      this.m_Okay.Clicked += new EventHandler(this.Okay_Clicked);
      this.m_Children.Add((Gump) this.m_Okay);
      GSlider gslider = new GSlider(2117, 35, 16, 95, 15, (double) num, 0.0, (double) num, 1.0);
      gslider.OnValueChange = new OnValueChange(this.Slider_OnValueChange);
      this.m_Children.Add((Gump) gslider);
      this.m_Slider = gslider;
      this.m_Children.Add((Gump) new GHotspot(28, 16, 109, 15, (Gump) gslider));
      GTextBox gtextBox = new GTextBox(0, false, 26, 43, 66, 15, num.ToString(), (IFont) Engine.GetFont(1), Hues.Load(1109), Hues.Load(1109), Hues.Load(1109));
      gtextBox.OnTextChange += new OnTextChange(this.TextBox_OnTextChange);
      gtextBox.OnBeforeTextChange += new OnBeforeTextChange(this.TextBox_OnBeforeTextChange);
      gtextBox.EnterButton = (IClickable) this.m_Okay;
      this.m_Children.Add((Gump) gtextBox);
      this.m_TextBox = gtextBox;
      gtextBox.Focus();
      this.m_IsDragging = true;
      this.m_OffsetX = this.Width / 2;
      this.m_OffsetY = this.Height / 2;
      Gumps.LastOver = (Gump) this;
      Gumps.Drag = (Gump) this;
      Gumps.Focus = (Gump) this;
      this.m_X = Engine.m_xMouse - this.m_OffsetX;
      this.m_Y = Engine.m_yMouse - this.m_OffsetY;
    }

    private void Okay_Clicked(object sender, EventArgs e)
    {
      try
      {
        int num = Convert.ToInt32(this.m_TextBox.String);
        if (num <= 0)
        {
          Gumps.Destroy((Gump) this);
        }
        else
        {
          if (num > this.m_Amount)
            num = this.m_Amount;
          this.m_IsDragging = false;
          this.m_Item.Amount = num;
          Network.Send((Packet) new PPickupItem(this.m_Item, this.m_Item.Amount));
          Gumps.Desktop.Children.Add((Gump) new GDraggedItem(this.m_Item));
          if (this.m_ToDestroy is Gump)
          {
            if (((Gump) this.m_ToDestroy).Parent is GContainer)
              ((GContainer) ((Gump) this.m_ToDestroy).Parent).m_Hash[(object) this.m_Item] = (object) null;
            Gumps.Destroy((Gump) this.m_ToDestroy);
          }
          else if (this.m_ToDestroy is Item)
          {
            Item obj = (Item) this.m_ToDestroy;
            obj.RestoreInfo = new RestoreInfo(obj);
            World.Remove(obj);
          }
          Gumps.Destroy((Gump) this);
        }
      }
      catch
      {
      }
    }

    private void Slider_OnValueChange(double v, double old, Gump g)
    {
      this.m_TextBox.String = ((int) v).ToString();
    }

    private void TextBox_OnBeforeTextChange(Gump g)
    {
      if (!this.m_First)
        return;
      this.m_First = false;
      ((GTextBox) g).String = "";
    }

    private void TextBox_OnTextChange(string text, Gump g)
    {
      try
      {
        int int32 = Convert.ToInt32(text);
        if (int32 < 0)
          this.m_Slider.SetValue(0.0, true);
        else if (int32 > this.m_Amount)
          this.m_Slider.SetValue((double) this.m_Amount, true);
        else
          this.m_Slider.SetValue((double) int32, false);
      }
      catch
      {
      }
    }
  }
}
