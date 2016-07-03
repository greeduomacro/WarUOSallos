// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.RenderSettings
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class RenderSettings : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("renderSettings", Construct);
    private int _terrainQuality;
    private int _smoothingMode;
    private bool _smoothCharacters;
    private bool _animatedCharacters;
    private bool _itemShadows;
    private bool _characterShadows;

    public override PersistableType TypeID
    {
      get
      {
        return RenderSettings.TypeCode;
      }
    }

    public int TerrainQuality
    {
      get
      {
        return this._terrainQuality;
      }
      set
      {
        this._terrainQuality = value;
      }
    }

    public int SmoothingMode
    {
      get
      {
        return this._smoothingMode;
      }
      set
      {
        this._smoothingMode = value;
      }
    }

    public bool SmoothCharacters
    {
      get
      {
        return this._smoothCharacters;
      }
      set
      {
        this._smoothCharacters = value;
      }
    }

    public bool AnimatedCharacters
    {
      get
      {
        return this._animatedCharacters;
      }
      set
      {
        this._animatedCharacters = value;
      }
    }

    public bool CharacterShadows
    {
      get
      {
        return this._characterShadows;
      }
      set
      {
        this._characterShadows = value;
      }
    }

    public bool ItemShadows
    {
      get
      {
        return this._itemShadows;
      }
      set
      {
        this._itemShadows = value;
      }
    }

    public RenderSettings()
      : this(false)
    {
    }

    private RenderSettings(bool isLoading)
    {
      if (isLoading)
        return;
      this._terrainQuality = 1;
      this._smoothingMode = 1;
      this._smoothCharacters = true;
      this._animatedCharacters = true;
      this._itemShadows = true;
      this._characterShadows = true;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new RenderSettings(true);
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("terrain-quality", this._terrainQuality);
      op.SetInt32("smoothing-mode", this._smoothingMode);
      op.SetBoolean("smooth-characters", this._smoothCharacters);
      op.SetBoolean("animated-characters", this._animatedCharacters);
      op.SetBoolean("item-shadows", this._itemShadows);
      op.SetBoolean("character-shadows", this._characterShadows);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this._terrainQuality = ip.GetInt32("terrain-quality");
      this._smoothingMode = ip.GetInt32("smoothing-mode");
      this._smoothCharacters = ip.GetBoolean("smooth-characters");
      this._animatedCharacters = ip.GetBoolean("animated-characters");
      this._itemShadows = ip.GetBoolean("item-shadows");
      this._characterShadows = ip.GetBoolean("character-shadows");
    }
  }
}
