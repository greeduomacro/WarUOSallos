// Decompiled with JetBrains decompiler
// Type: PlayUO.GLogOutQuery
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public class GLogOutQuery : GMessageBoxYesNo
  {
    private static GLogOutQuery m_Instance;

    private GLogOutQuery()
      : base("Quit\nUltima Online?", false, (MBYesNoCallback) null)
    {
    }

    public static void Display()
    {
      if (GLogOutQuery.m_Instance != null)
        return;
      GLogOutQuery.m_Instance = new GLogOutQuery();
      Gumps.Desktop.Children.Add((Gump) GLogOutQuery.m_Instance);
    }

    protected internal override void OnDispose()
    {
      GLogOutQuery.m_Instance = (GLogOutQuery) null;
    }

    protected override void OnSignal(bool response)
    {
      if (!response)
        return;
      Engine.m_Ingame = false;
      Network.Send((Packet) new PDisconnect());
      Network.Disconnect();
      Engine.ShowAcctLogin();
    }
  }
}
