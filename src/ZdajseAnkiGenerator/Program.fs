module Program

open Argu
open Arguments

[<EntryPoint>]
let main argv =
    let parser =
        ArgumentParser.Create<CliArguments>(programName = "Vulder.SchoolScrapper")
        
    // match parser.ParseCommandLine argv with
    // | p when p.Contains(Output_Path) && p.Contains(Json_File) -> Generator.generateDeck(p.GetResult(Json_File), p.GetResult(Output_Path)) |> Async.RunSynchronously
    // | _ -> printfn "%s" (parser.PrintUsage())
    0
