// Decompiled with JetBrains decompiler
// Type: PlayUO.Profiles.SpeechHues
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using Sallos;

namespace PlayUO.Profiles
{
  public class SpeechHues : PersistableObject
  {
    public static readonly PersistableType TypeCode = new PersistableType("speechHues", new ConstructCallback((object) null, __methodptr(Construct)));
    public const int Default = 96;
    private int[] m_Hues;

    public virtual PersistableType TypeID
    {
      get
      {
        return SpeechHues.TypeCode;
      }
    }

    [OptionHue]
    [Optionable("Regular", "Speech Hues", Default = 96)]
    public int Regular
    {
      get
      {
        return this[SpeechType.Regular];
      }
      set
      {
        this[SpeechType.Regular] = value;
      }
    }

    [Optionable("Yell", "Speech Hues", Default = 96)]
    [OptionHue]
    public int Yell
    {
      get
      {
        return this[SpeechType.Yell];
      }
      set
      {
        this[SpeechType.Yell] = value;
      }
    }

    [OptionHue]
    [Optionable("Emote", "Speech Hues", Default = 96)]
    public int Emote
    {
      get
      {
        return this[SpeechType.Emote];
      }
      set
      {
        this[SpeechType.Emote] = value;
      }
    }

    [Optionable("Whisper", "Speech Hues", Default = 96)]
    [OptionHue]
    public int Whisper
    {
      get
      {
        return this[SpeechType.Whisper];
      }
      set
      {
        this[SpeechType.Whisper] = value;
      }
    }

    public int this[SpeechType speechType]
    {
      get
      {
        return this.m_Hues[(int) speechType];
      }
      set
      {
        this.m_Hues[(int) speechType] = value;
      }
    }

    public SpeechHues()
    {
      base.\u002Ector();
      this.m_Hues = new int[6];
      for (int index = 0; index < this.m_Hues.Length; ++index)
        this.m_Hues[index] = 96;
    }

    private static PersistableObject Construct()
    {
      return (PersistableObject) new SpeechHues();
    }

    protected virtual void SerializeAttributes(PersistanceWriter op)
    {
      op.SetInt32("regular", this.Regular);
      op.SetInt32("yell", this.Yell);
      op.SetInt32("emote", this.Emote);
      op.SetInt32("whisper", this.Whisper);
    }

    protected virtual void DeserializeAttributes(PersistanceReader ip)
    {
      this.Regular = ip.GetInt32("regular");
      this.Yell = ip.GetInt32("yell");
      this.Emote = ip.GetInt32("emote");
      this.Whisper = ip.GetInt32("whisper");
    }
  }
}
