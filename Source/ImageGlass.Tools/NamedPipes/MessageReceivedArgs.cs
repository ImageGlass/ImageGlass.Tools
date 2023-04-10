/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

namespace ImageGlass.Tools;


/// <summary>
/// Message received event arguments.
/// </summary>
public class MessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name of the pipe.
    /// </summary>
    public string PipeName { get; private set; }


    /// <summary>
    /// Gets the message received from the named-pipe.
    /// </summary>
    public string MessageName { get; private set; }


    /// <summary>
    /// Gets the message received from the named-pipe.
    /// </summary>
    public string MessageData { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class.
    /// </summary>
    public MessageReceivedEventArgs(string pipeName, string msgName, string msgData)
    {
        PipeName = pipeName;
        MessageName = msgName;
        MessageData = msgData;
    }

}