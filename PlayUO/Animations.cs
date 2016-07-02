// Decompiled with JetBrains decompiler
// Type: PlayUO.Animations
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using Sallos;
using SharpDX.Direct3D9;
using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace PlayUO
{
  public class Animations
  {
    private static ushort[] _guassianBlurMatrix = new ushort[289]{ (ushort) 0, (ushort) 11, (ushort) 21, (ushort) 31, (ushort) 38, (ushort) 45, (ushort) 50, (ushort) 53, (ushort) 53, (ushort) 53, (ushort) 50, (ushort) 45, (ushort) 38, (ushort) 31, (ushort) 21, (ushort) 11, (ushort) 0, (ushort) 11, (ushort) 23, (ushort) 34, (ushort) 44, (ushort) 53, (ushort) 60, (ushort) 65, (ushort) 68, (ushort) 69, (ushort) 68, (ushort) 65, (ushort) 60, (ushort) 53, (ushort) 44, (ushort) 34, (ushort) 23, (ushort) 11, (ushort) 21, (ushort) 34, (ushort) 46, (ushort) 57, (ushort) 66, (ushort) 74, (ushort) 80, (ushort) 84, (ushort) 85, (ushort) 84, (ushort) 80, (ushort) 74, (ushort) 66, (ushort) 57, (ushort) 46, (ushort) 34, (ushort) 21, (ushort) 31, (ushort) 44, (ushort) 57, (ushort) 68, (ushort) 79, (ushort) 88, (ushort) 95, (ushort) 100, (ushort) 101, (ushort) 100, (ushort) 95, (ushort) 88, (ushort) 79, (ushort) 68, (ushort) 57, (ushort) 44, (ushort) 31, (ushort) 38, (ushort) 53, (ushort) 66, (ushort) 79, (ushort) 91, (ushort) 101, (ushort) 110, (ushort) 116, (ushort) 117, (ushort) 116, (ushort) 110, (ushort) 101, (ushort) 91, (ushort) 79, (ushort) 66, (ushort) 53, (ushort) 38, (ushort) 45, (ushort) 60, (ushort) 74, (ushort) 88, (ushort) 101, (ushort) 114, (ushort) 124, (ushort) 131, (ushort) 133, (ushort) 131, (ushort) 124, (ushort) 114, (ushort) 101, (ushort) 88, (ushort) 74, (ushort) 60, (ushort) 45, (ushort) 50, (ushort) 65, (ushort) 80, (ushort) 95, (ushort) 110, (ushort) 124, (ushort) 136, (ushort) 146, (ushort) 149, (ushort) 146, (ushort) 136, (ushort) 124, (ushort) 110, (ushort) 95, (ushort) 80, (ushort) 65, (ushort) 50, (ushort) 53, (ushort) 68, (ushort) 84, (ushort) 100, (ushort) 116, (ushort) 131, (ushort) 146, (ushort) 159, (ushort) 165, (ushort) 159, (ushort) 146, (ushort) 131, (ushort) 116, (ushort) 100, (ushort) 84, (ushort) 68, (ushort) 53, (ushort) 53, (ushort) 69, (ushort) 85, (ushort) 101, (ushort) 117, (ushort) 133, (ushort) 149, (ushort) 165, (ushort) 181, (ushort) 165, (ushort) 149, (ushort) 133, (ushort) 117, (ushort) 101, (ushort) 85, (ushort) 69, (ushort) 53, (ushort) 53, (ushort) 68, (ushort) 84, (ushort) 100, (ushort) 116, (ushort) 131, (ushort) 146, (ushort) 159, (ushort) 165, (ushort) 159, (ushort) 146, (ushort) 131, (ushort) 116, (ushort) 100, (ushort) 84, (ushort) 68, (ushort) 53, (ushort) 50, (ushort) 65, (ushort) 80, (ushort) 95, (ushort) 110, (ushort) 124, (ushort) 136, (ushort) 146, (ushort) 149, (ushort) 146, (ushort) 136, (ushort) 124, (ushort) 110, (ushort) 95, (ushort) 80, (ushort) 65, (ushort) 50, (ushort) 45, (ushort) 60, (ushort) 74, (ushort) 88, (ushort) 101, (ushort) 114, (ushort) 124, (ushort) 131, (ushort) 133, (ushort) 131, (ushort) 124, (ushort) 114, (ushort) 101, (ushort) 88, (ushort) 74, (ushort) 60, (ushort) 45, (ushort) 38, (ushort) 53, (ushort) 66, (ushort) 79, (ushort) 91, (ushort) 101, (ushort) 110, (ushort) 116, (ushort) 117, (ushort) 116, (ushort) 110, (ushort) 101, (ushort) 91, (ushort) 79, (ushort) 66, (ushort) 53, (ushort) 38, (ushort) 31, (ushort) 44, (ushort) 57, (ushort) 68, (ushort) 79, (ushort) 88, (ushort) 95, (ushort) 100, (ushort) 101, (ushort) 100, (ushort) 95, (ushort) 88, (ushort) 79, (ushort) 68, (ushort) 57, (ushort) 44, (ushort) 31, (ushort) 21, (ushort) 34, (ushort) 46, (ushort) 57, (ushort) 66, (ushort) 74, (ushort) 80, (ushort) 84, (ushort) 85, (ushort) 84, (ushort) 80, (ushort) 74, (ushort) 66, (ushort) 57, (ushort) 46, (ushort) 34, (ushort) 21, (ushort) 11, (ushort) 23, (ushort) 34, (ushort) 44, (ushort) 53, (ushort) 60, (ushort) 65, (ushort) 68, (ushort) 69, (ushort) 68, (ushort) 65, (ushort) 60, (ushort) 53, (ushort) 44, (ushort) 34, (ushort) 23, (ushort) 11, (ushort) 0, (ushort) 11, (ushort) 21, (ushort) 31, (ushort) 38, (ushort) 45, (ushort) 50, (ushort) 53, (ushort) 53, (ushort) 53, (ushort) 50, (ushort) 45, (ushort) 38, (ushort) 31, (ushort) 21, (ushort) 11, (ushort) 0 };
    private short[] m_Palette = new short[256];
    private int[] m_Palette32 = new int[256];
    public ArrayList m_Frames = new ArrayList();
    private const int DoubleXor = -2145386496;
    private const int DoubleOpaque = -2147450880;
    public Entry3D[] m_Index;
    public Entry3D[] m_Index2;
    public Entry3D[] m_Index3;
    public Entry3D[] m_Index4;
    public Entry3D[] m_Index5;
    private int m_Count;
    private int m_Count2;
    private int m_Count3;
    private int m_Count4;
    private int m_Count5;
    private static Stream m_Stream;
    private static Stream m_Stream2;
    private static Stream m_Stream3;
    private static Stream m_Stream4;
    private static Stream m_Stream5;
    private static BodyType[] m_Types;
    private int[] m_Table;
    private static Animations.Loader m_Loader;
    private static Animations.Loader m_Loader2;
    private static Animations.Loader m_Loader3;
    private static Animations.Loader m_Loader4;
    private static Animations.Loader m_Loader5;
    private int m_Action;
    private int m_BodyID;
    private int m_SA_Body;
    private int m_SA_Dir;
    private MountTable m_MountTable;
    private byte[] m_Data;

    public static bool IsLoading
    {
      get
      {
        if (!Animations.m_Loader.IsAlive && !Animations.m_Loader2.IsAlive && (!Animations.m_Loader3.IsAlive && !Animations.m_Loader4.IsAlive))
          return Animations.m_Loader5.IsAlive;
        return true;
      }
    }

    public MountTable MountTable
    {
      get
      {
        if (this.m_MountTable == null)
          this.m_MountTable = new MountTable();
        return this.m_MountTable;
      }
    }

    public Animations()
    {
      ArchivedFile archivedFile = Engine.FileManager.GetArchivedFile("play/config/body-types.dat");
      if (archivedFile != null)
      {
        using (BinaryReader binaryReader = new BinaryReader(archivedFile.Download()))
        {
          Animations.m_Types = new BodyType[(int) binaryReader.BaseStream.Length];
          for (int index = 0; index < Animations.m_Types.Length; ++index)
            Animations.m_Types[index] = (BodyType) binaryReader.ReadByte();
          if (970 < Animations.m_Types.Length)
            Animations.m_Types[970] = BodyType.Human;
        }
      }
      else
      {
        Debug.Error("data/config/body-types.dat does not exist");
        Animations.m_Types = new BodyType[0];
      }
      try
      {
        Animations.m_Stream = Engine.FileManager.OpenMUL(Files.AnimMul);
      }
      catch
      {
      }
      try
      {
        Animations.m_Stream2 = Engine.FileManager.OpenMUL("Anim2.mul");
      }
      catch
      {
      }
      try
      {
        Animations.m_Stream3 = Engine.FileManager.OpenMUL("Anim3.mul");
      }
      catch
      {
      }
      try
      {
        Animations.m_Stream4 = Engine.FileManager.OpenMUL("Anim4.mul");
      }
      catch
      {
      }
      try
      {
        Animations.m_Stream5 = Engine.FileManager.OpenMUL("Anim5.mul");
      }
      catch
      {
      }
      Animations.m_Loader = new Animations.Loader(this, 1);
      Animations.m_Loader2 = new Animations.Loader(this, 2);
      Animations.m_Loader3 = new Animations.Loader(this, 3);
      Animations.m_Loader4 = new Animations.Loader(this, 4);
      Animations.m_Loader5 = new Animations.Loader(this, 5);
    }

    public void Translate(ref int bodyID)
    {
      if (this.m_Table == null)
        this.LoadTable();
      if (bodyID <= 0 || bodyID >= this.m_Table.Length)
        bodyID = 0;
      else
        bodyID = this.m_Table[bodyID] & (int) short.MaxValue;
    }

    public void Translate(ref int bodyID, ref IHue h)
    {
      if (this.m_Table == null)
        this.LoadTable();
      if (bodyID <= 0 || bodyID >= this.m_Table.Length)
      {
        bodyID = 0;
      }
      else
      {
        int num = this.m_Table[bodyID];
        if ((num & int.MinValue) == 0)
          return;
        bodyID = num & (int) short.MaxValue;
        if (h != Hues.Default)
          return;
        h = Hues.Load(num >> 15 & (int) ushort.MaxValue);
      }
    }

    public void Translate(ref int bodyID, ref int hue)
    {
      if (this.m_Table == null)
        this.LoadTable();
      if (bodyID <= 0 || bodyID >= this.m_Table.Length)
      {
        bodyID = 0;
      }
      else
      {
        int num = this.m_Table[bodyID];
        if ((num & int.MinValue) == 0)
          return;
        bodyID = num & (int) short.MaxValue;
        if (hue != 0)
          return;
        hue = num >> 15 & (int) ushort.MaxValue;
      }
    }

    private void LoadTable()
    {
      int length = 400 + (this.m_Index.Length - 35000) / 175;
      this.m_Table = new int[length];
      for (int bodyID = 0; bodyID < length; ++bodyID)
      {
        GraphicTranslation graphicTranslation = GraphicTranslators.Bodies[bodyID];
        this.m_Table[bodyID] = graphicTranslation == null || BodyConverter.Contains(bodyID) ? bodyID : graphicTranslation.FallbackId | int.MinValue | ((graphicTranslation.FallbackData ^ 32768) & (int) ushort.MaxValue) << 15;
      }
    }

    public static bool WaitLoading()
    {
      return (!Animations.m_Loader.IsAlive || Animations.m_Loader.Wait()) && (!Animations.m_Loader2.IsAlive || Animations.m_Loader2.Wait()) && ((!Animations.m_Loader3.IsAlive || Animations.m_Loader3.Wait()) && (!Animations.m_Loader4.IsAlive || Animations.m_Loader4.Wait())) && (!Animations.m_Loader5.IsAlive || Animations.m_Loader5.Wait());
    }

    public static void StartLoading()
    {
      Animations.m_Loader.Start();
      Animations.m_Loader2.Start();
      Animations.m_Loader3.Start();
      Animations.m_Loader4.Start();
      Animations.m_Loader5.Start();
    }

    public BodyType GetBodyType(int body)
    {
      if (body >= 0 && body < Animations.m_Types.Length)
        return Animations.m_Types[body];
      return BodyType.Empty;
    }

    public Frame GetFrame(IAnimationOwner owner, int BodyID, int ActionID, int Direction, int Frame, int xCenter, int yCenter, IHue h, ref int TextureX, ref int TextureY, bool preserveHue)
    {
      if (BodyID <= 0)
        return Frame.Empty;
      this.m_Action = ActionID;
      Direction &= 7;
      int direction = Direction;
      if (Direction > 4)
        direction -= (Direction - 4) * 2;
      this.Translate(ref BodyID, ref h);
      this.m_BodyID = BodyID;
      int realIdNoMap = this.GetRealIDNoMap(BodyID, ActionID, direction);
      if (realIdNoMap < 0 || realIdNoMap >= this.m_Count)
        return Frame.Empty;
      int num = realIdNoMap << 2;
      if (Preferences.Current.RenderSettings.SmoothCharacters)
        num |= 2;
      if (!Preferences.Current.RenderSettings.AnimatedCharacters)
      {
        BodyType bodyType = this.GetBodyType(BodyID);
        bool flag = true;
        switch (bodyType)
        {
          case BodyType.Monster:
            flag = this.m_Action != 2 && this.m_Action != 3;
            break;
          case BodyType.Sea:
          case BodyType.Animal:
            flag = this.m_Action != 8 && this.m_Action != 12;
            break;
          case BodyType.Human:
          case BodyType.Equipment:
            flag = this.m_Action != 21 && this.m_Action != 22;
            break;
        }
        if (flag)
        {
          num |= 1;
          Frame = 0;
        }
      }
      Frames frames = owner != null ? owner.GetOwnedFrames(h, num) : h.GetAnimation(num);
      if (Frame >= frames.FrameCount || Frame < 0)
        return Frame.Empty;
      Frame frame = frames.FrameList[Frame];
      if (frame != null && frame.Image != null && !frame.Image.IsEmpty())
      {
        TextureX = Direction <= 4 ? xCenter - frame.CenterX : xCenter + (frame.CenterX - frame.Image.Width);
        TextureY = yCenter - frame.Image.Height - frame.CenterY;
      }
      frame.Image.Flip = Direction > 4;
      return frame;
    }

    public bool IsValid(int bodyID, int action, int direction)
    {
      int realId = this.GetRealID(bodyID, action, direction);
      switch (this.ConvertRealID(ref realId))
      {
        case 1:
          if (realId >= 0 && realId < this.m_Index.Length)
            return this.m_Index[realId].m_Lookup >= 0;
          return false;
        case 2:
          if (realId >= 0 && realId < this.m_Index2.Length)
            return this.m_Index2[realId].m_Lookup >= 0;
          return false;
        case 3:
          if (realId >= 0 && realId < this.m_Index3.Length)
            return this.m_Index3[realId].m_Lookup >= 0;
          return false;
        case 4:
          if (realId >= 0 && realId < this.m_Index4.Length)
            return this.m_Index4[realId].m_Lookup >= 0;
          return false;
        default:
          if (realId >= 0 && realId < this.m_Index5.Length)
            return this.m_Index5[realId].m_Lookup >= 0;
          return false;
      }
    }

    public int SafeAction(int desired, int fb1)
    {
      if (this.IsValid(this.m_SA_Body, desired, this.m_SA_Dir))
        return desired;
      return fb1;
    }

    public int SafeAction(int desired, int fb1, int fb2)
    {
      if (this.IsValid(this.m_SA_Body, desired, this.m_SA_Dir))
        return desired;
      if (this.IsValid(this.m_SA_Body, fb1, this.m_SA_Dir))
        return fb1;
      return fb2;
    }

    public int ConvertAction(int BodyID, int Serial, int X, int Y, int Direction, GenericAction g, Mobile m)
    {
      this.Translate(ref BodyID);
      BodyType bodyType = this.GetBodyType(BodyID);
      this.m_SA_Body = BodyID;
      this.m_SA_Dir = Direction;
      if (bodyType == BodyType.Monster)
      {
        switch (g)
        {
          case GenericAction.Die:
            return this.SafeAction(2 + (Direction >> 7 & 1), 2, 3);
          case GenericAction.MountedWalk:
            return 0;
          case GenericAction.MountedRun:
            return 0;
          case GenericAction.Walk:
            return 0;
          case GenericAction.Run:
            return 0;
          case GenericAction.MountedStand:
            return 1;
          case GenericAction.Stand:
            return 1;
        }
      }
      else if (bodyType == BodyType.Animal || bodyType == BodyType.Sea)
      {
        switch (g)
        {
          case GenericAction.Die:
            return this.SafeAction(8 + (Direction >> 7 & 1) * 4, 8, 12);
          case GenericAction.MountedWalk:
            return 0;
          case GenericAction.MountedRun:
            return this.SafeAction(1, 0);
          case GenericAction.Walk:
            return 0;
          case GenericAction.Run:
            return this.SafeAction(1, 0);
          case GenericAction.MountedStand:
            return 2;
          case GenericAction.Stand:
            return 2;
        }
      }
      else if (bodyType == BodyType.Human || bodyType == BodyType.Equipment)
      {
        switch (g)
        {
          case GenericAction.Die:
            return this.SafeAction(21 + (Direction >> 7 & 1), 21, 22);
          case GenericAction.MountedWalk:
            return 23;
          case GenericAction.MountedRun:
            return 24;
          case GenericAction.Walk:
            if (m != null && m.Warmode)
              return this.SafeAction(15, m.UsingTwoHandedWeapon() ? 1 : 0, 0);
            if (m.UsingTwoHandedWeapon())
              return this.SafeAction(1, 0);
            return this.SafeAction(0, 1, 15);
          case GenericAction.Run:
            if (m != null && m.UsingTwoHandedWeapon())
              return this.SafeAction(3, 2);
            return this.SafeAction(2, 3);
          case GenericAction.MountedStand:
            return 25;
          case GenericAction.Stand:
            if (m == null || !m.Warmode)
              return this.SafeAction(4, 7, 8);
            if (m.UsingTwoHandedWeapon())
              return this.SafeAction(8, 7, 4);
            return this.SafeAction(7, 8, 4);
        }
      }
      return 0;
    }

    public int ConvertMountItemToBody(int itemID)
    {
      if (this.m_MountTable == null)
        this.m_MountTable = new MountTable();
      return this.m_MountTable.Translate(itemID);
    }

    public int GetRealIDNoMap(int body, int action, int direction)
    {
      direction &= 7;
      int num = body < 400 ? (body < 200 ? body * 110 : (body - 200) * 65 + 22000) : (body - 400) * 175 + 35000;
      if (direction > 4)
        direction -= (direction - 4) * 2;
      return num + (action * 5 + direction);
    }

    public int ConvertRealID(ref int realID)
    {
      int bodyID;
      int num1;
      int num2;
      if (realID >= 35000)
      {
        bodyID = 400 + (realID - 35000) / 175;
        num1 = (realID - 35000) % 175 / 5;
        num2 = (realID - 35000) % 175 % 5;
      }
      else if (realID >= 22000)
      {
        bodyID = 200 + (realID - 22000) / 65;
        num1 = (realID - 22000) % 65 / 5;
        num2 = (realID - 22000) % 65 % 5;
      }
      else
      {
        bodyID = realID / 110;
        num1 = realID % 110 / 5;
        num2 = realID % 110 % 5;
      }
      int num3 = BodyConverter.Convert(ref bodyID);
      switch (num3)
      {
        case 2:
          realID = bodyID >= 200 ? 22000 + (bodyID - 200) * 65 + num1 * 5 + num2 : bodyID * 110 + num1 * 5 + num2;
          break;
        case 3:
          realID = bodyID >= 300 ? (bodyID >= 400 ? 35000 + (bodyID - 400) * 175 + num1 * 5 + num2 : 33000 + (bodyID - 300) * 110 + num1 * 5 + num2) : bodyID * 65 + num1 * 5 + num2;
          break;
        case 4:
          realID = bodyID >= 200 ? (bodyID >= 400 ? 35000 + (bodyID - 400) * 175 + num1 * 5 + num2 : 22000 + (bodyID - 200) * 65 + num1 * 5 + num2) : bodyID * 110 + num1 * 5 + num2;
          break;
        case 5:
          realID = bodyID >= 200 ? (bodyID >= 400 ? 35000 + (bodyID - 400) * 175 + num1 * 5 + num2 : 22000 + (bodyID - 200) * 65 + num1 * 5 + num2) : bodyID * 110 + num1 * 5 + num2;
          break;
      }
      return num3;
    }

    public int GetRealID(int BodyID, int ActionID, int Direction)
    {
      Direction &= 7;
      this.Translate(ref BodyID);
      int num = BodyID < 400 ? (BodyID < 200 ? BodyID * 110 : (BodyID - 200) * 65 + 22000) : (BodyID - 400) * 175 + 35000;
      if (Direction > 4)
        Direction -= (Direction - 4) * 2;
      return num + (ActionID * 5 + Direction);
    }

    public void UpdateInstance(long SeedID, object Anim)
    {
    }

    public void DisposeInstance(object Anim)
    {
      Frames frames = (Frames) Anim;
      if (frames != null && frames.FrameList != null)
      {
        int length = frames.FrameList.Length;
        for (int index = 0; index < length; ++index)
        {
          if (frames.FrameList[index] != null && frames.FrameList[index].Image != null)
          {
            frames.FrameList[index].Image.Dispose();
            frames.FrameList[index].Image = (Texture) null;
          }
        }
      }
      Anim = (object) null;
    }

    public unsafe Frames Create(int realID, IHue hue)
    {
      bool flag1 = (realID & 1) != 0;
      bool flag2 = (realID & 2) != 0;
      realID >>= 2;
      bool flag3 = hue is Hues.ShadowHue;
      int count;
      int num1;
      int length;
      Stream stream;
      switch (this.ConvertRealID(ref realID))
      {
        case 1:
          if (realID < 0 || realID >= this.m_Count || realID >= this.m_Index.Length)
            return Frames.Empty;
          Entry3D entry3D1 = this.m_Index[realID];
          count = entry3D1.m_Length;
          num1 = entry3D1.m_Lookup;
          length = entry3D1.m_Extra & (int) byte.MaxValue;
          stream = Animations.m_Stream;
          break;
        case 2:
          if (realID < 0 || realID >= this.m_Count2 || realID >= this.m_Index2.Length)
            return Frames.Empty;
          Entry3D entry3D2 = this.m_Index2[realID];
          count = entry3D2.m_Length;
          num1 = entry3D2.m_Lookup;
          length = entry3D2.m_Extra & (int) byte.MaxValue;
          stream = Animations.m_Stream2;
          break;
        case 3:
          if (realID < 0 || realID >= this.m_Count3 || realID >= this.m_Index3.Length)
            return Frames.Empty;
          Entry3D entry3D3 = this.m_Index3[realID];
          count = entry3D3.m_Length;
          num1 = entry3D3.m_Lookup;
          length = entry3D3.m_Extra & (int) byte.MaxValue;
          stream = Animations.m_Stream3;
          break;
        case 4:
          if (realID < 0 || realID >= this.m_Count4 || realID >= this.m_Index4.Length)
            return Frames.Empty;
          Entry3D entry3D4 = this.m_Index4[realID];
          count = entry3D4.m_Length;
          num1 = entry3D4.m_Lookup;
          length = entry3D4.m_Extra & (int) byte.MaxValue;
          stream = Animations.m_Stream4;
          break;
        default:
          if (realID < 0 || realID >= this.m_Count5 || realID >= this.m_Index5.Length)
            return Frames.Empty;
          Entry3D entry3D5 = this.m_Index5[realID];
          count = entry3D5.m_Length;
          num1 = entry3D5.m_Lookup;
          length = entry3D5.m_Extra & (int) byte.MaxValue;
          stream = Animations.m_Stream5;
          break;
      }
      if (num1 < 0 || count <= 0 || (length <= 0 || stream == null))
        return Frames.Empty;
      if (this.m_Data == null || count > this.m_Data.Length)
        this.m_Data = new byte[count];
      stream.Seek((long) num1, SeekOrigin.Begin);
      stream.Read(this.m_Data, 0, count);
      fixed (short* numPtr1 = this.m_Palette)
        fixed (int* numPtr2 = this.m_Palette32)
          fixed (byte* numPtr3 = this.m_Data)
          {
            if (!flag3)
              hue.CopyPixels((void*) numPtr3, (void*) numPtr1, 256);
            for (int index = 0; index < 256; ++index)
              numPtr2[index] = Engine.C16232((int) numPtr1[index]);
            if (flag1)
            {
              BodyType bodyType = this.GetBodyType(this.m_BodyID);
              bool flag4 = true;
              switch (bodyType)
              {
                case BodyType.Monster:
                  flag4 = this.m_Action != 2 && this.m_Action != 3;
                  break;
                case BodyType.Sea:
                case BodyType.Animal:
                  flag4 = this.m_Action != 8 && this.m_Action != 12;
                  break;
                case BodyType.Human:
                case BodyType.Equipment:
                  flag4 = this.m_Action != 21 && this.m_Action != 22;
                  break;
              }
              if (flag4)
                length = 1;
            }
            Frames frames = new Frames();
            frames.FrameCount = length;
            frames.FrameList = new Frame[length];
            for (int index1 = 0; index1 < length; ++index1)
            {
              int num2 = *(int*) (numPtr3 + 516 + (index1 << 2));
              byte* numPtr4 = numPtr3 + 512 + num2;
              short* numPtr5 = (short*) numPtr4;
              int num3 = flag3 ? 8 : 0;
              int num4 = flag3 ? 17 : 0;
              int num5 = (int) *numPtr5;
              int num6 = (int) numPtr5[1];
              int Width = (int) numPtr5[2];
              int Height = (int) numPtr5[3];
              byte* numPtr6 = numPtr4 + 8;
              frames.FrameList[index1] = new Frame();
              frames.FrameList[index1].CenterX = num5 + num3;
              frames.FrameList[index1].CenterY = num6 - num3;
              if (Width <= 0 || Height <= 0)
              {
                frames.FrameList[index1].Image = Texture.Empty;
              }
              else
              {
                Texture texture1 = new Texture(Width + num4, Height + num4, TextureTransparency.Simple);
                if (texture1.IsEmpty())
                {
                  frames.FrameList[index1].Image = Texture.Empty;
                }
                else
                {
                  texture1._shaderData = hue.ShaderData;
                  int num7 = num3 + num5 - 512;
                  int num8 = num6 + Height + num3 - 512;
                  LockData ld = texture1.Lock(LockFlags.WriteOnly);
                  short* numPtr7 = (short*) ld.pvSrc;
                  int num9 = ld.Pitch >> 1;
                  ushort* numPtr8 = (ushort*) ld.pvSrc;
                  int num10 = ld.Pitch >> 1;
                  ushort* numPtr9 = numPtr8 + num10 * ld.Height;
                  short* numPtr10 = numPtr7 + num7 + num8 * num9;
                  if (flag3)
                  {
                    ushort* numPtr11 = numPtr8;
                    while (numPtr11 < numPtr9)
                      *numPtr11++ = (ushort) 0;
                    fixed (ushort* numPtr12 = Animations._guassianBlurMatrix)
                    {
                      byte* numPtr13;
                      int num11;
                      for (; (num11 = *(int*) numPtr6) != 2147450879; numPtr6 = numPtr13 + (num11 & 4095))
                      {
                        numPtr13 = numPtr6 + 4;
                        if ((num11 >> 12 & 512) != 0)
                        {
                          num11 ^= -2145386496;
                          short* numPtr14 = numPtr10 + ((num11 >> 12 & 1023) * num9 + (num11 >> 22 & 1023));
                          for (short* numPtr15 = numPtr14 + (num11 & 4095); numPtr14 < numPtr15; ++numPtr14)
                          {
                            ushort* numPtr16 = numPtr12;
                            for (int index2 = -num3; index2 <= num3; ++index2)
                            {
                              ushort* numPtr17 = (ushort*) (numPtr14 + index2 * num9 - num3);
                              ushort* numPtr18 = numPtr17 + num4;
                              while (numPtr17 < numPtr18)
                              {
                                ushort* numPtr19 = numPtr17++;
                                int num12 = (int) (ushort) ((int) *numPtr19 + (int) *numPtr16++);
                                *numPtr19 = (ushort) num12;
                              }
                            }
                          }
                        }
                      }
                      int num13;
                      for (ushort* numPtr14 = numPtr8; numPtr14 < numPtr9; *numPtr14++ = (ushort) (32768 | num13 << 10 | num13 << 5 | num13))
                        num13 = (int) *numPtr14 * 31 / 22409;
                    }
                  }
                  else
                  {
                    byte* numPtr11;
                    int num11;
                    int num12;
                    for (; (num11 = *(int*) numPtr6) != 2147450879; numPtr6 = numPtr11 + (num12 & 3))
                    {
                      numPtr11 = numPtr6 + 4;
                      num12 = num11 ^ -2145386496;
                      short* numPtr12 = numPtr10 + ((num12 >> 12 & 1023) * num9 + (num12 >> 22 & 1023));
                      short* numPtr13 = numPtr12 + (num12 & 4092);
                      while (numPtr12 < numPtr13)
                      {
                        *numPtr12 = numPtr1[*numPtr11];
                        numPtr12[1] = numPtr1[numPtr11[1]];
                        numPtr12[2] = numPtr1[numPtr11[2]];
                        numPtr12[3] = numPtr1[numPtr11[3]];
                        numPtr12 += 4;
                        numPtr11 += 4;
                      }
                      switch (num12 & 3)
                      {
                        case 1:
                          *numPtr12 = numPtr1[*numPtr11];
                          break;
                        case 2:
                          numPtr12[1] = numPtr1[numPtr11[1]];
                          goto case 1;
                        case 3:
                          numPtr12[2] = numPtr1[numPtr11[2]];
                          goto case 2;
                      }
                    }
                    if (flag2)
                    {
                      Texture texture2 = texture1;
                      texture1 = new Texture(Width, Height, (Format) 21, TextureTransparency.Complex);
                      texture1._shaderData = hue.ShaderData;
                      LockData lockData = texture1.Lock(LockFlags.WriteOnly);
                      for (int y = 0; y < ld.Height; ++y)
                      {
                        for (int x = 0; x < ld.Width; ++x)
                        {
                          switch (Animations.GetPixelType(ld, x, y))
                          {
                            case Animations.PixelType.Inner:
                              ushort num13 = *(ushort*) ((IntPtr) ld.pvSrc + (IntPtr) (y * (ld.Pitch >> 1)) * 2 + (IntPtr) x * 2);
                              *(int*) ((IntPtr) lockData.pvSrc + (IntPtr) (y * (lockData.Pitch >> 2)) * 4 + (IntPtr) x * 4) = Engine.C16232((int) num13) | -16777216;
                              break;
                            case Animations.PixelType.Outer:
                              ushort num14 = *(ushort*) ((IntPtr) ld.pvSrc + (IntPtr) (y * (ld.Pitch >> 1)) * 2 + (IntPtr) x * 2);
                              int num15 = 0;
                              int num16 = 0;
                              int num17 = 0;
                              int num18 = 0;
                              for (int index2 = -1; index2 <= 1; ++index2)
                              {
                                for (int index3 = -1; index3 <= 1; ++index3)
                                {
                                  if (Animations.GetPixelType(ld, x + index3, y + index2) == Animations.PixelType.Inner)
                                  {
                                    ushort* numPtr12 = (ushort*) ((IntPtr) ld.pvSrc + (IntPtr) ((y + index2) * (ld.Pitch >> 1)) * 2 + (IntPtr) (x + index3) * 2);
                                    num15 += (int) *numPtr12 >> 10 & 31;
                                    num16 += (int) *numPtr12 >> 5 & 31;
                                    num17 += (int) *numPtr12 & 31;
                                    ++num18;
                                  }
                                }
                              }
                              if (num18 > 0)
                              {
                                int r = num15 * (int) byte.MaxValue / (31 * num18);
                                int g = num16 * (int) byte.MaxValue / (31 * num18);
                                int b = num17 * (int) byte.MaxValue / (31 * num18);
                                int num19 = (int) ((1.0 + 2.0 * (double) ((float) (Engine.GrayScale((int) num14) * (int) byte.MaxValue) / 31f / Engine.GrayScale(r, g, b))) / 3.0 * (double) byte.MaxValue);
                                if (num19 < 0)
                                  num19 = 0;
                                else if (num19 > (int) byte.MaxValue)
                                  num19 = (int) byte.MaxValue;
                                *(int*) ((IntPtr) lockData.pvSrc + (IntPtr) (y * (lockData.Pitch >> 2)) * 4 + (IntPtr) x * 4) = num19 << 24 | Engine.Blend32(r << 16 | g << 8 | b, Engine.C16232((int) num14), (int) sbyte.MaxValue);
                                break;
                              }
                              *(int*) ((IntPtr) lockData.pvSrc + (IntPtr) (y * (lockData.Pitch >> 2)) * 4 + (IntPtr) x * 4) = -16777216 | Engine.C16232((int) num14);
                              break;
                          }
                        }
                      }
                      texture2.Unlock();
                      texture2.Dispose();
                    }
                  }
                  texture1.Unlock();
                  texture1.SetPriority(0);
                  frames.FrameList[index1].Image = texture1;
                }
              }
            }
            this.m_Frames.Add((object) frames);
            return frames;
          }
    }

    private static unsafe Animations.PixelType GetPixelType(LockData ld, int x, int y)
    {
      if (x < 0 || x >= ld.Width || (y < 0 || y >= ld.Height) || ((int) *(ushort*) ((IntPtr) ld.pvSrc + (IntPtr) (y * (ld.Pitch >> 1)) * 2 + (IntPtr) x * 2) & 32768) == 0)
        return Animations.PixelType.None;
      for (int index1 = -1; index1 <= 1; ++index1)
      {
        for (int index2 = -1; index2 <= 1; ++index2)
        {
          if ((index2 == 0 || index1 == 0) && (x + index2 < 0 || x + index2 >= ld.Width || (y + index1 < 0 || y + index1 >= ld.Height) || ((int) *(ushort*) ((IntPtr) ld.pvSrc + (IntPtr) ((y + index1) * (ld.Pitch >> 1)) * 2 + (IntPtr) (x + index2) * 2) & 32768) == 0))
            return Animations.PixelType.Outer;
        }
      }
      return Animations.PixelType.Inner;
    }

    public void FullCleanup(int timeNow)
    {
      int num = timeNow - 15000;
      for (int index = this.m_Frames.Count - 1; index >= 0; --index)
      {
        Frames frames = (Frames) this.m_Frames[index];
        if (frames.Disposed || frames.LastAccessTime < num)
          TextureFactory.m_Disposing.Enqueue((object) frames);
      }
    }

    public void Dispose()
    {
      Animations.m_Loader.Stop();
      Animations.m_Loader2.Stop();
      Animations.m_Loader3.Stop();
      Animations.m_Loader4.Stop();
      Animations.m_Loader5.Stop();
      if (Animations.m_Stream != null)
      {
        Animations.m_Stream.Close();
        Animations.m_Stream = (Stream) null;
      }
      if (Animations.m_Stream2 != null)
      {
        Animations.m_Stream2.Close();
        Animations.m_Stream2 = (Stream) null;
      }
      if (Animations.m_Stream3 != null)
      {
        Animations.m_Stream3.Close();
        Animations.m_Stream3 = (Stream) null;
      }
      if (Animations.m_Stream4 != null)
      {
        Animations.m_Stream4.Close();
        Animations.m_Stream4 = (Stream) null;
      }
      if (Animations.m_Stream5 != null)
      {
        Animations.m_Stream5.Close();
        Animations.m_Stream5 = (Stream) null;
      }
      if (this.m_MountTable != null)
      {
        this.m_MountTable.Dispose();
        this.m_MountTable = (MountTable) null;
      }
      this.m_Table = (int[]) null;
      this.m_Data = (byte[]) null;
      this.m_Index = (Entry3D[]) null;
      this.m_Palette = (short[]) null;
    }

    public Entry3D[] GetIndex(int index)
    {
      switch (index)
      {
        case 2:
          return this.m_Index2;
        case 3:
          return this.m_Index3;
        case 4:
          return this.m_Index4;
        case 5:
          return this.m_Index5;
        default:
          return this.m_Index;
      }
    }

    public int GetCount(int index)
    {
      switch (index)
      {
        case 2:
          return this.m_Count2;
        case 3:
          return this.m_Count3;
        case 4:
          return this.m_Count4;
        case 5:
          return this.m_Count5;
        default:
          return this.m_Count;
      }
    }

    public int GetHeight(int bodyID, int actionID, int direction)
    {
      direction &= 7;
      int realId = this.GetRealID(bodyID, actionID, direction);
      int index1 = this.ConvertRealID(ref realId);
      Entry3D[] index2 = this.GetIndex(index1);
      int count = this.GetCount(index1);
      if (realId < 0 || realId >= count || realId >= index2.Length)
        return 0;
      return (index2[realId].m_Extra & -256) >> 8;
    }

    public int GetHeight(int realID)
    {
      int index1 = this.ConvertRealID(ref realID);
      Entry3D[] index2 = this.GetIndex(index1);
      int count = this.GetCount(index1);
      if (realID < 0 || realID >= count || realID >= index2.Length)
        return 0;
      return (index2[realID].m_Extra & -256) >> 8;
    }

    public int GetFrameCount(int bodyID, int actionID, int direction)
    {
      direction &= 7;
      int realId = this.GetRealID(bodyID, actionID, direction);
      int index1 = this.ConvertRealID(ref realId);
      Entry3D[] index2 = this.GetIndex(index1);
      int count = this.GetCount(index1);
      if (realId < 0 || realId >= count || realId >= index2.Length)
        return 0;
      return index2[realId].m_Extra & (int) byte.MaxValue;
    }

    public int GetFrameCount(int realID)
    {
      int index1 = this.ConvertRealID(ref realID);
      Entry3D[] index2 = this.GetIndex(index1);
      int count = this.GetCount(index1);
      if (realID < 0 || realID >= count || realID >= index2.Length)
        return 0;
      return index2[realID].m_Extra & (int) byte.MaxValue;
    }

    private class Loader
    {
      private const string RelativeApplicationDataPath = "Sallos/Ultima Online/Cache/Anim-{0}";
      private const string RelativeLegacyPath = "data/ultima/cache/anim.{0}.uoi";
      private Animations m_Owner;
      private Thread m_Thread;
      private int m_Index;

      public bool IsAlive
      {
        get
        {
          if (this.m_Thread != null)
            return this.m_Thread.IsAlive;
          return false;
        }
      }

      public Loader(Animations owner, int index)
      {
        this.m_Owner = owner;
        this.m_Index = index;
        this.m_Thread = new Thread(new ThreadStart(this.Thread_Start));
        this.m_Thread.Name = "Background Animation Loader";
      }

      public void Start()
      {
        if (this.m_Thread == null)
          return;
        this.m_Thread.Start();
      }

      public void Stop()
      {
        if (this.m_Thread == null || !this.m_Thread.IsAlive)
          return;
        this.m_Thread.Abort();
      }

      public bool Wait()
      {
        if (this.m_Thread != null)
          return this.m_Thread.Join(10);
        return true;
      }

      private static string GetCachePath(int index)
      {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), string.Format("Sallos/Ultima Online/Cache/Anim-{0}", (object) index));
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
        if (!directoryInfo.Exists)
          directoryInfo.Create();
        return path;
      }

      private unsafe void Thread_Start()
      {
        string cachePath = Animations.Loader.GetCachePath(this.m_Index);
        string path1 = Engine.FileManager.ResolveMUL(string.Format("anim{0}.mul", this.m_Index == 1 ? (object) "" : (object) this.m_Index.ToString()));
        string path2 = Engine.FileManager.ResolveMUL(string.Format("anim{0}.idx", this.m_Index == 1 ? (object) "" : (object) this.m_Index.ToString()));
        if (!File.Exists(path1) || !File.Exists(path2))
        {
          if (this.m_Index == 1)
          {
            this.m_Owner.m_Index = new Entry3D[0];
            this.m_Owner.m_Count = 0;
          }
          else if (this.m_Index == 2)
          {
            this.m_Owner.m_Index2 = new Entry3D[0];
            this.m_Owner.m_Count2 = 0;
          }
          else if (this.m_Index == 3)
          {
            this.m_Owner.m_Index3 = new Entry3D[0];
            this.m_Owner.m_Count3 = 0;
          }
          else if (this.m_Index == 4)
          {
            this.m_Owner.m_Index4 = new Entry3D[0];
            this.m_Owner.m_Count4 = 0;
          }
          else
          {
            this.m_Owner.m_Index5 = new Entry3D[0];
            this.m_Owner.m_Count5 = 0;
          }
        }
        else
        {
          if (!File.Exists(cachePath))
          {
            string str = Engine.FileManager.BasePath(string.Format("data/ultima/cache/anim.{0}.uoi", (object) this.m_Index));
            if (File.Exists(str))
            {
              try
              {
                File.Move(str, cachePath);
              }
              catch
              {
                File.Copy(str, cachePath, false);
              }
            }
          }
          if (File.Exists(cachePath))
          {
            using (FileStream fileStream = new FileStream(cachePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
              if (fileStream.Length >= 21L)
              {
                using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
                {
                  if (binaryReader.ReadBoolean())
                  {
                    DateTime timeStamp1 = Engine.GetTimeStamp(path1);
                    DateTime timeStamp2 = Engine.GetTimeStamp(path2);
                    DateTime dateTime1 = DateTime.FromFileTime(binaryReader.ReadInt64());
                    DateTime dateTime2 = DateTime.FromFileTime(binaryReader.ReadInt64());
                    if (timeStamp1 == dateTime1)
                    {
                      if (timeStamp2 == dateTime2)
                      {
                        int length = binaryReader.ReadInt32();
                        if (binaryReader.BaseStream.Length >= (long) (21 + length * 12))
                        {
                          Entry3D[] entry3DArray1 = new Entry3D[length];
                          Entry3D[] entry3DArray2 = new Entry3D[length];
                          fixed (Entry3D* entry3DPtr = entry3DArray2)
                            UnsafeMethods.ReadFile((FileStream) binaryReader.BaseStream, (void*) entry3DPtr, length * 12);
                          if (this.m_Index == 1)
                          {
                            this.m_Owner.m_Index = entry3DArray2;
                            this.m_Owner.m_Count = length;
                            return;
                          }
                          if (this.m_Index == 2)
                          {
                            this.m_Owner.m_Index2 = entry3DArray2;
                            this.m_Owner.m_Count2 = length;
                            return;
                          }
                          if (this.m_Index == 3)
                          {
                            this.m_Owner.m_Index3 = entry3DArray2;
                            this.m_Owner.m_Count3 = length;
                            return;
                          }
                          if (this.m_Index == 4)
                          {
                            this.m_Owner.m_Index4 = entry3DArray2;
                            this.m_Owner.m_Count4 = length;
                            return;
                          }
                          this.m_Owner.m_Index5 = entry3DArray2;
                          this.m_Owner.m_Count5 = length;
                          return;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          using (FileStream file = new FileStream(path2, FileMode.Open, FileAccess.Read, FileShare.Read))
          {
            int length = (int) (file.Length / 12L);
            Entry3D[] entry3DArray = new Entry3D[length];
            fixed (Entry3D* entry3DPtr1 = entry3DArray)
            {
              UnsafeMethods.ReadFile(file, (void*) entry3DPtr1, length * 12);
              using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.Read))
              {
                BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
                Entry3D* entry3DPtr2 = entry3DPtr1;
                for (Entry3D* entry3DPtr3 = entry3DPtr1 + length; entry3DPtr2 < entry3DPtr3; ++entry3DPtr2)
                {
                  if (entry3DPtr2->m_Lookup >= 0)
                  {
                    binaryReader.BaseStream.Seek((long) (entry3DPtr2->m_Lookup + 512), SeekOrigin.Begin);
                    int num1 = binaryReader.ReadInt32() & (int) byte.MaxValue;
                    int num2 = 0;
                    int num3 = -10000;
                    for (; num2 < num1; ++num2)
                    {
                      binaryReader.BaseStream.Seek((long) (entry3DPtr2->m_Lookup + 516 + (num2 << 2)), SeekOrigin.Begin);
                      binaryReader.BaseStream.Seek((long) (entry3DPtr2->m_Lookup + 514 + binaryReader.ReadInt32()), SeekOrigin.Begin);
                      int num4 = (int) binaryReader.ReadInt16();
                      int num5 = binaryReader.ReadInt32() >> 16;
                      if (num5 + num4 > num3)
                        num3 = num5 + num4;
                    }
                    entry3DPtr2->m_Extra = num1 | num3 << 8;
                  }
                }
              }
              using (FileStream fileStream = new FileStream(cachePath, FileMode.Create, FileAccess.Write, FileShare.None))
              {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
                {
                  binaryWriter.Write(false);
                  binaryWriter.Write(Engine.GetTimeStamp(path1).ToFileTime());
                  binaryWriter.Write(Engine.GetTimeStamp(path2).ToFileTime());
                  binaryWriter.Write(length);
                  UnsafeMethods.WriteFile((FileStream) binaryWriter.BaseStream, (void*) entry3DPtr1, length * 12);
                  binaryWriter.Seek(0, SeekOrigin.Begin);
                  binaryWriter.Write(true);
                }
              }
            }
            if (this.m_Index == 1)
            {
              this.m_Owner.m_Index = entry3DArray;
              this.m_Owner.m_Count = length;
            }
            else if (this.m_Index == 2)
            {
              this.m_Owner.m_Index2 = entry3DArray;
              this.m_Owner.m_Count2 = length;
            }
            else if (this.m_Index == 3)
            {
              this.m_Owner.m_Index3 = entry3DArray;
              this.m_Owner.m_Count3 = length;
            }
            else if (this.m_Index == 4)
            {
              this.m_Owner.m_Index4 = entry3DArray;
              this.m_Owner.m_Count4 = length;
            }
            else
            {
              this.m_Owner.m_Index5 = entry3DArray;
              this.m_Owner.m_Count5 = length;
            }
          }
        }
      }
    }

    private enum PixelType
    {
      None,
      Inner,
      Outer,
    }
  }
}
