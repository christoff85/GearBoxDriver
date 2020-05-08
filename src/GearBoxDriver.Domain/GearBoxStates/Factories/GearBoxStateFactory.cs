using System;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.GearBoxStates.Providers;

namespace GearboxDriver.Domain.GearBoxStates.Factories
{
    public class GearBoxStateFactory : IGearBoxStateFactory
    {
        private readonly IDriveModeFactory _driveModeFactory;
        private readonly IGearBoxStateValueProvider _gearBoxStateValueProvider;
        private readonly IGearShifter _gearShifter;

        public GearBoxStateFactory(IDriveModeFactory driveModeFactory, IGearBoxStateValueProvider gearBoxStateValueProvider, IGearShifter gearShifter)
        {
            _driveModeFactory = driveModeFactory ?? throw new ArgumentNullException(nameof(driveModeFactory));
            _gearBoxStateValueProvider = gearBoxStateValueProvider ?? throw new ArgumentNullException(nameof(gearBoxStateValueProvider));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
        }

        public IGearBoxState Create()
        {
            switch (_gearBoxStateValueProvider.GearBoxStateValue)
            {
                case GearBoxStateValue.DriveState:
                    return new DriveState(_driveModeFactory, _gearShifter);

                case GearBoxStateValue.ParkState:
                    return new ParkState();

                case GearBoxStateValue.ReverseState:
                    return new ReverseState();

                case GearBoxStateValue.NeutralState:
                    return new NeutralState();

                default:
                    throw new NotSupportedException();
            }
        }
    }
}