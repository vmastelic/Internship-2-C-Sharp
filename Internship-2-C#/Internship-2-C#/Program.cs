using System;
Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");

var users = new Dictionary<int, (string name, string surname, DateTime birthDate)>();
users[1] = ("Marijan", "Mastelic", new DateTime(2002, 10, 10));
users[2] = ("Vatroslav", "Mastelic", new DateTime(2002, 10, 10));
users[3] = ("Lucija", "Pavić", new DateTime(2000, 10, 12));

while (true)
{
    Console.WriteLine("\n1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije\n");
    Console.Write("Odabir: ");

    var choice = Console.ReadLine();
    if (choice == "1")usersMenu();
    else if (choice == "2")
        Console.WriteLine("Zasad nema nista.");
    else if (choice == "0") break;
}

void usersMenu()
{
    while (true)
    {
        Console.WriteLine("1 - Unos novog korisnika");
        Console.WriteLine("0 - Povratak na glavni izbornik");
        Console.Write("Odabir: ");
        var userMenuChoice = Console.ReadLine();
        if (userMenuChoice == "1")addUser(users);
        else if (userMenuChoice == "0") break;
    }
    return;
}

void addUser(Dictionary<int, (string name, string surname, DateTime birthDate)> users)
{
    Console.Write("Unesi ime novog korisnika: ");
    string name = Console.ReadLine();
    Console.Write("Unesi prezime novog korisnika: ");
    string surname = Console.ReadLine();
    Console.Write("Unesi datum rođenja u obliku (YYYY-MM-DD): ");
    DateTime birthDate = DateTime.Parse(Console.ReadLine());

    int newID = users.Keys.Max() + 1;
    users[newID] = (name, surname, birthDate);
    Console.WriteLine($"Korisnik {name} {surname} uspjesno dodan (ID:{newID}).\n");
}