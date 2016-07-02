// Decompiled with JetBrains decompiler
// Type: PlayUO.RectangleList
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Drawing;

namespace PlayUO
{
  public class RectangleList
  {
    private Rectangle[] m_Rects;
    private int m_Count;

    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }

    public Rectangle this[int index]
    {
      get
      {
        if (index < 0 || index >= this.m_Count)
          throw new IndexOutOfRangeException();
        return this.m_Rects[index];
      }
    }

    public RectangleList()
    {
      this.m_Rects = new Rectangle[8];
    }

    private static Rectangle[] Punch(Rectangle cookie, Rectangle cutter)
    {
      if (!cookie.IntersectsWith(cutter))
        return new Rectangle[1]{ cookie };
      int width1 = cutter.X - cookie.X;
      int height1 = cutter.Y - cookie.Y;
      int width2 = cookie.X + cookie.Width - (cutter.X + cutter.Width);
      int height2 = cookie.Y + cookie.Height - (cutter.Y + cutter.Height);
      int length = 0;
      if (width1 > 0)
        ++length;
      else
        width1 = 0;
      if (height1 > 0)
        ++length;
      else
        height1 = 0;
      if (width2 > 0)
        ++length;
      else
        width2 = 0;
      if (height2 > 0)
        ++length;
      else
        height2 = 0;
      Rectangle[] rectangleArray1 = new Rectangle[length];
      int num1 = 0;
      if (width1 > 0)
        rectangleArray1[num1++] = new Rectangle(cookie.X, cookie.Y, width1, cookie.Height);
      if (height1 > 0)
        rectangleArray1[num1++] = new Rectangle(cookie.X + width1, cookie.Y, cookie.Width - width1 - width2, height1);
      if (width2 > 0)
        rectangleArray1[num1++] = new Rectangle(cutter.X + cutter.Width, cookie.Y, width2, cookie.Height);
      if (height2 > 0)
      {
        Rectangle[] rectangleArray2 = rectangleArray1;
        int index = num1;
        int num2 = 1;
        int num3 = index + num2;
        rectangleArray2[index] = new Rectangle(cookie.X + width1, cutter.Y + cutter.Height, cookie.Width - width1 - width2, height2);
      }
      return rectangleArray1;
    }

    public void Add(Rectangle rect)
    {
      for (int index = 0; index < this.m_Count; ++index)
      {
        Rectangle rectangle = this.m_Rects[index];
        if (rect.IntersectsWith(rectangle))
        {
          foreach (Rectangle rect1 in RectangleList.Punch(rect, rectangle))
            this.Add(rect1);
          return;
        }
      }
      this.InternalAdd(rect);
    }

    public void Remove(Rectangle rect)
    {
      for (int index = this.m_Count - 1; index >= 0; --index)
      {
        Rectangle rectangle = this.m_Rects[index];
        if (rect.IntersectsWith(rectangle))
        {
          this.InternalRemove(index);
          foreach (Rectangle rect1 in RectangleList.Punch(rectangle, rect))
            this.InternalAdd(rect1);
        }
      }
    }

    public void Clear()
    {
      this.m_Count = 0;
    }

    private void InternalAdd(Rectangle rect)
    {
      if (this.m_Count >= this.m_Rects.Length)
      {
        Rectangle[] rectangleArray = this.m_Rects;
        this.m_Rects = new Rectangle[rectangleArray.Length * 2];
        for (int index = 0; index < rectangleArray.Length; ++index)
          this.m_Rects[index] = rectangleArray[index];
      }
      this.m_Rects[this.m_Count++] = rect;
    }

    private void InternalRemove(int index)
    {
      --this.m_Count;
      for (int index1 = index; index1 < this.m_Count; ++index1)
        this.m_Rects[index1] = this.m_Rects[index1 + 1];
    }
  }
}
