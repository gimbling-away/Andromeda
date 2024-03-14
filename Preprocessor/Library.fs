namespace Andromeda

module Preprocessor =
    [<Struct>]
    type Config =
        { include_directories: string[]
          definitions: Map<string, string> }

        static member Default =
            { include_directories = Array.empty
              definitions = Map.empty }
    
    let preprocess(source, config) : AST =
        failwith "TODO"