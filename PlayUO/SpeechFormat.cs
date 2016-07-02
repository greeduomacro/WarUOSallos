// Decompiled with JetBrains decompiler
// Type: PlayUO.SpeechFormat
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Profiles;

namespace PlayUO
{
  internal class SpeechFormat
  {
    public static readonly SpeechFormat Client = (SpeechFormat) new ClientFormat("Client: ", ". ", (string) null, (byte) 0, SpeechType.Regular);
    public static readonly SpeechFormat Yell = new SpeechFormat("Yell: ", "! ", (string) null, (byte) 9, SpeechType.Yell);
    public static readonly SpeechFormat Emote = new SpeechFormat("Emote: ", ": ", "*{0}*", (byte) 2, SpeechType.Emote);
    public static readonly SpeechFormat Whisper = new SpeechFormat("Whisper: ", "; ", (string) null, (byte) 8, SpeechType.Whisper);
    public static readonly SpeechFormat Guild = new SpeechFormat("Guild: ", "\\ ", (string) null, (byte) 13, SpeechType.Guild);
    public static readonly SpeechFormat Alliance = new SpeechFormat("Alliance: ", "| ", (string) null, (byte) 14, SpeechType.Alliance);
    public static readonly SpeechFormat Party = (SpeechFormat) new PartyFormat("Party: ", "/", (string) null, (byte) 0, SpeechType.Regular);
    public static readonly SpeechFormat Regular = new SpeechFormat((string) null, (string) null, (string) null, (byte) 0, SpeechType.Regular);
    public static readonly SpeechFormat[] Formats = new SpeechFormat[8]{ SpeechFormat.Client, SpeechFormat.Yell, SpeechFormat.Emote, SpeechFormat.Whisper, SpeechFormat.Guild, SpeechFormat.Alliance, SpeechFormat.Party, SpeechFormat.Regular };
    protected string m_Prepend;
    protected string m_Prefix;
    protected string m_Format;
    protected byte m_MessageType;
    protected SpeechType m_SpeechType;

    public virtual int Hue
    {
      get
      {
        return Preferences.Current.SpeechHues[this.m_SpeechType];
      }
    }

    public byte MessageType
    {
      get
      {
        return this.m_MessageType;
      }
    }

    public SpeechType SpeechType
    {
      get
      {
        return this.m_SpeechType;
      }
    }

    public SpeechFormat(string prepend, string prefix, string format, byte messageType, SpeechType speechType)
    {
      this.m_Prepend = prepend;
      this.m_Prefix = prefix;
      this.m_Format = format;
      this.m_MessageType = messageType;
      this.m_SpeechType = speechType;
    }

    public static SpeechFormat Find(string text)
    {
      for (int index = 0; index < SpeechFormat.Formats.Length; ++index)
      {
        if (SpeechFormat.Formats[index].IsMatch(text))
          return SpeechFormat.Formats[index];
      }
      return SpeechFormat.Formats[SpeechFormat.Formats.Length - 1];
    }

    public virtual void OnSpeech(string text)
    {
      this.Invoke(this.Mutate(text, false));
    }

    public virtual void Invoke(string text)
    {
      Network.Send((Packet) new PUnicodeSpeech(text, true, this));
    }

    public virtual bool IsMatch(string text)
    {
      if (this.m_Prefix != null)
        return text.StartsWith(this.m_Prefix);
      return true;
    }

    public virtual string Mutate(string text, bool display)
    {
      if (this.m_Prefix != null)
        text = text.Substring(this.m_Prefix.Length);
      if (!display && this.m_Format != null)
        text = string.Format(this.m_Format, (object) text);
      else if (display)
        text = this.m_Prepend + text + "_";
      return text;
    }
  }
}
