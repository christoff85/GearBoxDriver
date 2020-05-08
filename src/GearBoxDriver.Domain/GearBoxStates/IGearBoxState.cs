using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.GearBoxStates
{
    public interface IGearBoxState
    {
        void Accelerate(Threshold threshold);
        void ManualUpshift();
        void ManualDownshift();
    }
}