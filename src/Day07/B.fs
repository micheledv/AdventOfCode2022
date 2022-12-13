module AOC22.Day07.B

open AOC22.Day07.A

let totalSpace = 70000000

let neededSpace = 30000000

let result =
    let rootSize = files |> Seq.map (fun file -> file.Size) |> Seq.sum

    directories
    |> Map.filter (fun _ size ->
        let freeSpace = totalSpace - rootSize
        let missingSpace = neededSpace - freeSpace
        size >= missingSpace)
    |> Map.toSeq
    |> Seq.minBy snd
    |> fst
    |> fun directory -> Map.find directory directories
