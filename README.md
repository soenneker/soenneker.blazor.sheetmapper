[![](https://img.shields.io/nuget/v/soenneker.blazor.sheetmapper.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.sheetmapper/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.sheetmapper/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.sheetmapper/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.sheetmapper.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.sheetmapper/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.sheetmapper/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.sheetmapper/actions/workflows/codeql.yml)

# Soenneker.Blazor.SheetMapper

A Blazor upload-and-selection component for matching CSV or tab-delimited column headers to writable properties on a C# type.

![SheetMapper example](https://github.com/user-attachments/assets/7aa39358-be2d-47af-8f04-68ac37281112)

[Live demo](https://soenneker.github.io/soenneker.blazor.sheetmapper)

SheetMapper produces a property-to-header dictionary. It does not parse data rows, convert cell values, validate a complete mapping, or create model instances.

## Installation

```bash
dotnet add package Soenneker.Blazor.SheetMapper
```

Register SheetMapper and its FilePond and Tom Select dependencies in `Program.cs`:

```csharp
using Soenneker.Blazor.SheetMapper.Registrars;

builder.Services.AddSheetMapperAsScoped();
```

Add the component namespace to `_Imports.razor`:

```razor
@using Soenneker.Blazor.SheetMapper
```

## Usage

```razor
@page "/employees/import"

<SheetMapper @ref="_mapper"
             TargetType="typeof(Employee)"
             AutomaticallyMap="true" />

<button type="button" @onclick="UseMapping">Continue</button>

@code {
    private SheetMapper? _mapper;

    private void UseMapping()
    {
        Dictionary<string, string> mapping = _mapper!.GetCurrentMap();

        // Example:
        // mapping["FirstName"] == "First Name"
        // mapping["Department"] == "" when no column was selected
    }
}
```

`TargetType` is required. The component creates one selector for each public, writable, non-indexer property. Uploading a new file replaces the current headers and mapping.

## Mapping behavior

When `AutomaticallyMap` is enabled—or when `AutoMap()` is called—the mapper compares each property name with the uploaded headers:

- comparison is case-insensitive;
- spaces are removed from headers before the second comparison;
- punctuation, underscores, aliases, and data annotations are not normalized;
- multiple properties may select the same header.

Duplicate selections are marked in the UI but are not rejected. Validate the dictionary before using it if every property must map to a unique column.

`GetCurrentMap()` returns property names as keys and selected source headers as values. Unmapped properties have an empty-string value.

## Parameters

| Parameter | Default | Purpose |
| --- | --- | --- |
| `TargetType` | — | Type whose public writable properties are displayed. |
| `AutomaticallyMap` | `false` | Runs the built-in name-matching heuristic after an upload. |
| `ShowStatusIcons` | `true` | Shows mapped, unmapped, and duplicate indicators. |
| `NotMappedIcon` | `⚠️` | Markup used for an unmapped property. |
| `DuplicatedIcon` | `🔁` | Markup used when a header is selected more than once. |
| `MappedIcon` | `✅` | Markup used for a unique selection. |

The rendered markup uses Bootstrap-style classes such as `input-group`, `input-group-text`, and `form-control`. Include compatible host styles or override those classes in your application. Package CSS is loaded automatically when the component becomes interactive.

## Upload considerations

- The upload is read in the browser interop path and is limited by FilePond's configured maximum stream size; the default configuration used here limits the stream to 2 MB.
- File content is used only to discover headers. Your application remains responsible for validating the file, parsing rows, converting values, and enforcing import limits before persistence.
- Spreadsheet formats such as `.xlsx` are not supported. Export them as CSV or tab-delimited text first.
