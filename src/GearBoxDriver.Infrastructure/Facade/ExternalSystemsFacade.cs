using System;
using GearboxDriver.Domain;
using GearboxDriver.Domain.ValueObjects;
using GearBoxDriver.Infrastructure.External;

namespace GearBoxDriver.Infrastructure.Facade
{
    public class ExternalSystemsFacade : IExternalSystems
    {
        private readonly ExternalSystems _externalSystems;
        private readonly SoundModule _soundModule;

        public ExternalSystemsFacade(ExternalSystems externalSystems, SoundModule soundModule)
        {
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
            _soundModule = soundModule ?? throw new ArgumentNullException(nameof(soundModule));
        }

        public AngularSpeed GetAngularSpeed()
        {
            return new AngularSpeed(_externalSystems.AngularSpeed);
        }
        public Rpm GetCurrentRpm()
        {
            return new Rpm(_externalSystems.CurrentRpm);
        }

        public bool IsDrivingDown()
        {
            var lightsPosition = _externalSystems.Lights.GetLightsPosition();
            return lightsPosition >= 1 || lightsPosition  <= 3;
        }

        public void MakeSound(SoundVolume volume)
        {
            _soundModule.MakeSound(volume.Db);
        }

    }
}