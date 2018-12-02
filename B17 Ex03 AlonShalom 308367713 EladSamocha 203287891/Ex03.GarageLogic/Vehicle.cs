using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseId;
        protected float m_EnergyPercentage;
        public List<Wheels> m_VehicleWheels;
        protected Engine m_EngineOfVehicle;
        protected float m_MaxWheelAirPressure;
        protected string m_OwnerName;
        protected string m_OwnerPhone;

        public Vehicle()
        {
            m_VehicleWheels = new List<Wheels>();
        }

        public Engine EngineOfVehicle
        {
            get { return m_EngineOfVehicle; }
            set { m_EngineOfVehicle = value; }
        }

        protected void WheelsSystem(string i_Manufacturer, int i_NumberOfWheels, float i_CurrentWheelAirPressure)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheels newWheel = new Wheels(i_Manufacturer, i_CurrentWheelAirPressure, m_MaxWheelAirPressure);
                m_VehicleWheels.Add(newWheel);
            }
        }

        public void AddingAirToWheel()
        {
            foreach (Wheels currentWheel in m_VehicleWheels)
            {
                currentWheel.InflatingVehicleWheels();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            vehicleInfo.AppendFormat(
@"Vehicle ID: {0}
Model name: {1}
",
m_LicenseId,
m_ModelName);
            vehicleInfo.AppendLine(m_EngineOfVehicle.ToString());
            vehicleInfo.AppendLine(m_VehicleWheels[0].ToString());
            return vehicleInfo.ToString();
        }
    }
}