// Decompiled with JetBrains decompiler
// Type: PlayUO.Macro
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class Macro : IComparable
  {
    private MacroData m_Data;
    private int m_Index;
    private static Macro m_Current;
    private DateTime m_DelayEnd;

    public MacroData Data
    {
      get
      {
        return this.m_Data;
      }
    }

    public Keys Key
    {
      get
      {
        return this.m_Data.Key;
      }
      set
      {
        this.m_Data.Key = value;
      }
    }

    public Keys Mods
    {
      get
      {
        return this.m_Data.Mods;
      }
      set
      {
        this.m_Data.Mods = value;
      }
    }

    public bool Control
    {
      get
      {
        return this.m_Data.Control;
      }
      set
      {
        this.m_Data.Control = value;
      }
    }

    public bool Alt
    {
      get
      {
        return this.m_Data.Alt;
      }
      set
      {
        this.m_Data.Alt = value;
      }
    }

    public bool Shift
    {
      get
      {
        return this.m_Data.Shift;
      }
      set
      {
        this.m_Data.Shift = value;
      }
    }

    public ActionCollection Actions
    {
      get
      {
        return this.m_Data.Actions;
      }
    }

    public bool Running
    {
      get
      {
        return this.m_Index >= 0;
      }
    }

    public bool IsWheel
    {
      get
      {
        return this.m_Data.IsWheel;
      }
    }

    public Macro(MacroData data)
    {
      this.m_Data = data;
      this.m_Index = -1;
    }

    public void AddAction(Action a)
    {
      this.m_Data.Actions.Add(a);
    }

    public bool CheckKey(Keys key)
    {
      Keys modifierKeys = System.Windows.Forms.Control.ModifierKeys;
      if (key == this.Key)
        return modifierKeys == this.Mods;
      return false;
    }

    public void Start()
    {
      this.m_Index = 0;
      if (!this.Slice())
        return;
      Macros.Running.Add(this);
    }

    public void Stop()
    {
      if (this.m_Index < 0)
        return;
      if (Macros.Running.Contains(this))
        Macros.Running.Remove(this);
      this.m_Index = -1;
    }

    public static void Repeat()
    {
      if (Macro.m_Current == null)
        return;
      Macro.m_Current.m_Index = -1;
    }

    public static bool Delay(int ms)
    {
      if (Macro.m_Current == null)
        return true;
      if (Macro.m_Current.m_DelayEnd == DateTime.MinValue)
      {
        Macro.m_Current.m_DelayEnd = DateTime.Now + TimeSpan.FromMilliseconds((double) ms);
        return false;
      }
      if (!(DateTime.Now >= Macro.m_Current.m_DelayEnd))
        return false;
      Macro.m_Current.m_DelayEnd = DateTime.MinValue;
      return true;
    }

    public bool Slice()
    {
      Macro.m_Current = this;
      if (this.m_Index < this.Actions.Count)
      {
        Action action = this.Actions[this.m_Index];
        ActionHandler handler = action.Handler;
        if (handler == null || !Options.Current.HotkeysEnabled && handler.Name != "Toggle Hotkeys" || handler.Callback(action.Param))
          ++this.m_Index;
      }
      if (this.m_Index >= this.Actions.Count)
        this.m_Index = -1;
      Macro.m_Current = (Macro) null;
      return this.Running;
    }

    public int CompareTo(object obj)
    {
      Macro macro = (Macro) obj;
      if (this.IsWheel && !macro.IsWheel)
        return -1;
      if (!this.IsWheel && macro.IsWheel)
        return 1;
      int num = this.m_Data.Key - macro.m_Data.Key;
      if (num == 0)
        num = this.m_Data.Mods - macro.m_Data.Mods;
      return num;
    }
  }
}
