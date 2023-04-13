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
/// Arguments for <see cref="ImageGlassEvents.LANG_UPDATED"/> event.
/// </summary>
public class IgLanguageUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name the language.
    /// </summary>
    public string? LanguageName { get; set; }


    /// <summary>
    /// Gets the file path of the language pack.
    /// </summary>
    public string? LanguagePath { get; set; }


    /// <summary>
    /// Deserializes JSON string to <see cref="IgLanguageUpdatedEventArgs"/> object.
    /// </summary>
    public static IgLanguageUpdatedEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgLanguageUpdatedEventArgsJsonContext.Default.IgLanguageUpdatedEventArgs);

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
[JsonSerializable(typeof(IgLanguageUpdatedEventArgs))]
public partial class IgLanguageUpdatedEventArgsJsonContext : JsonSerializerContext { }

