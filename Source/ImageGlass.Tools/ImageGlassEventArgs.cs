/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

namespace ImageGlass.Tools;


public class IgImageEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current viewing image's index.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    /// Gets the current viewing image's path.
    /// </summary>
    public string? FilePath { get; init; }
}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LOADING"/> event.
/// </summary>
public class IgImageLoadingEventArgs : IgImageEventArgs
{
    /// <summary>
    /// Gets the loading image's index.
    /// </summary>
    public int NewIndex { get; init; }

    /// <summary>
    /// Gets the loading image frame index.
    /// </summary>
    public uint FrameIndex { get; init; }

    /// <summary>
    /// Gets the value indicating that the image is being viewed as a separate frame.
    /// </summary>
    public bool IsViewingSeparateFrame { get; init; }
}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LOADED"/> event.
/// </summary>
public class IgImageLoadedEventArgs : IgImageEventArgs
{
    /// <summary>
    /// Gets the loaded image frame index.
    /// </summary>
    public uint FrameIndex { get; init; }

    /// <summary>
    /// Checks if the loaded image is error.
    /// </summary>
    public bool IsError { get; init; }

    /// <summary>
    /// Gets the value indicating that the image is being viewed as a separate frame.
    /// </summary>
    public bool IsViewingSeparateFrame { get; init; }
}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.IMAGE_UNLOADED"/> event.
/// </summary>
public class IgImageUnloadedEventArgs : IgImageEventArgs { }


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LIST_UPDATED"/> event.
/// </summary>
public class IgImageListUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the initial image file path.
    /// </summary>
    public string? InitFilePath { get; init; }


    /// <summary>
    /// Gets the image files list.
    /// </summary>
    public List<string>? Files { get; init; }
}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.LANG_UPDATED"/> event.
/// </summary>
public class IgLanguageUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name the language.
    /// </summary>
    public string? LanguageName { get; init; }


    /// <summary>
    /// Gets the file path of the language pack.
    /// </summary>
    public string? LanguagePath { get; init; }
}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.THEME_UPDATED"/> event.
/// </summary>
public class IgThemeUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name the theme.
    /// </summary>
    public string? ThemeName { get; init; }


    /// <summary>
    /// Gets the name the theme.
    /// </summary>
    public bool IsDarkMode { get; init; }


    /// <summary>
    /// Gets the file path of the theme pack.
    /// </summary>
    public string? ThemePath { get; init; }

}


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.TOOL_TERMINATE"/> event.
/// </summary>
public class IgToolTernimatingEventArgs : EventArgs { }

