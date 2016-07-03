// Decompiled with JetBrains decompiler
// Type: PlayUO.GRadar
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PlayUO
{
  public class GRadar : Gump, IResizable
  {
    private static List<IRadarTrackable> _trackables = new List<IRadarTrackable>();
    private static VertexCache m_vCache = new VertexCache();
    private const short BLACK = -32768;
    protected int m_Width;
    protected int m_Height;
    private static bool m_ToClose;
    private static bool m_Open;
    private static int m_xBlock;
    private static int m_yBlock;
    private static int m_xWidth;
    private static int m_yHeight;
    private static int m_World;
    private static short[] m_Colors;
    private static Texture m_Image;
    private static Texture m_Swap;
    private static MapBlock[] m_StrongReferences;
    private static BitArray m_Guarded;
    public static Mobile m_FocusMob;

    public int MinWidth
    {
      get
      {
        return 68;
      }
    }

    public int MinHeight
    {
      get
      {
        return 68;
      }
    }

    public int MaxWidth
    {
      get
      {
        return 260;
      }
    }

    public int MaxHeight
    {
      get
      {
        return 260;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        this.m_Height = value;
      }
    }

    public GRadar()
      : base(25, 25)
    {
      GRadar.m_Open = true;
      this.m_Width = 260;
      this.m_Height = 260;
      this.m_Children.Add((Gump) new GVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLResizer((IResizable) this));
      this.m_Children.Add((Gump) new GTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHVResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GHTResizer((IResizable) this));
      this.m_Children.Add((Gump) new GLVResizer((IResizable) this));
      this.m_CanDrag = true;
      this.m_QuickDrag = false;
      GRadar.m_FocusMob = (Mobile) null;
    }

    public static void RegisterTrackable(IRadarTrackable trackable)
    {
      if (GRadar._trackables.Contains(trackable))
        return;
      GRadar._trackables.Add(trackable);
    }

    public static void Open()
    {
      if (!GRadar.m_Open)
        Gumps.Desktop.Children.Add((Gump) new GRadar());
      else
        GRadar.m_FocusMob = (Mobile) null;
    }

    protected internal override void OnDispose()
    {
      GRadar.m_Open = false;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      if (!Engine.amMoving)
        return !TargetManager.IsActive;
      return false;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      if (mb != MouseButtons.Right)
        return;
      GRadar.m_ToClose = true;
    }

    protected internal override void OnMouseUp(int X, int Y, MouseButtons mb)
    {
      if (!GRadar.m_ToClose)
        return;
      Gumps.Destroy((Gump) this);
    }

    protected internal override void OnMouseEnter(int X, int Y, MouseButtons mb)
    {
      if (!GRadar.m_ToClose || mb == MouseButtons.Right)
        return;
      GRadar.m_ToClose = false;
    }

    public static void Swap()
    {
      Texture texture = GRadar.m_Image;
      GRadar.m_Image = GRadar.m_Swap;
      GRadar.m_Swap = texture;
    }

    public static short GetRadarColor(int tid)
    {
      if (GRadar.m_Colors == null)
        GRadar.LoadColors();
      return GRadar.m_Colors[tid & 16383];
    }

    public static short GetRealColor(int tid)
    {
      if (GRadar.m_Colors == null)
        GRadar.LoadColors();
      return GRadar.m_Colors[tid];
    }

    private static unsafe void Load(int x, int y, int w, int h, int world, Texture tex)
    {
      if (GRadar.m_Colors == null)
        GRadar.LoadColors();
      if (GRadar.m_StrongReferences == null || GRadar.m_StrongReferences.Length != w * h)
        GRadar.m_StrongReferences = new MapBlock[w * h];
      if (GRadar.m_Guarded == null || GRadar.m_Guarded.Length != w * h * 64)
        GRadar.m_Guarded = new BitArray(w * h * 64);
      else
        GRadar.m_Guarded.SetAll(false);
      Region[] guardedRegions = Region.GuardedRegions;
      int num1 = x * 8;
      int num2 = y * 8;
      int num3 = w * 8;
      int num4 = h * 8;
      for (int index1 = 0; index1 < guardedRegions.Length; ++index1)
      {
        Region region = guardedRegions[index1];
        RegionWorld world1 = region.World;
        bool flag = false;
        switch (world1)
        {
          case RegionWorld.Britannia:
            flag = world == 0 || world == 1;
            break;
          case RegionWorld.Felucca:
            flag = world == 0;
            break;
          case RegionWorld.Trammel:
            flag = world == 1;
            break;
          case RegionWorld.Ilshenar:
            flag = world == 2;
            break;
          case RegionWorld.Malas:
            flag = world == 3;
            break;
          case RegionWorld.Tokuno:
            flag = world == 4;
            break;
        }
        if (flag)
        {
          int num5 = region.X - num1;
          int num6 = region.Y - num2;
          if (num5 < num3 && num6 < num4 && (num5 > -region.Width && num6 > -region.Height))
          {
            int num7 = num5 + region.Width;
            int num8 = num6 + region.Height;
            if (num5 < 0)
              num5 = 0;
            if (num6 < 0)
              num6 = 0;
            for (int index2 = num5; index2 < num7 && index2 < num3; ++index2)
            {
              for (int index3 = num6; index3 < num8 && index3 < num4; ++index3)
                GRadar.m_Guarded[index3 * num3 + index2] = true;
            }
          }
        }
      }
      TileMatrix matrix = Map.GetMatrix(world);
      LockData lockData = tex.Lock(LockFlags.WriteOnly);
      int num9 = lockData.Pitch >> 1;
      fixed (short* numPtr1 = GRadar.m_Colors)
      {
        for (int index1 = 0; index1 < w; ++index1)
        {
          short* numPtr2 = (short*) ((IntPtr) lockData.pvSrc + (index1 << 3) * 2);
          for (int index2 = 0; index2 < h; ++index2)
          {
            MapBlock block = matrix.GetBlock(x + index1, y + index2);
            GRadar.m_StrongReferences[index2 * w + index1] = block;
            HuedTile[][][] huedTileArray = block == null ? matrix.EmptyStaticBlock : block.m_StaticTiles;
            Tile[] tileArray = block == null ? matrix.InvalidLandBlock : block.m_LandTiles;
            int index3 = 0;
            int num5 = 0;
            while (index3 < 8)
            {
              for (int index4 = 0; index4 < 8; ++index4)
              {
                int num6 = -255;
                int num7 = -255;
                int index5 = 0;
                int num8 = 0;
                for (int index6 = 0; index6 < huedTileArray[index4][index3].Length; ++index6)
                {
                  HuedTile huedTile = huedTileArray[index4][index3][index6];
                  int num10 = 16384 + huedTile.itemId;
                  switch (num10)
                  {
                    case 16385:
                    case 22422:
                    case 24996:
                    case 24984:
                    case 25020:
                    case 24985:
                      goto case 16385;
                    default:
                      int num11 = (int) huedTile.z;
                      int num12 = num11 + (int) Map.GetItemHeight(huedTile.itemId);
                      if (num12 > num6 || num11 > num7 && num12 >= num6)
                      {
                        num6 = num12;
                        num7 = num11;
                        index5 = num10;
                        num8 = (int) huedTile.hueId;
                        goto case 16385;
                      }
                      else
                        goto case 16385;
                  }
                }
                if ((int) tileArray[num5 + index4].z > num6 && tileArray[num5 + index4].Visible)
                {
                  index5 = (int) tileArray[num5 + index4].landId;
                  num8 = 0;
                }
                numPtr2[index4] = num8 != 0 ? (short) Hues.Load(num8 & 16383 | 32768).Pixel((ushort) numPtr1[index5]) : numPtr1[index5];
              }
              numPtr2 += num9;
              ++index3;
              num5 += 8;
            }
          }
        }
        ArrayList items = Engine.Multis.Items;
        for (int index1 = 0; index1 < items.Count; ++index1)
        {
          Item obj = (Item) items[index1];
          if (obj.InWorld)
          {
            CustomMultiEntry customMulti = CustomMultiLoader.GetCustomMulti(obj.Serial, obj.Revision);
            Multi multi = (Multi) null;
            if (customMulti != null)
              multi = customMulti.Multi;
            if (multi == null)
              multi = obj.Multi;
            if (multi != null)
            {
              short[][] radar = multi.Radar;
              if (radar != null)
              {
                int xMin;
                int yMin;
                int xMax;
                int yMax;
                multi.GetBounds(out xMin, out yMin, out xMax, out yMax);
                int index2 = 0;
                int num5 = obj.Y - (y << 3) + yMin;
                while (index2 < radar.Length)
                {
                  if (num5 >= 0 && num5 < h << 3)
                  {
                    short* numPtr2 = (short*) ((IntPtr) lockData.pvSrc +  (num5 * num9) * 2);
                    short[] numArray = radar[index2];
                    int index3 = 0;
                    int index4 = obj.X - (x << 3) + xMin;
                    while (index3 < numArray.Length)
                    {
                      if (index4 >= 0 && index4 < w << 3 && (int) numArray[index3] != 0)
                        numPtr2[index4] = numPtr1[16384 + (int) numArray[index3]];
                      ++index3;
                      ++index4;
                    }
                  }
                  ++index2;
                  ++num5;
                }
              }
            }
          }
        }
      }
      tex.Unlock();
    }

    protected internal override void Draw(int X, int Y)
    {
      Mobile mobile = GRadar.m_FocusMob == null ? World.Player : GRadar.m_FocusMob;
      if (mobile != null)
        GRadar.DrawImage(X + 2, Y + 2, this.m_Width - 4, this.m_Height - 4, mobile.Visible || mobile.Player ? mobile.X : mobile.m_KUOC_X, mobile.Visible || mobile.Player ? mobile.Y : mobile.m_KUOC_Y, mobile.Visible || mobile.Player ? Engine.m_World : mobile.m_KUOC_F);
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.25f);
      Renderer.TransparentRect(0, X + 4, Y + 4, this.m_Width - 8, this.m_Height - 8);
      Renderer.DrawLine(X, Y + 2, X, Y + this.m_Height - 2);
      Renderer.DrawLine(X + 2, Y, X + this.m_Width - 2, Y);
      Renderer.DrawLine(X + this.m_Width - 1, Y + 2, X + this.m_Width - 1, Y + this.m_Height - 2);
      Renderer.DrawLine(X + 2, Y + this.m_Height - 1, X + this.m_Width - 2, Y + this.m_Height - 1);
      Renderer.DrawPoints(new Point(X + 1, Y + 1), new Point(X + 1, Y + this.m_Height - 2), new Point(X + this.m_Width - 2, Y + 1), new Point(X + this.m_Width - 2, Y + this.m_Height - 2));
      Renderer.SetAlpha(0.5f);
      Renderer.DrawLine(X + 1, Y + 2, X + 1, Y + this.m_Height - 2);
      Renderer.DrawLine(X + 2, Y + 1, X + this.m_Width - 2, Y + 1);
      Renderer.DrawLine(X + this.m_Width - 2, Y + 2, X + this.m_Width - 2, Y + this.m_Height - 2);
      Renderer.DrawLine(X + 2, Y + this.m_Height - 2, X + this.m_Width - 2, Y + this.m_Height - 2);
      Renderer.TransparentRect(0, X + 3, Y + 3, this.m_Width - 6, this.m_Height - 6);
      Renderer.PopAlpha();
      Renderer.TransparentRect(0, X + 2, Y + 2, this.m_Width - 4, this.m_Height - 4);
    }

    public static void Dispose()
    {
      if (GRadar.m_Image != null)
      {
        GRadar.m_Image.Dispose();
        GRadar.m_Image = (Texture) null;
      }
      if (GRadar.m_Swap != null)
      {
        GRadar.m_Swap.Dispose();
        GRadar.m_Swap = (Texture) null;
      }
      GRadar.m_Colors = (short[]) null;
    }

    private static Point GetPoint(int xTile, int yTile, int xCenter, int yCenter, int xDotCenter, int yDotCenter, double xScale, double yScale)
    {
      int num1 = xTile - xCenter;
      int num2 = yTile - yCenter;
      int num3 = xDotCenter;
      int num4 = yDotCenter;
      return new Point(num3 + (int) ((double) (num1 - num2) * xScale), num4 + (int) ((double) (num1 + num2) * yScale));
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      Mobile mobile = GRadar.m_FocusMob == null ? World.Player : GRadar.m_FocusMob;
      if (mobile == null)
        return;
      int xCenter = mobile.Visible || mobile.Player ? mobile.X : mobile.m_KUOC_X;
      int yCenter = mobile.Visible || mobile.Player ? mobile.Y : mobile.m_KUOC_Y;
      int width = this.m_Width - 4;
      int height = this.m_Height - 4;
      int xDotCenter = (width >> 1) - 1;
      int yDotCenter = (int) ((double) ((GRadar.m_Image.Height >> 1) - 16) / (double) GRadar.m_Image.Height * (double) height);
      double xScale = (double) width / 256.0;
      double yScale = (double) height / 256.0;
      if (Engine.GMPrivs)
      {
        int num1 = (int) ((double) (X - xDotCenter) / xScale / 2.0);
        int num2 = (int) ((double) (Y - yDotCenter) / yScale / 2.0);
        Engine.commandEntered(string.Format("[go {0} {1}", (object) (xCenter + (num2 + num1)), (object) (yCenter + (num2 - num1))));
      }
      else
      {
        TravelAgent travelAgent = Player.Current.TravelAgent;
        RuneInfo rune1 = (RuneInfo) null;
        RunebookInfo book = (RunebookInfo) null;
        int num1 = 0;
        foreach (RunebookInfo runebook in travelAgent.Runebooks)
        {
          if (runebook.IsValid)
          {
            foreach (RuneInfo rune2 in runebook.Runes)
            {
              int xDot;
              int yDot;
              GRadar.GetDotPoint(false, rune2.Point.X, rune2.Point.Y, width, height, xCenter, yCenter, xScale, yScale, xDotCenter, yDotCenter, out xDot, out yDot);
              xDot -= X;
              yDot -= Y;
              xDot += 2;
              yDot += 2;
              int num2 = Math.Max(Math.Abs(xDot), Math.Abs(yDot));
              if (rune1 == null || num2 < num1)
              {
                rune1 = rune2;
                book = runebook;
                num1 = num2;
              }
            }
          }
        }
        if (rune1 == null)
          return;
        Mobile player = World.Player;
        bool flag = Control.ModifierKeys == (Keys.Shift | Keys.Control);
        new TravelContext(book, rune1, !flag).Enqueue();
      }
    }

    private static void DrawDot(bool onScreen, int color, int xLoc, int yLoc, int x, int y, int width, int height, int xCenter, int yCenter, double xScale, double yScale, int xDotCenter, int yDotCenter, out int xDot, out int yDot)
    {
      if (!GRadar.GetDotPoint(true, xLoc, yLoc, width, height, xCenter, yCenter, xScale, yScale, xDotCenter, yDotCenter, out xDot, out yDot) && onScreen)
        return;
      int X = xDot + x;
      int Y = yDot + y;
      Renderer.SetTexture((Texture) null);
      Renderer.SolidRect(color, X, Y, 1, 1);
      Renderer.PushAlpha(0.5f);
      Renderer.SolidRect(color, X - 1, Y, 1, 1);
      Renderer.SolidRect(color, X + 1, Y, 1, 1);
      Renderer.SolidRect(color, X, Y - 1, 1, 1);
      Renderer.SolidRect(color, X, Y + 1, 1, 1);
      Renderer.SetAlpha(0.25f);
      Renderer.SolidRect(color, X - 2, Y, 1, 1);
      Renderer.SolidRect(color, X + 2, Y, 1, 1);
      Renderer.SolidRect(color, X, Y - 2, 1, 1);
      Renderer.SolidRect(color, X, Y + 2, 1, 1);
      Renderer.SetAlpha(0.15f);
      Renderer.SolidRect(color, X - 1, Y - 1, 1, 1);
      Renderer.SolidRect(color, X + 1, Y - 1, 1, 1);
      Renderer.SolidRect(color, X - 1, Y + 1, 1, 1);
      Renderer.SolidRect(color, X + 1, Y + 1, 1, 1);
      Renderer.PopAlpha();
      if (!onScreen)
        return;
      Texture travelIcon = Engine.ImageCache.TravelIcon;
      travelIcon.DrawClipped(X - travelIcon.Width / 2 - 1, Y - travelIcon.Height / 2 - 1, Clipper.TemporaryInstance(x, y, width, height));
    }

    private static bool GetDotPoint(bool cap, int xLoc, int yLoc, int width, int height, int xCenter, int yCenter, double xScale, double yScale, int xDotCenter, int yDotCenter, out int xDot, out int yDot)
    {
      bool flag = true;
      int num1 = xLoc - xCenter;
      int num2 = yLoc - yCenter;
      xDot = xDotCenter;
      yDot = yDotCenter;
      xDot += (int) ((double) (num1 - num2) * xScale);
      yDot += (int) ((double) (num1 + num2) * yScale);
      if (cap)
      {
        if (xDot <= 1)
        {
          xDot = 2;
          flag = false;
        }
        else if (xDot >= width - 2)
        {
          xDot = width - 3;
          flag = false;
        }
        if (yDot <= 1)
        {
          yDot = 2;
          flag = false;
        }
        else if (yDot >= height - 2)
        {
          yDot = height - 3;
          flag = false;
        }
      }
      return flag;
    }

    private static void DrawTags(int x, int y, int f, int width, int height, int xCenter, int yCenter)
    {
      int xDotCenter = (width >> 1) - 1;
      int yDotCenter = (int) ((double) ((GRadar.m_Image.Height >> 1) - 16) / (double) GRadar.m_Image.Height * (double) height);
      double xScale = (double) width / 256.0;
      double yScale = (double) height / 256.0;
      foreach (RunebookInfo runebook in Player.Current.TravelAgent.Runebooks)
      {
        if (runebook.IsValid)
        {
          foreach (RuneInfo rune in runebook.Runes)
          {
            int xDot;
            int yDot;
            GRadar.DrawDot(true, 16777215, rune.Point.X, rune.Point.Y, x, y, width, height, xCenter, yCenter, xScale, yScale, xDotCenter, yDotCenter, out xDot, out yDot);
          }
        }
      }
      if (GRadar.m_FocusMob != World.Player && GRadar.m_FocusMob != null)
      {
        Mobile player = World.Player;
        if (Engine.m_World == f)
        {
          int xDot;
          int yDot;
          GRadar.DrawDot(false, 16777215, player.X, player.Y, x, y, width, height, xCenter, yCenter, xScale, yScale, xDotCenter, yDotCenter, out xDot, out yDot);
          Texture @string = Engine.GetUniFont(2).GetString("You", Hues.Bright);
          if (xDot < xDotCenter && yDot < yDotCenter)
            GRadar.m_vCache.Draw(@string, xDot + x - @string.xMin + 2, yDot + y - @string.yMin + 2);
          else if (xDot >= xDotCenter && yDot < yDotCenter)
            GRadar.m_vCache.Draw(@string, xDot + x - @string.xMax - 2, yDot + y - @string.yMin + 2);
          else if (xDot < xDotCenter && yDot >= yDotCenter)
            GRadar.m_vCache.Draw(@string, xDot + x - @string.xMin + 2, yDot + y - @string.yMax - 2);
          else if (xDot >= xDotCenter && yDot >= yDotCenter)
            GRadar.m_vCache.Draw(@string, xDot + x - @string.xMax - 2, yDot + y - @string.yMax - 2);
        }
      }
      foreach (IRadarTrackable trackable in GRadar._trackables)
      {
        if (trackable.Facet == f && !trackable.HasExpired)
        {
          int xDot;
          int yDot;
          GRadar.DrawDot(false, trackable.Color, trackable.X, trackable.Y, x, y, width, height, xCenter, yCenter, xScale, yScale, xDotCenter, yDotCenter, out xDot, out yDot);
          string name = trackable.Name;
          if (!string.IsNullOrEmpty(name))
          {
            Texture @string = Engine.GetUniFont(2).GetString(name, Hues.Bright);
            if (xDot < xDotCenter && yDot < yDotCenter)
              GRadar.m_vCache.Draw(@string, xDot + x - @string.xMin + 2, yDot + y - @string.yMin + 2);
            else if (xDot >= xDotCenter && yDot < yDotCenter)
              GRadar.m_vCache.Draw(@string, xDot + x - @string.xMax - 2, yDot + y - @string.yMin + 2);
            else if (xDot < xDotCenter && yDot >= yDotCenter)
              GRadar.m_vCache.Draw(@string, xDot + x - @string.xMin + 2, yDot + y - @string.yMax - 2);
            else if (xDot >= xDotCenter && yDot >= yDotCenter)
              GRadar.m_vCache.Draw(@string, xDot + x - @string.xMax - 2, yDot + y - @string.yMax - 2);
          }
        }
      }
    }

    public static void Invalidate()
    {
      GRadar.m_xBlock = -1;
    }

    protected static void DrawImage(int X, int Y, int Width, int Height, int xCenter, int yCenter, int world)
    {
      if (GRadar.m_Image == null)
        GRadar.m_Image = new Texture(Width, Height, TextureTransparency.None);
      int num1 = xCenter >> 3;
      int num2 = yCenter >> 3;
      int num3 = xCenter & 7;
      int num4 = yCenter & 7;
      int num5 = num3;
      int num6 = num4;
      if (GRadar.m_xBlock == num1 && GRadar.m_yBlock == num2 && (GRadar.m_World == world && GRadar.m_Image != null))
      {
        Renderer.FilterEnable = true;
        GRadar.m_Image.Draw(X, Y, Width, Height, 0.0f + (float) num5 / (float) GRadar.m_Image.Width, 0.5f + (float) num6 / (float) GRadar.m_Image.Height, 0.5f + (float) num5 / (float) GRadar.m_Image.Width, 0.0f + (float) num6 / (float) GRadar.m_Image.Height, 1f + (float) num5 / (float) GRadar.m_Image.Width, 0.5f + (float) num6 / (float) GRadar.m_Image.Height, 0.5f + (float) num5 / (float) GRadar.m_Image.Width, 1f + (float) num6 / (float) GRadar.m_Image.Height);
        Renderer.FilterEnable = false;
        int X1 = X + (Width >> 1) - 1;
        int Y1 = (int) ((double) ((GRadar.m_Image.Height >> 1) - 16) / (double) GRadar.m_Image.Height * (double) Height) + Y;
        GRadar.DrawTags(X, Y, world, Width, Height, xCenter, yCenter);
        Renderer.SetTexture((Texture) null);
        Renderer.SolidRect(16777215, X1, Y1, 1, 1);
        Renderer.PushAlpha(0.5f);
        Renderer.SolidRect(16777215, X1 - 1, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 + 1, 1, 1);
        Renderer.SetAlpha(0.25f);
        Renderer.SolidRect(16777215, X1 - 2, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 2, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 - 2, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 + 2, 1, 1);
        Renderer.SetAlpha(0.15f);
        Renderer.SolidRect(16777215, X1 - 1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1 - 1, Y1 + 1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1 + 1, 1, 1);
        Renderer.PopAlpha();
      }
      else
      {
        int x = num1 - 15;
        int y = num2 - 15;
        int w = 32;
        int h = 32;
        GRadar.m_xWidth = w;
        GRadar.m_yHeight = h;
        GRadar.m_xBlock = num1;
        GRadar.m_yBlock = num2;
        GRadar.m_World = world;
        GRadar.Load(x, y, w, h, world, GRadar.m_Image);
        if (GRadar.m_Image != null && !GRadar.m_Image.IsEmpty())
        {
          Renderer.FilterEnable = true;
          GRadar.m_Image.Draw(X, Y, Width, Height, 0.0f + (float) num5 / (float) GRadar.m_Image.Width, 0.5f + (float) num6 / (float) GRadar.m_Image.Height, 0.5f + (float) num5 / (float) GRadar.m_Image.Width, 0.0f + (float) num6 / (float) GRadar.m_Image.Height, 1f + (float) num5 / (float) GRadar.m_Image.Width, 0.5f + (float) num6 / (float) GRadar.m_Image.Height, 0.5f + (float) num5 / (float) GRadar.m_Image.Width, 1f + (float) num6 / (float) GRadar.m_Image.Height);
          Renderer.FilterEnable = false;
        }
        int X1 = X + (Width >> 1) - 1;
        int Y1 = (int) ((double) ((GRadar.m_Image.Height >> 1) - 16) / (double) GRadar.m_Image.Height * (double) Height) + Y;
        GRadar.DrawTags(X, Y, world, Width, Height, xCenter, yCenter);
        Renderer.SetTexture((Texture) null);
        Renderer.SolidRect(16777215, X1, Y1, 1, 1);
        Renderer.PushAlpha(0.5f);
        Renderer.SolidRect(16777215, X1 - 1, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 + 1, 1, 1);
        Renderer.SetAlpha(0.25f);
        Renderer.SolidRect(16777215, X1 - 2, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 2, Y1, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 - 2, 1, 1);
        Renderer.SolidRect(16777215, X1, Y1 + 2, 1, 1);
        Renderer.SetAlpha(0.15f);
        Renderer.SolidRect(16777215, X1 - 1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1 - 1, 1, 1);
        Renderer.SolidRect(16777215, X1 - 1, Y1 + 1, 1, 1);
        Renderer.SolidRect(16777215, X1 + 1, Y1 + 1, 1, 1);
        Renderer.PopAlpha();
      }
    }

    private static unsafe void LoadColors()
    {
      Debug.TimeBlock("Initializing Radar");
      GRadar.m_Colors = new short[81920];
      byte[] buffer = new byte[163840];
      Stream stream = Engine.FileManager.OpenMUL("RadarCol.mul");
      UnsafeMethods.ReadFile((FileStream) stream, buffer, 0, buffer.Length);
      stream.Close();
      fixed (byte* numPtr1 = buffer)
        fixed (short* numPtr2 = GRadar.m_Colors)
        {
          ushort* numPtr3 = (ushort*) numPtr1;
          ushort* numPtr4 = (ushort*) numPtr2;
          int num = 0;
          while (num++ < 81920)
            *numPtr4++ = (ushort) ((uint) *numPtr3++ | 32768U);
          foreach (GraphicTranslation graphicTranslation in GraphicTranslators.Art.Table.Values)
          {
            if (graphicTranslation.UpdatedId < 16384 && graphicTranslation.FallbackId < 16384)
              numPtr2[graphicTranslation.UpdatedId] = numPtr2[graphicTranslation.FallbackId];
          }
        }
      Debug.EndBlock();
    }
  }
}
