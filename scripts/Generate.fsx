open System
open System.Diagnostics
open System.IO

[<Literal>]
let outputPath = "zdajse-collections"

let repositoryPath = Environment.GetEnvironmentVariable("DATA_REPOSITORY_PATH")

let getAllFiles path = Directory.GetFiles(path, "*")

let createOutputDirectory () =
    if not (Directory.Exists(outputPath)) then
        Directory.CreateDirectory(outputPath) |> ignore

let runProgram jsonPath =
   printfn "Running program with json file: %s" jsonPath
   let program = Process.Start("dotnet", $"run --no-restore --project src/ZdajseAnkiGenerator/ZdajseAnkiGenerator.fsproj --json-file {jsonPath} --output-path {outputPath}")
   program.WaitForExit()
   
   if program.ExitCode <> 0 then
       printfn "An error occurred while running the program with json file: %s" jsonPath
   else
        printfn "Program ran successfully with json file: %s" jsonPath

printfn "Generating anki decks from %s" repositoryPath

createOutputDirectory()

getAllFiles repositoryPath
|> Array.filter (fun x -> not (x.Contains("index.json")) && x.EndsWith(".json"))
|> Array.iter runProgram

printfn "Operation successfull"