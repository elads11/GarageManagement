using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const eFuelType k_CarFuelType = eFuelType.Octan98;
        private const float k_MaxFuelAmount = 42;
        private const float k_MaxWheelAirPressure = 30;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxBatteryLife = 150f;
        private readonly eFuelOrElectric r_FuelOrElectric;
        private readonly eCarColors r_CarColor;
        private readonly int r_NumberOfDoors;

        // $G$ DSN-999 (-5) Creation of vehicle entities should not be allowed outside of this project (constructors should be marked as internal).
        public Car(
            string i_ModelName,
            string i_LicenseId,
            string o_WheelsManufacturer,
            float o_WheelsCurrentAirPressure,
            float o_CurrentEnergyQuantity,
            eFuelOrElectric i_FuelOrElectric,
            eCarColors i_CarColor,
            int i_NumberOfDoors)
        {
            m_ModelName = i_ModelName;
            m_LicenseId = i_LicenseId;
            m_MaxWheelAirPressure = k_MaxWheelAirPressure;
            r_FuelOrElectric = i_FuelOrElectric;
            r_CarColor = i_CarColor;
            r_NumberOfDoors = i_NumberOfDoors;

            WheelsSystem(o_WheelsManufacturer, k_NumberOfWheels, o_WheelsCurrentAirPressure);
            if (r_FuelOrElectric == eFuelOrElectric.Fuel)
            {
                m_EngineOfVehicle = new FuelEngine(k_MaxFuelAmount, k_CarFuelType);
            }
            else
            {
                m_EngineOfVehicle = new ElectricEngine(k_MaxBatteryLife);
            }

            m_EngineOfVehicle.SetCurrentEnergy(o_CurrentEnergyQuantity);
        }

        public override string ToString()
        {
            StringBuilder carInfo = new StringBuilder(base.ToString());

            carInfo.AppendFormat("Car color: {0}", r_CarColor);
            carInfo.AppendLine();
            carInfo.AppendFormat("Number of doors: {0}", r_NumberOfDoors);

            return carInfo.ToString();
        }
    }
}