using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    // $G$ DSN-011 (-10) No use of factory class / internal constructors.
    internal class UserInterface
    {
        private static VehicleDatabase s_GarageVehicles = new VehicleDatabase();
        private string m_menuMessage;
        private string m_Input;
        private string[] m_PossibleChoices;

        // $G$ DSN-007 (-5) This method is too long, it should have been split into several methods.
        public void StartUpMenu()
        {
            bool isTimeToExit = false;
            int menuChoice = -1;

            while(isTimeToExit == false)
            {
                Console.Clear();
                m_menuMessage = string.Format(
    @"Welcome to the garage!
Please choose one of the options below.
enter a number of the choices below or enter 0 to quit
1.Enter a new vehicle into the garage
2.Browse a list of vehicles in the garage
3.Update vehicle status
4.Inflate air into wheels of a vehicle
5.Fill fuel tank of a vehicle
6.Charge an electric vehicle
7.Review a vehicle");
                Console.WriteLine(m_menuMessage);
                menuChoice = validIntegerInput(0, 7);
                if (menuChoice == 0)
                {
                    isTimeToExit = true;
                }

                try
                {
                    Console.Clear();
                    switch (menuChoice)
                    {
                        case 1:
                            bool isVehicleInGarage = false;
                            VehicleInGarage newVehicleInGarage = new VehicleInGarage();
                            Console.WriteLine("Please enter the license plate ID of the vehicle you would like to add");
                            m_Input = Console.ReadLine();
                            try
                            {
                                VehicleDatabase.s_VehicleDatabase.Add(m_Input, newVehicleInGarage);
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("There is already a vehicle with this ID in the garage it is now set to in repair");
                                VehicleDatabase.s_VehicleDatabase[m_Input].VehicleStatus = eVehicleStatusInGarage.UnderRepair;
                                Console.ReadLine();
                                isVehicleInGarage = true;
                            }

                            if (isVehicleInGarage == false)
                            {
                                addNewVehicle(m_Input);
                            }

                            break;
                        case 2:
                            browseListOfVehicles();
                            break;
                        case 3:
                            Console.WriteLine("please enter the license plate ID of the vehicle you want to update status");
                            m_Input = Console.ReadLine();
                            Console.WriteLine("please enter the new vehicle status");
                            m_PossibleChoices = Enum.GetNames(typeof(eVehicleStatusInGarage));
                            eVehicleStatusInGarage newVehicleStatus =
                                (eVehicleStatusInGarage)Enum.Parse(typeof(eVehicleStatusInGarage), choiceOutOfOptions(m_PossibleChoices));
                            updateVehicleStatus(m_Input, newVehicleStatus);
                            break;
                        case 4:
                            Console.WriteLine("please enter the license plate ID of the vehicle you would like to inflate wheel's air");
                            m_Input = Console.ReadLine();
                            inflateWheelsOfVehicle(m_Input);
                            break;
                        case 5:
                            float fuelAmount;
                            Console.WriteLine("please enter the license plate ID of the vehicle you would like fuel");
                            m_Input = Console.ReadLine();
                            Console.WriteLine("please enter the the amount of fuel(in liters)");
                            fuelAmount = float.Parse(Console.ReadLine());
                            m_PossibleChoices = Enum.GetNames(typeof(eFuelType));
                            eFuelType fuelTypeChoice =
                                (eFuelType)Enum.Parse(typeof(eFuelType), choiceOutOfOptions(m_PossibleChoices));
                            fuelVehicle(m_Input, fuelAmount, fuelTypeChoice);
                            break;
                        case 6:
                            float minutesAmountToFloat;
                            Console.WriteLine("please enter the license plate ID of the vehicle you would like to recharge.");
                            m_Input = Console.ReadLine();
                            Console.WriteLine("please enter the amount of minutes you want to charge");
                            minutesAmountToFloat = float.Parse(Console.ReadLine());
                            chargeElectricVehicle(m_Input, minutesAmountToFloat);
                            break;
                        case 7:
                            Console.WriteLine("please enter license plate ID of the vehicle you would like to review.");
                            m_Input = Console.ReadLine();
                            reviewVehicle(m_Input);
                            break;
                    }
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("not a valid option press enter to return to main menu");
                    Console.ReadLine();
                }
                catch(FormatException)
                {
                    Console.WriteLine("syntext error press enter to return to main menu");
                    Console.ReadLine();
                }
                catch(KeyNotFoundException)
                {
                    Console.WriteLine("you entered a key not in the system press enter to return to main menu");
                    Console.ReadLine();
                }
            }
        }
        
        private void addNewVehicle(string io_VehicleID)
        {
            bool isVehicleInserted = false;
            Console.WriteLine("Please enter the vehicle's owner full name");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Please enter the vehicle's owner phone number");
            string ownerPhone = Console.ReadLine();
            VehicleDatabase.s_VehicleDatabase[io_VehicleID] = new VehicleInGarage(ownerName, ownerPhone);
            while(isVehicleInserted == false)
            {
                try
                {
                    addInformationToNewVehicle(io_VehicleID, ref isVehicleInserted);
                }
                catch (FormatException)
                {
                    Console.WriteLine("syntex error, restarting vehicle insertion");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("not a valid option, restarting vehicle insertion");
                }
            } 
        }

        // $G$ DSN-001 (-10) No UI Separation - the actual garage logic is located in a UI interactive class.
        // $G$ DSN-001 (-20) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!
        // $G$ DSN-002 (-5) Creating concrete vehicle types should not be allowed outside of the object model project.
        // $G$ CSS-013 (-5) Bad variable name (should be in the form of i_PascalCase).
        private void addInformationToNewVehicle(string i_VehicleID, ref bool i_IsVehicleInserted)
        {
            Console.WriteLine("Please enter the vehicle's model name");
            string modelName = Console.ReadLine();
            Console.WriteLine("Please enter the vehicle's ramaning energy precentage");
            float energyPrecentage = validFloatInput(0f, 100f);
            Console.WriteLine("Please enter the vehicle's wheels manufacturer");
            string wheelsManufacturer = Console.ReadLine();
            string[] possibleChoicesOfVehicleTypes = VehicleDatabase.s_PossibleVehicleTypes;
            eVehicleType newVehicleType =
                (eVehicleType)Enum.Parse(typeof(eVehicleType), choiceOutOfOptions(possibleChoicesOfVehicleTypes));

            switch (newVehicleType)
            {
                case eVehicleType.Car:
                    {
                        Console.WriteLine("Please enter the car's wheels current air pressure");
                        float carWheelsCurrentAirPressure = validFloatInput(0f, 33f);
                        Console.WriteLine("Please enter the car's number of doors");
                        int numberOfDoors = validIntegerInput(2, 5);
                        m_PossibleChoices = Enum.GetNames(typeof(eCarColors));
                        eCarColors carColorChoice =
                                (eCarColors)Enum.Parse(typeof(eCarColors), choiceOutOfOptions(m_PossibleChoices));
                        Console.WriteLine("Please enter engine type");
                        m_PossibleChoices = Enum.GetNames(typeof(eFuelOrElectric));
                        eFuelOrElectric carEngineTypeChoice =
                                (eFuelOrElectric)Enum.Parse(typeof(eFuelOrElectric), choiceOutOfOptions(m_PossibleChoices));
                        float currentEnergyLeftCar;
                        if (carEngineTypeChoice == eFuelOrElectric.Fuel)
                        {
                            Console.WriteLine("Please enter current fuel amount(in liters)");
                            currentEnergyLeftCar = validFloatInput(0f, 42f);
                        }
                        else
                        {
                            Console.WriteLine("Please current time left in battery(in minutes)");
                            currentEnergyLeftCar = validFloatInput(0f, 150f);
                        }

                        VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle = new Car(
                            modelName,
                            i_VehicleID,
                            wheelsManufacturer,
                            carWheelsCurrentAirPressure,
                            currentEnergyLeftCar,
                            carEngineTypeChoice,
                            carColorChoice,
                            numberOfDoors);
                        Console.WriteLine("All info of the car successfully saved");
                        Console.ReadLine();
                        i_IsVehicleInserted = true;
                        break;
                    }

                case eVehicleType.Motorcycle:
                    Console.WriteLine("Please enter the vehicle's wheels current air pressure");
                    float MotorcycleWheelsCurrentAirPressure = validFloatInput(1, 30);
                    Console.WriteLine("Please enter the motorcycle's licence type");
                    m_PossibleChoices = Enum.GetNames(typeof(eLicenseType));
                    eLicenseType MotorcycleLicenseTypeChoice =
                                (eLicenseType)Enum.Parse(typeof(eLicenseType), choiceOutOfOptions(m_PossibleChoices));
                    Console.WriteLine("Please enter the motorcycle's engine capacity");
                    int engineCapacity = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the motorcycle's engine type");
                    m_PossibleChoices = Enum.GetNames(typeof(eFuelOrElectric));
                    eFuelOrElectric MotorcycleEngineTypeChoice =
                                (eFuelOrElectric)Enum.Parse(typeof(eFuelOrElectric), choiceOutOfOptions(m_PossibleChoices));
                    float currentEnergyLeftMotorcycle;
                    if (MotorcycleEngineTypeChoice == eFuelOrElectric.Fuel)
                    {
                        Console.WriteLine("Please enter current fuel amount(in liters)");
                        currentEnergyLeftMotorcycle = validFloatInput(0f, 5.5f);
                    }
                    else
                    {
                        Console.WriteLine("Please current time left in battery(in minutes)");
                        currentEnergyLeftMotorcycle = validFloatInput(0f, 162f);
                    }

                    VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle = new Motorcycle(
                        modelName,
                        i_VehicleID,
                        wheelsManufacturer,
                        MotorcycleWheelsCurrentAirPressure,
                        currentEnergyLeftMotorcycle,
                        MotorcycleEngineTypeChoice,
                        MotorcycleLicenseTypeChoice,
                        engineCapacity);
                    Console.WriteLine("All info of the motorcycle successfully saved");
                    Console.ReadLine();
                    i_IsVehicleInserted = true;
                    break;
                case eVehicleType.Truck:
                    Console.WriteLine("Please enter the truck's wheels current air pressure");
                    float truckWheelsCurrentAirPressure = validFloatInput(1, 32);
                    Console.WriteLine("Please enter current fuel amount(in liters)");
                    float currentEnergyLeftTruck = validFloatInput(0f, 135f);
                    Console.WriteLine("Does the truck carry hazardous substances? enter 1 for yes and 0 for no");
                    bool isCarryingHazardousSubstances;
                    int hazardInput = validIntegerInput(0, 1);
                    isCarryingHazardousSubstances = hazardInput == 1 ? true : false;
                    Console.WriteLine("Please enter the truck's max weight capacity");
                    float maxWeightCapacity = float.Parse(Console.ReadLine());
                    VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle = new Truck(
                        modelName,
                        i_VehicleID,
                        wheelsManufacturer,
                        truckWheelsCurrentAirPressure,
                        currentEnergyLeftTruck,
                        maxWeightCapacity,
                        isCarryingHazardousSubstances);
                    Console.WriteLine("All info of the truck successfully saved");
                    Console.ReadLine();
                    i_IsVehicleInserted = true;
                    break;
            }
        }

        public void browseListOfVehicles()
        {
            string vehicleFilter;
            int filterChoice = 0;

            Console.Clear();
            vehicleFilter = string.Format(
@"What vehicles would you like to browse, type a number
1.All vehicles
2.In repair
3.Fixed
4.Paid For
");
            Console.WriteLine(vehicleFilter);
            filterChoice = validIntegerInput(1, 4);
            
            if (filterChoice == 1)
            {
                PrintAllVehiclesID();
            }
            else
            {
                PrintFilteredVehiclesInGarage((eVehicleStatusInGarage)filterChoice);
            }
        }

        public void PrintAllVehiclesID()
        {
            foreach (KeyValuePair<string, VehicleInGarage> ID in VehicleDatabase.s_VehicleDatabase)
            {
                Console.WriteLine(ID.Key);
            }

            Console.WriteLine("Press enter to go back to main menu");
            Console.ReadLine();
        }

        public void PrintFilteredVehiclesInGarage(eVehicleStatusInGarage i_FilterChoice)
        {
            foreach (KeyValuePair<string, VehicleInGarage> ID in VehicleDatabase.s_VehicleDatabase)
            {
                if (ID.Value.VehicleStatus == i_FilterChoice)
                {
                    Console.WriteLine(ID.Key);
                }
            }

            Console.WriteLine("Press enter to go back to min menu");
            Console.ReadLine();
        }

        // $G$ CSS-999 (-3) Public methods should start with an Uppercase letter.
        public void updateVehicleStatus(string i_VehicleID, eVehicleStatusInGarage i_NewStatus)
        {
            VehicleDatabase.s_VehicleDatabase[i_VehicleID].VehicleStatus = i_NewStatus;
        }

        private void inflateWheelsOfVehicle(string i_VehicleID)
        {
            VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle.AddingAirToWheel();
        }

        private void fuelVehicle(string i_VehicleID, float i_FillAmount, eFuelType i_FuelType)
        {
            try
            {
                VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle.EngineOfVehicle.RefillEnergyInVehicle(i_FillAmount, i_FuelType);
            }
            catch(ValueOutOfRangeException)
            {
            }           
        }

        private void chargeElectricVehicle(string i_VehicleID, float i_MinutesAmount)
        {
            VehicleDatabase.s_VehicleDatabase[i_VehicleID].m_Vehicle.EngineOfVehicle.RefillEnergyInVehicle(i_MinutesAmount);
        }

        private void reviewVehicle(string i_VehicleID)
        {
            Console.WriteLine(VehicleDatabase.s_VehicleDatabase[i_VehicleID].ToString());
            Console.ReadLine();
        }

        private int validIntegerInput(int i_Min, int i_Max)
        {
            bool isValidInput = false;
            int menuChoice = 0;

            while (isValidInput == false)
            {
                try
                {
                    m_Input = Console.ReadLine();

                    menuChoice = int.Parse(m_Input);
                    
                    if (menuChoice < i_Min || menuChoice > i_Max)
                    {
                        throw new ValueOutOfRangeException(i_Min, i_Max);
                    }

                    isValidInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("syntex error, try again");
                }
                catch (ValueOutOfRangeException)
                {
                }
            }
            
            return menuChoice;
        }

        private float validFloatInput(float i_Min, float i_Max)
        {
            bool isValidInput = false;
            float menuChoice = 0;

            while (isValidInput == false)
            {
                try
                {
                    m_Input = Console.ReadLine();

                    menuChoice = float.Parse(m_Input);

                    if (menuChoice < i_Min || menuChoice > i_Max)
                    {
                        throw new ValueOutOfRangeException(i_Min, i_Max);
                    }

                    isValidInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("syntex error, try again");
                }
                catch (ValueOutOfRangeException)
                {
                }
            }

            return menuChoice;
        }

        private string choiceOutOfOptions(string[] i_options)
        {
            string choice;
            
            Console.WriteLine("Choose one of these options, type in your choice");
            foreach(string option in i_options)
            {
                Console.WriteLine(option);
            }

            choice = Console.ReadLine();
            // $G$ CSS-999 (-2) Missing blank line.
            return choice;
        }
    }
}