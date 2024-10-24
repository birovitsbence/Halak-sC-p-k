using System;
using System.Collections.Generic;

namespace SzimulacioLib.Entitasok
{
    public class Alga
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Kifejlett { get; private set; } = false;
        private int novekedesFazis = 0;

        public Alga(int x, int y)
        {
            X = x;
            Y = y;
        }

        //TODO: Növekedés beállítása a lehető legjobbra
        //TODO: Lehet több növekedési fázist behozni pl:3,4 (Jelenlegi: 2)

        // Alga növekedése: több fázisban fejlődik ki
        public void Novekszik()
        {
            // Ha az alga nem kifejlett, növeljük a fázis számát
            if (!Kifejlett)
            {
                novekedesFazis++;
                if (novekedesFazis >= 2) // Például 2 lépés után lesz kifejlett
                {
                    Kifejlett = true;
                }
            }
        }

        // Ha egy hal megeszi az algát, az eltűnik (ezt a pályamodell kezeli)
        public void Fogyaszt()
        {
            Kifejlett = false;
        }
    }
}