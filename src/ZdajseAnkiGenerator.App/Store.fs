module Store

open FileService

let mutable fileServiceInstance: IFileService option = None 
