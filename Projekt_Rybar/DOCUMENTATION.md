# Projekt_Rybar — Dokumentace projektu 

## Přehled
`Projekt_Rybar` je jednoduchá konzolová hra. Hráč může chytat ryby, sbírat je do inventáře, prodávat je za coiny a objevovat položky v katalogu. Stav hráče se ukládá do souboru `player.json` pomocí `System.Text.Json`.

## Jak ovládat
V konzoli používejte klávesy:
   - `R` — Chytit rybu
   - `I` — Zobrazit inventář
   - `K` — Katalog
   - `E` — Prodat rybu
   - `S` — Uložit a ukončit

Data hráče (coins, inventory, catalog) se ukládají / načítají ze souboru `player.json` v adresáři aplikace.

## Soubory projektu 
- `Program.cs` — Vstupní bod aplikace, UI a herní logika.
- `Player.cs` — Třída `Player`: `coins`, `inventory`, `catalog`.
- `Fish.cs` — Třída `Fish`: `name`, `rarita`, `cost`, `same` a logika generování ryby.

## Popis tříd a jejich vazeb

### `Program`
- Na startu načítá `Player` ze souboru `player.json` (pokud existuje) pomocí `JsonSerializer`.
- Důležité statické metody:
  - `CatchFish(Player player)` — Vytvoří novou `Fish`, přidá ji do `player.inventory` nebo zvýší `same` u již existující položky; aktualizuje `player.catalog` při objevení nové ryby.
  - `ShowInventory(Player player)` — Vypíše inventář a stav coinů; barví výstup podle rarity.
  - `ProdatRyby(int coins, List<Fish> inventory)` — Interaktivní prodej ryb, aktualizace `coins` a `inventory`.
  - `Catalog(Dictionary<string,string> catalog)` — Zobrazení katalogu; neobjevené položky jako `?????`.
  - `TextMenu()` / `DoMenu()` — Pomocné metody pro zobrazení menu a vstup.
- Persistence: při volbě `S` se `Player` serializuje do `player.json`.

Vazby:
- `Program` vytváří a manipuluje instancemi `Player` a `Fish`.
- `Program` serializuje `Player`, který obsahuje `Fish` v inventáři.

### `Player`
- Soubor: `Player.cs`
- Třída: `internal class Player`
- Veřejné vlastnosti:
  - `int coins { get; set; }` — Počet coinů (výchozí `0`).
  - `List<Fish> inventory { get; set; }` — Seznam vlastněných ryb; každá `Fish` může mít `same` pro počet stejných kusů.
  - `Dictionary<string, string> catalog { get; set; }` — Mapuje jméno ryby na její nalezenou raritu (`"common" | "rare" | "epic"`).
- Konstruktor: bezparametrický (inicializuje kolekce).

Vazby:
- `Player.inventory` drží instance `Fish`.
- `Player.catalog` udržuje objevené druhy (aktualizuje `Program` při chytání).

### `Fish`
- Soubor: `Fish.cs`
- Třída: `internal class Fish`
- Polia / vlastnosti:
  - `public string rarita;` — Rarita (`"common"`, `"rare"`, `"epic"`).
  - `public int cost;` — Cena při prodeji.
  - `public string name;` — Jméno ryby (z `allNames`).
  - `public int same;` — Počet identických kusů reprezentovaných tímto objektem.
  - `public static string[] allNames` — Pole možných jmen ryb.
- Konstruktor:
  - Používá `Random` k náhodnému výběru rarity a jména; nastaví `cost` a `same = 1`.
- Metoda `FishInfo()` vypisuje informace o rybě v inventáři.
- Poznámka: `allNames` obsahuje česká jména (např. `Kapr`, `Štika`).

Vazby:
- `Fish` objekty jsou uloženy v `Player.inventory`.
- `Program` vytváří `Fish` při akci chytit rybu.


## Textové schéma tříd (zjednodušeně)
- `Program`
  - vytváří/řídí → `Player`
  - vytváří → `Fish`
- `Player`
  - má → `List<Fish> inventory`
  - má → `Dictionary<string,string> catalog`
- `Fish`
  - je položka v → `Player.inventory`