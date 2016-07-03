// Decompiled with JetBrains decompiler
// Type: PlayUO.Cursor
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;
using System;

namespace PlayUO
{
  public class Cursor
  {
    private static CursorEntry[,] m_Cursors = new CursorEntry[16, 3];
    private static bool m_Visible = true;
    private static bool m_Gold;
    private static bool m_Hourglass;

    public static bool Hourglass
    {
      get
      {
        return Cursor.m_Hourglass;
      }
      set
      {
        Cursor.m_Hourglass = value;
      }
    }

    public static bool Gold
    {
      get
      {
        return Cursor.m_Gold;
      }
      set
      {
        Cursor.m_Gold = value;
      }
    }

    public static bool Visible
    {
      get
      {
        return Cursor.m_Visible;
      }
      set
      {
        Cursor.m_Visible = value;
      }
    }

    public static int Height
    {
      get
      {
        CursorEntry cursor = Cursor.GetCursor();
        if (cursor == null)
          return 0;
        return cursor.m_Image.Height;
      }
    }

    public static int Width
    {
      get
      {
        CursorEntry cursor = Cursor.GetCursor();
        if (cursor == null)
          return 0;
        return cursor.m_Image.Width;
      }
    }

    public static void MoveTo(Gump who)
    {
      Point screen = who.PointToScreen(new Point(who.Width / 2, who.Height / 2));
      System.Windows.Forms.Cursor.Position = Engine.m_Display.PointToScreen((System.Drawing.Point) screen);
      Gumps.Invalidate();
    }

    public static CursorEntry GetCursor()
    {
      if (!Cursor.m_Visible)
        return (CursorEntry) null;
      int idCursor = 7;
      int idType = 0;
      if (TargetManager.IsActive)
        idCursor = 12;
      else if (Cursor.m_Hourglass)
        idCursor = 13;
      else if (Gumps.Drag != null && Gumps.Drag.m_DragCursor)
        idCursor = 8;
      else if (Gumps.LastOver != null && Gumps.LastOver.m_OverridesCursor)
        idCursor = Gumps.LastOver.m_OverCursor;
      else if (GObjectProperties.Instance != null)
        idCursor = 9;
      else if (Engine.m_Ingame)
        idCursor = (int) Engine.pointingDir;
      if (Engine.m_Ingame)
      {
        Mobile player = World.Player;
        if (player != null)
        {
          if (player.Flags[MobileFlag.Warmode])
            idType = 1;
          else if (Cursor.m_Gold)
            idType = 2;
        }
        else if (Cursor.m_Gold)
          idType = 2;
      }
      return Cursor.m_Cursors[idCursor, idType] ?? (Cursor.m_Cursors[idCursor, idType] = Cursor.LoadCursor(idCursor, idType));
    }

    private static unsafe CursorEntry LoadCursor(int idCursor, int idType)
    {
      IHue hue;
      int itemId;
      switch (idType)
      {
        case 0:
          hue = Hues.Default;
          itemId = 8298 + idCursor;
          break;
        case 1:
          hue = Hues.Default;
          itemId = 8275 + idCursor;
          break;
        case 2:
          hue = Hues.Load(35181);
          itemId = 8298 + idCursor;
          break;
        default:
          return (CursorEntry) null;
      }
      Texture texture = hue.GetItem(itemId);
      if (texture == null || texture.IsEmpty())
        return new CursorEntry(0, 0, 0, 0, Texture.Empty);
      if (texture.m_Factory != null)
      {
        texture.m_Factory.Remove(texture);
        texture.m_Factory = (TextureFactory) null;
        texture.m_FactoryArgs = (object[]) null;
      }
      int xOffset = 0;
      int yOffset = 0;
      if (idType < 2)
      {
        LockData lockData = texture.Lock(LockFlags.ReadWrite);
        int num1 = lockData.Width;
        int num2 = lockData.Height;
        int num3 = lockData.Pitch >> 1;
        short* numPtr1 = (short*) lockData.pvSrc;
        short* numPtr2 = (short*) ((IntPtr) lockData.pvSrc + ((num2 - 1) * num3) * 2);
        for (int index = 0; index < num1; ++index)
        {
          if (((int) *numPtr1 & (int) short.MaxValue) == 992)
            xOffset = index;
          *numPtr1++ = (short) 0;
          *numPtr2++ = (short) 0;
        }
        short* numPtr3 = (short*) lockData.pvSrc;
        short* numPtr4 = (short*) ((IntPtr) lockData.pvSrc +  (num1 - 1) * 2);
        for (int index = 0; index < num2; ++index)
        {
          if (((int) *numPtr3 & (int) short.MaxValue) == 992)
            yOffset = index;
          *numPtr3 = (short) 0;
          *numPtr4 = (short) 0;
          numPtr3 += num3;
          numPtr4 += num3;
        }
        texture.Unlock();
      }
      else
      {
        CursorEntry cursorEntry = Cursor.m_Cursors[idCursor, 1] ?? Cursor.m_Cursors[idCursor, 0] ?? (Cursor.m_Cursors[idCursor, 1] = Cursor.LoadCursor(idCursor, 1));
        xOffset = cursorEntry.m_xOffset;
        yOffset = cursorEntry.m_yOffset;
        LockData lockData = texture.Lock(LockFlags.ReadWrite);
        int num1 = lockData.Width;
        int num2 = lockData.Height;
        int num3 = lockData.Pitch >> 1;
        short* numPtr1 = (short*) lockData.pvSrc;
        short* numPtr2 = (short*) ((IntPtr) lockData.pvSrc + ((num2 - 1) * num3) * 2);
        for (int index = 0; index < num1; ++index)
        {
          *numPtr1++ = (short) 0;
          *numPtr2++ = (short) 0;
        }
        short* numPtr3 = (short*) lockData.pvSrc;
        short* numPtr4 = (short*) ((IntPtr) lockData.pvSrc +  (num1 - 1) * 2);
        for (int index = 0; index < num2; ++index)
        {
          *numPtr3 = (short) 0;
          *numPtr4 = (short) 0;
          numPtr3 += num3;
          numPtr4 += num3;
        }
        texture.Unlock();
      }
      return new CursorEntry(idCursor, idType, xOffset, yOffset, texture);
    }

    public static void Draw()
    {
      CursorEntry cursor = Cursor.GetCursor();
      if (cursor == null)
        return;
      cursor.Draw(Engine.m_xMouse, Engine.m_yMouse);
    }

    public static void Dispose()
    {
      for (int index1 = 0; index1 < 16; ++index1)
      {
        for (int index2 = 0; index2 < 3; ++index2)
        {
          if (Cursor.m_Cursors[index1, index2] != null)
          {
            Cursor.m_Cursors[index1, index2].m_Image.Dispose();
            Cursor.m_Cursors[index1, index2].m_Image = (Texture) null;
          }
        }
      }
      Cursor.m_Cursors = (CursorEntry[,]) null;
    }
  }
}
