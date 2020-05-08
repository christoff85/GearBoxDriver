using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain
{
    public interface IGearBox
    {
        Gear GetCurrentGear();
        void SetCurrentGear(Gear gear);
        void SetGearBoxState(GearBoxStateValue gearBoxStateValue);
    }
}