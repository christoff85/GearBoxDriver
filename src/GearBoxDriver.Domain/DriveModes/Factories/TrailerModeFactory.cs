using System;
using GearboxDriver.Domain.DriveModes.Providers;

namespace GearboxDriver.Domain.DriveModes.Factories
{
    public class TrailerModeFactory : IDriveModeFactory
    {
        private readonly IDriveModeFactory _baseModeFactory;
        private readonly ITrailerModeParametersProvider _trailerModeParametersProvider;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public TrailerModeFactory(IDriveModeFactory baseModeFactory, ITrailerModeParametersProvider trailerModeParametersProvider, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _baseModeFactory = baseModeFactory ?? throw new ArgumentNullException(nameof(baseModeFactory));
            _trailerModeParametersProvider = trailerModeParametersProvider ?? throw new ArgumentNullException(nameof(trailerModeParametersProvider));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));

            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public IDriveMode Create()
        {
            var baseMode = _baseModeFactory.Create();

            return _trailerModeParametersProvider.IsTrailerAttached 
                ? new TrailerMode(baseMode, _gearShifter, _externalSystems) 
                : baseMode;
        }
    }
}