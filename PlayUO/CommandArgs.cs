// Decompiled with JetBrains decompiler
// Type: PlayUO.CommandArgs
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;

namespace PlayUO
{
  public sealed class CommandArgs
  {
    private Mobile m_Mobile;
    private string[] m_Parameters;
    private string[] m_Arguments;
    private int m_Step;
    private bool m_GoDefault;

    public Mobile Mobile
    {
      get
      {
        return this.m_Mobile;
      }
    }

    public string[] Parameters
    {
      get
      {
        return this.m_Parameters;
      }
    }

    public string[] Arguments
    {
      get
      {
        return this.m_Arguments;
      }
    }

    public string Command
    {
      get
      {
        if (this.m_Step - 1 < 0 || this.m_Step - 1 >= this.m_Parameters.Length)
          return "";
        return this.m_Parameters[this.m_Step - 1];
      }
    }

    public int Step
    {
      get
      {
        return this.m_Step;
      }
      set
      {
        this.m_Step = value;
      }
    }

    public bool GoDefault
    {
      get
      {
        return this.m_GoDefault;
      }
      set
      {
        this.m_GoDefault = value;
      }
    }

    public int Length
    {
      get
      {
        return this.m_Parameters.Length - this.m_Step;
      }
    }

    public CommandArgs(Mobile mob, string args)
    {
      this.m_Mobile = mob;
      CommandArgs.Split(args, out this.m_Parameters, out this.m_Arguments);
    }

    public CommandArgs(Mobile mob, string[] parms, string[] args)
    {
      this.m_Mobile = mob;
      this.m_Parameters = parms;
      this.m_Arguments = args;
    }

    public string GetArgument(int index)
    {
      if (index < 0 || index >= this.m_Arguments.Length - this.m_Step)
        return "";
      return this.m_Arguments[index + this.m_Step];
    }

    public string GetString(int index)
    {
      if (index < 0 || index >= this.m_Parameters.Length - this.m_Step)
        return "";
      return this.m_Parameters[index + this.m_Step];
    }

    public int GetInt32(int index)
    {
      int result = 0;
      if (index >= 0 && index < this.m_Parameters.Length - this.m_Step)
        int.TryParse(this.m_Parameters[index + this.m_Step], out result);
      return result;
    }

    public bool GetBoolean(int index)
    {
      if (index < 0 || index >= this.m_Parameters.Length - this.m_Step)
        return false;
      switch (this.GetString(index).ToLower())
      {
        case "1":
        case "on":
        case "yes":
        case "true":
          return true;
        case "0":
        case "no":
        case "off":
        case "false":
          return false;
        default:
          try
          {
            return bool.Parse(this.m_Parameters[index + this.m_Step]);
          }
          catch
          {
            return false;
          }
      }
    }

    public double GetDouble(int index)
    {
      if (index >= 0)
      {
        if (index < this.m_Parameters.Length - this.m_Step)
        {
          try
          {
            return double.Parse(this.m_Parameters[index + this.m_Step]);
          }
          catch
          {
            return 0.0;
          }
        }
      }
      return 0.0;
    }

    public TimeSpan GetTimeSpan(int index)
    {
      if (index >= 0)
      {
        if (index < this.m_Parameters.Length - this.m_Step)
        {
          try
          {
            return TimeSpan.Parse(this.m_Parameters[index + this.m_Step]);
          }
          catch
          {
            return TimeSpan.Zero;
          }
        }
      }
      return TimeSpan.Zero;
    }

    public static void Split(string value, out string[] parms, out string[] args)
    {
      char[] charArray = value.ToCharArray();
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      int startIndex1 = 0;
      while (startIndex1 < charArray.Length)
      {
        switch (charArray[startIndex1])
        {
          case '"':
            int startIndex2 = startIndex1 + 1;
            int index1 = startIndex2;
            while (index1 < charArray.Length && ((int) charArray[index1] != 34 || (int) charArray[index1 - 1] == 92))
              ++index1;
            arrayList1.Add((object) value.Substring(startIndex2, index1 - startIndex2));
            arrayList2.Add((object) value.Substring(startIndex2 - 1));
            startIndex1 = index1 + 2;
            continue;
          case ' ':
            ++startIndex1;
            continue;
          default:
            int index2 = startIndex1;
            while (index2 < charArray.Length && (int) charArray[index2] != 32)
              ++index2;
            arrayList1.Add((object) value.Substring(startIndex1, index2 - startIndex1));
            arrayList2.Add((object) value.Substring(startIndex1));
            startIndex1 = index2 + 1;
            continue;
        }
      }
      parms = (string[]) arrayList1.ToArray(typeof (string));
      args = (string[]) arrayList2.ToArray(typeof (string));
    }
  }
}
