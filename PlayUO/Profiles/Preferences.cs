// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.Preferences
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class Preferences : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("preferences", new ConstructCallback((object) null, __methodptr(Construct)), new PersistableType[10]{ FootstepData.TypeCode, SoundData.TypeCode, MusicData.TypeCode, SpeechHues.TypeCode, NotorietyHues.TypeCode, ScreenLayout.TypeCode, Options.TypeCode, ScavengerAgent.TypeCode, RenderSettings.TypeCode, RestockAgent.TypeCode });
    private FootstepData m_Footsteps;
    private SoundData m_Sound;
    private MusicData m_Music;
    private SpeechHues m_SpeechHues;
    private NotorietyHues m_NotorietyHues;
    private Options m_Options;
    private ScavengerAgent m_Scavenger;
    private ScreenLayout m_Layout;
    private RenderSettings _renderSettings;

    public virtual PersistableType TypeID
    {
      get
      {
        return Preferences.TypeCode;
      }
    }

    public static Preferences Current
    {
      get
      {
        return Profile.Current.Preferences;
      }
    }

    [Optionable("Footsteps")]
    public FootstepData Footsteps
    {
      get
      {
        return this.m_Footsteps;
      }
    }

    [Optionable("Sound")]
    public SoundData Sound
    {
      get
      {
        return this.m_Sound;
      }
    }

    [Optionable("Music")]
    public MusicData Music
    {
      get
      {
        return this.m_Music;
      }
    }

    [Optionable("Speech Hues")]
    public SpeechHues SpeechHues
    {
      get
      {
        return this.m_SpeechHues;
      }
    }

    [Optionable("Notoriety Hues")]
    public NotorietyHues NotorietyHues
    {
      get
      {
        return this.m_NotorietyHues;
      }
    }

    [Optionable("Options")]
    public Options Options
    {
      get
      {
        return this.m_Options;
      }
    }

    [Optionable("Scavenger")]
    public ScavengerAgent Scavenger
    {
      get
      {
        return this.m_Scavenger;
      }
    }

    [Optionable("Macros", "Configuration")]
    [MacroEditor]
    public string Macros
    {
      get
      {
        return "...";
      }
      set
      {
      }
    }

    [Optionable("Display Settings", "Configuration")]
    [RenderSettingEditor]
    public string DisplaySettings
    {
      get
      {
        return "...";
      }
      set
      {
      }
    }

    public RenderSettings RenderSettings
    {
      get
      {
        return this._renderSettings;
      }
    }

    public ScreenLayout Layout
    {
      get
      {
        return this.m_Layout;
      }
    }

    public Preferences()
      : this(false)
    {
    }

    private Preferences(bool isLoading)
    {
      base.\u002Ector();
      if (isLoading)
        return;
      this.m_Footsteps = new FootstepData();
      this.m_Sound = new SoundData();
      this.m_Music = new MusicData();
      this.m_SpeechHues = new SpeechHues();
      this.m_NotorietyHues = new NotorietyHues();
      this.m_Options = new Options();
      this.m_Scavenger = new ScavengerAgent();
      this.m_Layout = new ScreenLayout();
      this._renderSettings = new RenderSettings();
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new Preferences(true);
    }

    protected virtual void SerializeChildren(PersistanceWriter op)
    {
      this._renderSettings.Serialize(op);
      if (this.m_Footsteps == null)
        this.m_Footsteps = new FootstepData();
      this.m_Footsteps.Serialize(op);
      this.m_Sound.Serialize(op);
      this.m_Music.Serialize(op);
      this.m_SpeechHues.Serialize(op);
      this.m_NotorietyHues.Serialize(op);
      this.m_Options.Serialize(op);
      this.m_Scavenger.Serialize(op);
      this.m_Layout.Serialize(op);
    }

    protected virtual void DeserializeChildren(PersistanceReader ip)
    {
      while (ip.get_HasChild())
      {
        object obj = (object) ip.GetChild();
        if (obj is FootstepData)
          this.m_Footsteps = obj as FootstepData;
        else if (obj is SoundData)
          this.m_Sound = obj as SoundData;
        else if (obj is MusicData)
          this.m_Music = obj as MusicData;
        else if (obj is SpeechHues)
          this.m_SpeechHues = obj as SpeechHues;
        else if (obj is NotorietyHues)
          this.m_NotorietyHues = obj as NotorietyHues;
        else if (obj is Options)
          this.m_Options = obj as Options;
        else if (obj is ScreenLayout)
          this.m_Layout = obj as ScreenLayout;
        else if (obj is ScavengerAgent)
          this.m_Scavenger = obj as ScavengerAgent;
        else if (obj is RenderSettings)
          this._renderSettings = obj as RenderSettings;
      }
      if (this.m_Scavenger == null)
        this.m_Scavenger = new ScavengerAgent();
      if (this._renderSettings != null)
        return;
      this._renderSettings = new RenderSettings();
    }
  }
}
