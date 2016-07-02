// Decompiled with JetBrains decompiler
// Type: PlayUO.Prompts.ASCIIPrompt
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO.Prompts
{
  public class ASCIIPrompt : IPrompt
  {
    private int m_Serial;
    private int m_Prompt;
    private string m_Text;

    public int Serial
    {
      get
      {
        return this.m_Serial;
      }
    }

    public int Prompt
    {
      get
      {
        return this.m_Prompt;
      }
    }

    public string Text
    {
      get
      {
        return this.m_Text;
      }
    }

    public ASCIIPrompt(int serial, int prompt, string text)
    {
      this.m_Serial = serial;
      this.m_Prompt = prompt;
      if ((this.m_Text = this.Text) != null && (this.m_Text = this.m_Text.Trim()).Length > 0)
        Engine.AddTextMessage(this.m_Text, 12.5f);
      else
        this.m_Text = "";
    }

    public void OnReturn(string message)
    {
      Network.Send((Packet) new PPrompt_Reply_ASCII(this.m_Serial, this.m_Prompt, message));
    }

    public void OnCancel(PromptCancelType type)
    {
      if (type != PromptCancelType.UserCancel)
        return;
      Network.Send((Packet) new PPrompt_Cancel_ASCII(this.m_Serial, this.m_Prompt));
    }
  }
}
