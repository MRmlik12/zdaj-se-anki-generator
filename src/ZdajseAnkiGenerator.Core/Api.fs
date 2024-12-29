module ZdajseAnkiGenerator.App.Api.Api

open System
open System.Net.Http
open FSharp.Json

[<Literal>]
let private GITHUB_URL = "https://raw.githubusercontent.com/"
    
[<Literal>]
let private REPOSITORY = "bibixx/zdaj-se-pjatk-data"

let fetchSubjects () = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/index.json") |> Async.AwaitTask
    
    let! jsonContent = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    let deserializedIndex = Json.deserialize<Index> jsonContent
    
    return deserializedIndex
}

let fetchSubject (id : string) = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/{id}.json") |> Async.AwaitTask
    
    let! jsonContent = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    let deserializedIndex = Json.deserialize<Index> jsonContent
    
    return deserializedIndex
}
