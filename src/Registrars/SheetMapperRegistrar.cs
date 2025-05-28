using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.FilePond.Registrars;
using Soenneker.Blazor.SheetMapper.Abstract;
using Soenneker.Blazor.TomSelect.Registrars;

namespace Soenneker.Blazor.SheetMapper.Registrars;

/// <summary>
/// A Blazor component and utility library for mapping uploaded CSV or tabular files to C# objects. Supports header extraction and user-defined property mapping.
/// </summary>
public static class SheetMapperRegistrar
{
    /// <summary>
    /// Adds <see cref="ISheetMapper"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddSheetMapperAsScoped(this IServiceCollection services)
    {
        services.AddTomSelectInteropAsScoped().AddFilePondInteropAsScoped();

        services.TryAddScoped<ISheetMapperInterop, SheetMapperInterop>();
        services.TryAddScoped<ISheetMapper, SheetMapper>();

        return services;
    }
}