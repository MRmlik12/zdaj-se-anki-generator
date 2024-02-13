module Arguments

open Argu

type CliArguments =
    | Output_Path of outputPath: string
    | Json_File of json: string
        
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Output_Path _ -> "Output path for anki deck collection package"
            | Json_File _ -> "Downloaded JSON file which contains question and answer data from https://github.com/bibixx/zdaj-se-pjatk-data"
