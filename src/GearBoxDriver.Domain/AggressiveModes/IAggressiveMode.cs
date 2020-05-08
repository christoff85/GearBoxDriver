using GearboxDriver.Domain.DriveModes;

namespace GearboxDriver.Domain.AggressiveModes
{
    public interface IAggressiveMode
    {
        void Accelerate(GearShiftBoundaries gearShiftBoundaries);
    }
}