// Decompiled with JetBrains decompiler
// Type: PlayUO.HtmlElement
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace PlayUO
{
  public class HtmlElement
  {
    private static Regex[] m_AttributesRegex = new Regex[3]{ new Regex("(?<name>\\w+)\\s*=\\s*'(?<value>.*?)'"), new Regex("(?<name>\\w+)\\s*=\\s*\"(?<value>.*?)\""), new Regex("(?<name>\\w+)\\s*=\\s*(?<value>[^\\s]*)") };
    private string m_Name;
    private ElementType m_Type;
    private Hashtable m_Attributes;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public ElementType Type
    {
      get
      {
        return this.m_Type;
      }
    }

    public HtmlElement(string name, ElementType type, Hashtable attributes)
    {
      this.m_Name = name;
      this.m_Type = type;
      this.m_Attributes = attributes;
    }

    public string GetAttribute(string name)
    {
      if (this.m_Attributes == null)
        return (string) null;
      return (string) this.m_Attributes[(object) name];
    }

    public static HtmlElement[] GetElements(string text)
    {
      ArrayList arrayList = new ArrayList();
      int startIndex1 = 0;
      int startIndex2;
      while (true)
      {
        startIndex2 = startIndex1;
        int startIndex3 = text.IndexOf('<', startIndex1);
        if (startIndex3 != -1)
        {
          int num = text.IndexOf('>', startIndex3 + 1);
          if (num != -1)
          {
            if (startIndex3 != startIndex2)
              arrayList.Add((object) new HtmlElement(text.Substring(startIndex2, startIndex3 - startIndex2), ElementType.Text, (Hashtable) null));
            startIndex1 = num + 1;
            arrayList.Add((object) HtmlElement.Parse(text.Substring(startIndex3, startIndex1 - startIndex3)));
          }
          else
            goto label_4;
        }
        else
          break;
      }
      arrayList.Add((object) new HtmlElement(text.Substring(startIndex2), ElementType.Text, (Hashtable) null));
      goto label_8;
label_4:
      arrayList.Add((object) new HtmlElement(text.Substring(startIndex2), ElementType.Text, (Hashtable) null));
label_8:
      return (HtmlElement[]) arrayList.ToArray(typeof (HtmlElement));
    }

    public static HtmlElement Parse(string ele)
    {
      if (ele.StartsWith("<"))
        ele = ele.Substring(1);
      if (ele.EndsWith(">"))
        ele = ele.Substring(0, ele.Length - 1);
      ElementType type = ele.StartsWith("/") ? ElementType.End : ElementType.Start;
      if (type == ElementType.End)
        ele = ele.Substring(1);
      int length = ele.IndexOf(' ');
      if (length == -1)
        return new HtmlElement(ele, type, (Hashtable) null);
      string name = ele.Substring(0, length);
      int num;
      string input = ele.Substring(num = length + 1);
      Hashtable attributes = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      for (int index = 0; index < HtmlElement.m_AttributesRegex.Length; ++index)
      {
        while (true)
        {
          Match match = HtmlElement.m_AttributesRegex[index].Match(input);
          if (match.Success)
          {
            string str1 = match.Groups["name"].Value;
            string str2 = match.Groups["value"].Value;
            attributes[(object) str1] = (object) str2;
            input = input.Remove(match.Index, match.Length);
          }
          else
            break;
        }
      }
      return new HtmlElement(name, type, attributes);
    }
  }
}
