namespace GearBoxDriver.Infrastructure.External
{
    public class Gearbox
    {
        private int _maxDrive;
        private readonly object[] _gearBoxCurrentParams = new object[2]; //state, currentGear

        //state 1-Drive, 2-Park, 3-Reverse, 4-Neutral
        
        public object GetState() { return _gearBoxCurrentParams[0]; }

        public object GetCurrentGear() { return _gearBoxCurrentParams[1]; }
        public void SetCurrentGear(int currentGear) { _gearBoxCurrentParams[1] = currentGear; }


        public void SetGearBoxCurrentParams(object[] gearBoxCurrentParams)
        {
            if (gearBoxCurrentParams[0] != _gearBoxCurrentParams[0])
            {
                //zmienił się state
                _gearBoxCurrentParams[0] = gearBoxCurrentParams[0];
                int state = (int) gearBoxCurrentParams[0];

                if (state == 2)
                {
                    SetCurrentGear(0);
                }

                if (state == 3)
                {
                    SetCurrentGear(-1);
                }

                if (state == 4)
                {
                    SetCurrentGear(0);
                }

                SetCurrentGear((int) gearBoxCurrentParams[1]);
            }
            else
            {
                SetCurrentGear((int)gearBoxCurrentParams[1]);
            }
        }

        public int GetMaxDrive() { return _maxDrive; }
        public void SetMaxDrive(int maxDrive) { _maxDrive = maxDrive; }

    }
}