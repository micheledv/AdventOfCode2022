module AOC22.Day05.A

open System
open System.IO
open System.Text.RegularExpressions

type Command =
    { Quantity: int
      Source: int
      Destination: int }

let parseSchema lines =
    let maxLength = lines |> Seq.maxBy String.length |> String.length

    let columns = (maxLength - 3) / 4 + 1

    let parseLine (line: string) =
        let fullLine = line.PadRight(maxLength, ' ')

        Array.init columns (fun i -> i * 4 + 1)
        |> Array.map (fun offset ->
            match fullLine[offset] with
            | ' ' -> None
            | char -> Some char)

    let folder line state =
        Array.zip state (parseLine line)
        |> Array.map (fun (x, y) ->
            match y with
            | None -> x
            | Some v -> v :: x)

    Seq.foldBack folder lines (Array.create columns List.empty)

let parseCommand line =
    let reMatch = Regex.Match(line, "move (\d+) from (\d+) to (\d+)")

    let extract (i: int) = reMatch.Groups[i].Value |> Int32.Parse

    seq {
        for _ in 1 .. (extract 1) do
            { Quantity = 1
              Source = (extract 2) - 1
              Destination = (extract 3) - 1 }
    }

let input parseCommand =
    let data = Path.Join(__SOURCE_DIRECTORY__, "input.txt") |> File.ReadAllLines

    let blankLineIndex = Array.findIndex (fun (line: string) -> line.Length = 0) data

    let schema = data[.. (blankLineIndex - 2)]
    let commands = data[(blankLineIndex + 1) ..]

    parseSchema schema, Seq.collect parseCommand commands

let processCommand (state: char list []) command =
    let moved, newSource = List.splitAt command.Quantity state[command.Source]

    state[command.Source] <- newSource
    state[command.Destination] <- List.append moved state[command.Destination]
    state

let result =
    input parseCommand
    |> fun (schema, commands) -> Seq.fold processCommand schema commands
    |> Seq.map (List.head >> string)
    |> String.concat ""
