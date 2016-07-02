// Decompiled with JetBrains decompiler
// Type: PlayUO.PacketHandlers
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Prompts;
using PlayUO.Targeting;
using Sallos.Compression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Ultima.Client;
using Ultima.Data;

namespace PlayUO
{
  public class PacketHandlers
  {
    private static int m_PathfindIndex = 20;
    private static Regex m_ArgReplace = new Regex("~(?<1>\\d+).*?~", RegexOptions.Singleline);
    private static readonly byte[] rsaCspBlob = new byte[276]{ (byte) 6, (byte) 2, (byte) 0, (byte) 0, (byte) 0, (byte) 164, (byte) 0, (byte) 0, (byte) 82, (byte) 83, (byte) 65, (byte) 49, (byte) 0, (byte) 8, (byte) 0, (byte) 0, (byte) 1, (byte) 0, (byte) 1, (byte) 0, (byte) 125, (byte) 138, (byte) 16, (byte) 112, (byte) 214, (byte) 118, (byte) 172, (byte) 65, (byte) 223, (byte) 141, (byte) 198, (byte) 212, (byte) 79, (byte) 244, (byte) 167, (byte) 164, (byte) 245, (byte) 30, (byte) 201, (byte) 229, (byte) 157, (byte) 63, (byte) 165, (byte) 76, (byte) 100, (byte) 64, (byte) 153, (byte) 156, (byte) 186, (byte) 169, (byte) 52, (byte) 116, byte.MaxValue, (byte) 193, (byte) 158, (byte) 212, (byte) 26, (byte) 24, (byte) 125, (byte) 16, (byte) 90, (byte) 234, (byte) 0, (byte) 90, (byte) 167, (byte) 18, (byte) 31, (byte) 78, (byte) 29, (byte) 135, (byte) 242, (byte) 9, (byte) 83, (byte) 89, (byte) 222, (byte) 167, (byte) 94, (byte) 82, (byte) 97, (byte) 18, (byte) 170, (byte) 179, (byte) 77, (byte) 250, (byte) 66, (byte) 202, (byte) 13, (byte) 188, (byte) 133, (byte) 70, (byte) 127, (byte) 70, (byte) 35, (byte) 218, (byte) 240, (byte) 139, (byte) 140, (byte) 25, (byte) 112, (byte) 176, (byte) 133, (byte) 235, (byte) 179, (byte) 226, (byte) 163, (byte) 197, (byte) 228, (byte) 60, (byte) 219, (byte) 48, (byte) 223, (byte) 84, (byte) 169, (byte) 59, (byte) 189, (byte) 8, (byte) 146, (byte) 54, (byte) 227, (byte) 189, (byte) 192, (byte) 169, (byte) 141, (byte) 178, (byte) 92, (byte) 47, (byte) 115, (byte) 180, (byte) 130, (byte) 210, (byte) 86, (byte) 198, (byte) 213, (byte) 97, (byte) 106, (byte) 13, (byte) 133, (byte) 24, (byte) 42, (byte) 202, (byte) 155, (byte) 11, (byte) 99, (byte) 221, (byte) 91, (byte) 63, (byte) 159, (byte) 149, (byte) 227, (byte) 24, (byte) 153, (byte) 204, (byte) 222, (byte) 33, (byte) 60, (byte) 241, (byte) 10, (byte) 31, (byte) 22, (byte) 237, (byte) 35, (byte) 46, (byte) 58, (byte) 55, (byte) 93, (byte) 206, (byte) 96, (byte) 8, (byte) 45, (byte) 210, (byte) 254, (byte) 38, (byte) 6, (byte) 191, (byte) 43, (byte) 170, (byte) 210, (byte) 221, (byte) 77, (byte) 100, (byte) 188, (byte) 59, (byte) 56, (byte) 170, (byte) 220, (byte) 109, (byte) 212, (byte) 35, (byte) 167, (byte) 122, (byte) 85, (byte) 81, (byte) 241, (byte) 225, (byte) 180, (byte) 19, (byte) 143, (byte) 135, (byte) 214, (byte) 56, (byte) 61, (byte) 102, (byte) 179, (byte) 142, (byte) 106, (byte) 113, (byte) 218, (byte) 210, (byte) 143, (byte) 234, (byte) 81, (byte) 214, (byte) 17, (byte) 201, (byte) 32, (byte) 149, (byte) 15, (byte) 149, (byte) 112, (byte) 19, (byte) 231, (byte) 16, (byte) 126, (byte) 15, (byte) 207, (byte) 184, (byte) 248, (byte) 100, (byte) 214, (byte) 174, (byte) 131, (byte) 180, (byte) 167, (byte) 178, (byte) 237, (byte) 62, (byte) 249, (byte) 39, (byte) 178, (byte) 68, (byte) 188, (byte) 89, (byte) 105, (byte) 145, (byte) 122, (byte) 46, (byte) 210, (byte) 172, (byte) 133, (byte) 149, (byte) 251, (byte) 207, (byte) 71, (byte) 248, (byte) 38, (byte) 170, (byte) 140, (byte) 183, (byte) 79, (byte) 115, (byte) 129, (byte) 5, (byte) 41, (byte) 5, (byte) 0, (byte) 168, (byte) 139, (byte) 94, (byte) 237, (byte) 69, (byte) 56, (byte) 237, (byte) 125, (byte) 79, byte.MaxValue, (byte) 167 };
    private static RegionWorld[] _regionWorlds = new RegionWorld[5]{ RegionWorld.Felucca, RegionWorld.Trammel, RegionWorld.Ilshenar, RegionWorld.Malas, RegionWorld.Tokuno };
    private static string[] m_WorldNames = new string[5]{ "Felucca", "Trammel", "Ilshenar", "Malas", "Tokuno Islands" };
    private static int m_LastWorld = -1;
    private static string[] m_IPFReason = new string[5]{ "You can not pick that up.", "That is too far away.", "That is out of sight.", "That item does not belong to you. You'll have to steal it.", "You are already holding an item." };
    internal static Queue m_Sequences = new Queue();
    public static TimeSpan m_MoveDelay = TimeSpan.Zero;
    private static byte[] m_Key = new byte[16]{ (byte) 152, (byte) 91, (byte) 81, (byte) 126, (byte) 17, (byte) 12, (byte) 61, (byte) 119, (byte) 45, (byte) 40, (byte) 65, (byte) 34, (byte) 116, (byte) 173, (byte) 91, (byte) 57 };
    internal static PacketHandler[] m_Handlers = new PacketHandler[256];
    internal static PacketHandlers.EventFlags m_EventFlags;
    private static string m_BookTitle;
    private static string m_BookAuthor;
    private static string[] m_Args;
    private static DateTime m_HealStart;
    private static int m_xMapLeft;
    private static int m_xMapRight;
    private static int m_yMapTop;
    private static int m_yMapBottom;
    private static int m_xMapWidth;
    private static int m_yMapHeight;
    private static string[] m_BuyMenuNames;
    private static int[] m_BuyMenuPrices;
    private static int m_BuyMenuSerial;
    private static byte[] m_CompBuffer;
    internal static object m_CancelTarget;
    internal static DateTime m_CancelTimeout;
    internal static TargetAction m_CancelAction;

    private static PacketCallback Unhandled
    {
      get
      {
        return new PacketCallback(PacketHandlers.UnhandledStub);
      }
    }

    public static string[] IPFReason
    {
      get
      {
        return PacketHandlers.m_IPFReason;
      }
    }

    static PacketHandlers()
    {
      PacketHandlers.Register(27, 37, new PacketCallback(PacketHandlers.LoginConfirm));
      PacketHandlers.Register(85, 1, new PacketCallback(PacketHandlers.LoginComplete));
      PacketHandlers.Register(50, 2, new PacketCallback(PacketHandlers.Unk32));
      PacketHandlers.Register(28, -1, new PacketCallback(PacketHandlers.Message_ASCII));
      PacketHandlers.Register(174, -1, new PacketCallback(PacketHandlers.Message_Unicode));
      PacketHandlers.Register(193, -1, new PacketCallback(PacketHandlers.Message_Localized));
      PacketHandlers.Register(204, -1, new PacketCallback(PacketHandlers.Message_Localized_Affix));
      PacketHandlers.Register(194, -1, new PacketCallback(PacketHandlers.Prompt_Unicode));
      PacketHandlers.Register(154, -1, new PacketCallback(PacketHandlers.Prompt_ASCII));
      PacketHandlers.Register(214, -1, new PacketCallback(PacketHandlers.PropertyListContent));
      PacketHandlers.Register(17, -1, new PacketCallback(PacketHandlers.Mobile_Status));
      PacketHandlers.Register(32, 19, new PacketCallback(PacketHandlers.Mobile_Update));
      PacketHandlers.Register(119, 17, new PacketCallback(PacketHandlers.Mobile_Moving));
      PacketHandlers.Register(120, -1, new PacketCallback(PacketHandlers.Mobile_Incoming));
      PacketHandlers.Register(161, 9, new PacketCallback(PacketHandlers.Mobile_Attributes_HitPoints));
      PacketHandlers.Register(162, 9, new PacketCallback(PacketHandlers.Mobile_Attributes_Mana));
      PacketHandlers.Register(163, 9, new PacketCallback(PacketHandlers.Mobile_Attributes_Stamina));
      PacketHandlers.Register(45, 17, new PacketCallback(PacketHandlers.Mobile_Attributes));
      PacketHandlers.Register(110, 14, new PacketCallback(PacketHandlers.Mobile_Animation));
      PacketHandlers.Register(175, 13, new PacketCallback(PacketHandlers.Mobile_Death));
      PacketHandlers.Register(11, 7, new PacketCallback(PacketHandlers.Mobile_Damage));
      PacketHandlers.Register(46, 15, new PacketCallback(PacketHandlers.EquipItem));
      PacketHandlers.Register(136, 66, new PacketCallback(PacketHandlers.DisplayPaperdoll));
      PacketHandlers.Register(184, -1, new PacketCallback(PacketHandlers.DisplayProfile));
      PacketHandlers.Register(26, -1, new PacketCallback(PacketHandlers.WorldItem_1A));
      PacketHandlers.Register(243, 26, new PacketCallback(PacketHandlers.WorldItem_F3));
      PacketHandlers.Register(36, 9, new PacketCallback(PacketHandlers.Container_Open));
      PacketHandlers.Register(37, 21, new PacketCallback(PacketHandlers.Container_Item));
      PacketHandlers.Register(60, -1, new PacketCallback(PacketHandlers.Container_Items));
      PacketHandlers.Register(41, 1, new PacketCallback(PacketHandlers.Drop_Accept));
      PacketHandlers.Register(40, 5, new PacketCallback(PacketHandlers.Drop_Reject));
      PacketHandlers.Register(29, 5, new PacketCallback(PacketHandlers.DeleteObject));
      PacketHandlers.Register(33, 8, new PacketCallback(PacketHandlers.Movement_Reject));
      PacketHandlers.Register(34, 3, new PacketCallback(PacketHandlers.Movement_Accept));
      PacketHandlers.Register(124, -1, new PacketCallback(PacketHandlers.DisplayQuestionMenu));
      PacketHandlers.Register(149, 9, new PacketCallback(PacketHandlers.SelectHue));
      PacketHandlers.Register(166, -1, new PacketCallback(PacketHandlers.ScrollMessage));
      PacketHandlers.Register(171, -1, new PacketCallback(PacketHandlers.StringQuery));
      PacketHandlers.Register(176, -1, new PacketCallback(PacketHandlers.DisplayGump));
      PacketHandlers.Register(221, -1, new PacketCallback(PacketHandlers.CompressedGump));
      PacketHandlers.Register(84, 12, new PacketCallback(PacketHandlers.PlaySound));
      PacketHandlers.Register(35, 26, new PacketCallback(PacketHandlers.DragItem));
      PacketHandlers.Register(112, 28, new PacketCallback(PacketHandlers.StandardEffect));
      PacketHandlers.Register(192, 36, new PacketCallback(PacketHandlers.HuedEffect));
      PacketHandlers.Register(199, 49, new PacketCallback(PacketHandlers.ParticleEffect));
      PacketHandlers.Register(222, -1, (PacketCallback) (pvSrc =>
      {
        pvSrc.ReadInt32();
        if ((int) pvSrc.ReadByte() == 0)
          return;
        pvSrc.Trace(false);
      }));
      PacketHandlers.Register(223, -1, PacketHandlers.Unhandled);
      PacketHandlers.Register(78, 6, new PacketCallback(PacketHandlers.Light_Personal));
      PacketHandlers.Register(79, 2, new PacketCallback(PacketHandlers.Light_Global));
      PacketHandlers.Register(91, 4, new PacketCallback(PacketHandlers.GameTime));
      PacketHandlers.Register(101, 4, new PacketCallback(PacketHandlers.Weather));
      PacketHandlers.Register(109, 3, new PacketCallback(PacketHandlers.PlayMusic));
      PacketHandlers.Register(212, -1, new PacketCallback(PacketHandlers.Book_Open));
      PacketHandlers.Register(102, -1, new PacketCallback(PacketHandlers.Book_PageInfo));
      PacketHandlers.Register(216, -1, new PacketCallback(PacketHandlers.CustomizedHouseContent));
      PacketHandlers.Register(113, -1, new PacketCallback(PacketHandlers.BulletinBoard));
      PacketHandlers.Register(116, -1, new PacketCallback(PacketHandlers.ShopContent));
      PacketHandlers.Register(158, -1, new PacketCallback(PacketHandlers.SellContent));
      PacketHandlers.Register(59, -1, new PacketCallback(PacketHandlers.CloseShopDialog));
      PacketHandlers.Register(170, 5, new PacketCallback(PacketHandlers.CurrentTarget));
      PacketHandlers.Register(114, 5, new PacketCallback(PacketHandlers.WarmodeStatus));
      PacketHandlers.Register(240, -1, new PacketCallback(PacketHandlers.Custom));
      PacketHandlers.Register(108, 19, new PacketCallback(PacketHandlers.Target));
      PacketHandlers.Register(39, 2, new PacketCallback(PacketHandlers.ItemPickupFailed));
      PacketHandlers.Register(191, -1, new PacketCallback(PacketHandlers.Command));
      PacketHandlers.Register(44, 2, new PacketCallback(PacketHandlers.RequestResurrection));
      PacketHandlers.Register(185, 5, new PacketCallback(PacketHandlers.Features));
      PacketHandlers.Register(51, 2, new PacketCallback(PacketHandlers.Pause));
      PacketHandlers.Register(137, -1, new PacketCallback(PacketHandlers.CorpseEquip));
      PacketHandlers.Register(165, -1, new PacketCallback(PacketHandlers.LaunchBrowser));
      PacketHandlers.Register(47, 10, new PacketCallback(PacketHandlers.FightOccurring));
      PacketHandlers.Register(58, -1, new PacketCallback(PacketHandlers.Skills));
      PacketHandlers.Register(115, 2, new PacketCallback(PacketHandlers.PingReply));
      PacketHandlers.Register(153, 26, new PacketCallback(PacketHandlers.MultiTarget));
      PacketHandlers.Register(111, -1, new PacketCallback(PacketHandlers.SecureTrade));
      PacketHandlers.Register(186, 10, new PacketCallback(PacketHandlers.QuestArrow));
      PacketHandlers.Register(118, 16, new PacketCallback(PacketHandlers.ServerChange));
      PacketHandlers.Register(200, 2, new PacketCallback(PacketHandlers.ReviseUpdateRange));
      PacketHandlers.Register(203, 7, new PacketCallback(PacketHandlers.GQCount));
      PacketHandlers.Register(189, -1, new PacketCallback(PacketHandlers.VersionRequest_Client));
      PacketHandlers.Register(190, -1, new PacketCallback(PacketHandlers.VersionRequest_Assist));
      PacketHandlers.Register(220, 9, new PacketCallback(PacketHandlers.PropertyListHash));
      PacketHandlers.Register(218, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(188, 3, new PacketCallback(PacketHandlers.Season));
      PacketHandlers.Register(105, -1, PacketHandlers.Unhandled);
      PacketHandlers.Register(187, 9, PacketHandlers.Unhandled);
      PacketHandlers.Register(215, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(86, 11, new PacketCallback(PacketHandlers.MapCommand));
      PacketHandlers.Register(144, 19, new PacketCallback(PacketHandlers.MapWindow));
      PacketHandlers.Register(123, 2, new PacketCallback(PacketHandlers.Sequence));
      PacketHandlers.Register(23, -1, new PacketCallback(PacketHandlers.Mobile_HealthEffects));
      PacketHandlers.Register(62, 37, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(63, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(64, 201, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(65, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(66, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(67, 553, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(68, 713, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(69, 5, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(195, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(152, -1, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(178, -1, PacketHandlers.Unhandled);
      PacketHandlers.Register(56, 7, new PacketCallback(PacketHandlers.Pathfind));
      PacketHandlers.Register(209, 2, new PacketCallback(PacketHandlers.Trace));
      PacketHandlers.Register(49, -1, new PacketCallback(PacketHandlers.Packet31));
    }

    internal static void SetEvent(PacketHandlers.EventFlags eventFlag)
    {
      PacketHandlers.m_EventFlags |= eventFlag;
    }

    internal static bool CheckEvent(PacketHandlers.EventFlags eventFlag)
    {
      return (PacketHandlers.m_EventFlags & eventFlag) == eventFlag;
    }

    internal static void BeginSlice()
    {
      PacketHandlers.m_EventFlags = PacketHandlers.EventFlags.None;
    }

    internal static void FinishSlice()
    {
      if (PacketHandlers.CheckEvent(PacketHandlers.EventFlags.HealPotion))
        Engine.OnHealPotion();
      PacketHandlers.m_EventFlags = PacketHandlers.EventFlags.None;
    }

    private static void Packet31(PacketReader pvSrc)
    {
    }

    private static void Mobile_HealthEffects(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      for (ushort index = pvSrc.ReadUInt16(); (int) index > 0; --index)
        mobile.SetHealthLevel((int) pvSrc.ReadUInt16(), pvSrc.ReadByte());
    }

    private static void Unk32(PacketReader pvSrc)
    {
      Engine.AddTextMessage(pvSrc.ReadBoolean().ToString());
    }

    private static void BulletinBoard(PacketReader pvSrc)
    {
    }

    private static void VersionRequest_Client(PacketReader pvSrc)
    {
      Engine.AddTextMessage("Server is requesting the client version.", (IFont) Engine.GetFont(3), Hues.Load(34));
      Network.Send((Packet) new PClientVersion(Engine.GetVersionString()));
    }

    private static void VersionRequest_Assist(PacketReader pvSrc)
    {
      pvSrc.Trace(false);
      Engine.AddTextMessage("Server is requesting the assist version.", (IFont) Engine.GetFont(3), Hues.Load(34));
      Network.Send((Packet) new PAssistVersion(pvSrc.ReadInt32(), Engine.GetVersionString()));
    }

    private static void CustomizedHouseContent(PacketReader pvSrc)
    {
      int compressionType = (int) pvSrc.ReadByte();
      int num1 = (int) pvSrc.ReadByte();
      int serial = pvSrc.ReadInt32();
      int revision = pvSrc.ReadInt32();
      int num2 = (int) pvSrc.ReadUInt16();
      int length = (int) pvSrc.ReadUInt16();
      byte[] buffer = pvSrc.ReadBytes(length);
      Item obj = World.FindItem(serial);
      if (obj == null || obj.Multi == null || !obj.IsMulti)
        return;
      CustomMultiLoader.SetCustomMulti(serial, revision, obj.Multi, compressionType, buffer);
    }

    private static void PropertyListContent(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadUInt16();
      int serial = pvSrc.ReadInt32();
      int num2 = (int) pvSrc.ReadByte();
      int num3 = (int) pvSrc.ReadByte();
      int number1 = pvSrc.ReadInt32();
      if (num1 != 1 || num2 != 0 || num3 != 0)
        pvSrc.Trace(false);
      ObjectProperty[] array;
      using (ScratchList<ObjectProperty> scratchList = new ScratchList<ObjectProperty>())
      {
        List<ObjectProperty> objectPropertyList = scratchList.Value;
        int number2;
        while ((number2 = pvSrc.ReadInt32()) != 0)
        {
          int length = (int) pvSrc.ReadUInt16();
          string @string = Encoding.Unicode.GetString(pvSrc.ReadBytes(length));
          objectPropertyList.Add(new ObjectProperty(number2, @string));
        }
        array = objectPropertyList.ToArray();
      }
      ObjectPropertyList objectPropertyList1 = new ObjectPropertyList(serial, number1, array);
      Item obj1 = World.FindItem(serial);
      if (obj1 != null)
        obj1.PropertyID = number1;
      Mobile mobile = World.FindMobile(serial);
      if (mobile != null)
        mobile.PropertyID = number1;
      if (obj1 == null)
        return;
      object obj2 = (object) obj1;
      bool flag = false;
      Item obj3;
      for (; obj2 != null && obj2 is Item; obj2 = (object) obj3.Parent)
      {
        obj3 = (Item) obj2;
        if (obj3.Container != null && obj3.Container != null && obj3.Container.m_TradeContainer)
          flag = true;
        if (flag)
          break;
      }
      if (!flag || !(obj2 is Item))
        return;
      Item obj4 = (Item) obj2;
      if (obj4.Container == null || !(obj4.Container.Tooltip is ItemTooltip))
        return;
      GObjectProperties gobjectProperties = ((ItemTooltip) obj4.Container.Tooltip).Gump as GObjectProperties;
      if (gobjectProperties == null)
        return;
      gobjectProperties.SetList(1020000 + (obj4.ID & 16383), obj4.PropertyList);
    }

    private static void GQCount(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadInt16();
      int num2 = pvSrc.ReadInt32();
      switch (num2)
      {
        case 0:
          Engine.AddTextMessage("There are currently no calls in the global queue which you can answer.", (IFont) Engine.GetFont(3), Hues.Load(34));
          break;
        case 1:
          Engine.AddTextMessage("There is currently 1 call in the global queue which you can answer.", (IFont) Engine.GetFont(3), Hues.Load(34));
          break;
        default:
          Engine.AddTextMessage(string.Format("There are currently {0} calls in the global queue which you can answer.", (object) num2), (IFont) Engine.GetFont(3), Hues.Load(34));
          break;
      }
    }

    private static void Pathfind(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadInt16();
      int num2 = (int) pvSrc.ReadInt16();
      int num3 = (int) pvSrc.ReadInt16();
      if (--PacketHandlers.m_PathfindIndex != 1)
        return;
      PacketHandlers.m_PathfindIndex = 20;
      Engine.AddTextMessage(string.Format("Pathfind to ({0}, {1}, {2})", (object) num1, (object) num2, (object) num3), (IFont) Engine.GetFont(3), Hues.Load(946));
    }

    private static void CloseShopDialog(PacketReader pvSrc)
    {
      int num = pvSrc.ReadInt32();
      if ((int) pvSrc.ReadByte() != 0)
        pvSrc.Trace(false);
      Gumps.Destroy(Gumps.FindGumpByGUID(string.Format("GSellGump-{0}", (object) num)));
      Gumps.Destroy(Gumps.FindGumpByGUID(string.Format("GBuyGump-{0}", (object) num)));
    }

    private static void Trace(PacketReader pvSrc)
    {
      pvSrc.Trace(false);
    }

    private static void ReviseUpdateRange(PacketReader pvSrc)
    {
      World.Range = (int) pvSrc.ReadByte();
    }

    private static void SellContent(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int length = (int) pvSrc.ReadInt16();
      SellInfo[] info = new SellInfo[length];
      bool flag = false;
      for (int index = 0; index < length; ++index)
      {
        Item obj = World.WantItem(pvSrc.ReadInt32());
        info[index] = new SellInfo(obj, (int) pvSrc.ReadInt16(), (int) pvSrc.ReadUInt16(), (int) pvSrc.ReadUInt16(), (int) pvSrc.ReadUInt16(), pvSrc.ReadString((int) pvSrc.ReadUInt16()));
      }
      if (flag)
      {
        Engine.AddTextMessage("Selling items.");
        Network.Send((Packet) new PSellItems(serial, info));
      }
      else
        Gumps.Desktop.Children.Add((Gump) new GSellGump(serial, info));
    }

    private static void Season(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadByte();
      int num2 = (int) pvSrc.ReadByte();
      if (num1 > 4)
      {
        pvSrc.Trace(false);
      }
      else
      {
        if (num2 <= 1)
          return;
        pvSrc.Trace(false);
      }
    }

    internal static void Light_Global(PacketReader pvSrc)
    {
      Engine.Effects.GlobalLight = (int) pvSrc.ReadSByte();
    }

    internal static void Light_Personal(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.LightLevel = (int) pvSrc.ReadSByte();
    }

    private static void DragItem(PacketReader pvSrc)
    {
      int itemID = (int) pvSrc.ReadInt16();
      if ((int) pvSrc.ReadByte() != 0)
        pvSrc.Trace(false);
      ushort num1 = pvSrc.ReadUInt16();
      int num2 = (int) pvSrc.ReadUInt16();
      int sourceSerial = pvSrc.ReadInt32();
      int xSource = (int) pvSrc.ReadInt16();
      int ySource = (int) pvSrc.ReadInt16();
      int zSource = (int) pvSrc.ReadSByte();
      int targetSerial = pvSrc.ReadInt32();
      int xTarget = (int) pvSrc.ReadInt16();
      int yTarget = (int) pvSrc.ReadInt16();
      int zTarget = (int) pvSrc.ReadSByte();
      bool shouldDouble = Map.m_ItemFlags[itemID & 16383][(TileFlag) 2048L] && num2 > 1;
      if (itemID >= 3818 && itemID <= 3826)
      {
        int num3 = (itemID - 3818) / 3 * 3 + 3818;
        shouldDouble = false;
        itemID = num2 > 1 ? (num2 < 2 || num2 > 5 ? num3 + 2 : num3 + 1) : num3;
      }
      Engine.Effects.Add((Effect) new DragEffect(itemID, sourceSerial, xSource, ySource, zSource, targetSerial, xTarget, yTarget, zTarget, Hues.GetItemHue(itemID, (int) num1), shouldDouble));
    }

    private static void DisplayProfile(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      string header = pvSrc.ReadString();
      string footer = pvSrc.ReadUnicodeString();
      string body = pvSrc.ReadUnicodeString();
      Mobile mobile = World.FindMobile(serial);
      if (mobile == null)
        return;
      Gumps.Desktop.Children.Add((Gump) new GCharacterProfile(mobile, header, body, footer));
    }

    private static void Book_PageInfo(PacketReader pvSrc)
    {
      pvSrc.ReadInt32();
      int num = (int) pvSrc.ReadUInt16();
    }

    private static void Book_Open(PacketReader pvSrc)
    {
      pvSrc.ReadInt32();
      pvSrc.ReadBoolean();
      pvSrc.ReadBoolean();
      int num = (int) pvSrc.ReadInt16();
      int fixedLength1 = (int) pvSrc.ReadInt16();
      string str1 = pvSrc.ReadString(fixedLength1);
      int fixedLength2 = (int) pvSrc.ReadInt16();
      string str2 = pvSrc.ReadString(fixedLength2);
      PacketHandlers.m_BookTitle = str1;
      PacketHandlers.m_BookAuthor = str2;
      Engine.AddTextMessage("Books are not currently supported.");
    }

    private static void ServerChange(PacketReader pvSrc)
    {
      Engine.Multis.Clear();
      Mobile player = World.Player;
      if (player != null)
      {
        short num1 = pvSrc.ReadInt16();
        short num2 = pvSrc.ReadInt16();
        short num3 = pvSrc.ReadInt16();
        World.SetLocation((int) num1, (int) num2, (int) num3);
        player.SetLocation((int) num1, (int) num2, (int) num3);
        player.UpdateReal();
      }
      else
        pvSrc.Seek(6, SeekOrigin.Current);
    }

    private static void Mobile_Attributes(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.Refresh = true;
      mobile.MaximumHitPoints = (int) pvSrc.ReadUInt16();
      mobile.CurrentHitPoints = (int) pvSrc.ReadUInt16();
      mobile.MaximumMana = (int) pvSrc.ReadUInt16();
      mobile.CurrentMana = (int) pvSrc.ReadUInt16();
      mobile.MaximumStamina = (int) pvSrc.ReadUInt16();
      mobile.CurrentStamina = (int) pvSrc.ReadUInt16();
      mobile.Refresh = false;
    }

    private static void Message_Localized_Affix(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int num1 = (int) pvSrc.ReadInt16();
      int type = (int) pvSrc.ReadByte();
      IHue hue = Hues.Load((int) pvSrc.ReadInt16());
      IFont font = (IFont) Engine.GetUniFont((int) pvSrc.ReadInt16());
      int number = pvSrc.ReadInt32();
      int num2 = (int) pvSrc.ReadByte();
      string name = pvSrc.ReadString(30);
      string str1 = Localization.GetString(number);
      string str2 = pvSrc.ReadString();
      string str3 = pvSrc.ReadUnicodeString();
      if ((num2 & -8) != 0 || (num2 & 2) != 0 && serial > 0)
      {
        using (StreamWriter streamWriter = new StreamWriter("Message Localized Affix.log", true))
          streamWriter.WriteLine("Serial: 0x{0:X8}; Graphic: 0x{1:X4}; Type: {2}; Number: {3}; Flags: 0x{4:X2}; Name: '{5}'; Affix: '{6}'; Args: '{7}'; Text: '{8}';", (object) serial, (object) num1, (object) type, (object) number, (object) num2, (object) name, (object) str2, (object) str3, (object) str1);
      }
      if (str2.Length > 0)
      {
        switch (num2 & 1)
        {
          case 0:
            str1 += str2;
            break;
          case 1:
            str1 = str2 + str1;
            break;
        }
      }
      if (str3.Length > 0)
      {
        string[] strArray = str3.Split('\t');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index].Length > 1)
          {
            if (strArray[index].StartsWith("#"))
            {
              try
              {
                strArray[index] = Localization.GetString(Convert.ToInt32(strArray[index].Substring(1)));
              }
              catch
              {
              }
            }
          }
        }
        PacketHandlers.m_Args = strArray;
        str1 = PacketHandlers.m_ArgReplace.Replace(str1, new MatchEvaluator(PacketHandlers.ArgReplace_Eval));
      }
      if ((num2 & -8) != 0)
      {
        pvSrc.Trace(false);
        str1 = string.Format("0x{0:X2}\n{1}", (object) num2, (object) str1);
      }
      PacketHandlers.AddMessage(serial, font, hue, type, name, str1, number);
    }

    private static void QuestArrow(PacketReader pvSrc)
    {
      bool flag = pvSrc.ReadBoolean();
      short num1 = pvSrc.ReadInt16();
      short num2 = pvSrc.ReadInt16();
      int num3 = (int) pvSrc.ReadUInt32();
      if (flag)
        GQuestArrow.Activate((int) num1, (int) num2);
      else
        GQuestArrow.Stop();
    }

    private static void Drop_Accept(PacketReader pvSrc)
    {
    }

    private static void Drop_Reject(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadInt16();
      int num2 = (int) pvSrc.ReadInt16();
    }

    private static void Sequence(PacketReader pvSrc)
    {
      byte num = pvSrc.ReadByte();
      if ((int) num > 1)
        pvSrc.Trace(false);
      else if ((int) num == 0)
      {
        if (Engine.Effects.Locked)
          pvSrc.Trace(false);
        else
          Engine.Effects.Lock();
      }
      else
      {
        if ((int) num != 1)
          return;
        if (!Engine.Effects.Locked)
          pvSrc.Trace(false);
        else
          Engine.Effects.Unlock();
      }
    }

    private static string ArgReplace_Eval(Match m)
    {
      try
      {
        int index = Convert.ToInt32(m.Groups[1].Value) - 1;
        return PacketHandlers.m_Args[index];
      }
      catch
      {
        return m.Value;
      }
    }

    private static void Message_Localized(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int num1 = (int) pvSrc.ReadInt16();
      byte num2 = pvSrc.ReadByte();
      IHue hue = Hues.Load((int) pvSrc.ReadInt16());
      IFont font = (IFont) Engine.GetUniFont((int) pvSrc.ReadInt16());
      int number = pvSrc.ReadInt32();
      string name = pvSrc.ReadString(30);
      string str1 = pvSrc.ReadUnicodeLEString();
      string str2 = Localization.GetString(number);
      switch (number)
      {
        case 1010395:
        case 1042058:
        case 1042060:
        case 1049670:
        case 503253:
        case 503254:
        case 503255:
        case 503256:
        case 503258:
        case 503259:
        case 500916:
        case 500962:
        case 500963:
        case 500964:
        case 500965:
        case 500966:
        case 500967:
        case 500968:
        case 500969:
          GBandageTimer.Stop();
          break;
        case 502725:
        case 502726:
        case 502727:
        case 502728:
        case 502729:
        case 502731:
          Engine.m_Stealth = false;
          Engine.m_StealthSteps = 0;
          break;
        case 502730:
          Engine.m_Stealth = true;
          Engine.m_StealthSteps = Engine.ServerFeatures.AOS || Engine.m_ServerName == "192.65.242.134" ? (int) ((double) Engine.Skills[SkillName.Stealth].Value / 5.0) : (int) ((double) Engine.Skills[SkillName.Stealth].Value / 10.0);
          break;
        case 501846:
        case 501851:
          World.Player.Meditating = true;
          break;
        case 500134:
          World.Player.Meditating = false;
          break;
        case 500956:
        case 500957:
        case 500958:
        case 500959:
        case 500960:
          PacketHandlers.m_HealStart = DateTime.Now;
          GBandageTimer.Start();
          break;
      }
      if (str1.Length > 0)
      {
        string[] strArray = str1.Split('\t');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index].Length > 1)
          {
            if (strArray[index].StartsWith("#"))
            {
              try
              {
                strArray[index] = Localization.GetString(Convert.ToInt32(strArray[index].Substring(1)));
              }
              catch
              {
              }
            }
          }
        }
        PacketHandlers.m_Args = strArray;
        str2 = PacketHandlers.m_ArgReplace.Replace(str2, new MatchEvaluator(PacketHandlers.ArgReplace_Eval));
      }
      PacketHandlers.AddMessage(serial, font, hue, (int) num2, name, str2, number);
    }

    private static void MapCommand(PacketReader pvSrc)
    {
      pvSrc.ReadInt32();
      int num1 = (int) pvSrc.ReadByte();
      pvSrc.ReadBoolean();
      int num2 = (int) pvSrc.ReadInt16();
      int num3 = (int) pvSrc.ReadInt16();
      if (num1 != 1)
        return;
      GenericRadarTrackable trackable = GMapTracker.Trackable;
      trackable.X = PacketHandlers.m_xMapLeft + (int) ((double) (PacketHandlers.m_xMapRight - PacketHandlers.m_xMapLeft) * ((double) num2 / (double) PacketHandlers.m_xMapWidth));
      trackable.Y = PacketHandlers.m_yMapTop + (int) ((double) (PacketHandlers.m_yMapBottom - PacketHandlers.m_yMapTop) * ((double) num3 / (double) PacketHandlers.m_yMapHeight));
      trackable.Refresh();
      GRadar.RegisterTrackable((IRadarTrackable) trackable);
      Engine.AddTextMessage(string.Format("Map: ({0}, {1})", (object) trackable.X, (object) trackable.Y));
    }

    private static void MapWindow(PacketReader pvSrc)
    {
      pvSrc.ReadInt32();
      int num1 = (int) pvSrc.ReadInt16();
      int num2 = (int) pvSrc.ReadInt16();
      int num3 = (int) pvSrc.ReadInt16();
      int num4 = (int) pvSrc.ReadInt16();
      int num5 = (int) pvSrc.ReadInt16();
      int num6 = (int) pvSrc.ReadInt16();
      int num7 = (int) pvSrc.ReadInt16();
      PacketHandlers.m_xMapLeft = num2;
      PacketHandlers.m_yMapTop = num3;
      PacketHandlers.m_xMapRight = num4;
      PacketHandlers.m_yMapBottom = num5;
      PacketHandlers.m_xMapWidth = num6;
      PacketHandlers.m_yMapHeight = num7;
    }

    public static void Custom(PacketReader pvSrc)
    {
      switch (pvSrc.ReadByte())
      {
        case 0:
          PacketHandlers.Custom_Accept(pvSrc);
          break;
        case 1:
          PacketHandlers.Custom_AckPartyLocs(pvSrc);
          break;
        case 2:
          PacketHandlers.Custom_AckPartyLocsEx(pvSrc);
          break;
        case 3:
          PacketHandlers.Custom_RunebookContent(pvSrc);
          break;
        case 4:
          PacketHandlers.Custom_GuardlineData(pvSrc);
          break;
        case 5:
          PacketHandlers.Custom_Extension(pvSrc);
          break;
        default:
          pvSrc.Trace(false);
          break;
      }
    }

    private static void Custom_Extension(PacketReader reader)
    {
      byte[] numArray = reader.ReadBytes(reader.ReadInt32());
      byte[] rgbSignature = reader.ReadBytes(reader.ReadInt32());
      using (SHA1 shA1 = SHA1.Create())
      {
        byte[] hash = shA1.ComputeHash(numArray);
        using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
        {
          cryptoServiceProvider.ImportCspBlob(PacketHandlers.rsaCspBlob);
          if (!cryptoServiceProvider.VerifyHash(hash, (string) null, rgbSignature))
            throw new InvalidDataException();
        }
      }
      Assembly.Load(numArray).EntryPoint.Invoke((object) null, new object[1]);
    }

    private static void Custom_GuardlineData(PacketReader pvSrc)
    {
      int index = (int) pvSrc.ReadByte();
      if (index < 0 || index >= PacketHandlers._regionWorlds.Length)
        return;
      RegionWorld world = PacketHandlers._regionWorlds[index];
      List<Region> regionList = new List<Region>();
      while (!pvSrc.Finished)
      {
        int x = (int) pvSrc.ReadInt16();
        int y = (int) pvSrc.ReadInt16();
        int width = (int) pvSrc.ReadInt16();
        int height = (int) pvSrc.ReadInt16();
        int startZ = (int) pvSrc.ReadSByte();
        int num = (int) pvSrc.ReadSByte();
        regionList.Add(new Region(x, y, width, height, startZ, num - 1, world));
      }
      Region.GuardedRegions = regionList.ToArray();
      GRadar.Invalidate();
      Map.Invalidate();
      World.Viewport.Invalidate();
    }

    private static void Custom_RunebookContent(PacketReader pvSrc)
    {
      Item index1 = World.WantItem(pvSrc.ReadInt32());
      if (pvSrc.ReadBoolean())
        pvSrc.ReadUnicodeString();
      int num1 = (int) pvSrc.ReadByte();
      int num2 = (int) pvSrc.ReadByte();
      int num3 = (int) pvSrc.ReadByte();
      int num4 = (int) pvSrc.ReadByte();
      RunebookInfo runebookInfo = Player.Current.TravelAgent[index1];
      runebookInfo.Runes.Clear();
      for (int index2 = 0; index2 < num4; ++index2)
      {
        string str = (string) null;
        if (pvSrc.ReadBoolean())
          str = pvSrc.ReadUnicodeString();
        short num5 = pvSrc.ReadInt16();
        short num6 = pvSrc.ReadInt16();
        byte num7 = pvSrc.ReadByte();
        runebookInfo.Runes.Add(new RuneInfo(str ?? "", new Point3D((int) num5, (int) num6, 0), (int) num7));
      }
    }

    public static void Custom_AckPartyLocs(PacketReader pvSrc)
    {
      int serial;
      while ((serial = pvSrc.ReadInt32()) > 0)
      {
        Mobile mobile = World.WantMobile(serial);
        int num1 = (int) pvSrc.ReadInt16();
        int num2 = (int) pvSrc.ReadInt16();
        int num3 = (int) pvSrc.ReadByte();
        mobile.m_KUOC_X = num1;
        mobile.m_KUOC_Y = num2;
        mobile.m_KUOC_F = num3;
        GRadar.RegisterTrackable((IRadarTrackable) mobile);
      }
    }

    public static void Custom_AckPartyLocsEx(PacketReader pvSrc)
    {
      int serial;
      while ((serial = pvSrc.ReadInt32()) > 0)
      {
        Mobile mobile = World.WantMobile(serial);
        int num1 = (int) pvSrc.ReadInt16();
        int num2 = (int) pvSrc.ReadInt16();
        int num3 = (int) pvSrc.ReadByte();
        pvSrc.ReadBoolean();
        mobile.m_KUOC_X = num1;
        mobile.m_KUOC_Y = num2;
        mobile.m_KUOC_F = num3;
        GRadar.RegisterTrackable((IRadarTrackable) mobile);
        mobile.UpdateRadarExpiration();
      }
    }

    private static void Custom_Accept(PacketReader pvSrc)
    {
      int num = (int) pvSrc.ReadByte();
    }

    private static void MultiTarget(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadByte();
      Engine.m_MultiPreview = true;
      Engine.m_MultiSerial = pvSrc.ReadInt32();
      TargetManager.Server = (ServerTargetHandler) new MultiTargetHandler(Engine.m_MultiSerial);
      pvSrc.Seek(12, SeekOrigin.Current);
      Engine.m_MultiID = (int) pvSrc.ReadInt16();
      Engine.m_xMultiOffset = (int) pvSrc.ReadInt16();
      Engine.m_yMultiOffset = (int) pvSrc.ReadInt16();
      Engine.m_zMultiOffset = (int) pvSrc.ReadInt16();
      ArrayList arrayList1 = new ArrayList((ICollection) Engine.Multis.Load(Engine.m_MultiID));
      int count1 = arrayList1.Count;
      int num2 = 1000;
      int num3 = 1000;
      int num4 = -1000;
      int num5 = -1000;
      for (int index = 0; index < count1; ++index)
      {
        MultiItem multiItem = (MultiItem) arrayList1[index];
        if ((int) multiItem.X < num2)
          num2 = (int) multiItem.X;
        if ((int) multiItem.X > num4)
          num4 = (int) multiItem.X;
        if ((int) multiItem.Y < num3)
          num3 = (int) multiItem.Y;
        if ((int) multiItem.Y > num5)
          num5 = (int) multiItem.Y;
      }
      Engine.m_MultiMinX = num2;
      Engine.m_MultiMinY = num3;
      Engine.m_MultiMaxX = num4;
      Engine.m_MultiMaxY = num5;
      ArrayList arrayList2 = new ArrayList(arrayList1.Count);
      for (int index1 = num2; index1 <= num4; ++index1)
      {
        for (int index2 = num3; index2 <= num5; ++index2)
        {
          ArrayList arrayList3 = new ArrayList(8);
          int count2 = arrayList1.Count;
          int index3 = 0;
          while (index3 < count2)
          {
            MultiItem multiItem = (MultiItem) arrayList1[index3];
            if ((int) multiItem.X == index1 && (int) multiItem.Y == index2)
            {
              arrayList3.Add((object) StaticItem.Instantiate(multiItem.ItemID, (sbyte) multiItem.Z, multiItem.Flags));
              arrayList1.RemoveAt(index3);
              --count2;
            }
            else
              ++index3;
          }
          arrayList3.Sort(TileSorter.Comparer);
          int count3 = arrayList3.Count;
          for (int index4 = 0; index4 < count3; ++index4)
          {
            StaticItem staticItem = (StaticItem) arrayList3[index4];
            arrayList2.Add((object) new MultiItem()
            {
              X = (short) index1,
              Y = (short) index2,
              Z = (short) staticItem.Z,
              ItemID = (short) ((int) staticItem.ID & 16383 | 16384),
              Flags = staticItem.Serial
            });
          }
        }
      }
      Engine.m_MultiList = arrayList2;
    }

    private static void SecureTrade(PacketReader pvSrc)
    {
      byte num = pvSrc.ReadByte();
      int serial = pvSrc.ReadInt32();
      switch (num)
      {
        case 0:
          pvSrc.ReturnName = "Initiate Secure Trade";
          PacketHandlers.SecureTrade_Open(serial, pvSrc);
          break;
        case 1:
          pvSrc.ReturnName = "Close Secure Trade";
          PacketHandlers.SecureTrade_Close(serial, pvSrc);
          break;
        case 2:
          pvSrc.ReturnName = "Update Secure Trade Status";
          PacketHandlers.SecureTrade_Check(serial, pvSrc);
          break;
        default:
          pvSrc.Trace(false);
          break;
      }
    }

    private static void SecureTrade_Open(int serial, PacketReader pvSrc)
    {
      int serial1 = pvSrc.ReadInt32();
      int serial2 = pvSrc.ReadInt32();
      bool flag = pvSrc.ReadBoolean();
      Mobile player = World.Player;
      Mobile mobile = World.FindMobile(serial);
      string name1;
      string myName;
      if (player == null || (name1 = player.Name) == null || (myName = name1.Trim()).Length <= 0)
        myName = "Me";
      string theirName;
      if (flag)
      {
        theirName = pvSrc.ReadString();
      }
      else
      {
        string name2;
        if (mobile == null || (name2 = mobile.Name) == null || (theirName = name2.Trim()).Length <= 0)
          theirName = "Them";
      }
      GSecureTrade gsecureTrade = new GSecureTrade(serial1, (Gump) null, myName, theirName);
      Engine.GetUniFont(1);
      Hues.Load(1);
      IHue hue = Hues.Load(0);
      Item obj1 = World.WantItem(serial1);
      GSecureTradeCheck partner = new GSecureTradeCheck(250, 2, (Item) null, (GSecureTradeCheck) null);
      GSecureTradeCheck gsecureTradeCheck = new GSecureTradeCheck(2, 2, obj1, partner);
      gsecureTrade.Children.Add((Gump) gsecureTradeCheck);
      gsecureTrade.Children.Add((Gump) partner);
      GContainer gcontainer1 = obj1.OpenContainer(82, hue);
      gsecureTrade.m_Container = (Gump) gcontainer1;
      gcontainer1.X = 13;
      gcontainer1.Y = 33;
      gcontainer1.m_TradeContainer = true;
      gcontainer1.SetTag("Check1", (object) gsecureTradeCheck);
      gcontainer1.SetTag("Check2", (object) partner);
      gsecureTrade.Children.Add((Gump) gcontainer1);
      Item obj2 = World.WantItem(serial2);
      GContainer gcontainer2 = obj2.OpenContainer(82, hue);
      gcontainer2.X = 142;
      gcontainer2.Y = 33;
      gcontainer2.SetTag("Check1", (object) gsecureTradeCheck);
      gcontainer2.SetTag("Check2", (object) partner);
      gcontainer2.m_HitTest = false;
      gcontainer2.m_TradeContainer = true;
      gsecureTrade.Children.Add(gcontainer2.Gump);
      if (Engine.ServerFeatures.AOS)
        gsecureTrade.Tooltip = (ITooltip) new ItemTooltip(obj2);
      Gumps.Desktop.Children.Add((Gump) gsecureTrade);
    }

    private static void SecureTrade_Close(int serial, PacketReader pvSrc)
    {
      Item obj = World.FindItem(serial);
      if (obj == null || obj.Container == null)
        return;
      Gump gump = (Gump) obj.Container;
      gump.RemoveTag("Dispose");
      Gumps.Destroy(gump.Parent);
    }

    private static void SecureTrade_Check(int serial, PacketReader pvSrc)
    {
      bool flag1 = pvSrc.ReadInt32() != 0;
      bool flag2 = pvSrc.ReadInt32() != 0;
      Item obj = World.FindItem(serial);
      if (obj == null)
        return;
      obj.TradeCheck1 = flag1;
      obj.TradeCheck2 = flag2;
      if (obj.Container == null)
        return;
      Gump gump = (Gump) obj.Container;
      ((GSecureTradeCheck) gump.GetTag("Check1")).Checked = flag1;
      ((GSecureTradeCheck) gump.GetTag("Check2")).Checked = flag2;
    }

    private static void Skills_Absolute(PacketReader pvSrc, bool hasCapData)
    {
      Engine.PingReply(-1);
      Skills skills = Engine.Skills;
      int num;
      while ((num = (int) pvSrc.ReadInt16()) > 0)
      {
        Skill skill = skills[num - 1];
        if (skill != null)
        {
          skill.Value = (float) pvSrc.ReadInt16() / 10f;
          skill.Real = (float) pvSrc.ReadInt16() / 10f;
          skill.Lock = (SkillLock) pvSrc.ReadByte();
          if (hasCapData)
            pvSrc.Seek(2, SeekOrigin.Current);
          if (Engine.m_SkillsOpen && Engine.m_SkillsGump != null)
            Engine.m_SkillsGump.OnSkillChange(skill);
        }
      }
    }

    private static void Skills_Delta(PacketReader pvSrc, bool hasCapData)
    {
      Skill skill = Engine.Skills[(int) pvSrc.ReadInt16()];
      if (skill == null)
        return;
      float num1 = (float) pvSrc.ReadInt16() / 10f;
      if ((double) skill.Value != (double) num1)
      {
        float num2 = num1 - skill.Value;
        Math.Sign(num2);
        Engine.AddTextMessage(string.Format("Your skill in {0} has {1} by {2:F1}. Is it now {3:F1}.", (object) skill.Name, (double) num2 > 0.0 ? (object) "increased" : (object) "decreased", (object) Math.Abs(num2), (object) num1), (IFont) Engine.GetFont(3), Hues.Load(89));
        skill.Value = num1;
      }
      skill.Real = (float) pvSrc.ReadInt16() / 10f;
      skill.Lock = (SkillLock) pvSrc.ReadByte();
      if (hasCapData)
        pvSrc.Seek(2, SeekOrigin.Current);
      if (!Engine.m_SkillsOpen || Engine.m_SkillsGump == null)
        return;
      Engine.m_SkillsGump.OnSkillChange(skill);
    }

    private static void Skills(PacketReader pvSrc)
    {
      switch (pvSrc.ReadByte())
      {
        case 0:
          pvSrc.ReturnName = "Skills (Absolute)";
          PacketHandlers.Skills_Absolute(pvSrc, false);
          break;
        case 2:
          pvSrc.ReturnName = "Skills (Absolute, Capped)";
          PacketHandlers.Skills_Absolute(pvSrc, true);
          break;
        case 223:
          pvSrc.ReturnName = "Skills (Delta, Capped)";
          PacketHandlers.Skills_Delta(pvSrc, true);
          break;
        case byte.MaxValue:
          pvSrc.ReturnName = "Skills (Delta)";
          PacketHandlers.Skills_Delta(pvSrc, false);
          break;
        default:
          pvSrc.Trace(false);
          break;
      }
    }

    private static void FightOccurring(PacketReader pvSrc)
    {
      if ((int) pvSrc.ReadByte() != 0)
        pvSrc.Trace(false);
      Mobile mobile1 = World.FindMobile(pvSrc.ReadInt32());
      Mobile mobile2 = World.FindMobile(pvSrc.ReadInt32());
      if (mobile1 != null && !mobile1.Player)
        mobile1.QueryStats();
      if (mobile2 == null || mobile2.Player)
        return;
      mobile2.QueryStats();
    }

    private static void StringQuery(PacketReader pvSrc)
    {
      int num1 = pvSrc.ReadInt32();
      short num2 = pvSrc.ReadInt16();
      int fixedLength1 = (int) pvSrc.ReadInt16();
      string text1 = pvSrc.ReadString(fixedLength1);
      bool flag = pvSrc.ReadBoolean();
      byte num3 = pvSrc.ReadByte();
      int num4 = pvSrc.ReadInt32();
      int fixedLength2 = (int) pvSrc.ReadInt16();
      string text2 = pvSrc.ReadString(fixedLength2);
      GDragable gdragable = new GDragable(1140, 0, 0);
      gdragable.CanClose = false;
      gdragable.Modal = true;
      gdragable.X = (Engine.ScreenWidth - gdragable.Width) / 2;
      gdragable.Y = (Engine.ScreenHeight - gdragable.Height) / 2;
      GButton gbutton1 = new GButton(1147, 1149, 1148, 117, 190, new OnClick(Engine.StringQueryOkay_OnClick));
      GButton gbutton2 = new GButton(1144, 1146, 1145, 204, 190, flag ? new OnClick(Engine.StringQueryCancel_OnClick) : (OnClick) null);
      if (!flag)
        gbutton2.Enabled = false;
      GImage gimage = new GImage(1143, 60, 145);
      GWrappedLabel gwrappedLabel1 = new GWrappedLabel(text1, (IFont) Engine.GetFont(2), Hues.Load(1109), 60, 48, 272);
      GWrappedLabel gwrappedLabel2 = new GWrappedLabel(text2, (IFont) Engine.GetFont(2), Hues.Load(1109), 60, 48, 272);
      gwrappedLabel2.Y = gimage.Y - gwrappedLabel2.Height;
      GTextBox gtextBox = new GTextBox(0, false, 68, 140, gimage.Width - 8, gimage.Height, "", (IFont) Engine.GetFont(1), Hues.Load(1109), Hues.Load(1109), Hues.Load(1109));
      gtextBox.Focus();
      if ((int) num3 == 1)
        gtextBox.MaxChars = num4;
      gbutton1.SetTag("Dialog", (object) gdragable);
      gbutton1.SetTag("Serial", (object) num1);
      gbutton1.SetTag("Type", (object) num2);
      gbutton1.SetTag("Text", (object) gtextBox);
      gbutton2.SetTag("Dialog", (object) gdragable);
      gbutton2.SetTag("Serial", (object) num1);
      gbutton2.SetTag("Type", (object) num2);
      gdragable.Children.Add((Gump) gwrappedLabel1);
      gdragable.Children.Add((Gump) gwrappedLabel2);
      gdragable.Children.Add((Gump) gimage);
      gdragable.Children.Add((Gump) gtextBox);
      gdragable.Children.Add((Gump) gbutton2);
      gdragable.Children.Add((Gump) gbutton1);
      gdragable.m_CanDrag = true;
      Gumps.Desktop.Children.Add((Gump) gdragable);
    }

    private static void Prompt_Unicode(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int prompt = pvSrc.ReadInt32();
      pvSrc.ReadInt32();
      pvSrc.Seek(4, SeekOrigin.Current);
      string text = "";
      if ((int) pvSrc.ReadInt16() != 0)
      {
        pvSrc.Trace(false);
        pvSrc.Seek(-2, SeekOrigin.Current);
        text = pvSrc.ReadUnicodeLEString();
      }
      Engine.Prompt = (IPrompt) new UnicodePrompt(serial, prompt, text);
    }

    private static void Prompt_ASCII(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int prompt = pvSrc.ReadInt32();
      pvSrc.ReadInt32();
      string text = pvSrc.ReadString().Trim();
      Engine.Prompt = (IPrompt) new ASCIIPrompt(serial, prompt, text);
    }

    private static void Mobile_Death(PacketReader pvSrc)
    {
      Mobile m = World.FindMobile(pvSrc.ReadInt32());
      Item i = World.WantItem(pvSrc.ReadInt32());
      i.Query();
      if (m == null)
        return;
      while (m.Walking.Count > 0)
      {
        WalkAnimation walkAnimation = (WalkAnimation) m.Walking.Dequeue();
        m.SetLocation(walkAnimation.NewX, walkAnimation.NewY, walkAnimation.NewZ);
        m.Direction = (byte) walkAnimation.NewDir;
        walkAnimation.Dispose();
      }
      m.UpdateReal();
      m.IsMoving = false;
      i.Query();
      Mobile player = World.Player;
      if (TargetManager.Server == null && m.Notoriety != Notoriety.Innocent && (player != null && !player.Ghost) && (!player.Flags[MobileFlag.Hidden] && player.InRange((IPoint2D) m, 2) && !player.Meditating))
        i.Use();
      m.CorpseSerial = i.Serial;
      m.Update();
      i.SetLocation((Agent) World.Agent, m.X, m.Y, m.Z);
      i.Amount = (int) m.Body;
      i.ID = 8198;
      i.CorpseSerial = m.Serial;
      i.Direction = m.Direction;
      i.Update();
      m.Animation = new Animation();
      m.Animation.Action = Engine.m_Animations.ConvertAction((int) m.Body, m.Serial, m.X, m.Y, (int) m.Direction, GenericAction.Die, m);
      m.Animation.Delay = 0;
      m.Animation.Forward = true;
      m.Animation.Repeat = false;
      int priorSeen = m.LastSeen;
      m.Animation.OnAnimationEnd += (OnAnimationEnd) ((param0, param1) =>
      {
        if (m.CorpseSerial == i.Serial)
        {
          m.CorpseSerial = 0;
          m.Update();
          if (m.LastSeen == priorSeen)
            m.Delete();
        }
        i.CorpseSerial = 0;
        i.Direction = m.Direction;
        i.Update();
      });
      m.Animation.Run();
      if (!Options.Current.Screenshots || !m.Visible || (!m.HumanOrGhost || !World.InRange((IPoint2D) m)))
        return;
      string name = m.Name;
      string title;
      if (name == null || (title = name.Trim()).Length <= 0)
        return;
      int num1 = Renderer.m_Frames;
      object obj = Engine.m_Highlight;
      bool fade = GFader.Fade;
      int num2 = Options.Current.ContainerGrid ? 1 : 0;
      GFader.Fade = false;
      Engine.m_Highlight = (object) m;
      Renderer.m_Frames += 5;
      try
      {
        Renderer.ScreenShot(title);
      }
      finally
      {
        Renderer.m_Frames = num1;
        Engine.m_Highlight = obj;
        GFader.Fade = fade;
      }
      Engine.AddTextMessage("Screenshot taken.");
    }

    private static void CorpseEquip(PacketReader pvSrc)
    {
      Item obj1 = World.WantItem(pvSrc.ReadInt32());
      obj1.ClearCorpseItems();
      Layer layer;
      while ((layer = (Layer) pvSrc.ReadByte()) != Layer.Invalid)
      {
        Item obj2 = World.WantItem(pvSrc.ReadInt32());
        obj2.Layer = layer - (byte) 1;
        obj1.AddCorpseItem(obj2);
      }
    }

    private static void GameTime(PacketReader pvSrc)
    {
    }

    private static void Pause(PacketReader pvSrc)
    {
    }

    private static void Features(PacketReader pvSrc)
    {
      uint num = pvSrc.ReadUInt32();
      Engine.Features.Chat = ((int) num & 2) != 0;
      Engine.Features.LBR = ((int) num & 8) != 0;
      Engine.Features.AOS = ((int) num & 16) != 0;
    }

    private static void Container_Open(PacketReader pvSrc)
    {
      int serial1 = pvSrc.ReadInt32();
      int num1 = (int) pvSrc.ReadInt16();
      int serial2 = 0;
      if (num1 == 48)
      {
        serial2 = serial1;
        serial1 = PacketHandlers.m_BuyMenuSerial;
      }
      Item obj1 = World.WantItem(serial1);
      bool flag = false;
      int num2 = 0;
      int num3 = 0;
      if (obj1.Container != null)
      {
        GContainer container = obj1.Container;
        if (container != null && container.GumpID == num1)
        {
          flag = true;
          num2 = container.X;
          num3 = container.Y;
        }
      }
      int num4 = Engine.m_OpenedGumps++ % 20;
      if (obj1.Container != null)
        obj1.Container.Close();
      if (num1 == -1)
        obj1.QueueOpenSB = true;
      else if (num1 == 8)
      {
        Mobile mobile = World.FindMobile(serial1);
        mobile.BigStatus = true;
        mobile.OpenStatus(false);
      }
      else if (num1 == 48)
      {
        if (PacketHandlers.m_BuyMenuPrices == null || PacketHandlers.m_BuyMenuNames == null)
          return;
        List<BuyInfo> buyInfoList = new List<BuyInfo>();
        foreach (Item obj2 in obj1.Items)
        {
          if (buyInfoList.Count < PacketHandlers.m_BuyMenuPrices.Length && buyInfoList.Count < PacketHandlers.m_BuyMenuNames.Length)
          {
            string name = obj2.Serial >= 1073741824 ? PacketHandlers.m_BuyMenuNames[buyInfoList.Count] : Localization.GetString(1020000 + obj2.ID);
            buyInfoList.Add(new BuyInfo(obj2, PacketHandlers.m_BuyMenuPrices[buyInfoList.Count], name));
          }
        }
        Gumps.Desktop.Children.Add((Gump) new GBuyGump(serial2, buyInfoList.ToArray()));
        PacketHandlers.m_BuyMenuPrices = (int[]) null;
        PacketHandlers.m_BuyMenuNames = (string[]) null;
      }
      else
      {
        if (ActionContext.Active is OpenRestockContainerContext)
          return;
        Engine.Sounds.PlayContainerOpen(num1);
        GContainer gcontainer = num1 != 9 || obj1.LastTextHue == null || (obj1.LastTextHue.HueID() & (int) short.MaxValue) != 89 ? obj1.OpenContainer(num1, Hues.Default) : obj1.OpenContainer(num1, Hues.GetNotoriety(Notoriety.Innocent));
        if (flag)
        {
          gcontainer.X = num2;
          gcontainer.Y = num3;
        }
        Gumps.Desktop.Children.Add((Gump) gcontainer);
      }
    }

    private static void Container_Items(PacketReader pvSrc)
    {
      ArrayList dataStore = Engine.GetDataStore();
      int num1 = (int) pvSrc.ReadUInt16();
      if (num1 > 1000 || num1 == 0)
        return;
      bool flag = (pvSrc.Length - 5) / num1 == 20;
      for (int index = 0; index < num1; ++index)
      {
        int serial1 = pvSrc.ReadInt32();
        ushort num2 = checked ((ushort) ((int) pvSrc.ReadUInt16() + (int) pvSrc.ReadSByte()));
        ushort num3 = pvSrc.ReadUInt16();
        short num4 = pvSrc.ReadInt16();
        short num5 = pvSrc.ReadInt16();
        if (flag)
        {
          int num6 = (int) pvSrc.ReadByte();
        }
        int serial2 = pvSrc.ReadInt32();
        ushort num7 = pvSrc.ReadUInt16();
        Item obj1 = World.WantItem(serial2);
        Item obj2 = World.WantItem(serial1);
        obj2.Query();
        obj2.Flags[ItemFlag.CanMove] = true;
        obj2.ID = serial1 >= 1073741824 ? (int) num2 : ShrinkTable.ToItemId((int) num2) ?? (int) num2;
        obj2.Hue = num7;
        obj2.Amount = (int) num3;
        obj2.SetLocation((Agent) obj1, (int) num4, (int) num5, 0);
        if (obj2.Parent != null && (((Item) obj2.Parent).ID & 16383) == 8198 && obj2.PropertyList == null)
          obj2.QueryProperties();
        if (!dataStore.Contains((object) obj1))
          dataStore.Add((object) obj1);
      }
      int count = dataStore.Count;
      for (int index = 0; index < count; ++index)
      {
        Item container = (Item) dataStore[index];
        container.HasContainerContent = true;
        if (World.Player.Backpack == container)
          Player.Current.UseOnceAgent.Validate();
        if (container.QueueOpenSB)
        {
          container.QueueOpenSB = false;
          container.SpellbookGraphic = container.ID;
          container.SpellbookOffset = Spells.GetBookOffset(container.SpellbookGraphic);
          container.SpellContained = 0L;
          foreach (Item obj in container.Items)
            container.SetSpellContained(obj.Amount - container.SpellbookOffset, true);
          if (!container.OpenSB)
          {
            container.OpenSB = true;
            Spells.OpenSpellbook(container);
          }
          else
          {
            Gump gumpByGuid = Gumps.FindGumpByGUID(string.Format("Spellbook Icon #{0}", (object) container.Serial));
            if (gumpByGuid != null)
              gumpByGuid.OnDoubleClick(gumpByGuid.Width / 2, gumpByGuid.Height / 2);
          }
        }
      }
      Engine.ReleaseDataStore(dataStore);
      if (!(ActionContext.Active is OpenRestockContainerContext))
        return;
      Player.Current.RestockAgent.Invoke();
    }

    private static void Container_Item(PacketReader pvSrc)
    {
      uint num1 = pvSrc.ReadUInt32();
      ushort num2 = pvSrc.ReadUInt16();
      int num3 = (int) pvSrc.ReadByte();
      ushort num4 = pvSrc.ReadUInt16();
      ushort num5 = pvSrc.ReadUInt16();
      ushort num6 = pvSrc.ReadUInt16();
      int num7 = (int) pvSrc.ReadByte();
      uint num8 = pvSrc.ReadUInt32();
      ushort num9 = pvSrc.ReadUInt16();
      bool flag1 = num1 < 1073741824U;
      bool flag2 = num8 < 1073741824U;
      if (flag1 && flag2)
      {
        Mobile mobile = World.FindMobile((int) num1);
        if (mobile == null || !mobile.Visible)
          return;
        mobile.Update();
      }
      else
      {
        if (flag1 || flag2)
          return;
        Item obj1 = World.WantItem((int) num1);
        Item obj2 = World.WantItem((int) num8);
        obj1.Query();
        obj1.ID = (int) num2;
        obj1.Hue = num9;
        obj1.Amount = (int) num4;
        obj1.Flags[ItemFlag.CanMove] = true;
        obj1.SetLocation((Agent) obj2, (int) num5, (int) num6, 0);
        if (obj1.Parent == null || (int) num2 != 8198 || obj1.PropertyList != null)
          return;
        obj1.QueryProperties();
      }
    }

    private static void Mobile_Animation(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      int graphicId = (int) pvSrc.ReadInt16();
      int num1 = (int) pvSrc.ReadInt16();
      int num2 = (int) pvSrc.ReadInt16();
      bool flag1 = !pvSrc.ReadBoolean();
      bool flag2 = pvSrc.ReadBoolean();
      int num3 = (int) pvSrc.ReadByte();
      int action;
      switch (Engine.m_Animations.GetBodyType((int) mobile.Body))
      {
        case BodyType.Monster:
          action = graphicId % 22;
          break;
        case BodyType.Sea:
        case BodyType.Animal:
          int num4 = GraphicTranslators.Actions[0].Convert(graphicId);
          if (num4 < 0)
            return;
          action = num4 % 13;
          break;
        case BodyType.Human:
        case BodyType.Equipment:
          int num5 = GraphicTranslators.Actions[1].Convert(graphicId);
          if (num5 < 0)
            return;
          action = num5 % 35;
          break;
        default:
          return;
      }
      int direction = Engine.GetAnimDirection(mobile.Direction) & 7;
      if (!Engine.m_Animations.IsValid((int) mobile.Body, action, direction))
        return;
      Animation animation = new Animation();
      animation.Action = action;
      animation.RepeatCount = num2;
      animation.Forward = flag1;
      animation.Repeat = flag2;
      animation.Delay = num3;
      mobile.Animation = animation;
      animation.Run();
    }

    private static string EffLay(PacketHandlers.EffectLayer layer)
    {
      if (Enum.IsDefined(typeof (PacketHandlers.EffectLayer), (object) layer))
        return string.Format("EffectLayer.{0}", (object) layer);
      int num = (int) layer;
      if (num < 0)
        return string.Format("(EffectLayer)({0})", (object) num);
      return string.Format("(EffectLayer){0}", (object) num);
    }

    private static void Effect(PacketReader pvSrc, bool hasHueData, bool hasParticleData)
    {
      int num1 = (int) pvSrc.ReadByte();
      int Source = pvSrc.ReadInt32();
      int num2 = pvSrc.ReadInt32();
      int num3 = (int) pvSrc.ReadInt16();
      int xSource = (int) pvSrc.ReadInt16();
      int ySource = (int) pvSrc.ReadInt16();
      int zSource = (int) pvSrc.ReadSByte();
      int num4 = (int) pvSrc.ReadInt16();
      int num5 = (int) pvSrc.ReadInt16();
      int num6 = (int) pvSrc.ReadSByte();
      int num7 = (int) pvSrc.ReadByte();
      int duration = (int) pvSrc.ReadByte();
      int num8 = (int) pvSrc.ReadByte();
      int num9 = (int) pvSrc.ReadByte();
      pvSrc.ReadBoolean();
      bool flag = pvSrc.ReadBoolean();
      int hue = hasHueData ? pvSrc.ReadInt32() : 0;
      int num10 = hasHueData ? pvSrc.ReadInt32() : 0;
      int num11 = hasParticleData ? (int) pvSrc.ReadInt16() : 0;
      if (hasParticleData)
      {
        int num12 = (int) pvSrc.ReadInt16();
      }
      if (hasParticleData)
      {
        int num13 = (int) pvSrc.ReadInt16();
      }
      if (hasParticleData)
        pvSrc.ReadInt32();
      if (hasParticleData)
      {
        int num14 = (int) pvSrc.ReadByte();
      }
      if (hasParticleData)
      {
        int num15 = (int) pvSrc.ReadInt16();
      }
      if (num3 <= 1 && num1 != 1)
        return;
      if (num8 > 1 || num9 != 0)
        pvSrc.Trace(false);
      if (hue > 0)
        ++hue;
      Effect e;
      switch (num1)
      {
        case 0:
          e = (Effect) new MovingEffect(Source, num2, xSource, ySource, zSource, num4, num5, num6, num3, Hues.GetItemHue(num3, hue));
          ((MovingEffect) e).m_RenderMode = num10;
          ((MovingEffect) e).EffectId = num11;
          if (flag || num11 == 9501)
            e.Children.Add((Effect) new AnimatedItemEffect(num2, num4, num5, num6, 14013, Hues.GetItemHue(14027, hue), 14));
          if (num11 == 9501)
          {
            for (int index1 = 0; index1 < 3; ++index1)
            {
              MovingEffect movingEffect1 = (MovingEffect) null;
              for (int index2 = 0; index2 < 3; ++index2)
              {
                int num16 = Engine.Random.Next(4) - 3;
                int num17 = Engine.Random.Next(4) - 3;
                int num18 = 60 - Engine.Random.Next(10);
                MovingEffect movingEffect2 = new MovingEffect(-1, num2, num4 + num16, num5 + num17, num6 + num18, num4, num5, num6, num3, Hues.GetItemHue(num3, hue));
                movingEffect2.m_RenderMode = num10;
                movingEffect2.EffectId = num11;
                if (flag || num11 == 9501)
                  movingEffect2.Children.Add((Effect) new AnimatedItemEffect(num2, num4, num5, num6, 14027, Hues.GetItemHue(14013, hue), 14));
                if (movingEffect1 == null)
                  Engine.Effects.Add((Effect) movingEffect2);
                else
                  movingEffect1.Children.Add((Effect) movingEffect2);
                movingEffect1 = movingEffect2;
              }
            }
            break;
          }
          break;
        case 1:
          e = (Effect) new LightningEffect(Source, xSource, ySource, zSource, Hues.Load(hue ^ 32768));
          break;
        case 2:
          e = (Effect) new AnimatedItemEffect(xSource, ySource, zSource, num3, Hues.GetItemHue(num3, hue), duration);
          ((AnimatedItemEffect) e).m_RenderMode = num10;
          break;
        case 3:
          e = (Effect) new AnimatedItemEffect(Source, xSource, ySource, zSource, num3, Hues.GetItemHue(num3, hue), duration);
          ((AnimatedItemEffect) e).m_RenderMode = num10;
          if (num11 == 5030)
          {
            Engine.Effects.Add((Effect) new AnimatedItemEffect(Source, xSource, ySource, zSource, 14202, Hues.GetItemHue(14202, hue), 15)
            {
              m_RenderMode = num10
            });
            break;
          }
          break;
        default:
          pvSrc.Trace(false);
          return;
      }
      if (e == null)
        return;
      Engine.Effects.Add(e);
    }

    private static void StandardEffect(PacketReader pvSrc)
    {
      PacketHandlers.Effect(pvSrc, false, false);
    }

    private static void HuedEffect(PacketReader pvSrc)
    {
      PacketHandlers.Effect(pvSrc, true, false);
    }

    private static void ParticleEffect(PacketReader pvSrc)
    {
      PacketHandlers.Effect(pvSrc, true, true);
    }

    private static void UnhandledStub(PacketReader pvSrc)
    {
    }

    private static void PlayMusic(PacketReader pvSrc)
    {
      if (Preferences.Current.Music.Volume.IsMuted)
        return;
      int midiID = (int) pvSrc.ReadInt16();
      if (midiID < 0)
      {
        Music.Stop();
      }
      else
      {
        string fileName = Engine.MidiTable.Translate(midiID);
        if (fileName != null)
          Music.Play(fileName);
        else
          pvSrc.Trace(false);
      }
    }

    private static void CurrentTarget(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      Renderer.AlwaysHighlight = serial;
      if (serial == 0)
        return;
      Mobile mobile = World.FindMobile(serial);
      if (mobile == null)
        return;
      World.Opponent = mobile;
      Engine.m_LastAttacker = mobile;
      mobile.QueryStats();
    }

    private static void EquipItem(PacketReader pvSrc)
    {
      Item obj = World.WantItem(pvSrc.ReadInt32());
      ushort num1 = checked ((ushort) ((int) pvSrc.ReadUInt16() + (int) pvSrc.ReadSByte()));
      obj.Query();
      Layer layer = (Layer) pvSrc.ReadByte();
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      ushort num2 = pvSrc.ReadUInt16();
      obj.ID = (int) num1;
      obj.Hue = num2;
      obj.Layer = layer;
      obj.SetLocation((Agent) mobile, 0, 0, 0);
      mobile.EquipChanged();
    }

    private static PacketReader GetCompressedReader(PacketReader pvSrc)
    {
      if (PacketHandlers.m_CompBuffer == null)
        PacketHandlers.m_CompBuffer = new byte[4096];
      int num = pvSrc.ReadInt32();
      if (num == 0)
        return new PacketReader(PacketHandlers.m_CompBuffer, 0, 3, false, (byte) 0, "Gump Subset");
      int count = pvSrc.ReadInt32();
      if (count == 0)
        return new PacketReader(PacketHandlers.m_CompBuffer, 0, 3, false, (byte) 0, "Gump Subset");
      byte[] numArray = pvSrc.ReadBytes(num - 4);
      if (count > PacketHandlers.m_CompBuffer.Length)
        PacketHandlers.m_CompBuffer = new byte[count + 4095 & -4096];
      ZLib.Decompress(PacketHandlers.m_CompBuffer, ref count, numArray, numArray.Length);
      PacketReader packetReader = new PacketReader(PacketHandlers.m_CompBuffer, 0, count, true, (byte) 0, "Gump Subset");
      packetReader.Seek(0, SeekOrigin.Begin);
      return packetReader;
    }

    private static void CompressedGump(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int dialog = pvSrc.ReadInt32();
      int xOffset = pvSrc.ReadInt32();
      int yOffset = pvSrc.ReadInt32();
      string layout = PacketHandlers.GetCompressedReader(pvSrc).ReadString();
      string[] text = new string[pvSrc.ReadInt32()];
      pvSrc = PacketHandlers.GetCompressedReader(pvSrc);
      for (int index = 0; index < text.Length; ++index)
        text[index] = pvSrc.ReadUnicodeString((int) pvSrc.ReadUInt16());
      PacketHandlers.HandleGump(serial, dialog, xOffset, yOffset, layout, text);
    }

    private static void DisplayGump(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int dialog = pvSrc.ReadInt32();
      int xOffset = pvSrc.ReadInt32();
      int yOffset = pvSrc.ReadInt32();
      string layout = pvSrc.ReadString((int) pvSrc.ReadUInt16());
      string[] text = new string[(int) pvSrc.ReadUInt16()];
      for (int index = 0; index < text.Length; ++index)
        text[index] = pvSrc.ReadUnicodeString((int) pvSrc.ReadUInt16());
      PacketHandlers.HandleGump(serial, dialog, xOffset, yOffset, layout, text);
    }

    private static void HandleGump(int serial, int dialog, int xOffset, int yOffset, string layout, string[] text)
    {
      if (text.Length > 0 && text[0] == "Dost thou wish to step into the moongate? Continue to enter the gate, Cancel to stay here" && Options.Current.MoongateConfirmation)
      {
        Network.Send((Packet) new PGumpButton(serial, dialog, 1));
      }
      else
      {
        GServerGump.GetCachedLocation(dialog, ref xOffset, ref yOffset);
        Gumps.Desktop.Children.Add((Gump) new GServerGump(serial, dialog, xOffset, yOffset, layout, text));
      }
    }

    private static void PlaySound(PacketReader pvSrc)
    {
      byte num1 = pvSrc.ReadByte();
      short num2 = pvSrc.ReadInt16();
      int num3 = (int) pvSrc.ReadInt16();
      short num4 = pvSrc.ReadInt16();
      short num5 = pvSrc.ReadInt16();
      short num6 = pvSrc.ReadInt16();
      if ((int) num1 > 1)
        pvSrc.Trace(false);
      if ((int) num2 < 0)
        return;
      if ((int) num2 == 726 && (int) num4 == World.X && (int) num5 == World.Y)
        PacketHandlers.SetEvent(PacketHandlers.EventFlags.PotionSound);
      Engine.Sounds.PlaySound((int) num2, (int) num4, (int) num5, (int) num6, 0.75f, 0.0f);
    }

    public static void Command_Party(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadByte();
      switch (num1)
      {
        case 1:
          pvSrc.ReturnName = "Party Member List";
          int length1 = (int) pvSrc.ReadByte();
          Mobile[] mobileArray1 = new Mobile[length1];
          for (int index = 0; index < length1; ++index)
          {
            mobileArray1[index] = World.WantMobile(pvSrc.ReadInt32());
            mobileArray1[index].QueryStats();
          }
          Party.State = PartyState.Joined;
          Party.Members = mobileArray1;
          int num2 = Engine.GameY + Engine.GameHeight - 50;
          for (int index = 0; index < length1; ++index)
          {
            if (!mobileArray1[index].Player)
            {
              if (mobileArray1[index].StatusBar == null)
              {
                mobileArray1[index].OpenStatus(false);
                if (mobileArray1[index].StatusBar != null)
                {
                  mobileArray1[index].StatusBar.Gump.X = Engine.GameX + Engine.GameWidth - 30 - mobileArray1[index].StatusBar.Gump.Width;
                  mobileArray1[index].StatusBar.Gump.Y = num2 - mobileArray1[index].StatusBar.Gump.Height;
                  num2 -= mobileArray1[index].StatusBar.Gump.Height + 5;
                }
              }
              else
                num2 -= mobileArray1[index].StatusBar.Gump.Height + 5;
            }
          }
          break;
        case 2:
          pvSrc.ReturnName = "Remove Party Member";
          int length2 = (int) pvSrc.ReadByte();
          pvSrc.ReadInt32();
          Mobile[] mobileArray2 = new Mobile[length2];
          for (int index = 0; index < length2; ++index)
          {
            mobileArray2[index] = World.WantMobile(pvSrc.ReadInt32());
            mobileArray2[index].QueryStats();
          }
          Party.State = PartyState.Joined;
          Party.Members = mobileArray2;
          break;
        case 3:
        case 4:
          pvSrc.ReturnName = num1 == 3 ? "Private Party Message" : "Public Party Message";
          int serial1 = pvSrc.ReadInt32();
          string str1 = pvSrc.ReadUnicodeString();
          Mobile mobile = World.FindMobile(serial1);
          string name;
          string str2;
          if (mobile == null || (name = mobile.Name) == null || (str2 = name.Trim()).Length <= 0)
            str2 = "Someone";
          IHue Hue;
          if (str1 == "I'm stunned !!")
          {
            Hue = Hues.Load(34);
            if (mobile != null && !mobile.Player)
              Engine.Sounds.PlaySound(343, mobile.X, mobile.Y, mobile.Z, 0.75f, 0.0f);
          }
          else if (str1.StartsWith("I stunned ") && str1.EndsWith(" !!"))
          {
            Hue = Hues.Load(34);
            if (mobile != null && !mobile.Player)
              Engine.Sounds.PlaySound(481, mobile.X, mobile.Y, mobile.Z, 0.75f, 0.0f);
          }
          else
            Hue = !str1.StartsWith("Changing last target to ") ? (str1.StartsWith("Recalling to ") || str1.StartsWith("Gating to ") ? Hues.Load(89) : (num1 != 3 ? Hues.Load(Preferences.Current.SpeechHues.Regular) : Hues.Load(Preferences.Current.SpeechHues.Whisper))) : Hues.Load(53);
          Engine.AddTextMessage(string.Format("<{0}{1}> {2}", num1 == 3 ? (object) "Whisper: " : (object) "", (object) str2, (object) str1), Engine.DefaultFont, Hue);
          break;
        case 7:
          pvSrc.ReturnName = "Party Invitation";
          int serial2 = pvSrc.ReadInt32();
          Party.State = PartyState.Joining;
          Party.Leader = World.WantMobile(serial2);
          if (!Party.CheckAutomatedAccept())
            break;
          Network.Send((Packet) new PParty_Accept(Party.Leader));
          break;
        default:
          pvSrc.ReturnName = "Unknown Party Message";
          pvSrc.Trace(false);
          break;
      }
    }

    private static void Command(PacketReader pvSrc)
    {
      int num = (int) pvSrc.ReadInt16();
      switch (num)
      {
        case 4:
          pvSrc.ReturnName = "Close Dialog";
          PacketHandlers.Command_CloseDialog(pvSrc);
          break;
        case 6:
          PacketHandlers.Command_Party(pvSrc);
          break;
        case 8:
          pvSrc.ReturnName = "Set World";
          PacketHandlers.Command_SetWorld(pvSrc);
          break;
        case 16:
          pvSrc.ReturnName = "Equipment Description";
          PacketHandlers.Command_EquipInfo(pvSrc);
          break;
        case 20:
          pvSrc.ReturnName = "Mobile Popup";
          PacketHandlers.Command_Popup(pvSrc);
          break;
        case 23:
          pvSrc.ReturnName = "Open Wisdom Codex";
          PacketHandlers.Command_OpenWisdomCodex(pvSrc);
          break;
        case 24:
          pvSrc.ReturnName = "Map Patches";
          PacketHandlers.Command_MapPatches(pvSrc);
          break;
        case 25:
          pvSrc.ReturnName = "Extended Status";
          PacketHandlers.Command_ExtendedStatus(pvSrc);
          break;
        case 27:
          pvSrc.ReturnName = "Spellbook Content";
          PacketHandlers.Command_SpellbookContent(pvSrc);
          break;
        case 29:
          pvSrc.ReturnName = "Custom House";
          PacketHandlers.Command_CustomHouse(pvSrc);
          break;
        case 32:
          pvSrc.ReturnName = "Edit Custom House";
          PacketHandlers.Command_EditCustomHouse(pvSrc);
          break;
        case 33:
          pvSrc.ReturnName = "Clear Combat Ability";
          AbilityInfo.ClearActive();
          break;
        case 34:
          pvSrc.ReturnName = "Damage";
          PacketHandlers.Command_Damage(pvSrc);
          break;
        default:
          Debug.Trace("Unhandled subcommand {0} ( 0x{0:X4} )", (object) num);
          pvSrc.Trace(false);
          break;
      }
    }

    public static void Command_EditCustomHouse(PacketReader pvSrc)
    {
      if (World.FindItem(pvSrc.ReadInt32()) == null)
        return;
      int num = (int) pvSrc.ReadByte();
    }

    public static void Command_CustomHouse(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int revision = pvSrc.ReadInt32();
      Item obj = World.WantItem(serial);
      if (obj.Revision == revision)
        return;
      obj.Revision = revision;
      if (CustomMultiLoader.GetCustomMulti(serial, revision) == null)
      {
        Network.Send((Packet) new PQueryCustomHouse(serial));
      }
      else
      {
        Map.Invalidate();
        GRadar.Invalidate();
      }
    }

    private static void Mobile_Damage(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      int damage = (int) pvSrc.ReadUInt16();
      if (damage <= 0)
        return;
      Gumps.Desktop.Children.Add((Gump) new GDamageLabel(damage, mobile));
    }

    public static void Command_Damage(PacketReader pvSrc)
    {
      if ((int) pvSrc.ReadByte() != 1)
        pvSrc.Trace(false);
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      int damage = (int) pvSrc.ReadByte();
      if (damage <= 0)
        return;
      Gumps.Desktop.Children.Add((Gump) new GDamageLabel(damage, mobile));
    }

    public static void Command_SpellbookContent(PacketReader pvSrc)
    {
      if ((int) pvSrc.ReadInt16() != 1)
        pvSrc.Trace(false);
      Item container = World.FindItem(pvSrc.ReadInt32());
      if (container == null || !container.QueueOpenSB)
        return;
      container.QueueOpenSB = false;
      container.SpellbookGraphic = (int) pvSrc.ReadInt16();
      container.SpellbookOffset = (int) pvSrc.ReadInt16();
      for (int index1 = 0; index1 < 8; ++index1)
      {
        int num = (int) pvSrc.ReadByte();
        for (int index2 = 0; index2 < 8; ++index2)
          container.SetSpellContained(index1 * 8 + index2, (num & 1 << index2) != 0);
      }
      if (!container.OpenSB)
      {
        container.OpenSB = true;
        Spells.OpenSpellbook(container);
      }
      else
      {
        Gump gumpByGuid = Gumps.FindGumpByGUID(string.Format("Spellbook Icon #{0}", (object) container.Serial));
        if (gumpByGuid == null)
          return;
        gumpByGuid.OnDoubleClick(gumpByGuid.Width / 2, gumpByGuid.Height / 2);
      }
    }

    public static void Command_CloseDialog(PacketReader pvSrc)
    {
      int num = pvSrc.ReadInt32();
      int buttonID = pvSrc.ReadInt32();
      Gump[] array = Gumps.Desktop.Children.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] is GServerGump)
        {
          GServerGump gump = (GServerGump) array[index];
          if (gump.DialogID == num)
          {
            GServerGump.SetCachedLocation(gump.DialogID, gump.X, gump.Y);
            Gumps.Destroy((Gump) gump);
            Network.Send((Packet) new PGumpButton(gump, buttonID));
          }
        }
      }
    }

    public static void Command_ExtendedStatus(PacketReader pvSrc)
    {
      int serial = (int) pvSrc.ReadByte();
      Mobile mobile = World.FindMobile(serial);
      if (mobile == null)
        return;
      mobile.IsDeadPet = pvSrc.ReadBoolean();
      if (serial < 2)
        return;
      int num = (int) pvSrc.ReadByte();
    }

    public static void Command_OpenWisdomCodex(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadByte();
      pvSrc.ReadInt32();
      int num2 = (int) pvSrc.ReadByte();
      if (num1 == 1 || num2 == 1)
        return;
      pvSrc.Trace(false);
    }

    public static void Command_MapPatches(PacketReader pvSrc)
    {
      int num = pvSrc.ReadInt32();
      if (num > 5)
        pvSrc.Trace(false);
      for (int index = 0; index < num; ++index)
      {
        pvSrc.ReadInt32();
        pvSrc.ReadInt32();
      }
    }

    private static void PropertyListHash(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int num = pvSrc.ReadInt32();
      Item obj = World.FindItem(serial);
      if (obj != null)
      {
        obj.PropertyID = num;
        if (obj.Parent is Item && (((Item) obj.Parent).ID & 16383) == 8198 && obj.PropertyList == null)
          obj.QueryProperties();
      }
      Mobile mobile = World.FindMobile(serial);
      if (mobile == null)
        return;
      mobile.PropertyID = num;
    }

    public static void Command_EquipInfo(PacketReader pvSrc)
    {
      if (Engine.ServerFeatures.AOS)
      {
        int serial = pvSrc.ReadInt32();
        int num = pvSrc.ReadInt32();
        Item obj = World.FindItem(serial);
        if (obj != null)
        {
          obj.PropertyID = num;
          if (obj.Parent != null && (((Item) obj.Parent).ID & 16383) == 8198 && obj.PropertyList == null)
            obj.QueryProperties();
        }
        Mobile mobile = World.FindMobile(serial);
        if (mobile == null)
          return;
        mobile.PropertyID = num;
      }
      else
      {
        IFont font = (IFont) Engine.GetUniFont(3);
        IHue bright = Hues.Bright;
        int serial = pvSrc.ReadInt32();
        int number1 = pvSrc.ReadInt32();
        Item obj = World.FindItem(serial);
        WandInformation? nullable = new WandInformation?();
        PacketHandlers.AddMessage(serial, font, bright, 6, "You see", Localization.GetString(number1));
        ArrayList dataStore = Engine.GetDataStore();
        int number2;
        while (!pvSrc.Finished && (number2 = pvSrc.ReadInt32()) != -1)
        {
          if (number2 < 0)
          {
            switch (number2)
            {
              case -4:
                PacketHandlers.AddMessage(serial, font, bright, 6, "", "[" + Localization.GetString(1038000) + "]");
                continue;
              case -3:
                int fixedLength = (int) pvSrc.ReadInt16();
                string str = pvSrc.ReadString(fixedLength).Trim();
                if (str.Length > 0)
                {
                  PacketHandlers.AddMessage(serial, font, bright, 6, "", Localization.GetString(1037009) + " " + str);
                  continue;
                }
                continue;
              default:
                Engine.ReleaseDataStore(dataStore);
                pvSrc.Trace(false);
                Engine.AddTextMessage(string.Format("Unknown sub message : {0}", (object) number2));
                return;
            }
          }
          else
          {
            int charges = (int) pvSrc.ReadInt16();
            if (charges != -1)
              dataStore.Add((object) (Localization.GetString(number2) + ": " + (object) charges));
            else
              dataStore.Add((object) Localization.GetString(number2));
            if (obj != null && charges >= 0 && !nullable.HasValue)
            {
              WandEffect? effectByLabel = WandInformation.GetEffectByLabel(number2);
              if (effectByLabel.HasValue)
                nullable = new WandInformation?(new WandInformation(effectByLabel.Value, charges));
            }
          }
        }
        WandRepository.Set(obj, nullable);
        if (dataStore.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append('[');
          for (int index = 0; index < dataStore.Count; ++index)
          {
            stringBuilder.Append(dataStore[index]);
            if (index != dataStore.Count - 1)
              stringBuilder.Append('/');
          }
          stringBuilder.Append(']');
          PacketHandlers.AddMessage(serial, font, bright, 6, "", stringBuilder.ToString());
        }
        if (!pvSrc.Finished)
          pvSrc.Trace(false);
        Engine.ReleaseDataStore(dataStore);
      }
    }

    public static void Command_Popup(PacketReader pvSrc)
    {
      if ((int) pvSrc.ReadInt16() != 1)
        pvSrc.Trace(false);
      int serial = pvSrc.ReadInt32();
      int length = (int) pvSrc.ReadByte();
      PopupEntry[] list = new PopupEntry[length];
      object obj = (object) World.FindMobile(serial) ?? (object) World.FindItem(serial);
      ActionContext active = ActionContext.Active;
      if (active != null)
        active.OnContextBegin(obj);
      bool selected = false;
      for (int index = 0; index < length; ++index)
      {
        int num1 = (int) pvSrc.ReadInt16();
        int num2 = (int) pvSrc.ReadInt16();
        int Flags = (int) pvSrc.ReadInt16();
        list[index] = new PopupEntry(num1, num2, Flags);
        if ((Flags & 32) != 0)
        {
          int num3 = (int) pvSrc.ReadInt16();
        }
        if (active != null && active.OnContextItem(obj, num1, num2) && !selected)
        {
          Network.Send((Packet) new PPopupResponse(obj, num1));
          selected = true;
        }
      }
      if (active != null && !active.OnContextEnd(obj, selected) || obj == null)
        return;
      GContextMenu.Display(obj, list);
    }

    public static void Command_SetWorld(PacketReader pvSrc)
    {
      int index = (int) pvSrc.ReadByte();
      if (index < PacketHandlers.m_WorldNames.Length)
      {
        Engine.m_World = index;
        Engine.m_regMap = index < 2;
        Cursor.Gold = index > 0;
        if (index != PacketHandlers.m_LastWorld)
        {
          if (PacketHandlers.m_LastWorld != -1)
            Engine.AddTextMessage(string.Format("You enter {0}.", (object) PacketHandlers.m_WorldNames[index]));
          PacketHandlers.m_LastWorld = index;
        }
        Engine.AddTextMessage("Querying guardline data");
        Network.Send((Packet) new PPE_QueryGuardlineData());
        Map.Invalidate();
        GRadar.Invalidate();
      }
      else
        pvSrc.Trace(false);
    }

    private static void RequestResurrection(PacketReader pvSrc)
    {
    }

    private static void ItemPickupFailed(PacketReader pvSrc)
    {
      int index = (int) pvSrc.ReadByte();
      if (index < PacketHandlers.m_IPFReason.Length)
        Engine.AddTextMessage(PacketHandlers.m_IPFReason[index], (IFont) Engine.GetFont(3), Hues.Default);
      else if (index != 5 && index != (int) byte.MaxValue)
        pvSrc.Trace(false);
      MoveContext moveContext = ActionContext.Active as MoveContext;
      if (moveContext != null)
        moveContext.OnLiftFailed();
      Item obj = PPickupItem.m_Item;
      if (obj == null)
        return;
      if (Gumps.Drag != null && Gumps.Drag.GetType() == typeof (GDraggedItem) && ((GDraggedItem) Gumps.Drag).Item == obj)
        Gumps.Destroy(Gumps.Drag);
      RestoreInfo restoreInfo = obj.RestoreInfo;
      if (restoreInfo == null)
        return;
      obj.SetLocation(restoreInfo.m_Parent, restoreInfo.m_X, restoreInfo.m_Y, restoreInfo.m_Z);
      obj.RestoreInfo = (RestoreInfo) null;
    }

    private static void ShopContent(PacketReader pvSrc)
    {
      int num = pvSrc.ReadInt32();
      int length = (int) pvSrc.ReadByte();
      if (length <= 0)
        return;
      PacketHandlers.m_BuyMenuSerial = num;
      PacketHandlers.m_BuyMenuNames = new string[length];
      PacketHandlers.m_BuyMenuPrices = new int[length];
      for (int index = length - 1; index >= 0; --index)
      {
        PacketHandlers.m_BuyMenuPrices[index] = pvSrc.ReadInt32();
        string str = pvSrc.ReadString((int) pvSrc.ReadByte());
        try
        {
          str = Localization.GetString(Convert.ToInt32(str));
        }
        catch
        {
        }
        PacketHandlers.m_BuyMenuNames[index] = str;
      }
    }

    private static void ScrollMessage(PacketReader pvSrc)
    {
      int num = (int) pvSrc.ReadByte();
      pvSrc.ReadInt32();
      string text = pvSrc.ReadString((int) pvSrc.ReadUInt16());
      if (!(text != "MISSING UPDATE"))
        return;
      Gumps.Desktop.Children.Add((Gump) new GUpdateScroll(text));
    }

    private static void DisplayPaperdoll(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      string Name = pvSrc.ReadString(60);
      byte num = pvSrc.ReadByte();
      bool canDrag = mobile.Player || ((int) num & 2) != 0;
      Gumps.OpenPaperdoll(mobile, Name, canDrag);
    }

    private static void SelectHue(PacketReader pvSrc)
    {
      int num1 = pvSrc.ReadInt32();
      short num2 = pvSrc.ReadInt16();
      short num3 = pvSrc.ReadInt16();
      GAlphaBackground galphaBackground = new GAlphaBackground(0, 0, 244, 110);
      galphaBackground.m_NonRestrictivePicking = true;
      galphaBackground.Center();
      GItemArt gitemArt = new GItemArt(183, 3, (int) num3);
      gitemArt.X += (58 - (gitemArt.Image.xMax - gitemArt.Image.xMin)) / 2 - gitemArt.Image.xMin;
      gitemArt.Y += (82 - (gitemArt.Image.yMax - gitemArt.Image.yMin)) / 2 - gitemArt.Image.yMin;
      galphaBackground.Children.Add((Gump) gitemArt);
      GHuePicker Target = new GHuePicker(4, 4);
      Target.Brightness = 1;
      Target.SetTag("ItemID", (object) (int) num3);
      Target.SetTag("Item Art", (object) gitemArt);
      Target.SetTag("Dialog", (object) galphaBackground);
      Target.OnHueSelect = new OnHueSelect(Engine.HuePicker_OnHueSelect);
      galphaBackground.Children.Add((Gump) Target);
      galphaBackground.Children.Add((Gump) new GSingleBorder(3, 3, 162, 82));
      galphaBackground.Children.Add((Gump) new GSingleBorder(164, 3, 17, 82));
      GBrightnessBar gbrightnessBar = new GBrightnessBar(165, 4, 15, 80, Target);
      galphaBackground.Children.Add((Gump) gbrightnessBar);
      gbrightnessBar.Refresh();
      GFlatButton gflatButton1 = new GFlatButton(123, 87, 58, 20, "Picker", new OnClick(Engine.HuePickerPicker_OnClick));
      GFlatButton gflatButton2 = new GFlatButton(183, 87, 58, 20, "Okay", new OnClick(Engine.HuePickerOk_OnClick));
      gflatButton2.SetTag("Hue Picker", (object) Target);
      gflatButton2.SetTag("Dialog", (object) galphaBackground);
      gflatButton2.SetTag("Serial", (object) num1);
      gflatButton2.SetTag("ItemID", (object) num3);
      gflatButton2.SetTag("Relay", (object) num2);
      gflatButton1.SetTag("Hue Picker", (object) Target);
      gflatButton1.SetTag("Brightness Bar", (object) gbrightnessBar);
      galphaBackground.Children.Add((Gump) gflatButton1);
      galphaBackground.Children.Add((Gump) gflatButton2);
      Gumps.Desktop.Children.Add((Gump) galphaBackground);
      Engine.HuePicker_OnHueSelect(Target.Hue, (Gump) Target);
    }

    private static void LaunchBrowser(PacketReader pvSrc)
    {
      string url = pvSrc.ReadString();
      if (Engine.m_Fullscreen)
        Engine.AddTextMessage("Cannot open browser in fullscreen.");
      else
        Engine.OpenBrowser(url);
    }

    private static void WarmodeStatus(PacketReader pvSrc)
    {
      bool flag = pvSrc.ReadBoolean();
      if ((int) pvSrc.ReadByte() == 0)
      {
        switch (pvSrc.ReadByte())
        {
          case 32:
          case 50:
          case 0:
            if ((int) pvSrc.ReadByte() != 0)
            {
              pvSrc.Trace(false);
              break;
            }
            break;
          default:
            pvSrc.Trace(false);
            break;
        }
      }
      Mobile player = World.Player;
      if (player != null)
      {
        player.Flags[MobileFlag.Warmode] = flag;
        if (!flag)
          Engine.m_Highlight = (object) null;
      }
      Gumps.Invalidate();
      Engine.Redraw();
    }

    private static void DeleteObject(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      if ((serial & 1073741824) == 0)
      {
        World.Remove(World.FindMobile(serial));
      }
      else
      {
        Item obj = World.FindItem(serial);
        if (obj != null && obj.ItemId == 3852 && obj.IsChildOf((Agent) World.Player))
          PacketHandlers.SetEvent(PacketHandlers.EventFlags.ConsumeHeal);
        World.Remove(obj);
      }
    }

    private static ushort NormalizeColor(ushort color)
    {
      ushort num1 = (ushort) ((uint) color & 32768U);
      ushort num2 = (ushort) ((uint) color & 16384U);
      ushort num3 = (ushort) ((uint) color & 16383U);
      if ((int) num3 == 0)
        return num1;
      if ((int) num3 >= 3000)
        return (ushort) ((int) num1 | (int) num2 | 1);
      return color;
    }

    private static void WorldObject(uint serial, ushort visage, byte offset, ushort amount, ushort x, ushort y, short z, byte light, ushort color, byte flags, ushort amount2, int type, int unk)
    {
      bool wasFound = false;
      Item obj = World.WantItem((int) serial, ref wasFound);
      if (type == 2)
      {
        int MultiID = (int) visage & 16383;
        if (obj.Multi == null || obj.Multi.MultiID != MultiID)
        {
          obj.Multi = new Multi(MultiID);
          Engine.Multis.Register(obj);
        }
        visage = (ushort) 1;
      }
      else if (obj.Multi != null)
      {
        Engine.Multis.Unregister(obj);
        obj.Multi = (Multi) null;
      }
      obj.ID = (int) visage;
      obj.Amount = (int) amount;
      obj.Direction = light;
      obj.Hue = color;
      obj.Flags.Value = (int) flags;
      obj.SetLocation((Agent) World.Agent, (int) x, (int) y, (int) z);
      if (!obj.Visible && (obj.IsCorpse || obj.IsBones) && Options.Current.IncomingNames)
        obj.Look();
      if ((int) visage == 8198 && obj.CorpseSerial != 0)
      {
        Mobile mobile = World.FindMobile(obj.CorpseSerial);
        if (mobile != null)
          obj.Direction = mobile.Direction;
      }
      obj.Update();
    }

    private static void WorldItem_F3(PacketReader packet)
    {
      int num1 = (int) packet.ReadUInt16();
      int num2 = (int) packet.ReadByte();
      int num3 = (int) packet.ReadUInt32();
      int num4 = (int) packet.ReadUInt16();
      int num5 = (int) packet.ReadByte();
      int num6 = (int) packet.ReadUInt16();
      ushort num7 = packet.ReadUInt16();
      int num8 = (int) packet.ReadUInt16();
      int num9 = (int) packet.ReadUInt16();
      int num10 = (int) packet.ReadSByte();
      int num11 = (int) packet.ReadByte();
      int num12 = (int) packet.ReadUInt16();
      int num13 = (int) packet.ReadByte();
      int num14 = (int) num7;
      int type = num2;
      int unk = (int) packet.ReadInt16();
      PacketHandlers.WorldObject((uint) num3, (ushort) num4, (byte) num5, (ushort) num6, (ushort) num8, (ushort) num9, (short) num10, (byte) num11, (ushort) num12, (byte) num13, (ushort) num14, type, unk);
    }

    private static void WorldItem_1A(PacketReader pvSrc)
    {
      uint num1 = pvSrc.ReadUInt32();
      ushort num2 = pvSrc.ReadUInt16();
      byte offset = ((int) num2 & 32768) != 0 ? pvSrc.ReadByte() : (byte) 0;
      ushort num3 = ((int) num1 & int.MinValue) != 0 ? pvSrc.ReadUInt16() : (ushort) 1;
      ushort num4 = pvSrc.ReadUInt16();
      ushort num5 = pvSrc.ReadUInt16();
      byte light = ((int) num4 & 32768) != 0 ? pvSrc.ReadByte() : (byte) 0;
      sbyte num6 = pvSrc.ReadSByte();
      ushort color = ((int) num5 & 32768) != 0 ? pvSrc.ReadUInt16() : (ushort) 0;
      byte flags = ((int) num5 & 16384) != 0 ? pvSrc.ReadByte() : (byte) 0;
      uint serial = num1 & (uint) int.MaxValue;
      ushort visage = (ushort) ((uint) num2 & (uint) short.MaxValue);
      ushort x = (ushort) ((uint) num4 & (uint) short.MaxValue);
      ushort y = (ushort) ((uint) num5 & 16383U);
      int type = 0;
      if ((int) visage >= 16384)
      {
        visage += (ushort) 49152;
        type = 2;
      }
      PacketHandlers.WorldObject(serial, visage, offset, num3, x, y, (short) num6, light, color, flags, num3, type, 1);
    }

    private static void Mobile_Moving(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      bool flag = false;
      mobile.Body = pvSrc.ReadUInt16();
      if (!mobile.Player)
      {
        int x = (int) pvSrc.ReadInt16();
        int y = (int) pvSrc.ReadInt16();
        int z = (int) pvSrc.ReadSByte();
        int num = (int) pvSrc.ReadByte();
        WalkAnimation walkAnimation1 = WalkAnimation.PoolInstance(mobile, x, y, z, num);
        mobile.Walking.Enqueue((object) walkAnimation1);
        if (mobile.Walking.Count > 4)
        {
          WalkAnimation walkAnimation2 = (WalkAnimation) mobile.Walking.Dequeue();
          mobile.SetLocation((int) (short) walkAnimation2.NewX, (int) (short) walkAnimation2.NewY, (int) (short) walkAnimation2.NewZ);
          walkAnimation2.Dispose();
          flag = true;
        }
        ((WalkAnimation) mobile.Walking.Peek()).Start();
        mobile.SetReal(x, y, z, num);
      }
      else
        pvSrc.Seek(6, SeekOrigin.Current);
      mobile.Hue = pvSrc.ReadUInt16();
      mobile.Flags.Value = (int) pvSrc.ReadByte();
      mobile.Notoriety = (Notoriety) pvSrc.ReadByte();
      mobile.IsMoving = !mobile.Player || Engine.amMoving;
      mobile.LastSeen = Engine.Ticks;
      if (!mobile.Visible)
      {
        mobile.Update();
      }
      else
      {
        if (!flag)
          return;
        mobile.Update();
      }
    }

    private static void Movement_Accept(PacketReader pvSrc)
    {
      int num = (int) pvSrc.ReadByte();
      Mobile player = World.Player;
      if (player != null)
        player.Notoriety = (Notoriety) pvSrc.ReadByte();
      if (PacketHandlers.m_Sequences.Count == 0)
      {
        Engine.AddTextMessage("sequence count");
        Engine.Resync();
      }
      else
      {
        int[] numArray = (int[]) PacketHandlers.m_Sequences.Dequeue();
        if (num != numArray[0])
        {
          Engine.AddTextMessage("sequence mismatch");
          Engine.Resync();
        }
        else
          World.SetLocation(numArray[1], numArray[2], numArray[3]);
        PacketHandlers.m_MoveDelay -= TimeSpan.FromSeconds((double) numArray[4] * 0.1);
      }
      ++Engine.m_WalkAck;
    }

    private static void PingReply(PacketReader pvSrc)
    {
      Engine.PingReply((int) pvSrc.ReadByte());
    }

    internal static void AddSequence(int seq, int x, int y, int z, TimeSpan speed)
    {
      PacketHandlers.m_Sequences.Enqueue((object) new int[5]
      {
        seq,
        x,
        y,
        z,
        (int) (speed.TotalSeconds / 0.1)
      });
      PacketHandlers.m_MoveDelay += speed;
    }

    private static void Movement_Reject(PacketReader pvSrc)
    {
      PacketHandlers.m_Sequences.Clear();
      PacketHandlers.m_MoveDelay = TimeSpan.Zero;
      Engine.m_Sequence = 0;
      Engine.m_WalkReq = 0;
      Engine.m_WalkAck = 0;
      Mobile player = World.Player;
      if (player != null)
      {
        int num1 = (int) pvSrc.ReadByte();
        short num2 = pvSrc.ReadInt16();
        short num3 = pvSrc.ReadInt16();
        byte num4 = pvSrc.ReadByte();
        player.Direction = num4;
        sbyte num5 = pvSrc.ReadSByte();
        World.SetLocation((int) num2, (int) num3, (int) num5);
        player.SetLocation((int) num2, (int) num3, (int) num5);
        player.MovedTiles = 0;
        player.HorseFootsteps = 0;
        player.IsMoving = false;
        player.Walking.Clear();
        player.UpdateReal();
        player.Update();
      }
      if (!Engine.m_Stealth)
        return;
      ++Engine.m_StealthSteps;
    }

    internal static void ClearTarget(TargetAction action, TimeSpan timeout)
    {
      PacketHandlers.QueueTarget(action, (object) null, timeout);
    }

    internal static void QueueTarget(TargetAction action, object target, TimeSpan timeout)
    {
      PacketHandlers.m_CancelAction = action;
      PacketHandlers.m_CancelTarget = target;
      PacketHandlers.m_CancelTimeout = DateTime.Now + timeout;
    }

    private static void Target(PacketReader pvSrc)
    {
      byte num1 = pvSrc.ReadByte();
      int targetID = pvSrc.ReadInt32();
      byte num2 = pvSrc.ReadByte();
      pvSrc.ReadInt32();
      int num3 = (int) pvSrc.ReadInt16();
      int num4 = (int) pvSrc.ReadInt16();
      int num5 = (int) pvSrc.ReadByte();
      int num6 = (int) pvSrc.ReadSByte();
      int num7 = (int) pvSrc.ReadInt16();
      if ((int) num2 == 3)
      {
        if (TargetManager.Server == null)
          return;
        TargetManager.Server = (ServerTargetHandler) null;
      }
      else
      {
        if ((int) num1 > 1 || (int) num2 != 1 && (int) num2 != 2 && (int) num2 != 0)
          pvSrc.Trace(false);
        ServerTargetHandler serverTargetHandler;
        TargetManager.Server = serverTargetHandler = new ServerTargetHandler(targetID, (int) num1 != 0, (AggressionType) num2);
        TargetActions.Identify();
        TargetManager.ProcessQueue();
        if (!(PacketHandlers.m_CancelTimeout != DateTime.MinValue))
          return;
        if (PacketHandlers.m_CancelTimeout > DateTime.Now && (PacketHandlers.m_CancelAction == TargetAction.Unknown || serverTargetHandler.Action == PacketHandlers.m_CancelAction))
        {
          if (PacketHandlers.m_CancelTarget == null)
            TargetManager.Server.Cancel();
          else
            TargetManager.Server.Target(PacketHandlers.m_CancelTarget);
        }
        PacketHandlers.m_CancelAction = TargetAction.Unknown;
        PacketHandlers.m_CancelTarget = (object) null;
        PacketHandlers.m_CancelTimeout = DateTime.MinValue;
      }
    }

    private static void Mobile_Incoming(PacketReader pvSrc)
    {
      int serial1 = pvSrc.ReadInt32();
      if ((serial1 & -1073741824) != 0)
        pvSrc.Trace(false);
      ushort num1 = pvSrc.ReadUInt16();
      if (((long) serial1 & 2147483648L) != 0L)
      {
        int num2 = (int) pvSrc.ReadInt16();
      }
      ushort num3 = pvSrc.ReadUInt16();
      ushort num4 = pvSrc.ReadUInt16();
      sbyte num5 = pvSrc.ReadSByte();
      byte num6 = pvSrc.ReadByte();
      ushort num7 = pvSrc.ReadUInt16();
      byte num8 = pvSrc.ReadByte();
      Notoriety notoriety = (Notoriety) pvSrc.ReadByte();
      bool wasFound = false;
      Mobile mob = World.WantMobile(serial1, ref wasFound);
      bool visible = mob.Visible;
      List<Item> objList = new List<Item>((IEnumerable<Item>) mob.Items);
      int serial2;
      while ((serial2 = pvSrc.ReadInt32()) > 0)
      {
        Item obj = World.WantItem(serial2);
        obj.Query();
        ushort num9 = pvSrc.ReadUInt16();
        Layer layer = (Layer) pvSrc.ReadByte();
        ushort num10 = ((int) num9 & 32768) != 0 ? pvSrc.ReadUInt16() : (ushort) 0;
        obj.ID = (int) (ushort) ((uint) num9 & (uint) short.MaxValue);
        obj.Hue = num10;
        obj.Layer = layer;
        obj.SetLocation((Agent) mob, 0, 0, 0);
        objList.Remove(obj);
      }
      foreach (Item obj in objList)
        World.Remove(obj);
      if (mob.Player)
        num6 = (byte) ((uint) (byte) ((uint) num6 & 7U) | (uint) (byte) ((uint) mob.Direction & 128U));
      int num11 = (int) mob.Direction;
      if (!mob.Visible && !mob.Player && Options.Current.IncomingNames)
        mob.Look();
      if (!mob.Player)
      {
        mob.SetLocation((Agent) World.Agent, (int) num3, (int) num4, (int) num5);
        mob.Direction = num6;
        mob.Hue = num7;
        mob.Body = num1;
        mob.IsMoving = false;
        mob.MovedTiles = 0;
        mob.HorseFootsteps = 0;
        mob.Walking.Clear();
        mob.UpdateReal();
      }
      mob.Flags.Value = (int) num8;
      mob.Notoriety = notoriety;
      mob.LastSeen = Engine.Ticks;
      mob.EquipChanged();
      mob.Update();
      if (visible || mob.Player || (int) num1 != 400 && (int) num1 != 401 || mob.StatusBar == null && !mob.IsFriend && (!mob.IsInParty && !TargetManager.IsAcquirable(World.Player, mob)))
        return;
      mob.QueryStats();
    }

    private static void Mobile_Attributes_HitPoints(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.Refresh = true;
      mobile.MaximumHitPoints = (int) pvSrc.ReadUInt16();
      mobile.CurrentHitPoints = (int) pvSrc.ReadUInt16();
      mobile.Refresh = false;
    }

    private static void Mobile_Attributes_Stamina(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.Refresh = true;
      mobile.MaximumStamina = (int) pvSrc.ReadUInt16();
      mobile.CurrentStamina = (int) pvSrc.ReadUInt16();
      mobile.Refresh = false;
    }

    private static void Mobile_Attributes_Mana(PacketReader pvSrc)
    {
      Mobile mobile = World.FindMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.Refresh = true;
      mobile.MaximumMana = (int) pvSrc.ReadUInt16();
      mobile.CurrentMana = (int) pvSrc.ReadUInt16();
      mobile.Refresh = false;
    }

    private static void Mobile_Status(PacketReader pvSrc)
    {
      Mobile mobile = World.WantMobile(pvSrc.ReadInt32());
      if (mobile == null)
        return;
      mobile.Refresh = true;
      mobile.Name = pvSrc.ReadString(30);
      mobile.CurrentHitPoints = (int) pvSrc.ReadUInt16();
      mobile.MaximumHitPoints = (int) pvSrc.ReadUInt16();
      mobile.IsPet = pvSrc.ReadBoolean();
      byte num1 = pvSrc.ReadByte();
      if ((int) num1 >= 1)
      {
        mobile.Gender = (int) pvSrc.ReadByte();
        mobile.Strength = (int) pvSrc.ReadUInt16();
        mobile.Dexterity = (int) pvSrc.ReadUInt16();
        mobile.Intelligence = (int) pvSrc.ReadUInt16();
        mobile.CurrentStamina = (int) pvSrc.ReadUInt16();
        mobile.MaximumStamina = (int) pvSrc.ReadUInt16();
        mobile.CurrentMana = (int) pvSrc.ReadUInt16();
        mobile.MaximumMana = (int) pvSrc.ReadUInt16();
        mobile.Gold = pvSrc.ReadInt32();
        mobile.Armor = (int) pvSrc.ReadUInt16();
        mobile.Weight = (int) pvSrc.ReadUInt16();
        if ((int) num1 >= 2)
        {
          if ((int) num1 >= 5)
          {
            int num2 = (int) pvSrc.ReadUInt16();
            int num3 = (int) pvSrc.ReadByte();
          }
          mobile.StatCap = (int) pvSrc.ReadUInt16();
          if ((int) num1 >= 3)
          {
            mobile.FollowersCur = (int) pvSrc.ReadByte();
            mobile.FollowersMax = (int) pvSrc.ReadByte();
            if ((int) num1 >= 4)
            {
              mobile.FireResist = (int) pvSrc.ReadInt16();
              mobile.ColdResist = (int) pvSrc.ReadInt16();
              mobile.PoisonResist = (int) pvSrc.ReadInt16();
              mobile.EnergyResist = (int) pvSrc.ReadInt16();
              mobile.Luck = (int) pvSrc.ReadUInt16();
              mobile.DamageMin = (int) pvSrc.ReadUInt16();
              mobile.DamageMax = (int) pvSrc.ReadUInt16();
              mobile.TithingPoints = pvSrc.ReadInt32();
              if ((int) num1 > 5)
                pvSrc.Trace(false);
            }
            else
            {
              mobile.FireResist = 0;
              mobile.ColdResist = 0;
              mobile.PoisonResist = 0;
              mobile.EnergyResist = 0;
              mobile.Luck = 0;
              mobile.DamageMin = 0;
              mobile.DamageMax = 0;
            }
          }
          else
          {
            mobile.FollowersCur = 0;
            mobile.FollowersMax = 5;
          }
        }
        else
          mobile.StatCap = 225;
      }
      mobile.Refresh = false;
    }

    private static void Mobile_Update(PacketReader pvSrc)
    {
      Mobile mobile = World.WantMobile(pvSrc.ReadInt32());
      ushort num1 = pvSrc.ReadUInt16();
      byte num2 = pvSrc.ReadByte();
      ushort num3 = pvSrc.ReadUInt16();
      byte num4 = pvSrc.ReadByte();
      short num5 = pvSrc.ReadInt16();
      short num6 = pvSrc.ReadInt16();
      short num7 = pvSrc.ReadInt16();
      byte num8 = pvSrc.ReadByte();
      sbyte num9 = pvSrc.ReadSByte();
      if ((int) num2 != 0 || (int) num7 != 0)
        pvSrc.Trace(false);
      if (mobile.Player)
      {
        if (Engine.m_InResync)
        {
          Engine.m_InResync = false;
          Engine.AddTextMessage("Resynchronization complete.");
        }
        else
          mobile.InRange((int) num5, (int) num6, 18);
        PacketHandlers.m_Sequences.Clear();
        PacketHandlers.m_MoveDelay = TimeSpan.Zero;
        Engine.m_Sequence = 0;
        Engine.m_WalkAck = 0;
        Engine.m_WalkReq = 0;
      }
      if (mobile.Player)
      {
        if (((int) num1 == 402 || (int) num1 == 403) && ((int) mobile.Body != 402 && (int) mobile.Body != 403))
        {
          Network.Send((Packet) new PSetWarMode(false, (short) 32, (byte) 0));
          Engine.Effects.Add((Fade) new DeathEffect());
        }
        else if (((int) mobile.Body == 402 || (int) mobile.Body == 403) && ((int) num1 != 402 && (int) num1 != 403))
        {
          Animation animation = mobile.Animation = new Animation();
          animation.Action = 17;
          animation.Delay = 0;
          animation.Forward = true;
          animation.Repeat = false;
          animation.Run();
          Engine.Effects.Add((Fade) new ResurrectEffect());
        }
      }
      if (mobile.Player)
        World.SetLocation((int) num5, (int) num6, (int) num9);
      mobile.SetLocation((Agent) World.Agent, (int) num5, (int) num6, (int) num9);
      mobile.Body = num1;
      mobile.Hue = num3;
      mobile.IsMoving = false;
      mobile.MovedTiles = 0;
      mobile.HorseFootsteps = 0;
      mobile.Walking.Clear();
      mobile.UpdateReal();
      mobile.Direction = num8;
      mobile.Flags.Value = (int) num4;
      mobile.LastSeen = Engine.Ticks;
      mobile.Update();
    }

    private static void DisplayQuestionMenu(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int menuID = (int) pvSrc.ReadInt16();
      string question = pvSrc.ReadString((int) pvSrc.ReadByte());
      AnswerEntry[] answers = new AnswerEntry[(int) pvSrc.ReadByte()];
      for (int index = 0; index < answers.Length; ++index)
        answers[index] = new AnswerEntry(index, (int) pvSrc.ReadInt16(), (int) pvSrc.ReadUInt16(), pvSrc.ReadString((int) pvSrc.ReadByte()));
      if (answers.Length > 0 && answers[0].ItemID != 0)
        Gumps.Desktop.Children.Add((Gump) new GItemList(serial, menuID, question, answers));
      else
        Gumps.Desktop.Children.Add((Gump) new GQuestionMenu(serial, menuID, question, answers));
    }

    private static void AddMessage(int serial, IFont font, IHue hue, int type, string name, string text)
    {
      PacketHandlers.AddMessage(serial, font, hue, type, name, text, 0);
    }

    private static void AddMessage(int serial, IFont font, IHue hue, int type, string name, string text, int number)
    {
      if (ActionContext.Active is LookContext)
        return;
      name = name.Trim();
      text = text.Trim();
      if (number == 1004013)
      {
        Mobile player = World.Player;
        Engine.Sounds.PlaySound(481, player.X, player.Y, player.Z, 0.75f, 0.0f);
        Mobile mobile = Engine.m_LastAttacker;
        string str = "someone";
        if (mobile != null && !string.IsNullOrEmpty(mobile.Name))
          str = mobile.Name;
        Party.SendAutomatedMessage("I stunned {0} !!", (object) str);
      }
      else if (number == 1004014)
      {
        Mobile player = World.Player;
        Engine.Sounds.PlaySound(343, player.X, player.Y, player.Z, 0.75f, 0.0f);
        Party.SendAutomatedMessage("I'm stunned !!");
      }
      switch (number)
      {
        case 502698:
          TargetActions.Identify(TargetAction.Stealing);
          break;
        case 1049541:
          TargetActions.Lookahead = TargetAction.Discord;
          break;
        case 1049632:
          TargetActions.Identify(TargetAction.Bola);
          break;
        case 500236:
          TargetActions.Identify(TargetAction.PurplePotion);
          break;
        case 500819:
          TargetActions.Lookahead = TargetAction.DetectHidden;
          break;
        case 500948:
          TargetActions.Lookahead = TargetAction.Bandage;
          break;
      }
      if (number == 500119 || number == 1045157 || number == 3000201)
      {
        Engine.DelayedAction();
        ActionContext active = ActionContext.Active;
        if (active != null)
        {
          active.WasDelayed = true;
          return;
        }
      }
      if (serial > 0 && serial < 1073741824)
      {
        Mobile mobile = World.FindMobile(serial);
        if (mobile != null)
        {
          if (!string.IsNullOrEmpty(name))
            mobile.GuessedName = name;
          if (font is Font && type == 0 && text.StartsWith("["))
          {
            string str1 = text;
            if (str1.EndsWith(" (Chaos)"))
              str1 = str1.Substring(0, str1.Length - " (Chaos)".Length);
            else if (str1.EndsWith(" (Order)"))
              str1 = str1.Substring(0, str1.Length - " (Order)".Length);
            if (str1.EndsWith("]"))
            {
              int num = str1.LastIndexOf(", ");
              string str2;
              if (num >= 0)
              {
                int startIndex = num + 2;
                str2 = str1.Substring(startIndex, str1.Length - startIndex - 1);
              }
              else
                str2 = str1.Substring(1, str1.Length - 2);
              mobile.Guild = str2;
            }
          }
          else if (type == 6 && text.StartsWith("(") && text.EndsWith(")"))
          {
            if (text.EndsWith("Minax)") || text.EndsWith("Minax) (Evil)"))
              mobile.Faction = Faction.Minax;
            else if (text.EndsWith("Council of Mages)") || text.EndsWith("Council of Mages) (Hero)"))
              mobile.Faction = Faction.CouncilOfMages;
            else if (text.EndsWith("True Britannians)") || text.EndsWith("True Britannians) (Hero)"))
              mobile.Faction = Faction.TrueBritannians;
            else if (text.EndsWith("Shadowlords)") || text.EndsWith("Shadowlords) (Evil)"))
              mobile.Faction = Faction.Shadowlords;
          }
        }
      }
      switch (type)
      {
        case 0:
        case 2:
        case 3:
        case 4:
        case 7:
        case 8:
        case 9:
        case 10:
          if ((type == 0 || type == 2 || (type == 8 || type == 9) || (type == 13 || type == 14)) && (serial > 0 && serial < 1073741824))
          {
            Mobile mobile = World.FindMobile(serial);
            if (mobile != null && mobile.IsIgnored)
              break;
          }
          if (type == 3 || type == 4)
          {
            StreamWriter streamWriter = new StreamWriter("Messages.log", true);
            streamWriter.WriteLine("Serial = 0x{0:X8}", (object) serial);
            streamWriter.WriteLine("Font = {0}", (object) font);
            streamWriter.WriteLine("Hue = {0}", (object) hue);
            streamWriter.WriteLine("Type = {0}", (object) type);
            streamWriter.WriteLine("Name = \"{0}\"", (object) name);
            streamWriter.WriteLine("Text = \"{0}\"", (object) text);
            streamWriter.WriteLine(new string('#', 20));
            streamWriter.Flush();
            streamWriter.Close();
          }
          bool flag = false;
          if (type == 10 || number >= 1060718 && number <= 1060727)
          {
            Spell spellByPower = Spells.GetSpellByPower(text);
            if (spellByPower == null)
            {
              text += " - Unknown";
            }
            else
            {
              if (serial == World.Serial)
                TargetActions.Lookahead = (TargetAction) (spellByPower.SpellID - 1);
              text = text + " - " + spellByPower.Name;
            }
            flag = true;
          }
          Mobile mobile1 = World.FindMobile(serial);
          if (mobile1 != null)
          {
            if (flag && !mobile1.Player)
              hue = Hues.GetNotoriety(mobile1.Notoriety, false);
            if (mobile1.Player)
            {
              ActionContext active = ActionContext.Active;
              if (active != null && !active.OnSpeech(text))
                break;
            }
            if (type == 7)
              MessageManager.ClearMessages((IMessageOwner) mobile1);
            mobile1.AddTextMessage(name, text, font, hue, type == 10 || type == 7);
            break;
          }
          Item obj1 = World.FindItem(serial);
          if (obj1 != null)
          {
            if (type == 7)
              MessageManager.ClearMessages((IMessageOwner) obj1);
            obj1.AddTextMessage(name, text, font, hue, type == 10 || type == 7);
            break;
          }
          Engine.AddTextMessage(text, font, hue);
          break;
        case 1:
          if (name.Length > 0)
          {
            Engine.AddTextMessage(name + ": " + text, font, hue);
            break;
          }
          Engine.AddTextMessage(text, font, hue);
          break;
        case 6:
          Mobile mobile2 = World.FindMobile(serial);
          if (mobile2 != null)
          {
            mobile2.AddTextMessage("You see", text, font, hue, false);
            break;
          }
          Item obj2 = World.FindItem(serial);
          if (obj2 != null)
          {
            obj2.AddTextMessage("You see", text, font, hue, false);
            break;
          }
          Engine.AddTextMessage(text, font, hue);
          break;
        case 13:
          Engine.AddTextMessage(string.Format("[Guild][{0}]: {1}", string.IsNullOrEmpty(name) ? (object) "???" : (object) name, (object) text), font, hue);
          break;
        case 14:
          Engine.AddTextMessage(string.Format("[Alliance][{0}]: {1}", string.IsNullOrEmpty(name) ? (object) "???" : (object) name, (object) text), font, hue);
          break;
        default:
          StreamWriter streamWriter1 = new StreamWriter("Messages.log", true);
          streamWriter1.WriteLine("Serial = 0x{0:X8}", (object) serial);
          streamWriter1.WriteLine("Font = {0}", (object) font);
          streamWriter1.WriteLine("Hue = {0}", (object) hue);
          streamWriter1.WriteLine("Type = {0}", (object) type);
          streamWriter1.WriteLine("Name = \"{0}\"", (object) name);
          streamWriter1.WriteLine("Text = \"{0}\"", (object) text);
          streamWriter1.WriteLine(new string('#', 20));
          streamWriter1.Flush();
          streamWriter1.Close();
          break;
      }
    }

    private static void Message_Unicode(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int num = (int) pvSrc.ReadInt16();
      int type = (int) pvSrc.ReadByte();
      IHue hue = Hues.Load((int) pvSrc.ReadInt16());
      IFont font = (IFont) Engine.GetUniFont((int) pvSrc.ReadInt16());
      pvSrc.ReadString(4);
      string name = pvSrc.ReadString(30);
      string text = pvSrc.ReadUnicodeString();
      PacketHandlers.AddMessage(serial, font, hue, type, name, text);
    }

    private static void Weather(PacketReader pvSrc)
    {
      int num1 = (int) pvSrc.ReadByte();
      int num2 = (int) pvSrc.ReadByte();
      int num3 = (int) pvSrc.ReadSByte();
    }

    private static void LoginComplete(PacketReader pvSrc)
    {
      Music.Stop();
      Engine.Unlock();
      Engine.m_Loading = false;
      Engine.m_Ingame = true;
      Cursor.Hourglass = false;
      Engine.ClearScreen();
      Mobile player = World.Player;
      Preferences.Current.Layout.Apply(true);
      Engine.DrawNow();
      Engine.StartPings();
      Network.Send((Packet) new POpenPaperdoll());
      World.Player.QueryStats();
      if (ShaderData.IsSupported)
        return;
      Engine.AddTextMessage("*** Your video card does not support the 2.0 shader model.", 30f);
      Engine.AddTextMessage("*** Game graphics will not be rendered correctly.", 30f);
    }

    private static void Message_ASCII(PacketReader pvSrc)
    {
      int serial = pvSrc.ReadInt32();
      int num = (int) pvSrc.ReadInt16();
      int type = (int) pvSrc.ReadByte();
      short toWrite1;
      IHue hue = Hues.Load((int) (toWrite1 = pvSrc.ReadInt16()));
      short toWrite2;
      IFont font = (IFont) Engine.GetFont((int) (toWrite2 = pvSrc.ReadInt16()));
      string name = pvSrc.ReadString(30);
      string text = pvSrc.ReadString();
      if (World.Player != null && serial == 0 && (num == 0 && type == 0) && ((int) toWrite1 == -1 && (int) toWrite2 == -1 && name == "SYSTEM"))
      {
        Packet p = new Packet((byte) 3);
        p.m_Stream.Write((byte) 32);
        p.m_Stream.Write(toWrite1);
        p.m_Stream.Write(toWrite2);
        StringBuilder stringBuilder = new StringBuilder();
        string fileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
        stringBuilder.AppendFormat("{0}{1}E{2}{3} {4} {5}\0", (object) 'C', (object) 'H', (object) 'A', (object) 'T', (object) fileName, (object) Assembly.GetCallingAssembly().GetName().Version);
        for (int index = 0; index < stringBuilder.Length; ++index)
        {
          byte toWrite3 = (byte) ((uint) PacketHandlers.m_Key[index % PacketHandlers.m_Key.Length] ^ (uint) (byte) stringBuilder[index]);
          p.m_Stream.Write(toWrite3);
        }
        Network.Send(p);
      }
      else
        PacketHandlers.AddMessage(serial, font, hue, type, name, text);
    }

    private static void LoginConfirm(PacketReader pvSrc)
    {
      Engine.PingReply(-1);
      World.Clear();
      Map.Invalidate();
      Mobile mobile = World.WantMobile(pvSrc.ReadInt32());
      World.Serial = mobile.Serial;
      Macros.Reset();
      if (pvSrc.ReadInt32() != 0)
        pvSrc.Trace(false);
      mobile.Body = pvSrc.ReadUInt16();
      short num1 = pvSrc.ReadInt16();
      short num2 = pvSrc.ReadInt16();
      short num3 = pvSrc.ReadInt16();
      World.SetLocation((int) num1, (int) num2, (int) num3);
      mobile.SetLocation((Agent) World.Agent, (int) num1, (int) num2, (int) num3);
      mobile.UpdateReal();
      mobile.Direction = pvSrc.ReadByte();
      mobile.Update();
      Network.Send((Packet) new PQuerySkills());
      Engine.PingRequest(false);
      Network.Send((Packet) new PClientVersion(Engine.GetVersionString()));
      Network.Send((Packet) new PScreenSize());
      Network.Send((Packet) new PSetLanguage());
      Network.Send((Packet) new PUnknownLogin());
      PUpdateRange.Dispatch((object) null);
      Party.State = PartyState.Alone;
    }

    public static void Register(int packetID, int length, PacketCallback callback)
    {
      PacketHandlers.m_Handlers[packetID] = new PacketHandler(packetID, length, callback);
    }

    [Flags]
    internal enum EventFlags
    {
      None = 0,
      ConsumeHeal = 1,
      PotionSound = 2,
      HealPotion = PotionSound | ConsumeHeal,
    }

    private enum Condition
    {
      Ageless,
      LikeNew,
      Slightly,
      Somewhat,
      Fairly,
      Greatly,
      IDOC,
    }

    private enum EffectType
    {
      Moving,
      Lightning,
      Location,
      Fixed,
    }

    private enum EffectLayer
    {
      Head = 0,
      RightHand = 1,
      LeftHand = 2,
      Waist = 3,
      LeftFoot = 4,
      RightFoot = 5,
      CenterFeet = 7,
    }
  }
}
