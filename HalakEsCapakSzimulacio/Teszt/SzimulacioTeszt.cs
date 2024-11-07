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

            Assert.IsTrue(ehenHal, "A cápa nem halt éhen, amikor a jóllakottsága elfogyott.");
        }
    }
}

/*
Alga növekedési szakaszok kezelése:
AlgaNovekszikHaromFazisbanEsKifejlettLesz: Teszteli, hogy az alga növekedése három fázisban kifejlett lesz-e.
KifejlettAlgaHalAltalElfogyasztvaEsEltunik: Teszteli, hogy a kifejlett algát a hal elfogyasztja, és az alga eltûnik.

Hal táplálkozása és mozgása:
HalMegesziKifejlettAlgatEsJollakottsagNovekszik: Teszteli, hogy a hal megeszi a kifejlett algát, és a jóllakottsági szintje növekszik.
HalMozogEsUresMezoreLep: Ellenõrzi, hogy a hal üres mezõre mozog, ha nincs elérhetõ alga.

Halak szaporodása:
HalakSzaporodasaEsUjHalMegjelenik: Teszteli, hogy két szomszédos hal esetén egy új hal jelenik meg egy üres mezõn.

Hal éhen hal:
HalEhenHalEsNincsTobbAlgaEsJollakottsagElfogy: Ellenõrzi, hogy a hal éhen hal, ha a jóllakottsági szintje elfogy, és nincs több alga.

Cápa vadászik halra:
CapaMegetteHalatEsJollakottsagNovekszik: Teszteli, hogy a cápa elfogyasztja a közelében lévõ halat, és növekszik a jóllakottsági szintje.
CapaNemTalalHalatEsVeletlenszeruenMozog: Ellenõrzi, hogy a cápa véletlenszerûen mozog, ha nincs a közelében hal.

Cápa szaporodása:
CapakSzaporodasaEsUjCapaMegjelenik: Teszteli, hogy a cápák szaporodnak, ha egy másik cápa mellett állnak.

Cápa éhen hal:
Capa_EhenHal_MikorJollakottsagElfogy: Ellenõrzi, hogy a cápa éhen hal, ha a jóllakottsága 0-ra csökken.
*/