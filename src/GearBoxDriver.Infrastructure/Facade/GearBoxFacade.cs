using System;
using GearboxDriver.Domain;
using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.ValueObjects;
using GearBoxDriver.Infrastructure.External;

namespace GearBoxDriver.Infrastructure.Facade
{
    public class GearBoxFacade : IGearBox
    {
        private readonly Gearbox _gearBox;

        public GearBoxFacade(Gearbox gearBox)
        {
            _gearBox = gearBox ?? throw new ArgumentNullException(nameof(gearBox));
        }

        public Gear GetCurrentGear()
        {
            return new Gear((int)_gearBox.GetCurrentGear(), _gearBox.GetMaxDrive());
        }

        public void SetCurrentGear(Gear gear)
        {
            _gearBox.SetCurrentGear(gear.CurrentGear);
        }

        public void SetGearBoxState(GearBoxStateValue gearBoxStateValue)
        {
            switch (gearBoxStateValue)
            {
                case GearBoxStateValue.DriveState:
                    SetDriveState();
                    break;

                case GearBoxStateValue.ParkState:
                    SetParkState();
                    break;

                case GearBoxStateValue.ReverseState:
                    SetReverseState();
                    break;

                case GearBoxStateValue.NeutralState:
                    SetNeutralState();
                    break;
            }
        }

        private void SetDriveState()
        {
            _gearBox.SetGearBoxCurrentParams(new object[] { 1, 1 });
        }

        private void SetParkState()
        {
            _gearBox.SetGearBoxCurrentParams(new object[] { 2, 0 });
        }

        private void SetReverseState()
        {
            _gearBox.SetGearBoxCurrentParams(new object[] { 3, -1 });
        }

        private void SetNeutralState()
        {
            _gearBox.SetGearBoxCurrentParams(new object[] { 4, 0 });
        }
    }
}
