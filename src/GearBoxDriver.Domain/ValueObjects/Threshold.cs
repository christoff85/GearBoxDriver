using System;
using System.Collections.Generic;

namespace GearboxDriver.Domain.ValueObjects
{
    public class Threshold : ValueObject
    {
        private static readonly double KickDownBoundary = 0.5d;
        private static readonly double StrongKickDownBoundary = 0.7d;

        private readonly double _threshold;

        public Threshold(double threshold)
        {
            if(threshold < 0)
                throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold cannot be lesser than 0");

            _threshold = threshold;
        }

        public bool IsKickdown()
        {
            return _threshold > KickDownBoundary;
        }

        public bool IsStrongKickDown()
        {
            return _threshold > StrongKickDownBoundary;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _threshold;
        }
    }
}
