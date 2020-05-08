using System;
using System.Collections.Generic;

namespace GearboxDriver.Domain.ValueObjects
{
    public class AngularSpeed : ValueObject
    {
        private readonly double _angularSpeed;

        public AngularSpeed(double angularSpeed)
        {
            if (angularSpeed < 0.0d)
                throw new ArgumentOutOfRangeException(nameof(angularSpeed), "AngularSpeed cannot be lesser than 0");

            _angularSpeed = angularSpeed;
        }

        public static bool operator >(AngularSpeed a, AngularSpeed b) => a._angularSpeed > b._angularSpeed;
        public static bool operator <(AngularSpeed a, AngularSpeed b) => a._angularSpeed < b._angularSpeed;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _angularSpeed;
        }
    }
}