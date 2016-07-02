// Decompiled with JetBrains decompiler
// Type: PlayUO.NetworkContext
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using PlayUO.Videos;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PlayUO
{
  public sealed class NetworkContext
  {
    private bool _isOpen;
    private Socket _socket;
    public BaseCrypto _crypto;
    private InputQueue _inputQueue;
    private OutputQueue _outputQueue;
    private byte[] _receiveBuffer;
    private AsyncCallback _sendCallback;
    private AsyncCallback _receiveCallback;
    private List<INetworkDiagnostic> _networkDiagnostics;
    private Callback _connectionLostCallback;
    public static int prior1;
    public static int prior2;
    public static int prior3;

    public bool IsOpen
    {
      get
      {
        return this._isOpen;
      }
    }

    public Callback ConnectionLostCallback
    {
      get
      {
        return this._connectionLostCallback;
      }
      set
      {
        this._connectionLostCallback = value;
      }
    }

    public NetworkContext(Socket socket, BaseCrypto crypto)
    {
      if (socket == null)
        throw new ArgumentNullException("socket");
      if (crypto == null)
        throw new ArgumentNullException("crypto");
      this._isOpen = true;
      this._socket = socket;
      this._crypto = crypto;
      this._inputQueue = new InputQueue();
      this._outputQueue = new OutputQueue((IBufferPolicy) new BufferAllocator(512));
      this._receiveBuffer = new byte[2048];
      this._sendCallback = new AsyncCallback(this.OnSend);
      this._receiveCallback = new AsyncCallback(this.OnReceive);
      this.Receive();
    }

    internal void RegisterDiagnostic(INetworkDiagnostic networkDiagnostic)
    {
      if (networkDiagnostic == null)
        throw new ArgumentNullException("networkDiagnostic");
      if (this._networkDiagnostics == null)
        this._networkDiagnostics = new List<INetworkDiagnostic>();
      if (this._networkDiagnostics.Contains(networkDiagnostic))
        return;
      networkDiagnostic.Open();
      this._networkDiagnostics.Add(networkDiagnostic);
    }

    internal void UnregisterDiagnostic(INetworkDiagnostic networkDiagnostic)
    {
      if (networkDiagnostic == null)
        throw new ArgumentNullException("networkDiagnostic");
      if (this._networkDiagnostics == null || !this._networkDiagnostics.Contains(networkDiagnostic))
        return;
      this._networkDiagnostics.Remove(networkDiagnostic);
      networkDiagnostic.Close();
    }

    public void Flush()
    {
      OutputQueue.Gram gram = this._outputQueue.Flush();
      if (gram == null)
        return;
      this.Dispatch(gram);
    }

    public void Close()
    {
      this.Close(true);
    }

    public void Close(bool flush)
    {
      if (!this._isOpen)
        return;
      lock (this)
      {
        if (!this._isOpen)
          return;
        this._isOpen = false;
        if (flush)
        {
          try
          {
            this._socket.Shutdown(SocketShutdown.Both);
          }
          catch
          {
          }
          try
          {
            this._socket.Close();
          }
          catch
          {
          }
        }
        if (this._networkDiagnostics == null)
          return;
        foreach (INetworkDiagnostic item_0 in this._networkDiagnostics)
          item_0.Close();
        this._networkDiagnostics = (List<INetworkDiagnostic>) null;
      }
    }

    public void Send(Packet packet)
    {
      if (packet == null)
        throw new ArgumentNullException("packet");
      if (Playback.Active && !(packet is PPing))
        return;
      if (!(packet is PUseRequest))
      {
        if (!(packet is PPickupItem))
          goto label_6;
      }
      Engine.PushAction();
label_6:
      try
      {
        byte[] buffer = packet.Compile();
        if (buffer.Length <= 0)
          return;
        if (this._networkDiagnostics != null)
        {
          for (int index = 0; index < this._networkDiagnostics.Count; ++index)
            this._networkDiagnostics[index].PacketSent(packet, buffer, 0, buffer.Length);
        }
        if (packet.Encode)
          this._crypto.Encrypt(buffer, 0, buffer.Length, (IConsolidator) this._outputQueue);
        else
          this._outputQueue.Enqueue(buffer, 0, buffer.Length);
        OutputQueue.Gram gram = this._outputQueue.Query();
        if (gram == null)
          return;
        this.Dispatch(gram);
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
      finally
      {
        packet.Dispose();
      }
    }

    private void Dispatch(OutputQueue.Gram gram)
    {
      if (!this._isOpen)
        return;
      try
      {
        this._socket.BeginSend(gram.Buffer, 0, gram.Length, SocketFlags.None, this._sendCallback, (object) null);
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
    }

    private void Receive()
    {
      if (!this._isOpen)
        return;
      try
      {
        this._socket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, this._receiveCallback, (object) null);
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
    }

    private void OnSend(IAsyncResult asyncResult)
    {
      try
      {
        if (this._socket.EndSend(asyncResult) > 0)
        {
          OutputQueue.Gram gram = this._outputQueue.Proceed();
          if (gram == null)
            return;
          this.Dispatch(gram);
        }
        else
          this.GracefulShutdown();
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
    }

    private void OnReceive(IAsyncResult asyncResult)
    {
      try
      {
        int length = this._socket.EndReceive(asyncResult);
        if (length > 0)
        {
          lock (this._inputQueue)
            this._crypto.Decrypt(this._receiveBuffer, 0, length, (IConsolidator) this._inputQueue);
          this.Receive();
        }
        else
          this.GracefulShutdown();
      }
      catch (Exception ex)
      {
        this.HandleError(ex);
      }
    }

    private void HandleError(Exception ex)
    {
      if (!this._isOpen)
        return;
      lock (this)
      {
        if (!this._isOpen)
          return;
        Debug.Trace("Hard disconnect");
        Debug.Error(ex);
        if (this._connectionLostCallback != null)
          this._connectionLostCallback();
        this.Close(false);
      }
    }

    private void GracefulShutdown()
    {
      if (!this._isOpen)
        return;
      lock (this)
      {
        if (!this._isOpen)
          return;
        Debug.Trace("Graceful disconnect");
        if (this._connectionLostCallback != null)
          this._connectionLostCallback();
        this.Close(false);
      }
    }

    public void Cycle()
    {
      if (Playback.Video != null)
        Playback.Video.Cycle();
      if (this._inputQueue.Length <= 0)
        return;
      try
      {
        PacketHandlers.BeginSlice();
        lock (this._inputQueue)
        {
          while (this._inputQueue.Length > 0)
          {
            int local_0 = this._inputQueue.GetPacketId();
            if (local_0 < 0)
              break;
            PacketHandler local_1 = PacketHandlers.m_Handlers[local_0];
            if (local_1 != null)
            {
              int local_2 = local_1.Length;
              if (local_2 == -1)
              {
                local_2 = this._inputQueue.GetPacketLength();
                if (local_2 < 3)
                {
                  if (local_2 < 0)
                    break;
                  this._inputQueue.Clear();
                  break;
                }
              }
              if (this._inputQueue.Length < local_2)
                break;
              ArraySegment<byte> local_3 = this._inputQueue.Dequeue(local_2);
              PacketReader local_4 = PacketReader.Initialize(local_3.Array, local_3.Offset, local_3.Count, local_1.Length != -1, (byte) local_0, local_1.Callback.Method.Name);
              if (this._networkDiagnostics != null)
              {
                foreach (INetworkDiagnostic item_0 in this._networkDiagnostics)
                  item_0.PacketReceived(local_1, local_3.Array, local_3.Offset, local_3.Count);
              }
              if (!Playback.Active || local_1.PacketID == 115)
                local_1.Handle(local_4);
              NetworkContext.prior3 = NetworkContext.prior2;
              NetworkContext.prior2 = NetworkContext.prior1;
              NetworkContext.prior1 = local_0;
            }
            else
            {
              ArraySegment<byte> local_6 = this._inputQueue.Dequeue(this._inputQueue.Length);
              PacketReader.Initialize(local_6.Array, local_6.Offset, local_6.Count, true, (byte) local_0, "Unknown").Trace(false);
              NetworkContext.prior3 = NetworkContext.prior2;
              NetworkContext.prior2 = NetworkContext.prior1;
              NetworkContext.prior1 = local_0;
              break;
            }
          }
        }
      }
      finally
      {
        PacketHandlers.FinishSlice();
      }
    }
  }
}
