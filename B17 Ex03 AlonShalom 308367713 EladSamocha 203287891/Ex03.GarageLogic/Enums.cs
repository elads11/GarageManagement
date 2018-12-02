namespace Ex03.GarageLogic
{
    // $G$ DSN-009 (-5) You should separate different classes/enums into different files.
    public enum eCarColors
    {
        Yellow,
        White,
        Black,
        Blue
    }

    public enum eLicenseType
    {
        A,
        A2,
        B1,
        B2
    }

    public enum eFuelType
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }

    public enum eVehicleStatusInGarage
    {
        UnderRepair = 2,
        HasBeenRepaired = 3,
        Paid = 4
    }

    public enum eVehicleType
    {
        Car,
        Motorcycle,
        Truck
    }

    public enum eFuelOrElectric
    {
        Fuel,
        Electric
    }
}