using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public FuelEngine(float i_MaxFuelQuantityInLiters, eFuelType i_FuelType)
        {
            r_FuelType = i_FuelType;
            m_MaxResourceEnergy = i_MaxFuelQuantityInLiters;
        }

        public override void RefillEnergyInVehicle(float i_FuelAmountToRefill)
        {
            throw new ArgumentException();
        }

        public override void RefillEnergyInVehicle(float i_FuelAmountToRefill, eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException();
            }
            // $G$ DSN-001 (-3) Code duplication. except in Fuel type, Fuel and Electric Energy Sources are identical.
            if (m_MaxResourceEnergy >= m_CurrentResourceEnergy + i_FuelAmountToRefill)
            {
                m_CurrentResourceEnergy += i_FuelAmountToRefill;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxResourceEnergy - m_CurrentResourceEnergy);
            }
        }

        public override string ToString()
        {
            string fuelEngineInfo =
                string.Format(
@"Fuel Type: {0}
Current amount of fuel in tank: {1} Liters",
r_FuelType,
m_CurrentResourceEnergy);

            return fuelEngineInfo;
        }
    }
}