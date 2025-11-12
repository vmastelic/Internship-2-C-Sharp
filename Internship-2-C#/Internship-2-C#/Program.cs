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
    else if (choice == "2")
        Console.WriteLine("Zasad nema nista.");
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
        else if (userMenuChoice == "4") showUsersMenu();
        else if (userMenuChoice == "0") break;     
    }
    return;
}
void showUsersMenu()
{   
    Console.WriteLine("\n1 - Ispis korisnika abecedno po prezimenu");
    Console.WriteLine("2 - Ispis svih korisnika koji imaju više od 20 godina");
    Console.WriteLine("3 - Ispis svih korisnika koji imaju bar 2 putovanja");
    Console.Write("\nOdabir: ");
    var showChoice = Console.ReadLine();
    if (showChoice == "1") printUsersBySurname(users);
    else if(showChoice == "2") printUsersOverTwenty(users);
    else if( showChoice == "3") printUsersWithTrips(users);
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
    Console.Write("\nUpiši ID korisnika kojeg želiš urediti: ");
    if(int.TryParse(Console.ReadLine(), out int userID))
    {
        if (!users.ContainsKey(userID))
        {
            Console.WriteLine("Korisnik s tim ID-jem ne postoji.");
            Console.WriteLine("Pritisni bilo koju tipku za dalje...");
            Console.ReadKey();
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