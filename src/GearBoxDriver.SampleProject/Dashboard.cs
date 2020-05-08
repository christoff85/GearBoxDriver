using System;
using GearboxDriver.Domain;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.AggressiveModes.Providers;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.DriveModes.Providers;
using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.GearBoxStates.Providers;
using GearboxDriver.Domain.ValueObjects;

namespace GearBoxDriver.SampleProject
{
    public class Dashboard : IDashboard, 
        IAggressiveModeParametersProvider, 
        IGearBoxStateValueProvider, 
        IDriveModeParametersProvider, 
        IMDynamicsModeParametersProvider, 
        ITrailerModeParametersProvider
    {
        private readonly IGearBox _gearBox;

        public AggressiveModeValue AggressiveModeValue { get; private set; }
        public RpmShiftFactor AggressiveMode2UpshiftFactor => new RpmShiftFactor(1.2d);
        public RpmShiftFactor AggressiveMode3UpshiftFactor => new RpmShiftFactor(1.3d);

        public DriveModeValue DriveModeValue { get; private set; }
        public GearShiftBoundaries EcoModeGearShiftBoundaries => new GearShiftBoundaries(new Rpm(1000d), new Rpm(2000d));
        public GearShiftBoundaries ComfortModeGearShiftBoundaries => new GearShiftBoundaries(new Rpm(1000d), new Rpm(2500d));
        public GearShiftBoundaries SportModeGearShiftBoundaries => new GearShiftBoundaries(new Rpm(1500d), new Rpm(4500d));

        public GearBoxStateValue GearBoxStateValue { get; private set; }

        public bool IsMDynamicsEnabled { get; private set; }
        public AngularSpeed CutOffAngularSpeed => new AngularSpeed(50d);

        public bool IsTrailerAttached { get; private set; }

        public Dashboard(IGearBox gearBox)
        {
            _gearBox = gearBox ?? throw new ArgumentNullException();
        }

        public void SetAggresiveMode(AggressiveModeValue aggressiveModeValue)
        {
            AggressiveModeValue = aggressiveModeValue;
        }

        public void SetDriveMode(DriveModeValue driveModeValue)
        {
            DriveModeValue = driveModeValue;
        }

        public void SetGearState(GearBoxStateValue gearBoxStateValue)
        {
            GearBoxStateValue = gearBoxStateValue;
            _gearBox.SetGearBoxState(gearBoxStateValue);
        }

        public void SetMDynamics(bool isMDynamicsEnabled)
        {
            IsMDynamicsEnabled = isMDynamicsEnabled;
        }

        public void SetTrailerAttached(bool isTrailerAttached)
        {
            IsTrailerAttached = isTrailerAttached;
        }
    }
}
