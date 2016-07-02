// Decompiled with JetBrains decompiler
// Type: PlayUO.GObjectEditor
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace PlayUO
{
  public class GObjectEditor : GWindowsForm
  {
    private object m_Object;
    private static GObjectEditor m_Instance;
    private GEditorPanel m_Panel;

    public object Object
    {
      get
      {
        return this.m_Object;
      }
    }

    public static bool IsOpen
    {
      get
      {
        return GObjectEditor.m_Instance != null;
      }
    }

    public GObjectEditor(object obj)
      : base(0, 0, 317, 392)
    {
      Gumps.Focus = (Gump) this;
      this.m_Object = obj;
      this.m_NonRestrictivePicking = true;
      this.Text = "Option Editor";
      Hashtable categories = new Hashtable();
      this.BuildCategories(obj, categories);
      ArrayList arrayList = new ArrayList((ICollection) categories);
      arrayList.Sort((IComparer) new CategorySorter());
      ArrayList panels = new ArrayList();
      foreach (DictionaryEntry dictionaryEntry in arrayList)
      {
        string category = (string) dictionaryEntry.Key;
        ArrayList entries = (ArrayList) dictionaryEntry.Value;
        GCategoryPanel gcategoryPanel = new GCategoryPanel(obj, category, entries);
        panels.Add((object) gcategoryPanel);
      }
      GEditorPanel geditorPanel = new GEditorPanel(panels, 360);
      this.m_Panel = geditorPanel;
      geditorPanel.X += 2;
      geditorPanel.Y += 3;
      this.Client.m_NonRestrictivePicking = true;
      this.Client.Children.Add((Gump) geditorPanel);
      this.Center();
    }

    public static void Open(object obj)
    {
      if (GObjectEditor.m_Instance != null)
        return;
      GObjectEditor.m_Instance = new GObjectEditor(obj);
      Gumps.Desktop.Children.Add((Gump) GObjectEditor.m_Instance);
      Gumps.Focus = (Gump) GObjectEditor.m_Instance;
    }

    protected internal override void OnDispose()
    {
      if (TargetManager.Client is SetItemPropertyTarget)
        TargetManager.Client = (ClientTargetHandler) null;
      GObjectEditor.m_Instance = (GObjectEditor) null;
    }

    protected internal override void OnDragStart()
    {
      base.OnDragStart();
      this.m_Panel.Reset();
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      this.m_Panel.Reset();
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
      if (!(Gumps.Focus is GSliderBase) && Gumps.Focus != null && Gumps.Focus.IsChildOf((Gump) this))
        return;
      this.m_Panel.Reset();
    }

    private void BuildCategories(object obj, Hashtable categories)
    {
      foreach (PropertyInfo property in obj.GetType().GetProperties())
      {
        OptionableAttribute optionableAttribute = this.GetAttribute((MemberInfo) property, typeof (OptionableAttribute)) as OptionableAttribute;
        if (optionableAttribute != null && (!optionableAttribute.OnlyAOS || Engine.ServerFeatures.AOS))
        {
          if (!property.CanWrite)
          {
            this.BuildCategories(property.GetValue(obj, (object[]) null), categories);
          }
          else
          {
            ArrayList arrayList = (ArrayList) categories[(object) optionableAttribute.Category];
            if (arrayList == null)
              categories[(object) optionableAttribute.Category] = (object) (arrayList = new ArrayList());
            arrayList.Add((object) new ObjectEditorEntry(property, obj, (object) optionableAttribute, this.GetAttribute((MemberInfo) property, typeof (OptionRangeAttribute)), this.GetAttribute((MemberInfo) property, typeof (OptionHueAttribute))));
          }
        }
      }
    }

    public object GetAttribute(MemberInfo mi, Type type)
    {
      object[] customAttributes = mi.GetCustomAttributes(type, false);
      if (customAttributes == null)
        return (object) null;
      if (customAttributes.Length == 0)
        return (object) null;
      return customAttributes[0];
    }
  }
}
