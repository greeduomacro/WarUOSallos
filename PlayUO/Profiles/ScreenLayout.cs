// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.ScreenLayout
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO.Profiles
{
  public class ScreenLayout : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("screenLayout", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[2]{ SpellIconLayout.TypeCode, SkillIconLayout.TypeCode });
    private Rectangle m_GameBounds;
    private Rectangle m_ScreenBounds;
    private bool m_Maximized;
    private Size m_FullSize;
    private bool m_Fullscreen;
    private GumpLayoutCollection m_Gumps;
    private bool m_Applying;

    public virtual PersistableType TypeID
    {
      get
      {
        return ScreenLayout.TypeCode;
      }
    }

    public Rectangle ScreenBounds
    {
      get
      {
        return this.m_ScreenBounds;
      }
      set
      {
        this.m_ScreenBounds = value;
      }
    }

    public Rectangle GameBounds
    {
      get
      {
        return this.m_GameBounds;
      }
      set
      {
        this.m_GameBounds = value;
      }
    }

    public bool Maximized
    {
      get
      {
        return this.m_Maximized;
      }
      set
      {
        this.m_Maximized = value;
      }
    }

    public Size FullSize
    {
      get
      {
        return this.m_FullSize;
      }
      set
      {
        this.m_FullSize = value;
      }
    }

    public bool Fullscreen
    {
      get
      {
        return this.m_Fullscreen;
      }
      set
      {
        this.m_Fullscreen = value;
      }
    }

    public GumpLayoutCollection Gumps
    {
      get
      {
        return this.m_Gumps;
      }
      set
      {
        this.m_Gumps = value;
      }
    }

    public ScreenLayout()
      : this(false)
    {
    }

    private ScreenLayout(bool isLoading)
    {
      base.\u002Ector();
      this.m_Gumps = new GumpLayoutCollection();
      if (isLoading)
        return;
      Rectangle workingArea = Screen.FromControl((Control) Engine.m_Display).WorkingArea;
      Size size1 = new Size(800, 600);
      Size size2 = new Size(1024, 768);
      this.m_GameBounds = new Rectangle((size2.Width - size1.Width) / 2, (size2.Height - size1.Height) / 2, size1.Width, size1.Height);
      this.m_ScreenBounds = new Rectangle(workingArea.X + (workingArea.Width - size2.Width) / 2, workingArea.Y + (workingArea.Height - size2.Height) / 2, size2.Width, size2.Height);
      this.m_Maximized = false;
      this.m_FullSize = size2;
      this.m_Fullscreen = false;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new ScreenLayout(true);
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetRectangle("game", this.m_GameBounds);
      op.SetRectangle("screen", this.m_ScreenBounds);
      op.SetBoolean("maximized", this.m_Maximized);
      op.SetSize("full", this.m_FullSize);
      op.SetBoolean("fullscreen", this.m_Fullscreen);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.m_GameBounds = ip.GetRectangle("game");
      this.m_ScreenBounds = ip.GetRectangle("screen");
      this.m_Maximized = ip.GetBoolean("maximized");
      this.m_FullSize = ip.GetSize("full");
      this.m_Fullscreen = ip.GetBoolean("fullscreen");
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      for (int index = 0; index < this.m_Gumps.Count; ++index)
        this.m_Gumps[index].Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
        this.m_Gumps.Add(ip.GetChild() as GumpLayout);
    }

    public void Update()
    {
      if (this.m_Applying)
        return;
      Display display = Engine.m_Display;
      this.m_GameBounds = new Rectangle(Engine.GameX, Engine.GameY, Engine.GameWidth, Engine.GameHeight);
      if (!this.m_Fullscreen)
        this.m_Maximized = display.WindowState == FormWindowState.Maximized;
      if (!this.m_Maximized && !this.m_Fullscreen && display.WindowState == FormWindowState.Normal)
        this.m_ScreenBounds = new Rectangle(display.PointToScreen((System.Drawing.Point) new PlayUO.Point(0, 0)), display.ClientSize);
      Engine.ScreenWidth = display.ClientSize.Width;
      Engine.ScreenHeight = display.ClientSize.Height;
    }

    public void Apply(bool applyGumps)
    {
      if (this.m_Applying)
        return;
      this.m_Applying = true;
      try
      {
        Display display = Engine.m_Display;
        Engine.GameX = this.m_GameBounds.X;
        Engine.GameY = this.m_GameBounds.Y;
        Engine.GameWidth = this.m_GameBounds.Width;
        Engine.GameHeight = this.m_GameBounds.Height;
        if (this.m_Fullscreen)
        {
          display.FormBorderStyle = FormBorderStyle.None;
          display.WindowState = FormWindowState.Maximized;
        }
        else if (this.m_Maximized)
        {
          display.FormBorderStyle = FormBorderStyle.Sizable;
          display.WindowState = FormWindowState.Maximized;
        }
        else
        {
          Size frameBorderSize = SystemInformation.FrameBorderSize;
          int captionHeight = SystemInformation.CaptionHeight;
          display.FormBorderStyle = FormBorderStyle.Sizable;
          display.WindowState = FormWindowState.Normal;
          display.Location = (System.Drawing.Point) new PlayUO.Point(this.m_ScreenBounds.X - frameBorderSize.Width, this.m_ScreenBounds.Y - frameBorderSize.Height - captionHeight);
          display.ClientSize = this.m_ScreenBounds.Size;
        }
        int num1 = this.m_GameBounds.Width * this.m_GameBounds.Height;
        int num2 = num1 < 1920000 ? (num1 < 1310720 ? (num1 < 480000 ? (num1 < 307200 ? 3 : 5) : 7) : 9) : 11;
        Renderer.blockWidth = num2;
        Renderer.blockHeight = num2;
        Renderer.cellWidth = num2 << 3;
        Renderer.cellHeight = num2 << 3;
        if (!applyGumps)
          return;
        foreach (GumpLayout mGump in this.m_Gumps)
        {
          Gump gump = mGump.CreateGump();
          mGump.Setup(gump);
          gump.SetLayout(mGump);
          PlayUO.Gumps.Desktop.Children.Add(gump);
        }
      }
      finally
      {
        this.m_Applying = false;
      }
    }
  }
}
