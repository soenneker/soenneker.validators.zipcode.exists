using System.Threading.Tasks;
using AwesomeAssertions;
using Soenneker.Validators.ZipCode.Exists.Abstract;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Validators.ZipCode.Exists.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class ZipCodeExistsValidatorTests : HostedUnitTest
{
    private readonly IZipCodeExistsValidator _validator;

    public ZipCodeExistsValidatorTests(Host host) : base(host)
    {
        _validator = Resolve<IZipCodeExistsValidator>(true);
    }

    [Test]
    public async Task Validate_ValidZipCode_ReturnsTrue()
    {
        const string validZipCode = "00611";
        bool result = await _validator.Validate(validZipCode, CancellationToken);

        result.Should().BeTrue();
    }

    [Test]
    public async Task Validate_LongZipCode_ReturnsTrue()
    {
        const string longZipCode = "00611-5353";
        bool result = await _validator.Validate(longZipCode, CancellationToken);

        result.Should().BeTrue();
    }

    [Test]
    public async Task Validate_InvalidZipCode_ReturnsFalse()
    {
        const string validZipCode = "12345";
        bool result = await _validator.Validate(validZipCode, CancellationToken);

        result.Should().BeFalse();
    }
}