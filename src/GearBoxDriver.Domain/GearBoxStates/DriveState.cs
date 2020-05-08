using System;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.GearBoxStates
{
    public class DriveState : IGearBoxState
    {
        private readonly IDriveModeFactory _driveModeFactory;
        private readonly IGearShifter _gearShifter;

        public DriveState(IDriveModeFactory driveModeFactory, IGearShifter gearShifter)
        {
            _driveModeFactory = driveModeFactory ?? throw new ArgumentNullException(nameof(driveModeFactory));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
        }

        public void Accelerate(Threshold threshold)
        {
            var driveMode = _driveModeFactory.Create();
            driveMode.Accelerate(threshold);
        }

        public void ManualUpshift()
        {
            _gearShifter.Upshift();
        }

        public void ManualDownshift()
        {
            _gearShifter.Downshift();
        }
    }
}