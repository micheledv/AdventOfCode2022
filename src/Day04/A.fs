module AOC22.Day04.A

open System
open System.IO

let parseLine (line: string) =
    line.Split(",")
    |> Array.collect (fun (range: string) -> range.Split("-"))
    |> Array.map Int32.Parse
    |> function
        | [| firstStart; firstEnd; secondStart; secondEnd |] -> (firstStart, firstEnd), (secondStart, secondEnd)
        | _ -> failwith "Unexpected line format"

let input =
    Path.Join(__SOURCE_DIRECTORY__, "input.txt")
    |> File.ReadAllLines
    |> Seq.map parseLine

let fullyContains ((firstStart, firstEnd), (secondStart, secondEnd)) =
    let firstContainsSecond = firstStart <= secondStart && firstEnd >= secondEnd

    let secondContainsFirst = firstStart >= secondStart && firstEnd <= secondEnd

    firstContainsSecond || secondContainsFirst

let result = input |> Seq.filter fullyContains |> Seq.length
