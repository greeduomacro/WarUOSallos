// Decompiled with JetBrains decompiler
// Type: PlayUO.Renderer
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;
using PlayUO.Targeting;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public class Renderer
  {
    public static int m_Version = 0;
    public static int blockWidth = 7;
    public static int blockHeight = 7;
    public static int cellWidth = Renderer.blockWidth << 3;
    public static int cellHeight = Renderer.blockHeight << 3;
    private static System.Drawing.Point[] m_PointPool = new System.Drawing.Point[4];
    private static System.Drawing.Point m_MousePoint = System.Drawing.Point.Empty;
    public static Rectangle m_FoliageCheck = new Rectangle(Engine.ScreenWidth / 2 - 22, Engine.ScreenHeight / 2 - 60, 44, 82);
    public static float _alphaValue = 1f;
    public static int _alphaBits = -16777216;
    private static Stack<float> _alphaStack = new Stack<float>();
    private static int m_CharX = -1024;
    private static int m_CharY = -1024;
    private static int m_CharZ = -1024;
    private static Type tLandTile = typeof (LandTile);
    private static Type tDynamicItem = typeof (DynamicItem);
    private static Type tStaticItem = typeof (StaticItem);
    private static Type tMobileCell = typeof (MobileCell);
    private static List<Texture>[] m_Lists = new List<Texture>[16];
    private static List<Renderer.AlphaState> m_AlphaStates = new List<Renderer.AlphaState>();
    public const int FALSE = 0;
    public const int TRUE = 1;
    public const int CF_STRETCH = 1;
    private const double r21 = 0.0476190476190476;
    private const int A_FULL = -16777216;
    private const int A_255 = -16777216;
    public const int VertexBufferLength = 32768;
    public static int m_Frames;
    public static int m_ActFrames;
    public static RenderProfile _profile;
    private static ICell m_LastFind;
    private static int xwLast;
    private static int ywLast;
    private static int zwLast;
    private static int xLast;
    private static int yLast;
    private static TransformedColoredTextured[] m_GeoPool;
    private static bool _alphaTestEnable;
    private static Texture m_Texture;
    private static bool _filterEnable;
    public static bool _alphaEnable;
    private static bool m_DrawPing;
    private static bool m_DrawFPS;
    private static bool m_DrawPCount;
    private static bool m_DrawGrid;
    private static bool m_Invalidate;
    public static Texture m_TextSurface;
    public static VertexCache m_vTextCache;
    public static bool m_DeathOverride;
    public static ArrayList m_TextToDraw;
    private static bool m_WasDead;
    private static bool m_Transparency;
    private static bool m_CullLand;
    public static int m_xScroll;
    public static int m_yScroll;
    public static int m_xWorld;
    public static int m_yWorld;
    public static int m_zWorld;
    public static int m_xBaseLast;
    public static int m_yBaseLast;
    private static int m_AlwaysHighlight;
    public static bool m_Dead;
    public static int eOffsetX;
    public static int eOffsetY;
    private static Renderer.ScreenshotContext screenshotContext;
    private static Queue m_TransDrawQueue;
    private static Queue m_ToUpdateQueue;
    private static Queue m_MiniHealthQueue;
    private static ArrayList m_TextToDrawList;
    private static ArrayList m_RectsList;
    private static TransformedColoredTextured[] m_vMultiPool;
    private static TransformedColoredTextured[] m_vTransDrawPool;
    private static int m_xServerStart;
    private static int m_yServerStart;
    private static int m_xServerEnd;
    private static int m_yServerEnd;
    private static MapSubgroup[] _mapSubgroups;
    public static bool _timeRefresh;
    private static Texture lightTexture;
    private static Surface lightSurface;
    private static int _drawGroup;
    public static int m_Count;
    private static DrawBlendType _blendType;
    public static int _renderCount;
    private static DrawBlendType m_CurBlendType;
    private static bool m_CurAlphaTest;
    private static BaseTexture _tex0;
    private static BaseTexture _tex1;
    private static PixelShader _psh;
    private static BufferedVertexStream m_VertexStream;
    private static int m_AlphaStateCount;
    private static Surface _backBuffer;
    private static Renderer.ObjectFormat[] _formats;
    internal static IndexBuffer _currentIndexBuffer;
    private static bool m_CurFilter;

    public static bool FilterEnable
    {
      get
      {
        return Renderer._filterEnable;
      }
      set
      {
        Renderer._filterEnable = value;
      }
    }

    public static bool DrawGrid
    {
      get
      {
        if (Engine.GMPrivs)
          return Renderer.m_DrawGrid;
        return false;
      }
      set
      {
        Renderer.m_DrawGrid = value;
      }
    }

    public static bool DrawPCount
    {
      get
      {
        return Renderer.m_DrawPCount;
      }
      set
      {
        Renderer.m_DrawPCount = value;
      }
    }

    public static bool DrawPing
    {
      get
      {
        return Renderer.m_DrawPing;
      }
      set
      {
        Renderer.m_DrawPing = value;
      }
    }

    public static bool DrawFPS
    {
      get
      {
        return Renderer.m_DrawFPS;
      }
      set
      {
        Renderer.m_DrawFPS = value;
      }
    }

    public static bool Transparency
    {
      get
      {
        return Renderer.m_Transparency;
      }
      set
      {
        Renderer.m_Transparency = value;
      }
    }

    public static int AlwaysHighlight
    {
      get
      {
        return Renderer.m_AlwaysHighlight;
      }
      set
      {
        Renderer.m_AlwaysHighlight = value;
      }
    }

    public static bool ScreenshotMode
    {
      get
      {
        return Renderer.screenshotContext != null;
      }
    }

    public static Rectangle ServerBoundary
    {
      set
      {
        Renderer.m_xServerStart = value.X;
        Renderer.m_yServerStart = value.Y;
        Renderer.m_xServerEnd = value.Right;
        Renderer.m_yServerEnd = value.Bottom;
      }
    }

    public static ICell FindTileFromXY(int mx, int my, ref int TileX, ref int TileY)
    {
      return Renderer.FindTileFromXY(mx, my, ref TileX, ref TileY, false);
    }

    public static void ResetHitTest()
    {
      Renderer.m_LastFind = (ICell) null;
      Renderer.xwLast = 0;
      Renderer.ywLast = 0;
      Renderer.zwLast = 0;
      Renderer.xLast = 0;
      Renderer.yLast = 0;
    }

    public static bool LandTileHitTest(System.Drawing.Point[] points, System.Drawing.Point check)
    {
      int y1 = points[0].Y;
      int y2 = points[2].Y;
      if (check.Y < points[0].Y || check.Y > points[2].Y)
        return false;
      int num1 = check.X - points[3].X;
      int num2;
      int num3;
      if (num1 >= 0 && num1 < 22)
      {
        double num4 = 1.0 / 21.0 * (double) num1;
        num2 = points[3].Y + (int) ((double) (points[0].Y - points[3].Y) * num4);
        num3 = points[3].Y + (int) ((double) (points[2].Y - points[3].Y) * num4);
      }
      else
      {
        if (num1 < 22 || num1 >= 44)
          return false;
        double num4 = 1.0 / 21.0 * (double) (num1 - 22);
        num2 = points[0].Y + (int) ((double) (points[1].Y - points[0].Y) * num4);
        num3 = points[2].Y + (int) ((double) (points[1].Y - points[2].Y) * num4);
      }
      if (check.Y >= num2)
        return check.Y <= num3;
      return false;
    }

    private static void Fix(ref int v, int cap)
    {
      if (v < 0)
      {
        v = 0;
      }
      else
      {
        if (v <= cap)
          return;
        v = cap;
      }
    }

    public static bool SetViewport(int x, int y, int w, int h)
    {
      Renderer.PushAll();
      Viewport viewport = (Viewport) null;
      viewport.MinDepth = (__Null) 0.0;
      viewport.MaxDepth = (__Null) 1.0;
      int v1 = x;
      int v2 = y;
      int v3 = x + w;
      int v4 = y + h;
      Renderer.Fix(ref v1, Engine.ScreenWidth);
      Renderer.Fix(ref v2, Engine.ScreenHeight);
      Renderer.Fix(ref v3, Engine.ScreenWidth);
      Renderer.Fix(ref v4, Engine.ScreenHeight);
      viewport.X = (__Null) v1;
      viewport.Y = (__Null) v2;
      viewport.Width = (__Null) (v3 - v1);
      viewport.Height = (__Null) (v4 - v2);
      if (viewport.Width == null || viewport.Height == null)
        return false;
      Engine.m_Device.set_Viewport(viewport);
      return true;
    }

    public static ICell FindTileFromXY(int mx, int my, ref int TileX, ref int TileY, bool onlyMobs)
    {
      if (World.Serial == 0)
        return (ICell) null;
      Renderer.m_MousePoint.X = mx;
      Renderer.m_MousePoint.Y = my;
      Mobile player = World.Player;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (player != null)
      {
        num1 = player.X;
        num2 = player.Y;
        num3 = player.Z;
      }
      if (Engine.m_Ingame && mx >= Engine.GameX && (my >= Engine.GameY && mx < Engine.GameX + Engine.GameWidth) && my < Engine.GameY + Engine.GameHeight)
      {
        MapPackage map = Map.GetMap((num1 >> 3) - (Renderer.blockWidth >> 1), (num2 >> 3) - (Renderer.blockHeight >> 1), Renderer.blockWidth, Renderer.blockHeight);
        int num4 = (num1 >> 3) - (Renderer.blockWidth >> 1) << 3;
        int num5 = (num2 >> 3) - (Renderer.blockHeight >> 1) << 3;
        ArrayList[,] arrayListArray = map.cells;
        int num6 = num1 & 7;
        int num7 = num2 & 7;
        int index1 = Renderer.blockWidth / 2 * 8 + num6;
        int index2 = Renderer.blockHeight / 2 * 8 + num7;
        int num8 = 0;
        int num9 = (Engine.GameWidth >> 1) - 22 + (4 - num6) * 22 - (4 - num7) * 22;
        int num10 = num8 + (4 - num6) * 22 + (4 - num7) * 22 + (num3 << 2) + ((Engine.GameHeight >> 1) - (index1 + index2) * 22 - (4 - num7) * 22 - (4 - num6) * 22 - 22);
        int num11 = num9 - 1;
        int num12 = num10 - 1;
        int num13 = num11 + Engine.GameX;
        int num14 = num12 + Engine.GameY;
        int num15 = int.MaxValue;
        int num16 = int.MaxValue;
        int count1 = arrayListArray[index1 + 1, index2 + 1].Count;
        for (int index3 = 0; index3 < count1; ++index3)
        {
          ICell cell = (ICell) arrayListArray[index1 + 1, index2 + 1][index3];
          Type cellType = cell.CellType;
          if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem)
          {
            ITile tile = (ITile) cell;
            if (Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L] && (int) tile.Z >= num3 + 15 && (int) tile.Z < num16)
              num16 = (int) tile.Z;
          }
        }
        int count2 = arrayListArray[index1, index2].Count;
        for (int index3 = 0; index3 < count2; ++index3)
        {
          ICell cell = (ICell) arrayListArray[index1, index2][index3];
          Type cellType = cell.CellType;
          if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem || cellType == Renderer.tLandTile)
          {
            ITile tile = (ITile) cell;
            if (!Map.GetTileFlags((int) tile.ID)[(TileFlag) 268435456L])
            {
              int num17 = cellType == Renderer.tLandTile ? (int) tile.SortZ : (int) tile.Z;
              if (num17 >= num3 + 15)
              {
                if (cellType == Renderer.tLandTile)
                {
                  if (num17 < num16)
                    num16 = num17;
                  if (num3 + 16 < num15)
                    num15 = num3 + 16;
                }
                else if (num17 < num16)
                  num16 = num17;
              }
            }
          }
        }
        ICell cell1 = Renderer.m_LastFind;
        if (cell1 != null && Renderer.xwLast == num1 && (Renderer.ywLast == num2 && Renderer.zwLast == num3))
        {
          Type cellType1 = cell1.CellType;
          if ((onlyMobs ? (cellType1 == Renderer.tMobileCell ? 1 : 0) : 1) != 0)
          {
            int num17 = (Renderer.xLast - Renderer.yLast) * 22;
            int num18 = (Renderer.xLast + Renderer.yLast) * 22;
            if (cellType1 == Renderer.tMobileCell)
            {
              if (num16 >= int.MaxValue || (int) cell1.Z < num16)
              {
                IAnimatedCell animatedCell = (IAnimatedCell) cell1;
                int Body = 0;
                int Direction = 0;
                int Hue = 0;
                int Action = 0;
                int Frame = 0;
                animatedCell.GetPackage(ref Body, ref Action, ref Direction, ref Frame, ref Hue);
                int num19 = num17 + 22;
                int num20 = num18 - ((int) animatedCell.Z << 2) + 22;
                int xCenter = num19 + 1;
                int yCenter = num20 - 2;
                Mobile mobile = ((MobileCell) cell1).m_Mobile;
                if (mobile != null)
                {
                  IHue h = !mobile.Flags[MobileFlag.Hidden] ? (Engine.m_Highlight != mobile ? Hues.Load(Hue) : Hues.GetNotoriety(mobile.Notoriety)) : Hues.Grayscale;
                  int TextureX = 0;
                  int TextureY = 0;
                  Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame, xCenter, yCenter, h, ref TextureX, ref TextureY, false);
                  if (frame.Image != null && !frame.Image.IsEmpty())
                  {
                    TextureX += num13;
                    int num21 = TextureY + num14;
                    int num22 = TextureX;
                    int num23 = num21;
                    if (mx >= num22 && my >= num23 && (mx < num22 + frame.Image.Width && my < num23 + frame.Image.Height))
                    {
                      if (frame.Image.Flip)
                      {
                        if (frame.Image.HitTest(-(mx - num22), my - num23))
                        {
                          TileX = mobile.X;
                          TileY = mobile.Y;
                          return cell1;
                        }
                      }
                      else if (frame.Image.HitTest(mx - num22, my - num23))
                      {
                        TileX = mobile.X;
                        TileY = mobile.Y;
                        return cell1;
                      }
                    }
                  }
                }
              }
            }
            else if (cellType1 == Renderer.tStaticItem || cellType1 == Renderer.tDynamicItem)
            {
              IItem obj1 = (IItem) cell1;
              if ((int) obj1.ID != 16385 && (int) obj1.ID != 22422 && ((int) obj1.ID != 24996 && (int) obj1.ID != 24984) && ((int) obj1.ID != 25020 && (int) obj1.ID != 24985) && (num16 >= int.MaxValue || (int) cell1.Z < num16 && !Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 268435456L]) && (!Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 131072L] || Renderer.xLast < index1 || (Renderer.yLast < index2 || Renderer.xLast >= index1 + 8) || Renderer.yLast >= index1 + 8))
              {
                if (obj1.CellType == Renderer.tDynamicItem)
                {
                  Item obj2 = ((DynamicItem) obj1).m_Item;
                  if (obj2 != null && obj2.ID == 8198)
                  {
                    int amount = obj2.Amount;
                    int num19 = GraphicTranslators.Corpse.Convert(amount);
                    int animDirection = Engine.GetAnimDirection(obj2.Direction);
                    int num20 = Engine.m_Animations.ConvertAction(num19, obj2.Serial, obj2.X, obj2.Y, animDirection, GenericAction.Die, (Mobile) null);
                    int frameCount = Engine.m_Animations.GetFrameCount(num19, num20, animDirection);
                    int xCenter = num17 + 23;
                    int yCenter = num18 - (obj2.Z << 2) + 20;
                    IHue @default = Hues.Default;
                    int TextureX = 0;
                    int TextureY = 0;
                    Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) obj2, num19, num20, animDirection, frameCount - 1, xCenter, yCenter, @default, ref TextureX, ref TextureY, true);
                    int num21 = TextureX + num13;
                    int num22 = TextureY + num14;
                    int num23 = num21;
                    int num24 = num22;
                    if (mx >= num23 && my >= num24 && (mx < num23 + frame.Image.Width && my < num24 + frame.Image.Height))
                    {
                      if (frame.Image.Flip)
                      {
                        if (frame.Image.HitTest(-(mx - num23), my - num24))
                        {
                          TileX = obj2.X;
                          TileY = obj2.Y;
                          return cell1;
                        }
                        goto label_105;
                      }
                      else
                      {
                        if (frame.Image.HitTest(mx - num23, my - num24))
                        {
                          TileX = obj2.X;
                          TileY = obj2.Y;
                          return cell1;
                        }
                        goto label_105;
                      }
                    }
                    else
                      goto label_105;
                  }
                }
                int id = (int) obj1.ID & 16383;
                bool xDouble = false;
                int index3;
                if (cellType1 == Renderer.tStaticItem)
                {
                  index3 = Map.GetDispID(id, 0, ref xDouble);
                }
                else
                {
                  Item obj2 = ((DynamicItem) cell1).m_Item;
                  index3 = obj2 != null ? Map.GetDispID(id, obj2.Amount, ref xDouble) : Map.GetDispID(id, 0, ref xDouble);
                }
                AnimData anim = Map.GetAnim(index3);
                Texture texture = (int) anim.frameCount == 0 || !Map.m_ItemFlags[index3][(TileFlag) 16777216L] ? Hues.Default.GetItem(index3) : Hues.Default.GetItem(index3 + (int) anim[Renderer.m_Frames / ((int) anim.frameInterval + 1) % (int) anim.frameCount]);
                if (texture != null && !texture.IsEmpty())
                {
                  int num19 = num17 + 22;
                  int num20 = num18 - ((int) cell1.Z << 2) + 43;
                  int num21 = num19 - (texture.Width >> 1);
                  int num22 = num20 - texture.Height;
                  int num23 = num21 + num13;
                  int num24 = num22 + num14;
                  if (xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width + 5) && my < num24 + texture.Height + 5)
                  {
                    mx -= num23;
                    my -= num24;
                    if (mx < texture.Width && my < texture.Height && texture.HitTest(mx, my) || mx >= 5 && my >= 5 && texture.HitTest(mx - 5, my - 5))
                    {
                      TileX = (int) (short) (num4 + Renderer.xLast);
                      TileY = (int) (short) (num5 + Renderer.yLast);
                      return cell1;
                    }
                    mx += num23;
                    my += num24;
                  }
                  else if (!xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width) && (my < num24 + texture.Height && texture.HitTest(mx - num23, my - num24)))
                  {
                    TileX = (int) (short) (num4 + Renderer.xLast);
                    TileY = (int) (short) (num5 + Renderer.yLast);
                    return cell1;
                  }
                }
              }
            }
            else if (cellType1 == Renderer.tLandTile)
            {
              LandTile landTile = (LandTile) cell1;
              int num19 = (int) landTile.m_Z;
              if ((num15 >= int.MaxValue || (int) landTile.SortZ < num15) && (int) landTile.m_ID != 2)
              {
                int num20 = num17 + num13;
                int num21 = num18 + num14;
                if (mx >= num20 && mx < num20 + 44)
                {
                  Renderer.m_PointPool[0].X = num20 + 22;
                  Renderer.m_PointPool[0].Y = num21 - 4 * (int) landTile.z00;
                  Renderer.m_PointPool[1].X = num20 + 44;
                  Renderer.m_PointPool[1].Y = num21 + 22 - 4 * (int) landTile.z10;
                  Renderer.m_PointPool[2].X = num20 + 22;
                  Renderer.m_PointPool[2].Y = num21 + 44 - 4 * (int) landTile.z11;
                  Renderer.m_PointPool[3].X = num20;
                  Renderer.m_PointPool[3].Y = num21 + 22 - 4 * (int) landTile.z01;
                  if (Renderer.LandTileHitTest(Renderer.m_PointPool, Renderer.m_MousePoint))
                  {
                    TileX = (int) (short) (num4 + Renderer.xLast);
                    TileY = (int) (short) (num5 + Renderer.yLast);
                    return cell1;
                  }
                }
              }
            }
          }
          else
          {
            for (int index3 = Renderer.xLast + 6; index3 >= Renderer.xLast - 6; --index3)
            {
              for (int index4 = Renderer.yLast + 6; index4 >= Renderer.yLast - 6; --index4)
              {
                if (index3 >= 0 && index4 >= 0 && (index3 < Renderer.cellWidth - 1 && index4 < Renderer.cellHeight - 1))
                {
                  int num17 = (index3 - index4) * 22;
                  int num18 = (index3 + index4) * 22;
                  for (int index5 = arrayListArray[index3, index4].Count - 1; index5 >= 0; --index5)
                  {
                    ICell cell2 = (ICell) arrayListArray[index3, index4][index5];
                    Type cellType2 = cell2.CellType;
                    if (cellType2 == Renderer.tMobileCell)
                    {
                      if (num16 >= int.MaxValue || (int) cell2.Z < num16)
                      {
                        IAnimatedCell animatedCell = (IAnimatedCell) cell2;
                        int Body = 0;
                        int Direction = 0;
                        int Hue = 0;
                        int Action = 0;
                        int Frame = 0;
                        animatedCell.GetPackage(ref Body, ref Action, ref Direction, ref Frame, ref Hue);
                        int num19 = num17 + 22;
                        int num20 = num18 - ((int) animatedCell.Z << 2) + 22;
                        int xCenter = num19 + 1;
                        int yCenter = num20 - 2;
                        Mobile mobile = ((MobileCell) cell2).m_Mobile;
                        IHue h = !mobile.Flags[MobileFlag.Hidden] ? (Engine.m_Highlight != mobile ? Hues.Load(Hue) : Hues.GetNotoriety(mobile.Notoriety)) : Hues.Grayscale;
                        int TextureX = 0;
                        int TextureY = 0;
                        Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame, xCenter, yCenter, h, ref TextureX, ref TextureY, false);
                        if (frame.Image != null && !frame.Image.IsEmpty())
                        {
                          TextureX += num13;
                          int num21 = TextureY + num14;
                          int num22 = TextureX;
                          int num23 = num21;
                          if (mx >= num22 && my >= num23 && (mx < num22 + frame.Image.Width && my < num23 + frame.Image.Height))
                          {
                            if (frame.Image.Flip)
                            {
                              if (frame.Image.HitTest(-(mx - num22), my - num23))
                              {
                                TileX = (int) (short) (num4 + index3);
                                TileY = (int) (short) (num5 + index4);
                                Renderer.m_LastFind = cell2;
                                Renderer.xLast = index3;
                                Renderer.yLast = index4;
                                return cell2;
                              }
                            }
                            else if (frame.Image.HitTest(mx - num22, my - num23))
                            {
                              TileX = (int) (short) (num4 + index3);
                              TileY = (int) (short) (num5 + index4);
                              Renderer.m_LastFind = cell2;
                              Renderer.xLast = index3;
                              Renderer.yLast = index4;
                              return cell2;
                            }
                          }
                        }
                      }
                    }
                    else if (cellType2 == Renderer.tStaticItem || cellType2 == Renderer.tDynamicItem)
                    {
                      IItem obj1 = (IItem) cell2;
                      if ((int) obj1.ID != 16385 && (int) obj1.ID != 22422 && ((int) obj1.ID != 24996 && (int) obj1.ID != 24984) && ((int) obj1.ID != 25020 && (int) obj1.ID != 24985) && (num16 >= int.MaxValue || (int) cell2.Z < num16 && !Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 268435456L]) && (!Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 131072L] || index3 < index1 || (index4 < index2 || index3 >= index1 + 8) || index4 >= index1 + 8))
                      {
                        if (obj1.CellType == Renderer.tDynamicItem)
                        {
                          Item obj2 = ((DynamicItem) obj1).m_Item;
                          if (obj2 != null && obj2.ID == 8198)
                          {
                            int amount = obj2.Amount;
                            int num19 = GraphicTranslators.Corpse.Convert(amount);
                            int animDirection = Engine.GetAnimDirection(obj2.Direction);
                            int num20 = Engine.m_Animations.ConvertAction(num19, obj2.Serial, obj2.X, obj2.Y, animDirection, GenericAction.Die, (Mobile) null);
                            int frameCount = Engine.m_Animations.GetFrameCount(num19, num20, animDirection);
                            int xCenter = num17 + 23;
                            int yCenter = num18 - (obj2.Z << 2) + 20;
                            IHue @default = Hues.Default;
                            int TextureX = 0;
                            int TextureY = 0;
                            Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) obj2, num19, num20, animDirection, frameCount - 1, xCenter, yCenter, @default, ref TextureX, ref TextureY, true);
                            int num21 = TextureX + num13;
                            int num22 = TextureY + num14;
                            int num23 = num21;
                            int num24 = num22;
                            if (mx >= num23 && my >= num24 && (mx < num23 + frame.Image.Width && my < num24 + frame.Image.Height))
                            {
                              if (frame.Image.Flip)
                              {
                                if (frame.Image.HitTest(-(mx - num23), my - num24))
                                {
                                  TileX = obj2.X;
                                  TileY = obj2.Y;
                                  Renderer.m_LastFind = cell2;
                                  Renderer.xLast = index3;
                                  Renderer.yLast = index4;
                                  return cell2;
                                }
                                continue;
                              }
                              if (frame.Image.HitTest(mx - num23, my - num24))
                              {
                                TileX = obj2.X;
                                TileY = obj2.Y;
                                Renderer.m_LastFind = cell2;
                                Renderer.xLast = index3;
                                Renderer.yLast = index4;
                                return cell2;
                              }
                              continue;
                            }
                            continue;
                          }
                        }
                        int id = (int) obj1.ID & 16383;
                        bool xDouble = false;
                        int index6;
                        if (cellType2 == Renderer.tStaticItem)
                        {
                          index6 = Map.GetDispID(id, 0, ref xDouble);
                        }
                        else
                        {
                          Item obj2 = ((DynamicItem) cell2).m_Item;
                          index6 = obj2 != null ? Map.GetDispID(id, obj2.Amount, ref xDouble) : Map.GetDispID(id, 0, ref xDouble);
                        }
                        AnimData anim = Map.GetAnim(index6);
                        Texture texture = (int) anim.frameCount == 0 || !Map.m_ItemFlags[index6][(TileFlag) 16777216L] ? Hues.Default.GetItem(index6) : Hues.Default.GetItem(index6 + (int) anim[Renderer.m_Frames / ((int) anim.frameInterval + 1) % (int) anim.frameCount]);
                        if (texture != null && !texture.IsEmpty())
                        {
                          int num19 = num17 + 22;
                          int num20 = num18 - ((int) cell2.Z << 2) + 43;
                          int num21 = num19 - (texture.Width >> 1);
                          int num22 = num20 - texture.Height;
                          int num23 = num21 + num13;
                          int num24 = num22 + num14;
                          if (xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width + 5) && my < num24 + texture.Height + 5)
                          {
                            mx -= num23;
                            my -= num24;
                            if (mx < texture.Width && my < texture.Height && texture.HitTest(mx, my) || mx >= 5 && my >= 5 && texture.HitTest(mx - 5, my - 5))
                            {
                              TileX = (int) (short) (num4 + index3);
                              TileY = (int) (short) (num5 + index4);
                              Renderer.m_LastFind = cell2;
                              Renderer.xLast = index3;
                              Renderer.yLast = index4;
                              return cell2;
                            }
                            mx += num23;
                            my += num24;
                          }
                          else if (!xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width) && (my < num24 + texture.Height && texture.HitTest(mx - num23, my - num24)))
                          {
                            TileX = (int) (short) (num4 + index3);
                            TileY = (int) (short) (num5 + index4);
                            Renderer.m_LastFind = cell2;
                            Renderer.xLast = index3;
                            Renderer.yLast = index4;
                            return cell2;
                          }
                        }
                      }
                    }
                    else if (cellType2 == Renderer.tLandTile)
                    {
                      LandTile landTile = (LandTile) cell2;
                      int num19 = (int) landTile.m_Z;
                      if ((int) landTile.m_ID != 2 && (num15 >= int.MaxValue || (int) landTile.SortZ < num15))
                      {
                        int num20 = num17 + num13;
                        int num21 = num18 + num14;
                        if (mx >= num20 && mx < num20 + 44)
                        {
                          Renderer.m_PointPool[0].X = num20 + 22;
                          Renderer.m_PointPool[0].Y = num21 - 4 * (int) landTile.z00;
                          Renderer.m_PointPool[1].X = num20 + 44;
                          Renderer.m_PointPool[1].Y = num21 + 22 - 4 * (int) landTile.z10;
                          Renderer.m_PointPool[2].X = num20 + 22;
                          Renderer.m_PointPool[2].Y = num21 + 44 - 4 * (int) landTile.z11;
                          Renderer.m_PointPool[3].X = num20;
                          Renderer.m_PointPool[3].Y = num21 + 22 - 4 * (int) landTile.z01;
                          if (Renderer.LandTileHitTest(Renderer.m_PointPool, Renderer.m_MousePoint))
                          {
                            TileX = (int) (short) (num4 + index3);
                            TileY = (int) (short) (num5 + index4);
                            Renderer.m_LastFind = cell2;
                            Renderer.xLast = index3;
                            Renderer.yLast = index4;
                            return cell2;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
label_105:
        Renderer.m_LastFind = (ICell) null;
        Renderer.xLast = -100;
        Renderer.yLast = -100;
        Renderer.xwLast = num1;
        Renderer.ywLast = num2;
        Renderer.zwLast = num3;
        try
        {
          for (int index3 = Renderer.cellWidth - 2; index3 >= 0; --index3)
          {
            for (int index4 = Renderer.cellHeight - 2; index4 >= 0; --index4)
            {
              int num17 = (index3 - index4) * 22;
              int num18 = (index3 + index4) * 22;
              for (int index5 = arrayListArray[index3, index4].Count - 1; index5 >= 0; --index5)
              {
                ICell cell2 = (ICell) arrayListArray[index3, index4][index5];
                Type cellType = cell2.CellType;
                if (cellType == Renderer.tMobileCell)
                {
                  if (num16 >= int.MaxValue || (int) cell2.Z < num16)
                  {
                    IAnimatedCell animatedCell = (IAnimatedCell) cell2;
                    int Body = 0;
                    int Direction = 0;
                    int Hue = 0;
                    int Action = 0;
                    int Frame = 0;
                    animatedCell.GetPackage(ref Body, ref Action, ref Direction, ref Frame, ref Hue);
                    int num19 = num17 + 22;
                    int num20 = num18 - ((int) animatedCell.Z << 2) + 22;
                    int xCenter = num19 + 1;
                    int yCenter = num20 - 2;
                    Mobile mobile = ((MobileCell) cell2).m_Mobile;
                    IHue h = !mobile.Flags[MobileFlag.Hidden] ? (Engine.m_Highlight != mobile ? Hues.Load(Hue) : Hues.GetNotoriety(mobile.Notoriety)) : Hues.Grayscale;
                    int TextureX = 0;
                    int TextureY = 0;
                    Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame, xCenter, yCenter, h, ref TextureX, ref TextureY, false);
                    if (frame.Image != null && !frame.Image.IsEmpty())
                    {
                      TextureX += num13;
                      int num21 = TextureY + num14;
                      int num22 = TextureX;
                      int num23 = num21;
                      if (mx >= num22 && my >= num23 && (mx < num22 + frame.Image.Width && my < num23 + frame.Image.Height))
                      {
                        if (frame.Image.Flip)
                        {
                          if (frame.Image.HitTest(-(mx - num22), my - num23))
                          {
                            TileX = (int) (short) (num4 + index3);
                            TileY = (int) (short) (num5 + index4);
                            Renderer.m_LastFind = cell2;
                            Renderer.xLast = index3;
                            Renderer.yLast = index4;
                            return cell2;
                          }
                        }
                        else if (frame.Image.HitTest(mx - num22, my - num23))
                        {
                          TileX = (int) (short) (num4 + index3);
                          TileY = (int) (short) (num5 + index4);
                          Renderer.m_LastFind = cell2;
                          Renderer.xLast = index3;
                          Renderer.yLast = index4;
                          return cell2;
                        }
                      }
                    }
                  }
                }
                else if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem)
                {
                  IItem obj1 = (IItem) cell2;
                  if ((int) obj1.ID != 16385 && (int) obj1.ID != 22422 && ((int) obj1.ID != 24996 && (int) obj1.ID != 24984) && ((int) obj1.ID != 25020 && (int) obj1.ID != 24985) && (num16 >= int.MaxValue || (int) cell2.Z < num16 && !Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 268435456L]) && (!Map.m_ItemFlags[(int) obj1.ID & 16383][(TileFlag) 131072L] || index3 < index1 || (index4 < index2 || index3 >= index1 + 8) || index4 >= index1 + 8))
                  {
                    if (obj1.CellType == Renderer.tDynamicItem)
                    {
                      Item obj2 = ((DynamicItem) obj1).m_Item;
                      if (obj2 != null && obj2.ID == 8198)
                      {
                        int amount = obj2.Amount;
                        int num19 = GraphicTranslators.Corpse.Convert(amount);
                        int animDirection = Engine.GetAnimDirection(obj2.Direction);
                        int num20 = Engine.m_Animations.ConvertAction(num19, obj2.Serial, obj2.X, obj2.Y, animDirection, GenericAction.Die, (Mobile) null);
                        int frameCount = Engine.m_Animations.GetFrameCount(num19, num20, animDirection);
                        int xCenter = num17 + 23;
                        int yCenter = num18 - (obj2.Z << 2) + 20;
                        IHue @default = Hues.Default;
                        int TextureX = 0;
                        int TextureY = 0;
                        Frame frame = Engine.m_Animations.GetFrame((IAnimationOwner) obj2, num19, num20, animDirection, frameCount - 1, xCenter, yCenter, @default, ref TextureX, ref TextureY, true);
                        int num21 = TextureX + num13;
                        int num22 = TextureY + num14;
                        int num23 = num21;
                        int num24 = num22;
                        if (mx >= num23 && my >= num24 && (mx < num23 + frame.Image.Width && my < num24 + frame.Image.Height))
                        {
                          if (frame.Image.Flip)
                          {
                            if (frame.Image.HitTest(-(mx - num23), my - num24))
                            {
                              TileX = obj2.X;
                              TileY = obj2.Y;
                              Renderer.m_LastFind = cell2;
                              Renderer.xLast = index3;
                              Renderer.yLast = index4;
                              return cell2;
                            }
                            continue;
                          }
                          if (frame.Image.HitTest(mx - num23, my - num24))
                          {
                            TileX = obj2.X;
                            TileY = obj2.Y;
                            Renderer.m_LastFind = cell2;
                            Renderer.xLast = index3;
                            Renderer.yLast = index4;
                            return cell2;
                          }
                          continue;
                        }
                        continue;
                      }
                    }
                    int id = (int) obj1.ID & 16383;
                    bool xDouble = false;
                    int ItemID;
                    if (cellType == Renderer.tStaticItem)
                    {
                      ItemID = Map.GetDispID(id, 0, ref xDouble);
                    }
                    else
                    {
                      Item obj2 = ((DynamicItem) cell2).m_Item;
                      ItemID = obj2 != null ? Map.GetDispID(id, obj2.Amount, ref xDouble) : Map.GetDispID(id, 0, ref xDouble);
                    }
                    AnimData anim = Map.GetAnim(ItemID);
                    int itemId = ItemID;
                    if ((int) anim.frameCount != 0 && Map.m_ItemFlags[ItemID][(TileFlag) 16777216L])
                      itemId += (int) anim[Renderer.m_Frames / ((int) anim.frameInterval + 1) % (int) anim.frameCount];
                    Texture texture = Hues.Default.GetItem(itemId);
                    if (texture != null && !texture.IsEmpty())
                    {
                      int num19 = num17 + 22;
                      int num20 = num18 - ((int) cell2.Z << 2) + 43;
                      int num21 = num19 - (texture.Width >> 1);
                      int num22 = num20 - texture.Height;
                      int num23 = num21 + num13;
                      int num24 = num22 + num14;
                      if (xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width + 5) && my < num24 + texture.Height + 5)
                      {
                        mx -= num23;
                        my -= num24;
                        if (mx < texture.Width && my < texture.Height && texture.HitTest(mx, my) || mx >= 5 && my >= 5 && texture.HitTest(mx - 5, my - 5))
                        {
                          TileX = (int) (short) (num4 + index3);
                          TileY = (int) (short) (num5 + index4);
                          Renderer.m_LastFind = cell2;
                          Renderer.xLast = index3;
                          Renderer.yLast = index4;
                          return cell2;
                        }
                        mx += num23;
                        my += num24;
                      }
                      else if (!xDouble && mx >= num23 && (my >= num24 && mx < num23 + texture.Width) && (my < num24 + texture.Height && texture.HitTest(mx - num23, my - num24)))
                      {
                        TileX = (int) (short) (num4 + index3);
                        TileY = (int) (short) (num5 + index4);
                        Renderer.m_LastFind = cell2;
                        Renderer.xLast = index3;
                        Renderer.yLast = index4;
                        return cell2;
                      }
                    }
                  }
                }
                else if (cellType == Renderer.tLandTile)
                {
                  LandTile landTile = (LandTile) cell2;
                  int num19 = (int) landTile.m_Z;
                  if ((int) landTile.m_ID != 2 && (num15 >= int.MaxValue || (int) landTile.SortZ <= num15))
                  {
                    int num20 = num17 + num13;
                    int num21 = num18 + num14;
                    if (mx >= num20 && mx < num20 + 44)
                    {
                      Renderer.m_PointPool[0].X = num20 + 22;
                      Renderer.m_PointPool[0].Y = num21 - 4 * (int) landTile.z00;
                      Renderer.m_PointPool[1].X = num20 + 44;
                      Renderer.m_PointPool[1].Y = num21 + 22 - 4 * (int) landTile.z10;
                      Renderer.m_PointPool[2].X = num20 + 22;
                      Renderer.m_PointPool[2].Y = num21 + 44 - 4 * (int) landTile.z11;
                      Renderer.m_PointPool[3].X = num20;
                      Renderer.m_PointPool[3].Y = num21 + 22 - 4 * (int) landTile.z01;
                      if (Renderer.LandTileHitTest(Renderer.m_PointPool, Renderer.m_MousePoint))
                      {
                        TileX = (int) (short) (num4 + index3);
                        TileY = (int) (short) (num5 + index4);
                        Renderer.m_LastFind = cell2;
                        Renderer.xLast = index3;
                        Renderer.yLast = index4;
                        return cell2;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        catch
        {
        }
      }
      TileX = -1;
      TileY = -1;
      Renderer.m_LastFind = (ICell) null;
      Renderer.xLast = -1;
      Renderer.yLast = -1;
      return (ICell) null;
    }

    private static TransformedColoredTextured[] GeoPool(int count)
    {
      if (Renderer.m_GeoPool == null || Renderer.m_GeoPool.Length < count)
      {
        Renderer.m_GeoPool = new TransformedColoredTextured[count];
        for (int index = 0; index < count; ++index)
        {
          Renderer.m_GeoPool[index].Rhw = 1f;
          Renderer.m_GeoPool[index].Color = Renderer.GetQuadColor(0);
        }
      }
      else
      {
        for (int index = 0; index < count; ++index)
          Renderer.m_GeoPool[index].Color = Renderer.GetQuadColor(0);
      }
      return Renderer.m_GeoPool;
    }

    public static unsafe void TransparentRect(int Color, int X, int Y, int Width, int Height)
    {
      --Width;
      --Height;
      float num1 = (float) X;
      float num2 = (float) Y;
      TransformedColoredTextured[] transformedColoredTexturedArray = Renderer.GeoPool(5);
      transformedColoredTexturedArray[0].Color = transformedColoredTexturedArray[1].Color = transformedColoredTexturedArray[2].Color = transformedColoredTexturedArray[3].Color = Renderer.GetQuadColor(Color);
      transformedColoredTexturedArray[0].X = num1;
      transformedColoredTexturedArray[0].Y = num2;
      transformedColoredTexturedArray[1].X = num1 + (float) Width;
      transformedColoredTexturedArray[1].Y = num2;
      transformedColoredTexturedArray[2].X = num1 + (float) Width;
      transformedColoredTexturedArray[2].Y = num2 + (float) Height;
      transformedColoredTexturedArray[3].X = num1;
      transformedColoredTexturedArray[3].Y = num2 + (float) Height;
      transformedColoredTexturedArray[4] = transformedColoredTexturedArray[0];
      fixed (TransformedColoredTextured* pVertex = transformedColoredTexturedArray)
        Renderer.PushLineStrip(pVertex, 5);
    }

    public static void TransparentRect(int Color, int X, int Y, int Width, int Height, Clipper c)
    {
      Renderer.SolidRect(Color, X, Y, 1, Height, c);
      Renderer.SolidRect(Color, X + Width - 1, Y, 1, Height, c);
      Renderer.SolidRect(Color, X + 1, Y, Width - 2, 1, c);
      Renderer.SolidRect(Color, X + 1, Y + Height - 1, Width - 2, 1, c);
    }

    public static unsafe void SolidRect(int Color, int X, int Y, int Width, int Height)
    {
      if (Width <= 0 || Height <= 0)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      int quadColor = Renderer.GetQuadColor(Color);
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      float num3 = num1 + (float) Width;
      float num4 = num2 + (float) Height;
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        transformedColoredTexturedPtr1->Color = quadColor;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num4;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = quadColor;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num3;
        transformedColoredTexturedPtr2->Y = num2;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = quadColor;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num4;
        transformedColoredTexturedPtr3->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr4 = transformedColoredTexturedPtr3 + 1;
        transformedColoredTexturedPtr4->Color = quadColor;
        transformedColoredTexturedPtr4->Rhw = 1f;
        transformedColoredTexturedPtr4->X = num1;
        transformedColoredTexturedPtr4->Y = num2;
        transformedColoredTexturedPtr4->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void SolidRect(int Color, int X, int Y, int Width, int Height, Clipper c)
    {
      if (Width <= 0 || Height <= 0)
        return;
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      TransformedColoredTextured[] Vertices = Renderer.GeoPool(4);
      Vertices[0].Color = Vertices[1].Color = Vertices[2].Color = Vertices[3].Color = Renderer.GetQuadColor(Color);
      Vertices[0].X = Vertices[1].X = num1 + (float) Width;
      Vertices[0].Y = Vertices[2].Y = num2 + (float) Height;
      Vertices[1].Y = Vertices[3].Y = num2;
      Vertices[2].X = Vertices[3].X = num1;
      if (!c.Clip(X, Y, Width, Height, Vertices))
        return;
      fixed (TransformedColoredTextured* pVertex = Vertices)
        Renderer.PushQuad(pVertex);
    }

    public static unsafe void SolidQuad(int Color, Point[] pts)
    {
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      int quadColor = Renderer.GetQuadColor(Color);
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        transformedColoredTexturedPtr1->Color = quadColor;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = (float) pts[2].X - 0.5f;
        transformedColoredTexturedPtr1->Y = (float) pts[2].Y - 0.5f;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = quadColor;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = (float) pts[1].X - 0.5f;
        transformedColoredTexturedPtr2->Y = (float) pts[1].Y - 0.5f;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = quadColor;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = (float) pts[3].X - 0.5f;
        transformedColoredTexturedPtr3->Y = (float) pts[3].Y - 0.5f;
        transformedColoredTexturedPtr3->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr4 = transformedColoredTexturedPtr3 + 1;
        transformedColoredTexturedPtr4->Color = quadColor;
        transformedColoredTexturedPtr4->Rhw = 1f;
        transformedColoredTexturedPtr4->X = (float) pts[3].X - 0.5f;
        transformedColoredTexturedPtr4->Y = (float) pts[3].Y - 0.5f;
        transformedColoredTexturedPtr4->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void GradientRect4(int c00, int c10, int c11, int c01, int X, int Y, int Width, int Height)
    {
      if (Width <= 0 || Height <= 0)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      Renderer.GetColors(ref c00, ref c10, ref c11, ref c01);
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      float num3 = num1 + (float) Width;
      float num4 = num2 + (float) Height;
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        transformedColoredTexturedPtr1->Color = c11;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num4;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = c10;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num3;
        transformedColoredTexturedPtr2->Y = num2;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = c01;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num4;
        transformedColoredTexturedPtr3->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr4 = transformedColoredTexturedPtr3 + 1;
        transformedColoredTexturedPtr4->Color = c00;
        transformedColoredTexturedPtr4->Rhw = 1f;
        transformedColoredTexturedPtr4->X = num1;
        transformedColoredTexturedPtr4->Y = num2;
        transformedColoredTexturedPtr4->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void GradientRectLR(int Color, int Color2, int X, int Y, int Width, int Height)
    {
      if (Width <= 0 || Height <= 0)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      Renderer.GetColors(ref Color, ref Color2);
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      float num3 = num1 + (float) Width;
      float num4 = num2 + (float) Height;
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        transformedColoredTexturedPtr1->Color = Color2;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num4;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = Color2;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num3;
        transformedColoredTexturedPtr2->Y = num2;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = Color;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num4;
        transformedColoredTexturedPtr3->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr4 = transformedColoredTexturedPtr3 + 1;
        transformedColoredTexturedPtr4->Color = Color;
        transformedColoredTexturedPtr4->Rhw = 1f;
        transformedColoredTexturedPtr4->X = num1;
        transformedColoredTexturedPtr4->Y = num2;
        transformedColoredTexturedPtr4->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void GradientRect(int Color, int Color2, int X, int Y, int Width, int Height)
    {
      if (Width <= 0 || Height <= 0)
        return;
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      Renderer.GetColors(ref Color, ref Color2);
      float num1 = (float) X - 0.5f;
      float num2 = (float) Y - 0.5f;
      float num3 = num1 + (float) Width;
      float num4 = num2 + (float) Height;
      float z;
      ArraySegment<byte> arraySegment = Renderer.AcquireQuadStorage(out z).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        transformedColoredTexturedPtr1->Color = Color2;
        transformedColoredTexturedPtr1->Rhw = 1f;
        transformedColoredTexturedPtr1->X = num3;
        transformedColoredTexturedPtr1->Y = num4;
        transformedColoredTexturedPtr1->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + 1;
        transformedColoredTexturedPtr2->Color = Color;
        transformedColoredTexturedPtr2->Rhw = 1f;
        transformedColoredTexturedPtr2->X = num3;
        transformedColoredTexturedPtr2->Y = num2;
        transformedColoredTexturedPtr2->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr3 = transformedColoredTexturedPtr2 + 1;
        transformedColoredTexturedPtr3->Color = Color2;
        transformedColoredTexturedPtr3->Rhw = 1f;
        transformedColoredTexturedPtr3->X = num1;
        transformedColoredTexturedPtr3->Y = num4;
        transformedColoredTexturedPtr3->Z = z;
        TransformedColoredTextured* transformedColoredTexturedPtr4 = transformedColoredTexturedPtr3 + 1;
        transformedColoredTexturedPtr4->Color = Color;
        transformedColoredTexturedPtr4->Rhw = 1f;
        transformedColoredTexturedPtr4->X = num1;
        transformedColoredTexturedPtr4->Y = num2;
        transformedColoredTexturedPtr4->Z = z;
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void DrawLine(TransformedColoredTextured v1, TransformedColoredTextured v2, int color)
    {
      TransformedColoredTextured* pVertex = stackalloc TransformedColoredTextured[2];
      pVertex[0] = v1;
      pVertex[1] = v2;
      pVertex->Color = pVertex[1].Color = Renderer.GetQuadColor(color);
      Renderer.PushLineStrip(pVertex, 2);
    }

    private static unsafe void PushPointList(TransformedColoredTextured* pVertex, int count)
    {
    }

    public static unsafe void PushLineStrip(TransformedColoredTextured* pVertex, int count)
    {
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      float num = (float) (8191 - Renderer.m_Count) / 8192f;
      if (Renderer._drawGroup == 0)
        ++Renderer.m_Count;
      ArraySegment<byte> arraySegment = (Renderer._alphaEnable ? Renderer.AcquireAlphaStorage(0) : Renderer.AcquireSolidStorage(0)).Store((count - 1) * 2, count - 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        for (int index = 0; index < count - 1; ++index)
        {
          transformedColoredTexturedPtr[index * 2] = pVertex[index];
          transformedColoredTexturedPtr[index * 2].Z = num;
          transformedColoredTexturedPtr[index * 2 + 1] = pVertex[index + 1];
          transformedColoredTexturedPtr[index * 2 + 1].Z = num;
        }
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void PushVertices(TransformedColoredTextured* pVertex, int vertexCount, int type)
    {
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      float num = (float) (8191 - Renderer.m_Count) / 8192f;
      if (Renderer._drawGroup == 0)
        ++Renderer.m_Count;
      ArraySegment<byte> arraySegment = (Renderer._alphaEnable ? Renderer.AcquireAlphaStorage(type) : Renderer.AcquireSolidStorage(type)).Store(vertexCount, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr1 = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        TransformedColoredTextured* transformedColoredTexturedPtr2 = transformedColoredTexturedPtr1 + vertexCount;
        while (transformedColoredTexturedPtr1 < transformedColoredTexturedPtr2)
        {
          *transformedColoredTexturedPtr1 = *pVertex;
          transformedColoredTexturedPtr1->Z = num;
          ++transformedColoredTexturedPtr1;
          ++pVertex;
        }
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static IVertexStorage AcquireQuadStorage(out float z)
    {
      z = (float) (8191 - Renderer.m_Count) / 8192f;
      if (Renderer._drawGroup == 0)
        ++Renderer.m_Count;
      if (!Renderer._alphaEnable)
        return Renderer.AcquireSolidStorage(1);
      return Renderer.AcquireAlphaStorage(1);
    }

    private static unsafe void PushQuad(TransformedColoredTextured* pVertex)
    {
      if (Renderer._profile != null)
        Renderer._profile._composeTime.Start();
      float num = (float) (8191 - Renderer.m_Count) / 8192f;
      if (Renderer._drawGroup == 0)
        ++Renderer.m_Count;
      ArraySegment<byte> arraySegment = (Renderer._alphaEnable ? Renderer.AcquireAlphaStorage(1) : Renderer.AcquireSolidStorage(1)).Store(4, 1);
      fixed (byte* numPtr = arraySegment.Array)
      {
        TransformedColoredTextured* transformedColoredTexturedPtr = (TransformedColoredTextured*) (numPtr + arraySegment.Offset);
        for (int index = 0; index < 4; ++index)
        {
          transformedColoredTexturedPtr[index] = pVertex[index];
          transformedColoredTexturedPtr[index].Z = num;
        }
      }
      if (Renderer._profile == null)
        return;
      Renderer._profile._composeTime.Stop();
    }

    public static unsafe void DrawLines(TransformedColoredTextured[] v)
    {
      v[0].Color = v[1].Color = v[2].Color = v[3].Color = v[4].Color = Renderer.GetQuadColor(v[0].Color);
      fixed (TransformedColoredTextured* pVertex = v)
        Renderer.PushLineStrip(pVertex, v.Length);
    }

    public static unsafe void DrawPoints(params Point[] points)
    {
      int length = points.Length;
      TransformedColoredTextured[] transformedColoredTexturedArray = Renderer.GeoPool(length);
      for (int index = 0; index < length; ++index)
      {
        transformedColoredTexturedArray[index].X = 0.5f + (float) points[index].X;
        transformedColoredTexturedArray[index].Y = 0.5f + (float) points[index].Y;
      }
      fixed (TransformedColoredTextured* pVertex = transformedColoredTexturedArray)
        Renderer.PushPointList(pVertex, length);
    }

    public static unsafe void DrawLine(int X1, int Y1, int X2, int Y2)
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = Renderer.GeoPool(2);
      transformedColoredTexturedArray[0].X = (float) X1;
      transformedColoredTexturedArray[0].Y = (float) Y1;
      transformedColoredTexturedArray[1].X = (float) X2;
      transformedColoredTexturedArray[1].Y = (float) Y2;
      fixed (TransformedColoredTextured* pVertex = transformedColoredTexturedArray)
        Renderer.PushLineStrip(pVertex, 2);
    }

    public static unsafe void DrawLine(int X1, int Y1, int X2, int Y2, int color)
    {
      TransformedColoredTextured[] transformedColoredTexturedArray = Renderer.GeoPool(2);
      transformedColoredTexturedArray[0].X = (float) X1;
      transformedColoredTexturedArray[0].Y = (float) Y1;
      transformedColoredTexturedArray[1].X = (float) X2;
      transformedColoredTexturedArray[1].Y = (float) Y2;
      transformedColoredTexturedArray[0].Color = transformedColoredTexturedArray[1].Color = Renderer.GetQuadColor(color);
      fixed (TransformedColoredTextured* pVertex = transformedColoredTexturedArray)
        Renderer.PushLineStrip(pVertex, 2);
    }

    public static unsafe void DrawQuadPrecalc(TransformedColoredTextured[] v)
    {
      fixed (TransformedColoredTextured* pVertex = v)
        Renderer.PushQuad(pVertex);
    }

    public static unsafe void DrawQuadPrecalc(TransformedColoredTextured* pVertex)
    {
      Renderer.PushQuad(pVertex);
    }

    public static void GetColors(ref int c0, ref int c1)
    {
      if (!Renderer._alphaEnable)
      {
        c0 |= -16777216;
        c1 |= -16777216;
      }
      else
      {
        int num = Renderer._alphaBits;
        c0 &= 16777215;
        c0 |= num;
        c1 &= 16777215;
        c1 |= num;
      }
    }

    public static void GetColors(ref int c0, ref int c1, ref int c2, ref int c3)
    {
      if (!Renderer._alphaEnable)
      {
        c0 |= -16777216;
        c1 |= -16777216;
        c2 |= -16777216;
        c3 |= -16777216;
      }
      else
      {
        int num = Renderer._alphaBits;
        c0 &= 16777215;
        c0 |= num;
        c1 &= 16777215;
        c1 |= num;
        c2 &= 16777215;
        c2 |= num;
        c3 &= 16777215;
        c3 |= num;
      }
    }

    public static int GetQuadColor(int Color)
    {
      if (!Renderer._alphaEnable)
      {
        Color |= -16777216;
      }
      else
      {
        Color &= 16777215;
        Color |= Renderer._alphaBits;
      }
      return Color;
    }

    public static void Init(Capabilities Caps)
    {
      if (Renderer.m_VertexStream != null)
        Renderer.m_VertexStream.Unlock();
      Renderer.m_VertexStream = (BufferedVertexStream) null;
      Renderer._alphaTestEnable = false;
      Renderer._alphaEnable = false;
      Engine.m_Device.set_VertexFormat((VertexFormat) 324);
      Engine.m_Device.SetSamplerState(0, (SamplerState) 1, (TextureAddress) 3);
      Engine.m_Device.SetSamplerState(0, (SamplerState) 2, (TextureAddress) 3);
      Device device1 = Engine.m_Device;
      int num1 = 0;
      int num2 = 4;
      Color color1 = (Color) Color.Magenta;
      // ISSUE: explicit reference operation
      int bgra1 = ((Color) @color1).ToBgra();
      device1.SetSamplerState(num1, (SamplerState) num2, bgra1);
      Engine.m_Device.SetSamplerState(0, (SamplerState) 6, (TextureFilter) 1);
      Engine.m_Device.SetSamplerState(0, (SamplerState) 5, (TextureFilter) 1);
      Engine.m_Device.SetSamplerState(1, (SamplerState) 1, (TextureAddress) 3);
      Engine.m_Device.SetSamplerState(1, (SamplerState) 2, (TextureAddress) 3);
      Device device2 = Engine.m_Device;
      int num3 = 1;
      int num4 = 4;
      Color color2 = (Color) Color.Magenta;
      // ISSUE: explicit reference operation
      int bgra2 = ((Color) @color2).ToBgra();
      device2.SetSamplerState(num3, (SamplerState) num4, bgra2);
      Engine.m_Device.SetSamplerState(1, (SamplerState) 6, (TextureFilter) 2);
      Engine.m_Device.SetSamplerState(1, (SamplerState) 5, (TextureFilter) 2);
      Engine.m_Device.SetRenderState((RenderState) 7, true);
      Engine.m_Device.SetRenderState((RenderState) 14, true);
    }

    public static void SetTexture(Texture texture)
    {
      if (Renderer.m_Texture == texture)
        return;
      Renderer.m_Texture = texture;
      Renderer.UpdateAlphaSettings();
    }

    public static void Invalidate()
    {
      Renderer.m_Invalidate = true;
    }

    public static void PushAlpha(float alpha)
    {
      alpha *= Renderer._alphaValue;
      Renderer._alphaStack.Push(alpha);
      Renderer.UpdateAlpha(alpha);
    }

    public static void SetAlpha(float alpha)
    {
      Renderer.PopAlpha();
      Renderer.PushAlpha(alpha);
    }

    public static void PopAlpha()
    {
      if (Renderer._alphaStack.Count > 0)
      {
        double num = (double) Renderer._alphaStack.Pop();
      }
      if (Renderer._alphaStack.Count > 0)
        Renderer.UpdateAlpha(Renderer._alphaStack.Peek());
      else
        Renderer.UpdateAlpha(1f);
    }

    public static void UpdateAlphaSettings()
    {
      if (Renderer._blendType == DrawBlendType.Normal)
      {
        switch ((Renderer.m_Texture ?? Texture.Empty).Transparency)
        {
          case TextureTransparency.None:
            Renderer._alphaEnable = (double) Renderer._alphaValue < 1.0;
            Renderer._alphaTestEnable = false;
            break;
          case TextureTransparency.Simple:
            Renderer._alphaEnable = (double) Renderer._alphaValue < 1.0;
            Renderer._alphaTestEnable = true;
            break;
          case TextureTransparency.Complex:
            Renderer._alphaEnable = true;
            Renderer._alphaTestEnable = true;
            break;
        }
      }
      else
      {
        Renderer._alphaEnable = true;
        Renderer._alphaTestEnable = false;
      }
    }

    private static void UpdateAlpha(float alpha)
    {
      if ((double) Renderer._alphaValue == (double) alpha)
        return;
      Renderer._alphaValue = alpha;
      Renderer._alphaBits = (int) ((double) alpha * (double) byte.MaxValue);
      if (Renderer._alphaBits < 0)
        Renderer._alphaBits = 0;
      else if (Renderer._alphaBits > (int) byte.MaxValue)
        Renderer._alphaBits = -16777216;
      else
        Renderer._alphaBits <<= 24;
      Renderer.UpdateAlphaSettings();
    }

    public static void SetText(string text)
    {
      text = Engine.Encode(text);
      SpeechFormat speechFormat = SpeechFormat.Find(text);
      int hueId = Preferences.Current.SpeechHues[speechFormat.SpeechType];
      text = speechFormat.Mutate(text, true);
      if (Renderer.m_vTextCache == null)
        Renderer.m_vTextCache = new VertexCache();
      else
        Renderer.m_vTextCache.Invalidate();
      Renderer.m_TextSurface = Engine.GetUniFont(3).GetString(text, Hues.Load(hueId));
    }

    public static unsafe void Grid(LandTile lt, LandTile[,] landTiles, int x, int y, int bx, int by)
    {
      if (bx + 44 <= Engine.GameX || bx >= Engine.GameX + Engine.GameWidth)
        return;
      TransformedColoredTextured[] v = Renderer.GeoPool(5);
      v[0].Color = v[1].Color = v[2].Color = v[3].Color = 4227327;
      v[0].X = (float) (bx + 22);
      v[0].Y = (float) (by - ((int) lt.m_Z << 2));
      v[1].Y = (float) (by + 22 - ((int) landTiles[x + 1, y].m_Z << 2));
      v[1].X = (float) (bx + 44);
      v[2].Y = (float) (by + 44 - ((int) landTiles[x + 1, y + 1].m_Z << 2));
      v[2].X = (float) (bx + 22);
      v[3].Y = (float) (by + 22 - ((int) landTiles[x, y + 1].m_Z << 2));
      v[3].X = (float) bx;
      v[4] = v[0];
      Renderer.SetTexture((Texture) null);
      Renderer.DrawLines(v);
      int num = x & 7;
      if ((y & 7) == 0)
      {
        fixed (TransformedColoredTextured* pVertex = v)
        {
          pVertex->Color = pVertex[1].Color = Renderer.GetQuadColor(16720000);
          Renderer.PushLineStrip(pVertex, 2);
        }
      }
      if (num != 0)
        return;
      fixed (TransformedColoredTextured* transformedColoredTexturedPtr = v)
      {
        transformedColoredTexturedPtr[3].Color = transformedColoredTexturedPtr[4].Color = Renderer.GetQuadColor(16720000);
        Renderer.PushLineStrip(transformedColoredTexturedPtr + 3, 2);
      }
    }

    public static void ScreenShot(string title)
    {
      if (Engine.GMPrivs)
        return;
      Renderer.screenshotContext = new Renderer.ScreenshotContext(title);
      Map.Invalidate();
      Renderer.Draw();
      Renderer.screenshotContext = (Renderer.ScreenshotContext) null;
      Map.Invalidate();
    }

    private static void SaveStream(object state)
    {
      try
      {
        object[] objArray = (object[]) state;
        MemoryStream memoryStream = (MemoryStream) objArray[0];
        FileStream fileStream = new FileStream((string) objArray[1], FileMode.Create, FileAccess.Write, FileShare.None);
        memoryStream.WriteTo((Stream) fileStream);
        fileStream.Close();
        memoryStream.Close();
      }
      catch
      {
      }
    }

    public static void DrawMapLine(LandTile[,] landTiles, int bx, int by, int x, int y, int x2, int y2)
    {
      Renderer.SetTexture((Texture) null);
      Renderer.DrawLine(bx + 22, by - ((int) landTiles[x, y].m_Z << 2), bx + 22 + (x2 - y2) * 22, by + 22 - ((int) landTiles[x + x2, y + y2].m_Z << 2), 4259648);
    }

    public static void Draw()
    {
      try
      {
        Renderer.DrawUnsafe();
      }
      catch (SharpDXException ex)
      {
        Result resultCode = ex.get_ResultCode();
        // ISSUE: explicit reference operation
        int code = ((Result) @resultCode).get_Code();
        if (code == ((ResultDescriptor) ResultCode.DeviceLost).get_Code())
        {
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Application.DoEvents();
          Thread.Sleep(10);
        }
        else if (code == ((ResultDescriptor) ResultCode.DeviceNotReset).get_Code())
        {
          Engine.m_Device.Reset(new PresentParameters[1]
          {
            Engine.m_PresentParams
          });
          Engine.OnDeviceReset((object) null, (EventArgs) null);
          GC.Collect();
        }
        else
        {
          Thread.Sleep(10);
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Debug.Error((Exception) ex);
        }
      }
      catch (Exception ex)
      {
        Debug.Error(ex);
      }
    }

    private static bool Validate()
    {
      bool flag;
      do
      {
        flag = false;
        try
        {
          Engine.m_Device.TestCooperativeLevel();
        }
        catch (SharpDXException ex)
        {
          Result resultCode = ex.get_ResultCode();
          // ISSUE: explicit reference operation
          int code = ((Result) @resultCode).get_Code();
          if (code == ((ResultDescriptor) ResultCode.DeviceLost).get_Code())
          {
            try
            {
              Engine.m_Device.Reset(new PresentParameters[1]
              {
                Engine.m_PresentParams
              });
              Engine.OnDeviceReset((object) null, (EventArgs) null);
            }
            catch
            {
            }
            Application.DoEvents();
            Thread.Sleep(10);
            return false;
          }
          if (code == ((ResultDescriptor) ResultCode.DeviceNotReset).get_Code())
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
            GC.Collect();
            return true;
          }
          Application.DoEvents();
          Thread.Sleep(10);
          flag = true;
        }
      }
      while (flag);
      return true;
    }

    public static void DrawPlayerIcon(int x, int y, Texture icon, int color)
    {
      if (icon == Engine.ImageCache.LastTargetIcon)
        y -= icon.Height / 2;
      else
        y -= icon.Height * 2 / 3;
      x -= icon.Width / 2;
      icon.DrawGame(x, y, color);
    }

    public static MapSubgroup[] GetMapSubgroups(int size)
    {
      if (Renderer._mapSubgroups == null)
      {
        Renderer._mapSubgroups = new MapSubgroup[size * size * 2];
        int num1 = 0;
        for (int y = 0; y < size; ++y)
        {
          for (int x = 0; x < size; ++x)
          {
            MapSubgroup[] mapSubgroupArray1 = Renderer._mapSubgroups;
            int index1 = num1;
            int num2 = 1;
            int num3 = index1 + num2;
            mapSubgroupArray1[index1] = new MapSubgroup(x, y, true);
            MapSubgroup[] mapSubgroupArray2 = Renderer._mapSubgroups;
            int index2 = num3;
            int num4 = 1;
            num1 = index2 + num4;
            mapSubgroupArray2[index2] = new MapSubgroup(x, y, false);
          }
        }
        Array.Sort<MapSubgroup>(Renderer._mapSubgroups, (Comparison<MapSubgroup>) ((a, b) =>
        {
          int num = a.x + a.y - (b.x + b.y);
          if (num == 0)
            num = a.x - a.y - (a.ground ? 3 : 0) - (b.x - b.y - (b.ground ? 3 : 0));
          return num;
        }));
      }
      return Renderer._mapSubgroups;
    }

    private static int GetLightHueByItemId(int itemId)
    {
      int num = 0;
      if (itemId >= 15874 && itemId <= 15883 || itemId >= 14612 && itemId <= 14633)
        num = 2801;
      else if (itemId >= 14732 && itemId <= 14751 || itemId >= 15911 && itemId <= 15930)
        num = 2831;
      else if (itemId >= 14662 && itemId <= 14692)
        num = 2806;
      else if (itemId >= 14695 && itemId <= 14730)
        num = 2806;
      else if (itemId >= 3633 && itemId <= 3635 || (itemId == 6587 || itemId == 7979))
        num = 2840;
      else if (itemId == 3948 || itemId == 8148)
        num = 2802;
      else if (itemId == 4017 || itemId >= 6522 && itemId <= 6569)
        num = 2860;
      else if (itemId >= 6571 && itemId <= 6582)
        num = 2860;
      else if (itemId >= 3676 && itemId <= 3690)
        num = 2806;
      else if (itemId >= 3629 && itemId <= 3632)
        num = 2862;
      else if (itemId >= 13639 && itemId <= 13644 || itemId >= 13371 && itemId <= 13420 || itemId >= 12934 && itemId <= 12955)
        num = 2831;
      else if (itemId >= 4846 && itemId <= 4941)
        num = 2831;
      else if (itemId >= 7885 && itemId <= 7887)
        num = 2801;
      else if (itemId >= 7888 && itemId <= 7890)
        num = 2803;
      else if (itemId >= 6217 && itemId <= 6224 || itemId >= 6227 && itemId <= 6234)
        num = 2861;
      else if (itemId >= 3553 && itemId <= 3562)
        num = 2831;
      else if (itemId == 4012 || itemId >= 2555 && itemId <= 2580)
        num = 2830;
      else if (itemId == 5703)
        num = 2861;
      else if (itemId == 14239)
        num = 2806;
      else if (itemId >= 14000 && itemId <= 14035)
        num = 2860;
      else if (itemId >= 14036 && itemId <= 14051)
        num = 2860;
      else if (itemId >= 14052 && itemId <= 14067)
        num = 2860;
      return num;
    }

    private static float GetSample(int serial, int n)
    {
      float num1 = 0.0f;
      for (int index = 0; index < 2; ++index)
      {
        uint num2 = (uint) (1664525 * (1664525 * (1664525 * (serial + 1013904223) + serial) + (n * 2 + index)));
        uint num3 = num2 * (uint) ((int) num2 * ((int) num2 * 15731 + 789221) + 1376312589);
        num1 += (float) num3 / (float) uint.MaxValue;
      }
      return Math.Abs(num1 - 1f);
    }

    public static void RenderLight(int serial, int x, int y, int itemId, int lightId)
    {
      Renderer.RenderLight(serial, x, y, itemId, lightId, 0);
    }

    public static void RenderLight(int serial, int x, int y, int itemId, int lightId, int lightHueId)
    {
      float sample1 = Renderer.GetSample(serial, Renderer.m_Frames / 10);
      float sample2 = Renderer.GetSample(serial, Renderer.m_Frames / 10 + 1);
      float num = (float) (Renderer.m_Frames % 10) / 10f;
      float alpha = (float) (0.75 + ((double) sample1 * (1.0 - (double) num) + (double) sample2 * (double) num) * 0.25);
      Renderer.RenderLight(serial, x, y, itemId, lightId, lightHueId, alpha);
    }

    public static void RenderLight(int serial, int x, int y, int itemId, int lightId, int lightHueId, float alpha)
    {
      if (lightHueId <= 0)
        lightHueId = Renderer.GetLightHueByItemId(itemId);
      if (lightId == 0)
        lightId = (int) Map.GetQuality(itemId);
      if ((lightId == 26 || lightId == 27) && (itemId >= 10678 && itemId <= 10687 || itemId >= 59 && itemId <= 60))
        y -= lightId == 26 ? 10 : 8;
      Texture light = Hues.Load(lightHueId == 0 ? 0 : lightHueId + 1).GetLight(lightId);
      if (light == null)
        return;
      Renderer.PushAlpha(alpha);
      light.Draw(x - light.Width / 2, y - light.Height / 2, 16777215);
      Renderer.PopAlpha();
    }

    private static void RenderLights()
    {
      Mobile player = World.Player;
      if (player == null)
        return;
      int num1 = Engine.Effects.GlobalLight;
      if (player != null)
        num1 -= player.LightLevel;
      if (num1 < 0)
        num1 = 0;
      else if (num1 > 31)
        num1 = 31;
      if (num1 == 0)
        return;
      int x = player.X;
      int y = player.Y;
      int z = player.Z;
      MapPackage map = Map.GetMap((x >> 3) - (Renderer.blockWidth >> 1), (y >> 3) - (Renderer.blockHeight >> 1), Renderer.blockWidth, Renderer.blockHeight);
      int num2 = Renderer.blockWidth;
      int num3 = Renderer.blockHeight;
      ArrayList[,] arrayListArray = map.cells;
      int num4 = x & 7;
      int num5 = y & 7;
      int index1 = Renderer.blockWidth / 2 * 8 + num4;
      int index2 = Renderer.blockHeight / 2 * 8 + num5;
      int num6 = 0;
      int num7 = (Engine.GameWidth >> 1) - 22 + (4 - num4) * 22 - (4 - num5) * 22;
      int num8 = num6 + (4 - num4) * 22 + (4 - num5) * 22 + (z << 2) + ((Engine.GameHeight >> 1) - (index1 + index2) * 22 - (4 - num5) * 22 - (4 - num4) * 22 - 22);
      int num9 = num7 - 1;
      int num10 = num8 - 1;
      if (player != null && player.Walking.Count > 0)
      {
        WalkAnimation walkAnimation = (WalkAnimation) player.Walking.Peek();
        int xOffset = 0;
        int yOffset = 0;
        int fOffset = 0;
        if (!walkAnimation.Snapshot(ref xOffset, ref yOffset, ref fOffset))
        {
          if (!walkAnimation.Advance)
          {
            xOffset = walkAnimation.xOffset;
            yOffset = walkAnimation.yOffset;
          }
          else
          {
            xOffset = 0;
            yOffset = 0;
          }
        }
        num9 -= xOffset;
        num10 -= yOffset;
        Renderer.m_xScroll = xOffset;
        Renderer.m_yScroll = yOffset;
      }
      int size = Renderer.cellWidth < Renderer.cellHeight ? Renderer.cellWidth - 1 : Renderer.cellHeight - 1;
      Renderer.PushAll();
      if (Renderer.lightTexture == null)
        Renderer.lightTexture = new Texture(Engine.GameWidth, Engine.GameHeight, (Format) 21, (Pool) 0, false, TextureTransparency.Complex, (Usage) 1);
      if (Renderer.lightSurface == null)
        Renderer.lightSurface = Renderer.lightTexture.m_Surface.GetSurfaceLevel(0);
      MapSubgroup[] mapSubgroups = Renderer.GetMapSubgroups(size);
      int num11 = int.MaxValue;
      int num12 = int.MaxValue;
      int count1 = arrayListArray[index1 + 1, index2 + 1].Count;
      for (int index3 = 0; index3 < count1; ++index3)
      {
        ICell cell = (ICell) arrayListArray[index1 + 1, index2 + 1][index3];
        Type cellType = cell.CellType;
        if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem)
        {
          ITile tile = (ITile) cell;
          if (Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L] && (int) tile.Z >= z + 15 && (int) tile.Z < num11)
            num11 = (int) tile.Z;
        }
      }
      int count2 = arrayListArray[index1, index2].Count;
      for (int index3 = 0; index3 < count2; ++index3)
      {
        ICell cell = (ICell) arrayListArray[index1, index2][index3];
        Type cellType = cell.CellType;
        if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem || cellType == Renderer.tLandTile)
        {
          ITile tile = (ITile) cell;
          if (!Map.GetTileFlags((int) tile.ID)[(TileFlag) 268435456L])
          {
            int num13 = cellType == Renderer.tLandTile ? (int) tile.SortZ : (int) tile.Z;
            if (num13 >= z + 15)
            {
              if (cellType == Renderer.tLandTile)
              {
                if (num13 < num11)
                  num11 = num13;
                if (z + 16 < num12)
                  num12 = z + 16;
              }
              else if (num13 < num11)
                num11 = num13;
            }
          }
        }
      }
      Engine.m_Device.SetRenderTarget(0, Renderer.lightSurface);
      byte num14 = (byte) ((int) byte.MaxValue - (num1 * (int) byte.MaxValue + 15) / 31);
      try
      {
        Engine.m_Device.Clear((ClearFlags) 3, new ColorBGRA(num14, num14, num14, (byte) 0), 1f, 0);
      }
      catch
      {
      }
      Engine.m_Device.BeginScene();
      try
      {
        Renderer.SetAlpha(1f);
        Renderer.SetBlendType(DrawBlendType.Additive);
        for (int index3 = 0; index3 < mapSubgroups.Length; ++index3)
        {
          MapSubgroup mapSubgroup = mapSubgroups[index3];
          int index4 = mapSubgroup.x;
          int index5 = mapSubgroup.y;
          bool flag1 = false;
          int num13 = (index4 - index5) * 22 + num9;
          int num15 = (index4 + index5) * 22 + num10;
          int count3 = arrayListArray[index4, index5].Count;
          for (int index6 = 0; index6 < count3; ++index6)
          {
            ICell cell1 = (ICell) arrayListArray[index4, index5][index6];
            Type cellType1 = cell1.CellType;
            if (mapSubgroup.ground == flag1)
            {
              if (cellType1 == Renderer.tLandTile)
                flag1 = true;
            }
            else if (cellType1 == Renderer.tLandTile)
            {
              flag1 = true;
              int num16 = ((LandTile) cell1).graphicId;
              if (num16 >= 500 && num16 <= 503)
                Renderer.RenderLight(index4 * 4096 + index5, num13 + 22, num15 + 22 - (int) cell1.Z * 4, 4846, 0);
            }
            else if (cellType1 == Renderer.tMobileCell)
            {
              Mobile mobile = ((MobileCell) cell1).m_Mobile;
              if (mobile != null)
              {
                Item equip = mobile.FindEquip(Layer.TwoHanded);
                int xOffset = 0;
                int yOffset = 0;
                int fOffset = 0;
                if (mobile.Walking.Count > 0)
                  ((WalkAnimation) mobile.Walking.Peek()).Snapshot(ref xOffset, ref yOffset, ref fOffset);
                if (equip != null && Map.m_ItemFlags[equip.ID & 16383][(TileFlag) 8388608L])
                  Renderer.RenderLight(mobile.Serial, num13 + xOffset + 22, num15 + yOffset - (int) cell1.Z * 4, equip.ID & 16383, 29);
              }
            }
            else if (cellType1 == Renderer.tStaticItem || cellType1 == Renderer.tDynamicItem)
            {
              bool flag2 = cellType1 == Renderer.tStaticItem;
              IItem obj1 = (IItem) cell1;
              short num16;
              sbyte num17;
              TileFlags tileFlags;
              Item obj2;
              if (flag2)
              {
                StaticItem staticItem = (StaticItem) obj1;
                num16 = staticItem.m_ID;
                switch (num16)
                {
                  case 16385:
                  case 22422:
                  case 24996:
                  case 24984:
                  case 25020:
                  case 24985:
                    continue;
                  default:
                    num17 = staticItem.m_Z;
                    tileFlags = Map.m_ItemFlags[(int) num16 & 16383];
                    bool flag3 = num11 < int.MaxValue && ((int) num17 >= num11 || tileFlags[(TileFlag) 268435456L]);
                    if (!flag3)
                    {
                      if (index4 + 1 < Renderer.cellWidth && index5 + 1 < Renderer.cellHeight)
                      {
                        int count4 = arrayListArray[index4 + 1, index5 + 1].Count;
                        for (int index7 = 0; index7 < count4; ++index7)
                        {
                          ICell cell2 = (ICell) arrayListArray[index4 + 1, index5 + 1][index7];
                          Type cellType2 = cell2.CellType;
                          if (cellType2 == Renderer.tStaticItem || cellType2 == Renderer.tDynamicItem)
                          {
                            ITile tile = (ITile) cell2;
                            if ((num11 >= int.MaxValue || (int) tile.Z < num11 && !Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L]) && (!Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L] && (int) tile.Z >= (int) cell1.Z + 1))
                            {
                              flag3 = true;
                              break;
                            }
                          }
                        }
                        if (flag3)
                          continue;
                      }
                      if (index4 + 2 < Renderer.cellWidth && index5 + 2 < Renderer.cellHeight)
                      {
                        int count4 = arrayListArray[index4 + 2, index5 + 2].Count;
                        for (int index7 = 0; index7 < count4; ++index7)
                        {
                          ICell cell2 = (ICell) arrayListArray[index4 + 2, index5 + 2][index7];
                          Type cellType2 = cell2.CellType;
                          if (cellType2 == Renderer.tStaticItem || cellType2 == Renderer.tDynamicItem)
                          {
                            ITile tile = (ITile) cell2;
                            if ((num11 >= int.MaxValue || (int) tile.Z < num11 && !Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L]) && (Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L] && (int) tile.Z >= (int) cell1.Z + (int) cell1.Height))
                            {
                              flag3 = true;
                              break;
                            }
                          }
                        }
                        if (flag3)
                          continue;
                      }
                      obj2 = (Item) null;
                      break;
                    }
                    continue;
                }
              }
              else
              {
                DynamicItem dynamicItem = (DynamicItem) obj1;
                num16 = dynamicItem.m_ID;
                switch (num16)
                {
                  case 16385:
                  case 22422:
                  case 24996:
                  case 24984:
                  case 25020:
                  case 24985:
                    continue;
                  default:
                    num17 = dynamicItem.m_Z;
                    tileFlags = Map.m_ItemFlags[(int) num16 & 16383];
                    if (num11 >= int.MaxValue || (int) num17 < num11 && !tileFlags[(TileFlag) 268435456L])
                    {
                      obj2 = dynamicItem.m_Item;
                      break;
                    }
                    continue;
                }
              }
              int serial = obj2 != null ? obj2.Serial : index4 * 4096 + index5;
              if (tileFlags[(TileFlag) 8388608L])
                Renderer.RenderLight(serial, num13 + 22, num15 + 22 - (int) num17 * 4, (int) num16 & 16383, obj2 != null ? (int) obj2.Direction : 0);
            }
          }
        }
        Engine.Effects.RenderLights();
        Renderer.PushAll();
      }
      finally
      {
        Engine.m_Device.EndScene();
        Engine.m_Device.SetRenderTarget(0, Renderer.AcquireBackBuffer());
      }
      Renderer.SetAlpha(1f);
      Renderer.SetBlendType(DrawBlendType.Normal);
    }

    public static unsafe void DrawUnsafe()
    {
      if (Engine.m_Device == null || !Renderer.Validate())
        return;
      if (!Renderer._timeRefresh && Renderer._profile != null)
        Renderer._profile.Reset();
      Renderer.RenderLights();
      Stats.Reset();
      try
      {
        Engine.m_Device.Clear((ClearFlags) 3, ColorBGRA.op_Implicit((Color) Color.Black), 1f, 0);
      }
      catch
      {
      }
      Queue queue1 = Renderer.m_ToUpdateQueue;
      if (queue1 == null)
        queue1 = Renderer.m_ToUpdateQueue = new Queue();
      else if (queue1.Count > 0)
        queue1.Clear();
      Engine.m_Device.BeginScene();
      if (Renderer._profile != null)
        Renderer._profile._drawTime.Start();
      try
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        bool preserveHue1 = false;
        bool flag1 = false;
        int num4;
        Renderer.m_zWorld = num4 = 0;
        Renderer.m_yWorld = num4;
        Renderer.m_xWorld = num4;
        Mobile player1 = World.Player;
        if (player1 != null)
        {
          preserveHue1 = player1.Ghost;
          flag1 = player1.Flags[MobileFlag.Warmode];
          Renderer.m_xWorld = num1 = player1.X;
          Renderer.m_yWorld = num2 = player1.Y;
          Renderer.m_zWorld = num3 = player1.Z;
        }
        Renderer.m_Dead = preserveHue1;
        Renderer.m_xScroll = 0;
        Renderer.m_yScroll = 0;
        ArrayList arrayList1 = Renderer.m_TextToDrawList;
        if (arrayList1 == null)
          arrayList1 = Renderer.m_TextToDrawList = new ArrayList();
        else if (arrayList1.Count > 0)
          arrayList1.Clear();
        Renderer.m_TextToDraw = arrayList1;
        Queue queue2 = Renderer.m_MiniHealthQueue;
        if (queue2 == null)
          queue2 = Renderer.m_MiniHealthQueue = new Queue();
        else if (queue2.Count > 0)
          queue2.Clear();
        Renderer.eOffsetX = 0;
        Renderer.eOffsetY = 0;
        if (Engine.m_Ingame)
        {
          if (GDesktopBorder.Instance != null)
            GDesktopBorder.Instance.DoRender();
          Renderer.SetViewport(Engine.GameX, Engine.GameY, Engine.GameWidth, Engine.GameHeight);
          if (Renderer._profile != null)
            Renderer._profile._worldTime.Start();
          Map.Lock();
          MapPackage map = Map.GetMap((num1 >> 3) - (Renderer.blockWidth >> 1), (num2 >> 3) - (Renderer.blockHeight >> 1), Renderer.blockWidth, Renderer.blockHeight);
          int num5 = (num1 >> 3) - (Renderer.blockWidth >> 1) << 3;
          int num6 = (num2 >> 3) - (Renderer.blockHeight >> 1) << 3;
          ArrayList[,] arrayListArray = map.cells;
          int num7 = num1 & 7;
          int num8 = num2 & 7;
          int index1 = Renderer.blockWidth / 2 * 8 + num7;
          int index2 = Renderer.blockHeight / 2 * 8 + num8;
          int num9 = 0;
          int num10 = (Engine.GameWidth >> 1) - 22 + (4 - num7) * 22 - (4 - num8) * 22;
          int num11 = num9 + (4 - num7) * 22 + (4 - num8) * 22 + (num3 << 2) + ((Engine.GameHeight >> 1) - (index1 + index2) * 22 - (4 - num8) * 22 - (4 - num7) * 22 - 22);
          int num12 = num10 - 1;
          int num13 = num11 - 1;
          int num14 = num12 + Engine.GameX;
          int num15 = num13 + Engine.GameY;
          Mobile player2 = World.Player;
          bool flag2 = false;
          Renderer.m_xScroll = Renderer.m_yScroll = 0;
          if (player1 != null && player1.Walking.Count > 0)
          {
            WalkAnimation walkAnimation = (WalkAnimation) player1.Walking.Peek();
            int xOffset = 0;
            int yOffset = 0;
            int fOffset = 0;
            if (!walkAnimation.Snapshot(ref xOffset, ref yOffset, ref fOffset))
            {
              if (!walkAnimation.Advance)
              {
                xOffset = walkAnimation.xOffset;
                yOffset = walkAnimation.yOffset;
              }
              else
              {
                xOffset = 0;
                yOffset = 0;
              }
            }
            num14 -= xOffset;
            num15 -= yOffset;
            Renderer.m_xScroll = xOffset;
            Renderer.m_yScroll = yOffset;
          }
          bool flag3 = !Renderer.m_Invalidate && Renderer.m_CharX == num1 && (Renderer.m_CharY == num2 && Renderer.m_CharZ == num3) && (Renderer.m_WasDead == preserveHue1 && Renderer.m_xBaseLast == num14) && Renderer.m_yBaseLast == num15;
          Renderer.m_xBaseLast = num14;
          Renderer.m_yBaseLast = num15;
          Renderer.m_Invalidate = false;
          Renderer.m_WasDead = preserveHue1;
          Renderer.m_CharX = num1;
          Renderer.m_CharY = num2;
          Renderer.m_CharZ = num3;
          bool notorietyHalos = Options.Current.NotorietyHalos;
          ArrayList arrayList2 = new ArrayList();
          int num16 = int.MaxValue;
          int num17 = int.MaxValue;
          int count1 = arrayListArray[index1 + 1, index2 + 1].Count;
          for (int index3 = 0; index3 < count1; ++index3)
          {
            ICell cell = (ICell) arrayListArray[index1 + 1, index2 + 1][index3];
            Type cellType = cell.CellType;
            if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem)
            {
              ITile tile = (ITile) cell;
              if (Map.m_ItemFlags[(int) tile.ID & 16383][(TileFlag) 268435456L] && (int) tile.Z >= num3 + 15 && (int) tile.Z < num16)
                num16 = (int) tile.Z;
            }
          }
          int count2 = arrayListArray[index1, index2].Count;
          for (int index3 = 0; index3 < count2; ++index3)
          {
            ICell cell = (ICell) arrayListArray[index1, index2][index3];
            Type cellType = cell.CellType;
            if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem || cellType == Renderer.tLandTile)
            {
              ITile tile = (ITile) cell;
              if (!Map.GetTileFlags((int) tile.ID)[(TileFlag) 268435456L])
              {
                int num18 = cellType == Renderer.tLandTile ? (int) tile.SortZ : (int) tile.Z;
                if (num18 >= num3 + 15)
                {
                  if (cellType == Renderer.tLandTile)
                  {
                    if (num18 < num16)
                      num16 = num18;
                    if (num3 + 16 < num17)
                      num17 = num3 + 16;
                  }
                  else if (num18 < num16)
                    num16 = num18;
                }
              }
            }
          }
          Renderer.m_CullLand = num17 < int.MaxValue;
          IHue hue1 = preserveHue1 ? Hues.Grayscale : Hues.Default;
          Queue queue3 = Renderer.m_TransDrawQueue;
          if (queue3 == null)
            queue3 = Renderer.m_TransDrawQueue = new Queue();
          else if (queue3.Count > 0)
            queue3.Clear();
          RenderSettings renderSettings = Preferences.Current.RenderSettings;
          bool itemShadows = renderSettings.ItemShadows;
          bool characterShadows = renderSettings.CharacterShadows;
          int num19 = renderSettings.SmoothCharacters ? 1 : 0;
          bool flag4 = Engine.m_MultiPreview;
          int num20 = 0;
          int num21 = 0;
          int num22 = 0;
          int num23 = 0;
          int num24 = 0;
          int num25 = 0;
          int num26 = 0;
          int num27 = 0;
          int num28 = 0;
          int num29 = 0;
          int num30 = 0;
          int num31 = 0;
          int num32 = 0;
          BaseTargetHandler active = TargetManager.Active;
          bool flag5 = flag1 || active != null;
          if (flag4)
          {
            if (Gumps.IsWorldAt(Engine.m_xMouse, Engine.m_yMouse, true))
            {
              int TileX = 0;
              int TileY = 0;
              ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY, false);
              if (tileFromXy == null)
                flag4 = false;
              else if (tileFromXy.CellType == Renderer.tLandTile || tileFromXy.CellType == Renderer.tStaticItem)
              {
                int num18 = TileX - num5;
                int num33 = TileY - num6;
                int num34 = (int) tileFromXy.Z + (tileFromXy.CellType == Renderer.tStaticItem ? (int) tileFromXy.Height : 0);
                num23 = Engine.m_MultiList.Count;
                num20 = num18 - Engine.m_xMultiOffset;
                num21 = num33 - Engine.m_yMultiOffset;
                num22 = num34 - Engine.m_zMultiOffset;
                num24 = num20 + Engine.m_MultiMinX;
                num25 = num21 + Engine.m_MultiMinY;
                num26 = num20 + Engine.m_MultiMaxX;
                num27 = num21 + Engine.m_MultiMaxY;
              }
              else
                flag4 = false;
            }
            else
              flag4 = false;
          }
          else if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == (Keys.Shift | Keys.Control) && (Gumps.LastOver is GSpellIcon && ((GSpellIcon) Gumps.LastOver).m_SpellID == 57 || Gumps.LastOver is GContainerItem && (((GContainerItem) Gumps.LastOver).Item.ID & 16383) == 8037))
          {
            int num18 = 1 + (int) ((double) Engine.Skills[SkillName.Magery].Value / 15.0);
            num32 = 16737894;
            num28 = player1.X - num5 - num18;
            num29 = player1.Y - num6 - num18;
            num30 = player1.X - num5 + num18;
            num31 = player1.Y - num6 + num18;
          }
          else if (active is ServerTargetHandler)
          {
            ServerTargetHandler serverTargetHandler = active as ServerTargetHandler;
            int num18 = 0;
            int num33 = -1;
            bool flag6 = false;
            if (serverTargetHandler.Action == TargetAction.MeteorSwarm || serverTargetHandler.Action == TargetAction.ChainLightning)
            {
              num18 = 16737894;
              num33 = 2;
            }
            else if (serverTargetHandler.Action == TargetAction.MassCurse)
            {
              num18 = 16737894;
              num33 = 3;
            }
            else if (serverTargetHandler.Action == TargetAction.MassDispel)
            {
              num18 = 16737894;
              num33 = 8;
            }
            else if (serverTargetHandler.Action == TargetAction.Reveal)
            {
              num18 = 10079487;
              num33 = 1 + (int) ((double) Engine.Skills[SkillName.Magery].Value / 20.0);
            }
            else if (serverTargetHandler.Action == TargetAction.DetectHidden)
            {
              num18 = 10079487;
              num33 = (int) ((double) Engine.Skills[SkillName.DetectingHidden].Value / 10.0);
            }
            else if (serverTargetHandler.Action == TargetAction.ArchProtection)
            {
              num18 = 10079487;
              num33 = Engine.ServerFeatures.AOS ? 2 : 3;
            }
            else if (serverTargetHandler.Action == TargetAction.ArchCure)
            {
              num18 = 10079487;
              num33 = 3;
            }
            else if (serverTargetHandler.Action == TargetAction.WallOfStone)
            {
              num18 = 16737894;
              num33 = 1;
              flag6 = true;
            }
            else if (serverTargetHandler.Action == TargetAction.EnergyField || serverTargetHandler.Action == TargetAction.FireField || (serverTargetHandler.Action == TargetAction.ParalyzeField || serverTargetHandler.Action == TargetAction.PoisonField))
            {
              num18 = 16737894;
              num33 = 2;
              flag6 = true;
            }
            if (num18 != 0 && Gumps.IsWorldAt(Engine.m_xMouse, Engine.m_yMouse, true))
            {
              int TileX = 0;
              int TileY = 0;
              ICell tileFromXy = Renderer.FindTileFromXY(Engine.m_xMouse, Engine.m_yMouse, ref TileX, ref TileY, false);
              if (tileFromXy != null && (tileFromXy.CellType == Renderer.tLandTile || tileFromXy.CellType == Renderer.tStaticItem || (tileFromXy.CellType == Renderer.tMobileCell || tileFromXy.CellType == Renderer.tDynamicItem)))
              {
                num32 = num18;
                if (num33 >= 0)
                {
                  if (flag6)
                  {
                    int num34 = player1.X - TileX;
                    int num35 = player1.Y - TileY;
                    int num36 = num34 - num35;
                    int num37 = num34 + num35;
                    if ((num36 < 0 || num37 < 0) && (num36 >= 0 || num37 >= 0))
                    {
                      num28 = TileX - num5 - num33;
                      num30 = TileX - num5 + num33;
                      num29 = TileY - num6;
                      num31 = TileY - num6;
                    }
                    else
                    {
                      num28 = TileX - num5;
                      num30 = TileX - num5;
                      num29 = TileY - num6 - num33;
                      num31 = TileY - num6 + num33;
                    }
                  }
                  else
                  {
                    num28 = TileX - num5 - num33;
                    num29 = TileY - num6 - num33;
                    num30 = TileX - num5 + num33;
                    num31 = TileY - num6 + num33;
                  }
                }
              }
            }
          }
          StaticItem staticItem1 = (StaticItem) null;
          Item obj1 = (Item) null;
          bool xDouble = false;
          int size = Renderer.cellWidth < Renderer.cellHeight ? Renderer.cellWidth - 1 : Renderer.cellHeight - 1;
          TerrainMeshProvider current = TerrainMeshProviders.Current;
          foreach (MapSubgroup mapSubgroup in Renderer.GetMapSubgroups(size))
          {
            int index3 = mapSubgroup.x;
            int index4 = mapSubgroup.y;
            bool flag6 = false;
            int num18 = (index3 - index4) * 22 + num14;
            int num33 = (index3 + index4) * 22 + num15;
            Stopwatch stopwatch = (Stopwatch) null;
            int count3 = arrayListArray[index3, index4].Count;
            for (int index5 = 0; index5 < count3; ++index5)
            {
              if (stopwatch != null)
              {
                stopwatch.Stop();
                stopwatch = (Stopwatch) null;
              }
              ICell cell = (ICell) arrayListArray[index3, index4][index5];
              Type cellType = cell.CellType;
              if (mapSubgroup.ground == flag6)
              {
                if (cellType == Renderer.tLandTile)
                  flag6 = true;
              }
              else
              {
                if (cellType == Renderer.tLandTile)
                  flag6 = true;
                if (cellType == Renderer.tStaticItem || cellType == Renderer.tDynamicItem)
                {
                  bool flag7 = cellType == Renderer.tStaticItem;
                  bool flag8 = !flag7;
                  IItem obj2 = (IItem) cell;
                  int index6;
                  int num34;
                  TileFlags tileFlags;
                  IHue hue2;
                  if (flag7)
                  {
                    staticItem1 = (StaticItem) obj2;
                    index6 = (int) staticItem1.m_ID;
                    num34 = (int) staticItem1.m_Z;
                    tileFlags = Map.m_ItemFlags[index6];
                    if (num16 >= int.MaxValue || num34 < num16 && !tileFlags[(TileFlag) 268435456L])
                    {
                      hue2 = staticItem1.m_Hue;
                      xDouble = false;
                    }
                    else
                      continue;
                  }
                  else
                  {
                    DynamicItem dynamicItem = (DynamicItem) obj2;
                    int id = (int) dynamicItem.m_ID;
                    num34 = (int) dynamicItem.m_Z;
                    tileFlags = Map.m_ItemFlags[id];
                    if (num16 >= int.MaxValue || num34 < num16 && !tileFlags[(TileFlag) 268435456L])
                    {
                      hue2 = dynamicItem.m_Hue;
                      obj1 = dynamicItem.m_Item;
                      index6 = Map.GetDispID(id, (int) (ushort) obj1.Amount, ref xDouble);
                    }
                    else
                      continue;
                  }
                  if (!tileFlags[(TileFlag) 65536L])
                  {
                    bool flag9 = false;
                    int color = 16777215;
                    if (num32 != 0 && index3 >= num28 && (index4 >= num29 && index3 <= num30) && index4 <= num31)
                    {
                      hue2 = Hues.Grayscale;
                      color = num32;
                      flag9 = true;
                    }
                    if (itemShadows && ShadowManager.HasShadow(index6))
                    {
                      Texture texture = Hues.Shadow.GetItem(index6);
                      if (texture != null && !texture.IsEmpty())
                      {
                        int x = num18 + 22 - (texture.Width >> 1);
                        int y = num33 - (num34 << 2) + 43 - texture.Height;
                        Renderer.PushAlpha(0.5f);
                        texture.DrawShadow(x, y, (float) (texture.Width / 2), (float) (texture.Height - 8));
                        Renderer.PopAlpha();
                      }
                    }
                    if (flag3 && flag7 && !flag9)
                    {
                      StaticItem staticItem2 = staticItem1;
                      if (staticItem2.m_bInit)
                      {
                        if (staticItem2.m_bDraw)
                        {
                          Renderer.PushAlpha(staticItem2.m_fAlpha);
                          Renderer.SetTexture(staticItem2.m_sDraw);
                          fixed (TransformedColoredTextured* pVertex = staticItem2.m_vPool)
                            Renderer.PushQuad(pVertex);
                          Renderer.PopAlpha();
                          continue;
                        }
                        continue;
                      }
                    }
                    if (!flag7 && obj1 != null && obj1.ID == 8198)
                    {
                      if (obj1.CorpseSerial == 0)
                      {
                        int index7 = obj1.Amount;
                        GraphicTranslation graphicTranslation = GraphicTranslators.Corpse[index7];
                        if (graphicTranslation != null)
                        {
                          index7 = graphicTranslation.FallbackId;
                          hue2 = Hues.Load(graphicTranslation.FallbackData ^ 32768);
                        }
                        int animDirection = Engine.GetAnimDirection(obj1.Direction);
                        int num35 = Engine.m_Animations.ConvertAction(index7, obj1.Serial, obj1.X, obj1.Y, animDirection, GenericAction.Die, (Mobile) null);
                        int frameCount = Engine.m_Animations.GetFrameCount(index7, num35, animDirection);
                        int xCenter = num18 + 23;
                        int yCenter = num33 - (num34 << 2) + 20;
                        IHue h = !preserveHue1 ? hue2 : hue1;
                        int TextureX = 0;
                        int TextureY = 0;
                        Frame frame1 = Engine.m_Animations.GetFrame((IAnimationOwner) obj1, index7, num35, animDirection, frameCount - 1, xCenter, yCenter, h, ref TextureX, ref TextureY, preserveHue1);
                        obj1.DrawGame(frame1.Image, TextureX, TextureY, color);
                        obj1.MessageX = TextureX + frame1.Image.xMin + (frame1.Image.xMax - frame1.Image.xMin) / 2;
                        obj1.MessageY = TextureY;
                        obj1.BottomY = TextureY + frame1.Image.yMax;
                        obj1.MessageFrame = Renderer.m_ActFrames;
                        List<Item> sortedCorpseItems = obj1.GetSortedCorpseItems();
                        for (int index8 = 0; index8 < sortedCorpseItems.Count; ++index8)
                        {
                          Item obj3 = sortedCorpseItems[index8];
                          if (obj3.Parent == obj1)
                          {
                            if (!preserveHue1)
                              h = Hues.GetItemHue(obj3.ID, (int) obj3.Hue);
                            Frame frame2 = Engine.m_Animations.GetFrame((IAnimationOwner) obj3, obj3.AnimationId, num35, animDirection, frameCount - 1, xCenter, yCenter, h, ref TextureX, ref TextureY, preserveHue1);
                            obj3.DrawGame(frame2.Image, TextureX, TextureY, color);
                          }
                        }
                      }
                    }
                    else
                    {
                      float num35 = 1f;
                      int ItemID = index6 & 16383;
                      IHue hue3;
                      if (preserveHue1)
                        hue3 = hue1;
                      else if (flag7)
                        hue3 = hue2;
                      else if (obj1 != null && obj1.Flags[ItemFlag.Hidden])
                      {
                        hue3 = Hues.Grayscale;
                        num35 = 0.5f;
                      }
                      else
                        hue3 = hue2;
                      AnimData anim = Map.GetAnim(ItemID);
                      int num36 = index6;
                      if ((int) anim.frameCount != 0 && tileFlags[(TileFlag) 16777216L])
                        num36 += (int) anim[Renderer.m_Frames / ((int) anim.frameInterval + 1) % (int) anim.frameCount];
                      Texture texture = obj2.GetItem(hue3, (short) num36);
                      if (texture != null && !texture.IsEmpty())
                      {
                        int x = num18 + 22 - (texture.Width >> 1);
                        int y = num33 - (num34 << 2) + 43 - texture.Height;
                        if (flag2 && tileFlags[(TileFlag) 131072L])
                        {
                          if (new Rectangle(x + texture.xMin, y + texture.yMin, texture.xMax, texture.yMax).IntersectsWith(Renderer.m_FoliageCheck))
                            num35 *= 0.4f;
                        }
                        else if (tileFlags[(TileFlag) 8L])
                          num35 *= 0.9f;
                        if (flag8)
                        {
                          if (obj1 != null)
                          {
                            Renderer.PushAlpha(num35);
                            obj1.DrawGame(texture, x, y, color);
                            if (xDouble)
                              obj1.DrawGame(texture, x + 5, y + 5, color);
                            Renderer.PopAlpha();
                            if (Renderer.m_Transparency)
                              queue3.Enqueue((object) TransparentDraw.PoolInstance(texture, x, y, num35, xDouble));
                            obj1.MessageX = x + texture.xMin + (texture.xMax - texture.xMin) / 2;
                            obj1.MessageY = y;
                            obj1.BottomY = y + texture.yMax;
                            obj1.MessageFrame = Renderer.m_ActFrames;
                          }
                          else
                          {
                            Renderer.PushAlpha(num35);
                            texture.DrawGame(x, y, color);
                            if (xDouble)
                              texture.DrawGame(x + 5, y + 5, color);
                            Renderer.PopAlpha();
                          }
                        }
                        else
                        {
                          Renderer.PushAlpha(num35);
                          if (tileFlags[(TileFlag) 16777216L])
                          {
                            texture.DrawGame(x, y, color, staticItem1.m_vPool);
                          }
                          else
                          {
                            staticItem1.m_bDraw = texture.DrawGame(x, y, color, staticItem1.m_vPool);
                            staticItem1.m_fAlpha = num35;
                            staticItem1.m_bInit = !flag9;
                            staticItem1.m_sDraw = texture;
                          }
                          Renderer.PopAlpha();
                        }
                      }
                    }
                  }
                }
                else if (cellType == Renderer.tLandTile)
                {
                  LandTile tile = (LandTile) cell;
                  int num34 = (int) tile.m_Z;
                  if ((int) tile.m_ID != 2)
                  {
                    int x = num18;
                    int y = num33;
                    if (x < Engine.GameX + Engine.GameWidth && x + 44 > Engine.GameX && (num17 >= int.MaxValue || num34 < num17))
                    {
                      IHue hue2 = hue1;
                      int baseColor = 16777215;
                      if (num32 != 0 && index3 >= num28 && (index4 >= num29 && index3 <= num30) && index4 <= num31)
                      {
                        hue2 = Hues.Grayscale;
                        baseColor = num32;
                      }
                      tile.EnsureMesh(hue2, current);
                      if (tile._mesh != null)
                      {
                        Renderer.SetTexture(tile.m_sDraw);
                        tile._mesh.Update(tile, baseColor);
                        tile._mesh.Render(x, y);
                      }
                    }
                  }
                }
                else if (cellType == Renderer.tMobileCell && (num16 >= int.MaxValue || (int) cell.Z < num16))
                {
                  IAnimatedCell animatedCell = (IAnimatedCell) cell;
                  int Body = 0;
                  int Direction = 0;
                  int Hue = 0;
                  int Action = 0;
                  int Frame1 = 0;
                  Mobile mobile = ((MobileCell) cell).m_Mobile;
                  int fOffset = 0;
                  int xOffset = 0;
                  int yOffset = 0;
                  if (mobile.Player)
                    flag2 = true;
                  if (mobile.Walking.Count > 0)
                  {
                    WalkAnimation walkAnimation = (WalkAnimation) mobile.Walking.Peek();
                    if (!walkAnimation.Snapshot(ref xOffset, ref yOffset, ref fOffset))
                    {
                      if (!walkAnimation.Advance)
                      {
                        xOffset = walkAnimation.xOffset;
                        yOffset = walkAnimation.yOffset;
                      }
                      else
                      {
                        xOffset = 0;
                        yOffset = 0;
                      }
                      fOffset = walkAnimation.Frames;
                      mobile.SetLocation(walkAnimation.NewX, walkAnimation.NewY, walkAnimation.NewZ);
                      ((WalkAnimation) mobile.Walking.Dequeue()).Dispose();
                      if (mobile.Player)
                      {
                        if (Engine.amMoving)
                          Engine.DoWalk(Engine.movingDir, true);
                        Renderer.eOffsetX += xOffset;
                        Renderer.eOffsetY += yOffset;
                      }
                      if (mobile.Walking.Count == 0)
                      {
                        mobile.Direction = (byte) walkAnimation.NewDir;
                        mobile.IsMoving = false;
                        mobile.MovedTiles = 0;
                        mobile.HorseFootsteps = 0;
                      }
                      else
                        ((WalkAnimation) mobile.Walking.Peek()).Start();
                      queue1.Enqueue((object) mobile);
                    }
                  }
                  List<Item> sortedItems = mobile.GetSortedItems();
                  animatedCell.GetPackage(ref Body, ref Action, ref Direction, ref Frame1, ref Hue);
                  int num34 = Frame1;
                  int frameCount1 = Engine.m_Animations.GetFrameCount(Body, Action, Direction);
                  if (frameCount1 != 0)
                  {
                    int Frame2 = num34 % frameCount1;
                    int num35 = num18 + 22;
                    int num36 = num33 - ((int) animatedCell.Z << 2) + 22;
                    int num37 = num35 + 1;
                    int num38 = num36 - 2;
                    int num39 = num37 + xOffset;
                    int num40 = num38 + yOffset;
                    if (fOffset != 0)
                    {
                      Frame2 = (Frame2 + fOffset) % frameCount1;
                      Frame1 += fOffset;
                      Frame1 %= frameCount1;
                    }
                    if (mobile.Human && mobile.IsMoving && mobile.LastFrame != Frame1)
                    {
                      int? nullable = new int?();
                      if (mobile.IsMounted && (Direction & 128) != 0)
                      {
                        if (Frame1 == 1)
                          nullable = new int?(297);
                        else if (Frame1 == 3)
                          nullable = new int?(298);
                      }
                      else if (Frame1 == 1)
                        nullable = new int?(299);
                      else if (Frame1 == 6)
                        nullable = new int?(300);
                      if (nullable.HasValue && !Preferences.Current.Footsteps.Volume.IsMuted)
                      {
                        float Volume = 0.675f;
                        float Frequency = (float) ((Engine.Random.NextDouble() * 2.0 - 1.0) / 14.0);
                        if (mobile.Player)
                          Engine.Sounds.PlaySound(nullable.Value, -1, -1, -1, Volume, Frequency);
                        else
                          Engine.Sounds.PlaySound(nullable.Value, mobile.X, mobile.Y, mobile.Z, Volume, Frequency);
                      }
                      mobile.LastFrame = Frame1;
                    }
                    bool flag7 = false;
                    bool preserveHue2 = false;
                    IHue hue2 = (IHue) null;
                    bool flag8 = false;
                    float alpha1 = 1f;
                    int color1 = 16777215;
                    IHue h1;
                    if (preserveHue1)
                    {
                      h1 = hue1;
                      preserveHue2 = true;
                      hue2 = h1;
                    }
                    else if (num32 != 0 && index3 >= num28 && (index4 >= num29 && index3 <= num30) && index4 <= num31)
                    {
                      h1 = Hues.Grayscale;
                      color1 = num32;
                      preserveHue2 = true;
                      hue2 = h1;
                      flag7 = false;
                    }
                    else if ((mobile.Flags.Value & -224) != 0)
                    {
                      h1 = Hues.Load(33925);
                      preserveHue2 = true;
                      hue2 = h1;
                      flag7 = false;
                    }
                    else if (mobile.Flags[MobileFlag.Hidden])
                    {
                      h1 = Hues.Grayscale;
                      preserveHue2 = true;
                      hue2 = h1;
                    }
                    else if ((Engine.m_Highlight == mobile || Renderer.m_AlwaysHighlight == mobile.Serial) && !mobile.Player)
                    {
                      h1 = Hues.GetNotoriety(mobile.Notoriety);
                      preserveHue2 = true;
                      hue2 = h1;
                      flag7 = true;
                    }
                    else if (mobile.IsDeadPet)
                    {
                      h1 = Hues.Grayscale;
                      preserveHue2 = true;
                      hue2 = h1;
                      flag7 = true;
                    }
                    else
                      h1 = Hues.Load(Hue);
                    int TextureX1 = 0;
                    int TextureY1 = 0;
                    Frame frame1;
                    try
                    {
                      frame1 = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame2, num39, num40, h1, ref TextureX1, ref TextureY1, preserveHue2);
                    }
                    catch
                    {
                      frame1 = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame2, num39, num40, h1, ref TextureX1, ref TextureY1, preserveHue2);
                    }
                    bool flag9 = false;
                    float alpha2 = 1f;
                    int x1 = -1;
                    int y = -1;
                    Texture t = (Texture) null;
                    if (frame1.Image != null && !frame1.Image.IsEmpty())
                    {
                      mobile.MessageFrame = Renderer.m_ActFrames;
                      mobile.ScreenX = mobile.MessageX = num39;
                      mobile.ScreenY = num40;
                      mobile.MessageY = TextureY1;
                      if (mobile.Player)
                        Renderer.m_FoliageCheck = new Rectangle(TextureX1, TextureY1, frame1.Image.Width, frame1.Image.Height);
                      if (flag1 && !mobile.Player && (mobile.Notoriety >= Notoriety.Innocent && mobile.Notoriety <= Notoriety.Vendor) && notorietyHalos)
                      {
                        Texture playerHalo = Engine.ImageCache.PlayerHalo;
                        playerHalo.DrawGame(num39 - (playerHalo.Width >> 1), num40 - (playerHalo.Height >> 1), Engine.C16232((int) Hues.GetNotorietyData(mobile.Notoriety).colors[47]));
                      }
                      if (characterShadows && !mobile.IsDead)
                      {
                        int TextureX2 = 0;
                        int TextureY2 = 0;
                        Renderer.PushAlpha(0.5f);
                        Frame frame2 = Engine.m_Animations.GetFrame((IAnimationOwner) mobile, Body, Action, Direction, Frame2, num39, num40, (IHue) Hues.Shadow, ref TextureX2, ref TextureY2, false);
                        if (frame2.Image != null && !frame2.Image.IsEmpty())
                          frame2.Image.DrawShadow(TextureX2, TextureY2, (float) (num39 - TextureX2), (float) (num40 - TextureY2));
                        if (mobile.HumanOrGhost)
                        {
                          bool isMounted = mobile.IsMounted;
                          for (int index6 = 0; index6 < sortedItems.Count; ++index6)
                          {
                            Item obj2 = sortedItems[index6];
                            if (obj2.Layer == Layer.Mount || obj2.Layer == Layer.OneHanded || obj2.Layer == Layer.TwoHanded)
                            {
                              int animationId = obj2.AnimationId;
                              int num41 = Action;
                              int num42 = Frame1;
                              if (obj2.Layer == Layer.Mount)
                              {
                                if (mobile.IsMoving)
                                  num41 = (Direction & 128) == 0 ? 0 : 1;
                                else if (mobile.Animation == null)
                                  num41 = 2;
                                else if (num41 == 23)
                                  num41 = 0;
                                else if (num41 == 24)
                                  num41 = 1;
                                else if (num41 >= 25 && num41 <= 29)
                                  num41 = 2;
                                else
                                  continue;
                              }
                              else if (isMounted)
                              {
                                if (mobile.IsMoving)
                                  num41 = 23 + ((Direction & 128) >> 7);
                                else if (mobile.Animation == null)
                                  num41 = 25;
                              }
                              int hue3 = (int) obj2.Hue;
                              if (obj2.Layer == Layer.Mount)
                              {
                                int bodyID = animationId;
                                Engine.m_Animations.Translate(ref bodyID, ref hue3);
                              }
                              int frameCount2 = Engine.m_Animations.GetFrameCount(animationId, num41, Direction);
                              int Frame3 = frameCount2 != 0 ? num42 % frameCount2 : 0;
                              Frame frame3 = Engine.m_Animations.GetFrame((IAnimationOwner) obj2, animationId, num41, Direction, Frame3, num39, num40, (IHue) Hues.Shadow, ref TextureX2, ref TextureY2, false);
                              if (frame3.Image != null && !frame3.Image.IsEmpty())
                                frame3.Image.DrawShadow(TextureX2, TextureY2, (float) (num39 - TextureX2), (float) (num40 - TextureY2));
                            }
                          }
                        }
                        Renderer.PopAlpha();
                      }
                      if (mobile.Flags[MobileFlag.Hidden] || Body == 970 || (mobile.IsDeadPet || mobile.Ghost))
                      {
                        flag8 = true;
                        alpha1 = 0.5f;
                      }
                      Renderer.PushAlpha(alpha1);
                      if (mobile.HumanOrGhost && mobile.IsMounted)
                      {
                        alpha2 = Renderer._alphaValue;
                        t = frame1.Image;
                        x1 = TextureX1;
                        y = TextureY1;
                        flag9 = true;
                      }
                      else
                        flag9 = false;
                      if (!mobile.Ghost && !flag9)
                        mobile.DrawGame(frame1.Image, TextureX1, TextureY1, color1);
                      Renderer.PopAlpha();
                    }
                    int height;
                    int num43 = num33 - ((int) cell.Z << 2) + 18 - (height = Engine.m_Animations.GetHeight(Body, Action, Direction));
                    int x2 = num18 + 22 + xOffset;
                    int num44 = num43 + yOffset;
                    if (Options.Current.MiniHealth && mobile.OpenedStatus && (!mobile.Ghost && mobile.MaximumHitPoints > 0))
                      queue2.Enqueue((object) MiniHealthEntry.PoolInstance(x2, num44 + 4 + height, mobile));
                    if (mobile.HumanOrGhost)
                    {
                      bool isMounted = mobile.IsMounted;
                      for (int index6 = 0; index6 < sortedItems.Count; ++index6)
                      {
                        Item obj2 = sortedItems[index6];
                        if (!mobile.Ghost || obj2.Layer == Layer.OuterTorso)
                        {
                          int num41 = obj2.AnimationId;
                          int num42 = Action;
                          int num45 = Frame1;
                          if (mobile.Ghost)
                            num41 = 970;
                          if (obj2.Layer == Layer.Mount)
                          {
                            if (mobile.IsMoving)
                              num42 = (Direction & 128) == 0 ? 0 : 1;
                            else if (mobile.Animation == null)
                              num42 = 2;
                            else if (num42 == 23)
                              num42 = 0;
                            else if (num42 == 24)
                              num42 = 1;
                            else if (num42 >= 25 && num42 <= 29)
                              num42 = 2;
                            else
                              goto label_290;
                          }
                          else if (isMounted)
                          {
                            if (mobile.IsMoving)
                              num42 = 23 + ((Direction & 128) >> 7);
                            else if (mobile.Animation == null)
                              num42 = 25;
                          }
                          float num46 = alpha1;
                          int hue3 = (int) obj2.Hue;
                          if (obj2.Layer == Layer.Mount)
                          {
                            int bodyID = num41;
                            Engine.m_Animations.Translate(ref bodyID, ref hue3);
                          }
                          IHue h2;
                          bool preserveHue3;
                          if (!preserveHue2 || obj2.Layer == Layer.Mount && flag7)
                          {
                            h2 = Hues.GetItemHue(obj2.ID, (int) obj2.Hue);
                            preserveHue3 = false;
                          }
                          else
                          {
                            h2 = hue2;
                            preserveHue3 = preserveHue2;
                          }
                          int frameCount2 = Engine.m_Animations.GetFrameCount(num41, num42, Direction);
                          int Frame3 = frameCount2 != 0 ? num45 % frameCount2 : 0;
                          Frame frame2 = Engine.m_Animations.GetFrame((IAnimationOwner) obj2, num41, num42, Direction, Frame3, num39, num40, h2, ref TextureX1, ref TextureY1, preserveHue3);
                          if (frame2.Image != null && !frame2.Image.IsEmpty())
                          {
                            Renderer.PushAlpha(mobile.Ghost ? 0.5f : num46);
                            obj2.DrawGame(frame2.Image, TextureX1, TextureY1, color1);
                            Renderer.PopAlpha();
                          }
label_290:
                          if (obj2.Layer == Layer.Mount && flag9)
                          {
                            Renderer.PushAlpha(alpha2);
                            mobile.DrawGame(t, x1, y, color1);
                            Renderer.PopAlpha();
                          }
                        }
                      }
                    }
                    if (flag5)
                    {
                      int color2 = -1;
                      if (mobile == TargetManager.LastOffensiveTarget)
                        color2 = 16720384;
                      else if (mobile == TargetManager.LastDefensiveTarget)
                        color2 = 2285055;
                      else if (mobile == TargetManager.LastTarget)
                        color2 = 13421772;
                      if (color2 != -1)
                        Renderer.DrawPlayerIcon(num39, num40, Engine.ImageCache.LastTargetIcon, color2);
                    }
                    if (notorietyHalos)
                    {
                      if (mobile.m_IsFriend)
                        Renderer.DrawPlayerIcon(num39, TextureY1 - 10, Engine.ImageCache.PlayerAlly, 16777215);
                      else if (!mobile.Player && player1.Warmode && (mobile.Visible && mobile.Human) && (!mobile.IsDead && TargetManager.IsAcquirable(player1, mobile)))
                        Renderer.DrawPlayerIcon(num39, TextureY1 - 10, Engine.ImageCache.PlayerEnemy, 16777215);
                    }
                  }
                }
              }
            }
            if (stopwatch != null)
              stopwatch.Stop();
            if (flag4 && index3 >= num24 && (index3 <= num26 && index4 >= num25) && index4 <= num27)
            {
              int num34 = num18 + 22;
              int num35 = num33 + 43;
              if (Renderer.m_vMultiPool == null)
                Renderer.m_vMultiPool = VertexConstructor.Create();
              for (int index5 = 0; index5 < num23; ++index5)
              {
                MultiItem multiItem = (MultiItem) Engine.m_MultiList[index5];
                if ((int) multiItem.X == index3 - num20 && (int) multiItem.Y == index4 - num21 && (int) multiItem.Z == 0)
                {
                  int ItemID = (int) multiItem.ItemID & 16383;
                  AnimData anim = Map.GetAnim(ItemID);
                  Texture texture = (int) anim.frameCount == 0 || !Map.m_ItemFlags[ItemID][(TileFlag) 16777216L] ? hue1.GetItem((int) multiItem.ItemID) : hue1.GetItem((int) multiItem.ItemID + (int) anim[Renderer.m_Frames / ((int) anim.frameInterval + 1) % (int) anim.frameCount]);
                  if (texture != null && !texture.IsEmpty())
                    texture.DrawGame(num34 - (texture.Width >> 1), num35 - (num22 + (int) multiItem.Z << 2) - texture.Height);
                }
                else if ((int) multiItem.X + num20 > index3 || (int) multiItem.X + num20 == index3 && (int) multiItem.Y + num21 >= index4)
                  break;
              }
            }
            for (int index5 = 0; index5 < arrayList2.Count; ++index5)
            {
              Renderer.DrawQueueEntry drawQueueEntry = (Renderer.DrawQueueEntry) arrayList2[index5];
              if (drawQueueEntry.m_TileX == index3 && drawQueueEntry.m_TileY == index4)
              {
                arrayList2.RemoveAt(index5);
                --index5;
                drawQueueEntry.m_Texture.Flip = drawQueueEntry.m_Flip;
                Clipper Clipper = new Clipper(Engine.GameX, num33 - 46, Engine.GameWidth, Engine.GameHeight - num33 + 46);
                Renderer.PushAlpha(drawQueueEntry.m_fAlpha);
                drawQueueEntry.m_Texture.DrawClipped(drawQueueEntry.m_DrawX, drawQueueEntry.m_DrawY, Clipper);
                Renderer.PopAlpha();
              }
            }
          }
          if (Renderer.m_Transparency)
          {
            if (Renderer.m_vTransDrawPool == null)
              Renderer.m_vTransDrawPool = VertexConstructor.Create();
            while (queue3.Count > 0)
            {
              TransparentDraw transparentDraw = (TransparentDraw) queue3.Dequeue();
              Renderer.PushAlpha(transparentDraw.m_fAlpha * 0.5f);
              transparentDraw.m_Texture.DrawGame(transparentDraw.m_X, transparentDraw.m_Y);
              if (transparentDraw.m_Double)
                transparentDraw.m_Texture.DrawGame(transparentDraw.m_X + 5, transparentDraw.m_Y + 5);
              Renderer.PopAlpha();
              transparentDraw.Dispose();
            }
          }
          Renderer.SetTexture((Texture) null);
          while (queue2.Count > 0)
          {
            MiniHealthEntry miniHealthEntry = (MiniHealthEntry) queue2.Dequeue();
            Mobile mobile = miniHealthEntry.m_Mobile;
            Renderer.TransparentRect(0, miniHealthEntry.m_X - 16, miniHealthEntry.m_Y + 8, 32, 7);
            double num18 = (double) mobile.CurrentHitPoints / (double) mobile.MaximumHitPoints;
            if (num18 == double.NaN)
              num18 = 0.0;
            else if (num18 < 0.0)
              num18 = 0.0;
            else if (num18 > 1.0)
              num18 = 1.0;
            int Width = (int) (30.0 * num18 + 0.5);
            MobileFlags flags = mobile.Flags;
            int Color;
            int Color2;
            if (mobile.IsPoisoned)
            {
              Color = 65280;
              Color2 = 32768;
            }
            else if (flags[MobileFlag.YellowHits])
            {
              Color = 16760832;
              Color2 = 8413184;
            }
            else
            {
              Color = 2146559;
              Color2 = 1073280;
            }
            Renderer.PushAlpha(0.6f);
            Renderer.GradientRect(Color, Color2, miniHealthEntry.m_X - 15, miniHealthEntry.m_Y + 9, Width, 5);
            Renderer.GradientRect(6553600, 13107200, miniHealthEntry.m_X - 15 + Width, miniHealthEntry.m_Y + 9, 30 - Width, 5);
            Renderer.PopAlpha();
            miniHealthEntry.Dispose();
          }
          if (Engine.m_Ingame)
          {
            Engine.Effects.Draw();
            if (Renderer.eOffsetX != 0 || Renderer.eOffsetY != 0)
              Engine.Effects.Offset(Renderer.eOffsetX, Renderer.eOffsetY);
          }
          Map.Unlock();
          if (Renderer._profile != null)
            Renderer._profile._worldTime.Stop();
          Renderer.SetViewport(0, 0, Engine.ScreenWidth, Engine.ScreenHeight);
          if (Renderer.lightTexture != null)
          {
            int num18 = Engine.Effects.GlobalLight;
            if (player1 != null)
              num18 -= player1.LightLevel;
            if (num18 < 0)
              num18 = 0;
            else if (num18 > 31)
              num18 = 31;
            if (num18 != 0)
            {
              Renderer.PushAlpha((float) num18 / 31f);
              Renderer.SetBlendType(DrawBlendType.LightSource);
              Renderer.lightTexture.Draw(Engine.GameX, Engine.GameY);
              Renderer.SetBlendType(DrawBlendType.Normal);
              Renderer.PopAlpha();
            }
          }
        }
        if (!Engine.m_Ingame)
        {
          Texture sallosDragon = Engine.ImageCache.SallosDragon;
          if (sallosDragon != null)
            sallosDragon.Draw((Engine.ScreenWidth - sallosDragon.Width) / 2, (Engine.ScreenHeight - sallosDragon.Height) / 2);
        }
        if (!Engine.m_Loading)
        {
          MessageManager.BeginRender();
          if (Engine.m_Ingame && Renderer.m_TextSurface != null)
          {
            Renderer.SetTexture((Texture) null);
            if (player1 != null && player1.OpenedStatus && player1.StatusBar == null)
            {
              Renderer.PushAlpha(0.5f);
              Renderer.SolidRect(0, Engine.GameX + 2, Engine.GameY + Engine.GameHeight - 21, Engine.GameWidth - 46, 19);
              Renderer.PopAlpha();
              int X1 = Engine.GameX + Engine.GameWidth - 44;
              int Y1 = Engine.GameY + Engine.GameHeight - 21;
              Renderer.SolidRect(0, X1, Y1, 42, 19);
              int X2 = X1 + 1;
              int Y2 = Y1 + 1;
              if (player1.Ghost)
              {
                Renderer.GradientRect(12632256, 6316128, X2, Y2, 40, 5);
                int Y3 = Y2 + 6;
                Renderer.GradientRect(12632256, 6316128, X2, Y3, 40, 5);
                int Y4 = Y3 + 6;
                Renderer.GradientRect(12632256, 6316128, X2, Y4, 40, 5);
              }
              else
              {
                int Width = (int) ((double) player1.CurrentHitPoints / (double) player1.MaximumHitPoints * 40.0);
                if (Width > 40)
                  Width = 40;
                else if (Width < 0)
                  Width = 0;
                MobileFlags flags = player1.Flags;
                int Color;
                int Color2;
                if (player1.IsPoisoned)
                {
                  Color = 65280;
                  Color2 = 32768;
                }
                else if (flags[MobileFlag.YellowHits])
                {
                  Color = 16760832;
                  Color2 = 8413184;
                }
                else
                {
                  Color = 2146559;
                  Color2 = 1073280;
                }
                Renderer.GradientRect(Color, Color2, X2, Y2, Width, 5);
                Renderer.GradientRect(16711680, 8388608, X2 + Width, Y2, 40 - Width, 5);
                int Y3 = Y2 + 6;
                int num5 = (int) ((double) player1.CurrentMana / (double) player1.MaximumMana * 40.0);
                if (num5 > 40)
                  num5 = 40;
                else if (num5 < 0)
                  num5 = 0;
                Renderer.GradientRect(2146559, 1073280, X2, Y3, 40, 5);
                Renderer.GradientRect(16711680, 8388608, X2 + num5, Y3, 40 - num5, 5);
                int Y4 = Y3 + 6;
                int num6 = (int) ((double) player1.CurrentStamina / (double) player1.MaximumStamina * 40.0);
                if (num6 > 40)
                  num6 = 40;
                else if (num6 < 0)
                  num6 = 0;
                Renderer.GradientRect(2146559, 1073280, X2, Y4, 40, 5);
                Renderer.GradientRect(16711680, 8388608, X2 + num6, Y4, 40 - num6, 5);
              }
            }
            else
            {
              Renderer.PushAlpha(0.5f);
              Renderer.SolidRect(0, Engine.GameX + 2, Engine.GameY + Engine.GameHeight - 21, Engine.GameWidth - 4, 19);
              Renderer.PopAlpha();
            }
            Renderer.m_vTextCache.Draw(Renderer.m_TextSurface, Engine.GameX + 2, Engine.GameY + Engine.GameHeight - 2 - Renderer.m_TextSurface.Height);
          }
          if (Renderer._profile != null)
            Renderer._profile._gumpTime.Start();
          Gumps.Draw();
          if (Renderer._profile != null)
            Renderer._profile._gumpTime.Stop();
        }
        if (Engine.m_Ingame)
        {
          int num5 = Engine.GameY;
          int num6 = Engine.GameHeight;
          if (Renderer.m_TextSurface != null)
          {
            int num7 = Renderer.m_TextSurface.Height;
          }
          ArrayList arrayList2 = Renderer.m_RectsList;
          if (arrayList2 == null)
            arrayList2 = Renderer.m_RectsList = new ArrayList();
          else if (arrayList2.Count > 0)
            arrayList2.Clear();
          World.DrawAllMessages();
          arrayList1.Sort();
          int count1 = arrayList1.Count;
          for (int index = 0; index < count1; ++index)
          {
            TextMessage textMessage = (TextMessage) arrayList1[index];
            int x = textMessage.X + Engine.GameX;
            int y = textMessage.Y + Engine.GameY;
            if (x < Engine.GameX + 2)
              x = Engine.GameX + 2;
            else if (x + textMessage.Image.Width >= Engine.GameX + Engine.GameWidth - 2)
              x = Engine.GameX + Engine.GameWidth - textMessage.Image.Width - 2;
            if (y < Engine.GameY + 2)
              y = Engine.GameY + 2;
            else if (y + textMessage.Image.Height >= Engine.GameY + Engine.GameHeight - 2)
              y = Engine.GameY + Engine.GameHeight - textMessage.Image.Height - 2;
            arrayList2.Add((object) new Rectangle(x, y, textMessage.Image.Width, textMessage.Image.Height));
          }
          for (int index1 = 0; index1 < count1; ++index1)
          {
            TextMessage textMessage = (TextMessage) arrayList1[index1];
            Rectangle rect = (Rectangle) arrayList2[index1];
            float num8 = 1f;
            int count2 = arrayList2.Count;
            for (int index2 = index1 + 1; index2 < count2; ++index2)
            {
              if (((Rectangle) arrayList2[index2]).IntersectsWith(rect))
                num8 += ((TextMessage) arrayList1[index2]).Alpha;
            }
            float alpha = 1f / num8;
            if (textMessage.Disposing)
              alpha *= textMessage.Alpha;
            Renderer.PushAlpha(alpha);
            textMessage.Draw(rect.X, rect.Y);
            Renderer.PopAlpha();
          }
          if (Renderer.eOffsetX != 0 || Renderer.eOffsetY != 0)
            World.Offset(Renderer.eOffsetX, Renderer.eOffsetY);
        }
        if (!Engine.m_Loading)
          Cursor.Draw();
        Renderer.PushAll();
      }
      catch (SharpDXException ex)
      {
        Result resultCode = ex.get_ResultCode();
        // ISSUE: explicit reference operation
        int code = ((Result) @resultCode).get_Code();
        if (code == ((ResultDescriptor) ResultCode.DeviceLost).get_Code())
        {
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Application.DoEvents();
          Thread.Sleep(10);
        }
        else if (code == ((ResultDescriptor) ResultCode.DeviceNotReset).get_Code())
        {
          Engine.m_Device.Reset(new PresentParameters[1]
          {
            Engine.m_PresentParams
          });
          Engine.OnDeviceReset((object) null, (EventArgs) null);
          GC.Collect();
        }
        else
        {
          Thread.Sleep(10);
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Debug.Error((Exception) ex);
        }
      }
      catch (Exception ex)
      {
        Debug.Trace("Draw Exception:");
        Debug.Error(ex);
      }
      finally
      {
        Engine.m_Device.EndScene();
      }
      if (Renderer._profile != null)
        Renderer._profile._drawTime.Stop();
      try
      {
        Engine.m_Device.Present();
      }
      catch (SharpDXException ex)
      {
        Result resultCode = ex.get_ResultCode();
        // ISSUE: explicit reference operation
        int code = ((Result) @resultCode).get_Code();
        if (code == ((ResultDescriptor) ResultCode.DeviceLost).get_Code())
        {
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Application.DoEvents();
          Thread.Sleep(10);
        }
        else if (code == ((ResultDescriptor) ResultCode.DeviceNotReset).get_Code())
        {
          Engine.m_Device.Reset(new PresentParameters[1]
          {
            Engine.m_PresentParams
          });
          Engine.OnDeviceReset((object) null, (EventArgs) null);
          GC.Collect();
        }
        else
        {
          Thread.Sleep(10);
          try
          {
            Engine.m_Device.Reset(new PresentParameters[1]
            {
              Engine.m_PresentParams
            });
            Engine.OnDeviceReset((object) null, (EventArgs) null);
          }
          catch
          {
          }
          Debug.Error((Exception) ex);
        }
      }
      Renderer.m_Count = 0;
      if (Renderer.screenshotContext != null)
      {
        Renderer.screenshotContext.Save();
        Renderer.screenshotContext = (Renderer.ScreenshotContext) null;
      }
      ++Renderer.m_ActFrames;
      while (queue1.Count > 0)
      {
        Mobile mobile = (Mobile) queue1.Dequeue();
        ++mobile.MovedTiles;
        mobile.Update();
      }
      Map.Unlock();
    }

    public static void BeginGroup()
    {
      ++Renderer._drawGroup;
    }

    public static void EndGroup()
    {
      --Renderer._drawGroup;
      ++Renderer.m_Count;
    }

    public static void SetBlendType(DrawBlendType type)
    {
      if (Renderer._blendType == type)
        return;
      Renderer._blendType = type;
      Renderer.UpdateAlphaSettings();
    }

    public static void PushAll()
    {
      if (Renderer._profile != null)
      {
        ++Renderer._profile._pushes;
        Renderer._profile._pushTime.Start();
      }
      Renderer.PushAll(0, false, false);
      Renderer.PushAll(1, true, false);
      Renderer.PushAll(1, true, true);
      Renderer.PushAll(1, false, false);
      Renderer.PushAll(1, false, true);
      Renderer.PushAll(2, false, false);
      Renderer.PushAll(2, false, true);
      Renderer.PushAll(3, false, false);
      Renderer.PushAll(3, false, true);
      Renderer.PushAlphaStates();
      if (Renderer._profile != null)
        Renderer._profile._pushTime.Stop();
      ++Renderer._renderCount;
    }

    private static void EnsureFilterState(bool filterState)
    {
      if (Renderer.m_CurFilter == filterState)
        return;
      Renderer.m_CurFilter = filterState;
      TextureFilter textureFilter = filterState ? (TextureFilter) 2 : (TextureFilter) 1;
      Engine.m_Device.SetSamplerState(0, (SamplerState) 6, textureFilter);
      Engine.m_Device.SetSamplerState(0, (SamplerState) 5, textureFilter);
      Engine.m_Device.SetRenderState<ShadeMode>((RenderState) 9, (M0) 2);
      Engine.m_Device.SetRenderState((RenderState) 161, filterState && Preferences.Current.RenderSettings.SmoothingMode != 0);
    }

    private static void PushAlphaStates()
    {
      if (Renderer.m_AlphaStateCount == 0)
        return;
      if (Renderer.m_VertexStream == null)
        Renderer.m_VertexStream = new BufferedVertexStream(Engine.m_VertexBuffer, 32768, TransformedColoredTextured.StrideSize);
      Device device = Engine.m_Device;
      device.SetRenderState((RenderState) 14, false);
      device.SetRenderState((RenderState) 27, true);
      for (int index1 = 0; index1 < Renderer.m_AlphaStateCount; ++index1)
      {
        Renderer.AlphaState alphaState = Renderer.m_AlphaStates[index1];
        Texture tex = alphaState.m_Texture;
        TextureVB textureVb = alphaState.m_TextureVB;
        if (textureVb.m_Count > 0 && textureVb.m_Frame == Renderer._renderCount)
        {
          if (Renderer.m_CurAlphaTest)
          {
            Renderer.m_CurAlphaTest = false;
            device.SetRenderState((RenderState) 15, false);
          }
          Renderer.EnsureFilterState(alphaState.m_Filter);
          if (alphaState.m_BlendType != Renderer.m_CurBlendType)
          {
            Renderer.m_CurBlendType = alphaState.m_BlendType;
            switch (Renderer.m_CurBlendType)
            {
              case DrawBlendType.Normal:
                device.SetRenderState((RenderState) 206, false);
                device.SetRenderState<Blend>((RenderState) 19, (M0) 5);
                device.SetRenderState<Blend>((RenderState) 20, (M0) 6);
                device.SetRenderState<BlendOperation>((RenderState) 171, (M0) 1);
                break;
              case DrawBlendType.Additive:
                device.SetRenderState((RenderState) 206, false);
                device.SetRenderState<Blend>((RenderState) 19, (M0) 5);
                device.SetRenderState<Blend>((RenderState) 20, (M0) 2);
                device.SetRenderState<BlendOperation>((RenderState) 171, (M0) 1);
                break;
              case DrawBlendType.Subtractive:
                device.SetRenderState((RenderState) 206, true);
                device.SetRenderState<Blend>((RenderState) 19, (M0) 5);
                device.SetRenderState<Blend>((RenderState) 20, (M0) 2);
                device.SetRenderState<BlendOperation>((RenderState) 171, (M0) 3);
                device.SetRenderState<Blend>((RenderState) 207, (M0) 5);
                device.SetRenderState<Blend>((RenderState) 208, (M0) 7);
                device.SetRenderState<BlendOperation>((RenderState) 209, (M0) 5);
                break;
              case DrawBlendType.LightSource:
                device.SetRenderState((RenderState) 206, false);
                device.SetRenderState<Blend>((RenderState) 19, (M0) 1);
                device.SetRenderState<Blend>((RenderState) 20, (M0) 3);
                device.SetRenderState<BlendOperation>((RenderState) 171, (M0) 3);
                break;
              case DrawBlendType.BlackTransparency:
                device.SetRenderState((RenderState) 206, false);
                device.SetRenderState<Blend>((RenderState) 19, (M0) 1);
                device.SetRenderState<Blend>((RenderState) 20, (M0) 3);
                device.SetRenderState<BlendOperation>((RenderState) 171, (M0) 1);
                break;
            }
          }
          int index2 = alphaState._type;
          Renderer.ObjectFormat objectFormat = Renderer._formats[index2];
          int vertexCount = textureVb.m_Count * objectFormat.VertexCount;
          int num = Renderer.m_VertexStream.Push(textureVb._buffer, 0, vertexCount, true);
          if (num >= 0)
          {
            Renderer.EnsureTexture(tex);
            IndexBuffer indexBuffer = objectFormat.IndexBuffer;
            if (indexBuffer != null)
            {
              if (Renderer._currentIndexBuffer != indexBuffer)
              {
                Renderer._currentIndexBuffer = indexBuffer;
                device.set_Indices(indexBuffer);
              }
              device.DrawIndexedPrimitive(objectFormat.Type, num, 0, vertexCount, 0, objectFormat.PrimitiveCount * textureVb.m_Count);
            }
            else
              device.DrawPrimitives(objectFormat.Type, num, textureVb.m_Count * objectFormat.PrimitiveCount);
            if (Renderer._profile != null)
              ++Renderer._profile._draws;
          }
        }
      }
      device.SetRenderState((RenderState) 27, false);
      device.SetRenderState((RenderState) 14, true);
      Renderer.m_AlphaStateCount = 0;
    }

    private static void EnsureTexture(Texture tex)
    {
      BaseTexture baseTexture1 = (BaseTexture) null;
      BaseTexture baseTexture2 = (BaseTexture) null;
      PixelShader pixelShader = (PixelShader) null;
      if (tex != null)
      {
        baseTexture1 = (BaseTexture) tex.m_Surface;
        if (tex._shaderData != null)
        {
          if (tex._shaderData.DataSurface != null)
            baseTexture2 = (BaseTexture) tex._shaderData.DataSurface.m_Surface;
          pixelShader = tex._shaderData.PixelShader;
        }
      }
      if (baseTexture1 != Renderer._tex0)
      {
        Renderer._tex0 = baseTexture1;
        Engine.m_Device.SetTexture(0, baseTexture1);
        if (Renderer._profile != null)
          ++Renderer._profile._tex0;
      }
      if (baseTexture2 != null && baseTexture2 != Renderer._tex1)
      {
        Renderer._tex1 = baseTexture2;
        Engine.m_Device.SetTexture(1, baseTexture2);
        if (Renderer._profile != null)
          ++Renderer._profile._tex1;
      }
      if (pixelShader != Renderer._psh)
      {
        Renderer._psh = pixelShader;
        Engine.m_Device.set_PixelShader(pixelShader);
        if (Renderer._profile != null)
          ++Renderer._profile._psh;
      }
      if (tex == null || tex._shaderData == null || (pixelShader == null || tex._shaderData.RenderCallback == null))
        return;
      tex._shaderData.RenderCallback();
    }

    public static IVertexStorage AcquireSolidStorage(int type)
    {
      if (Renderer._profile != null)
        Renderer._profile._acquireTime.Start();
      int num = 0;
      if (Renderer._alphaTestEnable)
        num |= 1;
      if (Renderer._filterEnable)
        num |= 2;
      int index = num | type << 2;
      Texture texture = Renderer.m_Texture ?? Texture.Empty;
      TextureVB vb = texture.GetVB(type, Renderer._alphaTestEnable, Renderer._filterEnable);
      if (vb.m_Frame != Renderer._renderCount)
      {
        List<Texture> textureList = Renderer.m_Lists[index];
        if (textureList == null)
          Renderer.m_Lists[index] = textureList = new List<Texture>();
        textureList.Add(texture);
      }
      if (Renderer._profile != null)
        Renderer._profile._acquireTime.Stop();
      return (IVertexStorage) vb;
    }

    private static IndexBuffer CreateIndexBuffer(int primitiveCount, int vertexCount, int[] indices)
    {
      short[] numArray = new short[primitiveCount * indices.Length];
      int index1 = 0;
      int num1 = 0;
      int num2 = 0;
      while (num2 < primitiveCount)
      {
        int index2 = 0;
        while (index2 < indices.Length)
        {
          numArray[index1] = (short) (num1 + indices[index2]);
          ++index2;
          ++index1;
        }
        ++num2;
        num1 += vertexCount;
      }
      IndexBuffer indexBuffer = new IndexBuffer(Engine.m_Device, numArray.Length * 2, (Usage) 8, (Pool) 1, true);
      indexBuffer.Lock(0, 0, (LockFlags) 0).WriteRange<short>((M0[]) numArray);
      indexBuffer.Unlock();
      return indexBuffer;
    }

    private static Surface AcquireBackBuffer()
    {
      return Renderer._backBuffer ?? (Renderer._backBuffer = Renderer.GetBackBuffer());
    }

    private static Surface GetBackBuffer()
    {
      return Engine.m_Device.GetBackBuffer(0, 0);
    }

    public static void Reset()
    {
      Renderer.SetupFormats();
      Renderer._currentIndexBuffer = (IndexBuffer) null;
      Renderer._tex0 = (BaseTexture) null;
      Renderer._tex1 = (BaseTexture) null;
      Renderer._psh = (PixelShader) null;
      Renderer._backBuffer = (Surface) null;
      Renderer.lightSurface = (Surface) null;
      Renderer.m_CurBlendType = ~DrawBlendType.Normal;
      Renderer.m_CurAlphaTest = false;
      Renderer.m_CurFilter = false;
    }

    public static void SetupFormats()
    {
      Renderer._formats = new Renderer.ObjectFormat[4]
      {
        new Renderer.ObjectFormat((PrimitiveType) 2, 1, 2, (IndexBuffer) null),
        new Renderer.ObjectFormat((PrimitiveType) 4, 2, 4, Renderer.CreateIndexBuffer(16384, 4, new int[6]{ 3, 1, 2, 2, 1, 0 })),
        null,
        null
      };
      Renderer.SetupTerrainFormats();
    }

    public static void SetupTerrainFormats()
    {
      TerrainMeshProvider current = TerrainMeshProviders.Current;
      Renderer._formats[2] = new Renderer.ObjectFormat((PrimitiveType) 4, current.Divisions * current.Divisions * 2, current.Size, Renderer.CreateIndexBuffer(4096, current.Size, current.GetIndices(true)));
      if (current.Divisions == 1)
        Renderer._formats[3] = new Renderer.ObjectFormat((PrimitiveType) 4, current.Divisions * current.Divisions * 2, current.Size, Renderer.CreateIndexBuffer(4096, current.Size, current.GetIndices(false)));
      else
        Renderer._formats[3] = Renderer._formats[2];
    }

    public static IVertexStorage AcquireAlphaStorage(int type)
    {
      if (Renderer._profile != null)
        Renderer._profile._acquireTime.Start();
      DrawBlendType drawBlendType = Renderer._blendType;
      Texture texture = Renderer.m_Texture ?? Texture.Empty;
      Renderer.AlphaState alphaState = Renderer.m_AlphaStateCount > 0 ? Renderer.m_AlphaStates[Renderer.m_AlphaStateCount - 1] : (Renderer.AlphaState) null;
      if (alphaState == null || alphaState._type != type || (alphaState.m_BlendType != drawBlendType || alphaState.m_Texture != texture) || alphaState.m_Filter != Renderer._filterEnable)
      {
        if (Renderer.m_AlphaStateCount < Renderer.m_AlphaStates.Count)
          alphaState = Renderer.m_AlphaStates[Renderer.m_AlphaStateCount];
        else
          Renderer.m_AlphaStates.Add(alphaState = new Renderer.AlphaState());
        alphaState._type = type;
        alphaState.m_BlendType = drawBlendType;
        alphaState.m_Texture = texture;
        alphaState.m_Filter = Renderer._filterEnable;
        ++Renderer.m_AlphaStateCount;
      }
      if (Renderer._profile != null)
        Renderer._profile._acquireTime.Stop();
      return (IVertexStorage) alphaState.m_TextureVB;
    }

    private static void PushAll(int type, bool alphaTest, bool filter)
    {
      int num1 = 0;
      if (alphaTest)
        num1 |= 1;
      if (filter)
        num1 |= 2;
      int index1 = num1 | type << 2;
      List<Texture> textureList = Renderer.m_Lists[index1];
      if (textureList == null || textureList.Count == 0)
        return;
      Device device = Engine.m_Device;
      if (alphaTest != Renderer.m_CurAlphaTest)
      {
        Renderer.m_CurAlphaTest = alphaTest;
        device.SetRenderState((RenderState) 15, alphaTest);
      }
      Renderer.EnsureFilterState(filter);
      Renderer.ObjectFormat objectFormat = Renderer._formats[type];
      if (Renderer.m_VertexStream == null)
        Renderer.m_VertexStream = new BufferedVertexStream(Engine.m_VertexBuffer, 32768, TransformedColoredTextured.StrideSize);
      for (int index2 = 0; index2 < textureList.Count; ++index2)
      {
        Texture tex = textureList[index2];
        TextureVB vb = tex.GetVB(type, alphaTest, filter);
        if (vb.m_Count > 0 && vb.m_Frame == Renderer._renderCount)
        {
          int val1 = vb.m_Count;
          int vertexOffset = 0;
          while (val1 > 0)
          {
            int num2 = Math.Min(val1, Renderer.m_VertexStream.Length / objectFormat.VertexCount);
            int num3 = Renderer.m_VertexStream.Push(vb._buffer, vertexOffset, num2 * objectFormat.VertexCount, true);
            if (num3 >= 0)
            {
              Renderer.EnsureTexture(tex);
              IndexBuffer indexBuffer = objectFormat.IndexBuffer;
              if (indexBuffer != null)
              {
                if (Renderer._currentIndexBuffer != indexBuffer)
                {
                  Renderer._currentIndexBuffer = indexBuffer;
                  device.set_Indices(indexBuffer);
                }
                device.DrawIndexedPrimitive(objectFormat.Type, num3, 0, num2 * objectFormat.VertexCount, 0, num2 * objectFormat.PrimitiveCount);
              }
              else
                device.DrawPrimitives(objectFormat.Type, num3, num2 * objectFormat.PrimitiveCount);
              if (Renderer._profile != null)
                ++Renderer._profile._draws;
              vb.m_Frame = -1;
              vertexOffset += num2 * objectFormat.VertexCount;
              val1 -= num2;
            }
            else
              break;
          }
        }
      }
      textureList.Clear();
    }

    private sealed class ScreenshotContext
    {
      private readonly string title;

      public ScreenshotContext(string title)
      {
        if (title == null)
          throw new ArgumentNullException("title");
        this.title = title;
      }

      public void Save()
      {
        Bitmap bitmap = Renderer.ScreenshotContext.Capture();
        try
        {
          ThreadPool.QueueUserWorkItem(new WaitCallback(this.SaveCallback), (object) bitmap);
        }
        catch
        {
          bitmap.Dispose();
          throw;
        }
      }

      private void SaveCallback(object state)
      {
        string tempFileName;
        using (Bitmap bitmap = (Bitmap) state)
        {
          tempFileName = Path.GetTempFileName();
          bitmap.Save(tempFileName, ImageFormat.Png);
        }
        DirectoryInfo targetDirectory = new DirectoryInfo(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Ultima Online"), Renderer.ScreenshotContext.Normalize(World.Player.Name)));
        if (!targetDirectory.Exists)
          targetDirectory.Create();
        string normalizedFileName = Renderer.ScreenshotContext.Normalize(this.title);
        int attempt = 1;
        while (attempt < 1000 && !Renderer.ScreenshotContext.TrySave(targetDirectory, tempFileName, normalizedFileName, attempt))
          ++attempt;
      }

      private static Bitmap Capture()
      {
        Rectangle screen = Engine.m_Display.RectangleToScreen(new Rectangle(Engine.GameX, Engine.GameY, Engine.GameWidth, Engine.GameHeight));
        Bitmap bitmap = new Bitmap(screen.Width, screen.Height);
        try
        {
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            graphics.CopyFromScreen(screen.Location, System.Drawing.Point.Empty, screen.Size, CopyPixelOperation.SourceCopy);
          return bitmap;
        }
        catch
        {
          bitmap.Dispose();
          throw;
        }
      }

      private static string Normalize(string fileName)
      {
        return Regex.Replace(fileName, string.Format("[{0}]", (object) Regex.Escape(new string(Path.GetInvalidFileNameChars()))), string.Empty);
      }

      private static bool TrySave(DirectoryInfo targetDirectory, string temporaryPath, string normalizedFileName, int attempt)
      {
        string targetPath = Path.Combine(targetDirectory.FullName, string.Format("{0}-{1}.png", (object) normalizedFileName, (object) attempt));
        return Renderer.ScreenshotContext.TryCopy(temporaryPath, targetPath);
      }

      private static bool TryCopy(string sourcePath, string targetPath)
      {
        if (!File.Exists(targetPath))
        {
          try
          {
            File.Copy(sourcePath, targetPath, false);
            return true;
          }
          catch (PathTooLongException ex)
          {
            throw;
          }
          catch (DirectoryNotFoundException ex)
          {
            throw;
          }
          catch (FileNotFoundException ex)
          {
            throw;
          }
          catch (IOException ex)
          {
          }
        }
        return false;
      }
    }

    private class DrawQueueEntry
    {
      public Texture m_Texture;
      public int m_TileX;
      public int m_TileY;
      public int m_DrawX;
      public int m_DrawY;
      public bool m_Flip;
      public float m_fAlpha;
      public bool m_bAlpha;

      public DrawQueueEntry(Texture tex, int tx, int ty, int dx, int dy)
      {
        this.m_Texture = tex;
        this.m_TileX = tx;
        this.m_TileY = ty;
        this.m_DrawX = dx;
        this.m_DrawY = dy;
        this.m_Flip = this.m_Texture.Flip;
        this.m_fAlpha = Renderer._alphaValue;
        this.m_bAlpha = Renderer._alphaEnable;
      }
    }

    private class ObjectFormat
    {
      public PrimitiveType Type;
      public int PrimitiveCount;
      public int VertexCount;
      public IndexBuffer IndexBuffer;

      public ObjectFormat(PrimitiveType type, int primitiveCount, int vertexCount, IndexBuffer indexBuffer)
      {
        this.Type = type;
        this.PrimitiveCount = primitiveCount;
        this.VertexCount = vertexCount;
        this.IndexBuffer = indexBuffer;
      }
    }

    private class AlphaState
    {
      public int _type;
      public DrawBlendType m_BlendType;
      public Texture m_Texture;
      public TextureVB m_TextureVB;
      public bool m_Filter;

      public AlphaState()
      {
        this.m_TextureVB = new TextureVB();
      }
    }
  }
}
