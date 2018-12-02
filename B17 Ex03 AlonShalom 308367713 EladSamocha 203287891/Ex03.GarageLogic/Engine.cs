namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_MaxResourceEnergy;
        protected float m_CurrentResourceEnergy;

        public Engine()
        {
        }

        internal void SetCurrentEnergy(float i_EnergyPrecent)
        {
            m_CurrentResourceEnergy = (i_EnergyPrecent / 100f) * m_MaxResourceEnergy;
        }

        public abstract void RefillEnergyInVehicle(float i_EnergyToRefill);

        public abstract void RefillEnergyInVehicle(float i_EnergyToRefill, eFuelType i_FuelType);
    }
}
