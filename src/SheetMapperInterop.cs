using Soenneker.Blazor.SheetMapper.Abstract;
using System.Threading.Tasks;
using System.Threading;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Blazor.SheetMapper;

/// <inheritdoc cref="ISheetMapperInterop"/>
public sealed class SheetMapperInterop : ISheetMapperInterop
{
    private readonly AsyncInitializer _scriptInitializer;

    public SheetMapperInterop(IResourceLoader resourceLoader)
    {
        _scriptInitializer = new AsyncInitializer(async token =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Blazor.SheetMapper/css/sheetmapper.css", cancellationToken: token);
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
