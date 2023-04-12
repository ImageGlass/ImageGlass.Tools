/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

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
    /// It happens when ImageGlass app sends <see cref="ToolServerMsgs.TOOL_TERMINATE"/>
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
        var serverName = CreateServerName();

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
        if (e.MessageName == ToolServerMsgs.TOOL_TERMINATE)
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
    /// Gets the pipename prefix.
    /// </summary>
    public static string PIPENAME_PREFIX => "+IG_TOOL+_";

    /// <summary>
    /// Gets the message separator constant.
    /// This constant is auto-inserted in between message name and message data.
    /// </summary>
    public static string MSG_SEPARATOR => "{:+IG_TOOL+:}";

    /// <summary>
    /// Gets the command-line for enabling window top most.
    /// </summary>
    public static string CMD_WINDOW_TOP_MOST => "EnableWindowTopMost";


    /// <summary>
    /// Creates server name by combining <see cref="PIPENAME_PREFIX"/> and
    /// <see cref="Environment.ProcessPath"/>.
    /// </summary>
    /// <returns>Example: <c>+IG_TOOL+_app.exe"</c></returns>
    public static string CreateServerName()
    {
        var fileName = Path.GetFileName(Environment.ProcessPath);

        return $"{PIPENAME_PREFIX}{fileName}";
    }

    #endregion // Static props & methods

}