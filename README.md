# TestTaskBLS
Unity Projekt testowy dla BLS

- plugins - brak 
- packages - standardowe dla projektu Unity 2d core 2021.3.16f1 

Realizacja projektu zajeła mi 4-5 godzin pracy wieczorami z przerwami/
Nie wykorzystałem żadnych pluginów, choć rozważałem bibliotekę iTween

W projekcie postawiłem na spawnowanie przeciwników oraz pocisków gracza poprzez object pool, raz załadowane obiekty zostawały również re-używane w przypadku restartu gry.

Na scenie znajdują się prefaby canvasu dla background oraz canvasu statystyk gracza, canvas statystyk posiada skrypt który jednocześnie kontroluje
czy rozgrywka powinna się zacząć lub skończyć,
Game Manager, który odpowiada za zmienne ustawienia gry, tj liczbe przeciwników, ich rozstawienie, typy przeciwników, czas gry, oraz życie gracza,
oraz prefab gracza z wartościami jego prędkości oraz czasu pomiędzy spawnem pocisków.

**kontrowersyjne decyzje:**

*Poprzez Game Manager spawnuje collidery które mają za zadanie gasić obiekty który znalazły się już poza ekranem gracza, oraz kontroluje spawn przeciwnych samolotów.*

 - myślałem nad kontrolą pozycji lub dystansu, oraz o czasie na zgaszenie obiektu względem obliczenia ile potrzebuje aby być na ekranie, ale ostatecznie decyzja 
z colliderami wydała mi się najoptymalniejszą

*Przeciwnicy jak i pociski poruszają się poprzez Update, co jest rozwiązaniem które stanowczo powinno się zmienić.*

Myślałem nad opcjami takimi jak: 

- usunięcie update na enemies, oraz skrolowanie w prawo określonych obiektów, gracza + dodawanie prędkości skrolowania do prędkości pocisków, 
kamery na którą nakierowany jest canvas background, oraz colliderów blokujących obiekty wypadające poza ekran.

- kontrola globalna przeciwników i pocisków poprzez dodawanie wartości do ich pozycji z poziomu jednego update

- spawnowanie od 2-5 przeciwników w jednej linii pod gameobject który kontrolował by ich wspólne przesunięcie w kierunku gracza

Jednak na implementacje danych opcji potrzebował bym jeszcze troche czasu
