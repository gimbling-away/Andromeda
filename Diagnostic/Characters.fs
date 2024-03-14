module Andromeda.Diagnostic.Characters

[<Struct>]
type Characters =
    { hbar: char
      vbar: char
      xbar: char
      vbar_break: char
      vbar_gap: char
      uarrow: char
      rarrow: char
      ltop: char
      mtop: char
      rtop: char
      lbot: char
      rbot: char
      mbot: char
      lbox: char
      rbox: char
      lcross: char
      rcross: char
      underbar: char
      underline: char }

let public unicode =
    { hbar = '─'
      vbar = '│'
      xbar = '┼'
      vbar_break = '┆'
      vbar_gap = '┆'
      uarrow = '▲'
      rarrow = '▶'
      ltop = '╭'
      mtop = '┬'
      rtop = '╮'
      lbot = '╰'
      mbot = '┴'
      rbot = '╯'
      lbox = '['
      rbox = ']'
      lcross = '├'
      rcross = '┤'
      underbar = '┬'
      underline = '─' }

let ascii =
    { hbar = '-'
      vbar = '|'
      xbar = '+'
      vbar_break = '*'
      vbar_gap = ':'
      uarrow = '^'
      rarrow = '>'
      ltop = ','
      mtop = 'v'
      rtop = '.'
      lbot = '`'
      mbot = '^'
      rbot = '\''
      lbox = '['
      rbox = ']'
      lcross = '|'
      rcross = '|'
      underbar = '|'
      underline = '^' }
