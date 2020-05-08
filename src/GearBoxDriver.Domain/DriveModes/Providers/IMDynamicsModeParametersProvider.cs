using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes.Providers
{
    public interface IMDynamicsModeParametersProvider
    {
        AngularSpeed CutOffAngularSpeed { get; }
        bool IsMDynamicsEnabled { get; }
    }
}