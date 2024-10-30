using SzimulacioLib;

// Konzol törlése és pályaméret kiválasztása
Console.Clear();
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
    default:
        Console.WriteLine("\nÉrvénytelen választás, alapértelmezett méret (10x10) alkalmazva.");
        Thread.Sleep(1500);
        break;
}

// Sebesség kiválasztása
Console.Clear();
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
    default:
        Console.WriteLine("\nÉrvénytelen választás, alapértelmezett sebesség (500 ms) alkalmazva.");
        Thread.Sleep(1500);
        break;
}

// Szimulációk számának bekérése, és érvényesség ellenőrzése
Console.Clear();
Console.WriteLine("Hányszor fusson le a szimuláció?");
int futasokSzama;
while (!int.TryParse(Console.ReadLine(), out futasokSzama) || futasokSzama <= 0)
{
    Console.Clear();
    Console.WriteLine("Kérlek adj meg egy érvényes pozitív egész számot!");
}

// Szimuláció statisztikák inicializálása
int osszHalakEttek = 0;
int osszCapakEttek = 0;
double osszFutasIdo = 0;

// Szimuláció futtatása a felhasználó által megadott számú alkalommal
for (int i = 0; i < futasokSzama; i++)
{
    Console.Clear();
    Palyamodell palya = new Palyamodell(meret, sebesseg);

    // Szimuláció futtatása és idő visszakapása
    osszFutasIdo += palya.Szimulacio();

    // Szimuláció adatai összegzése
    osszHalakEttek += palya.HalakAltalElfogyasztottAlga;
    osszCapakEttek += palya.CapakAltalElfogyasztottHal;

    // Következő futásra vár, ha több futás van hátra
    if (i < futasokSzama - 1)
    {
        Console.WriteLine("Nyomd meg az Enter billentyűt a következő szimuláció indításához...");
        Console.ReadLine();
        Console.Clear();
    }
}

// Eredmények kiíratása a szimulációk után
Console.Clear();
Console.WriteLine("Minden szimuláció lefutott. Részletes statisztikák:");
Console.WriteLine($"Szimulációk száma: {futasokSzama}");
Console.WriteLine($"Pálya mérete: {meret}x{meret}");
Console.WriteLine($"Szimuláció sebessége: {sebesseg} ms");
Console.WriteLine($"Halak által elfogyasztott algák: {osszHalakEttek}");
Console.WriteLine($"Cápák által elfogyasztott halak: {osszCapakEttek}");
Console.WriteLine($"Futási idő: {Math.Round(osszFutasIdo, 2)} másodperc.");