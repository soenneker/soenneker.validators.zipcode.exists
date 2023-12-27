using System.Threading.Tasks;
using FluentAssertions;
using Soenneker.Validators.ZipCode.Exists.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Validators.ZipCode.Exists.Tests;

[Collection("Collection")]
public class ZipCodeExistsValidatorTests : FixturedUnitTest
{
    private readonly IZipCodeExistsValidator _validator;

    public ZipCodeExistsValidatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _validator = Resolve<IZipCodeExistsValidator>(true);
    }

    [Fact]
    public async Task Validate_ValidZipCode_ReturnsTrue()
    {
        const string validZipCode = "00611";
        bool result = await _validator.Validate(validZipCode);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_LongZipCode_ReturnsTrue()
    {
        const string longZipCode = "00611-5353";
        bool result = await _validator.Validate(longZipCode);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_InvalidZipCode_ReturnsFalse()
    {
        const string validZipCode = "12345";
        bool result = await _validator.Validate(validZipCode);

        result.Should().BeFalse();
    }
}