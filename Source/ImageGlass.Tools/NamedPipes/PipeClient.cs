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

/// <summary>
/// A simple class for connecting to the named-pipe server.
/// </summary>
public class PipeClient : IDisposable
{

    #region IDisposable Disposing

    public bool IsDisposed { get; private set; } = false;

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            ClientStream.Dispose();
        }

        // Free any unmanaged objects here.
        IsDisposed = true;
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PipeClient()
    {
        Dispose(false);
    }

    #endregion


    #region Properties & Events

    /// <summary>
    /// Gets or sets the client stream.
    /// </summary>
    private NamedPipeClientStream ClientStream { get; set; }


    /// <summary>
    /// Gets the name of the <see cref="PipeClient"/>.
    /// </summary>
    public string PipeName { get; init; }


    /// <summary>
    /// Occurs when a message is received from the pipe server.
    /// </summary>
    public event EventHandler<MessageReceivedEventArgs>? MessageReceived;


    /// <summary>
    /// Occurs when the client disconnected.
    /// </summary>
    public event EventHandler<DisconnectedEventArgs>? Disconnected;

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeClient"/> class.
    /// </summary>
    /// <param name="pipeName">
    /// The name of the pipe.
    /// </param>
    /// <param name="pipeDirection">
    /// Determines the direction of the pipe.
    /// </param>
    public PipeClient(string pipeName, PipeDirection pipeDirection)
    {
        PipeName = pipeName;

        ClientStream = new NamedPipeClientStream(".", pipeName, pipeDirection, PipeOptions.Asynchronous);
    }

    #endregion


    #region Methods

    /// <summary>
    /// Connects to the pipe server.
    /// </summary>
    /// <param name="timeout">The time to wait before timing out.</param>
    /// <exception cref="ObjectDisposedException">The object is disposed.</exception>
    /// <exception cref="TimeoutException">Could not connect to the server within the specified timeout period.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Timeout is less than 0 and not set to Infinite.</exception>
    public async Task ConnectAsync(int timeout = 5000)
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(typeof(PipeClient).Name);
        }

        await ClientStream.ConnectAsync(timeout);
        ClientStream.ReadMode = PipeTransmissionMode.Message;

        var clientState = new PipeClientState(ClientStream);

        ClientStream.BeginRead(
            clientState.Buffer,
            0,
            clientState.Buffer.Length,
            ReadCallback,
            clientState);
    }


    /// <summary>
    /// The read callback.
    /// </summary>
    private void ReadCallback(IAsyncResult result)
    {
        if (result.AsyncState is not PipeClientState pipeState) return;

        var received = pipeState.PipeClient.EndRead(result);

        // disconnected
        if (received == 0 || !pipeState.PipeClient.IsConnected)
        {
            Disconnected?.Invoke(this, new DisconnectedEventArgs(PipeName));
            return;
        }

        var stringData = Encoding.UTF8.GetString(pipeState.Buffer, 0, received);
        pipeState.Message.Append(stringData);

        if (pipeState.PipeClient.IsMessageComplete)
        {
            var fullMsg = pipeState.Message.ToString();
            var separatorPosition = fullMsg.IndexOf(ImageGlassTool.MSG_SEPARATOR);
            var msgDataPosition = separatorPosition + ImageGlassTool.MSG_SEPARATOR.Length;
            var msgName = fullMsg[0..separatorPosition];
            var msgData = fullMsg[msgDataPosition..];

            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(PipeName, msgName, msgData));
            pipeState.Message.Clear();
        }

        pipeState.PipeClient.BeginRead(pipeState.Buffer, 0, 255, ReadCallback, pipeState);
    }


    /// <summary>
    /// Sends a string to the server.
    /// </summary>
    /// <param name="value">The string to send to the server.</param>
    /// <exception cref="ObjectDisposedException"></exception>
    public async Task SendAsync(string value)
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(typeof(PipeClient).Name);
        }

        var buffer = Encoding.UTF8.GetBytes(value);

        await ClientStream.WriteAsync(buffer);
    }


    #endregion
}