// Decompiled with JetBrains decompiler
// Type: PlayUO.MacroData
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System.Windows.Forms;

namespace PlayUO
{
  public class MacroData : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("macro", Construct, ActionData.TypeCode);
    public const Keys WheelUp = (Keys) 69632;
    public const Keys WheelDown = (Keys) 69633;
    public const Keys WheelPress = (Keys) 69634;
    private Keys key;
    private Keys mods;
    private ActionCollection actions;

    public override PersistableType TypeID
    {
      get
      {
        return MacroData.TypeCode;
      }
    }

    public Keys Key
    {
      get
      {
        return this.key;
      }
      set
      {
        this.key = value;
      }
    }

    public Keys Mods
    {
      get
      {
        return this.mods;
      }
      set
      {
        this.mods = value;
      }
    }

    public ActionCollection Actions
    {
      get
      {
        return this.actions;
      }
    }

    public bool IsWheel
    {
      get
      {
        if (this.key != (Keys) 69632 && this.key != (Keys) 69633)
          return this.key == (Keys) 69634;
        return true;
      }
    }

    public bool Control
    {
      get
      {
        return this.GetMod(Keys.Control);
      }
      set
      {
        this.SetMod(Keys.Control, value);
      }
    }

    public bool Alt
    {
      get
      {
        return this.GetMod(Keys.Alt);
      }
      set
      {
        this.SetMod(Keys.Alt, value);
      }
    }

    public bool Shift
    {
      get
      {
        return this.GetMod(Keys.Shift);
      }
      set
      {
        this.SetMod(Keys.Shift, value);
      }
    }

    public MacroData()
    {
      this.actions = new ActionCollection();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new MacroData();
    }

    private bool GetMod(Keys key)
    {
      return (this.mods & key) != Keys.None;
    }

    private void SetMod(Keys key, bool value)
    {
      if (value)
        this.mods |= key;
      else
        this.mods &= ~key;
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      int num = 0;
      if (this.Control)
        num |= 1;
      if (this.Alt)
        num |= 2;
      if (this.Shift)
        num |= 4;
      op.SetInt32("key", (int) this.key);
      op.SetInt32("modBits", num);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.key = (Keys) ip.GetInt32("key");
      int int32 = ip.GetInt32("modBits");
      this.Control = (int32 & 1) != 0;
      this.Alt = (int32 & 2) != 0;
      this.Shift = (int32 & 4) != 0;
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.actions.Count; ++index)
        this.actions[index].Data.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.HasChild
        this.actions.Add(new Action(ip.GetChild() as ActionData));
    }
  }
}
