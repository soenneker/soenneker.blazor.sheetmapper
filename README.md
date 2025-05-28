[![](https://img.shields.io/nuget/v/soenneker.blazor.sheetmapper.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.sheetmapper/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.sheetmapper/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.sheetmapper/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.sheetmapper.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.sheetmapper/)

# Soenneker.Blazor.SheetMapper

A Blazor component and utility library for mapping CSV or tabular files to C# objects.

![image](https://github.com/user-attachments/assets/7aa39358-be2d-47af-8f04-68ac37281112)

Leverage [FilePond](https://github.com/soenneker/soenneker.blazor.filepond) for uploads and [TomSelect](https://github.com/soenneker/soenneker.blazor.tomselect) for interactive dropdowns. Automatically extract headers, map columns to your model, and retrieve a clean `{ property → column }` map.

[Demo](https://soenneker.github.io/soenneker.blazor.sheetmapper)

## Features

* **Header extraction** from CSV or tab-delimited files
* **Interactive mapping** of columns to model properties
* **Automatic heuristic mapping** (case‑ and whitespace‑insensitive)
* **Status icons** for *unmapped*, *duplicate*, and *mapped* states
* **Show/hide** the status‑icon column on demand
* **API** to fetch the final mapping as `Dictionary<string, string>`

---

## Installation

```bash
dotnet add package Soenneker.Blazor.SheetMapper
```

---

## Setup

1. **Register interop** in your DI container (e.g., `Program.cs`):

   ```csharp
   builder.Services.AddSheetMapperAsScoped();
   ```

2. **Import namespace** in your `_Imports.razor` or component:

   ```razor
   @using Soenneker.Blazor.SheetMapper
   ```

---

## Basic Usage

```razor
@page "/import"
@inject ISheetMapperInterop SheetMapperInterop

<SheetMapper
    @ref="sheetMapper"
    TargetType="typeof(Employee)"
    AutomaticallyMap="true"
    ShowStatusIcons="true" />

<button class="btn btn-primary mt-3" @onclick="ShowMap">
    Get Mapping
</button>

@code {
    private SheetMapper? sheetMapper;

    private void ShowMap()
    {
        if (sheetMapper is not null)
        {
            var map = sheetMapper.GetCurrentMap();
            // map: property → CSV column
        }
    }
}
```

---

## Component Parameters

| Parameter          | Type             | Default | Description                                                    |
| ------------------ | ---------------- | ------- | -------------------------------------------------------------- |
| `TargetType`       | `Type`           | —       | **Required.** Model type whose writable properties are mapped. |
| `AutomaticallyMap` | `bool`           | `false` | Run auto-mapping heuristic on file load.                       |
| `ShowStatusIcons`  | `bool`           | `true`  | Toggle visibility of the status‑icon column.                   |
| `NotMappedIcon`    | `RenderFragment` | ⚠️      | Icon/markup for unmapped state.                                |
| `DuplicatedIcon`   | `RenderFragment` | 🔁      | Icon/markup for duplicate‑mapping state.                       |
| `MappedIcon`       | `RenderFragment` | ✅       | Icon/markup for successful mapping.                            |

---

## Public API

* `void AutoMap()`
  Re-run the auto-mapping logic at any time.

* `Dictionary<string, string> GetCurrentMap()`
  Returns `{ property → selected column }`. Unmapped properties return `""`.

---

## Customization

### Status Icons

Override the built-in emojis with your own markup (SVG, `<i>`, etc.):

```razor
<SheetMapper
    TargetType="typeof(Employee)"
    NotMappedIcon="@<i class='fas fa-exclamation-triangle'></i>"
    DuplicatedIcon="@<i class='fas fa-sync-alt'></i>"
    MappedIcon="@<i class='fas fa-check-circle'></i>"
/>
```

### Show/Hide Icon Column

Use the `ShowStatusIcons` parameter to toggle the entire status‑icon column:

```razor
<!-- hides the icons -->
<SheetMapper TargetType="typeof(Employee)" ShowStatusIcons="false" />
```

---

## Styling

* **CSS classes** for fine‑tuning:

  * `.map-row` — wrapper for each mapping row
  * `.status-icon` — container for the icon

Override or extend them to match your design system.
