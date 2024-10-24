using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Capa
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int jollakottsag;
        private const int maxJollakottsag = 4;
        private Random random;

        public Capa(int x, int y)
        {
            X = x;
            Y = y;
            jollakottsag = maxJollakottsag; // Kezdetben tele van
            random = new Random();
        }

        public bool Taplalkozas(int[,] palya, List<Hal> halak)
        {
            // Ellenőrizzük, hogy van-e hal a közelben
            Hal talaltHal = halak.Find(h => Math.Abs(h.X - X) <= 1 && Math.Abs(h.Y - Y) <= 1);
            if (talaltHal != null)
            {
                jollakottsag = Math.Min(jollakottsag + 2, maxJollakottsag); // Növeljük a jóllakottságot
                halak.Remove(talaltHal); // Hal eltűnik
                return true;
            }
            return false; // Nem talált táplálékot
        }

        public void Mozog(int[,] palya)
        {
            // Véletlenszerű mozgás, 2 lépés
            for (int i = 0; i < 2; i++)
            {
                int irany = random.Next(4); // 0: fel, 1: le, 2: balra, 3: jobbra
                int ujX = X, ujY = Y;

                switch (irany)
                {
                    case 0: ujX = Math.Max(0, X - 1); break;
                    case 1: ujX = Math.Min(palya.GetLength(0) - 1, X + 1); break;
                    case 2: ujY = Math.Max(0, Y - 1); break;
                    case 3: ujY = Math.Min(palya.GetLength(1) - 1, Y + 1); break;
                }

                // Frissítsük a pozíciót
                X = ujX;
                Y = ujY;
            }
        }

        public bool EhenHal()
        {
            if (jollakottsag <= 0)
            {
                return true; // Éhen halt
            }

            jollakottsag--;
            return false; // Él
        }

        public void Szaporodas(List<Capa> capak)
        {
            // Szaporodás, ha van üres mező
            List<(int x, int y)> szomszedok = new List<(int, int)>
            {
                (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1)
            };

            foreach (var (x, y) in szomszedok)
            {
                if (x >= 0 && x < 30 && y >= 0 && y < 30 && !capak.Exists(c => c.X == x && c.Y == y))
                {
                    capak.Add(new Capa(x, y));
                    break;
                }
            }
        }
        public bool Kifejlett => jollakottsag > 0;
    }
}