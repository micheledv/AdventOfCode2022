module AOC22.Utils

open System

let tryParseInt (str: string) =
    match Int32.TryParse str with
    | true, n -> Some n
    | _ -> None
