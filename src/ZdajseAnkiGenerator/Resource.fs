module Resource

let getResourceNameFromAssembly (fileName : string) =
    $"ZdajseAnkiGenerator.Assets.{fileName}"

let getResourceFile (fileName : string) =
    let assembly = System.Reflection.Assembly.GetExecutingAssembly()
    let resourceName = getResourceNameFromAssembly fileName
    
    use stream = assembly.GetManifestResourceStream(resourceName)
    use reader = new System.IO.StreamReader(stream)
    
    reader.ReadToEnd()