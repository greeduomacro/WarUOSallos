// Decompiled with JetBrains decompiler
// Type: PlayUO.Engine
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Prompts;
using PlayUO.Targeting;
using PlayUO.Videos;
using SharpDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Sallos;
using SharpDX.Direct3D9;
using Archive = Sallos.Archive;
using Archives = Ultima.Data.Archives;
using Rectangle = System.Drawing.Rectangle;
using Color = System.Drawing.Color;

namespace PlayUO
{
  public class Engine
  {
    public static Queue m_DataStores = new Queue();
    public static int GameWidth = 640;
    public static int GameHeight = 480;
    public static int GameX = 20;
    public static int GameY = 20;
    public static bool m_regMap = true;
    public static string m_Text = "";
    public static bool m_Locked = true;
    public static Regex m_Encoder = new Regex("&#(?<1>[0-9a-fA-F]+);", RegexOptions.None);
    private static float m_MobileDuration = 7.5f;
    private static float m_ItemDuration = 5f;
    private static float m_SystemDuration = 7.5f;
    public static Stack<uint> _movementKeys = new Stack<uint>();
    public static MapBlock[] m_KeepAliveBlocks = new MapBlock[128];
    private static int m_LastDown = -1;
    private string cheese = "delicious";
    public const double HalfPI = 1.5707963267949;
    public const double FlipIt = 3.92699081698724;
    public const int FALSE = 0;
    public const int TRUE = 1;
    public const int WalkAckSync = 4;
    public const float C528Scale = 8.225806f;
    public const float C825Scale = 0.1215686f;
    public const string CommandPrefix = ". ";
    public const string Category = "UONET19";
    private const int QS_KEY = 1;
    private const int QS_MOUSEMOVE = 2;
    private const int QS_MOUSEBUTTON = 4;
    private const int QS_POSTMESSAGE = 8;
    private const int QS_TIMER = 16;
    private const int QS_PAINT = 32;
    private const int QS_SENDMESSAGE = 64;
    private const int QS_HOTKEY = 128;
    private const int QS_ANYTHING = 255;
    internal static int m_Ticks;
    internal static double m_dTicks;
    public static bool m_SetTicks;
    static Stopwatch _sw;
    public static Direct3D m_Direct3D;
    public static Device m_Device;
    public static Rectangle m_rRender;
    public static Multis m_Multis;
    public static Sounds m_Sounds;
    public static Gumps m_Gumps;
    public static LandArt m_LandArt;
    public static TextureArt m_TextureArt;
    public static ItemArt m_ItemArt;
    public static Animations m_Animations;
    public static Skills m_Skills;
    public static Effects m_Effects;
    public static Features m_Features;
    public static ServerFeatures m_ServerFeatures;
    public static int ScreenWidth;
    public static int ScreenHeight;
    public static bool exiting;
    public static int m_Sequence;
    public static int m_OkSequence;
    public static int m_WalkReq;
    public static int m_WalkAck;
    public static int m_xMouse;
    public static int m_yMouse;
    public static int m_dMouse;
    public static Display m_Display;
    public static TimeDelay m_MoveDelay;
    public static bool amMoving;
    public static Direction movingDir;
    public static Direction pointingDir;
    public static bool m_Redraw;
    public static Font[] m_Font;
    public static UnicodeFont[] m_UniFont;
    public static TimeDelay m_NewFrame;
    public static TimeDelay m_SleepMode;
    public static bool m_Ingame;
    public static bool m_Loading;
    public static ArrayList m_Timers;
    public static bool m_Fullscreen;
    public static int m_OpenedGumps;
    public static bool m_SkillsOpen;
    public static GSkills m_SkillsGump;
    public static bool m_JournalOpen;
    public static GJournal m_JournalGump;
    public static ArrayList m_Journal;
    public static FileManager m_FileManager;
    public static DateTime m_LastAction;
    private static DateTime m_LastStealthUse;
    private static DateTime m_LastLeapfrogPickup;
    public static Mobile m_BuyHorse;
    public static string m_LastCommand;
    public static Item m_LeapFrog;
    public static bool m_SayMacro;
    private static string m_LastSpeech;
    public static IFont m_DefaultFont;
    public static IHue m_DefaultHue;
    private static DateTime m_NextAction;
    public static Random m_Random;
    public static IPrompt m_Prompt;
    public static Texture m_Rain;
    public static Texture[] m_Snow;
    public static Texture m_FormX;
    public static Texture m_Slider;
    public static Texture[] m_Edge;
    public static Texture[] m_WinScrolls;
    public static Texture m_SkillUp;
    public static Texture m_SkillDown;
    public static Texture m_SkillLocked;
    public static ImageCache _imageCache;
    public static AuthenticationTicket _ticket;
    public static bool m_EventOk;
    public static Queue m_LoadQueue;
    public static Queue m_MapLoadQueue;
    public static bool m_PumpFPS;
    public static int m_KeepAliveBlockIndex;
    public static MidiTable m_MidiTable;
    public static ContainerBoundsTable m_ContainerBoundsTable;
    private static object m_ClickSender;
    private static EventArgs m_ClickArgs;
    private static object[] m_ClickList;
    public static bool m_Stealth;
    public static int m_StealthSteps;
    public static bool m_InResync;
    private static int m_PingID;
    public static Queue m_Pings;
    private static int m_Ping;
    public static Timer m_PingTimer;
    public static bool m_MultiPreview;
    public static int m_MultiSerial;
    public static int m_MultiID;
    public static int m_MultiMinX;
    public static int m_MultiMinY;
    public static int m_MultiMaxX;
    public static int m_MultiMaxY;
    public static int m_xMultiOffset;
    public static int m_yMultiOffset;
    public static int m_zMultiOffset;
    public static ArrayList m_MultiList;
    public static Timer m_ClickTimer;
    public static int m_xClick;
    public static int m_yClick;
    public static object m_Highlight;
    public static string m_ServerName;
    public static TimeDelay m_AllNames;
    public static DateTime m_HealSpam;
    public static DateTime m_NextHealPotion;
    public static int m_World;
    public static TimeDelay m_LastOverCheck;
    public static MouseEventArgs m_LastMouseArgs;
    public static bool m_MouseMoved;
    private static Point m_LastDownPoint;
    private static Timer m_PopupDelay;
    public static Mobile m_LastAttacker;
    public static PresentParameters m_PresentParams;
    public static VertexBuffer m_VertexBuffer;

    public static double dTicks
    {
      get
      {
        if (!Engine.m_SetTicks)
          Engine.UpdateTicks();
        return Engine.m_dTicks;
      }
    }

    public static int Ticks
    {
      get
      {
        if (!Engine.m_SetTicks)
          Engine.UpdateTicks();
        return Engine.m_Ticks;
      }
    }

    public static Multis Multis
    {
      get
      {
        if (Engine.m_Multis == null)
          Engine.m_Multis = new Multis();
        return Engine.m_Multis;
      }
    }

    public static ItemArt ItemArt
    {
      get
      {
        if (Engine.m_ItemArt == null)
          Engine.m_ItemArt = new ItemArt();
        return Engine.m_ItemArt;
      }
    }

    public static LandArt LandArt
    {
      get
      {
        if (Engine.m_LandArt == null)
          Engine.m_LandArt = new LandArt();
        return Engine.m_LandArt;
      }
    }

    public static TextureArt TextureArt
    {
      get
      {
        if (Engine.m_TextureArt == null)
          Engine.m_TextureArt = new TextureArt();
        return Engine.m_TextureArt;
      }
    }

    public static ServerFeatures ServerFeatures
    {
      get
      {
        if (Engine.m_ServerFeatures == null)
          Engine.m_ServerFeatures = new ServerFeatures();
        return Engine.m_ServerFeatures;
      }
    }

    public static Features Features
    {
      get
      {
        if (Engine.m_Features == null)
          Engine.m_Features = new Features();
        return Engine.m_Features;
      }
    }

    public static Sounds Sounds
    {
      get
      {
        if (Engine.m_Sounds == null)
        {
          Debug.TimeBlock("Initializing Sounds");
          Engine.m_Sounds = new Sounds();
          Debug.EndBlock();
        }
        return Engine.m_Sounds;
      }
    }

    public static Skills Skills
    {
      get
      {
        if (Engine.m_Skills == null)
        {
          Debug.TimeBlock("Initializing Skills");
          Engine.m_Skills = new Skills();
          Debug.EndBlock();
        }
        return Engine.m_Skills;
      }
    }

    public static bool RealGMPrivs
    {
      get
      {
        Mobile player = World.Player;
        if (player == null)
          return false;
        if ((int) player.Body == 987)
          return true;
        return player.Flags[MobileFlag.YellowHits];
      }
    }

    public static bool GMPrivs
    {
      get
      {
        if (World.Player != null)
          return (int) World.Player.Body == 987;
        return false;
      }
    }

    public static FileManager FileManager
    {
      get
      {
        return Engine.m_FileManager;
      }
    }

    public static float MobileDuration
    {
      get
      {
        return Engine.m_MobileDuration;
      }
    }

    public static float ItemDuration
    {
      get
      {
        return Engine.m_ItemDuration;
      }
    }

    public static float SystemDuration
    {
      get
      {
        return Engine.m_SystemDuration;
      }
    }

    public static IFont DefaultFont
    {
      get
      {
        return Engine.m_DefaultFont;
      }
    }

    public static IHue DefaultHue
    {
      get
      {
        return Engine.m_DefaultHue;
      }
    }

    public static TimeSpan TripTime
    {
      get
      {
        return TimeSpan.FromMilliseconds((double) (Engine.Ping / 2));
      }
    }

    public static bool IsActionReady
    {
      get
      {
        return DateTime.Now + Engine.TripTime > Engine.m_NextAction;
      }
    }

    public static Random Random
    {
      get
      {
        if (Engine.m_Random == null)
          Engine.m_Random = new Random();
        return Engine.m_Random;
      }
    }

    public static IPrompt Prompt
    {
      get
      {
        return Engine.m_Prompt;
      }
      set
      {
        if (Engine.m_Prompt == value)
          return;
        if (Engine.m_Prompt != null && value != null)
          Engine.m_Prompt.OnCancel(PromptCancelType.NewPrompt);
        Engine.m_Prompt = value;
      }
    }

    public static Effects Effects
    {
      get
      {
        return Engine.m_Effects;
      }
    }

    internal static ImageCache ImageCache
    {
      get
      {
        if (Engine._imageCache == null)
          Engine._imageCache = new ImageCache();
        return Engine._imageCache;
      }
    }

    public static MidiTable MidiTable
    {
      get
      {
        if (Engine.m_MidiTable == null)
          Engine.m_MidiTable = new MidiTable();
        return Engine.m_MidiTable;
      }
    }

    public static ContainerBoundsTable ContainerBoundsTable
    {
      get
      {
        if (Engine.m_ContainerBoundsTable == null)
          Engine.m_ContainerBoundsTable = new ContainerBoundsTable();
        return Engine.m_ContainerBoundsTable;
      }
    }

    public static int Ping
    {
      get
      {
        return Engine.m_Ping;
      }
    }

    public static bool Grid
    {
      get
      {
        return Renderer.DrawGrid;
      }
      set
      {
        Renderer.DrawGrid = value;
      }
    }

    public static bool FPS
    {
      get
      {
        return Renderer.DrawFPS;
      }
      set
      {
        Renderer.DrawFPS = value;
      }
    }

    public static bool Warmode
    {
      get
      {
        Mobile player = World.Player;
        if (player != null)
          return player.Flags[MobileFlag.Warmode];
        return false;
      }
      set
      {
        if (World.Player == null)
          return;
        Network.Send((Packet) new PSetWarMode(value, (short) 32, (byte) 0));
        if (value)
          return;
        Engine.m_Highlight = (object) null;
      }
    }

    public static void ClearScreen()
    {
      Gumps.Desktop.Children.Clear();
      int num1 = Engine.GameWidth;
      int num2 = Engine.GameHeight;
      int num3 = num1 / 48;
      int num4 = num2 / 48;
      int num5 = num3 * 48 - 4;
      int num6 = num4 * 48 - 4;
      int num7 = (num1 - num5) / 2;
      int num8 = (num2 - num6) / 2;
      for (int index = 0; index < num3; ++index)
      {
        Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(num7 + index * 48, -54));
        Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(num7 + index * 48, num2 + 6 + 4));
      }
      for (int index = 0; index < num4; ++index)
      {
        Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(-54, num8 + index * 48));
        Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(num1 + 6 + 4, num8 + index * 48));
      }
      Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(-54, -54));
      Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(num1 + 6 + 4, -54));
      Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(-54, num2 + 6 + 4));
      Gumps.Desktop.Children.Add((Gump) new GSpellPlaceholder(num1 + 6 + 4, num2 + 6 + 4));
      Gumps.Desktop.Children.Add((Gump) new GDesktopBorder());
      Gumps.Desktop.Children.Add((Gump) new GBandageTimer());
      Gumps.Desktop.Children.Add((Gump) new GMapTracker());
      Gumps.Desktop.Children.Add((Gump) new GQuestArrow());
      Gumps.Desktop.Children.Add((Gump) new GPingDisplay());
      Gumps.Desktop.Children.Add((Gump) new GTransparencyGump());
      Gumps.Desktop.Children.Add((Gump) new GQueueStatus());
      Reagent[] reagents = Spells.Reagents;
      int num9 = reagents.Length;
      if (Engine.ServerFeatures == null || !Engine.ServerFeatures.AOS)
        num9 = 8;
      PotionType[] potionTypeArray = new PotionType[6]{ PotionType.Yellow, PotionType.Orange, PotionType.Red, PotionType.Purple, PotionType.White, PotionType.Blue };
      ItemIDValidator[] itemIdValidatorArray = new ItemIDValidator[num9 + 1 + potionTypeArray.Length];
      for (int index = 0; index < num9; ++index)
        itemIdValidatorArray[index] = new ItemIDValidator(new int[1]
        {
          reagents[index].ItemID
        });
      for (int index = 0; index < potionTypeArray.Length; ++index)
        itemIdValidatorArray[num9 + index] = new ItemIDValidator(new int[1]
        {
          (int) (3846 + potionTypeArray[index])
        });
      itemIdValidatorArray[num9 + potionTypeArray.Length] = new ItemIDValidator(new int[2]{ 3617, 3817 });
      Gumps.Desktop.Children.Add((Gump) new GItemCounters(itemIdValidatorArray));
    }

    public static void UseItemByType(int[] itemIDs)
    {
      Item obj = Engine.FindItem(itemIDs);
      if (obj == null)
        return;
      obj.Use();
    }

    public static void UseItemByTypeAndHue(int[] itemIDs, int hue)
    {
      Item[] items = Engine.FindItems(itemIDs);
      if (items == null || items.Length == 0)
        return;
      foreach (Item obj in items)
      {
        if ((int) obj.Hue == hue)
        {
          obj.Use();
          break;
        }
      }
    }

    public static void Remount()
    {
      Mobile player = World.Player;
      if (player == null || player.FindEquip(Layer.Mount) != null)
        return;
      ItemRef mount = Player.Current.EquipAgent.Mount;
      if (mount != null)
      {
        Item onPlayer = mount.FindOnPlayer();
        if (onPlayer != null)
        {
          onPlayer.Use();
          return;
        }
      }
      MountTable mountTable = Engine.m_Animations.MountTable;
      foreach (Mobile mobile in World.Mobiles.Values)
      {
        if (player.InRange((int) mobile.XReal, (int) mobile.YReal, 1) && !mobile.IsDeadPet && mountTable.IsMount((int) mobile.Body))
        {
          if (mobile.Name == null || mobile.Name.Length == 0)
            mobile.QueryStats();
          else if (mobile.IsPet)
          {
            mobile.Use();
            break;
          }
        }
      }
    }

    public static void Dismount()
    {
      Mobile player = World.Player;
      if (player == null || player.FindEquip(Layer.Mount) == null)
        return;
      player.Use();
    }

    public static void Paste()
    {
      Debug.Trace("Paste();");
      string text = Clipboard.GetText();
      if (string.IsNullOrEmpty(text))
        return;
      Engine.Paste(text);
    }

    public static void Paste(string ToPaste)
    {
      Debug.Trace("Paste( {0} );", (object) ToPaste);
      string[] strArray = (Engine.m_Text + ToPaste.Replace("\r\n", "\n")).Split('\n');
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
      {
        strArray[index] = strArray[index].Trim();
        if (index < length - 1)
        {
          Debug.Trace("commandEntered( {0} );", (object) strArray[index]);
          Engine.commandEntered(strArray[index]);
        }
        else
        {
          Debug.Trace("SetText( {0} );", (object) strArray[index]);
          Engine.m_Text = strArray[index];
          Renderer.SetText(strArray[index]);
        }
      }
    }

    public static void Dequip()
    {
      Player.Current.EquipAgent.Arms.Dequip();
    }

    public static void Dequip(bool message)
    {
      Player.Current.EquipAgent.Arms.Dequip(message);
    }

    public static void Equip(int index)
    {
      Player.Current.EquipAgent.Arms.Equip(index);
    }

    public static void Dress()
    {
      Player.Current.EquipAgent.Dress.EnsureDressed();
    }

    public static void SetEquip(int index)
    {
      TargetManager.Client = (ClientTargetHandler) new SetEquipTargetHandler(index);
    }

    public static void SetAutoUse()
    {
      TargetManager.Client = (ClientTargetHandler) new AddAutoUseTargetHandler();
    }

    public static void AutoUse()
    {
      Player.Current.UseOnceAgent.Use();
    }

    public static double UpdateTicks()
    {
      if (Engine._sw == null)
        Engine._sw = Stopwatch.StartNew();
      Engine.m_Ticks = (int) Engine._sw.ElapsedMilliseconds;
      Engine.m_dTicks = (double) Engine._sw.ElapsedMilliseconds;
      Engine.m_SetTicks = true;
      return Engine.m_dTicks;
    }

    public static void ResetTicks()
    {
      Engine.m_SetTicks = false;
    }

    public static ArrayList GetDataStore()
    {
      return Engine.m_DataStores.Count <= 0 ? new ArrayList() : (ArrayList) Engine.m_DataStores.Dequeue();
    }

    public static void ReleaseDataStore(ArrayList list)
    {
      if (list.Count > 0)
        list.Clear();
      Engine.m_DataStores.Enqueue((object) list);
    }

    public static void Resurrect_OnAnimationEnd(Animation a, Mobile m)
    {
      if (m == null)
        return;
      m.Update();
    }

    public static int GetAnimDirection(byte dir)
    {
      return (((int) dir & 7) + 5) % 8 | (int) dir & 128;
    }

    public static byte GetWalkDirection(Direction d)
    {
      return (byte) (Direction.West & d - (byte) 1);
    }

    public static void DoWalk(Direction d, bool fromRenderer)
    {
      if (Playback.Active)
        return;
      fromRenderer = false;
      if (Engine.m_InResync)
        return;
      Mobile player = World.Player;
      if (player == null || player.Walking.Count > 0 || (int) player.Body != 987 && Engine.m_WalkReq >= Engine.m_WalkAck + 4)
        return;
      if (Engine.m_Stealth && Engine.m_StealthSteps == 0)
      {
        if (!(DateTime.Now >= Engine.m_LastStealthUse + TimeSpan.FromSeconds(2.0)))
          return;
        Engine.Skills[SkillName.Stealth].Use();
        Engine.m_LastStealthUse = DateTime.Now;
      }
      else
      {
        GContextMenu.Close();
        int x1 = player.X;
        int y1 = player.Y;
        int z = player.Z;
        bool ghost = player.Ghost;
        bool flag1 = !ghost && (player.CurrentStamina <= 0 && player.MaximumStamina > 0);
        bool flag2 = !flag1 && (player.CurrentStamina == 1 && player.MaximumStamina > 0);
        if (flag1 || flag2)
          flag1 = flag2 = !Engine.UsePotion(PotionType.Red);
        if (flag1)
          return;
        if (Engine.m_Stealth)
          flag2 = true;
        int newZ;
        int newDir;
        if (!Walking.Calculate(x1, y1, z, d, out newZ, out newDir))
        {
          if (((int) player.Direction & 7) != (newDir & 7))
          {
            WalkAnimation walkAnimation = WalkAnimation.PoolInstance(player, player.X, player.Y, player.Z, newDir);
            player.Walking.Enqueue((object) walkAnimation);
            player.IsMoving = true;
            walkAnimation.Start();
            Engine.SendMovementRequest(newDir, player.X, player.Y, player.Z, TimeSpan.FromSeconds(0.1));
            player.Direction = (byte) newDir;
          }
          else
          {
            player.MovedTiles = 0;
            player.HorseFootsteps = 0;
            player.IsMoving = false;
          }
        }
        else
        {
          int num = newDir & 7 | (flag2 || Engine.m_dMouse <= Engine.GameWidth / 3 ? 0 : 128);
          if (!flag2 && Options.Current.AlwaysRun)
            num |= 128;
          int x2 = x1;
          int y2 = y1;
          if (fromRenderer || (num & 7) == ((int) player.Direction & 7))
            Walking.Offset(num, ref x2, ref y2);
          else
            newZ = player.Z;
          if (Engine.m_LeapFrog != null && !Engine.m_LeapFrog.InRange((IPoint2D) new Point(x2, y2), 2) && Engine.m_LeapFrog.InRange(x1, y1, 2))
          {
            if (!(Engine.m_LastLeapfrogPickup + TimeSpan.FromSeconds(0.1) < DateTime.Now))
              return;
            Engine.m_LastLeapfrogPickup = DateTime.Now;
            Walking.Offset(num, ref x2, ref y2);
            Network.Send((Packet) new PPickupItem(Engine.m_LeapFrog, Engine.m_LeapFrog.Amount));
            Network.Send((Packet) new PDropItem(Engine.m_LeapFrog.Serial, (int) (short) x2, (int) (short) y2, (int) (sbyte) newZ, -1));
          }
          else
          {
            WalkAnimation walkAnimation = WalkAnimation.PoolInstance(player, x2, y2, newZ, num);
            player.Walking.Enqueue((object) walkAnimation);
            bool isMoving = player.IsMoving;
            player.IsMoving = true;
            walkAnimation.Start();
            player.SetReal(x2, y2, newZ, num);
            if (!isMoving && walkAnimation.Advance)
            {
              World.Offset(walkAnimation.xOffset, walkAnimation.yOffset);
              Engine.Effects.Offset(walkAnimation.xOffset, walkAnimation.yOffset);
            }
            Engine.Redraw();
            if ((num & 7) != ((int) player.Direction & 7))
            {
              Engine.SendMovementRequest(num, player.X, player.Y, player.Z, TimeSpan.FromSeconds(0.1));
              if (!fromRenderer)
              {
                player.Direction = (byte) num;
                return;
              }
            }
            if (!ghost && (int) player.Body != 987)
            {
              MapPackage cache = Map.GetCache();
              ArrayList arrayList = cache.cells[x2 - cache.CellX, y2 - cache.CellY];
              for (int index = 0; index < arrayList.Count; ++index)
              {
                ICell cell = (ICell) arrayList[index];
                if (cell is DynamicItem && ((DynamicItem) cell).m_Item.IsDoor)
                {
                  Network.Send((Packet) new POpenDoor());
                  break;
                }
              }
            }
            if (Engine.m_Stealth)
              --Engine.m_StealthSteps;
            Engine.SendMovementRequest(num, x2, y2, newZ, TimeSpan.FromSeconds(player.IsMounted ? 0.1 : 0.2));
            player.Direction = (byte) num;
            if (!Options.Current.Scavenger)
              return;
            Preferences.Current.Scavenger.Scavenge(false);
          }
        }
      }
    }

    public static void AddTimer(Timer t)
    {
      Engine.m_Timers.Add((object) t);
    }

    public static void RemoveTimer(Timer t)
    {
      Engine.m_Timers.Remove((object) t);
    }

    public static bool IsMoving()
    {
      Mobile player = World.Player;
      if (player != null)
        return player.Walking.Count > 0;
      return false;
    }

    public static byte ByteCap(int Value)
    {
      if (Value < 0)
        return 0;
      if (Value > (int) byte.MaxValue)
        return byte.MaxValue;
      return (byte) Value;
    }

    public static int Blend32(int a32, int b32, int n)
    {
      int num1 = a32 >> 16 & (int) byte.MaxValue;
      int num2 = a32 >> 8 & (int) byte.MaxValue;
      int num3 = a32 & (int) byte.MaxValue;
      int num4 = b32 >> 16 & (int) byte.MaxValue;
      int num5 = b32 >> 8 & (int) byte.MaxValue;
      int num6 = b32 & (int) byte.MaxValue;
      return (num1 * ((int) byte.MaxValue - n) + num4 * n + (int) sbyte.MaxValue) / (int) byte.MaxValue << 16 | (num2 * ((int) byte.MaxValue - n) + num5 * n + (int) sbyte.MaxValue) / (int) byte.MaxValue << 8 | (num3 * ((int) byte.MaxValue - n) + num6 * n + (int) sbyte.MaxValue) / (int) byte.MaxValue;
    }

    public static ushort C32216(int c32)
    {
      return (ushort) (32768 | ((c32 >> 16 & (int) byte.MaxValue) * 31 + (int) sbyte.MaxValue) / (int) byte.MaxValue << 10 | ((c32 >> 8 & (int) byte.MaxValue) * 31 + (int) sbyte.MaxValue) / (int) byte.MaxValue << 5 | ((c32 & (int) byte.MaxValue) * 31 + (int) sbyte.MaxValue) / (int) byte.MaxValue);
    }

    public static int C16232(int C16)
    {
      return (int) Engine.ByteCap((int) ((float) (C16 >> 10 & 31) * 8.225806f)) << 16 | (int) Engine.ByteCap((int) ((float) (C16 >> 5 & 31) * 8.225806f)) << 8 | (int) Engine.ByteCap((int) ((float) (C16 & 31) * 8.225806f));
    }

    public static int GrayScale(int Color)
    {
      int num = (int) ((float) ((double) ((float) (Color >> 10 & 31) * 8.225806f) * 0.29899999499321 + (double) ((float) (Color >> 5 & 31) * 8.225806f) * 0.587000012397766 + (double) ((float) (Color & 31) * 8.225806f) * (57.0 / 500.0)) * 0.1215686f);
      if (num < 0)
        num = 0;
      else if (num > 31)
        num = 31;
      return num;
    }

    public static float GrayScale(int r, int g, int b)
    {
      return (float) ((double) r * 0.29899999499321 + (double) g * 0.587000012397766 + (double) b * (57.0 / 500.0));
    }

    public static string Encode(string Input)
    {
      return Engine.m_Encoder.Replace(Input.ToString(), new MatchEvaluator(Engine.CharEntity_Match));
    }

    private static string CharEntity_Match(Match m)
    {
      try
      {
        int int32 = Convert.ToInt32(m.Groups[1].Value, 16);
        switch (int32)
        {
          case 10:
          case 13:
            return m.Groups[0].Value;
          default:
            return ((char) int32).ToString();
        }
      }
      catch
      {
        return m.Groups[0].Value;
      }
    }

    public static string FormatByteLength(int bytes)
    {
      if (bytes < 1000000)
        return string.Format("{0:N2} KB", (object) ((double) bytes / 1024.0), (object) bytes);
      if (bytes < 1000000000)
        return string.Format("{0:N2} MB", (object) ((double) bytes / 1024.0 / 1024.0), (object) bytes);
      return string.Format("{0:N2} GB", (object) ((double) bytes / 1024.0 / 1024.0 / 1024.0), (object) bytes);
    }

    public static void TimeRefresh_OnTick(Timer t)
    {
      int num1 = (int) t.GetTag("Frames");
      double num2 = (double) num1;
      Cursor.Hourglass = true;
      Engine.m_SetTicks = false;
      double dTicks1 = Engine.dTicks;
      Renderer._timeRefresh = true;
      while (--num1 >= 0)
        Renderer.Draw();
      if (Renderer._profile != null)
        Engine.AddTextMessage(Renderer._profile.ToString());
      Renderer._timeRefresh = false;
      Engine.m_SetTicks = false;
      double dTicks2 = Engine.dTicks;
      Cursor.Hourglass = false;
      Engine.AddTextMessage(string.Format("Time Refresh: {0} frames in {1:F2} seconds: {2:F2} FPS", (object) num2, (object) ((dTicks2 - dTicks1) * 0.001), (object) (num2 / ((dTicks2 - dTicks1) * 0.001))));
    }

    public static void RestoreSpeech()
    {
      if (Engine.m_LastSpeech == null)
        return;
      Renderer.SetText(Engine.m_LastSpeech);
      Engine.m_Text = Engine.m_LastSpeech;
    }

    public static void commandEntered(string cmd)
    {
      if (Playback.Active)
        return;
      if (cmd.Length > 0 && !Engine.m_SayMacro)
        Engine.m_LastCommand = cmd;
      if (Engine.m_Prompt != null)
      {
        Engine.m_Prompt.OnReturn(cmd);
        Engine.m_Prompt = (IPrompt) null;
      }
      else
      {
        cmd = cmd.Trim();
        if (cmd.Length <= 0)
          return;
        if (!Engine.m_SayMacro)
          Engine.m_LastSpeech = cmd;
        SpeechFormat.Find(cmd).OnSpeech(cmd);
      }
    }

    public static void AddTextMessage(string Message)
    {
      Engine.AddTextMessage(Message, Engine.m_SystemDuration, Engine.m_DefaultFont, Engine.m_DefaultHue);
    }

    public static string MakeProperCase(string text)
    {
      StringBuilder stringBuilder = new StringBuilder(text);
      for (int index = 0; index < stringBuilder.Length; ++index)
      {
        if (index == 0 || (int) stringBuilder[index - 1] == 32)
          stringBuilder[index] = char.ToUpper(stringBuilder[index]);
      }
      return stringBuilder.ToString();
    }

    public static void AddTextMessage(string Message, IFont Font)
    {
      Engine.AddTextMessage(Message, Engine.m_SystemDuration, Font, Engine.m_DefaultHue);
    }

    public static void AddTextMessage(string Message, IFont Font, IHue Hue)
    {
      Engine.AddTextMessage(Message, Engine.m_SystemDuration, Font, Hue);
    }

    public static void AddTextMessage(string Message, float Delay)
    {
      Engine.AddTextMessage(Message, Delay, Engine.m_DefaultFont, Engine.m_DefaultHue);
    }

    public static void AddTextMessage(string Message, float Delay, IFont Font)
    {
      Engine.AddTextMessage(Message, Delay, Font, Engine.m_DefaultHue);
    }

    public static void AddTextMessage(string Message, float Delay, IFont Font, IHue Hue)
    {
      if (!Engine.m_Ingame)
        return;
      Message = Message.TrimEnd();
      if (Message.Length <= 0)
        return;
      Engine.AddToJournal(new JournalEntry(Message, Hue, -1));
      Message = Engine.WrapText(Message, Engine.GameWidth / 2, Font).TrimEnd();
      if (Message.Length <= 0)
        return;
      MessageManager.AddMessage((IMessage) new GSystemMessage(Message, Font, Hue, Delay));
    }

    public static string WrapText(string text, int width, IFont f)
    {
      WrapKey wrapKey = new WrapKey(text, width);
      object obj = f.WrapCache[(object) wrapKey];
      if (obj != null)
        return (string) obj;
      if (f.GetStringWidth(text) <= width)
      {
        f.WrapCache.Add((object) wrapKey, (object) text);
        return text;
      }
      string[] strArray = text.Split(' ');
      StringBuilder stringBuilder1 = new StringBuilder();
      ArrayList dataStore = Engine.GetDataStore();
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        if (f.GetStringWidth(stringBuilder1.ToString() + strArray[index1]) > width)
        {
          if (f.GetStringWidth(strArray[index1]) > width)
          {
            stringBuilder1.Append(strArray[index1]);
            while (stringBuilder1.Length > 1 && f.GetStringWidth(stringBuilder1.ToString()) > width)
            {
              StringBuilder stringBuilder2 = new StringBuilder();
              stringBuilder2.Append(stringBuilder1[0]);
              for (int index2 = 1; index2 < stringBuilder1.Length; ++index2)
              {
                if (f.GetStringWidth(stringBuilder2.ToString() + (object) stringBuilder1[index2]) > width)
                {
                  dataStore.Add((object) stringBuilder2);
                  stringBuilder1 = new StringBuilder(stringBuilder1.ToString().Substring(stringBuilder2.Length));
                  break;
                }
                stringBuilder2.Append(stringBuilder1[index2]);
              }
            }
            if (index1 < strArray.Length - 1)
              stringBuilder1.Append(' ');
          }
          else
          {
            if (stringBuilder1.Length > 0)
              dataStore.Add((object) stringBuilder1);
            stringBuilder1 = new StringBuilder(strArray[index1]);
            if (index1 < strArray.Length - 1)
              stringBuilder1.Append(' ');
          }
        }
        else
        {
          stringBuilder1.Append(strArray[index1]);
          if (index1 < strArray.Length - 1)
            stringBuilder1.Append(' ');
        }
      }
      if (stringBuilder1.Length > 0)
      {
        while (stringBuilder1.Length > 1 && f.GetStringWidth(stringBuilder1.ToString()) > width)
        {
          StringBuilder stringBuilder2 = new StringBuilder();
          stringBuilder2.Append(stringBuilder1[0]);
          for (int index = 1; index < stringBuilder1.Length; ++index)
          {
            if (f.GetStringWidth(stringBuilder2.ToString() + (object) stringBuilder1[index]) > width)
            {
              dataStore.Add((object) stringBuilder2);
              stringBuilder1 = new StringBuilder(stringBuilder1.ToString().Substring(stringBuilder2.Length));
              break;
            }
            stringBuilder2.Append(stringBuilder1[index]);
          }
        }
        if (stringBuilder1.Length > 0)
          dataStore.Add((object) stringBuilder1);
      }
      StringBuilder stringBuilder3 = new StringBuilder();
      int count = dataStore.Count;
      for (int index = 0; index < count; ++index)
      {
        stringBuilder3.Append(((StringBuilder) dataStore[index]).ToString());
        if (index < count - 1)
          stringBuilder3.Append('\n');
      }
      string @string = stringBuilder3.ToString();
      f.WrapCache.Add((object) wrapKey, (object) @string);
      Engine.ReleaseDataStore(dataStore);
      return @string;
    }

    public static void DrawNow()
    {
      Engine.DoEvents();
      Renderer.Draw();
      Engine.DoEvents();
    }

    public static void WantDirectory(string Target)
    {
      string path = Engine.FileManager.BasePath(Target);
      if (Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }

    public static void MakeDirectory(string Target)
    {
      Directory.CreateDirectory(Engine.FileManager.BasePath(Target));
    }

    public static void Unlock()
    {
      Engine.m_Locked = false;
    }

    public static void Exception_Unhandled(object Sender, UnhandledExceptionEventArgs e)
    {
      Debug.Trace("Unhandled exception");
      Debug.Trace("Object -> {0}", Sender);
      object exceptionObject = e.ExceptionObject;
      if (exceptionObject is Exception)
        Debug.Error((Exception) exceptionObject);
      else
        Debug.Trace("Exception -> {0}", exceptionObject);
    }

    public static void DelayedAction()
    {
      DateTime dateTime = DateTime.Now + TimeSpan.FromSeconds(0.2) - Engine.TripTime;
      if (!(dateTime < Engine.m_NextAction))
        return;
      Engine.m_NextAction = dateTime;
    }

    public static void PushAction()
    {
      Engine.m_NextAction = DateTime.Now + TimeSpan.FromSeconds(0.6) + Engine.TripTime;
    }

    public static void Options_OnClick(Gump g)
    {
      Engine.OpenOptions();
    }

    public static void OpenOptions()
    {
      GObjectEditor.Open((object) Preferences.Current);
    }

    public static void OpenOptionsMacros()
    {
    }

    public static int RandomRange(int start, int count)
    {
      return start + Engine.Random.Next(count);
    }

    public static int GetRandomHue()
    {
      return Engine.RandomRange(2, 1000);
    }

    public static int GetRandomNeutralHue()
    {
      return Engine.RandomRange(1801, 108);
    }

    public static int GetRandomMetalHue()
    {
      return Engine.RandomRange(2401, 30);
    }

    public static IHue GetRandomBlueHue()
    {
      return Hues.Load(Engine.RandomRange(1301, 54));
    }

    public static IHue GetRandomRedHue()
    {
      return Hues.Load(Engine.RandomRange(1601, 54));
    }

    public static int GetRandomHairHue()
    {
      return Engine.RandomRange(1102, 48);
    }

    public static int GetRandomSkinHue()
    {
      return Engine.RandomRange(1002, 56);
    }

    public static int GetRandomYellowHue()
    {
      return Engine.RandomRange(1701, 54);
    }

    public static int Smallest(int x, int y)
    {
      if (x < y)
        return x;
      return y;
    }

    public static int Biggest(int x, int y)
    {
      if (x > y)
        return x;
      return y;
    }

    public static void ListView_OnValueChange(double Value, double Old, Gump Sender)
    {
      if (!Sender.HasTag("ListBox"))
        return;
      GListBox glistBox = (GListBox) Sender.GetTag("ListBox");
      if (glistBox == null)
        return;
      glistBox.StartIndex = (int) Value;
    }

    public static void ScrollUp_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Scroller"))
        return;
      GVSlider gvSlider = (GVSlider) Sender.GetTag("Scroller");
      if (gvSlider == null)
        return;
      gvSlider.SetValue(gvSlider.GetValue() - gvSlider.Increase, true);
    }

    public static void ScrollDown_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Scroller"))
        return;
      GVSlider gvSlider = (GVSlider) Sender.GetTag("Scroller");
      if (gvSlider == null)
        return;
      gvSlider.SetValue(gvSlider.GetValue() + gvSlider.Increase, true);
    }

    public static void URLButton_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("URL"))
        return;
      Engine.OpenBrowser((string) Sender.GetTag("URL"));
    }

    public static void OpenBrowser(string url)
    {
      Uri result;
      if (!Uri.TryCreate(url, UriKind.Absolute, out result) || result.IsFile || result.IsLoopback)
        return;
      if (string.Equals(Path.GetExtension(result.AbsolutePath), ".rpv", StringComparison.OrdinalIgnoreCase))
        Playback.Download(result);
      else
        Process.Start(result.ToString());
    }

    public static Version GetVersion()
    {
      return new Version(7, 0, 26, 4);
    }

    public static string GetVersionString()
    {
      return string.Format("{0} PlayUO", (object) Engine.GetVersion());
    }

    public static void Setup()
    {
      Cursor.Gold = false;
      Engine.m_LastAttacker = (Mobile) null;
      Renderer.AlwaysHighlight = 0;
      Engine._movementKeys = new Stack<uint>();
      for (int index = 0; index < 5; ++index)
        Engine._movementKeys.Push(3131961357U);
      Engine.m_Journal.Clear();
      Renderer.DrawFPS = false;
      Engine.m_Ingame = false;
      Engine.m_WalkAck = 0;
      Engine.m_WalkReq = 0;
      Macros.StopAll();
      World.Clear();
      Cursor.Hourglass = true;
      if (TargetManager.IsActive)
      {
        TargetManager.Active.Clear();
        if (TargetManager.IsActive)
          TargetManager.Active.Clear();
      }
      Gumps.Desktop.Children.Clear();
    }

    public static void ShowAcctLogin()
    {
      Engine.exiting = true;
    }

    private static void PlayRandomMidi()
    {
      if (Preferences.Current.Music.Volume.IsMuted)
        return;
      int[] numArray = new int[5]{ 1, 2, 3, 5, 7 };
      Random random = new Random();
      string fileName = Engine.MidiTable.Translate(numArray[random.Next(numArray.Length)]);
      if (fileName == null)
        return;
      Music.Play(fileName);
    }

    public static void TickTimers()
    {
      int count = Engine.m_Timers.Count;
      int index = 0;
      while (index < count)
      {
        if (!((Timer) Engine.m_Timers[index]).Tick())
        {
          Engine.m_Timers.RemoveAt(index);
          count = Engine.m_Timers.Count;
        }
        else
          ++index;
      }
    }

    public static void DoEvents()
    {
      Application.DoEvents();
    }

    public static Texture LoadImageAsAlpha(string path)
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile(string.Format("play/images/{0}", (object) path));
      if (archivedFile != null)
      {
        using (Stream stream = archivedFile.Download())
        {
          using (Bitmap bitmap = new Bitmap(stream))
          {
            bitmap.MakeTransparent(Color.Black);
            for (int y = 0; y < bitmap.Height; ++y)
            {
              for (int x = 0; x < bitmap.Width; ++x)
              {
                Color pixel = bitmap.GetPixel(x, y);
                bitmap.SetPixel(x, y, Color.FromArgb((int) pixel.B, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
              }
            }
            try
            {
              return Texture.FromBitmap(bitmap);
            }
            catch
            {
            }
          }
        }
      }
      Debug.Trace("LoadImageAsAlpha( \"{0}\" ) failed", (object) path);
      return Texture.Empty;
    }

    internal static Bitmap LoadArchivedBitmap(string path)
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/images/" + path);
      if (archivedFile == null)
        return (Bitmap) null;
      using (Stream stream = archivedFile.Download())
        return new Bitmap(stream);
    }

    internal static Bitmap LoadArchivedBitmap(Archive archive, string path)
    {
      if (archive != null)
      {
        ArchivedFile file = archive.FindFile(path);
        if (file != null)
        {
          using (Stream stream = file.Download())
            return new Bitmap(stream);
        }
      }
      return (Bitmap) null;
    }

    internal static Texture LoadArchivedTexture(string path)
    {
      try
      {
        using (Bitmap bitmap = Engine.LoadArchivedBitmap(path))
        {
          if (bitmap != null)
            return Texture.FromBitmap(bitmap);
        }
      }
      catch
      {
      }
      Debug.Trace("LoadArchivedTexture( \"{0}\" ) failed", (object) path);
      return Texture.Empty;
    }

    internal static Texture LoadArchivedTexture(Archive archive, string path)
    {
      try
      {
        using (Bitmap bitmap = Engine.LoadArchivedBitmap(archive, path))
        {
          if (bitmap != null)
            return Texture.FromBitmap(bitmap);
        }
      }
      catch
      {
      }
      Debug.Trace("LoadArchivedTexture( \"{0}\" ) failed", (object) path);
      return Texture.Empty;
    }

    public static void LoadParticles()
    {
      Engine.m_Rain = Engine.LoadArchivedTexture("rain.png");
      Engine.m_FormX = Engine.LoadImageAsAlpha("Form_X.bmp");
      Engine.m_Slider = Engine.LoadImageAsAlpha("Slider.bmp");
      Engine.m_SkillUp = Engine.LoadImageAsAlpha("Skill_Up.bmp");
      Engine.m_SkillDown = Engine.LoadImageAsAlpha("Skill_Down.bmp");
      Engine.m_SkillLocked = Engine.LoadImageAsAlpha("Skill_Locked.bmp");
      Engine.m_Snow = new Texture[12];
      for (int index = 0; index < 12; ++index)
        Engine.m_Snow[index] = Engine.LoadImageAsAlpha(string.Format("Snow_{0}.bmp", (object) (index + 1)));
      Engine.m_Edge = new Texture[8];
      for (int index = 0; index < 8; ++index)
        Engine.m_Edge[index] = Engine.LoadImageAsAlpha(string.Format("Edge_{0}.bmp", (object) (index + 1)));
      Engine.m_WinScrolls = new Texture[4];
      Engine.m_WinScrolls[0] = Engine.LoadImageAsAlpha("WinScroll_Up.bmp");
      Engine.m_WinScrolls[1] = Engine.LoadImageAsAlpha("WinScroll_Down.bmp");
      Engine.m_WinScrolls[2] = Engine.LoadImageAsAlpha("WinScroll_Left.bmp");
      Engine.m_WinScrolls[3] = Engine.LoadImageAsAlpha("WinScroll_Right.bmp");
    }

    public static DateTime GetTimeStamp(string path)
    {
      return new FileInfo(path).LastWriteTime;
    }

    private static void ParseArgs(string[] args)
    {
      int length = args.Length;
      int num1 = 0;
      while (num1 < length)
      {
        if (string.Equals(args[num1++], "-ticket", StringComparison.OrdinalIgnoreCase))
        {
          if (Engine._ticket != null)
            throw new InvalidOperationException("Authentication ticket already specified.");
          if (num1 + 4 > args.Length)
            throw new InvalidOperationException("Malformed authentication ticket specified.");
          Engine._ticket = new AuthenticationTicket();
          AuthenticationTicket authenticationTicket1 = Engine._ticket;
          string[] strArray1 = args;
          int index1 = num1;
          int num2 = 1;
          int num3 = index1 + num2;
          IPAddress ipAddress = IPAddress.Parse(strArray1[index1]);
          authenticationTicket1._ipAddress = ipAddress;
          AuthenticationTicket authenticationTicket2 = Engine._ticket;
          string[] strArray2 = args;
          int index2 = num3;
          int num4 = 1;
          int num5 = index2 + num4;
          int num6 = int.Parse(strArray2[index2]);
          authenticationTicket2._port = num6;
          AuthenticationTicket authenticationTicket3 = Engine._ticket;
          string[] strArray3 = args;
          int index3 = num5;
          int num7 = 1;
          int num8 = index3 + num7;
          long num9 = (long) ulong.Parse(strArray3[index3]);
          authenticationTicket3._key = (ulong) num9;
          AuthenticationTicket authenticationTicket4 = Engine._ticket;
          string[] strArray4 = args;
          int index4 = num8;
          int num10 = 1;
          num1 = index4 + num10;
          string str = strArray4[index4];
          authenticationTicket4._contentArchive = str;
        }
      }
    }

    public static void QueueMapLoad(int xBlock, int yBlock, TileMatrix matrix)
    {
      if (xBlock < 0 || yBlock < 0 || (xBlock >= matrix.BlockWidth || yBlock >= matrix.BlockHeight))
        return;
      Mobile player = World.Player;
      if (player != null)
      {
        int num = player.Ghost ? 1 : 0;
      }
      if (matrix.CheckLoaded(xBlock, yBlock))
        return;
      Engine.m_MapLoadQueue.Enqueue((object) new Worker(xBlock, yBlock, matrix));
    }

    public static void Preload(Worker w)
    {
      TileMatrix tileMatrix = w.Matrix;
      int blockWidth = tileMatrix.BlockWidth;
      int blockHeight = tileMatrix.BlockHeight;
      if (w.X < 0 || w.Y < 0 || (w.X >= blockWidth || w.Y >= blockHeight))
        return;
      Mobile player = World.Player;
      Hues.DefaultHue defaultHue = (Hues.DefaultHue) Hues.Default;
      MapBlock block = tileMatrix.GetBlock(w.X, w.Y);
      bool flag = false;
      for (int index = 0; !flag && index < Engine.m_KeepAliveBlocks.Length; ++index)
        flag = Engine.m_KeepAliveBlocks[index] == block;
      if (!flag)
      {
        Engine.m_KeepAliveBlocks[Engine.m_KeepAliveBlockIndex % Engine.m_KeepAliveBlocks.Length] = block;
        ++Engine.m_KeepAliveBlockIndex;
      }
      Tile[] tileArray = block == null ? tileMatrix.InvalidLandBlock : block.m_LandTiles;
      for (int index = 0; index < tileArray.Length; ++index)
      {
        if (!defaultHue.HintLand((int) tileArray[index].landId))
          Engine.m_LoadQueue.Enqueue((object) new LandLoader((int) tileArray[index].landId));
      }
      HuedTile[][][] huedTileArray1 = block == null ? tileMatrix.EmptyStaticBlock : block.m_StaticTiles;
      for (int index1 = 0; index1 < 8; ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
        {
          HuedTile[] huedTileArray2 = huedTileArray1[index1][index2];
          for (int index3 = 0; index3 < huedTileArray2.Length; ++index3)
          {
            if ((int) huedTileArray2[index3].hueId == 0 && !defaultHue.HintItem((int) huedTileArray2[index3].itemId))
              Engine.m_LoadQueue.Enqueue((object) new ItemLoader((int) huedTileArray2[index3].itemId));
          }
        }
      }
    }

    public static void ClickTimer_OnTick(Timer t)
    {
      if (Engine.m_ClickList != null)
      {
        Gump gump = (Gump) Engine.m_ClickList[0];
        Point point = (Point) Engine.m_ClickList[1];
        gump.OnSingleClick(point.X, point.Y);
      }
      else
        Engine.Click(Engine.m_ClickSender, Engine.m_ClickArgs);
      Engine.m_ClickTimer.Stop();
    }

    public static void Ignore()
    {
      TargetManager.Client = (ClientTargetHandler) new IgnoreTargetHandler();
      Engine.AddTextMessage("Who do you wish to ignore?");
    }

    [DllImport("User32")]
    private static extern int GetQueueStatus(int flags);

    public static bool Resync()
    {
      if (Engine.m_InResync)
        return false;
      Engine.m_InResync = true;
      Engine.AddTextMessage("Please wait, resynchronizing.");
      return Network.Send((Packet) new PResyncRequest());
    }

    public static bool HandleException(Exception ex)
    {
      try
      {
        GenericExceptionDialog genericExceptionDialog = new GenericExceptionDialog();
        string fmt = !(ex is NetworkException) ? (!(ex is StorageException) ? "PlayUO has encountered an error and must close." : "PlayUO has encountered a storage error and must close.") : "PlayUO has encountered a network error and must close.";
        genericExceptionDialog.SetExceptionInfo(ex, fmt);
        if (Engine.m_Display != null && !Engine.m_Display.IsDisposed)
        {
          int num1 = (int) genericExceptionDialog.ShowDialog((IWin32Window) Engine.m_Display);
        }
        else
        {
          int num2 = (int) genericExceptionDialog.ShowDialog();
        }
      }
      catch
      {
      }
      try
      {
        Debug.Error(ex);
        return true;
      }
      catch
      {
        return false;
      }
    }

    private static bool Verify()
    {
      Archive archive = Archive.AcquireArchive("ultima");
      if (archive == null || archive.FindFile("play/Ultima.Client.exe") == null)
        return false;
      byte[] publicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
      byte[] numArray = new byte[8]{ (byte) 108, (byte) 199, (byte) 232, (byte) 189, (byte) 137, (byte) 197, (byte) 198, (byte) 191 };
      if (publicKeyToken.Length != numArray.Length)
        return false;
      for (int index = 0; index < publicKeyToken.Length; ++index)
      {
        if ((int) publicKeyToken[index] != (int) numArray[index])
          return false;
      }
      return true;
    }

    [STAThread]
    public static void Main(string[] args)
    {
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Engine.CurrentDomain_UnhandledException);
      AppDomain.CurrentDomain.DomainUnload += (EventHandler) ((o, ea) => Music.Destroy());
      //if (!Engine.Verify())
      //  return;
      try
      {
        Engine.m_FileManager = new FileManager();
        if (Engine.m_FileManager.Error)
        {
          Engine.m_FileManager = (FileManager) null;
          GC.Collect();
          throw new InvalidOperationException("Unable to initialize file manager.");
        }
        Engine.MainA(args);
      }
      catch (Exception ex)
      {
        Engine.HandleException(ex);
        throw;
      }
    }

    public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      if (!e.IsTerminating || !(e.ExceptionObject is Exception))
        return;
      Engine.HandleException((Exception) e.ExceptionObject);
    }

    public static void UseMoongate()
    {
      Item obj = World.FindItem((IItemValidator) new PlayerDistanceValidator((IItemValidator) new ItemIDValidator(new int[3]{ 3546, 3948, 8148 }), 1));
      if (obj == null)
      {
        Engine.AddTextMessage("Moongate not found.", Engine.DefaultFont, Hues.Load(38));
      }
      else
      {
        if (!new UseContext((IEntity) obj, true).Enqueue())
          return;
        obj.AddTextMessage("Moongate", "- using -", Engine.DefaultFont, Hues.Load(53), true);
      }
    }

    public static void Disturb()
    {
      if (TargetManager.Server != null)
        TargetManager.Server.Cancel();
      else
        World.Player.Disturb();
    }

    [STAThread]
    public static void MainA(string[] Args)
    {
      Engine.ParseArgs(Args);
      //if (Engine._ticket == null)
      //  return;
      Engine.WantDirectory("data/");
      Engine.WantDirectory("data/ultima/");
      Engine.WantDirectory("data/ultima/logs/");
      Debug.Trace("Entered Main()");
      Debug.Block("Environment");
      Debug.Trace("Operating System = '{0}'", (object) Environment.OSVersion);
      Debug.Trace(".NET Framework   = '{0}'", (object) Environment.Version);
      Debug.Trace("Base Directory   = '{0}'", (object) Engine.m_FileManager.BasePath(""));
      Debug.Trace("Data Directory   = '{0}'", (object) Engine.m_FileManager.ResolveMUL(""));
      Debug.EndBlock();
      Engine.m_Timers = new ArrayList();
      Engine.m_Journal = new ArrayList();
      Engine.m_Pings = new Queue();
      Engine.m_LoadQueue = new Queue();
      Engine.m_MapLoadQueue = new Queue();
      MacroHandlers.Setup();
      Debug.Block("Main()");
      Engine.m_ClickTimer = new Timer(new OnTick(Engine.ClickTimer_OnTick), SystemInformation.DoubleClickTime);
      Debug.Try("Initializing Display");
      Engine.m_Display = new Display();
      Preferences.Current.Layout.Apply(false);
      Engine.m_Display.KeyPreview = true;
      Engine.m_Display.Show();
      Preferences.Current.Layout.Apply(false);
      Preferences.Current.Layout.Update();
      Debug.EndTry();
      Application.DoEvents();
      Debug.Block("Initializing DirectX");
      Engine.InitDX();
      Debug.EndBlock();
      Engine.m_Loading = true;
      Engine.m_Ingame = false;
      Cursor.Hourglass = true;
      Engine.DrawNow();
      Debug.TimeBlock("Initializing Animations");
      Engine.m_Animations = new Animations();
      Debug.EndBlock();
      Engine.m_Font = new Font[10];
      Engine.m_UniFont = new UnicodeFont[3];
      Debug.TimeBlock("Initializing Gumps");
      Engine.m_Gumps = new Gumps();
      Debug.EndBlock();
      Engine.m_DefaultFont = (IFont) Engine.GetUniFont(3);
      Engine.m_DefaultHue = Hues.Load(946);
      Renderer.SetText("");
      Macros.Reset();
      Engine.LoadParticles();
      Renderer.FilterEnable = false;
      Renderer.SetTexture(Engine.m_Rain);
      try
      {
        Engine.m_Device.ValidateDevice(1);
      }
      catch (Exception ex)
      {
        Engine.m_Rain.Dispose();
        Engine.m_Rain = Texture.Empty;
        Engine.m_SkillUp.Dispose();
        Engine.m_SkillUp = Hues.Default.GetGump(2435);
        Engine.m_SkillDown.Dispose();
        Engine.m_SkillDown = Hues.Default.GetGump(2437);
        Engine.m_SkillLocked.Dispose();
        Engine.m_SkillLocked = Hues.Default.GetGump(2092);
        Engine.m_Slider.Dispose();
        Engine.m_Slider = Hues.Default.GetGump(2117);
        for (int index = 0; index < Engine.m_Snow.Length; ++index)
        {
          Engine.m_Snow[index].Dispose();
          Engine.m_Snow[index] = Texture.Empty;
        }
        for (int index = 0; index < Engine.m_Edge.Length; ++index)
        {
          Engine.m_Edge[index].Dispose();
          Engine.m_Edge[index] = Texture.Empty;
        }
        Debug.Trace("ValidateDevice() failed on 32-bit textures");
        Debug.Error(ex);
      }
      Renderer.SetTexture((Texture) null);
      Engine.m_Effects = new Effects();
      Engine.m_Loading = false;
      System.Drawing.Point client = Engine.m_Display.PointToClient(System.Windows.Forms.Cursor.Position);
      Engine.m_EventOk = true;
      Engine.MouseMove((object) Engine.m_Display, new MouseEventArgs(Control.MouseButtons, 0, client.X, client.Y, 0));
      Compression.CheckCache();
      Engine.Setup();
      Engine.MouseMoveQueue();
      Engine.m_EventOk = false;
      Preferences.Current.Layout.Update();
      Engine.DrawNow();
      Engine.m_MoveDelay = new TimeDelay(0.0f);
      Engine.m_LastOverCheck = new TimeDelay(0.1f);
      Engine.m_NewFrame = new TimeDelay(0.05f);
      Engine.m_SleepMode = new TimeDelay(7.5f);
      Engine.m_EventOk = true;
      bool flag1 = false;
      Animations.StartLoading();
      Engine.Unlock();
      DateTime dateTime = DateTime.Now;
      int ticks = Engine.Ticks;
      bool flag2 = true;
      if (Animations.IsLoading)
      {
        do
        {
          Engine.DrawNow();
        }
        while (!Animations.WaitLoading());
      }
      uint seed;
      Hash.Start(out seed);
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        Hash.Append(assembly.FullName.ToString().GetHashCode(), ref seed);
      Hash.Finish(ref seed);
      Debug.Trace("Connecting to {0}:{1}", (object) Engine._ticket._ipAddress, (object) Engine._ticket._port);
      if (!Network.Connect((BaseCrypto) new GameCrypto(seed), new IPEndPoint(Engine._ticket._ipAddress, Engine._ticket._port)))
        throw new InvalidOperationException("Unable to connect.");
      Engine.m_ServerName = Engine._ticket._ipAddress.ToString();
      Network.Send((Packet) new PLoginSeedEx(seed));
      Network.Send((Packet) new PPE_Login(Engine._ticket._key));
      Engine.PingRequest(false);
      Compression.CheckCache();
      Stopwatch stopwatch = new Stopwatch();
      while (!Engine.exiting)
      {
        stopwatch.Start();
        Engine.m_SetTicks = false;
        ticks = Engine.Ticks;
        Macros.Slice();
        ActionContext.InvokeQueue();
        if (Gumps.Invalidated)
        {
          if (Engine.m_LastMouseArgs != null)
            Engine.MouseMove((object) Engine.m_Display, Engine.m_LastMouseArgs);
          Gumps.Invalidated = false;
        }
        if (Engine.m_MouseMoved)
          Engine.MouseMoveQueue();
        if (Engine.m_NewFrame.ElapsedReset())
        {
          ++Renderer.m_Frames;
          Engine.m_Redraw = false;
          Renderer.Draw();
          stopwatch.Reset();
          stopwatch.Start();
        }
        else if (Engine.m_Redraw || Engine.m_PumpFPS || Engine.amMoving && Options.Current.SmoothWalk && Engine.IsMoving())
        {
          Engine.m_Redraw = false;
          Renderer.Draw();
          stopwatch.Reset();
          stopwatch.Start();
        }
        if (flag2 && Engine.m_Ingame && (Party.State == PartyState.Joined && DateTime.Now >= dateTime))
        {
          dateTime = DateTime.Now + TimeSpan.FromSeconds(0.5);
          Network.Send((Packet) new PPE_QueryPartyLocsEx());
        }
        Thread.Sleep(World.Player == null || !World.Player.IsMoving ? 1 : 0);
        Engine.DoEvents();
        if (Engine.m_Ingame && !World.HasIdentified)
        {
          Mobile player = World.Player;
          if (player != null && !player.Flags[MobileFlag.Hidden])
          {
            player.OnSingleClick();
            World.HasIdentified = true;
          }
        }
        if (!Network.Slice())
        {
          flag1 = true;
          break;
        }
        Network.Flush();
        Engine.TickTimers();
        if (Engine.amMoving && Engine.m_Ingame)
          Engine.DoWalk(Engine.movingDir, false);
        if (Engine.m_LoadQueue.Count > 0)
        {
          for (int index = 0; Engine.m_LoadQueue.Count > 0 && index < 6; ++index)
            ((ILoader) Engine.m_LoadQueue.Dequeue()).Load();
        }
        if (Engine.m_MapLoadQueue.Count > 0)
          Engine.Preload((Worker) Engine.m_MapLoadQueue.Dequeue());
      }
      PlayUO.Profiles.Config.Current.Save();
      Thread.Sleep(5);
      if (Engine.m_Display != null && !Engine.m_Display.IsDisposed)
        Engine.m_Display.Hide();
      Thread.Sleep(5);
      Application.DoEvents();
      Thread.Sleep(5);
      Application.DoEvents();
      Engine.m_Animations.Dispose();
      if (Engine.m_ItemArt != null)
        Engine.m_ItemArt.Dispose();
      if (Engine.m_LandArt != null)
        Engine.m_LandArt.Dispose();
      if (Engine.m_TextureArt != null)
        Engine.m_TextureArt.Dispose();
      Engine.m_Gumps.Dispose();
      if (Engine.m_Sounds != null)
        Engine.m_Sounds.Dispose();
      if (Engine.m_Multis != null)
        Engine.m_Multis.Dispose();
      Engine.m_FileManager.Dispose();
      Cursor.Dispose();
      Music.Dispose();
      Hues.Dispose();
      GRadar.Dispose();
      if (Engine._imageCache != null)
      {
        Engine._imageCache.Dispose();
        Engine._imageCache = (ImageCache) null;
      }
      if (Engine.m_Rain != null)
      {
        Engine.m_Rain.Dispose();
        Engine.m_Rain = (Texture) null;
      }
      if (Engine.m_Slider != null)
      {
        Engine.m_Slider.Dispose();
        Engine.m_Slider = (Texture) null;
      }
      if (Engine.m_SkillUp != null)
      {
        Engine.m_SkillUp.Dispose();
        Engine.m_SkillUp = (Texture) null;
      }
      if (Engine.m_SkillDown != null)
      {
        Engine.m_SkillDown.Dispose();
        Engine.m_SkillDown = (Texture) null;
      }
      if (Engine.m_SkillLocked != null)
      {
        Engine.m_SkillLocked.Dispose();
        Engine.m_SkillLocked = (Texture) null;
      }
      if (Engine.m_Snow != null)
      {
        for (int index = 0; index < 12; ++index)
        {
          if (Engine.m_Snow[index] != null)
          {
            Engine.m_Snow[index].Dispose();
            Engine.m_Snow[index] = (Texture) null;
          }
        }
        Engine.m_Snow = (Texture[]) null;
      }
      if (Engine.m_Edge != null)
      {
        for (int index = 0; index < 8; ++index)
        {
          if (Engine.m_Edge[index] != null)
          {
            Engine.m_Edge[index].Dispose();
            Engine.m_Edge[index] = (Texture) null;
          }
        }
        Engine.m_Edge = (Texture[]) null;
      }
      if (Engine.m_WinScrolls != null)
      {
        for (int index = 0; index < Engine.m_WinScrolls.Length; ++index)
        {
          if (Engine.m_WinScrolls[index] != null)
          {
            Engine.m_WinScrolls[index].Dispose();
            Engine.m_WinScrolls[index] = (Texture) null;
          }
        }
        Engine.m_WinScrolls = (Texture[]) null;
      }
      if (Engine.m_FormX != null)
      {
        Engine.m_FormX.Dispose();
        Engine.m_FormX = (Texture) null;
      }
      if (Engine.m_Font != null)
      {
        for (int index = 0; index < 10; ++index)
        {
          if (Engine.m_Font[index] != null)
          {
            Engine.m_Font[index].Dispose();
            Engine.m_Font[index] = (Font) null;
          }
        }
        Engine.m_Font = (Font[]) null;
      }
      if (Engine.m_UniFont != null)
      {
        int length = Engine.m_UniFont.Length;
        for (int index = 0; index < length; ++index)
        {
          if (Engine.m_UniFont[index] != null)
          {
            Engine.m_UniFont[index].Dispose();
            Engine.m_UniFont[index] = (UnicodeFont) null;
          }
        }
        Engine.m_UniFont = (UnicodeFont[]) null;
      }
      if (Engine.m_MidiTable != null)
      {
        Engine.m_MidiTable.Dispose();
        Engine.m_MidiTable = (MidiTable) null;
      }
      if (Engine.m_ContainerBoundsTable != null)
      {
        Engine.m_ContainerBoundsTable.Dispose();
        Engine.m_ContainerBoundsTable = (ContainerBoundsTable) null;
      }
      Texture.DisposeAll();
      Debug.EndBlock();
      if (flag1)
        Debug.Trace("Network error caused termination");
      Network.Close();
      Debug.Dispose();
      Speech.Dispose();
      Map.Shutdown();
      Archives.Shutdown();
      Engine.m_LoadQueue = (Queue) null;
      Engine.m_MapLoadQueue = (Queue) null;
      Engine.m_DefaultFont = (IFont) null;
      Engine.m_DefaultHue = (IHue) null;
      Engine.m_Display = (Display) null;
      Engine.m_Encoder = (Regex) null;
      Engine.m_Effects = (Effects) null;
      Engine.m_Skills = (Skills) null;
      Engine.m_Features = (Features) null;
      Engine.m_Animations = (Animations) null;
      Engine.m_LandArt = (LandArt) null;
      Engine.m_TextureArt = (TextureArt) null;
      Engine.m_ItemArt = (ItemArt) null;
      Engine.m_Gumps = (Gumps) null;
      Engine.m_Sounds = (Sounds) null;
      Engine.m_Multis = (Multis) null;
      Engine.m_FileManager = (FileManager) null;
      Engine.m_Display = (Display) null;
      Engine.m_Font = (Font[]) null;
      Engine.m_UniFont = (UnicodeFont[]) null;
      Engine.m_Device = (Device) null;
      Engine.m_MoveDelay = (TimeDelay) null;
      Engine.m_Text = (string) null;
      Engine.m_Font = (Font[]) null;
      Engine.m_UniFont = (UnicodeFont[]) null;
      Engine.m_NewFrame = (TimeDelay) null;
      Engine.m_SleepMode = (TimeDelay) null;
      Engine.m_Timers = (ArrayList) null;
      Engine.m_SkillsGump = (GSkills) null;
      Engine.m_JournalGump = (GJournal) null;
      Engine.m_Journal = (ArrayList) null;
      Engine.m_FileManager = (FileManager) null;
      Engine.m_Encoder = (Regex) null;
      Engine.m_DefaultFont = (IFont) null;
      Engine.m_DefaultHue = (IHue) null;
      Engine.m_Random = (Random) null;
      Engine._movementKeys = (Stack<uint>) null;
      Engine.m_Prompt = (IPrompt) null;
      Engine.m_Pings = (Queue) null;
      Engine.m_PingTimer = (Timer) null;
      Engine.m_MultiList = (ArrayList) null;
      Engine.m_AllNames = (TimeDelay) null;
      Engine.m_LastOverCheck = (TimeDelay) null;
      Engine.m_LastMouseArgs = (MouseEventArgs) null;
      Engine.m_LastAttacker = (Mobile) null;
    }

    public static void MouseWheel(object sender, MouseEventArgs e)
    {
      if (!Engine.m_EventOk || !Playback.Active && (e.Delta > 0 && Engine.m_Ingame && Macros.Start((Keys) 69632) || e.Delta < 0 && Engine.m_Ingame && Macros.Start((Keys) 69633)))
        return;
      Gumps.MouseWheel(e.X, e.Y, e.Delta);
    }

    public static void ClearPings()
    {
      Engine.m_Pings.Clear();
      Engine.m_PingID = 0;
      Engine.m_Ping = 0;
      if (Engine.m_PingTimer == null)
        return;
      Engine.m_PingTimer.Delete();
    }

    public static void StartPings()
    {
      Engine.ClearPings();
      Engine.m_PingTimer = new Timer(new OnTick(Engine.Ping_OnTick), 5000);
      Engine.m_PingTimer.Start(false);
    }

    public static void Ping_OnTick(Timer t)
    {
      Engine.PingRequest(true);
    }

    public static void PingRequest(bool sendPing)
    {
      if (!Engine.m_Ingame || World.Player == null || Engine.m_WalkAck < Engine.m_WalkReq)
        return;
      int tickCount = Environment.TickCount;
      int PingID = 2 + Engine.m_PingID % 254;
      if (sendPing && !Network.Send((Packet) new PPing(PingID)))
        return;
      Engine.m_Pings.Clear();
      Engine.m_Pings.Enqueue((object) new int[2]
      {
        tickCount,
        sendPing ? PingID : -1
      });
      if (!sendPing)
        return;
      ++Engine.m_PingID;
    }

    public static void PingReply(int val)
    {
      if (val == 0)
        ActionContext.HandleSignal(true);
      else if (val == 1)
      {
        ActionContext.HandleSignal(false);
      }
      else
      {
        try
        {
          int[] numArray = Engine.m_Pings.Dequeue() as int[];
          int tickCount = Environment.TickCount;
          int num = numArray[0];
          if (numArray[1] != val)
            return;
          Engine.m_Ping = tickCount - num;
        }
        catch
        {
          Engine.StartPings();
        }
      }
    }

    private static void AttackDialog_YesNo(bool yes)
    {
    }

    public static void DoubleClick(object sender, EventArgs e)
    {
      if (!Engine.m_EventOk || Engine.m_Locked || (Engine.amMoving || !Engine.m_ClickTimer.Enabled))
        return;
      Engine.m_ClickTimer.Stop();
      if (Gumps.DoubleClick(Engine.m_xMouse, Engine.m_yMouse))
        return;
      int TileX = 0;
      int TileY = 0;
      Renderer.ResetHitTest();
      ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY);
      if (tileFromXy == null || TargetManager.IsActive)
        return;
      if (tileFromXy.GetType() == typeof (DynamicItem))
      {
        Item obj = ((DynamicItem) tileFromXy).m_Item;
        if (obj == null)
          return;
        obj.OnDoubleClick();
      }
      else
      {
        if (!(tileFromXy.GetType() == typeof (MobileCell)))
          return;
        Mobile mobile = ((MobileCell) tileFromXy).m_Mobile;
        if (mobile == null)
          return;
        mobile.OnDoubleClick();
      }
    }

    public static void ClickMessage(object sender, EventArgs e)
    {
      if (!Engine.m_EventOk || Engine.m_Locked || Engine.amMoving)
        return;
      Engine.m_ClickSender = sender;
      Engine.m_ClickArgs = e;
      Engine.m_xClick = Engine.m_xMouse;
      Engine.m_yClick = Engine.m_yMouse;
      Engine.m_ClickList = Gumps.FindListForSingleClick(Engine.m_xMouse, Engine.m_yMouse);
      Engine.m_ClickTimer.Stop();
      Engine.m_ClickTimer.Start(false);
    }

    public static void Click(object sender, EventArgs e)
    {
      if (!Engine.m_EventOk || Engine.m_Locked || Engine.amMoving)
        return;
      Engine.m_LastDown = -1;
      int TileX = 0;
      int TileY = 0;
      Renderer.ResetHitTest();
      ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xClick, Engine.m_yClick, ref TileX, ref TileY);
      if (tileFromXy == null || TargetManager.IsActive)
        return;
      if (tileFromXy.GetType() == typeof (DynamicItem))
        ((DynamicItem) tileFromXy).m_Item.OnSingleClick();
      else if (tileFromXy.GetType() == typeof (MobileCell))
        ((MobileCell) tileFromXy).m_Mobile.OnSingleClick();
      else if (tileFromXy.GetType() == typeof (StaticItem))
      {
        string Message = Localization.GetString(1020000 + ((int) ((StaticItem) tileFromXy).ID & 16383)).Trim();
        if (Message.Length <= 0)
          return;
        World.AddStaticMessage(((StaticItem) tileFromXy).Serial, Message);
      }
      else
      {
        if (!(tileFromXy.GetType() == typeof (LandTile)) || Gumps.Drag == null || !(Gumps.Drag.GetType() == typeof (GDraggedItem)))
          return;
        GDraggedItem gdraggedItem = (GDraggedItem) Gumps.Drag;
        Network.Send((Packet) new PDropItem(gdraggedItem.Item.Serial, TileX, TileY, (int) (sbyte) ((int) tileFromXy.Z + (int) tileFromXy.Height), -1));
        Gumps.Destroy((Gump) gdraggedItem);
      }
    }

    public static void SendMovementRequest(int dir, int x, int y, int z, TimeSpan speed)
    {
      uint key = Engine._movementKeys.Count > 0 ? Engine._movementKeys.Pop() : 0U;
      Network.Send((Packet) new PMoveRequest((int) (byte) dir, (int) (byte) Engine.m_Sequence, key, x, y, z, speed));
      ++Engine.m_WalkReq;
      ++Engine.m_Sequence;
      if (Engine.m_Sequence != 256)
        return;
      Engine.m_Sequence = 1;
    }

    public static void ChangeDir(Direction dir)
    {
      int dir1 = (int) Engine.GetWalkDirection(dir);
      Mobile player = World.Player;
      if (player == null || ((int) player.Direction & 7) == (dir1 & 7))
        return;
      player.Direction = (byte) dir1;
      Engine.SendMovementRequest(dir1, player.X, player.Y, player.Z, TimeSpan.FromSeconds(0.1));
    }

    public static void StringQueryOkay_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Dialog") || !Sender.HasTag("Serial") || (!Sender.HasTag("Type") || !Sender.HasTag("Text")))
        return;
      Gumps.Destroy((Gump) Sender.GetTag("Dialog"));
      Network.Send((Packet) new PStringQueryResponse((int) Sender.GetTag("Serial"), (short) Sender.GetTag("Type"), ((GTextBox) Sender.GetTag("Text")).String));
    }

    public static void StringQueryCancel_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Dialog") || !Sender.HasTag("Serial") || !Sender.HasTag("Type"))
        return;
      Gumps.Destroy((Gump) Sender.GetTag("Dialog"));
      Network.Send((Packet) new PStringQueryCancel((int) Sender.GetTag("Serial"), (short) Sender.GetTag("Type")));
    }

    public static void DestroyGump_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Gump"))
        return;
      Gump g = (Gump) Sender.GetTag("Gump");
      if (g == null)
        return;
      Gumps.Destroy(g);
    }

    public static void DestroyDialogShowAcctLogin_OnClick(Gump Sender)
    {
      World.Clear();
      Gump g = !Sender.HasTag("Dialog") ? Sender.Parent : (Gump) Sender.GetTag("Dialog");
      if (g == null)
        return;
      Gumps.Destroy(g);
      Network.Disconnect();
      Engine.ShowAcctLogin();
    }

    public static void HuePicker_OnHueSelect(int Hue, Gump Sender)
    {
      if (!Sender.HasTag("Dialog") || !Sender.HasTag("Item Art") || !Sender.HasTag("ItemID"))
        return;
      Gump gump = (Gump) Sender.GetTag("Dialog");
      Gumps.Destroy((Gump) Sender.GetTag("Item Art"));
      GItemArt gitemArt = new GItemArt(183, 3, (int) Sender.GetTag("ItemID"), Hues.GetItemHue((int) Sender.GetTag("ItemID"), Hue));
      gitemArt.X += (58 - (gitemArt.Image.xMax - gitemArt.Image.xMin)) / 2 - gitemArt.Image.xMin;
      gitemArt.Y += (82 - (gitemArt.Image.yMax - gitemArt.Image.yMin)) / 2 - gitemArt.Image.yMin;
      gump.Children.Add((Gump) gitemArt);
      Sender.SetTag("Item Art", (object) gitemArt);
    }

    public static void HuePickerSlider_OnValueChange(double Value, double Old, Gump Sender)
    {
      if (!Sender.HasTag("Hue Picker"))
        return;
      GHuePicker ghuePicker = (GHuePicker) Sender.GetTag("Hue Picker");
      if (ghuePicker == null)
        return;
      ghuePicker.Brightness = (int) Value;
    }

    public static void HuePickerPicker_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Hue Picker") || !Sender.HasTag("Brightness Bar"))
        return;
      GHuePicker picker = (GHuePicker) Sender.GetTag("Hue Picker");
      GBrightnessBar bar = (GBrightnessBar) Sender.GetTag("Brightness Bar");
      if (picker == null || bar == null)
        return;
      TargetManager.Client = (ClientTargetHandler) new HuePickerTargetHandler(picker, bar);
    }

    public static void HuePickerOk_OnClick(Gump Sender)
    {
      if (!Sender.HasTag("Dialog") || !Sender.HasTag("Hue Picker") || (!Sender.HasTag("Serial") || !Sender.HasTag("Relay")))
        return;
      Gump g = (Gump) Sender.GetTag("Dialog");
      if (g == null)
        return;
      GHuePicker ghuePicker = (GHuePicker) Sender.GetTag("Hue Picker");
      if (ghuePicker == null)
      {
        Gumps.Destroy(g);
      }
      else
      {
        Network.Send((Packet) new PSelectHueResponse((int) Sender.GetTag("Serial"), (short) Sender.GetTag("Relay"), (short) ghuePicker.Hue));
        Gumps.Destroy(g);
      }
    }

    public static void Help_OnClick(Gump Sender)
    {
      Engine.OpenHelp();
    }

    public static void Journal_OnClick(Gump Sender)
    {
      Engine.OpenJournal();
    }

    public static void Skills_OnClick(Gump Sender)
    {
      Engine.OpenSkills();
    }

    public static void Status_OnClick(Gump Sender)
    {
      Mobile mobile = World.FindMobile((int) Sender.GetTag("Serial"));
      if (mobile == null)
        return;
      mobile.QueryStats();
      mobile.OpenStatus(false);
    }

    public static void LogOut_OnClick(Gump Sender)
    {
      Engine.Quit();
    }

    public static void LogOut_YesNo(Gump sender, bool response)
    {
      if (!response)
        return;
      Engine.m_Ingame = false;
      Network.Disconnect();
      Engine.ShowAcctLogin();
    }

    public static void AttackModeToggle_OnClick(Gump Sender)
    {
      Network.Send((Packet) new PSetWarMode(!World.Player.Flags[MobileFlag.Warmode], (short) 32, (byte) 0));
    }

    public static void Quit_OnClick(Gump Sender)
    {
      Engine.exiting = true;
    }

    public static void Repeat()
    {
      if (Engine.m_LastCommand == null || Engine.m_LastCommand.Length <= 0)
        return;
      Engine.commandEntered(Engine.m_LastCommand);
    }

    public static void MouseDown(object sender, MouseEventArgs e)
    {
      if (!Engine.m_EventOk || e == null)
        return;
      Engine.m_LastMouseArgs = e;
      Engine.m_xMouse = e.X;
      Engine.m_yMouse = e.Y;
      if (!Playback.Active && Engine.m_Ingame && (e.Button == MouseButtons.Middle && Macros.Start((Keys) 69634)) || Gumps.MouseDown(e.X, e.Y, e.Button))
        return;
      if (!Engine.m_Locked && (e.Button & MouseButtons.Right) == MouseButtons.Right)
      {
        Engine.movingDir = Engine.GetDirection(e.X, e.Y, ref Engine.m_dMouse);
        Engine.amMoving = true;
      }
      else
      {
        if ((e.Button & MouseButtons.Left) != MouseButtons.Left || (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
          return;
        int TileX = 0;
        int TileY = 0;
        GContextMenu.Close();
        Renderer.ResetHitTest();
        ICell tileFromXy = Renderer.FindTileFromXY(e.X, e.Y, ref TileX, ref TileY, false);
        if (tileFromXy != null && tileFromXy.GetType() == typeof (MobileCell))
        {
          Network.Send((Packet) new PPopupRequest((MobileCell) tileFromXy));
        }
        else
        {
          if (tileFromXy == null || !(tileFromXy.CellType == typeof (DynamicItem)))
            return;
          Network.Send((Packet) new PPopupRequest(((DynamicItem) tileFromXy).m_Item));
        }
      }
    }

    public static void OpenRadar()
    {
      GRadar.Open();
    }

    public static void OpenJournal()
    {
      if (Engine.m_JournalOpen)
        return;
      Engine.m_JournalGump = new GJournal();
      Engine.m_JournalOpen = true;
      Gumps.Desktop.Children.Add((Gump) Engine.m_JournalGump);
    }

    public static void OpenHelp()
    {
      Network.Send((Packet) new PRequestHelp());
    }

    public static void OpenSkills()
    {
      if (Engine.m_SkillsOpen)
        return;
      Network.Send((Packet) new PQuerySkills());
      Engine.PingRequest(false);
      Engine.m_SkillsOpen = true;
      Engine.m_SkillsGump = new GSkills();
      Gumps.Desktop.Children.Add((Gump) Engine.m_SkillsGump);
    }

    public static void OpenStatus()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      player.QueryStats();
      player.BigStatus = true;
      player.OpenStatus(false);
    }

    public static void OpenSpellbook(int num)
    {
      Network.Send((Packet) new POpenSpellbook(num));
    }

    public static void OpenBackpack()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      backpack.Use();
    }

    public static void OpenPaperdoll()
    {
      Network.Send((Packet) new POpenPaperdoll());
    }

    public static void Quit()
    {
      GLogOutQuery.Display();
    }

    public static void AllNames()
    {
      if (Engine.m_AllNames != null && !Engine.m_AllNames.ElapsedReset())
        return;
      Engine.m_AllNames = new TimeDelay(1f);
      Mobile player = World.Player;
      if (player == null)
        return;
      foreach (Mobile mobile in World.Mobiles.Values)
      {
        if (mobile.Visible && !mobile.Player && World.InRange((IPoint2D) mobile))
          mobile.Look();
      }
      if (!TargetManager.IsActive && !player.Ghost && (!player.Flags[MobileFlag.Hidden] && !player.Meditating))
      {
        using (ScratchList<Item> scratchList = new ScratchList<Item>())
        {
          List<Item> objList = scratchList.Value;
          foreach (Item obj in World.Items.Values)
          {
            if (obj.Visible && (obj.IsCorpse || obj.IsBones) && World.InRange((IPoint2D) obj))
            {
              obj.Look();
              if (obj.InRange((IPoint2D) player, 2))
                objList.Add(obj);
            }
          }
          if (objList.Count <= 0)
            return;
          objList.Sort((Comparison<Item>) ((x, y) => PlayerDistanceSorter.Comparer.Compare((object) x, (object) y)));
          objList[0].Use();
        }
      }
      else
      {
        foreach (Item obj in World.Items.Values)
        {
          if (obj.Visible && (obj.IsCorpse || obj.IsBones) && World.InRange((IPoint2D) obj))
            obj.Look();
        }
      }
    }

    public static Item FindItem(int[] itemIDs)
    {
      Mobile player = World.Player;
      if (player == null || player.Ghost)
        return (Item) null;
      Item backpack = player.Backpack;
      if (backpack != null)
        return backpack.FindItem((IItemValidator) new ItemIDValidator(itemIDs));
      return (Item) null;
    }

    public static Item[] FindItems(int[] itemIDs)
    {
      Mobile player = World.Player;
      if (player == null || player.Ghost)
        return (Item[]) null;
      Item backpack = player.Backpack;
      if (backpack != null)
        return backpack.FindItems((IItemValidator) new ItemIDValidator(itemIDs));
      return (Item[]) null;
    }

    public static Item FindPotion(PotionType type)
    {
      return Engine.FindItem(new int[1]{ (int) (3846 + type) });
    }

    public static bool UsePotion(PotionType type)
    {
      Item potion = Engine.FindPotion(type);
      if (potion == null)
        return false;
      if (type != PotionType.Yellow || !(DateTime.Now < Engine.m_NextHealPotion))
        return potion.Use();
      if (Engine.m_HealSpam + TimeSpan.FromSeconds(0.5) < DateTime.Now)
      {
        World.Player.AddTextMessage("", Localization.GetString(500235), Engine.DefaultFont, Hues.Load(34), false);
        Engine.m_HealSpam = DateTime.Now;
      }
      return true;
    }

    public static void OnHealPotion()
    {
      Engine.m_NextHealPotion = DateTime.Now + TimeSpan.FromSeconds(10.0);
    }

    public static void KeyUp(KeyEventArgs e)
    {
      if (!Engine.m_EventOk || Playback.Active || (!Engine.m_Ingame || World.Player == null) || (e.Alt || e.Control || (e.Shift || e.KeyCode != Keys.Tab)))
        return;
      e.Handled = Network.Send((Packet) new PSetWarMode(false, (short) 32, (byte) 0));
    }

    public static void CancelClick()
    {
      Engine.m_ClickTimer.Stop();
    }

    public static bool AttackLast()
    {
      Mobile lastOffensiveTarget = TargetManager.LastOffensiveTarget;
      if (lastOffensiveTarget != null)
        return lastOffensiveTarget.Attack();
      return false;
    }

    public static void CountAmmo()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      int[] numArray = new int[2]{ 3903, 7163 };
      string[] strArray = new string[2]{ "Arrows", "Bolts" };
      for (int index1 = 0; index1 < numArray.Length; ++index1)
      {
        Item[] items = backpack.FindItems((IItemValidator) new ItemIDValidator(new int[1]{ numArray[index1] }));
        int num = 0;
        for (int index2 = 0; index2 < items.Length; ++index2)
          num += (int) (ushort) items[index2].Amount;
        Engine.AddTextMessage(strArray[index1] + ": " + (object) num, Engine.DefaultFont, num < 5 ? Hues.Load(34) : Engine.DefaultHue);
      }
    }

    public static void CountReagents()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      Item backpack = player.Backpack;
      if (backpack == null)
        return;
      Reagent[] reagents = Spells.Reagents;
      for (int index1 = 0; index1 < reagents.Length; ++index1)
      {
        Item[] items = backpack.FindItems((IItemValidator) new ItemIDValidator(new int[1]{ reagents[index1].ItemID }));
        int num = 0;
        for (int index2 = 0; index2 < items.Length; ++index2)
          num += (int) (ushort) items[index2].Amount;
        Engine.AddTextMessage(reagents[index1].Name + ": " + (object) num, Engine.DefaultFont, num < 5 ? Hues.Load(34) : Engine.DefaultHue);
      }
    }

    public static bool SmartPotion()
    {
      return Engine.SmartPotion(100);
    }

    public static bool SmartPotion(int perc)
    {
      Mobile player = World.Player;
      if (player != null)
      {
        if (player.Ghost)
        {
          Engine.AddTextMessage("A potion can not help you at this point.");
        }
        else
        {
          if (player.IsPoisoned)
          {
            if (!Engine.UsePotion(PotionType.Orange))
            {
              Engine.AddTextMessage("You do not have any orange potions!", Engine.DefaultFont, Hues.Load(34));
              return false;
            }
          }
          else if (player.MaximumHitPoints > 0 && player.CurrentHitPoints * 100 / player.MaximumHitPoints < perc && !Engine.UsePotion(PotionType.Yellow))
          {
            Engine.AddTextMessage("You do not have any yellow potions!", Engine.DefaultFont, Hues.Load(34));
            return false;
          }
          return true;
        }
      }
      return false;
    }

    public static bool BandageSelf()
    {
      Mobile player = World.Player;
      if (player != null)
      {
        if (player.Ghost)
          Engine.AddTextMessage("You are dead.");
        else if (player.CurrentHitPoints == player.MaximumHitPoints && !player.IsPoisoned)
        {
          Engine.AddTextMessage("You do not need to be bandaged.");
        }
        else
        {
          Item backpack = player.Backpack;
          if (backpack == null)
          {
            Engine.AddTextMessage("You do not have a backpack.");
          }
          else
          {
            Item[] items = backpack.FindItems((IItemValidator) new ItemIDValidator(new int[2]{ 3617, 3817 }));
            if (items.Length <= 0)
            {
              Engine.AddTextMessage("You have no bandages!", Engine.DefaultFont, Hues.Load(34));
            }
            else
            {
              Array.Sort((Array) items, AmountSorter.Comparer);
              int num1 = 0;
              for (int index = 0; index < items.Length; ++index)
                num1 += (int) (ushort) items[index].Amount;
              new Engine.BandageContext(items[0], player).Enqueue();
              int num2 = num1 - 1;
              if (num2 == 0)
                Engine.AddTextMessage("That was your last bandage!", Engine.DefaultFont, Hues.Load(34));
              else if (num2 <= 5)
                Engine.AddTextMessage(string.Format("You are running very low on bandages! There are {0} remaining.", (object) num2), Engine.DefaultFont, Hues.Load(34));
              else if (num2 <= 10)
                Engine.AddTextMessage(string.Format("You are running low on bandages. There are {0} remaining.", (object) num2), Engine.DefaultFont, Hues.Load(34));
              else
                Engine.AddTextMessage(string.Format("You have {0} bandages remaining.", (object) num2));
              return true;
            }
          }
        }
      }
      return false;
    }

    public static void KeyDown(object sender, KeyEventArgs e)
    {
      if (!Engine.m_EventOk || Playback.Active)
        return;
      if (Engine.m_Ingame && Macros.Start(e.KeyCode) && (e.Alt || e.Control || !Preferences.Current.Options.KeyPassthrough))
      {
        e.Handled = true;
      }
      else
      {
        Keys keyCode = e.KeyCode;
        bool shift = e.Shift;
        bool control = e.Control;
        bool alt = e.Alt;
        if (Engine.m_Ingame && World.Player != null && (!alt && !control) && (!shift && keyCode == Keys.Tab && Gumps.TextFocus == null))
          e.Handled = Network.Send((Packet) new PSetWarMode(true, (short) 32, (byte) 0));
        else
          e.Handled = false;
      }
    }

    public static void AddToJournal(JournalEntry je)
    {
      if (Engine.m_JournalGump != null)
        Engine.m_JournalGump.OnEntryAdded();
      Engine.m_Journal.Add((object) je);
    }

    public static void MouseMove(object sender, MouseEventArgs e)
    {
      if (!Engine.m_EventOk || e == null)
        return;
      Engine.m_LastMouseArgs = e;
      Engine.m_MouseMoved = true;
      if (Engine.m_xMouse != e.X || Engine.m_yMouse != e.Y)
        Engine.m_Redraw = true;
      Engine.m_xMouse = e.X;
      Engine.m_yMouse = e.Y;
    }

    private static void PopupDelay_OnTick(Timer t)
    {
      GObjectProperties.Display(t.GetTag("object"));
      t.Stop();
      Engine.m_PopupDelay = (Timer) null;
    }

    public static void MouseMoveQueue()
    {
      if (!Engine.m_EventOk)
        return;
      MouseEventArgs mouseEventArgs = Engine.m_LastMouseArgs;
      Engine.m_MouseMoved = false;
      Engine.pointingDir = Engine.GetDirection(mouseEventArgs.X, mouseEventArgs.Y, ref Engine.m_dMouse);
      if (Engine.m_xMouse != mouseEventArgs.X || Engine.m_yMouse != mouseEventArgs.Y)
        Engine.m_Redraw = true;
      Engine.m_xMouse = mouseEventArgs.X;
      Engine.m_yMouse = mouseEventArgs.Y;
      if (Gumps.MouseMove(mouseEventArgs.X, mouseEventArgs.Y, mouseEventArgs.Button))
        return;
      if (!Engine.m_Locked && Engine.amMoving)
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
        Engine.movingDir = Engine.pointingDir;
      }
      else if (Engine.amMoving && Engine.m_Ingame)
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
      }
      else if (Gumps.Drag != null)
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
      }
      else if (mouseEventArgs.Button == MouseButtons.None && World.Serial != 0)
      {
        if (!Engine.ServerFeatures.AOS)
          return;
        int TileX = 0;
        int TileY = 0;
        ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY, true);
        Engine.m_Highlight = !World.Player.Flags[MobileFlag.Warmode] || tileFromXy == null || !(tileFromXy.CellType == typeof (MobileCell)) ? (object) null : (object) ((MobileCell) tileFromXy).m_Mobile;
        if (tileFromXy is DynamicItem)
        {
          Item obj = ((DynamicItem) tileFromXy).m_Item;
          if (obj.IsMovable)
          {
            if (obj.PropertyList == null)
            {
              obj.QueryProperties();
              GObjectProperties.Hide();
              if (Engine.m_PopupDelay != null)
                Engine.m_PopupDelay.Stop();
              Engine.m_PopupDelay = (Timer) null;
            }
            else
            {
              if (GObjectProperties.Instance != null && GObjectProperties.Instance.Object == obj)
                return;
              if (Engine.m_PopupDelay == null)
              {
                Engine.m_PopupDelay = new Timer(new OnTick(Engine.PopupDelay_OnTick), 250);
                Engine.m_PopupDelay.SetTag("object", (object) obj);
                Engine.m_PopupDelay.Start(false);
              }
              else
                Engine.m_PopupDelay.SetTag("object", (object) obj);
            }
          }
          else
          {
            GObjectProperties.Hide();
            if (Engine.m_PopupDelay != null)
              Engine.m_PopupDelay.Stop();
            Engine.m_PopupDelay = (Timer) null;
          }
        }
        else if (tileFromXy is MobileCell)
        {
          Mobile mobile = ((MobileCell) tileFromXy).m_Mobile;
          if (mobile.PropertyList == null)
          {
            mobile.QueryProperties();
            GObjectProperties.Hide();
            if (Engine.m_PopupDelay != null)
              Engine.m_PopupDelay.Stop();
            Engine.m_PopupDelay = (Timer) null;
          }
          else
          {
            if (GObjectProperties.Instance != null && GObjectProperties.Instance.Object == mobile)
              return;
            if (Engine.m_PopupDelay == null)
            {
              Engine.m_PopupDelay = new Timer(new OnTick(Engine.PopupDelay_OnTick), 250);
              Engine.m_PopupDelay.SetTag("object", (object) mobile);
              Engine.m_PopupDelay.Start(false);
            }
            else
              Engine.m_PopupDelay.SetTag("object", (object) mobile);
          }
        }
        else
        {
          GObjectProperties.Hide();
          if (Engine.m_PopupDelay != null)
            Engine.m_PopupDelay.Stop();
          Engine.m_PopupDelay = (Timer) null;
        }
      }
      else if (mouseEventArgs.Button == MouseButtons.Left && Engine.m_Ingame)
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
        if (Engine.m_LastDown > 0)
        {
          int TileX = 0;
          int TileY = 0;
          ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY, true);
          if (Engine.m_LastDown < 1073741824)
          {
            if (tileFromXy != null && !(tileFromXy.CellType != typeof (MobileCell)) && (((MobileCell) tileFromXy).m_Mobile.Serial == Engine.m_LastDown && (Engine.m_LastDownPoint ^ new Point(Engine.m_xMouse, Engine.m_yMouse)) < 2))
              return;
            Mobile mobile = World.FindMobile(Engine.m_LastDown);
            if (mobile != null)
            {
              mobile.QueryStats();
              mobile.OpenStatus(true);
            }
            Engine.m_LastDown = 0;
          }
          else
          {
            if (tileFromXy != null && !(tileFromXy.CellType != typeof (DynamicItem)) && (((DynamicItem) tileFromXy).Serial == Engine.m_LastDown && (Engine.m_LastDownPoint ^ new Point(Engine.m_xMouse, Engine.m_yMouse)) < 2))
              return;
            Mobile player = World.Player;
            if (player != null && !player.Ghost)
            {
              Item obj = World.FindItem(Engine.m_LastDown);
              if (obj != null)
              {
                Gump gump = obj.OnBeginDrag();
                if (gump.GetType() == typeof (GDragAmount))
                {
                  ((GDragAmount) gump).ToDestroy = (object) obj;
                }
                else
                {
                  obj.RestoreInfo = new RestoreInfo(obj);
                  World.Remove(obj);
                }
              }
            }
            Engine.m_LastDown = 0;
          }
        }
        else
        {
          if (Engine.m_LastDown != -1)
            return;
          int TileX = 0;
          int TileY = 0;
          Renderer.ResetHitTest();
          ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY, false);
          Engine.m_LastDownPoint = new Point(Engine.m_xMouse, Engine.m_yMouse);
          if (tileFromXy != null && tileFromXy.GetType() == typeof (MobileCell))
          {
            Engine.m_LastDown = ((MobileCell) tileFromXy).m_Mobile.Serial;
          }
          else
          {
            if (tileFromXy == null || !(tileFromXy.GetType() == typeof (DynamicItem)))
              return;
            Item obj = ((DynamicItem) tileFromXy).m_Item;
            if (obj != null)
            {
              if (obj.IsMovable)
                Engine.m_LastDown = ((DynamicItem) tileFromXy).Serial;
              else
                Engine.m_LastDown = -1;
            }
            else
              Engine.m_LastDown = -1;
          }
        }
      }
      else if (!Engine.m_Locked && Engine.amMoving && (!Engine.amMoving || !Engine.m_Ingame))
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
        Engine.amMoving = false;
      }
      else if (!Engine.m_Locked && Engine.amMoving)
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
        Engine.movingDir = Engine.pointingDir;
      }
      else
      {
        GObjectProperties.Hide();
        if (Engine.m_PopupDelay != null)
          Engine.m_PopupDelay.Stop();
        Engine.m_PopupDelay = (Timer) null;
      }
    }

    public static void Redraw()
    {
      Engine.m_Redraw = true;
    }

    public static void MouseUp(object sender, MouseEventArgs e)
    {
      if (!Engine.m_EventOk || e == null)
        return;
      Engine.m_LastMouseArgs = e;
      Engine.m_LastDown = -1;
      Engine.m_xMouse = e.X;
      Engine.m_yMouse = e.Y;
      Gump drag = Gumps.Drag;
      if (Gumps.MouseUp(e.X, e.Y, e.Button))
        return;
      if (!Engine.m_Locked && e.Button == MouseButtons.Right && (Control.MouseButtons == MouseButtons.None || Gumps.Drag != null))
        Engine.amMoving = false;
      else if (drag != null && drag.GetType() == typeof (GDraggedItem))
      {
        Renderer.ResetHitTest();
        GDraggedItem gdraggedItem = (GDraggedItem) drag;
        gdraggedItem.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        Gumps.Destroy((Gump) gdraggedItem);
        Item obj1 = gdraggedItem.Item;
        int TileX = 0;
        int TileY = 0;
        ICell tileFromXy = Renderer.FindTileFromXY(e.X, e.Y, ref TileX, ref TileY);
        if (tileFromXy != null)
        {
          if (tileFromXy.CellType == typeof (MobileCell))
            Network.Send((Packet) new PDropItem(obj1.Serial, -1, -1, 0, ((MobileCell) tileFromXy).m_Mobile.Serial));
          else if (tileFromXy.CellType == typeof (DynamicItem))
          {
            Item obj2 = ((DynamicItem) tileFromXy).m_Item;
            if (obj2.IsContainer)
              Network.Send((Packet) new PDropItem(obj1.Serial, -1, -1, 0, obj2.Serial));
            else if (obj2.IsStackable && obj1.ID == obj2.ID && (int) obj1.Hue == (int) obj2.Hue)
              Network.Send((Packet) new PDropItem(obj1.Serial, obj2.X, obj2.Y, (int) (sbyte) obj2.Z, obj2.Serial));
            else
              Network.Send((Packet) new PDropItem(obj1.Serial, obj2.X, obj2.Y, (int) (sbyte) obj2.Z, -1));
          }
          else
            Network.Send((Packet) new PDropItem(obj1.Serial, TileX, TileY, (int) (sbyte) ((int) tileFromXy.Z + (int) tileFromXy.Height), -1));
        }
      }
      else
      {
        if (!TargetManager.IsActive || drag != null || (Gumps.Drag != null || e.Button != MouseButtons.Left) || Control.MouseButtons.HasFlag((Enum) MouseButtons.Left))
          return;
        int TileX = 0;
        int TileY = 0;
        ICell tileFromXy = Renderer.FindTileFromXY(e.X, e.Y, ref TileX, ref TileY);
        if (tileFromXy != null)
        {
          if (tileFromXy is MobileCell)
            TargetManager.Target((object) ((MobileCell) tileFromXy).m_Mobile);
          else if (tileFromXy is DynamicItem)
            TargetManager.Target((object) ((DynamicItem) tileFromXy).m_Item);
          else if (tileFromXy is StaticItem)
            TargetManager.Target((object) new StaticTarget(TileX, TileY, (int) ((StaticItem) tileFromXy).m_Z, (int) ((StaticItem) tileFromXy).m_RealID, (int) ((StaticItem) tileFromXy).m_RealID, ((StaticItem) tileFromXy).m_Hue));
          else if (tileFromXy is LandTile)
            TargetManager.Target((object) new GroundTarget(TileX, TileY, (int) ((LandTile) tileFromXy).m_Z));
        }
      }
      Engine.CancelClick();
    }

    public static Direction GetDirection(int xFrom, int yFrom, int xTo, int yTo)
    {
      int num1 = xFrom - xTo;
      int num2 = yFrom - yTo;
      int num3 = (num1 - num2) * 44;
      int num4 = (num1 + num2) * 44;
      int num5 = Math.Abs(num3);
      int num6 = Math.Abs(num4);
      return (num6 >> 1) - num5 < 0 ? ((num5 >> 1) - num6 < 0 ? (num3 < 0 || num4 < 0 ? (num3 < 0 || num4 >= 0 ? (num3 >= 0 || num4 >= 0 ? Direction.North : Direction.East) : Direction.South) : Direction.West) : (num3 > 0 ? Direction.Left : Direction.Right)) : (num4 > 0 ? Direction.Up : Direction.Down);
    }

    public static Direction GetDirection(int x, int y, ref int distance)
    {
      int num1 = Engine.GameX + Engine.GameWidth / 2 - x;
      int num2 = Engine.GameY + Engine.GameHeight / 2 - y;
      int num3 = Math.Abs(num1);
      int num4 = Math.Abs(num2);
      int num5 = (int) ((double) Engine.GameWidth / (double) Engine.GameHeight * (double) num4);
      distance = (int) Math.Sqrt((double) (num1 * num1 + num5 * num5));
      if ((num4 >> 1) - num3 >= 0)
        return num2 > 0 ? Direction.Up : Direction.Down;
      if ((num3 >> 1) - num4 >= 0)
        return num1 > 0 ? Direction.Left : Direction.Right;
      if (num1 >= 0 && num2 >= 0)
        return Direction.West;
      if (num1 >= 0 && num2 < 0)
        return Direction.South;
      return num1 < 0 && num2 < 0 ? Direction.East : Direction.North;
    }

    public static Font GetFont(int id)
    {
      if (id < 0 || id >= 10)
        id = 0;
      return Engine.m_Font[id] ?? (Engine.m_Font[id] = new Font(id));
    }

    public static UnicodeFont GetUniFont(int id)
    {
      if (id < 0 || id >= 3)
        id = 1;
      return Engine.m_UniFont[id] ?? (Engine.m_UniFont[id] = new UnicodeFont(id));
    }

    public static void OnDeviceReset(object sender, EventArgs e)
    {
      ++Renderer.m_Version;
      if (Engine.m_VertexBuffer != null)
        ((DisposeBase) Engine.m_VertexBuffer).Dispose();
      Engine.m_VertexBuffer = new VertexBuffer(Engine.m_Device, 32768 * TransformedColoredTextured.StrideSize, (Usage) 520, (VertexFormat) 324, (Pool) 0);
      Engine.m_Device.SetStreamSource(0, Engine.m_VertexBuffer, 0, TransformedColoredTextured.StrideSize);
        Engine.m_Device.VertexFormat = (VertexFormat) 324;
      Capabilities capabilities = Engine.m_Device.Capabilities;
      // ISSUE: explicit reference operation
      ShaderData.DeviceVersion = ((Capabilities) @capabilities).PixelShaderVersion;
      Texture.Square = ((Enum) (object) (TextureCaps) capabilities.TextureCaps).HasFlag((Enum) (object) (TextureCaps) 32);
      Texture.Pow2 = ((Enum) (object) (TextureCaps) capabilities.TextureCaps).HasFlag((Enum) (object) (TextureCaps) 2);
      Texture.MaxTextureWidth = (int) capabilities.MaxTextureWidth;
      Texture.MaxTextureHeight = (int) capabilities.MaxTextureHeight;
      Texture.MinTextureWidth = 1;
      Texture.MinTextureHeight = 1;
      Texture.CanSysMem = ((Enum) (object) (DeviceCaps) capabilities.DeviceCaps).HasFlag((Enum) (object) (DeviceCaps) 256);
      Texture.CanVidMem = ((Enum) (object) (DeviceCaps) capabilities.DeviceCaps).HasFlag((Enum) (object) (DeviceCaps) 512);
      Texture.MaxAspect = (int) capabilities.MaxTextureAspectRatio;
      Renderer.Init(capabilities);
      Engine.m_Device.SetRenderState((RenderState) 26, false);
      Engine.m_Device.SetRenderState((RenderState) 143, false);
      Engine.m_Device.SetRenderState((RenderState) 48, false);
      Engine.m_Device.SetRenderState((RenderState) 52, false);
      Engine.m_Device.SetRenderState((RenderState) 7, true);
      Engine.m_Device.SetRenderState((RenderState) 14, true);
      Engine.m_Device.SetRenderState<Cull>((RenderState) 22, (Cull) 3);
      Engine.m_Device.SetRenderState((RenderState) 176, false);
      Engine.m_Device.SetRenderState((RenderState) 29, false);
      Engine.m_Device.SetRenderState<ShadeMode>((RenderState)9, (ShadeMode) 2);
      Engine.m_Device.SetRenderState((RenderState) 137, false);
      Engine.m_Device.SetRenderState<VertexBlend>((RenderState)151, 0);
      Engine.m_Device.SetRenderState<Blend>((RenderState)19, (Blend) 5);
      Engine.m_Device.SetRenderState<Blend>((RenderState)20, (Blend) 6);
      Engine.m_Device.SetRenderState((RenderState) 24, 1);
      Engine.m_Device.SetRenderState<Compare>((RenderState) 25, (Compare) 7);
      Engine.m_Device.SetRenderState((RenderState) 27, false);
      Engine.m_Device.SetRenderState((RenderState) 15, false);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 5, (TextureArgument) 2);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 6, (TextureArgument) 0);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 4, (TextureOperation) 4);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 2, (TextureArgument) 2);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 3, (TextureArgument) 0);
      Engine.m_Device.SetTextureStageState(0, (TextureStage) 1, (TextureOperation) 4);
      Renderer.Reset();
    }

    public static void InitDX()
    {
      Engine.m_Fullscreen = Preferences.Current.Layout.Fullscreen;
      PresentParameters presentParameters = new PresentParameters();
      presentParameters.SwapEffect = (SwapEffect) 1;
      presentParameters.EnableAutoDepthStencil = true;
      presentParameters.AutoDepthStencilFormat = (Format) 80;
      presentParameters.PresentationInterval =  (PresentInterval) int.MinValue;
      presentParameters.DeviceWindowHandle = Engine.m_Display.Handle;
      if (Engine.m_Fullscreen)
      {
        presentParameters.Windowed = false;
        ArrayList arrayList = new ArrayList();
        AdapterCollection adapters = Engine.m_Direct3D.Adapters;
        if (adapters != null && ((ReadOnlyCollection<AdapterInformation>) adapters).Count > 0)
        {
          using (IEnumerator<DisplayMode> enumerator = ((ReadOnlyCollection<DisplayMode>) ((ReadOnlyCollection<AdapterInformation>) adapters)[0].GetDisplayModes((Format) 25)).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              DisplayMode current = enumerator.Current;
              arrayList.Add((object) current);
            }
          }
          arrayList.Sort((IComparer) new Engine.DisplayModeComparer(Engine.ScreenWidth, Engine.ScreenHeight, (Format) 25));
        }
        if (arrayList.Count == 0)
          throw new Exception("No display modes found");
        DisplayMode displayMode = (DisplayMode) arrayList[0];
        Debug.Trace("Display Mode: {0}x{1}, {2}, {3}hz", (object) (int) displayMode.Width, (object) (int) displayMode.Height, (object) (Format) displayMode.Format, (object) (int) displayMode.RefreshRate);
        presentParameters.BackBufferCount =  1;
        presentParameters.SwapEffect =  (SwapEffect) 2;
        presentParameters.BackBufferFormat = displayMode.Format;
        presentParameters.BackBufferWidth = displayMode.Width;
        presentParameters.BackBufferHeight = displayMode.Height;
      }
      else
        presentParameters.Windowed = true;
      PresentParameters[] presentParametersArray = new PresentParameters[3]{ new PresentParameters((int) presentParameters.BackBufferWidth, (int) presentParameters.BackBufferHeight, (Format) presentParameters.BackBufferFormat, (int) presentParameters.BackBufferCount, (MultisampleType) presentParameters.MultiSampleType, (int) presentParameters.MultiSampleQuality, (SwapEffect) presentParameters.SwapEffect, (IntPtr) presentParameters.DeviceWindowHandle, (presentParameters.Windowed), (presentParameters.EnableAutoDepthStencil), (Format) presentParameters.AutoDepthStencilFormat, (PresentFlags) presentParameters.PresentFlags, (int) presentParameters.FullScreenRefreshRateInHz, (PresentInterval) presentParameters.PresentationInterval), new PresentParameters((int) presentParameters.BackBufferWidth, (int) presentParameters.BackBufferHeight, (Format) presentParameters.BackBufferFormat, (int) presentParameters.BackBufferCount, (MultisampleType) presentParameters.MultiSampleType, (int) presentParameters.MultiSampleQuality, (SwapEffect) presentParameters.SwapEffect, (IntPtr) presentParameters.DeviceWindowHandle,( presentParameters.Windowed), (presentParameters.EnableAutoDepthStencil), (Format) presentParameters.AutoDepthStencilFormat, (PresentFlags) presentParameters.PresentFlags, (int) presentParameters.FullScreenRefreshRateInHz, (PresentInterval) presentParameters.PresentationInterval), new PresentParameters((int) presentParameters.BackBufferWidth, (int) presentParameters.BackBufferHeight, (Format) presentParameters.BackBufferFormat, (int) presentParameters.BackBufferCount, (MultisampleType) presentParameters.MultiSampleType, (int) presentParameters.MultiSampleQuality, (SwapEffect) presentParameters.SwapEffect, (IntPtr) presentParameters.DeviceWindowHandle,(presentParameters.Windowed),(presentParameters.EnableAutoDepthStencil), (Format) presentParameters.AutoDepthStencilFormat, (PresentFlags) presentParameters.PresentFlags, (int) presentParameters.FullScreenRefreshRateInHz, (PresentInterval) presentParameters.PresentationInterval) };
      int smoothingMode = Preferences.Current.RenderSettings.SmoothingMode;
      for (int index = 0; index < 3; ++index)
      {
        MultisampleType multisampleType;
        switch ((3 + smoothingMode - index) % 3)
        {
          case 0:
            multisampleType = (MultisampleType) 0;
            break;
          case 1:
            multisampleType = (MultisampleType) 2;
            break;
          case 2:
            multisampleType = (MultisampleType) 4;
            break;
          default:
            goto default;
        }
        presentParametersArray[index].MultiSampleType = multisampleType;
      }
      Engine.m_Direct3D = new Direct3D();
      Exception innerException = (Exception) null;
      for (int index = 0; index < presentParametersArray.Length; ++index)
      {
        Engine.m_PresentParams = presentParametersArray[index];
        try
        {
          try
          {
            Engine.m_Device = new Device(Engine.m_Direct3D, 0, (DeviceType) 1, Engine.m_Display.Handle, (CreateFlags) 64, new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            break;
          }
          catch
          {
            Engine.m_Device = new Device(Engine.m_Direct3D, 0, (DeviceType) 1, Engine.m_Display.Handle, (CreateFlags) 32, new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            break;
          }
        }
        catch (Exception ex)
        {
          innerException = ex;
        }
      }
      if (Engine.m_Device == null)
        throw new ApplicationException("Unable to create Direct3D device.", innerException);
      ++Renderer.m_Version;
      Engine.OnDeviceReset((object) Engine.m_Device, (EventArgs) null);
      Debug.Trace("Fullscreen = {0}", (object) Engine.m_Fullscreen);
      Engine.m_rRender = new Rectangle(0, 0, Engine.ScreenWidth, Engine.ScreenHeight);
      if (!Texture.CanSysMem && !Texture.CanVidMem)
        throw new Exception("Device does not support textures in video memory nor system memory.");
    }

    private class BandageContext : TargetContext
    {
      public BandageContext(Item bandage, Mobile toHeal)
        : base((object) toHeal)
      {
        this.toUse = (IEntity) bandage;
        this.isManual = true;
      }
    }

    private class DictionaryComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        DictionaryEntry dictionaryEntry = (DictionaryEntry) x;
        return (int) ((DictionaryEntry) y).Key - (int) dictionaryEntry.Key;
      }
    }

    private class DisplayModeComparer : IComparer
    {
      private int m_WantWidth;
      private int m_WantHeight;
      private Format m_WantFormat;

      public DisplayModeComparer(int w, int h, Format f)
      {
        this.m_WantWidth = w;
        this.m_WantHeight = h;
        this.m_WantFormat = f;
      }

      public int Compare(object x, object y)
      {
        DisplayMode displayMode1 = (DisplayMode) x;
        DisplayMode displayMode2 = (DisplayMode) y;
        int num = Math.Abs(displayMode1.Width * displayMode1.Height - this.m_WantWidth * this.m_WantHeight) - Math.Abs(displayMode2.Width * displayMode2.Height - this.m_WantWidth * this.m_WantHeight);
        if (num != 0)
          return num;
        return (displayMode1.Format != this.m_WantFormat ? (displayMode1.Format != (Format) 25 ? (displayMode1.Format != (Format) 23 ? 3 : 2) : 1) : 0) - (displayMode2.Format != this.m_WantFormat ? (displayMode2.Format != (Format) 25 ? (displayMode2.Format != (Format) 23 ? 3 : 2) : 1) : 0);
      }
    }
  }
}
