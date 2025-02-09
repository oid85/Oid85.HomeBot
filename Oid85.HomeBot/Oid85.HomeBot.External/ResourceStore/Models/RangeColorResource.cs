﻿using System.Text.Json.Serialization;

namespace Oid85.HomeBot.External.ResourceStore.Models;

/// <summary>
/// Цвет диапазона
/// </summary>
public class RangeColorResource
{
    /// <summary>
    /// Верхний уровень
    /// </summary>
    [JsonPropertyName("highLevel")]
    public double HighLevel { get; set; }
    
    /// <summary>
    /// Нижний уровень
    /// </summary>
    [JsonPropertyName("lowLevel")]
    public double LowLevel { get; set; }
    
    /// <summary>
    /// Код цвета (RGB)
    /// </summary>
    [JsonPropertyName("colorCode")]
    public string ColorCode { get; set; } = string.Empty;
}
