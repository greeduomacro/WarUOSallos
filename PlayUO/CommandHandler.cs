// Decompiled with JetBrains decompiler
// Type: PlayUO.CommandHandler
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public sealed class CommandHandler
  {
    private string m_Name;
    private bool m_Solitary;
    private CommandCallback m_Callback;

    public string Name
    {
      get
      {
        return this.m_Name;
      }
    }

    public bool Solitary
    {
      get
      {
        return this.m_Solitary;
      }
    }

    public CommandCallback Callback
    {
      get
      {
        return this.m_Callback;
      }
    }

    public CommandHandler(string name, bool solitary, CommandCallback callback)
    {
      this.m_Name = name;
      this.m_Solitary = solitary;
      this.m_Callback = callback;
    }
  }
}
