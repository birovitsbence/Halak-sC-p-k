using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Hal
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int jollakottsag;
        private const int maxJollakottsag = 15;
        private Random random;

        public Hal(int x, int y)
        {
            X = x;
            Y = y;
            jollakottsag = maxJollakottsag; // Kezdetben tele van
            random = new Random();
        }

        public bool Taplalkozas(int[,] palya, List<Alga> algak)
        {
            // Ellenőrizzük, hogy van-e alga a közelben
            Alga talaltAlga = algak.Find(a => Math.Abs(a.X - X) <= 1 && Math.Abs(a.Y - Y) <= 1 && a.Kifejlett);
            if (talaltAlga != null)
            {
                jollakottsag = Math.Min(jollakottsag + 5, maxJollakottsag);
                algak.Remove(talaltAlga);
                return true;
            }
            return false; // Nem talált táplálékot
        }

        public void Mozog(int[,] palya)
        {
            // Véletlenszerű mozgás
            for (int i = 0; i < 1; i++)
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

        public void Szaporodas(List<Hal> halak)
        {
            // Szaporodás, ha van üres mező
            List<(int x, int y)> szomszedok = new List<(int, int)>
            {
                (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1)
            };

            foreach (var (x, y) in szomszedok)
            {
                if (x >= 0 && x < 30 && y >= 0 && y < 30 && !halak.Exists(h => h.X == x && h.Y == y))
                {
                    halak.Add(new Hal(x, y)); // Új hal létrejön
                    break;
                }
            }
        }
        public bool Kifejlett => jollakottsag > 0;
    }
}
