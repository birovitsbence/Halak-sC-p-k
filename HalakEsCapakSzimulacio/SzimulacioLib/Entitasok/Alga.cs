namespace SzimulacioLib.Entitasok
{
    public class Alga
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Kifejlett { get; set; } = false;

        public Alga(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Alga növekedése két fázisban: kezdemény -> kifejlett alga
        public void Novekszik()
        {
            if (!Kifejlett)
            {
                Kifejlett = true; // Algák kifejlődnek, ha nincs hal a mezőn
            }
        }

        // Ha egy hal megeszi az algát, az eltűnik
        public void Fogyaszt()
        {
            Kifejlett = false;
        }
    }
}
