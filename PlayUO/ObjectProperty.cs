// Decompiled with JetBrains decompiler
// Type: PlayUO.ObjectProperty
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Text.RegularExpressions;

namespace PlayUO
{
  public class ObjectProperty
  {
    private static Regex m_ArgReplace = new Regex("~(?<1>\\d+).*?~", RegexOptions.Singleline);
    private int m_Number;
    private string m_Arguments;
    private string m_Text;
    private static string[] m_Args;

    public int Number
    {
      get
      {
        return this.m_Number;
      }
    }

    public string Arguments
    {
      get
      {
        return this.m_Arguments;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
    }

    public ObjectProperty(int number, string arguments)
    {
      this.m_Number = number;
      this.m_Arguments = arguments;
      ObjectProperty.m_Args = arguments.Split('\t');
      for (int index = 0; index < ObjectProperty.m_Args.Length; ++index)
      {
        if (ObjectProperty.m_Args[index].Length > 1)
        {
          if (ObjectProperty.m_Args[index].StartsWith("#"))
          {
            try
            {
              ObjectProperty.m_Args[index] = Localization.GetString(Convert.ToInt32(ObjectProperty.m_Args[index].Substring(1)));
            }
            catch
            {
            }
          }
        }
      }
      this.m_Text = Localization.GetString(number);
      this.m_Text = ObjectProperty.m_ArgReplace.Replace(this.m_Text, new MatchEvaluator(ObjectProperty.ArgReplace_Eval));
    }

    private static string ArgReplace_Eval(Match m)
    {
      try
      {
        int index = Convert.ToInt32(m.Groups[1].Value) - 1;
        return ObjectProperty.m_Args[index];
      }
      catch
      {
        return m.Value;
      }
    }
  }
}
