// Decompiled with JetBrains decompiler
// Type: PlayUO.Action
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class Action
  {
    private ActionData m_Data;
    private ActionHandler m_Action;

    public ActionData Data
    {
      get
      {
        return this.m_Data;
      }
    }

    public ActionHandler Handler
    {
      get
      {
        return this.m_Action;
      }
      set
      {
        this.m_Action = value;
      }
    }

    public string Line
    {
      get
      {
        if (this.m_Data.Param != null && this.m_Data.Param.Length > 0)
          return this.m_Data.Command + " " + this.m_Data.Param;
        return this.m_Data.Command;
      }
    }

    public string Command
    {
      get
      {
        return this.m_Data.Command;
      }
      set
      {
        this.m_Data.Command = value;
      }
    }

    public string Param
    {
      get
      {
        return this.m_Data.Param;
      }
      set
      {
        this.m_Data.Param = value;
      }
    }

    public Action(string command, string param)
      : this(new ActionData(command, param))
    {
    }

    public Action(ActionData data)
    {
      this.m_Data = data;
      this.m_Action = ActionHandler.Find(this.m_Data.Command);
    }
  }
}
