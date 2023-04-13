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
/// Arguments for <see cref="ImageGlassEvents.TOOL_TERMINATE"/> event.
/// </summary>
public class IgToolTernimatingEventArgs : EventArgs
{
    /// <summary>
    /// Deserializes JSON string to <see cref="IgToolTernimatingEventArgs"/> object.
    /// </summary>
    public static IgToolTernimatingEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgToolTernimatingEventArgsJsonContext.Default.IgToolTernimatingEventArgs);

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
[JsonSerializable(typeof(IgToolTernimatingEventArgs))]
public partial class IgToolTernimatingEventArgsJsonContext : JsonSerializerContext { }

