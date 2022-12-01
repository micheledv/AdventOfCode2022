module AOC22.Day01.B

open AOC22.Day01.A

let result =
    input |> List.map List.sum |> List.sortDescending |> List.take 3 |> List.sum
