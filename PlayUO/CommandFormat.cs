// Decompiled with JetBrains decompiler
// Type: PlayUO.CommandFormat
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using System;
using System.Collections;

namespace PlayUO
{
  internal abstract class CommandFormat : SpeechFormat
  {
    private Hashtable m_Commands;

    public CommandFormat(string prepend, string prefix, string format, byte messageType, SpeechType speechType)
      : base(prepend, prefix, format, messageType, speechType)
    {
      this.m_Commands = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
    }

    protected void Register(string name, CommandCallback callback)
    {
      this.Register(name, false, callback);
    }

    protected void Register(string name, bool solitary, CommandCallback callback)
    {
      this.m_Commands[(object) name] = (object) new CommandHandler(name, solitary, callback);
    }

    protected virtual void OnDefault(CommandArgs args)
    {
    }

    public override void Invoke(string text)
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      CommandArgs args = new CommandArgs(player, text);
      CommandHandler commandHandler = this.m_Commands[(object) args.GetString(0)] as CommandHandler;
      if (commandHandler != null)
      {
        ++args.Step;
        if (!commandHandler.Solitary || args.Length == 0)
        {
          commandHandler.Callback(args);
          if (!args.GoDefault)
            return;
        }
        --args.Step;
      }
      this.OnDefault(args);
    }
  }
}
