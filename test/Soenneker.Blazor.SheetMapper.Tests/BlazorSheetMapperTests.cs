using Soenneker.Blazor.SheetMapper.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.SheetMapper.Tests;

[Collection("Collection")]
public sealed class BlazorSheetMapperTests : FixturedUnitTest
{
    private readonly ISheetMapper _blazorlibrary;

    public BlazorSheetMapperTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _blazorlibrary = Resolve<ISheetMapper>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
