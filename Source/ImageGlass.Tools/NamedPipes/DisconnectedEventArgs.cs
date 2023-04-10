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
public class DisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name of the pipe.
    /// </summary>
    public string PipeName { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="DisconnectedEventArgs"/> class.
    /// </summary>
    public DisconnectedEventArgs(string pipeName)
    {
        PipeName = pipeName;
    }
}