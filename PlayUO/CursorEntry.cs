// Decompiled with JetBrains decompiler
// Type: PlayUO.CursorEntry
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Targeting;

namespace PlayUO
{
  internal class CursorEntry
  {
    private static TransformedColoredTextured[] m_vTargetPool = VertexConstructor.Create();
    public int m_Graphic;
    public int m_Type;
    public int m_xOffset;
    public int m_yOffset;
    public Texture m_Image;
    private bool m_Draw;
    private VertexCache m_vCache;

    public CursorEntry(int graphic, int type, int xOffset, int yOffset, Texture image)
    {
      this.m_Graphic = graphic;
      this.m_Type = type;
      this.m_xOffset = xOffset;
      this.m_yOffset = yOffset;
      this.m_Image = image;
      this.m_Draw = this.m_Image != null && !this.m_Image.IsEmpty();
      this.m_vCache = new VertexCache();
    }

    public void Draw(int xMouse, int yMouse)
    {
      if (!this.m_Draw)
        return;
      this.m_vCache.Draw(this.m_Image, xMouse - this.m_xOffset, yMouse - this.m_yOffset);
      if (this.m_Graphic != 12)
        return;
      int color = 0;
      BaseTargetHandler active = TargetManager.Active;
      ServerTargetHandler serverTargetHandler = active as ServerTargetHandler;
      if (active != null)
      {
        if (active.IsOffensive)
        {
          color = 13369344;
          if (serverTargetHandler != null && serverTargetHandler.Action == TargetAction.Poison)
            color = 65280;
        }
        else if (active.IsDefensive)
        {
          color = 2285055;
          if (serverTargetHandler != null && serverTargetHandler.Action == TargetAction.Cure)
            color = 65450;
        }
      }
      if (color > 0)
      {
        Engine.ImageCache.TargetCursorHighlight.Draw(xMouse - this.m_xOffset - 3, yMouse - this.m_yOffset - 11, color);
        if (!(active is ClientTargetHandler))
          return;
        Engine.ImageCache.TargetCursorLocal.Draw(xMouse - this.m_xOffset, yMouse - this.m_yOffset, 11184810);
      }
      else
      {
        if (!(active is ClientTargetHandler))
          return;
        Engine.ImageCache.TargetCursorLocal.Draw(xMouse - this.m_xOffset, yMouse - this.m_yOffset, 11184810);
      }
    }
  }
}
