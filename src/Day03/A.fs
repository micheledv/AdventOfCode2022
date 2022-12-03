module AOC22.Day03.A

open System
open System.IO
open Microsoft.FSharp.Collections

let calculateItemPriority char =
    if Char.ToLower(char) = char then
        int char - int 'a' + 1
    else
        int char - int 'A' + 27

let splitRucksack (rucksack: string) =
    let compartmentLength = rucksack.Length / 2

    let firstHalf = rucksack.Substring(0, compartmentLength)

    let secondHalf = rucksack.Substring(compartmentLength, compartmentLength)

    firstHalf, secondHalf

let findMisplacedItem firstHalf secondHalf =
    Set.intersect (Set.ofSeq firstHalf) (Set.ofSeq secondHalf) |> Seq.head

let input = Path.Join(__SOURCE_DIRECTORY__, "input.txt") |> File.ReadAllLines

let result =
    input
    |> Seq.map (
        splitRucksack
        >> fun (first, second) -> findMisplacedItem first second
        >> calculateItemPriority
    )
    |> Seq.sum
