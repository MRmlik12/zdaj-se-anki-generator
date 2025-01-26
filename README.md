# Zdaj.se Anki Generator

Wygeneruj zestaw fiszek do [Anki](https://apps.ankiweb.net/) w formie quizu z pytaniami i odpowiedziami z serwisu [zdaj.se](https://zdaj.se/).

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
