using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryLife)
        {
            m_MaxResourceEnergy = i_MaxBatteryLife;
        }

        public override void RefillEnergyInVehicle(float i_HoursToCharge)
        {
            try
            {
                if (m_MaxResourceEnergy < m_CurrentResourceEnergy + i_HoursToCharge)
                {
                    throw new ValueOutOfRangeException(0, m_MaxResourceEnergy - m_CurrentResourceEnergy);
                }
                else
                {
                    m_CurrentResourceEnergy += i_HoursToCharge;
                }
            }
            catch
            {
            }        
        }

        public override void RefillEnergyInVehicle(float i_HoursToCharge, eFuelType i_FuelType)
        {
            throw new ArgumentException();
        }

        public override string ToString()
        {
            string electricEngineInfo =
                string.Format(
@"Current amount of minuts left in battery: {0} minutes", m_CurrentResourceEnergy);

            return electricEngineInfo;
        }
    }
}