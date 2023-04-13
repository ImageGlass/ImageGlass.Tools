/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/
namespace ImageGlass.Tools;

using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;


/// <summary>
/// A simple named-pipe server.
/// </summary>
public class PipeServer : IDisposable
{
    private CancellationTokenSource _cancellationTokenSource;



    #region IDisposable Disposing

    public bool IsDisposed { get; private set; } = false;

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            Stop();

            _cancellationTokenSource.Dispose();
            ServerStream.Dispose();
        }

        // Free any unmanaged objects here.
        IsDisposed = true;
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PipeServer()
    {
        Dispose(false);
    }

    #endregion


    #region Properties & Events

    /// <summary>
    /// Gets the named-pipe server stream.
    /// </summary>
    public NamedPipeServerStream ServerStream { get; private set; }


    /// <summary>
    /// Gets the name of the <see cref="PipeServer"/>.
    /// </summary>
    public string PipeName { get; init; }


    /// <summary>
    /// Gets the tag number of the <see cref="PipeServer"/>. Default value is <c>0</c>.
    /// </summary>
    public int TagNumber { get; set; } = 0;


    /// <summary>
    /// Occurs when a message is received from the named pipe.
    /// </summary>
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;


    /// <summary>
    /// Occurs when the client disconnected.
    /// </summary>
    public event EventHandler<DisconnectedEventArgs> ClientDisconnected;

    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="PipeServer"/> class.
    /// </summary>
    /// <param name="pipeName">The name of the pipe.</param>
    /// <param name="pipeDirection">Determines the direction of the pipe.</param>
    public PipeServer(string pipeName, PipeDirection pipeDirection, int tagNumber = 0)
    {
        PipeName = pipeName;
        TagNumber = tagNumber;

        ServerStream = new NamedPipeServerStream(
            PipeName,
            pipeDirection,
            1,
            PipeTransmissionMode.Message,
            PipeOptions.Asynchronous);

        _cancellationTokenSource = new CancellationTokenSource();
    }



    #region Methods

    /// <summary>
    /// Start the pipe server.
    /// </summary>
    /// <exception cref="ObjectDisposedException"></exception>
    public void Start()
    {
        Start(_cancellationTokenSource.Token);
    }


    /// <summary>
    /// Start the pipe server.
    /// </summary>
    public void Start(CancellationToken token = default)
    {
        if (IsDisposed) return;

        var state = new PipeServerState(ServerStream, token);
        ServerStream.BeginWaitForConnection(ConnectionCallback, state);
    }


    /// <summary>
    /// The connection callback.
    /// </summary>
    private void ConnectionCallback(IAsyncResult result)
    {
        if (result.AsyncState is not PipeServerState pipeServer) return;
        if (IsDisposed) return;

        pipeServer.PipeServer.EndWaitForConnection(result);
        pipeServer.PipeServer.BeginRead(pipeServer.Buffer, 0, 255, ReadCallback, pipeServer);
    }


    /// <summary>
    /// The read callback.
    /// </summary>
    private void ReadCallback(IAsyncResult result)
    {
        if (result.AsyncState is not PipeServerState pipeState) return;
        if (IsDisposed) return;

        var received = pipeState.PipeServer.EndRead(result);

        // disconnected
        if (received == 0 || !pipeState.PipeServer.IsConnected)
        {
            ClientDisconnected?.Invoke(this, new DisconnectedEventArgs(PipeName));
            return;
        }

        var stringData = Encoding.UTF8.GetString(pipeState.Buffer, 0, received);
        pipeState.Message.Append(stringData);

        if (pipeState.PipeServer.IsMessageComplete)
        {
            var fullMsg = pipeState.Message.ToString();
            var separatorPosition = fullMsg.IndexOf(ImageGlassTool.MSG_SEPARATOR);
            var msgDataPosition = separatorPosition + ImageGlassTool.MSG_SEPARATOR.Length;
            var msgName = fullMsg[0..separatorPosition];
            var msgData = fullMsg[msgDataPosition..];

            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(PipeName, msgName, msgData));
            pipeState.Message.Clear();
        }

        if (!(_cancellationTokenSource.IsCancellationRequested
            || pipeState.ExternalCancellationToken.IsCancellationRequested))
        {
            if (pipeState.PipeServer.IsConnected)
            {
                pipeState.PipeServer.BeginRead(pipeState.Buffer, 0, 255, ReadCallback, pipeState);
            }
            else
            {
                pipeState.PipeServer.BeginWaitForConnection(ConnectionCallback, pipeState);
            }
        }
    }


    /// <summary>
    /// Stops the pipe server.
    /// </summary>
    public void Stop()
    {
        if (IsDisposed) return;

        _cancellationTokenSource.Cancel();

        if (ServerStream.IsConnected)
        {
            ServerStream.Disconnect();
        }
    }


    /// <summary>
    /// Waits for a client to connect.
    /// </summary>
    public async Task WaitForConnectionAsync(CancellationToken token = default)
    {
        if (IsDisposed) return;

        await ServerStream.WaitForConnectionAsync(token);
    }


    /// <summary>
    /// Sends a message to the client. The final message is packed with
    /// a <see cref="ImageGlassTool.MSG_SEPARATOR"/> between the <paramref name="msgName"/>
    /// and <paramref name="msgData"/>:
    /// <code>$"{<paramref name="msgName"/>}{<see cref="ImageGlassTool.MSG_SEPARATOR"/>}{<paramref name="msgData"/>}"</code>
    /// </summary>
    public async Task SendAsync(string msgName, string? msgData = "")
    {
        if (IsDisposed) return;

        var buffer = Encoding.UTF8.GetBytes($"{msgName}{ImageGlassTool.MSG_SEPARATOR}{msgData ?? ""}");

        await ServerStream.WriteAsync(buffer);
    }

    #endregion
}