// Decompiled with JetBrains decompiler
// Type: PlayUO.GRenderSettingEditorForm
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GRenderSettingEditorForm : GWindowsForm
  {
    private static GRenderSettingEditorForm m_Instance;

    public static bool IsOpen
    {
      get
      {
        return GRenderSettingEditorForm.m_Instance != null;
      }
    }

    public GRenderSettingEditorForm()
      : base(0, 0, 478, 336)
    {
      this.Text = "Display Settings";
      this.Center();
      this.Client.Children.Add((Gump) new RenderSettingsVisualizer(0, 0));
    }

    public static void Open()
    {
      if (GRenderSettingEditorForm.m_Instance != null)
        return;
      GRenderSettingEditorForm.m_Instance = new GRenderSettingEditorForm();
      Gumps.Desktop.Children.Add((Gump) GRenderSettingEditorForm.m_Instance);
      Gumps.Focus = (Gump) GRenderSettingEditorForm.m_Instance;
    }

    protected internal override void Draw(int X, int Y)
    {
      base.Draw(X, Y);
    }

    protected internal override void OnDispose()
    {
      GRenderSettingEditorForm.m_Instance = (GRenderSettingEditorForm) null;
      PlayUO.Profiles.Config.Current.Save();
      base.OnDispose();
    }
  }
}
