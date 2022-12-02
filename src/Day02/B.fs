module AOC22.Day02.B

open AOC22.Day02.A

let parseOutcome =
    function
    | "X" -> Defeat
    | "Y" -> Draw
    | "Z" -> Victory
    | _ -> failwith "Unexpected outcome"

let myShapeForOutcome outcome theirShape =
    match (outcome, theirShape) with
    | Victory, Rock -> Paper
    | Victory, Scissor -> Rock
    | Victory, Paper -> Scissor
    | Defeat, Rock -> Scissor
    | Defeat, Scissor -> Paper
    | Defeat, Paper -> Rock
    | Draw, _ -> theirShape

let parseLine (line: string) =
    let tokens = line.Split(" ")
    let theirShape = parseShape tokens[0]
    let outcome = parseOutcome tokens[1]

    outcome, theirShape

let normalize (outcome, theirShape) =
    (myShapeForOutcome outcome theirShape), outcome

let result = input |> Seq.map (parseLine >> normalize >> totalScore) |> Seq.sum
