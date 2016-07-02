// Decompiled with JetBrains decompiler
// Type: PlayUO.StaticMessage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class StaticMessage : TextMessage
  {
    private int m_Serial;

    public override int X
    {
      get
      {
        return this.m_X - Renderer.m_xScroll;
      }
    }

    public override int Y
    {
      get
      {
        return this.m_Y - Renderer.m_yScroll;
      }
    }

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public StaticMessage(int xMouse, int yMouse, int Serial, string Message)
      : base(Message, Engine.ItemDuration, Engine.DefaultFont, Hues.Load(946))
    {
      this.m_Serial = Serial;
      this.m_X = xMouse - (this.m_Image.Width >> 1);
      this.m_Y = yMouse - this.m_Image.Height;
    }

    public void Offset(int X, int Y)
    {
      this.m_X -= X;
      this.m_Y -= Y;
    }
  }
}
