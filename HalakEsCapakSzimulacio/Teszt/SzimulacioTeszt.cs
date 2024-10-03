using SzimulacioLib;

namespace Teszt;

[TestClass]
public class SzimulacioTeszt
{
    [TestMethod]
    public void AlapPalyameretTeszt()
    {
        Palyamodell palya = new Palyamodell(10);
        Assert.IsNotNull(palya);
    }

    [TestMethod]
    public void KozepesPalyameretTeszt()
    {
        Palyamodell palya = new Palyamodell(20);
        Assert.IsNotNull(palya);
    }

    [TestMethod]
    public void NagyPalyameretTeszt()
    {
        Palyamodell palya = new Palyamodell(30);
        Assert.IsNotNull(palya);
    }
}