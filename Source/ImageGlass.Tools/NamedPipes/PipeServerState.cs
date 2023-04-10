/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/
using System.IO.Pipes;
using System.Text;

namespace ImageGlass.Tools;


/// <summary>
/// Asynchronous state for the pipe server.
/// </summary>
internal class PipeServerState
{

    private const int BufferSize = 8125;


    #region Public Properties

    /// <summary>
    /// Gets the byte buffer.
    /// </summary>
    public byte[] Buffer { get; private set; }

    /// <summary>
    /// Gets the pipe server.
    /// </summary>
    public NamedPipeServerStream PipeServer { get; private set; }

    /// <summary>
    /// The external cancellation token.
    /// </summary>
    public CancellationToken ExternalCancellationToken { get; private set; }

    /// <summary>
    /// Gets the message.
    /// </summary>
    public StringBuilder Message { get; private set; }

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeServerState"/> class.
    /// </summary>
    /// <param name="pipeServer">The pipe server instance.</param>
    /// <param name="token">A token referenced by and external cancellation token.</param>
    public PipeServerState(NamedPipeServerStream pipeServer, CancellationToken token)
        : this(pipeServer, new byte[BufferSize], token)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeServerState"/> class.
    /// </summary>
    /// <param name="pipeServer">The pipe server instance.</param>
    /// <param name="buffer">The byte buffer.</param>
    /// <param name="token">A token referenced by and external cancellation token.</param>
    public PipeServerState(NamedPipeServerStream pipeServer, byte[] buffer, CancellationToken token)
    {
        this.PipeServer = pipeServer;
        this.Buffer = buffer;
        this.ExternalCancellationToken = token;
        this.Message = new StringBuilder();
    }

    #endregion

}