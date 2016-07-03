// Decompiled with JetBrains decompiler
// Type: PlayUO.Gumps
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Ultima.Data;

namespace PlayUO
{
  public sealed class Gumps
  {
    private const short Opaque = -32768;
    private static Gump m_Desktop;
    private static Gump m_Drag;
    private static Gump m_Capture;
    private static Gump m_Focus;
    private static Gump m_Modal;
    private static Gump m_LastDragOver;
    private static Gump m_StartDrag;
    private static Point m_StartDragPoint;
    private static Gump m_LastOver;
    private static GTextBox m_TextFocus;
    private static TimeDelay m_TipDelay;
    private Hashtable m_Objects;
    private static Hashtable m_ToRestore;
    private static bool m_Invalidated;
    private Gumps.GumpFactory m_Factory;

    public static bool Invalidated
    {
      get
      {
        return Gumps.m_Invalidated;
      }
      set
      {
        Gumps.m_Invalidated = value;
      }
    }

    public static Gump Modal
    {
      get
      {
        return Gumps.m_Modal;
      }
      set
      {
        Gumps.m_Modal = value;
        if (Gumps.m_Modal == null || Gumps.m_TextFocus == null || Gumps.m_TextFocus.IsChildOf(Gumps.m_Modal))
          return;
        Gumps.m_TextFocus.Unfocus();
      }
    }

    public static Gump Focus
    {
      get
      {
        return Gumps.m_Focus;
      }
      set
      {
        if (Gumps.m_Focus != value)
          Gumps.RecurseFocusChanged(Gumps.m_Desktop, value);
        Gumps.m_Focus = value;
      }
    }

    public string Name
    {
      get
      {
        return "Gumps";
      }
    }

    public static Gump Capture
    {
      get
      {
        return Gumps.m_Capture;
      }
      set
      {
        Gumps.m_Capture = value;
      }
    }

    public static Gump Drag
    {
      get
      {
        return Gumps.m_Drag;
      }
      set
      {
        if (value == null && Gumps.m_Drag != null)
          Gumps.m_Drag.m_IsDragging = false;
        Gumps.m_Drag = value;
      }
    }

    public static Gump StartDrag
    {
      get
      {
        return Gumps.m_StartDrag;
      }
      set
      {
        Gumps.m_StartDrag = value;
      }
    }

    public static GTextBox TextFocus
    {
      get
      {
        return Gumps.m_TextFocus;
      }
      set
      {
        Gumps.m_TextFocus = value;
      }
    }

    public static Gump LastDragOver
    {
      get
      {
        return Gumps.m_LastDragOver;
      }
    }

    public static Gump LastOver
    {
      get
      {
        return Gumps.m_LastOver;
      }
      set
      {
        if (Gumps.m_LastOver == value)
          return;
        if (Gumps.m_LastOver != null)
          Gumps.m_LastOver.OnMouseLeave();
        Gumps.m_LastOver = value;
      }
    }

    public static Gump Desktop
    {
      get
      {
        return Gumps.m_Desktop;
      }
    }

    public Gumps()
    {
      Gumps.m_Desktop = new Gump(0, 0);
      Gumps.m_Desktop.GUID = "Desktop";
      Gumps.m_ToRestore = new Hashtable();
    }

    public static void Invalidate()
    {
      Gumps.m_Invalidated = true;
    }

    public static void Destroy(Gump g)
    {
      if (g == null)
        return;
      Gumps.m_Invalidated = true;
      g.Children.Clear();
      if (g == Gumps.m_Drag)
        Gumps.m_Drag = (Gump) null;
      if (g == Gumps.m_Capture)
        Gumps.m_Capture = (Gump) null;
      if (g == Gumps.m_Focus)
        Gumps.m_Focus = (Gump) null;
      if (g == Gumps.m_Modal)
        Gumps.m_Modal = (Gump) null;
      if (g == Gumps.m_LastDragOver)
        Gumps.m_LastDragOver = (Gump) null;
      if (g == Gumps.m_StartDrag)
        Gumps.m_StartDrag = (Gump) null;
      if (g == Gumps.m_LastOver)
        Gumps.m_LastOver = (Gump) null;
      if (g == Gumps.m_TextFocus)
        Gumps.m_TextFocus = (GTextBox) null;
      if (g.m_Restore && g.GUID != null && g.GUID.Length > 0)
        Gumps.m_ToRestore[(object) g.GUID] = (object) new Point(g.X, g.Y);
      if (g.HasTag("Dispose"))
      {
        switch ((string) g.GetTag("Dispose"))
        {
          case "Spellbook":
            Item obj = (Item) g.GetTag("Container");
            if (obj != null)
            {
              obj.OpenSB = false;
              break;
            }
            break;
          case "Modal":
            Gumps.m_Modal = (Gump) null;
            break;
        }
      }
      g.m_Disposed = true;
      g.OnDispose();
      if (g.Parent == null)
        return;
      g.Parent.Children.Remove(g);
    }

    public static void Restore(Gump ToRestore)
    {
      if (Gumps.m_ToRestore == null || ToRestore.GUID == null || (ToRestore.GUID.Length <= 0 || !Gumps.m_ToRestore.Contains((object) ToRestore.GUID)))
        return;
      Point point = (Point) Gumps.m_ToRestore[(object) ToRestore.GUID];
      ToRestore.X = point.X;
      ToRestore.Y = point.Y;
    }

    public static void TextBoxTab(Gump Start)
    {
      GumpList children;
      int num1;
      if (Start.Parent is GWindowsTextBox)
      {
        children = Start.Parent.Parent.Children;
        num1 = children.IndexOf(Start.Parent);
      }
      else
      {
        children = Start.Parent.Children;
        num1 = children.IndexOf(Start);
      }
      Gump[] array = children.ToArray();
      int length = array.Length;
      if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
      {
        int num2 = num1 + 1;
        for (int index = 0; index < length; ++index)
        {
          Gump gump = array[(index + num2) % length];
          GTextBox gtextBox = !(gump is GWindowsTextBox) ? gump as GTextBox : ((GWindowsTextBox) gump).TextBox;
          if (gtextBox != null)
          {
            if (Gumps.m_TextFocus != null)
              Gumps.m_TextFocus.Unfocus();
            gtextBox.Focus();
            break;
          }
        }
      }
      else
      {
        int num2 = num1 - 1;
        for (int index = 0; index < length; ++index)
        {
          Gump gump = array[(length + num2 - index) % length];
          GTextBox gtextBox = !(gump is GWindowsTextBox) ? gump as GTextBox : ((GWindowsTextBox) gump).TextBox;
          if (gtextBox != null)
          {
            if (Gumps.m_TextFocus != null)
              Gumps.m_TextFocus.Unfocus();
            gtextBox.Focus();
            break;
          }
        }
      }
    }

    public static Gump FindGumpByGUID(string GUID)
    {
      Stack stack = new Stack();
      stack.Push((object) Gumps.m_Desktop);
      while (stack.Count > 0)
      {
        Gump gump = (Gump) stack.Pop();
        if (gump.GUID == GUID)
          return gump;
        foreach (object obj in gump.Children.ToArray())
          stack.Push(obj);
      }
      return (Gump) null;
    }

    public static bool KeyDown(char c)
    {
      if (Gumps.m_Modal != null)
      {
        if (Gumps.m_TextFocus != null && Gumps.m_TextFocus.IsChildOf(Gumps.m_Modal) && Gumps.m_TextFocus.OnKeyDown(c))
          return true;
        if (Gumps.m_Focus != null && Gumps.m_Focus.IsChildOf(Gumps.m_Modal))
        {
          if (!Gumps.RecurseKeyDown(Gumps.m_Focus, c))
            Gumps.RecurseKeyDown(Gumps.m_Focus.Parent, c);
        }
        else
          Gumps.RecurseKeyDown(Gumps.m_Modal, c);
        return true;
      }
      if (Gumps.m_TextFocus != null && Gumps.m_TextFocus.OnKeyDown(c))
        return true;
      for (Gump g = Gumps.m_Focus; g != null; g = g.Parent)
      {
        if (Gumps.RecurseKeyDown(g, c))
          return true;
      }
      return false;
    }

    private static bool RecurseKeyDown(Gump g, char c)
    {
      if (!g.Visible)
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        if (Gumps.RecurseKeyDown(array[index], c))
          return true;
      }
      if (g.GetType() != typeof (GTextBox))
        return g.OnKeyDown(c);
      return false;
    }

    public static bool MouseUp(int X, int Y, MouseButtons mb)
    {
      Gumps.m_StartDrag = (Gump) null;
      if (Gumps.m_Capture != null)
      {
        Point client = Gumps.m_Capture.PointToClient(new Point(X, Y));
        Gumps.m_Capture.OnMouseUp(client.X, client.Y, mb);
        return true;
      }
      if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0)
        return false;
      if (Gumps.m_Drag != null && (mb & MouseButtons.Left) == MouseButtons.Left)
      {
        bool flag = !Gumps.IsWorldAt(X, Y, false);
        if (Gumps.m_Drag.m_IsDragging && Gumps.m_LastDragOver != null)
        {
          Gumps.m_LastDragOver.OnDragDrop(Gumps.m_Drag);
          Engine.CancelClick();
        }
        if (Gumps.m_Drag != null)
          Gumps.m_Drag.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        Gumps.m_LastDragOver = (Gump) null;
        return flag;
      }
      if (Gumps.m_Drag != null)
        return !Gumps.IsWorldAt(X, Y, false);
      if (!Gumps.RecurseMouseUp(0, 0, Gumps.m_Desktop, X, Y, mb))
        return Gumps.m_Modal != null;
      return true;
    }

    private static bool RecurseMouseUp(int X, int Y, Gump g, int mX, int mY, MouseButtons mb)
    {
      if (!g.Visible || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseMouseUp(X + g1.X, Y + g1.Y, g1, mX, mY, mb))
          return true;
      }
      if (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height))
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          g.OnMouseUp(mX - X, mY - Y, mb);
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          g.OnMouseUp(mX - X, mY - Y, mb);
          return true;
        }
      }
      return false;
    }

    public static bool MouseDown(int X, int Y, MouseButtons mb)
    {
      if (Gumps.m_Capture != null)
      {
        Point client = Gumps.m_Capture.PointToClient(new Point(X, Y));
        Gumps.m_Capture.OnMouseDown(client.X, client.Y, mb);
        Gumps.Focus = Gumps.m_Capture;
        return true;
      }
      if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0)
        return false;
      if (Gumps.RecurseMouseDown(0, 0, Gumps.m_Desktop, X, Y, mb))
        return true;
      if (Gumps.m_Modal != null)
      {
        Gumps.Focus = Gumps.m_Modal;
        return true;
      }
      if (Gumps.m_Drag != null)
      {
        Gumps.Focus = Gumps.m_Drag;
        return !Gumps.IsWorldAt(X, Y, false);
      }
      Gumps.Focus = (Gump) null;
      return false;
    }

    private static bool RecurseMouseDown(int X, int Y, Gump g, int mX, int mY, MouseButtons mb)
    {
      if (!g.Visible || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseMouseDown(X + g1.X, Y + g1.Y, g1, mX, mY, mb))
          return true;
      }
      if (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height))
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_TextFocus != null)
          {
            Gumps.m_TextFocus.Unfocus();
            Gumps.m_TextFocus = (GTextBox) null;
          }
          if (Gumps.m_Drag == null && g.m_CanDrag && mb == MouseButtons.Left)
          {
            Gumps.m_StartDrag = g;
            Gumps.m_StartDragPoint = new Point(mX, mY);
            g.m_OffsetX = mX - X;
            g.m_OffsetY = mY - Y;
            if (g.m_QuickDrag)
            {
              g.m_IsDragging = true;
              Gumps.m_Drag = g;
              g.OnDragStart();
            }
          }
          g.OnMouseDown(mX - X, mY - Y, mb);
          Gumps.Focus = g;
          if (g == Gumps.m_Drag)
            return !Gumps.IsWorldAt(mX, mY, false);
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_TextFocus != null)
          {
            Gumps.m_TextFocus.Unfocus();
            Gumps.m_TextFocus = (GTextBox) null;
          }
          if (Gumps.m_Drag == null && g.m_CanDrag && mb == MouseButtons.Left)
          {
            Gumps.m_StartDrag = g;
            Gumps.m_StartDragPoint = new Point(mX, mY);
            g.m_OffsetX = mX - X;
            g.m_OffsetY = mY - Y;
            if (g.m_QuickDrag)
            {
              g.m_IsDragging = true;
              g.OnDragStart();
              Gumps.m_Drag = g;
            }
          }
          g.OnMouseDown(mX - X, mY - Y, mb);
          Gumps.Focus = g;
          if (g == Gumps.m_Drag)
            return !Gumps.IsWorldAt(mX, mY, false);
          return true;
        }
      }
      return false;
    }

    public static object[] FindListForSingleClick(int x, int y)
    {
      if (Gumps.m_Capture != null)
      {
        Point client = Gumps.m_Capture.PointToClient(new Point(x, y));
        return new object[2]{ (object) Gumps.m_Capture, (object) client };
      }
      if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0)
        return (object[]) null;
      return Gumps.RecurseFindListForSingleClick(0, 0, Gumps.m_Desktop, x, y);
    }

    private static object[] RecurseFindListForSingleClick(int x, int y, Gump g, int mx, int my)
    {
      if (!g.Visible)
        return (object[]) null;
      if (g.m_NonRestrictivePicking || mx >= x && mx < x + g.Width && (my >= y && my < y + g.Height))
      {
        Gump[] array = g.Children.ToArray();
        for (int index = array.Length - 1; index >= 0; --index)
        {
          Gump g1 = array[index];
          object[] listForSingleClick = Gumps.RecurseFindListForSingleClick(x + g1.X, y + g1.Y, g1, mx, my);
          if (listForSingleClick != null)
            return listForSingleClick;
        }
        if (!g.m_NonRestrictivePicking || mx >= x && mx < x + g.Width && (my >= y && my < y + g.Height))
        {
          if (Gumps.m_Modal == null && g.HitTest(mx - x, my - y))
            return new object[2]{ (object) g, (object) new Point(mx - x, my - y) };
          if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mx - x, my - y))
            return new object[2]{ (object) g, (object) new Point(mx - x, my - y) };
        }
      }
      return (object[]) null;
    }

    public static bool DoubleClick(int X, int Y)
    {
      if (Gumps.m_Capture != null)
      {
        Point client = Gumps.m_Capture.PointToClient(new Point(X, Y));
        Gumps.m_Capture.OnDoubleClick(client.X, client.Y);
        return true;
      }
      if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0)
        return false;
      if (!Gumps.RecurseDoubleClick(0, 0, Gumps.m_Desktop, X, Y))
        return Gumps.m_Modal != null;
      return true;
    }

    private static void RecurseFocusChanged(Gump g, Gump focus)
    {
      g.OnFocusChanged(focus);
      foreach (Gump g1 in g.Children.ToArray())
        Gumps.RecurseFocusChanged(g1, focus);
    }

    private static bool RecurseDoubleClick(int X, int Y, Gump g, int mX, int mY)
    {
      if (!g.Visible || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseDoubleClick(X + g1.X, Y + g1.Y, g1, mX, mY))
          return true;
      }
      if (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height))
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_TextFocus != null)
          {
            Gumps.m_TextFocus.Unfocus();
            Gumps.m_TextFocus = (GTextBox) null;
          }
          g.OnDoubleClick(mX - X, mY - Y);
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_TextFocus != null)
          {
            Gumps.m_TextFocus.Unfocus();
            Gumps.m_TextFocus = (GTextBox) null;
          }
          g.OnDoubleClick(mX - X, mY - Y);
          return true;
        }
      }
      return false;
    }

    public static void MouseWheel(int X, int Y, int Delta)
    {
      if (Gumps.m_Capture != null)
      {
        Gumps.m_Capture.OnMouseWheel(Delta);
      }
      else
      {
        if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0 || (Gumps.RecurseMouseWheel(0, 0, Gumps.m_Desktop, X, Y, Delta) || Gumps.m_Focus == null))
          return;
        Gumps.m_Focus.OnMouseWheel(Delta);
      }
    }

    public static bool RecurseMouseWheel(int X, int Y, Gump g, int mX, int mY, int Delta)
    {
      if (!g.Visible || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseMouseWheel(X + g1.X, Y + g1.Y, g1, mX, mY, Delta))
          return true;
      }
      if (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height))
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          g.OnMouseWheel(Delta);
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          g.OnMouseWheel(Delta);
          return true;
        }
      }
      return false;
    }

    public static bool MouseMove(int X, int Y, MouseButtons mb)
    {
      if (Gumps.m_Capture != null)
      {
        Point client = Gumps.m_Capture.PointToClient(new Point(X, Y));
        if (Gumps.m_LastOver != Gumps.m_Capture)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_Capture.OnMouseEnter(client.X, client.Y, mb);
          Gumps.m_LastOver = Gumps.m_Capture;
        }
        Gumps.m_Capture.OnMouseMove(client.X, client.Y, mb);
        return true;
      }
      if (Gumps.m_Desktop == null || Gumps.m_Desktop.Children.Count == 0)
        return false;
      if (Gumps.m_Drag != null && Gumps.m_Drag.m_IsDragging)
      {
        int X1 = X - Gumps.m_Drag.m_OffsetX;
        int Y1 = Y - Gumps.m_Drag.m_OffsetY;
        if (X1 + Gumps.m_Drag.Width < Gumps.m_Drag.m_DragClipX)
          X1 = Gumps.m_Drag.m_DragClipX - Gumps.m_Drag.Width;
        else if (X1 > Engine.ScreenWidth - Gumps.m_Drag.m_DragClipX)
          X1 = Engine.ScreenWidth - Gumps.m_Drag.m_DragClipX;
        if (Y1 + Gumps.m_Drag.Height < Gumps.m_Drag.m_DragClipY)
          Y1 = Gumps.m_Drag.m_DragClipY - Gumps.m_Drag.Height;
        else if (Y1 > Engine.ScreenHeight - Gumps.m_Drag.m_DragClipY)
          Y1 = Engine.ScreenHeight - Gumps.m_Drag.m_DragClipY;
        Point client1 = Gumps.m_Drag.Parent.PointToClient(new Point(X1, Y1));
        Gumps.m_Drag.X = client1.X;
        Gumps.m_Drag.Y = client1.Y;
        Gumps.m_Drag.OnDragMove();
        Gump target = (Gump) null;
        Gumps.RecurseFindDrop(0, 0, Gumps.m_Desktop, X, Y, mb, ref target);
        if (target != null)
        {
          if (Gumps.m_LastDragOver != target)
          {
            if (Gumps.m_LastDragOver != null)
              Gumps.m_LastDragOver.OnDragLeave(Gumps.m_Drag);
            target.OnDragEnter(Gumps.m_Drag);
          }
        }
        else if (Gumps.m_LastDragOver != null)
          Gumps.m_LastDragOver.OnDragLeave(Gumps.m_Drag);
        Gumps.m_LastDragOver = target;
        if (Gumps.m_LastOver != Gumps.m_Drag)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = Gumps.m_Drag;
          if (Gumps.m_LastOver != null)
          {
            Point client2 = Gumps.m_LastOver.PointToClient(new Point(X, Y));
            Gumps.m_LastOver.OnMouseEnter(client2.X, client2.Y, mb);
          }
        }
        return !Gumps.IsWorldAt(X, Y, false);
      }
      Gump gump = Gumps.m_StartDrag;
      if (!Gumps.RecurseMouseMove(0, 0, Gumps.m_Desktop, X, Y, mb))
      {
        if (gump != null && gump.m_CanDrag && mb == MouseButtons.Left)
        {
          Gumps.m_Drag = gump;
          gump.m_IsDragging = true;
          gump.OnDragStart();
        }
        else if (Gumps.m_LastOver != null)
        {
          Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = (Gump) null;
        }
        return Gumps.m_Modal != null;
      }
      if (gump != Gumps.m_LastOver && gump != null && (gump.m_CanDrag && !gump.m_IsDragging) && (!gump.m_QuickDrag && mb == MouseButtons.Left))
      {
        Gumps.m_Drag = gump;
        if (Gumps.m_LastOver != Gumps.m_Drag)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = Gumps.m_Drag;
          if (Gumps.m_LastOver != null)
          {
            Point p = new Point(X, Y);
            Point client = Gumps.m_LastOver.PointToClient(p);
            Gumps.m_LastOver.OnMouseEnter(client.X, client.Y, mb);
          }
        }
        gump.m_IsDragging = true;
        gump.OnDragStart();
        if (Gumps.m_Drag != null && Gumps.m_Drag.m_IsDragging)
          Gumps.MouseMove(X, Y, mb);
        if (Gumps.m_LastOver != Gumps.m_Drag)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = Gumps.m_Drag;
          if (Gumps.m_LastOver != null)
          {
            Point p = new Point(X, Y);
            Point client = Gumps.m_LastOver.PointToClient(p);
            Gumps.m_LastOver.OnMouseEnter(client.X, client.Y, mb);
          }
        }
      }
      else if (gump == Gumps.m_LastOver && gump != null && (gump.m_CanDrag && !gump.m_IsDragging) && (!gump.m_QuickDrag && mb == MouseButtons.Left && (Gumps.m_StartDragPoint ^ new Point(X, Y)) >= 2))
      {
        Gumps.m_Drag = gump;
        if (Gumps.m_LastOver != Gumps.m_Drag)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = Gumps.m_Drag;
          if (Gumps.m_LastOver != null)
          {
            Point p = new Point(X, Y);
            Point client = Gumps.m_LastOver.PointToClient(p);
            Gumps.m_LastOver.OnMouseEnter(client.X, client.Y, mb);
          }
        }
        gump.m_IsDragging = true;
        gump.OnDragStart();
        if (Gumps.m_Drag != null && Gumps.m_Drag.m_IsDragging)
          Gumps.MouseMove(X, Y, mb);
        if (Gumps.m_LastOver != Gumps.m_Drag)
        {
          if (Gumps.m_LastOver != null)
            Gumps.m_LastOver.OnMouseLeave();
          Gumps.m_LastOver = Gumps.m_Drag;
          if (Gumps.m_LastOver != null)
          {
            Point p = new Point(X, Y);
            Point client = Gumps.m_LastOver.PointToClient(p);
            Gumps.m_LastOver.OnMouseEnter(client.X, client.Y, mb);
          }
        }
      }
      return true;
    }

    private static bool RecurseFindDrop(int X, int Y, Gump g, int mX, int mY, MouseButtons mb, ref Gump target)
    {
      if (!g.Visible || g == Gumps.m_Drag || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseFindDrop(X + g1.X, Y + g1.Y, g1, mX, mY, mb, ref target))
          return true;
      }
      if ((!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height)) && g.m_CanDrop)
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          target = g;
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          target = g;
          return true;
        }
      }
      return false;
    }

    public static bool IsWorldAt(int X, int Y)
    {
      if (Gumps.m_Modal != null)
        return true;
      return !Gumps.RecurseIsWorldAt(0, 0, Gumps.m_Desktop, X, Y, false);
    }

    public static bool IsWorldAt(int X, int Y, bool CheckDrag)
    {
      if (Gumps.m_Modal != null)
        return true;
      return !Gumps.RecurseIsWorldAt(0, 0, Gumps.m_Desktop, X, Y, CheckDrag);
    }

    private static bool RecurseIsWorldAt(int X, int Y, Gump g, int mX, int mY, bool CheckDrag)
    {
      if (!g.Visible || !CheckDrag && g == Gumps.m_Drag || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseIsWorldAt(X + g1.X, Y + g1.Y, g1, mX, mY, CheckDrag))
          return true;
      }
      return (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height)) && g.HitTest(mX - X, mY - Y);
    }

    private static bool RecurseMouseMove(int X, int Y, Gump g, int mX, int mY, MouseButtons mb)
    {
      if (!g.Visible || !g.m_NonRestrictivePicking && (mX < X || mX >= X + g.Width || (mY < Y || mY >= Y + g.Height)))
        return false;
      Gump[] array = g.Children.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        Gump g1 = array[index];
        if (Gumps.RecurseMouseMove(X + g1.X, Y + g1.Y, g1, mX, mY, mb))
          return true;
      }
      if (!g.m_NonRestrictivePicking || mX >= X && mX < X + g.Width && (mY >= Y && mY < Y + g.Height))
      {
        if (Gumps.m_Modal == null && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_LastOver == g)
          {
            g.OnMouseMove(mX - X, mY - Y, mb);
          }
          else
          {
            if (Gumps.m_LastOver != null)
              Gumps.m_LastOver.OnMouseLeave();
            g.OnMouseEnter(mX - X, mY - Y, mb);
            Gumps.m_TipDelay = g.Tooltip == null ? (TimeDelay) null : new TimeDelay(g.Tooltip.Delay);
            Gumps.m_LastOver = g;
          }
          return true;
        }
        if (Gumps.m_Modal != null && g.IsChildOf(Gumps.m_Modal) && g.HitTest(mX - X, mY - Y))
        {
          if (Gumps.m_LastOver == g)
          {
            g.OnMouseMove(mX - X, mY - Y, mb);
          }
          else
          {
            if (Gumps.m_LastOver != null)
              Gumps.m_LastOver.OnMouseLeave();
            g.OnMouseEnter(mX - X, mY - Y, mb);
            Gumps.m_TipDelay = g.Tooltip == null ? (TimeDelay) null : new TimeDelay(g.Tooltip.Delay);
            Gumps.m_LastOver = g;
          }
          return true;
        }
      }
      return false;
    }

    public static void MessageBoxOk(string Prompt, bool Modal, OnClick ClickHandler)
    {
      GBackground gbackground = new GBackground(2604, 356, 212, 142, 134, true);
      GButton gbutton = new GButton(1153, 164, 170, new OnClick(Gumps.MessageBoxOk_OnClick));
      gbutton.SetTag("Dialog", (object) gbackground);
      gbutton.SetTag("ClickHandler", (object) ClickHandler);
      GWrappedLabel gwrappedLabel = new GWrappedLabel(Prompt, (IFont) Engine.GetFont(1), Hues.Load(1899), gbackground.OffsetX, gbackground.OffsetY, Engine.ScreenWidth / 2 - gbackground.OffsetX * 2);
      gbackground.Width = gwrappedLabel.Width + gbackground.OffsetX * 2;
      gbackground.Height = gwrappedLabel.Height + 10 + gbackground.OffsetY * 2;
      if (gbackground.Width < 150)
        gbackground.Width = 150;
      gbackground.Center();
      gbutton.X = (gbackground.Width - gbutton.Width) / 2;
      gbutton.Y = gbackground.Height - gbackground.OffsetY;
      gbackground.Children.Add((Gump) gwrappedLabel);
      gbackground.Children.Add((Gump) gbutton);
      if (Modal)
        gbackground.Modal = true;
      gbackground.m_CanDrag = true;
      gbackground.m_QuickDrag = true;
      Gumps.m_Desktop.Children.Add((Gump) gbackground);
    }

    public static void MessageBoxOk_OnClick(Gump Sender)
    {
      Gump g = (Gump) Sender.GetTag("Dialog");
      OnClick onClick = (OnClick) Sender.GetTag("ClickHandler");
      if (onClick != null)
        onClick(Sender);
      if (g == null)
        return;
      Gumps.Destroy(g);
    }

    public static bool Check(ref int gumpID, ref int hue)
    {
      if (Engine.m_Gumps.m_Factory.Exists(gumpID))
        return true;
      GraphicTranslation graphicTranslation = GraphicTranslators.Gumps[gumpID];
      if (graphicTranslation == null)
        return false;
      gumpID = graphicTranslation.FallbackId;
      if (hue == 0)
        hue = graphicTranslation.FallbackData;
      return true;
    }

    public static int GetEquipGumpID(int itemID, int gender, ref int hue)
    {
      int num = (int) Map.GetAnimation(itemID);
      if (gender == 0)
      {
        int gumpID = num + 50000;
        if (Gumps.Check(ref gumpID, ref hue))
          return gumpID;
        gumpID += 10000;
        if (Gumps.Check(ref gumpID, ref hue))
          return gumpID;
      }
      else
      {
        int gumpID = num + 60000;
        if (Gumps.Check(ref gumpID, ref hue))
          return gumpID;
        gumpID -= 10000;
        if (Gumps.Check(ref gumpID, ref hue))
          return gumpID;
      }
      return 0;
    }

    public static void OpenPaperdoll(Mobile m, string Name, bool canDrag)
    {
      if (m == null)
        return;
      bool flag1 = m.Paperdoll != null;
      bool flag2 = flag1 && Gumps.m_LastOver == m.Paperdoll;
      bool flag3 = flag1 && Gumps.m_Drag == m.Paperdoll;
      int num1 = flag3 ? Gumps.m_Drag.m_OffsetX : 0;
      int num2 = flag3 ? Gumps.m_Drag.m_OffsetY : 0;
      int Index = flag1 ? m.Paperdoll.Parent.Children.IndexOf((Gump) m.Paperdoll) : -1;
      int X = int.MaxValue;
      int Y = 5;
      if (flag1)
      {
        X = m.Paperdoll.X;
        Y = m.Paperdoll.Y;
        Gumps.Destroy((Gump) m.Paperdoll);
      }
      else if (m.PaperdollX < int.MaxValue && m.PaperdollY < int.MaxValue)
      {
        X = m.PaperdollX;
        Y = m.PaperdollY;
        m.PaperdollX = int.MaxValue;
        m.PaperdollY = int.MaxValue;
      }
      OnClick[] onClickArray = new OnClick[8]{ new OnClick(Engine.Help_OnClick), new OnClick(Engine.Options_OnClick), new OnClick(Engine.LogOut_OnClick), new OnClick(Engine.Journal_OnClick), new OnClick(Engine.Skills_OnClick), null, new OnClick(Engine.AttackModeToggle_OnClick), new OnClick(Engine.Status_OnClick) };
      int[] numArray1 = new int[8]{ 44, 71, 98, 124, 151, 179, 205, 233 };
      int[] numArray2 = new int[8]{ 2031, 2006, 2009, 2012, 2015, 2018, World.Player.Flags[MobileFlag.Warmode] ? 2024 : 2021, 2027 };
      GPaperdoll gpaperdoll;
      if (m.Player)
      {
        gpaperdoll = new GPaperdoll(m, 2000, X, Y, canDrag);
        if (!flag1 && X >= int.MaxValue)
          gpaperdoll.X = Engine.ScreenWidth - gpaperdoll.Width - 5;
        GButton[] gbuttonArray = new GButton[7];
        for (int index = 0; index < 7; ++index)
        {
          gbuttonArray[index] = new GButton(numArray2[index], numArray2[index] + 2, numArray2[index] + 1, 185, numArray1[index], onClickArray[index]);
          gbuttonArray[index].Enabled = onClickArray[index] != null;
          gpaperdoll.Children.Add((Gump) gbuttonArray[index]);
        }
      }
      else
      {
        gpaperdoll = new GPaperdoll(m, 2001, X, Y, canDrag);
        if (!flag1 && X >= int.MaxValue)
          gpaperdoll.X = Engine.ScreenWidth - gpaperdoll.Width - 5;
      }
      gpaperdoll.Title = Name;
      GButton gbutton = new GButton(numArray2[7], numArray2[7] + 2, numArray2[7] + 1, 185, numArray1[7], onClickArray[7]);
      gbutton.SetTag("Serial", (object) m.Serial);
      gpaperdoll.Children.Add((Gump) gbutton);
      int num3 = (int) m.Hue;
      bool flag4 = false;
      foreach (Item added in m.Items)
      {
        if (!flag4 || added.Layer == Layer.OuterTorso)
          gpaperdoll.OnChildAdded(added);
      }
      gpaperdoll.SetTag("Dispose", (object) "Paperdoll");
      gpaperdoll.SetTag("Serial", (object) m.Serial);
      if (flag2)
        Gumps.m_LastOver = (Gump) gpaperdoll;
      if (flag3)
      {
        gpaperdoll.m_IsDragging = true;
        gpaperdoll.OffsetX = num1;
        gpaperdoll.OffsetY = num2;
        Gumps.m_Drag = (Gump) gpaperdoll;
      }
      if (gpaperdoll.X + gpaperdoll.Width - gpaperdoll.m_DragClipX < 0)
        gpaperdoll.X = gpaperdoll.m_DragClipX - gpaperdoll.Width;
      else if (gpaperdoll.X + gpaperdoll.m_DragClipX >= Engine.ScreenWidth)
        gpaperdoll.X = Engine.ScreenWidth - gpaperdoll.m_DragClipX;
      if (gpaperdoll.Y + gpaperdoll.Height - gpaperdoll.m_DragClipY < 0)
        gpaperdoll.Y = gpaperdoll.m_DragClipY - gpaperdoll.Height;
      else if (gpaperdoll.Y + gpaperdoll.m_DragClipY >= Engine.ScreenHeight)
        gpaperdoll.Y = Engine.ScreenHeight - gpaperdoll.m_DragClipY;
      if (Index != -1)
        Gumps.Desktop.Children.Insert(Index, (Gump) gpaperdoll);
      else
        Gumps.Desktop.Children.Add((Gump) gpaperdoll);
      m.SetContainerView((IContainerView) gpaperdoll);
    }

    public void DisplayObject(string Name)
    {
      Gumps.m_Desktop.Children.Add((Gump) this.m_Objects[(object) Name]);
    }

    public void Dispose()
    {
      Stack stack = new Stack();
      stack.Push((object) Gumps.m_Desktop);
      while (stack.Count > 0)
      {
        Gump gump = (Gump) stack.Pop();
        if (gump != null)
        {
          GumpList children = gump.Children;
          if (children != null)
          {
            foreach (object obj in children.ToArray())
              stack.Push(obj);
          }
          try
          {
            gump.OnDispose();
          }
          catch (Exception ex)
          {
            Debug.Trace("Exception in {0}.OnDispose()", (object) gump);
            Debug.Error(ex);
          }
        }
      }
      Gumps.m_Desktop = (Gump) null;
      Gumps.m_Drag = (Gump) null;
      Gumps.m_Capture = (Gump) null;
      Gumps.m_Focus = (Gump) null;
      Gumps.m_Modal = (Gump) null;
      Gumps.m_LastDragOver = (Gump) null;
      Gumps.m_StartDrag = (Gump) null;
      Gumps.m_LastOver = (Gump) null;
      Gumps.m_TextFocus = (GTextBox) null;
      Gumps.m_TipDelay = (TimeDelay) null;
      if (this.m_Objects != null)
      {
        this.m_Objects.Clear();
        this.m_Objects = (Hashtable) null;
      }
      if (Gumps.m_ToRestore == null)
        return;
      Gumps.m_ToRestore.Clear();
      Gumps.m_ToRestore = (Hashtable) null;
    }

    public static void Draw()
    {
      if (Gumps.m_Desktop == null)
        return;
      Gumps.m_Desktop.Render(0, 0);
      if (Gumps.m_LastOver == null || Gumps.m_LastOver.Tooltip == null || (Gumps.m_TipDelay == null || !Gumps.m_TipDelay.Elapsed) || !Cursor.Visible)
        return;
      Gump gump = Gumps.m_LastOver.Tooltip.GetGump();
      if (gump == null)
        return;
      bool flag1 = Engine.m_xMouse < Engine.ScreenWidth / 2;
      bool flag2 = Engine.m_yMouse < Engine.ScreenHeight / 2;
      int X = Engine.m_xMouse - gump.Width - 2;
      int Y = Engine.m_yMouse - gump.Height - 2;
      if (flag1)
        X = !flag2 ? Engine.m_xMouse : Engine.m_xMouse + Cursor.Width + 2;
      if (flag2)
        Y = !flag1 ? Engine.m_yMouse : Engine.m_yMouse + Cursor.Height + 2;
      if (X < 2)
        X = 2;
      else if (X + gump.Width + 2 > Engine.ScreenWidth)
        X = Engine.ScreenWidth - gump.Width - 2;
      if (Y < 2)
        Y = 2;
      else if (Y + gump.Height + 2 > Engine.ScreenHeight)
        Y = Engine.ScreenHeight - gump.Height - 2;
      gump.Render(X, Y);
    }

    [Obsolete("please don't ever use this", false)]
    public Size Measure(int gumpID)
    {
      Texture gump = Hues.Default.GetGump(gumpID);
      if (gump != null)
        return new Size(gump.Width, gump.Height);
      return Size.Empty;
    }

    public Texture ReadFromDisk(int GumpID, IHue Hue)
    {
      if (this.m_Factory == null)
        this.m_Factory = new Gumps.GumpFactory(this);
      return this.m_Factory.Load(GumpID, Hue);
    }

    private class GumpFactory : TextureFactory
    {
      private int m_GumpID;
      private IHue m_Hue;
      private Gumps m_Gumps;
      private readonly Archive archive;
      private byte[] data;

      public override TextureTransparency Transparency
      {
        get
        {
          return TextureTransparency.Simple;
        }
      }

      public GumpFactory(Gumps gumps)
      {
        this.m_Gumps = gumps;
        this.archive = new Archive(Engine.FileManager.ResolveMUL("gumpartLegacyMUL.uop"));
      }

      public bool Exists(int gumpId)
      {
        if (gumpId != 0)
          return this.archive.FileExists(Gumps.GumpFactory.GetFilePath(gumpId));
        return false;
      }

      public Texture Load(int gumpID, IHue hue)
      {
        if (gumpID == 2624)
          hue = Hues.Load(32769);
        this.m_GumpID = gumpID;
        this.m_Hue = hue;
        return this.Construct(false);
      }

      public override Texture Reconstruct(object[] args)
      {
        this.m_GumpID = (int) args[0];
        this.m_Hue = (IHue) args[1];
        return this.Construct(true);
      }

      protected override void CoreAssignArgs(Texture tex)
      {
        tex.m_Factory = (TextureFactory) this;
        tex.m_FactoryArgs = new object[2]
        {
          (object) this.m_GumpID,
          (object) this.m_Hue
        };
        tex._shaderData = this.m_Hue.ShaderData;
      }

      private static string GetFilePath(int gumpId)
      {
        return string.Format("build/gumpartlegacymul/{0:00000000}.tga", (object) gumpId);
      }

      protected override bool CoreLookup()
      {
        if (this.m_GumpID == 0)
          this.data = (byte[]) null;
        else
          this.archive.TryReadFile(Gumps.GumpFactory.GetFilePath(this.m_GumpID), out this.data);
        if (this.data != null)
          return this.data.Length > 8;
        return false;
      }

      protected override void CoreGetDimensions(out int width, out int height)
      {
        if (this.data == null)
          throw new InvalidOperationException();
        width = BitConverter.ToInt32(this.data, 0);
        height = BitConverter.ToInt32(this.data, 4);
      }

      protected override unsafe void CoreProcessImage(int width, int height, int stride, ushort* pLine, ushort* pLineEnd, ushort* pImageEnd, int lineDelta, int lineEndDelta)
      {
        byte[] numArray = this.data;
        fixed (byte* numPtr1 = numArray)
        {
          int* numPtr2 = (int*) (numPtr1 + 8);
          int* numPtr3 = numPtr2;
          int* numPtr4 = numPtr3 + height;
          while (pLine < pImageEnd)
          {
            int num = numPtr3 + 1 < numPtr4 ? numPtr3[1] : numArray.Length / 4 - 2;
            this.m_Hue.CopyEncodedLine((ushort*) (numPtr2 + *numPtr3), (ushort*) (numPtr2 + num), pLine, pLineEnd);
            pLine += lineEndDelta;
            pLineEnd += lineEndDelta;
            ++numPtr3;
          }
        }
      }
    }
  }
}
