// Decompiled with JetBrains decompiler
// Type: PlayUO.ClientFormat
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System;
using System.Collections;
using Ultima.Client;

namespace PlayUO
{
  internal class ClientFormat : CommandFormat
  {
    private Hashtable m_GiveEntries;

    public ClientFormat(string prepend, string prefix, string format, byte messageType, SpeechType speechType)
      : base(prepend, prefix, format, messageType, speechType)
    {
      this.Register("Disturb", new CommandCallback(this.Disturb_OnCommand));
      this.Register("LeaveHouse", new CommandCallback(this.LeaveHouse_OnCommand));
      this.Register("OpenRunebooks", new CommandCallback(this.OpenRunebooks));
      this.Register("Dress", (CommandCallback) (args => Engine.Dress()));
      this.Register("Recall", new CommandCallback(this.Recall_OnCommand));
      this.Register("Gate", new CommandCallback(this.Gate_OnCommand));
      this.Register("Target", new CommandCallback(this.Target_OnCommand));
      this.Register("Acquire", new CommandCallback(this.Acquire_OnCommand));
      this.Register("AlwaysRun", new CommandCallback(this.AlwaysRun_OnCommand));
      this.Register("SmoothWalk", new CommandCallback(this.SmoothWalk_OnCommand));
      this.Register("Footsteps", new CommandCallback(this.Footsteps_OnCommand));
      this.Register("Sound", new CommandCallback(this.Sound_OnCommand));
      this.Register("Music", new CommandCallback(this.Music_OnCommand));
      this.Register("QueueTargets", new CommandCallback(this.QueueTargets_OnCommand));
      this.Register("Remove", new CommandCallback(this.Remove_OnCommand));
      this.Register("UseGate", new CommandCallback(this.UseGate_OnCommand));
      this.Register("Friend", new CommandCallback(this.Friend_OnCommand));
      this.Register("Ignore", new CommandCallback(this.Ignore_OnCommand));
      this.Register("Scavenge", new CommandCallback(this.Scavenge_OnCommand));
      this.Register("RegDrop", new CommandCallback(this.RegDrop_OnCommand));
      this.Register("PotDrop", new CommandCallback(this.PotDrop_OnCommand));
      this.Register("DragToBag", new CommandCallback(this.DragToBag_OnCommand));
      this.Register("Move", new CommandCallback(this.Move_OnCommand));
      this.Register("Stack", new CommandCallback(this.Stack_OnCommand));
      this.Register("BringTo", new CommandCallback(this.BringTo_OnCommand));
      this.Register("TurnTo", new CommandCallback(this.TurnTo_OnCommand));
      this.Register("Disarm", new CommandCallback(this.Disarm_OnCommand));
      this.Register("Stun", new CommandCallback(this.Stun_OnCommand));
      this.Register("Noto", new CommandCallback(this.NotoQuery_OnCommand));
      this.Register("NotoQuery", new CommandCallback(this.NotoQuery_OnCommand));
      this.Register("HouseLevel", new CommandCallback(this.HouseLevel_OnCommand));
      this.Register("AutoPickup", new CommandCallback(this.AutoPickup_OnCommand));
      this.Register("Speed", new CommandCallback(this.Speed_OnCommand));
      this.Register("RunSpeed", new CommandCallback(this.RunSpeed_OnCommand));
      this.Register("WalkSpeed", new CommandCallback(this.WalkSpeed_OnCommand));
      this.Register("DropTarg", new CommandCallback(this.DropTarg_OnCommand));
      this.Register("CancelStealth", new CommandCallback(this.CancelStealth_OnCommand));
      this.Register("Give", new CommandCallback(this.Give_OnCommand));
      this.Register("FPS", new CommandCallback(this.FPS_OnCommand));
      this.Register("Restock", (CommandCallback) (args => Player.Current.RestockAgent.Invoke()));
      this.Register("Lootbag", (CommandCallback) (args =>
      {
        TargetManager.Client = (ClientTargetHandler) new SetRestockTargetTargetHandler(false);
        Engine.AddTextMessage("Target your lootbag.");
      }));
      this.Register("ClearMoves", (CommandCallback) (args =>
      {
        if (ActionContext.Queued.Count > 0)
        {
          ActionContext.Clear();
          Engine.AddTextMessage("Action queue cleared.");
        }
        else
          Engine.AddTextMessage("Action queue is already empty.");
      }));
      this.Register("Organize", (CommandCallback) (args => Player.Current.OrganizeAgent.Invoke()));
      this.Register("SetOrganize", (CommandCallback) (args =>
      {
        TargetManager.Client = (ClientTargetHandler) new SetOrganizeTemplateTargetHandler(false);
        Engine.AddTextMessage("Target your template lootbag.");
      }));
      this.Register("LeapFrog", (CommandCallback) (args =>
      {
        Gump drag = Gumps.Drag;
        if (drag is GDraggedItem)
        {
          Engine.m_LeapFrog = ((GDraggedItem) drag).Item;
          Engine.AddTextMessage("Leapfrog item set.");
        }
        else
        {
          Engine.m_LeapFrog = (Item) null;
          Engine.AddTextMessage("You are not holding an item. Leapfrog item cleared.");
        }
      }));
    }

    private void Recall_OnCommand(CommandArgs args)
    {
      string b = args.GetArgument(0);
      foreach (RunebookInfo runebook in Player.Current.TravelAgent.Runebooks)
      {
        if (runebook.IsValid)
        {
          foreach (RuneInfo rune in runebook.Runes)
          {
            if (string.Equals(rune.Name, b, StringComparison.CurrentCultureIgnoreCase))
            {
              new TravelContext(runebook, rune, true).Enqueue();
              return;
            }
          }
        }
      }
      Engine.AddTextMessage("No rune with that name was found.");
    }

    private void Gate_OnCommand(CommandArgs args)
    {
      string b = args.GetArgument(0);
      foreach (RunebookInfo runebook in Player.Current.TravelAgent.Runebooks)
      {
        if (runebook.IsValid)
        {
          foreach (RuneInfo rune in runebook.Runes)
          {
            if (string.Equals(rune.Name, b, StringComparison.CurrentCultureIgnoreCase))
            {
              new TravelContext(runebook, rune, false).Enqueue();
              return;
            }
          }
        }
      }
      Engine.AddTextMessage("No rune with that name was found.");
    }

    private void LeaveHouse_OnCommand(CommandArgs args)
    {
      new ClientFormat.LeaveHouseAction().Dispatch();
    }

    private void OpenRunebooks(CommandArgs args)
    {
      foreach (Item obj in World.Items.Values)
      {
        Item item = obj;
        if (item.ID == 8901 || item.ID == 3643 || item.ID == 3834)
          new GenericContext((System.Action) (() => Network.Send((Packet) new PPE_QueryRunebookContent(item)))).Enqueue();
      }
    }

    private void Target_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new AcquireTargetHandler();
    }

    private void Acquire_OnCommand(CommandArgs args)
    {
      TargetManager.Reacquire();
    }

    private void AutoPickup_OnCommand(CommandArgs args)
    {
      bool flag = !Options.Current.Scavenger;
      if (args.Length > 0)
        flag = args.GetBoolean(0);
      Options.Current.Scavenger = flag;
    }

    private void AlwaysRun_OnCommand(CommandArgs args)
    {
      bool flag = !Options.Current.AlwaysRun;
      if (args.Length > 0)
        flag = args.GetBoolean(0);
      Options.Current.AlwaysRun = flag;
    }

    private void SmoothWalk_OnCommand(CommandArgs args)
    {
      bool flag = !Options.Current.SmoothWalk;
      if (args.Length > 0)
        flag = args.GetBoolean(0);
      Options.Current.SmoothWalk = flag;
    }

    private void QueueTargets_OnCommand(CommandArgs args)
    {
      bool flag = !Options.Current.QueueTargets;
      if (args.Length > 0)
        flag = args.GetBoolean(0);
      Options.Current.QueueTargets = flag;
    }

    private void Footsteps_OnCommand(CommandArgs args)
    {
      bool flag = !Preferences.Current.Footsteps.Volume.Mute;
      if (args.Length > 0)
        flag = !args.GetBoolean(0);
      Preferences.Current.Footsteps.Volume.Mute = flag;
      Engine.AddTextMessage(string.Format("Footsteps are {0}", flag ? (object) "muted" : (object) "unmuted"));
    }

    private void Sound_OnCommand(CommandArgs args)
    {
      bool flag = !Preferences.Current.Sound.Volume.Mute;
      if (args.Length > 0)
        flag = !args.GetBoolean(0);
      Preferences.Current.Sound.Volume.Mute = flag;
      Engine.AddTextMessage(string.Format("Sound is {0}", flag ? (object) "muted" : (object) "unmuted"));
    }

    private void Music_OnCommand(CommandArgs args)
    {
      string a = args.Length > 0 ? args.GetString(0) : (string) null;
      if (string.Equals(a, "stop", StringComparison.OrdinalIgnoreCase))
      {
        Music.Stop();
      }
      else
      {
        bool? nullable = new bool?();
        if (string.Equals(a, "on", StringComparison.OrdinalIgnoreCase))
          nullable = new bool?(false);
        else if (string.Equals(a, "off", StringComparison.OrdinalIgnoreCase))
          nullable = new bool?(true);
        else if (a == null || string.Equals(a, "toggle", StringComparison.OrdinalIgnoreCase))
          nullable = new bool?(!Preferences.Current.Music.Volume.Mute);
        if (nullable.HasValue)
        {
          Preferences.Current.Music.Volume.Mute = nullable.Value;
          if (nullable.Value)
          {
            Engine.AddTextMessage("Music disabled.");
            Music.Stop();
          }
          else
            Engine.AddTextMessage("Music enabled.");
        }
        else
          Engine.AddTextMessage("Use 'on', 'off', 'toggle', or 'stop'.");
      }
    }

    private void UseGate_OnCommand(CommandArgs args)
    {
      Engine.UseMoongate();
    }

    private void DragToBag_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new DragToBagTargetHandler(false);
      Engine.AddTextMessage("Target an item to move.");
    }

    private void Move_OnCommand(CommandArgs args)
    {
      TargetManager.Client = args.Length != 0 ? (ClientTargetHandler) new MoveTargetHandler(new int?(args.GetInt32(0))) : (ClientTargetHandler) new MoveTargetHandler(new int?());
      Engine.AddTextMessage("Target one of the items to move.");
    }

    private void Stack_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new StackTargetHandler();
      Engine.AddTextMessage("Target the destination item.");
    }

    private void BringTo_OnCommand(CommandArgs args)
    {
      if (args.Length == 0)
      {
        TargetManager.Client = (ClientTargetHandler) new BringToTargetHandler(0, 0);
        Engine.AddTextMessage("Target the destination item.");
      }
      else if (args.Length == 2)
      {
        TargetManager.Client = (ClientTargetHandler) new BringToTargetHandler(args.GetInt32(0), args.GetInt32(1));
        Engine.AddTextMessage("Target the destination item.");
      }
      else
        Engine.AddTextMessage("Use 'bringto' or 'bringto x y'.");
    }

    private void RegDrop_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new RegDropTargetHandler();
      Engine.AddTextMessage("Target the destination container.");
    }

    private void PotDrop_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new PotDropTargetHandler();
      Engine.AddTextMessage("Target the destination container.");
    }

    private void Disturb_OnCommand(CommandArgs args)
    {
      Engine.Disturb();
    }

    private void Friend_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new FriendTargetHandler();
      Engine.AddTextMessage("Target a player to toggle their friendship status.", Engine.DefaultFont, Hues.Load(89));
    }

    private void Ignore_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new IgnoreTargetHandler();
      Engine.AddTextMessage("Target a player to toggle their ignored status.", Engine.DefaultFont, Hues.Load(89));
    }

    private void TurnTo_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new TurnTargetHandler();
      Engine.AddTextMessage("Turn to where?");
    }

    private void Disarm_OnCommand(CommandArgs args)
    {
      Network.Send((Packet) new PWrestleDisarm());
    }

    private void Stun_OnCommand(CommandArgs args)
    {
      Network.Send((Packet) new PWrestleStun());
    }

    private void Remove_OnCommand(CommandArgs args)
    {
      TargetManager.Client = (ClientTargetHandler) new RemoveTargetHandler();
      Engine.AddTextMessage("Remove what?");
    }

    private void Scavenge_OnCommand(CommandArgs args)
    {
      if (args.Length > 0)
      {
        switch (args.GetString(0).ToLower())
        {
          case "all":
            TargetManager.Client = (ClientTargetHandler) new ScavengerTargetHandler(true, true);
            break;
          case "only":
            TargetManager.Client = (ClientTargetHandler) new ScavengerTargetHandler(true, false);
            break;
          case "remove":
            TargetManager.Client = (ClientTargetHandler) new ScavengerTargetHandler(false, true);
            break;
          default:
            Engine.AddTextMessage("Use 'all', 'only', or 'remove'.");
            break;
        }
      }
      else
        Preferences.Current.Scavenger.Scavenge(true);
    }

    private void NotoQuery_OnCommand(CommandArgs args)
    {
      int num = (int) (1 + Options.Current.NotorietyQuery) % 3;
      if (args.Length > 0 && !(args.GetString(0).ToLower() == "smart"))
        args.GetBoolean(0);
      Preferences.Current.Scavenger.Scavenge(true);
    }

    private void HouseLevel_OnCommand(CommandArgs args)
    {
      if (args.Length != 1)
        return;
      int houseLevel = Options.Current.HouseLevel;
      int num;
      switch (args.GetString(0).ToLower())
      {
        case "up":
          num = houseLevel + 1;
          break;
        case "down":
          num = houseLevel - 1;
          break;
        case "on":
          num = 1;
          break;
        case "off":
          num = 5;
          break;
        default:
          num = args.GetInt32(0);
          break;
      }
      Options.Current.HouseLevel = num;
    }

    private void Speed_OnCommand(CommandArgs args)
    {
      if (!Engine.GMPrivs)
      {
        Engine.AddTextMessage("You do not have access to this command.");
      }
      else
      {
        float num1 = 0.4f;
        if (args.Length > 0)
        {
          int num2 = args.GetInt32(0) - 1;
          if (num2 < 0)
            num2 = 0;
          num1 -= (float) num2 * 0.1f;
          if ((double) num1 < 0.0)
            num1 = 0.0f;
        }
        Walking.Speed = num1;
        Engine.AddTextMessage(string.Format("Speed set to {0:N0} tiles/sec.", (object) (float) ((double) World.Player.Speed / (double) Walking.RunSpeed)));
      }
    }

    private void RunSpeed_OnCommand(CommandArgs args)
    {
      if (!Engine.GMPrivs)
      {
        Engine.AddTextMessage("You do not have access to this command.");
      }
      else
      {
        int num = 200;
        if (args.Length > 0)
          num = args.GetInt32(0);
        Walking.RunSpeed = (float) num * (1f / 1000f);
        Engine.AddTextMessage(string.Format("Running speed set to {0:N0} tiles/sec.", (object) (float) ((double) World.Player.Speed / (double) Walking.RunSpeed)));
      }
    }

    private void WalkSpeed_OnCommand(CommandArgs args)
    {
      if (!Engine.GMPrivs)
      {
        Engine.AddTextMessage("You do not have access to this command.");
      }
      else
      {
        int num = 400;
        if (args.Length > 0)
          num = args.GetInt32(0);
        Walking.WalkSpeed = (float) num * (1f / 1000f);
        Engine.AddTextMessage(string.Format("Walking speed set to {0:N0} tiles/sec.", (object) (float) ((double) World.Player.Speed / (double) Walking.WalkSpeed)));
      }
    }

    private void DropTarg_OnCommand(CommandArgs args)
    {
      if (!TargetManager.IsActive)
        return;
      TargetManager.Active.Cancel();
    }

    private void CancelStealth_OnCommand(CommandArgs args)
    {
      if (Engine.m_Stealth)
      {
        Engine.m_Stealth = false;
        Engine.m_StealthSteps = 0;
        Engine.AddTextMessage("You have deactivated stealth.");
      }
      else
        Engine.AddTextMessage("Stealth has not yet been activated.");
    }

    private void RegisterGive(GiveEntry entry)
    {
      this.m_GiveEntries[(object) entry.Name] = (object) entry;
    }

    private void Give_OnCommand(CommandArgs args)
    {
      if (this.m_GiveEntries == null)
      {
        this.m_GiveEntries = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
        this.RegisterGive((GiveEntry) new GiveFixedEntry("Recalls", 2, new IItemValidator[3]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3974 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3962 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3963 })
        }));
        this.RegisterGive((GiveEntry) new GiveFixedEntry("Gates", 2, new IItemValidator[3]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3974 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3962 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3980 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Regs", 25, new IItemValidator[8]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3962 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3963 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3972 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3973 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3974 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3976 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3980 }),
          (IItemValidator) new ItemIDValidator(new int[1]{ 3981 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Drake", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3974 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Pearl", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3962 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Moss", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3963 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Garlic", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3972 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Shade", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3976 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Ginseng", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3973 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Silk", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3981 })
        }));
        this.RegisterGive((GiveEntry) new GiveRatioEntry("Ash", 25, new IItemValidator[1]
        {
          (IItemValidator) new ItemIDValidator(new int[1]{ 3980 })
        }));
      }
      if (args.Length > 0)
      {
        GiveEntry entry = this.m_GiveEntries[(object) args.GetString(0)] as GiveEntry;
        if (entry != null)
        {
          int desiredAmount = -1;
          if (args.Length > 1)
            desiredAmount = args.GetInt32(1);
          if (desiredAmount > 0 || desiredAmount == -1)
          {
            TargetManager.Client = (ClientTargetHandler) new GiveTargetHandler(entry, desiredAmount);
            Engine.AddTextMessage(string.Format("Who do you wish to give {0} to?", entry.Validators.Length == 1 ? (object) "this item" : (object) "these items"));
          }
          else
            Engine.AddTextMessage("You have specified an invalid amount.");
        }
        else
          Engine.AddTextMessage("You have specified an unknown item name.");
      }
      else
        Engine.AddTextMessage("You must specify an item name.");
    }

    protected override void OnDefault(CommandArgs args)
    {
      Engine.AddTextMessage("You have entered an unknown command.");
    }

    public void FPS_OnCommand(CommandArgs args)
    {
      int num = 100;
      if (args.Length > 0)
        num = args.GetInt32(0);
      Timer timer = new Timer(new OnTick(Engine.TimeRefresh_OnTick), 1, 1);
      timer.SetTag("Frames", (object) num);
      timer.Start(false);
    }

    private class LeaveHouseAction : ActionContext
    {
      protected override bool CheckDispatch()
      {
        foreach (ActionContext actionContext in ActionContext.Pending)
        {
          if (actionContext is ClientFormat.LeaveHouseAction)
            return false;
        }
        return base.CheckDispatch();
      }

      public override void OnDispatch()
      {
        Mobile player = World.Player;
        if (player != null)
        {
          Network.Send((Packet) new PPopupRequest(player));
          player.AddTextMessage(player.Name, "- leaving -", Engine.DefaultFont, Hues.Load(38), true);
        }
        base.OnDispatch();
      }

      public override bool OnContextItem(object owner, int entryID, int stringID)
      {
        return stringID == 6207;
      }

      public override bool OnContextEnd(object owner, bool selected)
      {
        return false;
      }
    }
  }
}
