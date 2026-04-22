using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.SheetMapper.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BlazorSheetMapperTests : HostedUnitTest
{

    public BlazorSheetMapperTests(Host host) : base(host)
    {

    }

    [Test]
    public void Default()
    {

    }
}
