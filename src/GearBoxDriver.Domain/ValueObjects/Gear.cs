using System;
using System.Collections.Generic;

namespace GearboxDriver.Domain.ValueObjects
{
    public class Gear : ValueObject
    {
        private static readonly int ReverseGear = -1;
        private static readonly int NeutralGear = 0;
        private static readonly int FirstGear = 1;

        public int CurrentGear { get; }
        private readonly int _maxDrive;

        public Gear(int currentGear, int maxDrive)
        {
            if(currentGear > maxDrive)
                throw new ArgumentOutOfRangeException(nameof(currentGear), "Current gear cannot be greater than max drive");

            if(currentGear < ReverseGear)
                throw new ArgumentOutOfRangeException(nameof(currentGear), "Current gear cannot be lesser than Reverse");
            
            CurrentGear = currentGear;
            _maxDrive = maxDrive;
        }

        public Gear Upshift()
        {
            if (CurrentGear == ReverseGear || CurrentGear == NeutralGear || CurrentGear == _maxDrive)
                return this;

            return new Gear(CurrentGear + 1, _maxDrive);
        }

        public Gear Downshift()
        {
            if (CurrentGear == ReverseGear || CurrentGear == NeutralGear || CurrentGear == FirstGear)
                return this;

            return new Gear(CurrentGear - 1, _maxDrive);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrentGear;
            yield return _maxDrive;
        }
    }
}
