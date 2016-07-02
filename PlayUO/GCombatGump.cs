// Decompiled with JetBrains decompiler
// Type: PlayUO.GCombatGump
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GCombatGump : GDragable
  {
    private static IHue m_ActiveHue = (IHue) new Hues.ColorFillHue(5903637);
    private static IHue m_AbleHue = (IHue) new Hues.ColorFillHue(1381722);
    private static IHue m_DefaultHue = (IHue) new Hues.ColorFillHue(5917233);
    private GAbilityIcon m_PrimaryIcon;
    private GAbilityIcon m_SecondaryIcon;
    private static GCombatGump m_Instance;

    public GCombatGump()
      : base(11010, 50, 50)
    {
      AbilityInfo[] abilities = AbilityInfo.Abilities;
      AbilityInfo active = AbilityInfo.Active;
      AbilityInfo primary = AbilityInfo.Primary;
      AbilityInfo secondary = AbilityInfo.Secondary;
      IFont font = (IFont) Engine.GetUniFont(1);
      OnClick onClick = new OnClick(this.Name_OnClick);
      this.m_Children.Add((Gump) new GLabel("INDEX", (IFont) Engine.GetFont(6), Hues.Default, 100, 4));
      this.m_Children.Add((Gump) new GLabel("INDEX", (IFont) Engine.GetFont(6), Hues.Default, 262, 4));
      for (int index = 0; index < abilities.Length; ++index)
      {
        AbilityInfo a = abilities[index];
        IHue hueFor = GCombatGump.GetHueFor(a);
        GLabel glabel = (GLabel) new GTextButton(Localization.GetString(a.Name), font, hueFor, hueFor, 56 + index / 9 * 162, 38 + index % 9 * 15, onClick);
        a.NameLabel = (GTextButton) glabel;
        glabel.SetTag("Ability", (object) a);
        glabel.Tooltip = (ITooltip) new Tooltip(Localization.GetString(a.Tooltip), true, 240);
        glabel.Tooltip.Delay = 0.25f;
        this.m_Children.Add((Gump) glabel);
      }
      this.m_PrimaryIcon = new GAbilityIcon(true, true, primary.Icon, 218, 105);
      this.m_PrimaryIcon.Tooltip = (ITooltip) new Tooltip(Localization.GetString(primary.Name), true);
      this.m_PrimaryIcon.Tooltip.Delay = 0.25f;
      this.m_PrimaryIcon.Hue = primary == AbilityInfo.Active ? Hues.Load(32806) : Hues.Default;
      this.m_Children.Add((Gump) this.m_PrimaryIcon);
      this.m_Children.Add((Gump) new GLabel("Primary", (IFont) Engine.GetFont(6), Hues.Default, 268, 105));
      this.m_Children.Add((Gump) new GLabel("Ability Icon", (IFont) Engine.GetFont(6), Hues.Default, 268, 119));
      this.m_SecondaryIcon = new GAbilityIcon(true, false, secondary.Icon, 218, 150);
      this.m_SecondaryIcon.Tooltip = (ITooltip) new Tooltip(Localization.GetString(secondary.Name), true);
      this.m_SecondaryIcon.Tooltip.Delay = 0.25f;
      this.m_SecondaryIcon.Hue = secondary == AbilityInfo.Active ? Hues.Load(32806) : Hues.Default;
      this.m_Children.Add((Gump) this.m_SecondaryIcon);
      this.m_Children.Add((Gump) new GLabel("Secondary", (IFont) Engine.GetFont(6), Hues.Default, 268, 150));
      this.m_Children.Add((Gump) new GLabel("Ability Icon", (IFont) Engine.GetFont(6), Hues.Default, 268, 164));
    }

    public static void Open()
    {
      if (GCombatGump.m_Instance != null)
        return;
      GCombatGump.m_Instance = new GCombatGump();
      Gumps.Desktop.Children.Add((Gump) GCombatGump.m_Instance);
    }

    protected internal override void OnDispose()
    {
      base.OnDispose();
      GCombatGump.m_Instance = (GCombatGump) null;
    }

    private void InternalUpdate()
    {
      foreach (Gump gump in this.m_Children.ToArray())
      {
        GTextButton gtextButton = gump as GTextButton;
        if (gtextButton != null)
        {
          AbilityInfo a = (AbilityInfo) gtextButton.GetTag("Ability");
          if (a != null)
            a.NameLabel.FocusHue = a.NameLabel.DefaultHue = GCombatGump.GetHueFor(a);
        }
      }
    }

    public static void Update()
    {
      if (GCombatGump.m_Instance != null)
        GCombatGump.m_Instance.InternalUpdate();
      GAbilityIcon.Update();
    }

    private void Name_OnClick(Gump sender)
    {
      AbilityInfo abilityInfo = (AbilityInfo) sender.GetTag("Ability");
      if (AbilityInfo.Active == abilityInfo)
        AbilityInfo.Active = (AbilityInfo) null;
      else
        AbilityInfo.Active = abilityInfo;
    }

    public static IHue GetHueFor(AbilityInfo a)
    {
      if (a == AbilityInfo.Active)
        return GCombatGump.m_ActiveHue;
      if (a == AbilityInfo.Primary || a == AbilityInfo.Secondary)
        return GCombatGump.m_AbleHue;
      return GCombatGump.m_DefaultHue;
    }
  }
}
