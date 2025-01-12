namespace ZdajseAnkiGenerator.Web.FileService

open System.Runtime.InteropServices.JavaScript
open System.Threading.Tasks
open FileService

module FileUtilsEmbed =
    [<JSImport("saveFile", "FileUtils")>]
    extern Task SaveFile(string fileName, byte[] contents)

type BrowserFileService() =
    do 
        Task.Run(fun () -> 
            JSHost.ImportAsync("FileUtils", "fileUtils.js") |> Async.AwaitTask
        ) |> ignore

    interface IFileService with
        member this.SaveFile(content: byte[]) =
            task {
                do! FileUtilsEmbed.SaveFile("test.apkg", content)
            }
