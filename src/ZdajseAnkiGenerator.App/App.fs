module App

open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open MainView

type MainView() as this =
    inherit UserControl()
    do
        base.Content <- view this

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