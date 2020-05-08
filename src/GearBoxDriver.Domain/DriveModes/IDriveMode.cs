using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public interface IDriveMode
    {
        void Accelerate(Threshold threshold);
    }
}