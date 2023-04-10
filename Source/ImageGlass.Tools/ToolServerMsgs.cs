/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

namespace ImageGlass.Tools;


/// <summary>
/// Contains messages of <see cref="PipeServer"/> to send to <see cref="PipeClient"/>.
/// </summary>
public static class ToolServerMsgs
{
    /// <summary>
    /// Requests <see cref="PipeClient"/> to terminate the process.
    /// </summary>
    public static string TOOL_TERMINATE => "igtool.cmd.tool_terminate";



    /// <summary>
    /// Occurs when an image is being loaded.
    /// </summary>
    public static string IMAGE_LOADING => "igtool.cmd.image_loading";

    /// <summary>
    /// Occurs when the image is loaded.
    /// </summary>
    public static string IMAGE_LOADED => "igtool.cmd.image_loaded";

    /// <summary>
    /// Occurs when the image is unloaded.
    /// </summary>
    public static string IMAGE_UNLOADED => "igtool.cmd.image_unloaded";



    /// <summary>
    /// Occurs when the image list is updated.
    /// </summary>
    public static string IMAGE_LIST_UPDATED => "igtool.cmd.list_updated";

    /// <summary>
    /// Occurs when the language is updated.
    /// </summary>
    public static string LANG_UPDATED => "igtool.cmd.lang_updated";

    /// <summary>
    /// Occurs when the theme is updated.
    /// </summary>
    public static string THEME_UPDATED => "igtool.cmd.theme_updated";
}
