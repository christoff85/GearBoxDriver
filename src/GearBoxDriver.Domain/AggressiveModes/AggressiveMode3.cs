using System;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.AggressiveModes
{
    public class AggressiveMode3 : IAggressiveMode
    {
        private readonly RpmShiftFactor _rpmUpshiftFactor;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public AggressiveMode3(RpmShiftFactor rpmUpshiftFactor, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _rpmUpshiftFactor = rpmUpshiftFactor ?? throw new ArgumentNullException(nameof(rpmUpshiftFactor));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public void Accelerate(GearShiftBoundaries gearShiftBoundaries)
        {
            var currentRpm = _externalSystems.GetCurrentRpm();
            if (currentRpm < gearShiftBoundaries.DownshiftBoundary)
            {
                _gearShifter.Downshift();
            }

            else if (currentRpm > gearShiftBoundaries.UpshiftBoundary * _rpmUpshiftFactor)
            {
                _gearShifter.Upshift();
                _externalSystems.MakeSound(new SoundVolume(40));
            }
        }
    }
}