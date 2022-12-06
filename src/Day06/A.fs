module AOC22.Day06.A

open System.IO

let input n =
    let data = Path.Join(__SOURCE_DIRECTORY__, "input.txt") |> File.ReadAllText

    seq { for i in 0 .. (data.Length - n) -> i + n, data.Substring(i, n) }

let run n =
    input n
    |> Seq.find (fun (_, v) -> v.ToCharArray() |> Set.ofArray |> Set.toArray |> Array.length = n)
    |> fun (i, _) -> i

let result = run 4
