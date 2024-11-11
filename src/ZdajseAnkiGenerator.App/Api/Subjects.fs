module ZdajseAnkiGenerator.App.Api.Subjects

open System
open System.Net.Http
open FSharp.Json

[<Literal>]
let private GITHUB_URL = "https://raw.githubusercontent.com/"
    
[<Literal>]
let private REPOSITORY = "bibixx/zdaj-se-pjatk-data"

type Subject = {
    [<JsonField("id")>]
    Id: string
    
    [<JsonField("title")>]
    Title: string
    
    [<JsonField("questionsCount")>]
    QuestionsCount: int32
}   

type Index = {
    [<JsonField("pages")>]
    Pages: Subject list
}

let fetchSubjects () = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/index.json") |> Async.AwaitTask
    
    let! jsonContent = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    let deserializedIndex = Json.deserialize<Index> jsonContent
    
    return deserializedIndex
}
