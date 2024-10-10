Program.cs:
Pálya méretének kiválasztása:

A felhasználó választ három lehetőség közül (10x10, 20x20, vagy 30x30), ami meghatározza a szimulációs pálya méretét.
Ha érvénytelen értéket ad meg, az alapértelmezett 10x10-es pálya méretet kapja.
Sebesség kiválasztása:

A felhasználó választja ki, milyen gyorsan menjen a szimuláció (100 ms, 500 ms, 1000 ms).
Ha hibás választás történik, az alapértelmezett 500 ms sebesség lesz érvényben.
Pálya indítása:

Létrehozza a Palyamodell objektumot a kiválasztott mérettel és sebességgel, majd elindítja a szimulációt az Indit() metódussal.
Palyamodell.cs:
Konstruktor (Palyamodell):

Létrehozza a szimulációs pályát (mátrix) és három listát a háromféle entitás (alga, hal, cápa) tárolására.
A pálya méretének megfelelően létrehoz egy random kezdőállapotot az entitásokkal.
KezdetiAllapot():

Az entitások (algák, halak, cápák) véletlenszerűen helyezkednek el a pályán.
Az entitások száma a pálya méretének arányában van elosztva.
Indit():

Egy ciklusban futtatja a szimulációt mindaddig, amíg van hal vagy cápa a pályán.
Minden körben:
Törli a konzol tartalmát.
Megjeleníti a pálya aktuális állapotát (Megjelenit() metódus).
Végrehajtja az adott lépést a szimulációban (Lepes() metódus).
Számolja a lépéseket és lassítja a ciklust a felhasználó által választott sebességgel.
Megjelenit():

Végigmegy a pályán, és a konzolon megjeleníti, hogy adott mezőn van-e alga (A), hal (H) vagy cápa (C), vagy üres (.).
Lepes():

Algák növekedése: Minden alga nő egy ciklusban.
Halak viselkedése:
Minden hal megpróbál algát enni, ha sikerül, akkor életben marad, és lehet, hogy szaporodik.
Ha nem talál ételt, akkor mozog.
Ha a hal éhen hal, akkor eltávolítják a listáról.
Cápák viselkedése:
Minden cápa halat próbál enni. Ha talál ételt, akkor életben marad, és szaporodhat is.
Ha nem talál halat, akkor mozog.
Ha éhen hal, akkor eltávolítják a listáról.
