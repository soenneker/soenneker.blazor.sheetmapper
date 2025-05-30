using Soenneker.Blazor.SheetMapper.Abstract;
using System.Threading.Tasks;
using System.Threading;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Blazor.SheetMapper;

/// <inheritdoc cref="ISheetMapperInterop"/>
public sealed class SheetMapperInterop : ISheetMapperInterop
{
    private readonly AsyncSingleton _scriptInitializer;

    public SheetMapperInterop(IResourceLoader resourceLoader)
    {
        _scriptInitializer = new AsyncSingleton(async (token, _) =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Blazor.SheetMapper/css/sheetmapper.css", cancellationToken: token).NoSync();

            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _scriptInitializer.DisposeAsync();
    }
}