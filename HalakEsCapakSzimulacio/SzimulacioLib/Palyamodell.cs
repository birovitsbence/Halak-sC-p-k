using SzimulacioLib.Entitasok;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

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
        public const string Capa = "🐟";
        public const string Hal = "🐠";
        public const string Alga = "🌿";
        public const string Viz = "🔵";

        // Új mezők a statisztikákhoz
        public int HalakAltalElfogyasztottAlga { get; private set; }
        public int CapakAltalElfogyasztottHal { get; private set; }

        public Palyamodell(int meret, int sebesseg)
        {
            this.meret = meret;
            this.sebesseg = sebesseg;
            palya = new int[meret, meret];
            algak = new List<Alga>();
            halak = new List<Hal>();
            capak = new List<Capa>();
            random = new Random();

            // Statisztikák alaphelyzetbe állítása
            HalakAltalElfogyasztottAlga = 0;
            CapakAltalElfogyasztottHal = 0;

            KezdetiAllapot();
        }

        private void KezdetiAllapot()
        {
            int algaSzam = meret * meret / 6;
            int halSzam = meret * meret / 6;
            int capaSzam = meret * meret / 60;

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

        public void Szimulacio()
    {
        int lepesSzamlalo = 0;
        DateTime startTime = DateTime.Now; // Szimuláció kezdeti időpontja

        while (halak.Count > 0 || capak.Count > 0) // Futás, amíg van hal vagy cápa
        {
            Console.Clear();

            // Megjelenítés
            Megjelenit();

            // Lépés szimulációban
            Lepes();

            // Lépésszámláló
            lepesSzamlalo++;

            // Lassítás
            System.Threading.Thread.Sleep(sebesseg);
        }

        // Végső üzenet
        DateTime endTime = DateTime.Now; // Szimuláció végső időpontja
        TimeSpan duration = endTime - startTime;
        Console.WriteLine($"A szimuláció {Math.Round(duration.TotalSeconds,2)} másodpercig futott.");
        Console.WriteLine("A szimuláció befejeződött. Nyomj meg egy gombot a kilépéshez.");
        Console.ReadKey();
    }

        private void Megjelenit()
        {
            // Kurzor pozíciójának visszaállítása a kezdőpontra
            Console.SetCursorPosition(0, 0); 

            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    string kijelzo = Viz;
                    if (algak.Exists(a => a.X == i && a.Y == j))
                    {
                        kijelzo = Alga;
                    }
                    else if (halak.Exists(h => h.X == i && h.Y == j))
                    {
                        kijelzo = Hal;
                    }
                    else if (capak.Exists(c => c.X == i && c.Y == j))
                    {
                        kijelzo = Capa;
                    }

                    // Karakter kiírása színnel együtt
                    Console.Write(kijelzo + " ");
                    Console.ResetColor(); // Szín visszaállítása az alapértelmezettre
                }
                Console.WriteLine();
            }
        }
        private void Lepes()
        {
            // Algák növekedése
            foreach (var alga in algak)
            {
                alga.Novekszik();
            }

            //TODO: esély beállítása a lehető legjobbra

            if (random.Next(0, 100) < 10) // 10% esély, hogy egy új alga jön létre
            {
                List<(int x, int y)> uresMezok = new List<(int, int)>();
                for (int i = 0; i < palya.GetLength(0); i++)
                {
                    for (int j = 0; j < palya.GetLength(1); j++)
                    {
                        // Ellenőrizzük, hogy a mező üres-e
                        if (!algak.Exists(a => a.X == i && a.Y == j) &&
                            !halak.Exists(h => h.X == i && h.Y == j) &&
                            !capak.Exists(c => c.X == i && c.Y == j))
                        {
                            uresMezok.Add((i, j)); // Ha üres, adjuk hozzá az üres mezők listájához
                        }
                    }
                }

                if (uresMezok.Count > 0) // Ha van üres mező, akkor létrejöhet alga
                {
                    var uresMezo = uresMezok[random.Next(uresMezok.Count)];
                    algak.Add(new Alga(uresMezo.x, uresMezo.y));
                    Console.WriteLine("Új alga jött létre a koordinátán: (" + uresMezo.x + ", " + uresMezo.y + ")");
                }
            }

            // Halak mozgása, táplálkozása és szaporodása
            for (int i = 0; i < halak.Count; i++)
            {
                var hal = halak[i];
                if (hal.Taplalkozas(palya, algak)) // Hal megpróbál táplálkozni
                {
                    // Növeljük az elfogyasztott algák számát
                    HalakAltalElfogyasztottAlga++;

                    // Alga eltávolítása a listából
                    var alga = algak.Find(a => a.X == hal.X && a.Y == hal.Y);
                    if (alga != null)
                    {
                        algak.Remove(alga); // Elfogyasztott alga eltávolítása
                    }

                    // Ha táplálkozott, új szaporodás lehetősége
                    hal.Szaporodas(halak);
                }
                else
                {
                    hal.Mozog(palya); // Hal mozgása
                }

                // Halak elhalálozása
                if (hal.EhenHal())
                {
                    halak.Remove(hal);
                    i--; // A lista mérete változik, ezért csökkenteni kell az indexet
                }
            }

            // Cápák mozgása, táplálkozása és szaporodása
            for (int i = 0; i < capak.Count; i++)
            {
                var capa = capak[i];
                if (capa.Taplalkozas(palya, halak)) // Cápa megpróbál táplálkozni
                {
                    // Növeljük az elfogyasztott halak számát
                    CapakAltalElfogyasztottHal++;

                    // Ha táplálkozott, új szaporodás lehetősége
                    if (capa.Kifejlett)
                    {
                        capa.Szaporodas(capak);
                    }
                }
                else
                {
                    capa.Mozog(palya); // Cápa mozgása
                }

                // Cápák elhalálozása
                if (capa.EhenHal())
                {
                    capak.Remove(capa);
                    i--;
                }
            }
        }
    }
}