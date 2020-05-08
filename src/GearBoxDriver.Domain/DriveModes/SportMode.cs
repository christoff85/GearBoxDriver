using System;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public class SportMode : IDriveMode
    {
        private readonly GearShiftBoundaries _gearShiftBoundaries;
        private readonly IGearShifter _gearShifter;
        private readonly IAggressiveModeFactory _aggressiveModeFactory;

        public SportMode(GearShiftBoundaries gearShiftBoundaries, IGearShifter gearShifter, IAggressiveModeFactory aggressiveModeFactory)
        {
            _gearShiftBoundaries = gearShiftBoundaries ?? throw new ArgumentNullException(nameof(gearShiftBoundaries));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _aggressiveModeFactory = aggressiveModeFactory ?? throw new ArgumentNullException(nameof(aggressiveModeFactory));
        }

        public void Accelerate(Threshold threshold)
        {
            if (threshold.IsKickdown())
            {
                KickdownShift(threshold);
                return;
            }

            CurrentRpmShift();
        }

        private void KickdownShift(Threshold threshold)
        {
            _gearShifter.Downshift();

            if (threshold.IsStrongKickDown())
            {
                _gearShifter.Downshift();
            }
        }

        private void CurrentRpmShift()
        {
            var aggresiveMode = _aggressiveModeFactory.Create();
            aggresiveMode.Accelerate(_gearShiftBoundaries);
        }
    }
}