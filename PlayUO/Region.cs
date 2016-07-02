// Decompiled with JetBrains decompiler
// Type: PlayUO.Region
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.IO;

namespace PlayUO
{
  public class Region
  {
    private int m_X;
    private int m_Y;
    private int m_Width;
    private int m_Height;
    private int m_StartZ;
    private int m_EndZ;
    private RegionWorld m_World;
    private static Region[] m_GuardedRegions;

    public int X
    {
      get
      {
        return this.m_X;
      }
    }

    public int Y
    {
      get
      {
        return this.m_Y;
      }
    }

    public int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    public RegionWorld World
    {
      get
      {
        return this.m_World;
      }
    }

    public static Region[] GuardedRegions
    {
      get
      {
        if (Region.m_GuardedRegions == null)
          Region.m_GuardedRegions = Region.Load("guardlines.def");
        return Region.m_GuardedRegions;
      }
      set
      {
        Region.m_GuardedRegions = value;
      }
    }

    public Region(int x, int y, int width, int height, int startZ, int endZ, RegionWorld world)
    {
      this.m_X = x;
      this.m_Y = y;
      this.m_Width = width;
      this.m_Height = height;
      this.m_StartZ = startZ;
      this.m_EndZ = endZ;
      this.m_World = world;
    }

    public Region(string line)
    {
      string[] strArray = line.Split(' ');
      this.m_X = int.Parse(strArray[0]);
      this.m_Y = int.Parse(strArray[1]);
      this.m_Width = int.Parse(strArray[2]);
      this.m_Height = int.Parse(strArray[3]);
      this.m_StartZ = int.Parse(strArray[4]);
      this.m_EndZ = int.Parse(strArray[5]);
      if (strArray.Length < 7)
        return;
      switch (strArray[6])
      {
        case "B":
          this.m_World = RegionWorld.Britannia;
          break;
        case "F":
          this.m_World = RegionWorld.Felucca;
          break;
        case "T":
          this.m_World = RegionWorld.Trammel;
          break;
        case "I":
          this.m_World = RegionWorld.Ilshenar;
          break;
        case "M":
          this.m_World = RegionWorld.Malas;
          break;
        case "K":
          this.m_World = RegionWorld.Tokuno;
          break;
      }
    }

    public static Region Find(Region[] regs, int x, int y, int z, int w)
    {
      for (int index = 0; index < regs.Length; ++index)
      {
        Region region = regs[index];
        RegionWorld regionWorld = region.m_World;
        bool flag = false;
        switch (regionWorld)
        {
          case RegionWorld.Britannia:
            flag = w == 0 || w == 1;
            break;
          case RegionWorld.Felucca:
            flag = w == 0;
            break;
          case RegionWorld.Trammel:
            flag = w == 1;
            break;
          case RegionWorld.Ilshenar:
            flag = w == 2;
            break;
          case RegionWorld.Malas:
            flag = w == 3;
            break;
          case RegionWorld.Tokuno:
            flag = w == 4;
            break;
        }
        if (flag && (long) (uint) (x - region.m_X) < (long) region.m_Width && ((long) (uint) (y - region.m_Y) < (long) region.m_Height && z >= region.m_StartZ) && z <= region.m_EndZ)
          return region;
      }
      return (Region) null;
    }

    public static Region[] Load(string path)
    {
      if (!File.Exists(path))
        return new Region[0];
      ArrayList arrayList = new ArrayList();
      try
      {
        using (StreamReader streamReader = new StreamReader(path))
        {
          string line;
          while ((line = streamReader.ReadLine()) != null)
          {
            if (line.Length != 0 && !line.StartsWith("#"))
              arrayList.Add((object) new Region(line));
          }
        }
      }
      catch
      {
      }
      return (Region[]) arrayList.ToArray(typeof (Region));
    }
  }
}
