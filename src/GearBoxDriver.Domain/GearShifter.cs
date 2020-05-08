using System;

namespace GearboxDriver.Domain
{
    public class GearShifter : IGearShifter
    {
        private readonly IGearBox _gearBox;

        public GearShifter(IGearBox gearBox)
        {
            _gearBox = gearBox ?? throw new ArgumentNullException(nameof(gearBox));
        }

        public void Upshift()
        {
            var currentGear = _gearBox.GetCurrentGear();
            var nextGear = currentGear.Upshift();
            _gearBox.SetCurrentGear(nextGear);
        }

        public void Downshift()
        {
            var currentGear = _gearBox.GetCurrentGear();
            var nextGear = currentGear.Downshift();
            _gearBox.SetCurrentGear(nextGear);
        }
    }
}