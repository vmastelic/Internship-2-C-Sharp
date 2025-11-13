using System;

var users = new Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)>();
var trips = new Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)>();

users[1] = ("Marijan", "Mastelić", new DateTime(2002, 10, 10), new List<int>());
users[2] = ("Vatroslav", "Aastelić", new DateTime(2002, 10, 10), new List<int>());
users[3] = ("Lucija", "Pavić", new DateTime(2000, 10, 12), new List<int>());

trips[1] = (new DateTime(2025, 11, 11), 200, 18m, 1.49m, 18m * 1.49m);
trips[2] = (new DateTime(2025, 4, 29), 50, 5m, 1.3m, 5m * 1.3m);
trips[3] = (new DateTime(2023, 12, 15), 450, 38m, 1.6m, 38m * 1.60m);
trips[4] = (new DateTime(2022, 4, 1), 800, 63m, 1.2m, 63m * 1.2m);
trips[5] = (new DateTime(2025, 8, 7), 27, 4m, 1.5m, 4m * 1.5m);

users[1].tripsID.Add(1);
users[1].tripsID.Add(2);
users[2].tripsID.Add(3);
users[3].tripsID.Add(4);
users[3].tripsID.Add(5);

while (true)
{
    Console.Clear();
    Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
    Console.WriteLine("\n1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije\n");
    Console.Write("Odabir: ");

    var choice = Console.ReadLine();
    if (choice == "1") usersMenu();
    else if (choice == "2") tripsMenu();
    else if (choice == "0") break;
}
void usersMenu()
{
    while (true)
    {
        Console.WriteLine("\n1 - Unos novog korisnika");
        Console.WriteLine("2 - Brisanje korisnika");
        Console.WriteLine("3 - Uređivanje korisnika");
        Console.WriteLine("4 - Pregled svih korisnika");
        Console.WriteLine("0 - Povratak na glavni izbornik");
        Console.Write("\nOdabir: ");
        var userMenuChoice = Console.ReadLine();
        if (userMenuChoice == "1") addUser(users);
        else if (userMenuChoice == "2") deleteUserMenu();
        else if (userMenuChoice == "3") modifyUser(users);
        else if (userMenuChoice == "4") printUsersMenu();
        else if (userMenuChoice == "0") break;     
    }
    return;
}
void printUsersMenu()
{   
    Console.WriteLine("\n1 - Ispis korisnika abecedno po prezimenu");
    Console.WriteLine("2 - Ispis svih korisnika koji imaju više od 20 godina");
    Console.WriteLine("3 - Ispis svih korisnika koji imaju bar 2 putovanja");
    Console.Write("\nOdabir: ");
    var printChoice = Console.ReadLine();
    if (printChoice == "1") printUsersBySurname(users);
    else if(printChoice == "2") printUsersOverTwenty(users);
    else if( printChoice == "3") printUsersWithTrips(users);
}
void printUsersWithTrips(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Clear();
    Console.WriteLine("Popis korisnika koji imaju bar 2 putovanja: ");
    foreach (var user in users)
    {
        int tripsNumber = user.Value.tripsID.Count();
        if(tripsNumber >= 2)
            Console.WriteLine($"{user.Key} - {user.Value.name} - {user.Value.surname} - {user.Value.birthDate:yyyy-MM-dd}");
    }

    Console.Write("\nPritisni bilo kou tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();

}
void printUsersOverTwenty(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Clear();
    Console.WriteLine("Popis korisnika koji imaju više od dvadeset godina:");
    DateTime currentDate = DateTime.Today;

    foreach (var user in users)
    {
        int userAge = currentDate.Year - user.Value.birthDate.Year;
        if (currentDate.Month < user.Value.birthDate.Month || (currentDate.Month == user.Value.birthDate.Month && currentDate.Day < user.Value.birthDate.Day))
            userAge--;

        if (userAge > 20)
            Console.WriteLine($"{user.Key} - {user.Value.name} - {user.Value.surname} - {user.Value.birthDate:yyyy-MM-dd}");
    }
        
    Console.Write("Pritisni bilo kou tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void printUsersBySurname(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Clear();
    Console.WriteLine("Popis korisnika abecedno po prezimenu:\n");
    var usersList = users.ToList();

    usersList.Sort((x, y) =>
    {
        int surnameCompare = x.Value.surname.CompareTo(y.Value.surname);
        if(surnameCompare == 0)
            return x.Value.name.CompareTo(y.Value.name);
        return surnameCompare;
    });

    foreach(var user in usersList)
        Console.WriteLine($"{user.Key} - {user.Value.name} - {user.Value.surname} - {user.Value.birthDate:yyyy-MM-dd}");

    Console.Write("\nPritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void modifyUser(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Write("\nUpiši ID korisnika kojeg želite urediti: ");
    if(int.TryParse(Console.ReadLine(), out int userID))
    {
        if (!users.ContainsKey(userID))
        {
            Console.WriteLine("Korisnik s tim ID-jem ne postoji.");
            Console.WriteLine("Pritisnite bilo koju tipku za dalje...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Jeste li sigurni da želite urediti korisnika?");
        Console.Write("(da/ne): ");
        var confirm = Console.ReadLine();
        if (confirm == "ne")
        {
            Console.WriteLine("Uređivanje otkazano.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else if (confirm != "da")
        {
            Console.WriteLine("Nepoznat unos, uređivanje otkazano.");
            Console.Write("Pritisni bilo koju tipku za nastavak...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        var user = users[userID];
        string newName = user.name;
        string newSurname = user.surname;
        DateTime newBirthDate = user.birthDate;

        Console.WriteLine("\nTrenutni podaci o korisniku:");
        Console.WriteLine("Ime: {0}", user.name);
        Console.WriteLine("Prezime: {0}", user.surname);
        Console.WriteLine("Datum rođenja: {0}", user.birthDate.ToString("yyyy-MM-dd"));

        Console.Write("\nUnesi novo ime ili pritisni Enter za ostavljanje starog: ");
        string newNameInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newNameInput))
            newName = newNameInput;

        Console.Write("Unesi novo prezime ili pritisni Enter za ostavljanje starog: ");
        string newSurnameInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newSurnameInput))
            newSurname = newSurnameInput;

        Console.Write("Unesi novi datum rođenja (YYYY-MM-DD) ili Enter za ostavit isto: ");
        string newDateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDateInput) && DateTime.TryParse(newDateInput, out DateTime newParsedDate))
        {
            if (newParsedDate.Year >= 1925 && newParsedDate.Year <= 2025)
                newBirthDate = newParsedDate;
            else Console.WriteLine("Datum ne smije biti ranije od 1925 i nakon 2025!");
        }
        users[userID] = (newName, newSurname, newBirthDate, user.tripsID);
        Console.WriteLine("Korisnik uspješno ažuriran.");
        Console.WriteLine("Ime: {0}", newName);
        Console.WriteLine("Prezime: {0}", newSurname);
        Console.WriteLine("Datum rođenja: {0}", newBirthDate.ToString("yyyy-MM-dd"));
    }
    else Console.WriteLine("Unos ID-ja neispravan.");

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void deleteUserMenu()
{
    while (true)
    {
        Console.WriteLine("\n1 - Brisanje korisnika po ID-u.");
        Console.WriteLine("2 - Brisanje korisnika po imenu i prezimenu.");
        Console.WriteLine("0 - Povratak na izbornik korisnika.");
        Console.Write("\nOdabir: ");
        var deleteChoice = Console.ReadLine();
        if (deleteChoice == "1")
        {
            deleteUserID(users);
            break;
        }
        else if (deleteChoice == "2")
        {
            deleteUserNameSurname(users);
            break;
        }
        else if (deleteChoice == "0") break;
    }
}
void deleteUserNameSurname(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Write("Upiši ime korisnika kojeg želiš izbrisati: ");
    string name = Console.ReadLine();
    Console.Write("Upiši prezime korisnika kojeg želiš izbrisati: ");
    string surname = Console.ReadLine();
    bool userFound = false;
    foreach(var user in users)
    {
        if (user.Value.name.Equals(name, StringComparison.OrdinalIgnoreCase) && user.Value.surname.Equals(surname, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Jesi li siguran da želiš izbrisati korisnika {user.Value.name} {user.Value.surname}?");
            Console.Write("(da/ne): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "da")
            {

                users.Remove(user.Key);
                Console.WriteLine("Korisnik obrisan.");
                Console.Write("Pritisni bilo koju tipku za nastavak...");
                Console.ReadKey();
                Console.Clear();
            }
            else Console.WriteLine("Brisanje otkazano");
            userFound = true;
            break;
        }
    }
    if (!userFound)
        Console.WriteLine("Ne postoji korisnik s tim imenom i prezimenom.");

}
void deleteUserID(Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Write("Upiši ID korisnika kojeg želiš izbrisati: ");
    if (int.TryParse(Console.ReadLine(), out int userID))
    {
        if (users.ContainsKey(userID))
        {
            var user = users[userID];
            Console.WriteLine($"Jesi li siguran da želiš izbrisati korisnika {user.name} {user.surname}?");
            Console.Write("(da/ne): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "da")
            {
                users.Remove(userID);
                Console.WriteLine($"Korisnik {user.name} {user.surname} obrisan.");
            }
            else Console.WriteLine("Brisanje otkazano");
        }
        else Console.WriteLine("Korisnik s tim ID-jem ne postoji.");
    }
    else Console.WriteLine("Neispravan unos ID-ja.");

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void addUser(Dictionary<int, (string name, string surname, DateTime birthDate, List<int>tripsID)> users)
{
    string name;
    while (true)
    {
        Console.Write("\nUnesi ime novog korisnika: ");
        name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
            break;
        Console.WriteLine("Ime ne smije biti prazno!");
    }
    string surname;
    while (true)
    {
        Console.Write("Unesi prezime novog korisnika: ");
        surname = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(surname))
            break;
        Console.WriteLine("Prezime ne smije biti prazno!");
    }
    DateTime birthDate;
    while (true)
    {
        Console.Write("Unesi datum rođenja novog korisnika u obliku (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out birthDate))
        {
            if(birthDate.Year > 2025 || birthDate.Year < 1925)
            {
                Console.WriteLine("Datum rođenja ne smije biti ranije od 1925 i nakon 2025!");
                continue;
            }
            break;
        }
        Console.WriteLine("Neispravan format datuma!");
    }
    int newID = users.Keys.Max() + 1;
    users[newID] = (name, surname, birthDate, new List<int>());
    Console.WriteLine($"Korisnik {name} {surname} uspješno dodan (ID:{newID}).\n");
    Console.Write("Pristisnite bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}

void tripsMenu()
{
    while (true)
    {
        Console.WriteLine("\n1 - Unos novog putovanja");
        Console.WriteLine("2 - Brisanje putovanja");
        Console.WriteLine("3 - Uređivanje putovanja");
        Console.WriteLine("4 - Pregled svih putovanja");
        Console.WriteLine("0 - Povratak na glavni izbornik");
        Console.Write("\nOdabir: ");
        var tripsMenuChoice = Console.ReadLine();
        if (tripsMenuChoice == "1") addTrip(trips, users);
        else if (tripsMenuChoice == "2") deleteTripsMenu();
        else if (tripsMenuChoice == "3") modifyTrip(trips);
        else if (tripsMenuChoice == "4") printTripsMenu();
        else if (tripsMenuChoice == "0") break;
    }
    return;
}
void printTripsMenu()
{
    Console.WriteLine("\n1 - Sva putovanja redom kako su spremljena");
    Console.WriteLine("2 - Sva putovanja sortirana po trošku uzlazno");
    Console.WriteLine("3 - Sva  putovanja sortirana po trošku silazno");
    Console.WriteLine("4 - Sva  putovanja sortirana po kilometraži uzlazno");
    Console.WriteLine("5 - Sva  putovanja sortirana po kilometraži silazno");
    Console.WriteLine("6 - Sva  putovanja sortirana po datumu uzlazno");
    Console.WriteLine("7 - Sva  putovanja sortirana po datumu silazno");
    Console.Write("\nOdabir: ");
    var printChoice = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(printChoice)) printTrips(trips, printChoice);
    return;
    
}
void printTrips(Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)> trips,
    string printChoice)
{
    var tripList = trips.ToList();
    if (printChoice == "1") { }    
    else if(printChoice == "2")
        tripList.Sort((x, y) => x.Value.totalCost.CompareTo(y.Value.totalCost));
    else if (printChoice == "3")
        tripList.Sort((x, y) => y.Value.totalCost.CompareTo(x.Value.totalCost));
    else if (printChoice == "4")
        tripList.Sort((x, y) => x.Value.distance.CompareTo(y.Value.distance));
    else if(printChoice == "5")
        tripList.Sort((x, y) => y.Value.distance.CompareTo(x.Value.distance));
    else if(printChoice == "6")
        tripList.Sort((x, y) => x.Value.tripDate.CompareTo(y.Value.tripDate));
    else if(printChoice == "7")
        tripList.Sort((x, y) => y.Value.tripDate.CompareTo(x.Value.tripDate));

    Console.Clear();
    Console.WriteLine("Popis putovanja:\n");
    foreach (var trip in tripList)
    {
        Console.WriteLine("Putovanje #{0}", trip.Key);
        Console.WriteLine($"Datum: {trip.Value.tripDate:yyyy-MM-dd}");
        Console.WriteLine($"Kilometri: {trip.Value.distance}km");
        Console.WriteLine($"Gorivo: {trip.Value.fuel} L");
        Console.WriteLine($"Cijena po litri: {trip.Value.fuelPricePerLiter} EUR/L");
        Console.WriteLine($"Ukupno: {trip.Value.totalCost} EUR\n");
    }

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void addTrip(Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)> trips,
    Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    int userID;
    while (true)
    {
        Console.Write("Unesi ID korisnika čije je putovanje: ");
        if (int.TryParse(Console.ReadLine(), out userID) && users.ContainsKey(userID)) 
            break;
        Console.WriteLine("Ne postoji korisnik s tim ID-jem.");
    }
    int newID = trips.Keys.Max() + 1;
    users[userID].tripsID.Add(newID);

    DateTime tripDate;
    while (true)
    {
        Console.Write("Unesi datum novog putovanja u obliku (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out tripDate))
        {
            if (tripDate.Year > 2025 || tripDate.Year < 1925)
            {
                Console.WriteLine("Datum putovanja ne smije biti ranije od 1925 i nakon 2025!");
                continue;
            }
            break;
        }
        Console.WriteLine("Neispravan format datuma!\n");
    }
    int distance;
    while (true)
    {
        Console.Write("Unesi udaljenost putovanja u km: ");
        if(int.TryParse(Console.ReadLine(), out distance) && distance > 0)
            break;
        Console.WriteLine("Neispravan unos udaljenosti!\n");

    }
    decimal fuel;
    while (true)
    {
        Console.Write("Unesi potrošeno gorivo: ");
        if (decimal.TryParse(Console.ReadLine(), out fuel) && fuel > 0)
            break;
        Console.WriteLine("Neispravan unos goriva!\n");

    }
    decimal fuelPricePerLiter;
    while (true)
    {
        Console.Write("Unesi cijenu goriva po litri: ");
        if (decimal.TryParse(Console.ReadLine(), out fuelPricePerLiter) && fuelPricePerLiter > 0)
            break;
        Console.WriteLine("Neispravan unos cijene goriva!\n");
    }

    decimal totalCost = fuel * fuelPricePerLiter;
    trips[newID] = (tripDate, distance, fuel, fuelPricePerLiter, totalCost);
    Console.WriteLine("Novo putovanje dodano.");
    Console.Write("Pristisnite bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void deleteTripsMenu()
{
    while (true)
    {
        Console.WriteLine("\n1 - Brisanje putovanja po ID-u.");
        Console.WriteLine("2 - Brisanje putovanja skupljih od nekog iznosa.");
        Console.WriteLine("3 - Brisanje putovanja jeftinijih od nekog iznosa.");
        Console.Write("\nOdabir: ");
        var deleteChoice = Console.ReadLine();
        if (deleteChoice == "1")
        {
            deleteTripID(trips, users);
            break;
        }
        else if(deleteChoice == "2")
        {
            deleteTripsByCost(trips, users, deleteChoice);
            break;
        }
        else if (deleteChoice == "3")
        {
            deleteTripsByCost(trips, users, deleteChoice);
            break;
        }
        else if (deleteChoice == "0") break;
    }
}
void deleteTripsByCost(
    Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)> trips,
    Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users,
    string deleteChoice)
{
    var deletionIDs = new List<int>();
    decimal tripCost;
    if (deleteChoice == "2")
    {
        Console.Write("Upiši iznos za koji će sva putovanja, ako su skuplja od iznosa biti izbrisana: ");
        if (!decimal.TryParse(Console.ReadLine(), out tripCost))
        {
            {
                Console.WriteLine("Neispravan unos iznosa!");
                Console.Write("\nPritisni bilo koju tipku za nastavak...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }
        foreach (var trip in trips)
            if (trip.Value.totalCost > tripCost)
                deletionIDs.Add(trip.Key);
    }
    else if (deleteChoice == "3")
    {
        Console.Write("Upiši iznos za koji će sva putovanja, ako su jeftinija od iznosa biti izbrisana: ");
        if (!decimal.TryParse(Console.ReadLine(), out tripCost))
        {
            {
                Console.WriteLine("Neispravan unos iznosa!");
                Console.Write("\nPritisni bilo koju tipku za nastavak...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }
        foreach (var trip in trips)
            if (trip.Value.totalCost < tripCost)
                deletionIDs.Add(trip.Key);
    }
    if(deletionIDs.Count == 0)
        Console.WriteLine("Nema putovanja za brisanje");
    else
    {
        foreach(var tripID in deletionIDs)
        {
            foreach(var user in users)
            {
                if (user.Value.tripsID.Contains(tripID))
                {
                    user.Value.tripsID.Remove(tripID);
                    break;
                }
            }
            trips.Remove(tripID);
        }
        Console.WriteLine($"\nObrisano {deletionIDs.Count} putovanja.");
    }

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void deleteTripID(Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)> trips,
    Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)> users)
{
    Console.Write("Upiši ID putovanja koje želiš izbrisati: ");
    if (int.TryParse(Console.ReadLine(), out int tripID))
    {
        if (trips.ContainsKey(tripID))
        {
            var trip = trips[tripID];
            Console.WriteLine($"Jesi li siguran da želiš izbrisati putovanje dugo {trip.distance} km?");
            Console.Write("(da/ne): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "da")
            {
                foreach (var user in users)
                    if (user.Value.tripsID.Contains(tripID))
                    {
                        user.Value.tripsID.Remove(tripID);
                        break;
                    }
                trips.Remove(tripID);
                Console.WriteLine("Putovanje obrisano.");
            }
            else Console.WriteLine("Brisanje otkazano");
        }
        else Console.WriteLine("Putovanje s tim ID-jem ne postoji.");
    }
    else Console.WriteLine("Neispravan unos ID-ja.");

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}
void modifyTrip(Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)> trips)
{
    Console.Write("\nUpiši ID putovanja kojeg želiš urediti: ");
    if (int.TryParse(Console.ReadLine(), out int tripID))
    {
        if (!trips.ContainsKey(tripID))
        {
            Console.WriteLine("Putovanje s tim ID-jem ne postoji.");
            Console.WriteLine("Pritisnite bilo koju tipku za dalje...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        Console.WriteLine($"Jeste li sigurni da želite urediti putovanje?");
        Console.Write("(da/ne): ");
        var confirm = Console.ReadLine();
        if (confirm == "ne")
        {
            Console.WriteLine("Uređivanje otkazano.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else if (confirm != "da")
        {
            Console.WriteLine("Nepoznat unos, uređivanje otkazano.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        var trip = trips[tripID];

        DateTime newTripDate = trip.tripDate;
        int newDistance = trip.distance;
        decimal newFuel = trip.fuel;
        decimal newFuelPricePerLiter = trip.fuelPricePerLiter;
        decimal newTotalCost = trip.totalCost;

        Console.WriteLine("\nTrenutni podaci o putovanju:");
        Console.WriteLine($"Datum: {trip.tripDate:yyyy-MM-dd}");
        Console.WriteLine($"Dužina: {trip.distance}km");
        Console.WriteLine($"Gorivo potrošeno: {trip.fuel} L");
        Console.WriteLine($"Cijena goriva po litri: {trip.fuelPricePerLiter} EUR/L");
        Console.WriteLine($"Ukupni trošak: {trip.totalCost} EUR");

        Console.Write("Unesi novi datum putovanja (YYYY-MM-DD) ili Enter za ostavit isto: ");
        string newDateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDateInput) && DateTime.TryParse(newDateInput, out DateTime parsedDate))
        {
            if (parsedDate.Year >= 1925 && parsedDate.Year <= 2025)
                newTripDate = parsedDate;
            else Console.WriteLine("Datum ne smije biti ranije od 1925 i nakon 2025!");
        }

        Console.Write("\nUnesi novu udaljenost ili Enter za zadržavanje stare: ");
        string newDistanceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDistanceInput) && int.TryParse(newDistanceInput, out int parsedDistance))
            newDistance = parsedDistance;

        Console.Write("\nUnesi novu količinu goriva ili Enter za zadržavanje stare: ");
        string newFuelInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFuelInput) && decimal.TryParse(newFuelInput, out decimal parsedFuel))
            newFuel = parsedFuel;

        Console.Write("\nUnesi novu cijenu goriva po litri ili Enter za zadržavanje stare: ");
        string newFuelPriceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFuelPriceInput) && decimal.TryParse(newFuelPriceInput, out decimal parsedFuelPrice))
            newFuelPricePerLiter = parsedFuelPrice;

        newTotalCost = newFuel * newFuelPricePerLiter;

        trips[tripID] = (newTripDate, newDistance, newFuel, newFuelPricePerLiter, newTotalCost);
        Console.WriteLine("\nPutovanje uspješno ažurirano:");
        Console.WriteLine($"Datum: {newTripDate:yyyy-MM-dd}");
        Console.WriteLine($"Dužina: {newDistance} km");
        Console.WriteLine($"Gorivo: {newFuel} L");
        Console.WriteLine($"Cijena goriva: {newFuelPricePerLiter} EUR/L");
        Console.WriteLine($"Ukupni trošak: {newTotalCost} EUR");
    }
    else Console.WriteLine("Unos ID-ja neispravan.");

    Console.Write("Pritisni bilo koju tipku za nastavak...");
    Console.ReadKey();
    Console.Clear();
}