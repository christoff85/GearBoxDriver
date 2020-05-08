namespace GearboxDriver.Domain.DriveModes.Providers
{
    public interface IDriveModeParametersProvider
    {
        DriveModeValue DriveModeValue { get; }
        GearShiftBoundaries EcoModeGearShiftBoundaries { get; }
        GearShiftBoundaries ComfortModeGearShiftBoundaries { get; }
        GearShiftBoundaries SportModeGearShiftBoundaries { get; }
    }
}