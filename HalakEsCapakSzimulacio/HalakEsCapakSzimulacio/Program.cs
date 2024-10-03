using SzimulacioLib;

static void Main(string[] args)
{
    int palyaMeret = PalyameretValasztas();
    int sebesseg = SebessegValasztas();

    Palyamodell palya = new Palyamodell(palyaMeret);
    palya.Indit(sebesseg);
}

static int PalyameretValasztas()
{
    Console.WriteLine("Válassza ki a pálya méretét (1: Kicsi, 2: Közepes, 3: Nagy):");
    string input = Console.ReadLine();
    return input switch
    {
        "1" => 10,
        "2" => 20,
        "3" => 30,
        _ => 10,
    };
}

static int SebessegValasztas()
{
    Console.WriteLine("Válassza ki a szimuláció sebességét (1: Lassú, 2: Normál, 3: Gyors):");
    string input = Console.ReadLine();
    return input switch
    {
        "1" => 1000,
        "2" => 500,
        "3" => 100,
        _ => 500,
    };
}