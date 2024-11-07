using SzimulacioLib.Entitasok;
using System;
using System.Collections.Generic;

namespace SzimulacioLib
{
    public class Palyamodell
    {
        private int[,] palya;
        private List<Alga> algak;
        private List<Hal> halak;
        private List<Capa> capak;
        private int meret;
        private int sebesseg;
        private Random random;
        private FastConsole fastConsole;

        // Unicode karakterek beállítása alapértelmezettként a különböző entitásokhoz
        public string Capa { get; private set; } = "🐟";
        public string Hal { get; private set; } = "🐠";
        public string Alga { get; private set; } = "🌿";
        public string Viz { get; private set; } = "🔵";

        public int HalakAltalElfogyasztottAlga { get; private set; }
        public int CapakAltalElfogyasztottHal { get; private set; }

        private void KezdetiAllapot()
        {
            // Az entitások kezdeti száma a pálya méretének függvényében
            int algaSzam = meret * meret / 2;
            int halSzam = meret * meret / 6;
            int capaSzam = meret * meret / 40;

            for (int i = 0; i < algaSzam; i++)
            {
                algak.Add(new Alga(random.Next(0, meret), random.Next(0, meret)));
            }

            for (int i = 0; i < halSzam; i++)
            {
                halak.Add(new Hal(random.Next(0, meret), random.Next(0, meret)));
            }

            for (int i = 0; i < capaSzam; i++)
            {
                capak.Add(new Capa(random.Next(0, meret), random.Next(0, meret)));
            }
        }

        public Palyamodell(int meret, int sebesseg)
        {
            this.meret = meret;
            this.sebesseg = sebesseg;
            palya = new int[meret, meret];
            algak = new List<Alga>();
            halak = new List<Hal>();
            capak = new List<Capa>();
            random = new Random();
            fastConsole = new FastConsole(meret * 2, meret);

            if (OperatingSystem.IsWindows())
            {
                Capa = "C";
                Hal = "H";
                Alga = "A";
                Viz = ".";
            }

            HalakAltalElfogyasztottAlga = 0;
            CapakAltalElfogyasztottHal = 0;

            KezdetiAllapot(); // Inicializálja a pályát a kezdeti állapotokkal
        }

        public double Szimulacio()
        {
            DateTime startTime = DateTime.Now;

            while (halak.Count > 0 || capak.Count > 0)
            {
                Megjelenit();
                Lepes();
                System.Threading.Thread.Sleep(sebesseg);
            }

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;
            Console.WriteLine($"\n\nA szimuláció {Math.Round(duration.TotalSeconds, 2)} másodpercig futott.");
            Console.WriteLine("A szimuláció befejeződött. Nyomj meg egy gombot a kilépéshez.");
            Console.ReadKey();

            return duration.TotalSeconds; // A teljes szimuláció futási idejét adja vissza
        }

        private void Megjelenit()
        {
            fastConsole.Clear();

            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    string kijelzo = Viz;
                    ConsoleColor color = ConsoleColor.Blue;

                    if (algak.Exists(a => a.X == i && a.Y == j))
                    {
                        kijelzo = Alga;
                        color = ConsoleColor.Green;
                    }
                    else if (halak.Exists(h => h.X == i && h.Y == j))
                    {
                        kijelzo = Hal;
                        color = ConsoleColor.Magenta;
                    }
                    else if (capak.Exists(c => c.X == i && c.Y == j))
                    {
                        kijelzo = Capa;
                        color = ConsoleColor.Red;
                    }

                    fastConsole.WriteAt(j * 2, i, kijelzo, color);
                }
            }

            fastConsole.Render();
        }

        private void Lepes()
        {
            // Algák növekedése és új alga létrejövésének megnövelt esélye
            foreach (var alga in algak)
            {
                alga.Novekszik();
            }

            if (random.Next(0, 100) < 8)
            {
                List<(int x, int y)> uresMezok = new List<(int, int)>();
                for (int i = 0; i < palya.GetLength(0); i++)
                {
                    for (int j = 0; j < palya.GetLength(1); j++)
                    {
                        if (!algak.Exists(a => a.X == i && a.Y == j) &&
                            !halak.Exists(h => h.X == i && h.Y == j) &&
                            !capak.Exists(c => c.X == i && c.Y == j))
                        {
                            uresMezok.Add((i, j));
                        }
                    }
                }

                if (uresMezok.Count > 0)
                {
                    var uresMezo = uresMezok[random.Next(uresMezok.Count)];
                    algak.Add(new Alga(uresMezo.x, uresMezo.y));
                }
            }

            // Halak táplálkozása, mozgása és gyakrabban történő szaporodása
            for (int i = 0; i < halak.Count; i++)
            {
                var hal = halak[i];
                if (hal.Taplalkozas(palya, algak))
                {
                    HalakAltalElfogyasztottAlga++;
                    var alga = algak.Find(a => a.X == hal.X && a.Y == hal.Y);
                    if (alga != null)
                    {
                        algak.Remove(alga);
                    }

                    // Gyakoribb szaporodás a halak esetében
                    if (random.Next(0, 100) < 30)
                    {
                        hal.Szaporodas(halak, meret);
                    }
                }
                else
                {
                    hal.Mozog(palya);
                }

                if (hal.EhenHal())
                {
                    halak.Remove(hal);
                    i--;
                }
            }

            // Cápák táplálkozása, mozgása és csökkentett táplálékbevitel
            for (int i = 0; i < capak.Count; i++)
            {
                var capa = capak[i];
                if (capa.Taplalkozas(palya, halak))
                {
                    CapakAltalElfogyasztottHal++;
                    if (capa.Kifejlett)
                    {
                        if (random.Next(0, 100) < 20) // 20% esély a szaporodásra
                        {
                            capa.Szaporodas(capak, meret); // A pálya méretét is átadjuk, ha szükséges
                        }
                    }
                }
                else
                {
                    capa.Mozog(palya);
                }

                if (capa.EhenHal())
                {
                    capak.Remove(capa);
                    i--;
                }
            }
        }
    }
}