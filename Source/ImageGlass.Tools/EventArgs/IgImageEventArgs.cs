/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageGlass.Tools;


public class IgImageEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current viewing image's index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets the current viewing image's path.
    /// </summary>
    public string? FilePath { get; set; }


    /// <summary>
    /// Deserializes JSON string to <see cref="IgImageEventArgs"/> object.
    /// </summary>
    public static IgImageEventArgs? Deserialize(string json)
    {
        var obj = JsonSerializer.Deserialize(json, IgImageEventArgsJsonContext.Default.IgImageEventArgs);

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
[JsonSerializable(typeof(IgImageEventArgs))]
public partial class IgImageEventArgsJsonContext : JsonSerializerContext { }

