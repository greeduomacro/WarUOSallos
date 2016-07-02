// Decompiled with JetBrains decompiler
// Type: PlayUO.GWindowsForm
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Windows.Forms;

namespace PlayUO
{
  public class GWindowsForm : Gump
  {
    private int m_Width;
    private int m_Height;
    private GLabel m_CaptionLabel;
    private Gump m_Client;
    private GWindowsButton m_CloseButton;

    public override int Width
    {
      get
      {
        return this.m_Width;
      }
      set
      {
        this.m_Width = value;
        this.Resize();
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
        this.Resize();
      }
    }

    public string Text
    {
      get
      {
        return this.m_CaptionLabel.Text;
      }
      set
      {
        this.m_CaptionLabel.Text = value;
      }
    }

    public GWindowsButton CloseButton
    {
      get
      {
        return this.m_CloseButton;
      }
      set
      {
        this.m_CloseButton = value;
      }
    }

    public Gump Client
    {
      get
      {
        return this.m_Client;
      }
    }

    public GWindowsForm()
      : this(0, 0, 200, 200)
    {
    }

    public GWindowsForm(int width, int height)
      : this(0, 0, width, height)
    {
    }

    public GWindowsForm(int x, int y, int width, int height)
      : base(x, y)
    {
      this.m_CanDrag = true;
      this.m_QuickDrag = true;
      this.m_Width = width;
      this.m_Height = height;
      this.m_CaptionLabel = new GLabel("Form", (IFont) Engine.GetUniFont(1), Hues.Default, 7, 3);
      this.m_Children.Add((Gump) this.m_CaptionLabel);
      this.m_Client = (Gump) new GEmpty(0, 0, 0, 0);
      this.m_Children.Add(this.m_Client);
      this.m_CloseButton = new GWindowsButton("", 0, 0, 16, 14);
      this.m_CloseButton.ImageColor = 0;
      this.m_CloseButton.Image = Engine.m_FormX;
      this.m_CloseButton.Clicked += new EventHandler(this.CloseButton_Clicked);
      this.m_Children.Add((Gump) this.m_CloseButton);
      this.ResizeClient();
    }

    public virtual void Resize()
    {
      this.ResizeClient();
    }

    public virtual void ResizeClient()
    {
      this.m_Client.X = 4;
      this.m_Client.Y = 23;
      this.m_Client.Width = this.Width - 8;
      this.m_Client.Height = this.Height - 27;
      this.m_CloseButton.X = this.Width - 6 - this.m_CloseButton.Width;
      this.m_CloseButton.Y = 6;
    }

    protected internal override bool HitTest(int X, int Y)
    {
      return true;
    }

    protected internal override void OnMouseDown(int X, int Y, MouseButtons mb)
    {
      this.BringToTop();
    }

    protected internal override void OnDragStart()
    {
      if (this.m_OffsetY > 21)
      {
        this.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
      }
      this.BringToTop();
    }

    protected internal override void Draw(int X, int Y)
    {
      Renderer.SetTexture((Texture) null);
      GumpPaint.DrawRaised3D(X, Y, this.m_Width, this.m_Height);
      Gump focus = Gumps.Focus;
      if (focus == this || focus != null && focus.IsChildOf((Gump) this))
      {
        Renderer.GradientRectLR(GumpColors.ActiveCaption, GumpColors.ActiveCaptionGradient, X + 4, Y + 4, this.Width - 8, 18);
        this.m_CaptionLabel.Hue = GumpHues.ActiveCaptionText;
      }
      else
      {
        Renderer.GradientRectLR(GumpColors.InactiveCaption, GumpColors.InactiveCaptionGradient, X + 4, Y + 4, this.Width - 8, 18);
        this.m_CaptionLabel.Hue = GumpHues.InactiveCaptionText;
      }
    }

    public virtual void Close()
    {
      Gumps.Destroy((Gump) this);
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
