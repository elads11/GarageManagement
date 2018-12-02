using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 12;
        private const float k_MaxWheelAirPressure = 32f;
        private const eFuelType k_TruckFuelType = eFuelType.Octan96;
        private const float k_MaxFuelAmount = 135f;
        private float m_MaxWeightCapacity;
        private bool m_IsCarryingHazardousSubstances;

        public Truck(
            string i_ModelName,
            string i_LicenseId,
            string o_WheelsManufacturer,
            float o_WheelsCurrentAirPressure,
            float o_CurrentEnergyQuantity,
            float i_MaxWeightCapacity,
            bool i_IsCarryingHazardousSubstances)
        {
            m_ModelName = i_ModelName;
            m_LicenseId = i_LicenseId;
            m_MaxWheelAirPressure = k_MaxWheelAirPressure;
            m_MaxWeightCapacity = i_MaxWeightCapacity;
            m_IsCarryingHazardousSubstances = i_IsCarryingHazardousSubstances;
            m_EngineOfVehicle = new FuelEngine(k_MaxFuelAmount, k_TruckFuelType);

            WheelsSystem(o_WheelsManufacturer, k_NumberOfWheels, o_WheelsCurrentAirPressure);
            m_EngineOfVehicle.SetCurrentEnergy(o_CurrentEnergyQuantity);
        }

        public override string ToString()
        {
            StringBuilder truckInfo = new StringBuilder(base.ToString());

            truckInfo.AppendFormat("Contain hazardous substances: {0}", m_IsCarryingHazardousSubstances);
            truckInfo.AppendLine();
            truckInfo.AppendFormat("Max weight capacity: {0}", m_MaxWeightCapacity);
            return truckInfo.ToString();
        }        
    }
}