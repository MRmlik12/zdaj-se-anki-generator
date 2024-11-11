open System.Runtime.Versioning
open App
open Avalonia
open Avalonia.Browser
open Avalonia.Logging

module Program =
    [<assembly: SupportedOSPlatform("browser")>]
    do ()

    [<CompiledName "BuildAvaloniaApp">]
    let buildAvaloniaApp () = 
        AppBuilder
            .Configure<App>()
            .LogToTrace(LogEventLevel.Verbose)

    [<EntryPoint>]
    let main argv =
        buildAvaloniaApp()
            .StartBrowserAppAsync("out")
        |> ignore
        0