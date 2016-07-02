// Decompiled with JetBrains decompiler
// Type: PlayUO.GMessageBoxYesNo
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GMessageBoxYesNo : GDragable
  {
    private MBYesNoCallback m_Callback;

    public GMessageBoxYesNo(string prompt, bool modal, MBYesNoCallback callback)
      : base(2070, 0, 0)
    {
      this.m_Callback = callback;
      this.Center();
      this.m_CanClose = false;
      this.m_Children.Add((Gump) new GLabel(prompt, (IFont) Engine.GetFont(1), Hues.Load(927), 33, 27));
      this.m_Children.Add((Gump) new GMessageBoxYesNo.GMBYNButton(this, 2071, 37, false));
      this.m_Children.Add((Gump) new GMessageBoxYesNo.GMBYNButton(this, 2074, 100, true));
      Gumps.Modal = (Gump) this;
    }

    public void Signal(bool response)
    {
      this.OnSignal(response);
      if (this.m_Callback == null)
        return;
      this.m_Callback((Gump) this, response);
    }

    protected virtual void OnSignal(bool response)
    {
    }

    private class GMBYNButton : GButtonNew
    {
      private GMessageBoxYesNo m_Owner;
      private bool m_Response;

      public GMBYNButton(GMessageBoxYesNo owner, int gumpID, int x, bool response)
        : base(gumpID, gumpID + 2, gumpID + 1, x, 75)
      {
        this.m_Owner = owner;
        this.m_Response = response;
        this.m_CanEnter = response;
      }

      protected override void OnClicked()
      {
        this.m_Owner.Signal(this.m_Response);
        Gumps.Destroy((Gump) this.m_Owner);
      }
    }
  }
}
