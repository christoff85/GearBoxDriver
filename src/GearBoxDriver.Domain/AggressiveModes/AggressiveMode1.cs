using System;
using GearboxDriver.Domain.DriveModes;

namespace GearboxDriver.Domain.AggressiveModes
{
    public class AggressiveMode1 : IAggressiveMode
    {
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public AggressiveMode1(IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public void Accelerate(GearShiftBoundaries gearShiftBoundaries)
        {
            var currentRpm = _externalSystems.GetCurrentRpm();
            if (currentRpm < gearShiftBoundaries.DownshiftBoundary)
            {
                _gearShifter.Downshift();
                return;
            }

            if (currentRpm > gearShiftBoundaries.UpshiftBoundary)
            {
                _gearShifter.Upshift();
            }
        }
    }
}