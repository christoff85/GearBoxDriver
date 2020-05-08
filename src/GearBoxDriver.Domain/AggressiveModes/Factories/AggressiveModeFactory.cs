using System;
using GearboxDriver.Domain.AggressiveModes.Providers;

namespace GearboxDriver.Domain.AggressiveModes.Factories
{
    public class AggressiveModeFactory : IAggressiveModeFactory
    {
        private readonly IAggressiveModeParametersProvider _aggressiveModeParametersProvider;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public AggressiveModeFactory(IAggressiveModeParametersProvider aggressiveModeParametersProvider, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _aggressiveModeParametersProvider = aggressiveModeParametersProvider ?? throw new ArgumentNullException(nameof(aggressiveModeParametersProvider));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public IAggressiveMode Create()
        {
            switch (_aggressiveModeParametersProvider.AggressiveModeValue)
            {
                case AggressiveModeValue.AggresiveMode1:
                    return new AggressiveMode1(_gearShifter, _externalSystems);

                case AggressiveModeValue.AggresiveMode2:
                    return new AggressiveMode2(_aggressiveModeParametersProvider.AggressiveMode2UpshiftFactor, _gearShifter, _externalSystems);

                case AggressiveModeValue.AggresiveMode3:
                    return new AggressiveMode3(_aggressiveModeParametersProvider.AggressiveMode3UpshiftFactor, _gearShifter, _externalSystems);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}