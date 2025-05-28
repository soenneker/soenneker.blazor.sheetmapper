using System.Threading.Tasks;
using System.Threading;
using System;

namespace Soenneker.Blazor.SheetMapper.Abstract;

/// <summary>
/// A Blazor interop library for the select user control library, Tom Select
/// </summary>
public interface ISheetMapperInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the SheetMapper interop by loading required scripts and styles.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the initialization operation.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}