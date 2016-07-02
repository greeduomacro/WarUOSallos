// Decompiled with JetBrains decompiler
// Type: PlayUO.GRadioButton
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.Windows.Forms;

namespace PlayUO
{
  public class GRadioButton : GImage
  {
    protected bool m_State;
    protected int[] m_GumpIDs;
    protected Gump m_ParentOverride;

    public Gump ParentOverride
    {
      get
      {
        return this.m_ParentOverride;
      }
      set
      {
        this.m_ParentOverride = value;
      }
    }

    public bool State
    {
      get
      {
        return this.m_State;
      }
      set
      {
        if (this.m_State == value)
          return;
        this.m_State = value;
        this.GumpID = this.m_GumpIDs[value ? 1 : 0];
        if (!value || this.m_Parent == null && this.m_ParentOverride == null)
          return;
        Stack stack = new Stack();
        stack.Push(this.m_ParentOverride != null ? (object) this.m_ParentOverride : (object) this.m_Parent);
        while (stack.Count > 0)
        {
          foreach (Gump gump in ((Gump) stack.Pop()).Children.ToArray())
          {
            if (gump is GRadioButton && gump != this)
            {
              GRadioButton gradioButton = (GRadioButton) gump;
              if (gradioButton.State)
                gradioButton.State = false;
            }
            if (gump.Children.Count > 0)
              stack.Push((object) gump);
          }
        }
      }
    }

    public GRadioButton(int inactiveID, int activeID, bool initialState, int x, int y)
      : base(initialState ? activeID : inactiveID, x, y)
    {
      this.m_GumpIDs = new int[2]
      {
        inactiveID,
        activeID
      };
      this.m_State = initialState;
    }

    protected internal override void OnMouseUp(int x, int y, MouseButtons mb)
    {
      this.State = true;
    }

    protected internal override bool HitTest(int x, int y)
    {
      if (this.m_Invalidated)
        this.Refresh();
      if (this.m_Draw && (this.m_Clipper == null || this.m_Clipper.Evaluate(this.PointToScreen(new Point(x, y)))))
        return this.m_Image.HitTest(x, y);
      return false;
    }
  }
}
