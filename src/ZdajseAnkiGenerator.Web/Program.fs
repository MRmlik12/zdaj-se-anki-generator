open System.Runtime.Versioning
open App
open Avalonia
open Avalonia.Browser

module Program =
    [<assembly: SupportedOSPlatform("browser")>]
    do ()

    [<CompiledName "BuildAvaloniaApp">]
    let buildAvaloniaApp () = 
        AppBuilder
            .Configure<App>()

    [<EntryPoint>]
    let main argv =
        buildAvaloniaApp()
            .StartBrowserAppAsync("out")
        |> ignore
        0