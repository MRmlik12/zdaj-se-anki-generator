module Generator

open System.IO
open AnkiNet
open FSharp.Json
open Types

let getChecked (checkedField : bool) = if checkedField then "checked" else ""

let getParsedSchema (path : string) = task {
    let! json = File.ReadAllTextAsync path

    return Json.deserialize<Schema> json
}

let sanitizeValue (value : string) = value.Replace(@"""", @"\""").Replace("'", @"''");

let generateDeck (jsonPath : string, outputPath : string) = async {
    let! schema = getParsedSchema jsonPath |> Async.AwaitTask
    let cardType = AnkiCardType("Basic", 0, "{{Front}}", "{{Front}}<hr id=\"answer\">{{Back}}")
    
    let noteType = AnkiNoteType(
        "Basic",
        [| cardType |],
        [| "Front"; "Back" |]
    )
    
    let collection = AnkiCollection()
    let noteTypeId = collection.CreateNoteType(noteType)
    let deckId = collection.CreateDeck(schema.id)
    
    for data in schema.data do
        let allAnswers = data.answers |> Array.map (fun x -> $"<input type='checkbox'><label>{x.answer}</label><br>") |> String.concat ""
        let correctAnswers = data.answers |> Array.map (fun x -> $"<input type='checkbox' {getChecked x.correct}><label>{x.answer}</label><br>") |> String.concat ""
        collection.CreateNote(deckId, noteTypeId, sanitizeValue(data.question + "<br>" + allAnswers), sanitizeValue(correctAnswers))
   
    let! writeResult = AnkiFileWriter.WriteToFileAsync($"{outputPath}/{schema.id}.apkg", collection) |> Async.AwaitTask
    writeResult
}
 