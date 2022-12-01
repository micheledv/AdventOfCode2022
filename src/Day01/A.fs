module AOC22.Day01.A

open System.IO
open AOC22.Utils

let input =
    let data =
        Path.Join(__SOURCE_DIRECTORY__, "input.txt")
        |> File.ReadAllLines
        |> Seq.map tryParseInt

    let grouper =
        function
        | None ->
            function
            | [] -> []
            | line :: lines -> [] :: line :: lines
        | Some value ->
            function
            | [] -> [ [ value ] ]
            | line :: lines -> (value :: line) :: lines

    Seq.foldBack grouper data []

let result = input |> List.map List.sum |> List.max
