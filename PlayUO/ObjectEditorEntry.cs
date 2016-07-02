// Decompiled with JetBrains decompiler
// Type: PlayUO.ObjectEditorEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Reflection;

namespace PlayUO
{
  public class ObjectEditorEntry : IComparable
  {
    private PropertyInfo m_Property;
    private object m_Object;
    private OptionableAttribute m_Optionable;
    private OptionRangeAttribute m_Range;
    private OptionHueAttribute m_Hue;

    public PropertyInfo Property
    {
      get
      {
        return this.m_Property;
      }
    }

    public object Object
    {
      get
      {
        return this.m_Object;
      }
    }

    public OptionableAttribute Optionable
    {
      get
      {
        return this.m_Optionable;
      }
    }

    public OptionRangeAttribute Range
    {
      get
      {
        return this.m_Range;
      }
    }

    public OptionHueAttribute Hue
    {
      get
      {
        return this.m_Hue;
      }
    }

    public ObjectEditorEntry(PropertyInfo prop, object obj, object optionable, object range, object hue)
    {
      this.m_Property = prop;
      this.m_Object = obj;
      this.m_Optionable = optionable as OptionableAttribute;
      this.m_Range = range as OptionRangeAttribute;
      this.m_Hue = hue as OptionHueAttribute;
    }

    public int CompareTo(object obj)
    {
      return this.m_Optionable.Name.CompareTo(((ObjectEditorEntry) obj).m_Optionable.Name);
    }
  }
}
