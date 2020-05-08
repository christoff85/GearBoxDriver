using System;
using System.Collections.Generic;

namespace GearboxDriver.Domain.ValueObjects
{
    public class RpmShiftFactor : ValueObject
    {
        private readonly double _rpmShiftFactor;

        public RpmShiftFactor(double rpmShiftFactor)
        {
            if (rpmShiftFactor < 0.0d)
                throw new ArgumentOutOfRangeException(nameof(rpmShiftFactor), "RpmShiftFactor cannot be lesser than 0");

            _rpmShiftFactor = rpmShiftFactor;
        }

        public static Rpm operator *(Rpm rpm, RpmShiftFactor factor) => rpm * factor._rpmShiftFactor;
        public static Rpm operator *(RpmShiftFactor factor, Rpm rpm) => factor._rpmShiftFactor * rpm;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _rpmShiftFactor;
        }
    }
}