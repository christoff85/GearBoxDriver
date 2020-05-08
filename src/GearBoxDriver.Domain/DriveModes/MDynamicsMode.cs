using System;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public class MDynamicsMode : IDriveMode
    {
        private readonly AngularSpeed _cutOffAngularSpeed;
        private readonly IDriveMode _baseMode;
        private readonly IExternalSystems _externalSystems;

        public MDynamicsMode(AngularSpeed cutOffAngularSpeed, IDriveMode baseMode, IExternalSystems externalSystems)
        {
            _cutOffAngularSpeed = cutOffAngularSpeed ?? throw new ArgumentNullException(nameof(cutOffAngularSpeed));
            _baseMode = baseMode ?? throw new ArgumentNullException(nameof(baseMode));
            _externalSystems = externalSystems ?? throw new ArgumentNullException(nameof(externalSystems));
        }

        public void Accelerate(Threshold threshold)
        {
            var currentAngularSpeed = _externalSystems.GetAngularSpeed();

            if (currentAngularSpeed > _cutOffAngularSpeed)
                return;

            _baseMode.Accelerate(threshold);
        }
    }
}