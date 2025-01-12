module FileService

open System.Threading.Tasks

type IFileService =
    abstract member SaveFile: content: byte[] -> Task