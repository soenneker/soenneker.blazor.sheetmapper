using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Blazor.SheetMapper.Abstract;

/// <summary>
/// Provides an interface for mapping CSV file headers to properties of a specified C# model type.
/// </summary>
public interface ISheetMapper
{
    /// <summary>
    /// Gets or sets the target type to map CSV columns to. Must be a class with public writable properties.
    /// </summary>
    Type? TargetType { get; set; }

    /// <summary>
    /// Gets or sets whether the component should automatically map CSV headers to target properties using simple heuristics.
    /// </summary>
    bool AutomaticallyMap { get; set; }

    /// <summary>
    /// Attempts to automatically map CSV headers to target properties using case-insensitive matching and basic normalization (e.g., removing spaces).
    /// </summary>
    void AutoMap();

    /// <summary>
    /// Returns the current column-to-property map. The key is the property name and the value is the matched CSV column header, or an empty string if not mapped.
    /// </summary>
    /// <returns>A dictionary representing the current property-to-CSV-header mapping.</returns>
    Dictionary<string, string> GetCurrentMap();

    /// <summary>
    /// Initializes the sheet mapper interop service. Should be called during component initialization.
    /// </summary>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    ValueTask InitializeInterop();
}