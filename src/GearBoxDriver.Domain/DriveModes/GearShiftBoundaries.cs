using System;
using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.DriveModes
{
    public class GearShiftBoundaries
    {
        public Rpm DownshiftBoundary { get; }
        public Rpm UpshiftBoundary { get; }

        public GearShiftBoundaries(Rpm downshiftBoundary, Rpm upshiftBoundary)
        {
            DownshiftBoundary = downshiftBoundary ?? throw new ArgumentNullException(nameof(downshiftBoundary));
            UpshiftBoundary = upshiftBoundary ?? throw new ArgumentNullException(nameof(upshiftBoundary));
        }
    }
}
