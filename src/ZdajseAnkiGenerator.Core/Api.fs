module Api

open System
open System.Net.Http
open FSharp.Json
open Types

[<Literal>]
let private GITHUB_URL = "https://raw.githubusercontent.com/"
    
[<Literal>]
let private REPOSITORY = "bibixx/zdaj-se-pjatk-data"

let public fetchSubjects () = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/index.json") |> Async.AwaitTask
    
    let! jsonContent = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    let deserializedIndex = Json.deserialize<Index> jsonContent
    
    return deserializedIndex
}

let public fetchSubject (id : string) = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/{id}.json") |> Async.AwaitTask
    
    let! jsonContent = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    let deserializedIndex = Json.deserialize<Schema> jsonContent
    
    return deserializedIndex
}
