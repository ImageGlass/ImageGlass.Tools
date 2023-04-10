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
/// Asynchronous state for the pipe client.
/// </summary>
internal class PipeClientState
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
    public NamedPipeClientStream PipeClient { get; private set; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public StringBuilder Message { get; private set; }

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeClientState"/> class.
    /// </summary>
    /// <param name="pipeServer">The pipe server instance.</param>
    public PipeClientState(NamedPipeClientStream pipeServer)
        : this(pipeServer, new byte[BufferSize])
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="PipeClientState"/> class.
    /// </summary>
    /// <param name="pipeServer">The pipe server instance.</param>
    /// <param name="buffer">The byte buffer.</param>
    public PipeClientState(NamedPipeClientStream pipeServer, byte[] buffer)
    {
        this.PipeClient = pipeServer;
        this.Buffer = buffer;
        this.Message = new StringBuilder();
    }

    #endregion

}