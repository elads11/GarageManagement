namespace Ex03.GarageLogic
{
    public class Wheels
    {
        private readonly float r_ManufacturerMaxAirPressure;
        private readonly string r_Manufacturer;
        private float m_CurrentWheelAirPressure;

        public Wheels(string i_Manufacturer, float i_CurrentWheelAirPressure, float i_ManufacturerMaxAirPressure)
        {
            r_Manufacturer = i_Manufacturer;
            m_CurrentWheelAirPressure = i_CurrentWheelAirPressure;
            r_ManufacturerMaxAirPressure = i_ManufacturerMaxAirPressure;
        }

        public void InflatingVehicleWheels()
        {
            m_CurrentWheelAirPressure = r_ManufacturerMaxAirPressure;
        }

        public override string ToString()
        {
            string wheelsInfo = string.Format(
@"Wheels manufacturer : {0}
Wheels current air pressure: {1}",
r_Manufacturer,
m_CurrentWheelAirPressure);

            return wheelsInfo;
        }
    }
}