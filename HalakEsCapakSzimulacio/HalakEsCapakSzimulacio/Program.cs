using SzimulacioLib;
using System;

Console.WriteLine("Válassz pálya méretet: ");
Console.WriteLine("1. Kis pálya (10x10)");
Console.WriteLine("2. Közepes pálya (20x20)");
Console.WriteLine("3. Nagy pálya (30x30)");
int meretValasztas = int.Parse(Console.ReadLine()!);

int meret = 10;
switch (meretValasztas)
{
    case 1: meret = 10; break;
    case 2: meret = 20; break;
    case 3: meret = 30; break;
    default: Console.WriteLine("Érvénytelen választás, alapértelmezett méret (10x10) alkalmazva."); break;
}

Console.WriteLine("Válassz sebességet: ");
Console.WriteLine("1. Lassú");
Console.WriteLine("2. Közepes");
Console.WriteLine("3. Gyors");

int sebessegValasztas = int.Parse(Console.ReadLine());

int sebesseg = 500;
switch (sebessegValasztas)
{
    case 1: sebesseg = 1000; break;
    case 2: sebesseg = 500; break;
    case 3: sebesseg = 100; break;
    default: Console.WriteLine("Érvénytelen választás, alapértelmezett sebesség (500 ms) alkalmazva."); break;
}

Console.WriteLine("Hányszor fusson le a szimuláció?");
int futasokSzama;
while (!int.TryParse(Console.ReadLine(), out futasokSzama) || futasokSzama <= 0)
{
    Console.WriteLine("Kérlek adj meg egy érvényes pozitív egész számot!");
}

// Statisztikák összegyűjtése az összes futásról
int osszHalakEttek = 0;
int osszCapakEttek = 0;
double osszFutasIdo = 0;

for (int i = 0; i < futasokSzama; i++)
{
    Console.WriteLine($"\nSzimuláció {i + 1} futása:");

    // Új pálya indítása minden szimulációhoz
    Palyamodell palya = new Palyamodell(meret, sebesseg);

    // Szimuláció futtatása
    DateTime startTime = DateTime.Now;
    palya.Szimulacio();
    DateTime endTime = DateTime.Now;

    // Időtartam összesítése
    TimeSpan duration = endTime - startTime;
    osszFutasIdo += duration.TotalSeconds;

    // Szimuláció adatai összegzése
    osszHalakEttek += palya.HalakAltalElfogyasztottAlga;
    osszCapakEttek += palya.CapakAltalElfogyasztottHal;

    // Ha van még hátralévő futás, várj Enter billentyűre
    if (i < futasokSzama - 1)
    {
        Console.WriteLine("Nyomd meg az Enter billentyűt a következő szimuláció indításához...");
        Console.ReadLine();
    }
}

// Összegzett statisztikák kiírása a szimuláció végén
Console.WriteLine("\nMinden szimuláció lefutott. Részletes statisztikák:");
Console.WriteLine($"Halak által elfogyasztott algák: {osszHalakEttek}");
Console.WriteLine($"Cápák által elfogyasztott halak: {osszCapakEttek}");
Console.WriteLine($"Futási idő: {osszFutasIdo} másodperc.");