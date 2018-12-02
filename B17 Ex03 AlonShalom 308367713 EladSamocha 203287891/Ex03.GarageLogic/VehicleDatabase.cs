using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleDatabase
    {
        // $G$ DSN-999 (-7) Why static? it's not object-oriented.
        // $G$ DSN-006 (-3) Fields should be private.
        public static Dictionary<string, VehicleInGarage> s_VehicleDatabase = new Dictionary<string, VehicleInGarage>();
        public static string[] s_PossibleVehicleTypes = { "Car", "Motorcycle", "Truck" };
    }
}