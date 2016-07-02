// Decompiled with JetBrains decompiler
// Type: PlayUO.LayoutEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;

namespace PlayUO
{
  public sealed class LayoutEntry
  {
    private string name;
    private int[] parameters;
    private Dictionary<string, string> attributes;

    public int[] Parameters
    {
      get
      {
        return this.parameters;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public int this[int index]
    {
      get
      {
        if (index < 0 || index >= this.parameters.Length)
          return 0;
        return this.parameters[index];
      }
    }

    public LayoutEntry(string format)
    {
      string[] strArray = format.Split(' ');
      if (strArray.Length <= 0)
        return;
      this.name = strArray[0];
      using (new ScratchList<int>())
      {
        List<int> intList = new List<int>();
        for (int index1 = 1; index1 < strArray.Length; ++index1)
        {
          try
          {
            int int32 = Convert.ToInt32(strArray[index1]);
            intList.Add(int32);
          }
          catch
          {
            int length = strArray[index1].IndexOf('=');
            if (length > 0)
            {
              try
              {
                string index2 = strArray[index1].Substring(0, length);
                string str = strArray[index1].Substring(length + 1);
                if (this.attributes == null)
                  this.attributes = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
                this.attributes[index2] = str;
              }
              catch
              {
              }
            }
          }
        }
        this.parameters = intList.ToArray();
      }
    }

    public string GetAttribute(string name)
    {
      string str = (string) null;
      if (this.attributes != null)
        this.attributes.TryGetValue(name, out str);
      return str;
    }
  }
}
