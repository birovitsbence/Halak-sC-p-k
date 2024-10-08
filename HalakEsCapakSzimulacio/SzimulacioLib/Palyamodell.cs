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

        public Palyamodell(int meret, int sebesseg)
        {
            this.meret = meret;
            this.sebesseg = sebesseg;
            palya = new int[meret, meret];
            algak = new List<Alga>();
            halak = new List<Hal>();
            capak = new List<Capa>();
            random = new Random();

            KezdetiAllapot();
        }

        private void KezdetiAllapot()
        {
            // Az entitások arányos eloszlása a pálya mérete alapján
            int algaSzam = meret * meret / 4;
            int halSzam = meret * meret / 6;
            int capaSzam = meret * meret / 8;

            // Algák elhelyezése
            for (int i = 0; i < algaSzam; i++)
            {
                algak.Add(new Alga(random.Next(0, meret), random.Next(0, meret)));
            }

            // Halak elhelyezése
            for (int i = 0; i < halSzam; i++)
            {
                halak.Add(new Hal(random.Next(0, meret), random.Next(0, meret)));
            }

            // Cápák elhelyezése
            for (int i = 0; i < capaSzam; i++)
            {
                capak.Add(new Capa(random.Next(0, meret), random.Next(0, meret)));
            }
        }

        public void Indit()
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
            Console.WriteLine($"A szimuláció {duration.TotalSeconds} másodpercig futott.");
            Console.WriteLine("A szimuláció befejeződött. Nyomj meg egy gombot a kilépéshez.");
            Console.ReadKey();
        }

        private void Megjelenit()
        {
            // Pálya megjelenítése
            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    char kijelzo = '.';
                    // Ellenőrizzük, hogy van-e alga, hal vagy cápa ezen a mezőn
                    if (algak.Exists(a => a.X == i && a.Y == j))
                        kijelzo = 'A';
                    if (halak.Exists(h => h.X == i && h.Y == j))
                        kijelzo = 'H';
                    if (capak.Exists(c => c.X == i && c.Y == j))
                        kijelzo = 'C';

                    Console.Write(kijelzo + " ");
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

            // Halak mozgása, táplálkozása és szaporodása
            for (int i = 0; i < halak.Count; i++)
            {
                var hal = halak[i];
                if (hal.Taplalkozas(palya, algak)) // Hal megpróbál táplálkozni
                {
                    // Ha táplálkozott, új szaporodás lehetősége
                    if (hal.Kifejlett)
                    {
                        hal.Szaporodas(halak);
                    }
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
                    // Ha táplálkozott, új szaporodás lehetősége
                    if (capa.Kifejlett)
                    {
                        capa.Szaporodas(capak);
                    }
                }
                else
                {
                    capa.Mozog(palya); // Cápa mozgása, lépjen 2 mezőt
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
