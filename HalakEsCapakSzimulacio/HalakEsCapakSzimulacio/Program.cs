using SzimulacioLib;

Console.WriteLine("Válassz pálya méretet: ");
Console.WriteLine("1. Kis pálya (10x10)");
Console.WriteLine("2. Közepes pálya (20x20)");
Console.WriteLine("3. Nagy pálya (30x30)");
int meretValasztas = int.Parse(Console.ReadLine());

int meret = 10;
switch (meretValasztas)
{
    case 1: meret = 10; break;
    case 2: meret = 20; break;
    case 3: meret = 30; break;
    default: Console.WriteLine("Érvénytelen választás, alapértelmezett méret (10x10) alkalmazva."); break;
}

Console.WriteLine("Válassz sebességet: ");
Console.WriteLine("1. Gyors (100 ms)");
Console.WriteLine("2. Közepes (500 ms)");
Console.WriteLine("3. Lassú (1000 ms)");
int sebessegValasztas = int.Parse(Console.ReadLine());

int sebesseg = 500;
switch (sebessegValasztas)
{
    case 1: sebesseg = 100; break;
    case 2: sebesseg = 500; break;
    case 3: sebesseg = 1000; break;
    default: Console.WriteLine("Érvénytelen választás, alapértelmezett sebesség (500 ms) alkalmazva."); break;
}

// Pálya indítása
Palyamodell palya = new Palyamodell(meret, sebesseg);
palya.Szimulacio();