/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageGlass.Tools;


/// <summary>
/// Arguments for <see cref="ImageGlassEvents.THEME_UPDATED"/> event.
/// </summary>
public class IgThemeUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name the theme.
    /// </summary>
    public string? ThemeName { get; set; }


    /// <summary>
    /// Gets the name the theme.
    /// </summary>
    public bool IsDarkMode { get; set; }


    /// <summary>
    /// Gets the file path of the theme pack.
    /// </summary>
    public string? ThemePath { get; set; }


    /// <summary>
    /// Deserializes JSON string to <see cref="IgThemeUpdatedEventArgs"/> object.
    /// </summary>
    public static IgThemeUpdatedEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgThemeUpdatedEventArgsJsonContext.Default.IgThemeUpdatedEventArgs);

        return obj;
    }
}
[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Metadata,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull,
    IgnoreReadOnlyFields = true,
    IgnoreReadOnlyProperties = true
)]
[JsonSerializable(typeof(IgThemeUpdatedEventArgs))]
public partial class IgThemeUpdatedEventArgsJsonContext : JsonSerializerContext { }
