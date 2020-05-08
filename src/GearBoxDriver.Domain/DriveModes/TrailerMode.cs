using System;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public class TrailerMode : IDriveMode
    {
        private readonly IDriveMode _baseMode;
        private readonly IGearShifter _gearShifter;
        private readonly IExternalSystems _externalSystems;

        public TrailerMode(IDriveMode baseMode, IGearShifter gearShifter, IExternalSystems externalSystems)
        {
            _baseMode = baseMode ?? throw new ArgumentNullException(nameof(baseMode));
            _gearShifter = gearShifter ?? throw new ArgumentNullException(nameof(gearShifter));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public void Accelerate(Threshold threshold)
        {
            if (_externalSystems.IsDrivingDown())
            {
                _gearShifter.Downshift();
            }
            
            _baseMode.Accelerate(threshold);
        }
    }
}