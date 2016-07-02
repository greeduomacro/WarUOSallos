// Decompiled with JetBrains decompiler
// Type: PlayUO.TravelContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  internal class TravelContext : UseContext
  {
    private RunebookInfo m_Book;
    private RuneInfo m_Rune;
    private bool m_Recall;

    public RunebookInfo BookInfo
    {
      get
      {
        return this.m_Book;
      }
    }

    public RuneInfo RuneInfo
    {
      get
      {
        return this.m_Rune;
      }
    }

    public bool Recall
    {
      get
      {
        return this.m_Recall;
      }
    }

    public TravelContext(RunebookInfo book, RuneInfo rune, bool recall)
      : base((IEntity) book.Find(), false)
    {
      this.m_Book = book;
      this.m_Rune = rune;
      this.m_Recall = recall;
    }

    public override void OnDispatch()
    {
      Item book = this.toUse as Item;
      if (book == null)
        return;
      Network.Send((Packet) new PPE_InvokeRunebook(book, this.m_Rune, this.m_Recall));
    }

    protected override void OnEnqueue()
    {
      Mobile player = World.Player;
      player.AddTextMessage(player.Name, "- " + this.m_Rune.Name + " -", Engine.DefaultFont, Hues.Load(53), true);
      Party.SendAutomatedMessage("{0} to {1}", this.m_Recall ? (object) "Recalling" : (object) "Gating", (object) this.m_Rune.Name);
    }
  }
}
