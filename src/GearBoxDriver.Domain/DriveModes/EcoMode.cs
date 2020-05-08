using System;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public class EcoMode : IDriveMode
    {
        private readonly GearShiftBoundaries _gearShiftBoundaries;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public EcoMode(GearShiftBoundaries gearShiftBoundaries, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _gearShiftBoundaries = gearShiftBoundaries ?? throw new ArgumentNullException(nameof(gearShiftBoundaries));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public void Accelerate(Threshold threshold)
        {
            var currentRpm = _externalSystems.GetCurrentRpm();
            if (currentRpm > _gearShiftBoundaries.UpshiftBoundary)
            {
                _gearShifter.Upshift();
                return;
            }

            if (currentRpm < _gearShiftBoundaries.DownshiftBoundary)
            {
                _gearShifter.Downshift();
            }
        }
    }
}