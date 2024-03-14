namespace Andromeda.Diagnostic

open System
open System.IO
open Andromeda.Diagnostic

[<Struct>]
type CharSet =
    | Unicode
    | Ascii

[<Struct>]
type Level =
    | Error
    | Warning
    | Information

    member this.Code() =
        match this with
        | Error -> 'E'
        | Warning -> 'W'
        | Information -> 'I'

    member this.Colour() =
        match this with
        | Error -> "\x1b[31m"
        | Warning -> "\x1b[33m"
        | Information -> "\x1b[36m"

[<Struct>]
type ExtraKind =
    | Note
    | Help
    | Custom of string

[<Struct>]
type Extra = { kind: ExtraKind; message: string }

[<Struct>]
type Label =
    { start: uint
      finish: uint
      colour: ConsoleColor
      text: string }

[<Struct>]
type Report =
    { code: uint
      level: Level
      title: string
      filename: string
      index: uint32

      mutable labels: Label array
      mutable extras: Extra array

      charset: CharSet
      color: bool
      source: string }

    member this.SortLabels() =
        Array.sortInPlaceBy (_.finish) this.labels

    member this.SortExtras() =
        Array.sortInPlaceBy (_.message.Length) this.extras

    member this.GetLineNumber(offset) =
        let span = this.source.AsSpan().Slice(0, int offset + 1)

        let mutable count = 0

        // We can't use Seq.length because (ReadOnly)Span's enumerator doesn't implement the interface.
        for _ in span.EnumerateLines() do
            count <- count + 1

        count

    member this.PrintHeader(writer: TextWriter) =
        if this.color then
            writer.WriteLine $"{this.level.Colour()}[{this.level.Code()}{this.code}] {this.level}: \x1b[0m{this.title}"
        else
            writer.WriteLine $"[{this.level.Code()}{this.code}] {this.level}: {this.title}"

    member this.Margin() =
        let rec count_digits n count =
            let x = n / 10
            if x <> 0 then count_digits x (count + 1) else count

        count_digits (this.GetLineNumber (Array.last this.labels).finish) 1

    member this.PrintFileName(writer: TextWriter, margin: int) =
        let ltop =
            match this.charset with
            | Unicode -> Characters.unicode.ltop
            | Ascii -> Characters.ascii.ltop

        let hbar =
            match this.charset with
            | Unicode -> Characters.unicode.hbar
            | Ascii -> Characters.ascii.hbar

        let spaces = " " |> String.replicate margin

        writer.WriteLine(
            $"{spaces}\x1b[2;37m{ltop}{hbar}[\x1b[0m{this.filename}:{this.GetLineNumber this.index}\x1b[2;37m]\x1b[0m"
        )

    member this.Emit(writer: TextWriter) =
        this.SortLabels()
        this.SortExtras()
        this.PrintHeader(writer)

        let margin = this.Margin()
        this.PrintFileName(writer, margin)
