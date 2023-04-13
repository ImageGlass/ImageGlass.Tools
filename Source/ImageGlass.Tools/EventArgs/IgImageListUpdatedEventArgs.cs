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
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LIST_UPDATED"/> event.
/// </summary>
public class IgImageListUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the initial image file path.
    /// </summary>
    public string? InitFilePath { get; set; }


    /// <summary>
    /// Gets the image files list.
    /// </summary>
    public List<string>? Files { get; set; }


    /// <summary>
    /// Deserializes JSON string to <see cref="IgImageListUpdatedEventArgs"/> object.
    /// </summary>
    public static IgImageListUpdatedEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgImageListUpdatedEventArgsJsonContext.Default.IgImageListUpdatedEventArgs);

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
[JsonSerializable(typeof(IgImageListUpdatedEventArgs))]
public partial class IgImageListUpdatedEventArgsJsonContext : JsonSerializerContext { }

