using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_MaxWheelAirPressure = 33;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxBatteryLife = 162f;
        private const eFuelType k_MotorcycleFuelType = eFuelType.Octan95;
        private const float k_MaxFuelAmount = 5.5f;
        private readonly eFuelOrElectric r_FuelOrElectric;
        private readonly int r_EngineCapacity;
        private readonly eLicenseType r_LicenseType;

        // $G$ DSN-008 (-5) A constructor should not be interactive. It should be used for initialization only.
        public Motorcycle(
            string i_ModelName,
            string i_LicenseId,
            string o_WheelsManufacturer,
            float o_WheelsCurrentAirPressure,
            float o_CurrentEnergyQuantity,
            eFuelOrElectric i_FuelOrElectric,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
        {
            m_ModelName = i_ModelName;
            m_LicenseId = i_LicenseId;
            m_MaxWheelAirPressure = k_MaxWheelAirPressure;
            r_FuelOrElectric = i_FuelOrElectric;
            r_LicenseType = i_LicenseType;
            r_EngineCapacity = i_EngineCapacity;

            WheelsSystem(o_WheelsManufacturer, k_NumberOfWheels, o_WheelsCurrentAirPressure);

            // $G$ DSN-001 (-5) These code belong in the vehicle factory, as it is the only component who should know the concrete types.
            if (r_FuelOrElectric == eFuelOrElectric.Fuel)
            {
                m_EngineOfVehicle = new FuelEngine(k_MaxFuelAmount, k_MotorcycleFuelType);
            }
            else
            {
                m_EngineOfVehicle = new ElectricEngine(k_MaxBatteryLife);
            }

            m_EngineOfVehicle.SetCurrentEnergy(o_CurrentEnergyQuantity);
        }

        public override string ToString()
        {
            StringBuilder motorCycleInfo = new StringBuilder(base.ToString());

            motorCycleInfo.AppendFormat("Licence type: {0}", r_LicenseType);
            motorCycleInfo.AppendLine();
            motorCycleInfo.AppendFormat("Engine capacity: {0}", r_EngineCapacity);

            return motorCycleInfo.ToString();
        }
    }
}