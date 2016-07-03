// Decompiled with JetBrains decompiler
// Type: PlayUO.PartyFormat
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  internal class PartyFormat : CommandFormat
  {
    public PartyFormat(string prepend, string prefix, string format, byte messageType, SpeechType speechType)
      : base(prepend, prefix, format, messageType, speechType)
    {
      this.Register("accept", true, new CommandCallback(this.Accept_OnCommand));
      this.Register("reject", true, new CommandCallback(this.Reject_OnCommand));
      this.Register("decline", true, new CommandCallback(this.Reject_OnCommand));
      this.Register("add", true, new CommandCallback(this.Add_OnCommand));
      this.Register("rem", true, new CommandCallback(this.Remove_OnCommand));
      this.Register("remove", true, new CommandCallback(this.Remove_OnCommand));
      this.Register("quit", true, new CommandCallback(this.Quit_OnCommand));
      this.Register("loot", false, new CommandCallback(this.Loot_OnCommand));
    }

    protected override void OnDefault(CommandArgs args)
    {
        switch (Party.State)
      {
        case PartyState.Alone:
          Engine.AddTextMessage(string.Format("Note to self: {0}", (object) args.GetArgument(0)), Engine.DefaultFont, Hues.Load(946));
          break;
        case PartyState.Joining:
          Engine.AddTextMessage("Use '/accept' or '/decline'.", Engine.DefaultFont, Hues.Load(946));
          break;
        case PartyState.Joined:
          Mobile mob;
          string text = this.GetText(args.GetArgument(0), out mob);
          if (mob == null)
          {
            Network.Send((Packet) new PParty_PublicMessage(text));
            break;
          }
          Network.Send((Packet) new PParty_PrivateMessage(mob, text));
          string name = args.Mobile.Name;
          string str;
          if (name == null || (str = name.Trim()).Length == 0)
            str = "You";
          Engine.AddTextMessage(string.Format("<{0}> {1}", (object) str, (object) text), Engine.DefaultFont, Hues.Load(Preferences.Current.SpeechHues.Whisper));
          break;
      }
    }

    private void Accept_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Joining)
        Network.Send((Packet) new PParty_Accept(Party.Leader));
      else
        args.GoDefault = true;
    }

    private void Reject_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Joining)
        Network.Send((Packet) new PParty_Decline(Party.Leader));
      else
        args.GoDefault = true;
    }

    private void Add_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Alone)
        Network.Send((Packet) new PParty_AddMember());
      else if (Party.State == PartyState.Joined && Party.IsLeader)
        Network.Send((Packet) new PParty_AddMember());
      else
        args.GoDefault = true;
    }

    private void Remove_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Joined && Party.IsLeader)
        Network.Send((Packet) new PParty_RemoveMember());
      else
        args.GoDefault = true;
    }

    private void Quit_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Joined)
        Network.Send((Packet) new PParty_Quit());
      else
        args.GoDefault = true;
    }

    private void Loot_OnCommand(CommandArgs args)
    {
      if (Party.State == PartyState.Joined)
      {
        if (args.Length > 0)
          Network.Send((Packet) new PParty_SetCanLoot(args.GetBoolean(0)));
        else
          Engine.AddTextMessage("Use '/loot on' or '/loot off'.", Engine.DefaultFont, Hues.Load(946));
      }
      else
        args.GoDefault = true;
    }

    protected string GetText(string text, out Mobile mob)
    {
      if (text.Length > 0 && char.IsDigit(text, 0) && Party.State == PartyState.Joined)
      {
        int index = (int) text[0] - 49;
        if (index >= 0 && index < Party.Members.Length)
        {
          mob = Party.Members[index];
          if (mob != null && !mob.Player)
            return text.Substring(1);
        }
      }
      mob = (Mobile) null;
      return text;
    }

    public override string Mutate(string text, bool display)
    {
      text = base.Mutate(text, false);
      if (display)
      {
        Mobile mob;
        text = this.GetText(text, out mob);
        string str = "Party";
        if (mob != null)
        {
          string name = mob.Name;
          if (name == null || (str = name.Trim()).Length == 0)
            str = "Someone";
        }
        if (!display && this.m_Format != null)
          text = string.Format(this.m_Format, (object) text);
        else if (display)
          text = str + ": " + text + "_";
      }
      return text;
    }
  }
}
