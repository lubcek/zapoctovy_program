# zapoctovy_program

Zápočtový program - Hungry Horace

Programování 2 NPRG031

Lubor Čech, I. ročník, ls 2021/2022

MFF UK

Dokumentace - doprovodný dokument
-----------------------------------------------------------------------------------------

1. ANOTACE

   Zadáním programu bylo vytvoření hry s několika pohybujícími se objekty.
   Hru bylo možno vybrat z doporučeného seznamu nebo si vymyslet vlastní.
   Já jsem si vybral vytvoření hry na principu Hungry Horace.

2. PŘESNÉ ZADÁNÍ

   Hungry horace je hra, kde se hráč pohybuje bludištěm pomocí své postavičky
   a sbírá po cestě jídlo. Spolu s ním je v bludišti strážce, který jídlo hlídá
   a když Horace chytí, ten ztratí život a přesouvá se na začátek mapy. Životů
   má Horace na začátku několik, když ztratí všechny, hra končí. Navíc se na mapě
   nachází zvonek, po jehož sebrání je možné určitý čas strážce porazit a získat
   velké mnnožtví bodů.
   Hráč může směr pohybu Horace ovládat pomocí šipek.

3. ALGORITMUS

   Algoritmus je takový, že hra neustále každých 10 milisekund vyhodnocuje svůj stav
   a stav všech prvků formuláře. Během každé smyčky se kontrolují podmínky, které by
   mohly stav hry změnit.
   Pokud je některá taková podmínka splněná, provede se uzavřená změna stavu hry ve
   ve smyčce buď přímo nebo voláním pomocné funkce.
   Jinak se v programu žádný sofistikovaný a složitý algoritmus nenachází.   

-----------------------------------------------------------------------------------------

4. PROGRAM

   Program má strukturu několika windows forms, mezi nimiž se uživatel pohybuje.
   Celkem program obsahuje 4 formuláře - Menu, Info, Rules a Game.

- Menu:
   - exit_Click - ukončí program
   - info_Click - uzavře aktuální formulář (Menu) a vytvoří a zobrazí formulář
                     obsahující informace o programu
   - rules_Click - uzavře aktuální formulář (Menu) a vytvoří a zobrazí formulář
                      obsahující pravidla hry
   - game_Click - uzavře aktuální formulář (Menu) a vytvoří a zobrazí formulář
                     obsahující herní mapu a hru samotnou
- Info:
   - menu_Click - uzavře aktuální formulář (Info) a vytvoří a zobrazí formulář
	     		   obsahující menu programu
- Rules:
   - menu_Click - uzavře aktuální formulář (Rules) a vytvoří a zobrazí formulář
			   obsahující menu programu
- Game:
   - keyIsDown - funkce přijímající signály z šipek, které určí, kterým směrem
	  	        se má Horace pohybovat.
   - resetGame - funkce provádějící úplný reset hry, používá se pro spuštění hry
		        poprvé nebo resetování hry po ztrátě všech životů. Nastavuje vše
		        na výchozí hodnoty a stavy.
   - nextGame - podobá se funkci resetGame, ovšem neresetuje celou hru, ale jen
   		       scénu. Zachovává tak informace o celkovém skóre, životech a kole.
   - gameOver - zastaví hru a zobrazí další prvky formuláře. Hráči program vypíše
		       celkové skóre a dá možnost spustit novou hru nebo se vrátit do Menu
   - newGame_Click - tlačítko, které spustí novou hru stejně jako při spouštění
			      z Menu
   - BackToMenu_Click - tlačítko, které uzavře aktuální formulář (Game) a spustí
				   formulář s Menu
   - mainGameTimer - hlavní a nejdůležitější část programu. Herní smyčka, která
			      v průběhu neustále vyhodnocuje stav hry a podle toho upravuje 
			      prvky formuláře. Konkrétní části funkce jsou popsány v poznámkách
			      v programu
            
		Nejdůležitější částí herní smyčky je pohyb Horace a strážce. Ten probíhá tak,
		   že smyčka kontroluje aktivní ukazatele směru a podle nich posouvá polohu
		   PictureBoxu představujícího Horace a strážce s jejich aktuální rychlostí.
		   Když narazí do zdi, zastaví se a - Horace čeká na nový pokyn ke změne směru
		   šipkou, strážce vygeneruje náhodně směr kolmý na původní směr pohybu.

      Když se Horace a strážce setkají, vyhodnotí se, kdo koho porazí a podle toho
		  buď Horace získá body a strážce dočasně zmizí nebo Horace ztratí život a jak
		  on tak i strážce se přesunou zpět na výchozí pozici.

-----------------------------------------------------------------------------------------

6. VSTUPNÍ DATA

   Kvůli povaze programu nejsou v podstatě vyžadována vstupní data. Za vstup lze
   považovat signály z kláves šipek, kterými hráč mění směr Horace v průběhu hry.

7. VÝSTUPNÍ DATA

   Stejně jako vstupní data, ani výstupní nejsou vzhledem k povaze programu pevně
   definována. Můžeme za ně však považovat grafický výstup s vykreslením formuláře,
   který obsahuje aktualní pozici v programu (vzhledem ke struktuře formulářů),
   aktuální situaci přímo ve formuláři Game nebo lze nad výstupními daty uvažovat
   jako nad celkovým skóre zobrazeným po konci hry.

-----------------------------------------------------------------------------------------

8. PRŮBĚH PRACÍ

   Původní představa podoby programu byla taková, že všechny prvky budou v jednom
   formuláři a pouze se bude měnit jejich viditelnost. Toto se však vzhledem k množství
   prvků brzy ukázalo jako velice nepraktické řešení. Proto jsem program předělal a
   rozdělil jednotlivé části mezi více formulářů. Ovšem zde jsem zjistil, že formuláře
   zbytečně nadpoužívám a mé představy o podobě programu by byly moc sloužité. Proto jsem
   založil projekt potřetí a ten již zpracoval do podoby, kterou má program nyní.
   V průběhu psaní kódu jsem relaxoval kreslením obrázků pro program a přitom mě napadlo,
   že není potřeba vytvářet gify pro animování pohybu prvků, ale stačí vytvořit několik
   obrázků, které se budou během chodu hry střídat.

9. CO BY SE MOHLO JEŠTĚ DODĚLAT, ALE NEZBYL NA TO ČAS

- Určitě by se dal lépe napsat algoritmus pohybu strážce, který je teď zcela náhodný.
    Původní myšlenka byla taková, že se bude snažit pokračovat ve své cestě, tedy když se
    bude pohybovat doprava, a pak zahne dolů, bude se při další příležitosti snažit opět
    zatočit vpravo. Ovšem kvůli způsobu vyhodnocování směru pohybu, kdy se změní směr v
    jednom průběhu herní smyčky a to, jestli se tímto směrem lze pohybovat se vyhodnotí
    až ve druhém běhu, mě nenapadlo, jak by se dalo tohoto dosáhnout.
    
- Dále by jistě šlo přidat další herní mapy, aby se pouze neresetovala jedna.

- Dalším vhodným doplňkem pro zlepšení hry by byla možnost hru pozastavit, aby bylo
    možné ji opět spustit od pozastaveného okamžiku
    
- S ohledem na to, jak jsem studoval originální verzi hry (respektive ne originální,
    ale verzi dostupnou na Steamu), daly by se přidat některé další mechanismy jako např.
    že se na mapě postupně objevuje více strážců nebo jsou v bludišti místa, která jsou
    3D, tedy že jimi lze projít, ačkoliv při pohledu shora to vypadá, že je tam zeď.

-----------------------------------------------------------------------------------------

Psaní programu bylo zajímavé. Velmi kladně hodnotím, že jsem se nemusel dostat k nějakému
konkrétnímu výslednému programu, který přesně zpracuje a vyhodnotí zadaná data, ale mohl
jsem si prostě hrát. Když mě napadla nějaká drobnost, kterou bych mohl přidat, tak jsem
ji přidal, nebo jsem něco upravil, aby to fungovalo trochu odlišně.
Původní zamýšlená kostra programu se udělala poměrně rychle, ale pak probíhala práce
právě na oněch drobných úpravách a obalování kostry dalšími mechanismy hry.

Jediné, co mě mrzí, je deadline, do kterého musím program odevzdat a tím v podstatě
ztratít motivaci mu dále věnovat čas a zlepšovat jej. Protože přece jen nic dokonalého
to není a dá se dělat spousta užitečnějších věcí, než zrovna vylepšovat tuto hru.
Ovšem čas venovaný tomuto projektu byla opravdu zábava až na ty nejvíc rutinní práce jako
třeba rozmisťování mincí do herní mapy.

Lubor Čech, 14. 7. 2022, Mikulov		
