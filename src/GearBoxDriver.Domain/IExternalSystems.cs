using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain
{
    public interface IExternalSystems
    {
        AngularSpeed GetAngularSpeed();
        Rpm GetCurrentRpm();

        bool IsDrivingDown();
        void MakeSound(SoundVolume volume);
    }
}