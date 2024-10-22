module ZdajseAnkiGenerator.App.Api.Subjects

open System.Text.Json.Serialization
open FsHttp
    
[<Literal>]
let private REPOSITORY = "bibixx/zdaj-se-pjatk-data"

[<JsonFSharpConverter>]   
type Subject = {
    Id: string
    Title: string
    
    [<JsonPropertyName("questionsCount")>]
    QuestionsCount: int32
}   

[<JsonFSharpConverter>]   
type Index = {
    Pages: Subject list
}
// [<JsonFSharpConverter>] 
// type public GithubBlobFile = {
//     Name: string
//     Path: string
//     Sha: string
//     Url: string
//     
//     [<JsonPropertyName("download_url")>]
//     DownloadUrl: string option
//     
//     [<JsonPropertyName("type")>]
//     BlobType: string
// }

let fetchSubjects () = async {
    let! deserializeJsonAsync =
        http {
            GET $"https://raw.githubusercontent.com/{REPOSITORY}/refs/heads/master/index.json"
            Accept "aapplication/json"
            header "User-Agent" "curl/7.72.0"
        }
        |> Request.sendAsync
    let! deserializeJson = deserializeJsonAsync
                            |> Response.deserializeJsonAsync<Index>
        
    return deserializeJson
}

// let fetchBlobFiles() = async {
//     let! deserializeJsonAsync =
//         http {
//             GET REPOSITORY
//             Accept "application/vnd.github+json"
//             header "X-GitHub-Api-Version" "2022-11-28"
//             header "User-Agent" "curl/7.72.0"
//         }
//         |> Request.sendAsync
//
//     let! deserializeJson = deserializeJsonAsync
//                             |> Response.deserializeJsonAsync<GithubBlobFile list>
//         
//     return deserializeJson
// }
    