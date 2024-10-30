using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Alga
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Kifejlett { get; set; } = false;

        private int novekedesFazis = 0;
        private const int maxFazis = 3; // Az algának ennyi növekedési lépés szükséges a teljes kifejlettséghez

        public Alga(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Novekszik()
        {
            if (!Kifejlett)
            {
                novekedesFazis++;
                if (novekedesFazis >= maxFazis) // Eléri a maximum fázist, kifejletté válik
                {
                    Kifejlett = true;
                }
            }
        }

        public void Fogyaszt()
        {
            // Alga visszaáll kezdeti állapotba, ha elfogyasztják
            Kifejlett = false;
            novekedesFazis = 0;
        }
    }
}