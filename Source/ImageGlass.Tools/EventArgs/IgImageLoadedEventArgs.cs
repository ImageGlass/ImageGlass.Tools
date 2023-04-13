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
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LOADED"/> event.
/// </summary>
public class IgImageLoadedEventArgs : IgImageEventArgs
{
    /// <summary>
    /// Gets the loaded image frame index.
    /// </summary>
    public uint FrameIndex { get; set; }

    /// <summary>
    /// Checks if the loaded image is error.
    /// </summary>
    public bool IsError { get; set; }

    /// <summary>
    /// Gets the value indicating that the image is being viewed as a separate frame.
    /// </summary>
    public bool IsViewingSeparateFrame { get; set; }

    /// <summary>
    /// Deserializes JSON string to <see cref="IgImageLoadedEventArgs"/> object.
    /// </summary>
    public new static IgImageLoadedEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgImageLoadedEventArgsJsonContext.Default.IgImageLoadedEventArgs);

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
[JsonSerializable(typeof(IgImageLoadedEventArgs))]
public partial class IgImageLoadedEventArgsJsonContext : JsonSerializerContext { }

