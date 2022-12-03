module AOC22.Day03.B

open AOC22.Day03.A

let grouper item state =
    match Seq.tryHead state with
    | None -> Seq.singleton [ item ]
    | Some head ->
        if head |> List.length < 3 then
            Seq.append (Seq.singleton (item :: head)) (Seq.tail state)
        else
            Seq.append (Seq.singleton [ item ]) state

let groupedInput = Seq.foldBack grouper input Seq.empty

let findMisplacedItem group =
    group
    |> List.map Set.ofSeq
    |> List.pairwise
    |> List.map (fun (first, second) -> Set.intersect first second)
    |> function
        | [ first; second ] -> Set.intersect first second
        | _ -> failwith "Unexpected group size"
    |> Seq.head

let result =
    groupedInput |> Seq.map (findMisplacedItem >> calculateItemPriority) |> Seq.sum
