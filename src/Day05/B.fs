module AOC22.Day05.B

open System
open AOC22.Day05.A

open System.Text.RegularExpressions

let parseCommand line =
    let reMatch = Regex.Match(line, "move (\d+) from (\d+) to (\d+)")

    let extract (i: int) = reMatch.Groups[i].Value |> Int32.Parse

    seq {
        { Quantity = extract 1
          Source = (extract 2) - 1
          Destination = (extract 3) - 1 }
    }

let result =
    input parseCommand
    |> fun (schema, commands) -> Seq.fold processCommand schema commands
    |> Seq.map (List.head >> string)
    |> String.concat ""
