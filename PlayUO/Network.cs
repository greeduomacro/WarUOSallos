// Decompiled with JetBrains decompiler
// Type: PlayUO.Network
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Net;
using System.Net.Sockets;

namespace PlayUO
{
  public class Network
  {
    private static NetworkContext _networkContext;

    public static NetworkContext Context
    {
      get
      {
        return Network._networkContext;
      }
    }

    public static bool Connect(BaseCrypto cryptoProvider, IPEndPoint ipEndPoint)
    {
      if (cryptoProvider == null)
        throw new ArgumentNullException("cryptoProvider");
      if (ipEndPoint == null)
        throw new ArgumentNullException("ipEndPoint");
      Network.Disconnect();
      try
      {
        Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
          socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, true);
        }
        catch (Exception ex)
        {
          Debug.Trace("SetSocketOption failed.");
          Debug.Error(ex);
        }
        Network.ProcessAsyncConnect(socket, socket.BeginConnect((EndPoint) ipEndPoint, (AsyncCallback) null, (object) null));
        Network._networkContext = new NetworkContext(socket, cryptoProvider);
        Network._networkContext.ConnectionLostCallback = (Callback) (() =>
        {
          Gumps.MessageBoxOk("Connection lost", true, new OnClick(Engine.DestroyDialogShowAcctLogin_OnClick));
          Network._networkContext = (NetworkContext) null;
          Cursor.Hourglass = false;
          Engine.amMoving = false;
        });
        return true;
      }
      catch (SocketException ex)
      {
        return false;
      }
    }

    private static void ProcessAsyncConnect(Socket socket, IAsyncResult asyncResult)
    {
      do
      {
        Engine.DrawNow();
      }
      while (!asyncResult.AsyncWaitHandle.WaitOne(10, false));
      socket.EndConnect(asyncResult);
    }

    public static void Disconnect()
    {
      Network.Disconnect(true);
    }

    public static void Disconnect(bool flush)
    {
      Engine.ClearPings();
      ActionContext.Clear();
      if (Network._networkContext == null)
        return;
      Network._networkContext.Close(flush);
      Network._networkContext = (NetworkContext) null;
    }

    public static void Close()
    {
      Network.Disconnect();
    }

    public static void Flush()
    {
      if (!Network.VerifyContext())
        return;
      Network._networkContext.Flush();
    }

    private static bool VerifyContext()
    {
      if (Network._networkContext != null)
      {
        if (Network._networkContext.IsOpen)
          return true;
        Network._networkContext = (NetworkContext) null;
      }
      return false;
    }

    public static bool Send(Packet p)
    {
      if (Network.VerifyContext())
        Network._networkContext.Send(p);
      return true;
    }

    public static bool Slice()
    {
      if (Network.VerifyContext())
        Network._networkContext.Cycle();
      return true;
    }
  }
}
