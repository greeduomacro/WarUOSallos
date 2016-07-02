// Decompiled with JetBrains decompiler
// Type: PlayUO.GPropertyEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GPropertyEntry : GEmpty
  {
    private object m_Object;
    private ObjectEditorEntry m_Entry;
    private GAlphaBackground m_NameBack;
    private GAlphaBackground m_ValueBack;
    private GLabel m_Name;
    private GLabel m_Value;
    private GAlphaBackground m_Hue;
    private GPropertyHuePicker m_Picker;
    private Clipper m_Clipper;

    public ObjectEditorEntry Entry
    {
      get
      {
        return this.m_Entry;
      }
    }

    public object Object
    {
      get
      {
        return this.m_Object;
      }
    }

    public GPropertyEntry(object obj, ObjectEditorEntry entry)
      : base(0, 0, 279, 22)
    {
      this.m_Object = obj;
      this.m_Entry = entry;
      this.m_NonRestrictivePicking = true;
      bool flag = entry.Property.PropertyType == typeof (Volume);
      this.m_NameBack = new GAlphaBackground(0, 0, 140, 22);
      this.m_NameBack.FillColor = GumpColors.Window;
      this.m_NameBack.FillAlpha = 1f;
      this.m_NameBack.DrawBorder = false;
      this.m_NameBack.ShouldHitTest = false;
      this.m_Children.Add((Gump) this.m_NameBack);
      this.m_ValueBack = new GAlphaBackground(139, 0, 140, 22);
      this.m_ValueBack.FillColor = GumpColors.Window;
      this.m_ValueBack.FillAlpha = 1f;
      this.m_ValueBack.BorderColor = GumpColors.Control;
      this.m_ValueBack.ShouldHitTest = false;
      this.m_ValueBack.DrawBorder = false;
      this.m_Children.Add((Gump) this.m_ValueBack);
      this.m_Name = new GLabel(entry.Optionable.Name, (IFont) Engine.GetUniFont(2), GumpHues.WindowText, 0, 0);
      this.m_Name.X = 5 - this.m_Name.Image.xMin;
      this.m_Name.Y = (22 - (this.m_Name.Image.yMax - this.m_Name.Image.yMin + 1)) / 2 - this.m_Name.Image.yMin;
      this.m_NameBack.Children.Add((Gump) this.m_Name);
      object obj1 = entry.Property.GetValue(obj, (object[]) null);
      string valString = this.GetValString(obj1);
      if (flag)
        return;
      IFont Font = (obj1 is ValueType ? (obj1.Equals(entry.Optionable.Default) ? 1 : 0) : (object.ReferenceEquals(obj1, entry.Optionable.Default) ? 1 : 0)) == 0 ? (IFont) Engine.GetUniFont(1) : (IFont) Engine.GetUniFont(2);
      this.m_Value = new GLabel(valString, Font, GumpHues.WindowText, 0, 0);
      if (entry.Hue != null)
      {
        GAlphaBackground galphaBackground = new GAlphaBackground(4, 4, 22, 14);
        galphaBackground.FillColor = Engine.C16232((int) Hues.Load((int) obj1).Pixel(ushort.MaxValue));
        galphaBackground.FillAlpha = 1f;
        galphaBackground.ShouldHitTest = false;
        this.m_ValueBack.Children.Add((Gump) galphaBackground);
        this.m_Value.Text = "Hue";
        this.m_Value.X = 30 - this.m_Value.Image.xMin;
        this.m_Value.Y = (22 - (this.m_Value.Image.yMax - this.m_Value.Image.yMin + 1)) / 2 - this.m_Value.Image.yMin;
        this.m_Hue = galphaBackground;
      }
      else
      {
        this.m_Value.X = 5 - this.m_Value.Image.xMin;
        this.m_Value.Y = (22 - (this.m_Value.Image.yMax - this.m_Value.Image.yMin + 1)) / 2 - this.m_Value.Image.yMin;
      }
      this.m_ValueBack.Children.Add((Gump) this.m_Value);
    }

    public void Reset()
    {
      if (this.m_Picker != null)
        Gumps.Destroy((Gump) this.m_Picker);
      this.m_Picker = (GPropertyHuePicker) null;
    }

    public void SetClipper(Clipper c)
    {
      this.m_Clipper = c;
      this.m_Name.Clipper = c;
      if (this.m_Value != null)
        this.m_Value.Clipper = c;
      this.m_NameBack.Clipper = c;
      this.m_ValueBack.Clipper = c;
      if (this.m_Hue == null)
        return;
      this.m_Hue.Clipper = c;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      Point screen = this.PointToScreen(new Point(X, Y));
      if (this.m_Clipper != null)
        return this.m_Clipper.Evaluate(screen);
      return true;
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      this.m_NameBack.FillColor = GumpPaint.Blend(GumpColors.Window, GumpColors.Highlight, 0.9f);
      this.m_ValueBack.FillColor = this.m_NameBack.FillColor;
    }

    protected internal override void OnMouseLeave()
    {
      this.m_NameBack.FillColor = GumpColors.Window;
      this.m_ValueBack.FillColor = this.m_NameBack.FillColor;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Left)
        return;
      if (this.Parent.Parent is GEditorPanel)
        ((GEditorPanel) this.Parent.Parent).Reset();
      if (this.m_Entry.Property.PropertyType == typeof (Volume))
        return;
      object obj = this.m_Entry.Property.GetValue(this.m_Object, (object[]) null);
      if (obj is bool)
        this.SetValue((object) !(bool) obj);
      else if (obj is Item || this.m_Entry.Property.PropertyType == typeof (Item))
        TargetManager.Client = (ClientTargetHandler) new SetItemPropertyTarget(this);
      else if (obj is Enum)
      {
        Array values = Enum.GetValues(obj.GetType());
        for (int index = 0; index < values.Length; ++index)
        {
          if (values.GetValue(index).Equals(obj))
          {
            this.SetValue(values.GetValue((index + 1) % values.Length));
            break;
          }
        }
      }
      else if (this.m_Entry.Hue != null)
      {
        if (this.m_Picker != null)
          return;
        GPropertyHuePicker gpropertyHuePicker = this.m_Picker = new GPropertyHuePicker(this);
        gpropertyHuePicker.X = this.Width - 1;
        gpropertyHuePicker.Y = 0;
        this.m_Children.Add((Gump) gpropertyHuePicker);
      }
      else if (this.m_Entry.Property.IsDefined(typeof (MacroEditorAttribute), true))
      {
        Gumps.Destroy(this.Parent.Parent.Parent.Parent);
        GMacroEditorForm.Open();
      }
      else
      {
        if (!this.m_Entry.Property.IsDefined(typeof (RenderSettingEditor), true))
          return;
        Gumps.Destroy(this.Parent.Parent.Parent.Parent);
        GRenderSettingEditorForm.Open();
      }
    }

    public void SetValue(object val)
    {
      this.m_Entry.Property.SetValue(this.m_Object, val, (object[]) null);
      if (this.m_Value == null)
        return;
      IFont font = (val is ValueType ? (val.Equals(this.m_Entry.Optionable.Default) ? 1 : 0) : (object.ReferenceEquals(val, this.m_Entry.Optionable.Default) ? 1 : 0)) == 0 ? (IFont) Engine.GetUniFont(1) : (IFont) Engine.GetUniFont(2);
      if (this.m_Hue == null)
        this.m_Value.Text = this.GetValString(val);
      else
        this.m_Hue.FillColor = Engine.C16232((int) Hues.Load((int) val).Pixel(ushort.MaxValue));
      this.m_Value.Font = font;
    }

    private string GetValString(object val)
    {
      return val != null ? (!(val is bool) ? (!(val is Item) ? val.ToString() : Localization.GetString(1020000 + (((Item) val).ID & 16383))) : ((bool) val ? "On" : "Off")) : "null";
    }
  }
}
