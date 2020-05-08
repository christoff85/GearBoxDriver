using System;
using GearboxDriver.Domain.DriveModes.Providers;

namespace GearboxDriver.Domain.DriveModes.Factories
{
    public class MDynamicsModeFactory : IDriveModeFactory
    {
        private readonly IDriveModeFactory _baseModeFactory;
        private readonly IMDynamicsModeParametersProvider _mDynamicsModeParametersProvider;
        private readonly IExternalSystems _externalSystems;

        public MDynamicsModeFactory(IDriveModeFactory baseModeFactory, IMDynamicsModeParametersProvider mDynamicsModeParametersProvider, IExternalSystems externalSystems)
        {
            _baseModeFactory = baseModeFactory ?? throw new ArgumentNullException(nameof(baseModeFactory));
            _mDynamicsModeParametersProvider = mDynamicsModeParametersProvider ?? throw new ArgumentNullException(nameof(mDynamicsModeParametersProvider));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public IDriveMode Create()
        {
            var baseMode = _baseModeFactory.Create();

            return _mDynamicsModeParametersProvider.IsMDynamicsEnabled 
                ? new MDynamicsMode(_mDynamicsModeParametersProvider.CutOffAngularSpeed, baseMode, _externalSystems) 
                : baseMode;
        }
    }
}