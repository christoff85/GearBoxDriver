using System;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.DriveModes.Providers;

namespace GearboxDriver.Domain.DriveModes.Factories
{
    public class DriveModeFactory : IDriveModeFactory
    {
        private readonly IAggressiveModeFactory _aggressiveModeFactory;
        private readonly IDriveModeParametersProvider _driveModeParametersProvider;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public DriveModeFactory(IAggressiveModeFactory aggressiveModeFactory, IDriveModeParametersProvider driveModeParametersProvider, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _aggressiveModeFactory = aggressiveModeFactory ?? throw new ArgumentNullException(nameof(aggressiveModeFactory));
            _driveModeParametersProvider = driveModeParametersProvider ?? throw new ArgumentNullException(nameof(driveModeParametersProvider));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public IDriveMode Create()
        {
            switch (_driveModeParametersProvider.DriveModeValue)
            {
                case DriveModeValue.EcoMode:
                    return new EcoMode(_driveModeParametersProvider.EcoModeGearShiftBoundaries, _gearShifter, _externalSystems);
                  
                case DriveModeValue.ComfortMode:
                    return new ComfortMode(_driveModeParametersProvider.ComfortModeGearShiftBoundaries, _gearShifter, _aggressiveModeFactory);
    
                case DriveModeValue.SportMode:
                    return new SportMode(_driveModeParametersProvider.SportModeGearShiftBoundaries, _gearShifter, _aggressiveModeFactory);
    
                default:
                    throw new NotSupportedException();
            }
        }
    }
}