![F#](https://img.shields.io/badge/F%23-378BBA?logo=fsharp&logoColor=fff&style=for-the-badge)
![GitHub repo size](https://img.shields.io/github/repo-size/MRmlik12/zdaj-se-anki-generator?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/MRmlik12/zdaj-se-anki-generator?style=for-the-badge)

# Zdaj.se Anki Generator

Wygeneruj talie kart do [Anki](https://apps.ankiweb.net/) w formie quizu z pytaniami i odpowiedziami z serwisu [zdaj.se](https://zdaj.se/).

## Pobranie zestawu fiszek dla przedmiotów

Aktualnie zestawy do [Anki](https://apps.ankiweb.net/) są dostępne dla wszystkich przedmiotów, które są dostępne w repozytorium [zdaj.se](https://zdaj.se/). Aby pobrać należy mieć utworzone konto na GitHubie i przejść w następujących krokach:

1. W tym repozytorium przejdź do zakładki [Actions](https://github.com/MRmlik12/zdaj-se-anki-generator/actions)
2. Wybrać najnowszą wykonaną akcję, która ma w sobie nazwę joba `Generate decks`
3. W sekcji `Artifacts` pobrać z listy plik `generated-decks` i rozpakować 
4. Zaimportować plik `.apkg` do aplikacji Anki
5. Gotowe!

## Uruchomienie projektu

Do uruchomienia aplikacji wymagane jest zainstalowanie .NET SDK w wersji 9

```bash
$ dotnet restore
$ dotnet run --no-restore --project src/ZdajSeAnkiGenerator.csproj
```

## Dostępne parametry

* `--output-path` - Ścieżka do zapisania kolekcji fiszek
* `--json-file` - Ścieżka do pliku JSON z repozytorium [zdaj.se](https://github.com/bibixx/zdaj-se-pjatk-data)
* `--help` - Wyświetla dostępne parametry

## Podziękowania 

* [zdaj.se](https://zdaj.se/) Za bazę danych pytań i odpowiedzi do przedmiotów <3
