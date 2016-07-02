// Decompiled with JetBrains decompiler
// Type: PlayUO.GAbilityIcon
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;

namespace PlayUO
{
  public class GAbilityIcon : GDragable
  {
    private static ArrayList m_Instances = new ArrayList();
    private bool m_InBook;
    private bool m_Primary;

    public GAbilityIcon(bool inBook, bool primary, int gumpID, int x, int y)
      : base(gumpID, x, y)
    {
      this.m_InBook = inBook;
      this.m_Primary = primary;
      GAbilityIcon.m_Instances.Add((object) this);
      this.m_QuickDrag = false;
      this.m_CanClose = !inBook;
    }

    public static void Update()
    {
      for (int index = 0; index < GAbilityIcon.m_Instances.Count; ++index)
      {
        GAbilityIcon gabilityIcon = (GAbilityIcon) GAbilityIcon.m_Instances[index];
        AbilityInfo abilityInfo = gabilityIcon.m_Primary ? AbilityInfo.Primary : AbilityInfo.Secondary;
        gabilityIcon.GumpID = abilityInfo.Icon;
        gabilityIcon.Hue = abilityInfo == AbilityInfo.Active ? Hues.Load(32806) : Hues.Default;
        gabilityIcon.Tooltip = (ITooltip) new Tooltip(Localization.GetString(abilityInfo.Name), true);
        gabilityIcon.Tooltip.Delay = 0.25f;
      }
    }

    protected internal override void OnDispose()
    {
      GAbilityIcon.m_Instances.Remove((object) this);
      base.OnDispose();
    }

    protected internal override void OnDragStart()
    {
      if (this.m_InBook)
      {
        this.m_IsDragging = false;
        Gumps.Drag = (Gump) null;
        GAbilityIcon gabilityIcon = new GAbilityIcon(false, this.m_Primary, this.GumpID, Engine.m_xMouse, Engine.m_yMouse);
        gabilityIcon.Hue = this.Hue;
        gabilityIcon.m_OffsetX = gabilityIcon.Width / 2;
        gabilityIcon.m_OffsetY = gabilityIcon.Height / 2;
        gabilityIcon.X = Engine.m_xMouse - gabilityIcon.m_OffsetX;
        gabilityIcon.Y = Engine.m_yMouse - gabilityIcon.m_OffsetY;
        gabilityIcon.m_IsDragging = true;
        Gumps.Desktop.Children.Add((Gump) gabilityIcon);
        Gumps.Drag = (Gump) gabilityIcon;
      }
      else
        base.OnDragStart();
    }

    protected internal override void OnDoubleClick(int X, int Y)
    {
      AbilityInfo abilityInfo = this.m_Primary ? AbilityInfo.Primary : AbilityInfo.Secondary;
      if (AbilityInfo.Active == abilityInfo)
        AbilityInfo.Active = (AbilityInfo) null;
      else
        AbilityInfo.Active = abilityInfo;
    }
  }
}
