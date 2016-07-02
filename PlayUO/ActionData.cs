// Decompiled with JetBrains decompiler
// Type: PlayUO.ActionData
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO
{
  public class ActionData : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("action", new ConstructCallback((object) null, __methodptr(Construct)));
    private string command;
    private string param;

    public virtual PersistableType TypeID
    {
      get
      {
        return ActionData.TypeCode;
      }
    }

    public string Command
    {
      get
      {
        return this.command;
      }
      set
      {
        this.command = value;
      }
    }

    public string Param
    {
      get
      {
        return this.param;
      }
      set
      {
        this.param = value;
      }
    }

    public ActionData()
    {
      base.\u002Ector();
    }

    public ActionData(string command, string param)
    {
      base.\u002Ector();
      this.command = command;
      this.param = param;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ActionData();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetString("command", this.command);
      if (this.param == null || this.param.Length <= 0)
        return;
      op.SetString("param", this.param);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.command = ip.GetString("command");
      this.param = ip.GetString("param");
      if (this.param != null)
        return;
      this.param = string.Empty;
    }
  }
}
