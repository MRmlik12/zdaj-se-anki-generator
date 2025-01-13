module Generator

open System.IO
open AnkiNet
open FSharp.Json
open Resource
open Types

let getCorrect (checkedField : bool) = if checkedField then "correct" else ""

let getParsedSchema (path : string) = task {
    let! json = File.ReadAllTextAsync path

    return Json.deserialize<Schema> json
}

let sanitizeValue (value : string) = value.Replace(@"""", @"\""").Replace("'", @"''");

let generateDeck (jsonPath : string, outputPath : string) = async {
    let! schema = getParsedSchema jsonPath |> Async.AwaitTask
    let frontTemplate = getResourceFile "front.html"
    let backTemplate = getResourceFile "back.html"
    let cardType = AnkiCardType("Quiz", 0, frontTemplate, backTemplate) 
    
    let noteType = AnkiNoteType(
        "Quiz",
        [| cardType |],
        [| "Front"; "Back" |]
    )
    
    let collection = AnkiCollection()
    let noteTypeId = collection.CreateNoteType(noteType)
    let deckId = collection.CreateDeck(schema.id)
    
    for data in schema.data do
        let answers = data.answers |> Array.mapi (fun i x -> $"<input id='{i}' type='checkbox'><label class='{getCorrect x.correct}'>{x.answer}</label><br>") |> String.concat ""
        let sanitizedCard = sanitizeValue(data.question + "<br>" + answers)
        collection.CreateNote(deckId, noteTypeId, sanitizedCard, sanitizedCard)
   
    let! writeResult = AnkiFileWriter.WriteToFileAsync($"{outputPath}/{schema.id}.apkg", collection) |> Async.AwaitTask
    writeResult
}
 