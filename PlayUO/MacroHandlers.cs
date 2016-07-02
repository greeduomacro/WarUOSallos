// Decompiled with JetBrains decompiler
// Type: PlayUO.MacroHandlers
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System;
using System.Collections.Generic;
using Ultima.Client;

namespace PlayUO
{
  public class MacroHandlers
  {
    private static Dictionary<string, WandEffect> _wandEffects;

    public static void Register(ActionCallback callback, string action)
    {
      MacroHandlers.Register(callback, action, (ParamNode[]) null);
    }

    public static void Register(ActionCallback callback, string action, params string[] list)
    {
      ParamNode[] paramNodeArray = new ParamNode[list.Length];
      for (int index = 0; index < paramNodeArray.Length; ++index)
        paramNodeArray[index] = new ParamNode(list[index], list[index]);
      MacroHandlers.Register(callback, action, paramNodeArray);
    }

    public static void Register(ActionCallback callback, string action, string[,] list)
    {
      ParamNode[] paramNodeArray = new ParamNode[list.GetLength(0)];
      for (int index = 0; index < paramNodeArray.Length; ++index)
        paramNodeArray[index] = new ParamNode(list[index, 0], list[index, 1]);
      MacroHandlers.Register(callback, action, paramNodeArray);
    }

    private static bool Toggle(bool old, string val)
    {
      if (val != null && val.Length > 0)
      {
        switch (val.ToLower())
        {
          case "yes":
          case "on":
          case "1":
            return true;
          case "off":
          case "no":
          case "0":
            return false;
        }
      }
      return !old;
    }

    public static void Register(ActionCallback callback, string action, params ParamNode[] options)
    {
      ActionHandler.Register(action, options, callback);
    }

    public static void Setup()
    {
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Screenshots_OnAction), "Options|Screenshots@Death Screenshots", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.RegCounter_OnAction), "Interface|RegCounter@Reg Counter", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Warmode_OnAction), "Actions|Warmode", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Halos_OnAction), "Options|Halos@Halos", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.ParticleCount_OnAction), "Options|ParticleCount@Particle Counter", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Temperature_OnAction), "Options|Temperature@Temperature", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Ping_OnAction), "Options|Ping@Ping Display", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Transparency_OnAction), "Options|Transparency", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.ContainerGrid_OnAction), "Options|ContainerGrid@Container Grid", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Grid_OnAction), "Options|Grid@Terrain Grid", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.PumpFPS_OnAction), "Options|PumpFPS@Pump FPS", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.FPS_OnAction), "Options|FPS@Display FPS", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.MiniHealth_OnAction), "Options|MiniHealth@Mini Health", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.ToggleHotkeys_OnAction), "Options|ToggleHotkeys@Toggle Hotkeys", ParamNode.Toggle);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Cast_OnAction), "Actions|Cast@Cast Spell", new ParamNode("Mage", new ParamNode[8]
      {
        new ParamNode("First", new string[8]
        {
          "Clumsy",
          "Create Food",
          "Feeblemind",
          "Heal",
          "Magic Arrow",
          "Night Sight",
          "Reactive Armor",
          "Weaken"
        }),
        new ParamNode("Second", new string[8]
        {
          "Agility",
          "Cunning",
          "Cure",
          "Harm",
          "Magic Trap",
          "Magic Untrap",
          "Protection",
          "Strength"
        }),
        new ParamNode("Third", new string[8]
        {
          "Bless",
          "Fireball",
          "Magic Lock",
          "Poison",
          "Telekinesis",
          "Teleport",
          "Unlock",
          "Wall of Stone"
        }),
        new ParamNode("Fourth", new string[8]
        {
          "Arch Cure",
          "Arch Protection",
          "Curse",
          "Fire Field",
          "Greater Heal",
          "Lightning",
          "Mana Drain",
          "Recall"
        }),
        new ParamNode("Fifth", new string[8]
        {
          "Blade Spirits",
          "Dispel Field",
          "Incognito",
          "Magic Reflection",
          "Mind Blast",
          "Paralyze",
          "Poison Field",
          "Summ. Creature"
        }),
        new ParamNode("Sixth", new string[8]
        {
          "Dispel",
          "Energy Bolt",
          "Explosion",
          "Invisibility",
          "Mark",
          "Mass Curse",
          "Paralyze Field",
          "Reveal"
        }),
        new ParamNode("Seventh", new string[8]
        {
          "Chain Lightning",
          "Energy Field",
          "Flame Strike",
          "Gate Travel",
          "Mana Vampire",
          "Mass Dispel",
          "Meteor Swarm",
          "Polymorph"
        }),
        new ParamNode("Eighth", new string[8]
        {
          "Earthquake",
          "Energy Vortex",
          "Resurrection",
          "Air Elemental",
          "Summon Daemon",
          "Earth Elemental",
          "Fire Elemental",
          "Water Elemental"
        })
      }), new ParamNode("Paladin", new string[10]
      {
        "Cleanse by Fire",
        "Close Wounds",
        "Consecrate Weapon",
        "Dispel Evil",
        "Divine Fury",
        "Enemy of One",
        "Holy Light",
        "Noble Sacrifice",
        "Remove Curse",
        "Sacred Journey"
      }), new ParamNode("Necromancer", new ParamNode[4]
      {
        new ParamNode("Curses", new string[5]
        {
          "Blood Oath",
          "Corpse Skin",
          "Curse Weapon",
          "Evil Omen",
          "Mind Rot"
        }),
        new ParamNode("Damaging", new string[4]
        {
          "Pain Spike",
          "Poison Strike",
          "Strangle",
          "Wither"
        }),
        new ParamNode("Transorming", new string[4]
        {
          "Horrific Beast",
          "Lich Form",
          "Vampiric Embrace",
          "Wraith Form"
        }),
        new ParamNode("Summoning", new string[3]
        {
          "Animate Dead",
          "Summon Familiar",
          "Vengeful Spirit"
        })
      }));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UsePotion_OnAction), "Items|UsePotion@Use Potion", new string[9, 2]
      {
        {
          "Smart",
          "Smart"
        },
        {
          "Cure",
          "Orange"
        },
        {
          "Heal",
          "Yellow"
        },
        {
          "Poison",
          "Green"
        },
        {
          "Agility",
          "Blue"
        },
        {
          "Refresh",
          "Red"
        },
        {
          "Strength",
          "White"
        },
        {
          "Explosion",
          "Purple"
        },
        {
          "Night Sight",
          "Black"
        }
      });
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Open_OnAction), "Interface|Open", new ParamNode("Spellbook", new ParamNode[3]
      {
        new ParamNode("Mage", "Spellbook"),
        new ParamNode("Paladin", "PaladinSpellbook"),
        new ParamNode("Necromancer", "NecroSpellbook")
      }), new ParamNode("Combat Book", "Abilities"), new ParamNode("Backpack", "Backpack"), new ParamNode("Help", "Help"), new ParamNode("Journal", "Journal"), new ParamNode("Network Stats", "NetStats"), new ParamNode("Configuration", "Options"), new ParamNode("Paperdoll", "Paperdoll"), new ParamNode("Overview", "Radar"), new ParamNode("Skills", "Skills"), new ParamNode("Status", "Status"), new ParamNode("Macro Editor", "Macros"), new ParamNode("Info Browser", "InfoBrowser"));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UseSkill_OnAction), "Actions|Use@Use Skill", new ParamNode("Actions", new string[11]
      {
        "Animal Taming",
        "Begging",
        "Detecting Hidden",
        "Hiding",
        "Meditation",
        "Poisoning",
        "Remove Trap",
        "Spirit Speak",
        "Stealing",
        "Stealth",
        "Tracking"
      }), new ParamNode("Lore & Knowledge", new string[7]
      {
        "Anatomy",
        "Animal Lore",
        "Arms Lore",
        "Evaluating Intelligence",
        "Forensic Evaluation",
        "Taste Identification",
        "Item Identification"
      }), new ParamNode("Crafting", new string[2]
      {
        "Inscription",
        "Cartography"
      }), new ParamNode("Bardic", new string[3]
      {
        "Discordance",
        "Peacemaking",
        "Provocation"
      }));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Target_OnAction), "Actions|Target", new ParamNode("Self", "Self"), new ParamNode("Last", "Last"), new ParamNode("Acquire", "Acquire"), new ParamNode("Find", "Find"));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Clear_OnAction), "Other|Clear", new ParamNode("Screen", "Screen"), new ParamNode("Target Queue", "TargetQueue"), new ParamNode("Target Cursor", "Target"));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.DelayMacro_OnAction), "Other|DelayMacro@Delay Macro");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Repeat_OnAction), "Other|Repeat", "Speech", "Macro");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UseOnce_OnAction), "Items|UseOnce@Use Once", "Use", "Set");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UseItemByType_OnAction), "Items|UseItemByType@Use By Type", "Bola", "Bandage", "Dagger", "Candle", "Moongate", "Purple Petal", "Orange Petal");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UseItemInHand_OnAction), "Items|UseItemInHand@Use In Hand");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.UseWand_OnAction), "Items|UseWand@Use Wand", "Identification", "Clumsy", "Feeblemind", "Harm", "Magic Arrow", "Weaken", "Fireball", "Heal", "Greater Heal", "Lightning", "Mana Drain");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.SetEquip_OnAction), "Equipment|SetEquip@Set Equipment", ParamNode.Count(0, 10, "Slot {0}"));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Equip_OnAction), "Equipment|Arm", ParamNode.Count(0, 10, "Slot {0}"));
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Dequip_OnAction), "Equipment|Disarm", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Dress_OnAction), "Equipment|Dress", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Bow_OnAction), "Other|Animations|Bow", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Salute_OnAction), "Other|Animations|Salute", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Wrestle_OnAction), "Other|Wrestle@Wrestle Move", "Disarm", "Stun");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Resync_OnAction), "Other|Resync@Resynchronize", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.BandageSelf_OnAction), "Actions|BandageSelf@Bandage Self", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Paste_OnAction), "Interface|Paste");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Dismount_OnAction), "Actions|Dismount", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Remount_OnAction), "Actions|Remount", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.StopMacros_OnAction), "Other|StopMacros@Stop Macros", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.WaitForTargetLast_OnAction), "Other|WaitForTargetLast", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.WaitForTarget_OnAction), "Other|WaitForTarget", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Quit_OnAction), "Interface|Quit", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.AllNames_OnAction), "Interface|AllNames@All Names", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.SetAbility_OnAction), "Other|Ability@Set Ability", "Primary", "Secondary", "None");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Count_OnAction), "Interface|Count", "Regs", "Ammo");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Attack_OnAction), "Other|Attack", "Last", "Red");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Last_OnAction), "Actions|Last", "Object", "Skill", "Spell");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Disrupt_OnAction), "Actions|Disrupt", ParamNode.Empty);
      MacroHandlers.Register(new ActionCallback(MacroHandlers.Say_OnAction), "Interface|Say");
      MacroHandlers.Register(new ActionCallback(MacroHandlers.RestoreSpeech_OnAction), "Interface|Restore@Restore Speech", ParamNode.Empty);
    }

    private static bool RestoreSpeech_OnAction(string args)
    {
      Engine.RestoreSpeech();
      return true;
    }

    private static bool Clear_OnAction(string args)
    {
      string[] strArray = args.ToLower().Split(' ');
      if (strArray[0] == "screen")
        Engine.ClearScreen();
      else if (strArray[0] == "targetqueue")
        TargetManager.ClearQueue();
      else if (strArray[0] == "target" && TargetManager.IsActive)
        TargetManager.Active.Cancel();
      return true;
    }

    private static bool Disrupt_OnAction(string args)
    {
      Engine.Disturb();
      return true;
    }

    private static bool Target_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLowerInvariant())
        {
          case "self":
            TargetManager.TargetSelf();
            break;
          case "smart":
          case "last":
            TargetManager.TargetSmart();
            break;
          case "aquire":
          case "acquire":
            TargetManager.TargetAcquire();
            break;
          case "find":
            TargetManager.Reacquire();
            break;
        }
      }
      return true;
    }

    private static bool Say_OnAction(string args)
    {
      Engine.m_SayMacro = true;
      Engine.commandEntered(Engine.Encode(args));
      Engine.m_SayMacro = false;
      return true;
    }

    public static bool UseWand_OnAction(string args)
    {
      if (MacroHandlers._wandEffects == null)
        MacroHandlers._wandEffects = MacroHandlers.CreateWandEffectTable();
      WandEffect effect;
      if (MacroHandlers._wandEffects.TryGetValue(args, out effect))
      {
        Mobile player = World.Player;
        if (player != null)
        {
          Item pickUp = WandRepository.Find(effect);
          if (pickUp != null)
          {
            if (pickUp.Parent == player)
            {
              pickUp.Use();
            }
            else
            {
              Item equip1 = player.FindEquip(Layer.OneHanded);
              if (equip1 != null)
              {
                Item backpack = player.Backpack;
                if (backpack != null)
                  new MoveContext(equip1, equip1.Amount, (IEntity) backpack, false).Enqueue();
              }
              Item equip2 = player.FindEquip(Layer.TwoHanded);
              if (equip2 != null)
              {
                Item backpack = player.Backpack;
                if (backpack != null)
                  new MoveContext(equip2, equip2.Amount, (IEntity) backpack, false).Enqueue();
              }
              new EquipContext(pickUp, pickUp.Amount, player, false).Enqueue();
              new UseContext((IEntity) pickUp, false).Enqueue();
            }
          }
          else
            Engine.AddTextMessage("Wand not found.", Engine.DefaultFont, Hues.Load(38));
        }
      }
      return true;
    }

    private static Dictionary<string, WandEffect> CreateWandEffectTable()
    {
      Dictionary<string, WandEffect> dictionary = new Dictionary<string, WandEffect>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      dictionary["Identification"] = WandEffect.Identification;
      dictionary["Clumsy"] = WandEffect.Clumsiness;
      dictionary["Feeblemind"] = WandEffect.Feeblemindedness;
      dictionary["Harm"] = WandEffect.Harming;
      dictionary["Magic Arrow"] = WandEffect.MagicArrow;
      dictionary["Weaken"] = WandEffect.Weakness;
      dictionary["Fireball"] = WandEffect.Fireball;
      dictionary["Heal"] = WandEffect.Healing;
      dictionary["Greater Heal"] = WandEffect.GreaterHealing;
      dictionary["Lightning"] = WandEffect.Lightning;
      dictionary["Mana Drain"] = WandEffect.ManaDraining;
      return dictionary;
    }

    public static bool UseItemInHand_OnAction(string args)
    {
      Mobile player = World.Player;
      if (player != null)
      {
        Item obj = player.FindEquip(Layer.OneHanded) ?? player.FindEquip(Layer.TwoHanded);
        if (obj != null)
          obj.Use();
      }
      return true;
    }

    private static bool UseItemByType_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        int[] itemIDs = (int[]) null;
        switch (args.ToLower())
        {
          case "bola":
            itemIDs = new int[1]{ 9900 };
            break;
          case "bandage":
            itemIDs = new int[2]{ 3617, 3817 };
            break;
          case "dagger":
            itemIDs = new int[2]{ 3921, 3922 };
            break;
          case "candle":
            itemIDs = new int[2]{ 2575, 2600 };
            break;
          case "moongate":
            Engine.UseMoongate();
            break;
          case "trinsic petal":
          case "strength petal":
          case "purple petal":
            Engine.UseItemByTypeAndHue(new int[1]{ 4129 }, 14);
            return true;
          case "orange petal":
            Engine.UseItemByTypeAndHue(new int[1]{ 4129 }, 43);
            return true;
          default:
            try
            {
              itemIDs = new int[1]{ int.Parse(args) };
              break;
            }
            catch
            {
              break;
            }
        }
        if (itemIDs != null)
          Engine.UseItemByType(itemIDs);
      }
      return true;
    }

    private static bool LastObject_OnAction(string args)
    {
      PUseRequest.SendLast();
      return true;
    }

    private static bool Last_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "object":
            PUseRequest.SendLast();
            break;
          case "spell":
            PCastSpell.SendLast();
            break;
          case "skill":
            PUseSkill.SendLast();
            break;
        }
      }
      return true;
    }

    private static bool Attack_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "last":
            Engine.AttackLast();
            break;
        }
      }
      return true;
    }

    private static bool Count_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "regs":
            Engine.CountReagents();
            break;
          case "ammo":
            Engine.CountAmmo();
            break;
        }
      }
      return true;
    }

    private static bool UseSkill_OnAction(string args)
    {
      if (args != null && args.ToLower() == "smartpotion")
      {
        Engine.SmartPotion();
      }
      else
      {
        Skill skill = Engine.Skills[Engine.Skills.GetSkill(args)];
        if (skill != null)
          skill.Use();
        else
          Engine.AddTextMessage(string.Format("Unknown skill '{0}'", (object) args));
      }
      return true;
    }

    private static bool SetAbility_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "primary":
            AbilityInfo.Active = AbilityInfo.Primary;
            break;
          case "secondary":
            AbilityInfo.Active = AbilityInfo.Secondary;
            break;
          case "none":
            AbilityInfo.Active = (AbilityInfo) null;
            break;
        }
      }
      return true;
    }

    private static bool AllNames_OnAction(string args)
    {
      Engine.AllNames();
      return true;
    }

    private static bool Quit_OnAction(string args)
    {
      Engine.Quit();
      return true;
    }

    private static bool StopMacros_OnAction(string args)
    {
      Macros.StopAll();
      return true;
    }

    private static bool WaitForTarget_OnAction(string args)
    {
      return TargetManager.IsActive;
    }

    private static bool WaitForTargetLast_OnAction(string args)
    {
      if (TargetManager.IsActive && TargetManager.LastTarget != null)
        return TargetManager.TargetIsInRange();
      return false;
    }

    private static bool Dismount_OnAction(string args)
    {
      Engine.Dismount();
      return true;
    }

    private static bool Remount_OnAction(string args)
    {
      Engine.Remount();
      return true;
    }

    private static bool Paste_OnAction(string args)
    {
      if (args != null && args.Length > 0)
        Engine.Paste(args);
      else
        Engine.Paste();
      return true;
    }

    private static bool BandageSelf_OnAction(string args)
    {
      Engine.BandageSelf();
      return true;
    }

    private static bool Resync_OnAction(string args)
    {
      Engine.Resync();
      return true;
    }

    private static bool Wrestle_OnAction(string args)
    {
      try
      {
        switch ((WrestleType) Enum.Parse(typeof (WrestleType), args, true))
        {
          case WrestleType.Disarm:
            return Network.Send((Packet) new PWrestleDisarm());
          case WrestleType.Stun:
            return Network.Send((Packet) new PWrestleStun());
        }
      }
      catch
      {
        Engine.AddTextMessage(string.Format("Unknown wrestle type: {0}", (object) args));
      }
      return true;
    }

    private static bool Open_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "help":
            Engine.OpenHelp();
            break;
          case "options":
            Engine.OpenOptions();
            break;
          case "journal":
            Engine.OpenJournal();
            break;
          case "skills":
            Engine.OpenSkills();
            break;
          case "status":
            Engine.OpenStatus();
            break;
          case "spellbook":
            Engine.OpenSpellbook(1);
            break;
          case "necrospellbook":
            Engine.OpenSpellbook(2);
            break;
          case "paladinspellbook":
            Engine.OpenSpellbook(3);
            break;
          case "paperdoll":
            Engine.OpenPaperdoll();
            break;
          case "backpack":
            Engine.OpenBackpack();
            break;
          case "radar":
            GRadar.Open();
            break;
          case "abilities":
            GCombatGump.Open();
            break;
          case "macros":
            GMacroEditorForm.Open();
            break;
        }
      }
      return true;
    }

    private static bool Bow_OnAction(string args)
    {
      return Network.Send((Packet) new PAction("bow"));
    }

    private static bool Salute_OnAction(string args)
    {
      return Network.Send((Packet) new PAction("salute"));
    }

    private static bool SetEquip_OnAction(string args)
    {
      try
      {
        Engine.SetEquip(Convert.ToInt32(args));
      }
      catch
      {
      }
      return true;
    }

    private static bool Equip_OnAction(string args)
    {
      try
      {
        Engine.Equip(Convert.ToInt32(args));
      }
      catch
      {
      }
      return true;
    }

    private static bool Dequip_OnAction(string args)
    {
      Engine.Dequip();
      return true;
    }

    private static bool Dress_OnAction(string args)
    {
      Engine.Dress();
      return true;
    }

    private static bool UsePotion_OnAction(string args)
    {
      if (args.ToLower() == "smart")
      {
        Engine.SmartPotion();
        return true;
      }
      try
      {
        PotionType type = (PotionType) Enum.Parse(typeof (PotionType), args, true);
        if (!Engine.UsePotion(type))
          Engine.AddTextMessage(string.Format("You do not have any {0} potions!", (object) type.ToString().ToLower()), Engine.DefaultFont, Hues.Load(34));
      }
      catch
      {
        Engine.AddTextMessage(string.Format("Unknown potion type: {0}", (object) args));
      }
      return true;
    }

    private static bool UseOnce_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "set":
            Engine.SetAutoUse();
            break;
          case "use":
            Engine.AutoUse();
            break;
        }
      }
      return true;
    }

    private static bool Cast_OnAction(string args)
    {
      Spell spellByName = Spells.GetSpellByName(args);
      if (spellByName != null)
        spellByName.Cast();
      return true;
    }

    private static bool DelayMacro_OnAction(string args)
    {
      try
      {
        return Macro.Delay(int.Parse(args));
      }
      catch
      {
      }
      return true;
    }

    private static bool Repeat_OnAction(string args)
    {
      if (args != null && args.Length > 0)
      {
        switch (args.ToLower())
        {
          case "speech":
            Engine.Repeat();
            break;
          case "macro":
            Macro.Repeat();
            break;
        }
      }
      return true;
    }

    private static bool Screenshots_OnAction(string args)
    {
      Options.Current.Screenshots = MacroHandlers.Toggle(Options.Current.Screenshots, args);
      return true;
    }

    private static bool RegCounter_OnAction(string args)
    {
      GItemCounters.Active = MacroHandlers.Toggle(GItemCounters.Active, args);
      return true;
    }

    private static bool Warmode_OnAction(string args)
    {
      Engine.Warmode = MacroHandlers.Toggle(Engine.Warmode, args);
      return true;
    }

    private static bool Halos_OnAction(string args)
    {
      Options.Current.NotorietyHalos = MacroHandlers.Toggle(Options.Current.NotorietyHalos, args);
      return true;
    }

    private static bool ParticleCount_OnAction(string args)
    {
      Renderer.DrawPCount = MacroHandlers.Toggle(Renderer.DrawPCount, args);
      return true;
    }

    private static bool Temperature_OnAction(string args)
    {
      Engine.Effects.DrawTemperature = MacroHandlers.Toggle(Engine.Effects.DrawTemperature, args);
      return true;
    }

    private static bool Ping_OnAction(string args)
    {
      Renderer.DrawPing = MacroHandlers.Toggle(Renderer.DrawPing, args);
      return true;
    }

    private static bool Transparency_OnAction(string args)
    {
      Renderer.Transparency = MacroHandlers.Toggle(Renderer.Transparency, args);
      return true;
    }

    private static bool ContainerGrid_OnAction(string args)
    {
      Options.Current.ContainerGrid = MacroHandlers.Toggle(Options.Current.ContainerGrid, args);
      return true;
    }

    private static bool Grid_OnAction(string args)
    {
      Engine.Grid = MacroHandlers.Toggle(Engine.Grid, args);
      return true;
    }

    private static bool PumpFPS_OnAction(string args)
    {
      Engine.m_PumpFPS = MacroHandlers.Toggle(Engine.m_PumpFPS, args);
      return true;
    }

    private static bool FPS_OnAction(string args)
    {
      Engine.FPS = MacroHandlers.Toggle(Engine.FPS, args);
      return true;
    }

    private static bool MiniHealth_OnAction(string args)
    {
      Options.Current.MiniHealth = MacroHandlers.Toggle(Options.Current.MiniHealth, args);
      return true;
    }

    private static bool ToggleHotkeys_OnAction(string args)
    {
      Options.Current.HotkeysEnabled = MacroHandlers.Toggle(Options.Current.HotkeysEnabled, args);
      return true;
    }
  }
}
