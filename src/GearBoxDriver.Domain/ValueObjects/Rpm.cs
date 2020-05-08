using System;
using System.Collections.Generic;

namespace GearboxDriver.Domain.ValueObjects
{
    public class Rpm : ValueObject
    {
        private readonly double _rpm;

        public Rpm(double rpm)
        {
            if(rpm < 0.0d)
                throw new ArgumentOutOfRangeException(nameof(rpm), "Rpm cannot be lesser than 0");

            _rpm = rpm;
        }

        public static bool operator > (Rpm a, Rpm b) => a._rpm > b._rpm;
        public static bool operator < (Rpm a, Rpm b) => a._rpm < b._rpm;
        
        public static Rpm operator *(Rpm rpm, double factor) => new Rpm(rpm._rpm * factor);
        public static Rpm operator *(double factor, Rpm rpm) => new Rpm(factor * rpm._rpm);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _rpm;
        }
    }
}