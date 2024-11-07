using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Hal
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int jollakottsag;
        private const int maxJollakottsag = 20; // Maximális jóllakottság a halak számára
        private Random random;

        public Hal(int x, int y)
        {
            X = x;
            Y = y;
            jollakottsag = maxJollakottsag; // Kezdetben maximális jóllakottsággal indul
            random = new Random();
        }

        public bool Taplalkozas(int[,] palya, List<Alga> algak)
        {
            // Megpróbálunk egy kifejlett algát találni a hal körül, amit megehet
            Alga talaltAlga = algak.Find(a => Math.Abs(a.X - X) <= 1 && Math.Abs(a.Y - Y) <= 1 && a.Kifejlett);
            if (talaltAlga != null)
            {
                jollakottsag = Math.Min(jollakottsag + 10, maxJollakottsag); // Jóllakottság növelése, de nem haladhatja meg a maximális értéket
                algak.Remove(talaltAlga); // Alga eltávolítása a listából
                return true;
            }
            return false;
        }

        public void Mozog(int[,] palya)
        {
            // Véletlenszerű mozgás a négy irány valamelyikébe
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

        public bool EhenHal()
        {
            // Csökkenti a jóllakottságot; ha eléri a 0-t, a hal meghal
            if (jollakottsag <= 2)
            {
                return true; // Hal éhen halt
            }
            jollakottsag -= 2;
            return false;
        }

        public void Szaporodas(List<Hal> halak, int palyaMeret)
        {
            // Szaporodás üres mezőre, ha két hal szomszédos mezőkön áll
            List<(int x, int y)> szomszedok = new List<(int, int)>
            {
                (X - 1, Y), (X + 1, Y), (X, Y - 1), (X, Y + 1)
            };

            foreach (var (x, y) in szomszedok)
            {
                // Ellenőrzi, hogy a szomszédos mező üres-e, és benne van-e a pálya méretén belül
                if (x >= 0 && x < palyaMeret && y >= 0 && y < palyaMeret &&
                    !halak.Exists(h => h.X == x && h.Y == y))
                {
                    halak.Add(new Hal(x, y)); // Új hal létrehozása

                    // Második hal létrehozása 20% eséllyel
                    if (random.Next(0, 100) < 20)
                    {
                        halak.Add(new Hal(x, y));
                    }
                    break;
                }
            }
        }

        public bool Kifejlett => jollakottsag > 0; // Igaz, ha a hal életben van
    }
}