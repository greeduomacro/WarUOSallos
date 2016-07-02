// Decompiled with JetBrains decompiler
// Type: PlayUO.GContextMenu
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  public class GContextMenu : Gump
  {
    private int m_Width;
    private int m_Height;
    private object m_Owner;
    private static GContextMenu m_Instance;

    public override int X
    {
      get
      {
        int num = !(this.m_Owner is Mobile) ? ((Item) this.m_Owner).MessageX - this.m_Width / 2 : ((Mobile) this.m_Owner).ScreenX - this.m_Width / 2;
        if (num < 0)
          num = 0;
        else if (num + this.m_Width > Engine.ScreenWidth)
          num = Engine.ScreenWidth - this.m_Width;
        return num;
      }
    }

    public override int Y
    {
      get
      {
        int num;
        if (this.m_Owner is Mobile)
        {
          Mobile mobile = (Mobile) this.m_Owner;
          int screenY = mobile.ScreenY;
          if (Options.Current.MiniHealth && mobile.OpenedStatus && (mobile.StatusBar == null && mobile.MaximumHitPoints > 0) && mobile.CurrentHitPoints > 0)
            screenY += 11;
          num = screenY + 8;
        }
        else
          num = ((Item) this.m_Owner).BottomY + 4;
        if (num < 0)
          num = 0;
        else if (num + this.m_Height > Engine.ScreenHeight)
          num = Engine.ScreenHeight - this.m_Height;
        return num;
      }
    }

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
    }

    public override int Height
    {
      get
      {
        return this.m_Height;
      }
    }

    private GContextMenu(object owner, PopupEntry[] list)
      : base(100, 100)
    {
      this.m_Owner = owner;
      this.m_GUID = "MobilePopup";
      int num1 = 0;
      int num2 = 0;
      int length = list.Length;
      IFont font = (IFont) Engine.GetUniFont(3);
      IHue bright = Hues.Bright;
      IHue focusHue = Hues.Load(53);
      IHue @default = Hues.Default;
      OnClick onClick = new OnClick(this.Entry_OnClick);
      for (int index = 0; index < length; ++index)
      {
        PopupEntry popupEntry = list[index];
        GLabel glabel;
        if (popupEntry.Flags == 1)
        {
          glabel = new GLabel(popupEntry.Text, font, @default, 7, 7 + num2);
        }
        else
        {
          glabel = (GLabel) new GTextButton(popupEntry.Text, font, bright, focusHue, 7, 7 + num2, onClick);
          glabel.SetTag("EntryID", (object) popupEntry.EntryID);
        }
        glabel.X -= glabel.Image.xMin;
        glabel.Y -= glabel.Image.yMin;
        num2 += glabel.Image.yMax - glabel.Image.yMin + 4;
        if (glabel.Image.xMax - glabel.Image.xMin + 1 > num1)
          num1 = glabel.Image.xMax - glabel.Image.xMin + 1;
        this.m_Children.Add((Gump) glabel);
      }
      int num3 = num2 - 3;
      this.m_Width = num1 + 14;
      this.m_Height = num3 + 14;
    }

    public static void Close()
    {
      Gumps.Destroy((Gump) GContextMenu.m_Instance);
    }

    public static void Display(object owner, PopupEntry[] list)
    {
      Gumps.Destroy((Gump) GContextMenu.m_Instance);
      GContextMenu.m_Instance = new GContextMenu(owner, list);
      Gumps.Desktop.Children.Add((Gump) GContextMenu.m_Instance);
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return false;
    }

    protected internal override void Draw(int X, int Y)
    {
      int num1 = this.m_Width - 24;
      int num2 = this.m_Height - 24;
      Engine.m_Edge[0].Draw(X, Y, 0);
      Engine.m_Edge[1].Draw(X + 12, Y, num1, 12, 0);
      Engine.m_Edge[2].Draw(X + 12 + num1, Y, 0);
      Engine.m_Edge[3].Draw(X, Y + 12, 12, num2, 0);
      Engine.m_Edge[4].Draw(X + 12 + num1, Y + 12, 12, num2, 0);
      Engine.m_Edge[5].Draw(X, Y + 12 + num2, 0);
      Engine.m_Edge[6].Draw(X + 12, Y + 12 + num2, num1, 12, 0);
      Engine.m_Edge[7].Draw(X + 12 + num1, Y + 12 + num2, 0);
      Renderer.SetTexture((Texture) null);
      Renderer.PushAlpha(0.4f);
      Renderer.SolidRect(0, X + 12, Y + 12, num1, num2);
      Renderer.PopAlpha();
    }

    private void Entry_OnClick(Gump Sender)
    {
      Network.Send((Packet) new PPopupResponse(this.m_Owner, (int) Sender.GetTag("EntryID")));
      Gumps.Destroy((Gump) this);
    }
  }
}
