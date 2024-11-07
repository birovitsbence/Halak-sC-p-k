using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Capa
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int jollakottsag;
        public bool Kifejlett => jollakottsag > 0;

        private const int maxJollakottsag = 3; // Maximális jóllakottsági szint
        private Random random;

        public Capa(int x, int y)
        {
            X = x;
            Y = y;
            jollakottsag = maxJollakottsag;
            random = new Random();
        }

        public bool Taplalkozas(int[,] palya, List<Hal> halak)
        {
            // Keresünk egy halat, amit a cápa megehet
            Hal talaltHal = halak.Find(h => Math.Abs(h.X - X) <= 2 && Math.Abs(h.Y - Y) <= 2);
            if (talaltHal != null)
            {
                jollakottsag = Math.Min(jollakottsag + 1, maxJollakottsag); // Növeli a jóllakottságát, de nem haladja meg a max értéket
                halak.Remove(talaltHal); // Hal eltávolítása
                return true;
            }
            return false;
        }

        public void Mozog(int[,] palya)
        {
            // A cápa véletlenszerűen mozog két lépést
            for (int i = 0; i < 2; i++)
            {
                int irany = random.Next(4);
                int ujX = X, ujY = Y;

                switch (irany)
                {
                    case 0: ujX = Math.Max(0, X - 1); break;
                    case 1: ujX = Math.Min(palya.GetLength(0) - 1, X + 1); break;
                    case 2: ujY = Math.Max(0, Y - 1); break;
                    case 3: ujY = Math.Min(palya.GetLength(1) - 1, Y + 1); break;
                }

                X = ujX;
                Y = ujY;
            }
        }

        private int csokkenesiLepesek = 2; // 2 lépésenként csökken a jóllakottság

        public bool EhenHal()
        {
            if (csokkenesiLepesek > 0)
            {
                csokkenesiLepesek--;
                return false;
            }

            csokkenesiLepesek = 2; // visszaállítjuk, hogy 2 lépés múlva ismét csökkenjen
            if (jollakottsag <= 0)
            {
                return true; // A cápa éhen halt
            }
            jollakottsag -= 2;
            return false;
        }

        public void Szaporodas(List<Capa> capak, int palyaMeret)
        {
            if (jollakottsag > maxJollakottsag / 2) // Szaporodási feltétel
            {
                List<(int x, int y)> szomszedok = new List<(int, int)>
                {
                    (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1)
                };

                foreach (var (x, y) in szomszedok)
                {
                    // Ellenőrizzük, hogy a szomszédos mező üres-e, és benne van-e a pálya méreteiben
                    if (x >= 0 && x < palyaMeret && y >= 0 && y < palyaMeret &&
                        !capak.Exists(c => c.X == x && c.Y == y))
                    {
                        capak.Add(new Capa(x, y)); // Új cápa létrehozása
                        break;
                    }
                }
            }
        }
    }
}