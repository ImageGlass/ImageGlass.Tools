/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

namespace ImageGlass.Tools;

public static class ImageGlassTool
{
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
}