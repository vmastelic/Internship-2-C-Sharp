using System;

var users = new Dictionary<int, (string name, string surname, DateTime birthDate, List<int> tripsID)>();
var trips = new Dictionary<int, (DateTime tripDate, int distance, decimal fuel, decimal fuelPricePerLiter, decimal totalCost)>();

users[1] = ("Marijan", "Mastelic", new DateTime(2002, 10, 10), new List<int>());
users[2] = ("Vatroslav", "Mastelic", new DateTime(2002, 10, 10), new List<int>());
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
        Console.WriteLine("0 - Povratak na glavni izbornik");
        Console.Write("\nOdabir: ");
        var userMenuChoice = Console.ReadLine();
        if (userMenuChoice == "1") addUser(users);
        else if (userMenuChoice == "0") break;
    }
    return;
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
        Console.Write("\nUnesi prezime novog korisnika: ");
        surname = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(surname))
            break;
        Console.WriteLine("Prezime ne smije biti prazno!");
    }
    DateTime birthDate;
    while (true)
    {
        Console.Write("\nUnesi datum rođenja novog korisnika u obliku (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out birthDate))
            break;
        Console.WriteLine("Neispravan format datuma!");
    }
    int newID = users.Keys.Max() + 1;
    users[newID] = (name, surname, birthDate, new List<int>());
    Console.Clear();
    Console.WriteLine($"Korisnik {name} {surname} uspješno dodan (ID:{newID}).\n");
}