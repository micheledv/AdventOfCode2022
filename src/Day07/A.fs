module AOC22.Day07.A

open System.Text.RegularExpressions

type File = { Path: string; Size: int }

type State =
    { CurrentDirectory: string
      Files: File list }

let input =
    System.IO.Path.Join(__SOURCE_DIRECTORY__, "input.txt")
    |> System.IO.File.ReadAllLines

let joinPath basepath (filepath: string) =
    System.IO.Path.Join(basepath, filepath.Trim()) |> System.IO.Path.GetFullPath

let foldFiles state (line: string) =
    if line.StartsWith("$ ls") then
        state
    elif line.StartsWith("$ cd") then
        match line[4..] with
        | "/" -> { state with CurrentDirectory = System.IO.Path.DirectorySeparatorChar |> string }
        | path -> { state with CurrentDirectory = joinPath state.CurrentDirectory path }
    else if line.StartsWith("dir") then
        state
    else
        let groups = Regex("(\d+) (.+)").Match(line).Groups

        let file =
            { Path = joinPath state.CurrentDirectory groups[2].Value
              Size = System.Int32.Parse(groups[1].Value) }

        { state with Files = file :: state.Files }

let ancestors file =
    let rawAncestors = file.Path.Split(System.IO.Path.DirectorySeparatorChar)

    let folder =
        function
        | [] -> fun item -> [ joinPath (System.IO.Path.DirectorySeparatorChar |> string) item ]
        | x :: xs -> fun item -> [ joinPath x item ] @ (x :: xs)

    rawAncestors[0 .. (Array.length rawAncestors) - 2]
    |> Array.filter (fun item -> String.length item > 0)
    |> Array.fold folder []

let aggregateDirectories map file =
    let folder map directory =
        Map.change
            directory
            (function
            | None -> Some file.Size
            | Some size -> Some(size + file.Size))
            map

    file |> ancestors |> Seq.fold folder map

let files =
    input
    |> Seq.fold foldFiles { CurrentDirectory = "/"; Files = [] }
    |> (fun state -> state.Files)

let directories = files |> Seq.fold aggregateDirectories Map.empty

let result =
    directories
    |> Map.filter (fun _ size -> size <= 100000)
    |> Map.values
    |> Seq.sum
