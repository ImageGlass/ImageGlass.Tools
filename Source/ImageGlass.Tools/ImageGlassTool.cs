/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

using System.Diagnostics;
using System.IO.Pipes;

namespace ImageGlass.Tools;

public class ImageGlassTool : IDisposable
{
    #region IDisposable Disposing

    public bool IsDisposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            _client.Dispose();
        }

        // Free any unmanaged objects here.
        IsDisposed = true;
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ImageGlassTool()
    {
        Dispose(false);
    }

    #endregion


    private readonly PipeClient _client;


    // Public properties
    #region Public properties

    /// <summary>
    /// Occurs when ImageGlass app sends a message to <see cref="ImageGlassTool"/>.
    /// </summary>
    public event EventHandler<MessageReceivedEventArgs>? ToolMessageReceived;


    /// <summary>
    /// Occurs when ImageGlass app requests <see cref="ImageGlassTool"/> app to close.
    /// <para>
    /// It happens when ImageGlass app sends <see cref="ImageGlassEvents.TOOL_TERMINATE"/>
    /// message, or disconnects to <see cref="ImageGlassTool"/> app.
    /// </para>
    /// </summary>
    public event EventHandler<DisconnectedEventArgs>? ToolClosingRequest;

    #endregion // Public properties


    /// <summary>
    /// Initializes <see cref="ImageGlassTool"/>.
    /// </summary>
    public ImageGlassTool()
    {
        var serverName = GetServerNameFromCmdLineArgs();

        _client = new PipeClient(serverName, PipeDirection.InOut);
        _client.MessageReceived += Client_MessageReceived;
        _client.Disconnected += Client_Disconnected;
    }


    // ImageGlass server connection
    #region ImageGlass server connection

    private void Client_MessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.MessageName)) return;

        OnToolMessageReceived(e);
    }


    private void Client_Disconnected(object? sender, DisconnectedEventArgs e)
    {
        OnToolClosingRequest(e);
    }


    /// <summary>
    /// Starts connecting to ImageGlass app.
    /// </summary>
    public async Task ConnectAsync()
    {
        if (_client == null) return;

        await _client.ConnectAsync();
    }


    /// <summary>
    /// Emits <see cref="ToolMessageReceived"/> event.
    /// </summary>
    protected virtual void OnToolMessageReceived(MessageReceivedEventArgs e)
    {
        // terminate slideshow
        if (e.MessageName == ImageGlassEvents.TOOL_TERMINATE)
        {
            OnToolClosingRequest(new(_client.PipeName));
            return;
        }

        ToolMessageReceived?.Invoke(this, e);
    }


    /// <summary>
    /// Emits <see cref="ToolClosingRequest"/> event.
    /// </summary>
    protected virtual void OnToolClosingRequest(DisconnectedEventArgs e)
    {
        ToolClosingRequest?.Invoke(this, e);
    }

    #endregion // ImageGlass server connection




    // Static props & methods
    #region Static props & methods

    /// <summary>
    /// Gets the prefix of pipe name command line argument passed to tool client.
    /// </summary>
    public static string PIPE_CODE_CMD_LINE => "--ig-tool-pipe-code=";

    /// <summary>
    /// Gets the pipename prefix.
    /// </summary>
    public static string PIPE_NAME_PREFIX => "+IG_TOOL+_";

    /// <summary>
    /// Gets the message separator constant.
    /// This constant is auto-inserted in between message name and message data.
    /// </summary>
    public static string MSG_SEPARATOR => "{:+IG_TOOL+:}";


    /// <summary>
    /// Creates server name by combining <see cref="PIPE_NAME_PREFIX"/> and <paramref name="code"/>.
    /// </summary>
    /// <returns>Example: <c>+IG_TOOL+_hello"</c></returns>
    public static string CreateServerName(string code)
    {
        return $"{PIPE_NAME_PREFIX}{code}";
    }


    /// <summary>
    /// Gets server name by combining <see cref="PIPE_NAME_PREFIX"/> and
    /// value of the command line arg <see cref="PIPE_CODE_CMD_LINE"/>.
    /// </summary>
    /// <returns>Example: <c>"+IG_TOOL+_hello"</c></returns>
    public static string GetServerNameFromCmdLineArgs()
    {
        var cmd = Environment.GetCommandLineArgs()
            .FirstOrDefault(i => i.StartsWith(PIPE_CODE_CMD_LINE, StringComparison.InvariantCultureIgnoreCase));
        var pipeCode = cmd?[PIPE_CODE_CMD_LINE.Length..];

        if (!string.IsNullOrEmpty(pipeCode))
        {
            return $"{PIPE_NAME_PREFIX}{pipeCode}";
        }

        return PIPE_NAME_PREFIX;
    }


    /// <summary>
    /// Launches tool app.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static Process? LaunchTool(string filename, string args, bool asAdmin = false)
    {
        var proc = new Process();
        proc.StartInfo.FileName = filename;
        proc.StartInfo.Arguments = args;

        proc.StartInfo.Verb = asAdmin ? "runas" : "";
        proc.StartInfo.UseShellExecute = true;

        try
        {
            proc.Start();

            return proc;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("system cannot find the file", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FileNotFoundException(ex.Message, filename);
            }
        }

        return null;
    }


    #endregion // Static props & methods

}