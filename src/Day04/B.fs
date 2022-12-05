module AOC22.Day04.B

open AOC22.Day04.A

let overlaps ((firstStart, firstEnd), (secondStart, secondEnd)) =
    let directArray = [| firstStart; firstEnd + 1; secondStart; secondEnd + 1 |]

    let indirectArray = [| secondStart; secondEnd + 1; firstStart; firstEnd + 1 |]

    let sortedArray = Array.sort directArray

    sortedArray <> directArray && sortedArray <> indirectArray

let result = input |> Seq.filter overlaps |> Seq.length
