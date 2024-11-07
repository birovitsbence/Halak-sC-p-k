using SzimulacioLib;
using SzimulacioLib.Entitasok;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Teszt
{
    [TestClass]
    public class SzimulacioTeszt
    {
        [TestMethod]
        public void AlgaNovekszikHaromFazisbanEsKifejlettLesz()
        {
            var alga = new Alga(0, 0);
            alga.Novekszik();
            Assert.IsFalse(alga.Kifejlett);
            alga.Novekszik();
            Assert.IsFalse(alga.Kifejlett);
            alga.Novekszik();
            Assert.IsTrue(alga.Kifejlett);
        }

        [TestMethod]
        public void KifejlettAlgaHalAltalElfogyasztvaEsEltunik()
        {
            var alga = new Alga(0, 0) { Kifejlett = true };
            alga.Fogyaszt();
            Assert.IsFalse(alga.Kifejlett);
        }

        [TestMethod]
        public void HalMegesziKifejlettAlgatEsJollakottsagNovekszik()
        {
            var hal = new Hal(0, 0);
            var alga = new Alga(1, 0) { Kifejlett = true };
            List<Alga> algak = new List<Alga> { alga };

            var eredmeny = hal.Taplalkozas(new int[10, 10], algak);
            Assert.IsTrue(eredmeny);
            Assert.AreEqual(0, algak.Count);
        }

        [TestMethod]
        public void HalMozogEsUresMezoreLep()
        {
            var hal = new Hal(5, 5);
            int[,] palya = new int[10, 10];
            hal.Mozog(palya);
            Assert.IsTrue(hal.X >= 4 && hal.X <= 6);
            Assert.IsTrue(hal.Y >= 4 && hal.Y <= 6);
        }

        [TestMethod]
        public void HalakSzaporodasaEsUjHalMegjelenik()
        {
            int palyaMeret = 10;
            var halak = new List<Hal> { new Hal(5, 5), new Hal(5, 6) };
            halak[0].Szaporodas(halak, palyaMeret);
            Assert.AreEqual(3, halak.Count);
        }

        [TestMethod]
        public void HalEhenHalEsNincsTobbAlgaEsJollakottsagElfogy()
        {
            var hal = new Hal(0, 0);
            for (int i = 0; i < 16; i++)
                hal.EhenHal();

            Assert.IsTrue(hal.EhenHal());
        }

        [TestMethod]
        public void CapaMegetteHalatEsJollakottsagNovekszik()
        {
            var capa = new Capa(0, 0);
            var hal = new Hal(1, 0);
            List<Hal> halak = new List<Hal> { hal };

            var eredmeny = capa.Taplalkozas(new int[10, 10], halak);
            Assert.IsTrue(eredmeny);
            Assert.AreEqual(0, halak.Count);
        }

        [TestMethod]
        public void CapaNemTalalHalatEsVeletlenszeruenMozog()
        {
            var capa = new Capa(5, 5);
            int[,] palya = new int[10, 10];
            capa.Mozog(palya);
            Assert.IsTrue(capa.X >= 4 && capa.X <= 6);
            Assert.IsTrue(capa.Y >= 4 && capa.Y <= 6);
        }

        [TestMethod]
        public void CapakSzaporodasaEsUjCapaMegjelenik()
        {
            int palyaMeret = 10;
            var capak = new List<Capa> { new Capa(5, 5), new Capa(5, 6) };
            capak[0].Szaporodas(capak, palyaMeret);
            Assert.AreEqual(3, capak.Count);
        }

        [TestMethod]
        public void Capa_EhenHal_MikorJollakottsagElfogy()
        {
            var capa = new Capa(0, 0);

            bool ehenHal = false;
            while (!ehenHal)
            {
                ehenHal = capa.EhenHal();
            }

            Assert.IsTrue(ehenHal, "A c�pa nem halt �hen, amikor a j�llakotts�ga elfogyott.");
        }
    }
}

/*
Alga n�veked�si szakaszok kezel�se:
AlgaNovekszikHaromFazisbanEsKifejlettLesz: Teszteli, hogy az alga n�veked�se h�rom f�zisban kifejlett lesz-e.
KifejlettAlgaHalAltalElfogyasztvaEsEltunik: Teszteli, hogy a kifejlett alg�t a hal elfogyasztja, �s az alga elt�nik.

Hal t�pl�lkoz�sa �s mozg�sa:
HalMegesziKifejlettAlgatEsJollakottsagNovekszik: Teszteli, hogy a hal megeszi a kifejlett alg�t, �s a j�llakotts�gi szintje n�vekszik.
HalMozogEsUresMezoreLep: Ellen�rzi, hogy a hal �res mez�re mozog, ha nincs el�rhet� alga.

Halak szaporod�sa:
HalakSzaporodasaEsUjHalMegjelenik: Teszteli, hogy k�t szomsz�dos hal eset�n egy �j hal jelenik meg egy �res mez�n.

Hal �hen hal:
HalEhenHalEsNincsTobbAlgaEsJollakottsagElfogy: Ellen�rzi, hogy a hal �hen hal, ha a j�llakotts�gi szintje elfogy, �s nincs t�bb alga.

C�pa vad�szik halra:
CapaMegetteHalatEsJollakottsagNovekszik: Teszteli, hogy a c�pa elfogyasztja a k�zel�ben l�v� halat, �s n�vekszik a j�llakotts�gi szintje.
CapaNemTalalHalatEsVeletlenszeruenMozog: Ellen�rzi, hogy a c�pa v�letlenszer�en mozog, ha nincs a k�zel�ben hal.

C�pa szaporod�sa:
CapakSzaporodasaEsUjCapaMegjelenik: Teszteli, hogy a c�p�k szaporodnak, ha egy m�sik c�pa mellett �llnak.

C�pa �hen hal:
Capa_EhenHal_MikorJollakottsagElfogy: Ellen�rzi, hogy a c�pa �hen hal, ha a j�llakotts�ga 0-ra cs�kken.
*/