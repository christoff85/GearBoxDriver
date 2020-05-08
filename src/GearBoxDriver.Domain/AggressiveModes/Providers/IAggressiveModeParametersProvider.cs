using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.AggressiveModes.Providers
{
    public interface IAggressiveModeParametersProvider
    {
        AggressiveModeValue AggressiveModeValue { get; }
        RpmShiftFactor AggressiveMode2UpshiftFactor { get; }
        RpmShiftFactor AggressiveMode3UpshiftFactor { get; }
    }
}