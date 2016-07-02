// Decompiled with JetBrains decompiler
// Type: PlayUO.GumpImage
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GumpImage
  {
    private bool m_Draw;
    private Texture m_Image;
    private int m_Width;
    private int m_Height;
    private int m_GumpID;
    private bool m_Tile;
    private int m_X;
    private int m_Y;
    private VertexCache m_vCache;

    public bool CanDraw
    {
      get
      {
        return this.m_Draw;
      }
    }

    public Texture Image
    {
      get
      {
        return this.m_Image;
      }
      set
      {
        this.m_Image = value;
        if (this.m_vCache == null)
          return;
        this.m_vCache.Invalidate();
      }
    }

    public int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        if (!this.m_Tile)
        {
          if (this.m_Width == value)
            return;
          this.m_Width = value;
          this.m_Tile = true;
        }
        else
          this.m_Width = value;
      }
    }

    public int Height
    {
      get
      {
        return this.m_Height;
      }
      set
      {
        if (!this.m_Tile)
        {
          if (this.m_Height == value)
            return;
          this.m_Height = value;
          this.m_Tile = true;
        }
        else
          this.m_Height = value;
      }
    }

    public int GumpID
    {
      get
      {
        return this.m_GumpID;
      }
    }

    public int X
    {
      get
      {
        return this.m_X;
      }
      set
      {
        this.m_X = value;
      }
    }

    public int Y
    {
      get
      {
        return this.m_Y;
      }
      set
      {
        this.m_Y = value;
      }
    }

    public GumpImage(int GumpID)
    {
      this.m_GumpID = GumpID;
      this.m_Image = Hues.Default.GetGump(GumpID);
      if (this.m_Image == null || this.m_Image.IsEmpty())
        return;
      this.m_Width = this.m_Image.Width;
      this.m_Height = this.m_Image.Height;
      this.m_Draw = true;
      this.m_Tile = false;
    }

    public GumpImage(int GumpID, int X, int Y, int Width, int Height)
    {
      this.m_GumpID = GumpID;
      this.m_X = X;
      this.m_Y = Y;
      this.m_Width = Width;
      this.m_Height = Height;
      this.m_Image = Hues.Default.GetGump(GumpID);
      if (this.m_Image == null || this.m_Image.IsEmpty())
        return;
      this.m_Draw = true;
      this.m_Tile = true;
    }

    public void Draw(int x, int y)
    {
      if (!this.m_Draw)
        return;
      x += this.m_X;
      y += this.m_Y;
      if (this.m_Tile)
      {
        this.m_Image.Draw(x, y, this.m_Width, this.m_Height, 16777215);
      }
      else
      {
        if (this.m_vCache == null)
          this.m_vCache = new VertexCache();
        this.m_vCache.Draw(this.m_Image, x, y);
      }
    }
  }
}
