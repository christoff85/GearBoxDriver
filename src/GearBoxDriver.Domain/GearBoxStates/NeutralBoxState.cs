﻿using GearboxDriver.Domain.ValueObjects;

namespace GearboxDriver.Domain.GearBoxStates
{
    public class NeutralState : IGearBoxState
    {
        public void Accelerate(Threshold threshold)
        {
            // do nothing
        }

        public void ManualUpshift()
        {
            // do nothing
        }

        public void ManualDownshift()
        {
            // do nothing
        }
    }
}