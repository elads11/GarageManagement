using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private string m_OwnersName;
        private string m_OwnersPhone;
        private eVehicleStatusInGarage m_VehicleStatus;
        public Vehicle m_Vehicle;

        public VehicleInGarage()
        {
        }

        public VehicleInGarage(string i_OwnersName, string i_OwnersPhone)
        {
            m_OwnersName = i_OwnersName;
            m_OwnersPhone = i_OwnersPhone;
            m_VehicleStatus = eVehicleStatusInGarage.UnderRepair;          
        }

        public eVehicleStatusInGarage VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public override string ToString()
        {
            StringBuilder VehicleInformation = new StringBuilder();
            VehicleInformation.AppendFormat("Owner's name {0}", m_OwnersName);
            VehicleInformation.AppendLine();
            VehicleInformation.AppendFormat("Vehicle status: {0}", m_VehicleStatus);
            VehicleInformation.AppendLine();
            VehicleInformation.AppendLine(m_Vehicle.ToString());
            return VehicleInformation.ToString();
                /*string.Format("Vehicle's owner name: {0}\n" +
                "Vehicle's owner phone: {1}\n" +
                "Vehicle's status: {3}",
                m_OwnersName, m_OwnersPhone, m_VehicleStatus);  */          
        }
    }
}