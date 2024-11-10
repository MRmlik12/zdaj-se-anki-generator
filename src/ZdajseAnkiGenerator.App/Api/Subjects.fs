module ZdajseAnkiGenerator.App.Api.Subjects

open System
open System.Net.Http
open System.Net.Http.Json
open System.Text.Json.Serialization

[<Literal>]
let private GITHUB_URL = "https://raw.githubusercontent.com/"
    
[<Literal>]
let private REPOSITORY = "bibixx/zdaj-se-pjatk-data"

[<JsonFSharpConverter>]   
type Subject = {
    [<JsonPropertyName("id")>]
    Id: string
    
    [<JsonPropertyName("title")>]
    Title: string
    
    [<JsonPropertyName("questionsCount")>]
    QuestionsCount: int32
}   

[<JsonFSharpConverter>]   
type Index = {
    [<JsonPropertyName("pages")>]
    Pages: Subject list
}

let fetchSubjects () = async {
    use client = new HttpClient()
    client.BaseAddress <- Uri(GITHUB_URL)
    let! response = client.GetAsync($"{REPOSITORY}/refs/heads/master/index.json") |> Async.AwaitTask
    
    let! deserializedIndex = response.Content.ReadFromJsonAsync<Index>() |> Async.AwaitTask
    
    return deserializedIndex
}
