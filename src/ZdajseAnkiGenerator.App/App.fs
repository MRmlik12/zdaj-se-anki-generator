module App

open System.Text.Json
open System.Text.Json.Serialization
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open MainView

// FsHttp.GlobalConfig.Json.defaultJsonSerializerOptions <-
//     let options = JsonSerializerOptions()
//     options.Converters.Add(JsonFSharpConverter())
//     options.PropertyNamingPolicy <- JsonNamingPolicy.SnakeCaseLower
//     options 

type MainView() as this =
    inherit UserControl()
    do
        base.Content <- view()

type MainWindow() =
    inherit Window()
    do
        base.Title <- "Zdaj.se Anki Generator"
        base.Content <- MainView()

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme())
        this.RequestedThemeVariant <- Styling.ThemeVariant.Dark

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | :? ISingleViewApplicationLifetime as singleViewLifetime ->
            singleViewLifetime.MainView <- MainView()
        | _ -> ()
        base.OnFrameworkInitializationCompleted()