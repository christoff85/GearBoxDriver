using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.GearBoxStates;

namespace GearBoxDriver.SampleProject
{
    public interface IDashboard
    {
        void SetAggresiveMode(AggressiveModeValue aggressiveModeValue);
        void SetDriveMode(DriveModeValue driveModeValue);
        void SetGearState(GearBoxStateValue gearBoxStateValue);
        void SetMDynamics(bool isMDynamicsEnabled);
        void SetTrailerAttached(bool isTrailerAttached);
    }
}