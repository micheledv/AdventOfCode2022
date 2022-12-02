module AOC22.Day02.A

open System.IO

type Shape =
    | Rock
    | Paper
    | Scissor

type Outcome =
    | Victory
    | Defeat
    | Draw

let outcomeForGame myShape theirShape =
    if myShape = theirShape then
        Draw
    else
        match (myShape, theirShape) with
        | Paper, Rock -> Victory
        | Scissor, Paper -> Victory
        | Rock, Scissor -> Victory
        | _ -> Defeat

let scoreForShape =
    function
    | Rock -> 1
    | Paper -> 2
    | Scissor -> 3

let scoreForOutcome =
    function
    | Victory -> 6
    | Defeat -> 0
    | Draw -> 3

let totalScore (myShape, outcome) =
    scoreForOutcome outcome + scoreForShape myShape

let parseShape =
    function
    | "A" -> Rock
    | "B" -> Paper
    | "C" -> Scissor
    | "X" -> Rock
    | "Y" -> Paper
    | "Z" -> Scissor
    | _ -> failwith "Unexpected shape"

let input = Path.Join(__SOURCE_DIRECTORY__, "input.txt") |> File.ReadAllLines

let parseLine (line: string) =
    let tokens = line.Split(" ")
    let myShape = parseShape tokens[1]
    let theirShape = parseShape tokens[0]

    myShape, theirShape

let normalize (myShape, theirShape) =
    myShape, outcomeForGame myShape theirShape

let result = input |> Seq.map (parseLine >> normalize >> totalScore) |> Seq.sum
