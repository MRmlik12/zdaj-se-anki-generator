namespace ZdajseAnkiGenerator.Web

open System.Runtime.Versioning
open App
open Avalonia
open Avalonia.Browser
open Avalonia.Logging
open Store
open ZdajseAnkiGenerator.Web.FileService

module Program =
    [<assembly: SupportedOSPlatform("browser")>]
    do ()

    [<CompiledName "BuildAvaloniaApp">]
    let buildAvaloniaApp () =
        fileServiceInstance <- Some (BrowserFileService())
        AppBuilder
            .Configure<App>()
            .LogToTrace(LogEventLevel.Verbose)

    [<EntryPoint>]
    let main argv =
        buildAvaloniaApp()
            .StartBrowserAppAsync("out")
        |> ignore
        0