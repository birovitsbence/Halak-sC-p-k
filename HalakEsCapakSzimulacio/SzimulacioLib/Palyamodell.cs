using SzimulacioLib.Entitasok;
namespace SzimulacioLib;

public class Palyamodell
{
    private int[,] palya;
    private List<Alga> algak;
    private List<Hal> halak;
    private List<Capa> capak;
    private int meret;

    public Palyamodell(int meret)
    {
        this.meret = meret;
        palya = new int[meret, meret];
        algak = new List<Alga>();
        halak = new List<Hal>();
        capak = new List<Capa>();

        KezdetiAllapot();
    }

    private void KezdetiAllapot()
    {
        algak.Add(new Alga(0, 0));
        halak.Add(new Hal(1, 1));
        capak.Add(new Capa(2, 2));
    }

    public void Indit(int sebesseg)
    {
        while (true)
        {
            Console.Clear();
            Megjelenit();
            Lepes();
            System.Threading.Thread.Sleep(sebesseg);
        }
    }

    private void Megjelenit()
    {
        for (int i = 0; i < meret; i++)
        {
            for (int j = 0; j < meret; j++)
            {
                Console.Write(palya[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    private void Lepes()
    {
        // Halak, cápák, algák lépései és frissítése
    }
}