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
/// Arguments for <see cref="ImageGlassEvents.IMAGE_UNLOADED"/> event.
/// </summary>
public class IgImageUnloadedEventArgs : IgImageEventArgs
{
    /// <summary>
    /// Deserializes JSON string to <see cref="IgImageUnloadedEventArgs"/> object.
    /// </summary>
    public new static IgImageUnloadedEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgImageUnloadedEventArgsJsonContext.Default.IgImageUnloadedEventArgs);

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
[JsonSerializable(typeof(IgImageUnloadedEventArgs))]
public partial class IgImageUnloadedEventArgsJsonContext : JsonSerializerContext { }

