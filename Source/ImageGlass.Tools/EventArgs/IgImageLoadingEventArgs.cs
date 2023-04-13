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
/// Arguments for <see cref="ImageGlassEvents.IMAGE_LOADING"/> event.
/// </summary>
public class IgImageLoadingEventArgs : IgImageEventArgs
{
    /// <summary>
    /// Gets the loading image's index.
    /// </summary>
    public int NewIndex { get; set; }

    /// <summary>
    /// Gets the loading image frame index.
    /// </summary>
    public uint FrameIndex { get; set; }

    /// <summary>
    /// Gets the value indicating that the image is being viewed as a separate frame.
    /// </summary>
    public bool IsViewingSeparateFrame { get; set; }


    /// <summary>
    /// Deserializes JSON string to <see cref="IgImageLoadingEventArgs"/> object.
    /// </summary>
    public static new IgImageLoadingEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgImageLoadingEventArgsJsonContext.Default.IgImageLoadingEventArgs);

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
[JsonSerializable(typeof(IgImageLoadingEventArgs))]
public partial class IgImageLoadingEventArgsJsonContext : JsonSerializerContext { }

